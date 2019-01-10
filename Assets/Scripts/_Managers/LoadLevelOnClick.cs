using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelOnClick : MonoBehaviour
{

    public bool isLoading;
    public bool isCinematic;
    public float timeRate;
    public string sceneName;
    public AudioClip start_BSO;
    public AudioClip start_SFX;
    // Use this for initialization
    void Start()
    {
        if (isLoading) StartCoroutine(ChangeScene());
        if (isCinematic) StartCoroutine(BeginGame());
    }

    void Update()
    {
        
    }

    IEnumerator ChangeScene()
    {

        // yield return new WaitForSeconds(1);
        GameManager.manager.soundMng.SetCurrentEffect(start_SFX);
        yield return new WaitForSeconds(timeRate);
        //Camera.main.gameObject.GetComponent<Animator>().SetTrigger("Active");
       // Fader.SetupAsDefaultFader(); Fader.Instance.SetColor(Color.black).FadeIn(.3f).Pause(.4f).FadeOut(.3f);
        //Camera.main.GetComponent<SerafinCore.ScreenTransitionEffectManager>().StartFade(SerafinCore.TransitionType.Normal);
        yield return new WaitForSeconds(.5f);
        GameManager.manager.soundMng.SetCurrentBSO(start_BSO);
        GameManager.manager.state.SetCurrentState("Presentation");
        SceneManager.LoadScene(sceneName);
    }
    IEnumerator BeginGame()
    {
        yield return new WaitForSeconds(timeRate);
        //Camera.main.gameObject.GetComponent<Animator>().SetTrigger("Active");
       // Fader.SetupAsDefaultFader(); Fader.Instance.SetColor(Color.black).FadeIn(.3f).Pause(.4f).FadeOut(.3f);
        //Camera.main.GetComponent<SerafinCore.ScreenTransitionEffectManager>().StartFade(SerafinCore.TransitionType.Normal);
        Camera.main.gameObject.GetComponent<Animator>().SetTrigger("Active");
        yield return new WaitForSeconds(0.35F);
        GameManager.manager.soundMng.SetCurrentBSO(start_BSO);
        //GameManager.manager.state.SetCurrentState("InGame");
        SceneManager.LoadScene(sceneName);
    }
    public void LoadScene()
    {
        StartCoroutine(ChangeScene());
    }

}
