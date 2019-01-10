using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlatformManager : MonoBehaviour
{
    public static PlatformManager destructor;

    [HeaderAttribute("InGame Assets")]
    public Transform lastPoint;    
    public GameObject[] sectorLv1;
    public GameObject[] sectorLv2;
    public GameObject[] sectorLv3;
    public GameObject emptyGround;
   public bool endGame;

    [SpaceAttribute]
    [HeaderAttribute("Tutorial Attributes")]
    public bool isTutorial;
    public bool jumpComplete;
    public bool platformComplete;
    public bool gravityComplete;
    public bool completeTutorial;
    public Text tutorialOrders;
    public Text tutorialValues;
    public string [] tutorialTextArray;
    public int jump;
    public int platform;
    public int gravity;
    public bool [] assistant;
    public bool assistantActive;

    // Use this for initialization
    void Start()
    {
        if (destructor == null) destructor = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTutorial)
        {
            if(platform >= 3) PlatformManager.destructor.platformComplete = true;
            if(jump >= 3) PlatformManager.destructor.jumpComplete = true;
            if(gravity >= 1) PlatformManager.destructor.gravityComplete = true;
            
            if(completeTutorial) tutorialValues.text = ""; 
            else TutorialTextUI();
        }

    }

 
    public void GeneratePlatform()
    {
        GameObject clone;

        if(!isTutorial && Time.timeScale == 1)
            switch (GameManager.manager.difficuly)
            {
                case Difficuly.Easy:
                    clone = Instantiate(sectorLv1[Random.Range(0, sectorLv1.Length)], lastPoint.position, lastPoint.rotation) as GameObject;
                    lastPoint = clone.transform.GetChild(0);
                    break;
                case Difficuly.Medium:
                    clone = Instantiate(sectorLv2[Random.Range(0, sectorLv2.Length)], lastPoint.position, lastPoint.rotation) as GameObject;
                    lastPoint = clone.transform.GetChild(0);
                    break;
                case Difficuly.Hard:
                    clone = Instantiate(sectorLv3[Random.Range(0, sectorLv3.Length)], lastPoint.position, lastPoint.rotation) as GameObject;
                    lastPoint = clone.transform.GetChild(0);
                    break;
            }
        else if(isTutorial && Time.timeScale == 1){
            if(jumpComplete && !platformComplete && !gravityComplete && assistant[1])
            {
                clone = Instantiate(sectorLv1[1], lastPoint.position, lastPoint.rotation) as GameObject;
                lastPoint = clone.transform.GetChild(0);
            }
            else if(jumpComplete && platformComplete && !gravityComplete  && assistant[2])
            {
                clone = Instantiate(sectorLv1[2], lastPoint.position, lastPoint.rotation) as GameObject;
                lastPoint = clone.transform.GetChild(0);
            }
            else if(!jumpComplete && !platformComplete && !gravityComplete  && assistant[0])
            {
                clone = Instantiate(sectorLv1[0], lastPoint.position, lastPoint.rotation) as GameObject;
                lastPoint = clone.transform.GetChild(0);
            }
            else
            {
                clone = Instantiate(sectorLv1[0], lastPoint.position, lastPoint.rotation) as GameObject;
                lastPoint = clone.transform.GetChild(0);
            }

            if(jumpComplete && platformComplete && gravityComplete)
            {
                clone = Instantiate(sectorLv2[Random.Range(0, sectorLv2.Length)], lastPoint.position, lastPoint.rotation) as GameObject;
                lastPoint = clone.transform.GetChild(0);
            }
            
        }
        else 
        {
            clone = Instantiate(emptyGround, lastPoint.position, lastPoint.rotation) as GameObject;
            lastPoint = clone.transform.GetChild(0);
        }
        

    }

    void TutorialTextUI()
    {
        if(!assistantActive && !completeTutorial) StartCoroutine(TutorialCO());

        if((assistant[0] && !assistant[1] && !assistant[2]) && !assistantActive && !completeTutorial) tutorialValues.text = "("+ jump.ToString() + "/3)";
        else if((assistant[0] && assistant[1] && !assistant[2]) && !assistantActive && !completeTutorial) tutorialValues.text = "("+ platform.ToString() + "/3)"; 
        else if((assistant[0] && assistant[1] && assistant[2]) && !assistantActive && !completeTutorial) tutorialValues.text = "("+ gravity.ToString() + "/1)"; 


    }

    IEnumerator TutorialCO()
    {
        
            if(!jumpComplete && !platformComplete && !gravityComplete && !assistant[0])
            {   
                assistantActive = true;
                tutorialOrders.text = ""; 
                tutorialValues.text = "";  
                yield return new WaitForSeconds(2);                     
                tutorialOrders.text = "Darkness is coming!";
                yield return new WaitForSeconds(2);
                tutorialOrders.text = "You need understand your new powers";
                yield return new WaitForSeconds(2);
                tutorialOrders.text = "Touch the screen to jump";
                yield return new WaitForSeconds(3);
                tutorialOrders.text = "";
                yield return new WaitForSeconds(1);
                tutorialOrders.text = tutorialTextArray[0];
                assistant[0] = true;
                assistantActive = false;
            }
            else if(jumpComplete && !platformComplete && !gravityComplete && !assistant[1])
            { 
                assistantActive = true;
                tutorialValues.text = ""; 
                tutorialOrders.text = "";
                // yield return new WaitForSeconds(2);
                tutorialOrders.text = "Congratulations!";
                yield return new WaitForSeconds(1);
                tutorialOrders.text = "You will have some obstacles. You must jump to avoid them";
                yield return new WaitForSeconds(3);
                tutorialOrders.text = "";
                yield return new WaitForSeconds(1);
                tutorialOrders.text = tutorialTextArray[1];
                assistant[1] = true;
                assistantActive = false;
            }
            else if(jumpComplete && platformComplete && !gravityComplete && !assistant[2])
            {           
                assistantActive = true;     
                tutorialValues.text = ""; 
                tutorialOrders.text = "";
                // yield return new WaitForSeconds(2);
                tutorialOrders.text = "Congratulations!";
                yield return new WaitForSeconds(1);
                tutorialOrders.text = "Touch the icon to change your gravity and travel to other ground";                
                yield return new WaitForSeconds(3);
                tutorialOrders.text = "";
                yield return new WaitForSeconds(1);
                tutorialOrders.text = tutorialTextArray[2];
                GameManager.manager.ui.uiTutorial.SetActive(true);
                assistant[2] = true;
                assistantActive = false;
            }            
            else if(jumpComplete && platformComplete && gravityComplete && !assistant[3])
            {
                assistantActive = true;
                tutorialValues.text = ""; 
                tutorialOrders.text = "";
                // yield return new WaitForSeconds(2);
                tutorialOrders.text = "Congratulations!";
                yield return new WaitForSeconds(1);
                tutorialOrders.text = "You are ready to face the darkness. Good luck!";
                yield return new WaitForSeconds(2);
                tutorialValues.text = ""; 
                tutorialOrders.text = "";
                assistant[3] = true;
                assistantActive = false;
                completeTutorial = true;
                GameManager.manager.gameOver = true;
                GameManager.manager.player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                GameManager.manager.player.GetComponent<Rigidbody2D>().velocity = new Vector2(8, 0);
                yield return new WaitForSeconds(2);
                GameManager.manager.state.previousState = StateMachine.State.GameOver;
                GameManager.manager.state.SetCurrentState("Tutorial");
                GameManager.manager.state.tutorialWindow.SetActive(false);
                GameManager.manager.state.gameMenu.SetActive(true);
                Camera.main.gameObject.GetComponent<Animator>().SetTrigger("Active");
                yield return new WaitForSeconds(0.3F);
                SceneManager.LoadScene(2);
            }
    }
   
}
