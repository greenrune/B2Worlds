using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public enum JumpTime { GoingUp, GoingDown, Normal }
public class PlayerStatus : MonoBehaviour
{

    // public float shangeSpeed;
    public JumpTime jumpVector;
    public Vector2 jumpForce = new Vector2(0, 300);
    // public GameObject [] shadow;
    public bool doubleJump;
    public AudioClip jumpFX;
    public AudioClip coinFX;
    public AudioClip powerUpFX;
    public GameObject playerDead;
    public GameObject dust;
    //public ParticleSystem distorsion;
    public ParticleSystem switchExplosion;
    public Transform dustSpawner;
    private Rigidbody2D body;
    public Animator anim;
    
    public bool multiplyCoins;
    public GameObject magnet;
    public GameObject fastParticle;
    
    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        GameManager.manager.player = this;
        if(!PlatformManager.destructor.isTutorial) GameManager.manager.ui.gravityButton.SetActive(true);
        //GameManager.manager.ui.gravityButton.GetComponent<GEAnim>().MoveIn();
        // anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (body == null) body = GetComponent<Rigidbody2D>();
        // if (Input.GetMouseButtonDown(0) && Time.timeScale != 0 && !doubleJump)
        // {
        //     GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //     GetComponent<Rigidbody2D>().AddForce(jumpForce, ForceMode2D.Impulse);
        //     doubleJump = true;
        //     GameManager.manager.soundMng.SetCurrentEffect(jumpFX);
        //     //anim.SetBool("Jump", true);
        //     GameObject clone = Instantiate(dust, new Vector3(dustSpawner.position.x, dustSpawner.position.y, -9), dustSpawner.rotation) as GameObject;
        //     if (GameManager.manager.worldSide == playerRot.Up) clone.transform.localScale = new Vector3(-clone.transform.localScale.x, -clone.transform.localScale.y, -clone.transform.localScale.z);
        
        //     if(PlatformManager.destructor.isTutorial){
        //         jump ++;
        //         if(jump >= 3) PlatformManager.destructor.jumpComplete = true;
        //     }
        // }

         if(Input.GetMouseButton(0))
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if(raycastResults.Count > 0)
            {
                foreach(var go in raycastResults)
                {  
                    Debug.Log(go.gameObject.name,go.gameObject);
                }
            }
            else Jump();
        }

        if(Time.timeScale > 1) 
        {
            fastParticle.SetActive(true);
        }
        else fastParticle.SetActive(false);
        //if (GetComponent<Rigidbody2D>().velocity.y < 0) anim.SetTrigger("Down");
        // else  anim.SetTrigger("Down");
    }

    public void ActivateMagnet()
    {
        magnet.SetActive(true);
    }
    void Jump()
    {
        if (Time.timeScale != 0 && !doubleJump && Time.timeScale == 1)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(jumpForce, ForceMode2D.Impulse);
            doubleJump = true;
            GameManager.manager.soundMng.SetCurrentEffect(jumpFX);
            //anim.SetBool("Jump", true);
            GameObject clone = Instantiate(dust, new Vector3(dustSpawner.position.x, dustSpawner.position.y, -9), dustSpawner.rotation) as GameObject;
            if (GameManager.manager.worldSide == playerRot.Up) clone.transform.localScale = new Vector3(-clone.transform.localScale.x, -clone.transform.localScale.y, -clone.transform.localScale.z);
            if(PlatformManager.destructor.isTutorial && PlatformManager.destructor.assistant[0]) PlatformManager.destructor.jump++;
            
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Name: " + eventData.pointerCurrentRaycast.gameObject.name);
    }
    public void UpdateWorld(playerRot _side)
    {
        StartCoroutine(UpdateWorld_CO(_side));
         if(PlatformManager.destructor.isTutorial){
                PlatformManager.destructor.gravity++;
            }
    }
    public IEnumerator UpdateWorld_CO(playerRot _side)
    {

        switch (_side)
        {
            case playerRot.Up:
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250), ForceMode2D.Impulse);
                yield return new WaitForSeconds(0.15f);
                body.gravityScale *= -1.5f;
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, -transform.localScale.z);
                jumpForce.y = -500;
                // shadow[0].SetActive(false);
                // shadow[1].SetActive(true);
                break;
            case playerRot.Down:
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -250), ForceMode2D.Impulse);
                yield return new WaitForSeconds(0.15f);
                body.gravityScale *= -1.5f;
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, -transform.localScale.z);
                jumpForce.y = 500;
                // shadow[0].SetActive(true);
                // shadow[1].SetActive(false);
                break;
        }
        //distorsion.Play();
        GameManager.manager.quest.AddProgressToAchievement("Worlds", 1);
        switchExplosion.Play();
        StartCoroutine(UpdateGravity());
    }

    IEnumerator UpdateGravity()
    {
        yield return new WaitForSeconds(0.5F);
        if (GameManager.manager.worldSide == playerRot.Down) GetComponent<Rigidbody2D>().gravityScale = 6;
        else GetComponent<Rigidbody2D>().gravityScale = -6;
    }

    public void ResetPosition()
    {
        transform.position = new Vector2(transform.position.x, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("GroundPart")) && doubleJump)
        {
            doubleJump = false;
        }
        
        if (other.gameObject.CompareTag("PowerUp"))
        {
            //GameManager.manager.ui.gravityButton.SetActive(true);
            //GameManager.manager.ui.gravityButton.GetComponent<GEAnim>().MoveIn();
            GameManager.manager.soundMng.SetCurrentEffect(powerUpFX);
            Destroy(other.gameObject);
        }
        
    }

    //void OnCollisionStay2D(Collision2D other)
    //{
    //          anim.SetBool("Jump", false);

    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.gameObject.CompareTag("Platform"))
        // {
        //     doubleJump = false;

        //     //dust.Play();
        // }
        if (other.gameObject.CompareTag("Tutorial"))
        {
            if(PlatformManager.destructor.isTutorial){
                PlatformManager.destructor.platform ++;                
            }
        }        
        else if (other.gameObject.CompareTag("Attack"))
        {
            Debug.Log("Death");
            GameManager.manager.state.SetCurrentState("GameOver");
            GameManager.manager.gameOver = true;

            GameObject clone = Instantiate(playerDead, transform.position, transform.rotation) as GameObject;
            clone.transform.localScale = transform.localScale;
            clone.GetComponent<Rigidbody2D>().gravityScale = GetComponent<Rigidbody2D>().gravityScale;
            clone.GetComponent<Rigidbody2D>().AddForce(jumpForce, ForceMode2D.Impulse);
            gameObject.SetActive(false);

        }
        else if (other.gameObject.CompareTag("PowerUp"))
        {
            Debug.Log("Power Up");
        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            if(!multiplyCoins) GameManager.manager.CoinAverage(1);
            else GameManager.manager.CoinAverage(2);
            GameManager.manager.soundMng.SetCurrentEffect(coinFX);
            Destroy(other.gameObject);
        }
    }
}
