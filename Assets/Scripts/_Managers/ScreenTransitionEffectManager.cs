using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SerafinCore
{
    public delegate float EaseCurveFunction(float start, float end, float value);
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Screen Transition")]
    public class ScreenTransitionEffectManager : MonoBehaviour
    {
        public float fadeSpeed = 4.8f;
        private float selectedSpeed;
        public float fadeWait = .1f;
        public Shader shader;

        [Range(0,1.0f)]
        public float maskValue;
        public Color maskColor = Color.black;
        public Texture2D maskTexture;
        public bool maskInvert;

        public Image FadeImage;
        private Object[] maskTextures;
        private Material m_Material;
        private bool m_maskInvert;
        private EaseCurveFunction transitionFunction;

        private bool fadeScene;

        Material material
        {
            get
            {
                if (m_Material == null)
                {
                    m_Material = new Material(shader);
                    m_Material.hideFlags = HideFlags.HideAndDontSave;
                }
                return m_Material;
            }
        }

        void Awake()
        {
            // Disable if we don't support image effects
            if (!SystemInfo.supportsImageEffects)
            {
                enabled = false;
                return;
            }

            shader = Shader.Find("Hidden/ScreenTransitionImageEffect");

            // Disable the image effect if the shader can't
            // run on the users graphics card
            if (shader == null || !shader.isSupported)
                enabled = false;

            maskTextures = Resources.LoadAll("Serafin/Masks", typeof(Texture2D));
            fadeScene = false;
        }

        void OnDisable()
        {
            if (m_Material)
            {
                DestroyImmediate(m_Material);
            }
        }
        
        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (!enabled)
            {
                Graphics.Blit(source, destination);
                return;
            }

            material.SetColor("_MaskColor", maskColor);
            material.SetFloat("_MaskValue", maskValue);
            material.SetTexture("_MainTex", source);
            material.SetTexture("_MaskTex", maskTexture);

            if (material.IsKeywordEnabled("INVERT_MASK") != maskInvert)
            {
                if (maskInvert)
                    material.EnableKeyword("INVERT_MASK");
                else
                    material.DisableKeyword("INVERT_MASK");
            }

            Graphics.Blit(source, destination, material);
        }

        public void StartFade(TransitionType type)
        {
            if (!SetupTransition(type)) return;
            fadeScene = false;
            StopAllCoroutines();
            selectedSpeed = fadeSpeed;
            StartCoroutine(FadeView());
        }

        public void StartFade(TransitionType type, bool entry)
        {
            if (!SetupTransition(type)) return;
            fadeScene = true;
            selectedSpeed = fadeSpeed / 2.0f;
            StopAllCoroutines();
            if (entry)
                StartCoroutine(FadeOutScene());
            else
                StartCoroutine(FadeInScene());
        }

        bool SetupTransition(TransitionType type)
        {
            switch (type)
            {
                case TransitionType.None:
                    return false;
                case TransitionType.Normal:
                    maskTexture = null;
                    break;
                default:
                    maskTexture = maskTextures[(int)type - 2] as Texture2D;
                    break;
            }
            // transitionFunction = SerafinUtility.clerp;
            return true;
        }

        IEnumerator FadeInScene()
        {
            yield return StartCoroutine(FadeIn());
            ScreenTransitionsEventManager.dispatchOnEndedFadeIn();
            yield break;
        }

        IEnumerator FadeOutScene()
        {
            yield return StartCoroutine(FadeOut());
            ScreenTransitionsEventManager.dispatchOnEndedFadeOut();
            yield break;
        }

        public void FadeScene()
        {
            StartCoroutine(FadeView());
        }
        IEnumerator FadeView()
        {
            yield return StartCoroutine(FadeIn());
            ScreenTransitionsEventManager.dispatchOnEndedFadeInOut();
            yield return new WaitForSeconds(fadeWait);
            yield return StartCoroutine(FadeOut());
            yield break;
        }

        IEnumerator FadeIn()
        {
            if (maskTexture == null)
            {
                FadeImage.gameObject.SetActive(true);
                FadeImage.color = Color.clear;
                while (FadeImage.color.a <= 1)
                {
                    FadeToBlack();
                    yield return null;
                }
            }
            else
            {
                FadeImage.gameObject.SetActive(false);
                maskValue = 0;
                while (maskValue <= 1)
                {
                    maskValue = transitionFunction(maskValue, 1.1f, selectedSpeed * Time.deltaTime);
                    yield return null;
                }
                if (fadeScene)
                    maskValue = 0;
            }
            yield break;
        }

        IEnumerator FadeOut()
        {
            if (maskTexture == null)
            {
                FadeImage.color = maskColor;
                while (FadeImage.color.a >= 0)
                {
                    FadeToClear();
                    yield return null;
                }
                FadeImage.gameObject.SetActive(false);
            }
            else
            {
                FadeImage.gameObject.SetActive(false);
                maskValue = 1;
                while (maskValue >= 0)
                {
                    maskValue = transitionFunction(maskValue, -0.2f, selectedSpeed * Time.deltaTime);
                    yield return null;
                }
            }
            yield break;
        }
        
        void FadeToClear()
        {
            // Lerp the alpha-colour of the image between itself and transparent.
            //FadeImage.color = Color.Lerp(maskColor, Color.clear, fadeSpeed * Time.deltaTime);
            Color tempColor = FadeImage.color;
            float alpha = tempColor.a;
            alpha = transitionFunction(alpha, -0.2f, selectedSpeed * Time.deltaTime);
            FadeImage.color = new Color(tempColor.r, tempColor.g, tempColor.b, alpha);
        }

        void FadeToBlack()
        {
            // Lerp the alpha-colour of the image between black and itself.
            //FadeImage.color = Color.Lerp(Color.clear, maskColor, fadeSpeed * Time.deltaTime);
            Color tempColor = FadeImage.color;
            float alpha = tempColor.a;
            alpha = transitionFunction(alpha, 1.1f, selectedSpeed * Time.deltaTime);
            FadeImage.color = new Color(tempColor.r, tempColor.g, tempColor.b, alpha);
        }
    }

    public class ScreenTransitionsEventManager
    {
        public delegate void EndedFadeInOut();
        public static event EndedFadeInOut onEndedFadeInOut;
        public static void dispatchOnEndedFadeInOut()
        {
            if (onEndedFadeInOut != null) onEndedFadeInOut();
        }

        public static event EndedFadeInOut onEndedFadeIn;
        public static void dispatchOnEndedFadeIn()
        {
            if (onEndedFadeIn != null) onEndedFadeIn();
        }

        public static event EndedFadeInOut onEndedFadeOut;
        public static void dispatchOnEndedFadeOut()
        {
            if (onEndedFadeOut != null) onEndedFadeOut();
        }
    }
}