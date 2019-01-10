using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Start,
        InGame,
        PauseGame,
        Revive,
        GameOver,
        Presentation,
        Restart, 
        Tutorial
    }

    Animator animatorStateMachine;
    private Dictionary<int, State> battleStateHash = new Dictionary<int, State>();
    private Dictionary<string, State> battleStateStringHash = new Dictionary<string, State>();
    public State currentState, previousState;
    public GameObject startMenu, gameMenu, pauseMenu, reviveMenu, gameOverMenu, adsButton, PresentationWindow, playButton, tutorialWindow, enhancer_Panel;    
    public AudioClip swip_SFX, gameOver_SFX, game_BSO, start_BSO;
    public Text waitTime;
    public Color windowColor;
    void Awake()
    {
        // animatorStateMachine = GetComponent<Animator>();
        // if (!animatorStateMachine || !animatorStateMachine.runtimeAnimatorController)
        // { Debug.LogError("Falta la maquina de estados o no ha sido configurada"); }
    }

    void Start()
    {
        // Fader.SetupAsDefaultFader();
        // Fader.Instance.FadeOut(0.5F);
        // Se cachean todos los estados de la makina de estados (Case Sensitive!)
        State[] states = (State[])System.Enum.GetValues(typeof(State));
        foreach (State state in states)
        {
            battleStateHash.Add(Animator.StringToHash("Base Layer." + state.ToString()), state);
            battleStateStringHash.Add(state.ToString(), state);
        }


        currentState = State.Start;
        StartState();

        if (!PlayerPrefs.HasKey("stateMachine"))
            PlayerPrefs.SetString("stateMachine", "Start");
        else if (PlayerPrefs.GetString("stateMachine") == "Restart")
        {
            // usado para actualizar el estado previo
            currentState = State.Start;
            //missionPanel.GetComponentInParent<MissionGUIController>().LoadCurrentMissionsData();
            SetCurrentState("InGame");
        }

        // GameManager.manager.TimeScaleGame(0);
    }

    public void SetCurrentState(string stateName)
    {
        //if (GameManager.manager.currentDailyIntents > 0 && stateName == "Missions")
        //{
        previousState = currentState;
        PlayerPrefs.SetString("stateMachine", stateName);
        // animatorStateMachine.SetTrigger(stateName);
        currentState = battleStateStringHash[stateName];
        SendMessage(currentState.ToString() + "State", SendMessageOptions.DontRequireReceiver);
        //}
    }

    void StartState()
    {
        // if (previousState == State.Start) GameManager.manager.TimeScaleGame(1);
        //playButton.MoveIn();
        //Text[] allText = playButton.gameObject.transform.GetComponentsInChildren<Text>();
        //GameManager.manager.animMaster.StartAnim ();

        if (previousState == State.GameOver || previousState == State.PauseGame)
        {
            StartCoroutine(StartGame());
        }
    }
    void TutorialState()
    {

        if (previousState == State.InGame)
        {
            GameManager.manager.worldSide = playerRot.Down;
                
            GameManager.manager.distance = 0;
            GameManager.manager.coins = 0;
            GameManager.manager.ui.CancelUpdates();
            GameManager.manager.ui.UpdateCoin(0);
            GameManager.manager.gameOver = false;
            GameManager.manager.startGame = true;
            GameManager.manager.platformVelicity = new Vector2(-8, 0);
            StartCoroutine(StartGame_CO());
        }
    }
    void PresentationState()
    {
        StartCoroutine(PresentationCR());
    }

    IEnumerator PresentationCR()
    {
        startMenu.SetActive(true);
        yield return new WaitForSeconds(3);
        PresentationWindow.SetActive(false);
    }
    void RestartState()
    {
        if (previousState == State.GameOver)
        {
            GameManager.manager.soundMng.SetCurrentBSO(game_BSO);
            GameManager.manager.distance = 0;
            GameManager.manager.coins = 0;
            StartCoroutine(RestartGame());
            GameManager.manager.platformVelicity = new Vector2(-8, 0);
        }
        else
        {
            startMenu.SetActive(false);
            gameMenu.SetActive(true);
            GameManager.manager.TimeScaleGame(1);
            SetCurrentState("InGame");
        }
    }
    void InGameState()
    {
        
        // camManager.SetCurrentCamera(0);
        print("InGameState");
        if(PlayerPrefs.GetInt("Presentation") == 0) 
        {
            StartCoroutine(StartPresentation_CO());            
        }
        else if(PlayerPrefs.GetInt("Presentation") == 1) 
        {
            if (previousState == State.Start || previousState == State.Presentation || previousState == State.Tutorial)
            {
                GameManager.manager.worldSide = playerRot.Down;
                //GameManager.manager.ui.gravityButton.SetActive(false);
                GameManager.manager.ui.CancelUpdates();
                GameManager.manager.startGame = true;
                GameManager.manager.distance = 0;
                GameManager.manager.coins = 0;
                GameManager.manager.ui.UpdateCoin(0);
                GameManager.manager.platformVelicity = new Vector2(-8, 0);
                StartCoroutine(StartGame_CO());
            }
            else if (previousState == State.GameOver)
            {
                GameManager.manager.worldSide = playerRot.Down;
                //GameManager.manager.ui.gravityButton.SetActive(false);
                GameManager.manager.distance = 0;
                GameManager.manager.coins = 0;
                GameManager.manager.ui.CancelUpdates();
                GameManager.manager.ui.UpdateCoin(0);
                GameManager.manager.gameOver = false;
                GameManager.manager.startGame = true;
                GameManager.manager.platformVelicity = new Vector2(-8, 0);
                StartCoroutine(StartGame_CO());
            }
            else if (previousState == State.PauseGame)
            {
                StartCoroutine(InGameState_CR());
                GameManager.manager.soundMng.SetCurrentEffect(swip_SFX);
            }
            else if (previousState == State.Restart)
            {
                GameManager.manager.worldSide = playerRot.Down;
                //GameManager.manager.ui.gravityButton.SetActive(false);
                GameManager.manager.startGame = true;
                GameManager.manager.ui.CancelUpdates();
                gameOverMenu.SetActive(false);
                if(!PlatformManager.destructor.isTutorial) gameMenu.SetActive(true);
                GameManager.manager.distance = 0;
                GameManager.manager.coins = 0;
                GameManager.manager.ui.UpdateCoin(0);
                GameManager.manager.platformVelicity = new Vector2(-8, 0);
                GameManager.manager.gameOver = false;
                GameManager.manager.soundMng.SetCurrentEffect(swip_SFX);
                StartCoroutine(EnhacedWindow());
            }
        }
    }

    IEnumerator InGameState_CR()
    {
        pauseMenu.SetActive(false);
        GameManager.manager.TimeScaleGame(0.1f);

        waitTime.gameObject.SetActive(true);
        waitTime.text = "3";
        yield return new WaitForSeconds(0.1f);
        waitTime.text = "2";
        yield return new WaitForSeconds(0.1f);
        waitTime.text = "1";
        yield return new WaitForSeconds(0.1f);
        waitTime.text = "GO!";
        yield return new WaitForSeconds(0.1f);
        waitTime.gameObject.SetActive(false);

        GameManager.manager.TimeScaleGame(1);
        gameMenu.SetActive(true);
        GameManager.manager.gameOver = false;
    }
    void PauseGameState()
    {
        if(previousState == State.InGame)
        {
            GameManager.manager.gameOver = true;
            pauseMenu.SetActive(true);
            GameManager.manager.soundMng.SetCurrentEffect(swip_SFX);
            gameMenu.SetActive(false);
            GameManager.manager.TimeScaleGame(0.01f);
        }
    }
    IEnumerator StartPresentation_CO()
    {
        GameManager.manager.soundMng.bsoEmitter.Stop();
        Camera.main.gameObject.GetComponent<Animator>().SetTrigger("Active");
        yield return new WaitForSeconds(0.35F);        
        startMenu.SetActive(false);
        SceneManager.LoadScene("Prolog");
        PlayerPrefs.SetInt("Presentation", 1);
    }

    IEnumerator StartGame_CO()
    {
        GameManager.manager.TimeScaleGame(1);
        Camera.main.gameObject.GetComponent<Animator>().SetTrigger("Active");
        yield return new WaitForSeconds(0.35F);
        startMenu.SetActive(false);
        tutorialWindow.SetActive(false);
        gameMenu.SetActive(true);        
        GameManager.manager.soundMng.SetCurrentBSO(game_BSO);
        SceneManager.LoadScene(2);
        StartCoroutine(EnhacedWindow());
    }

    IEnumerator EnhacedWindow()
    {
        enhancer_Panel.transform.localScale = Vector3.one;
        enhancer_Panel.SetActive(true);
        GameManager.manager.animMaster.enhancerPanel.MoveIn();
        yield return new WaitForSeconds(4);
        GameManager.manager.animMaster.enhancerPanel.MoveOut();
        yield return new WaitForSeconds(1);
        enhancer_Panel.SetActive(false);
    }

    void GameOverState()
    {
        // camManager.SetCurrentCamera(0);
        print("GameOverState");
        if (previousState == State.InGame || previousState == State.Tutorial)
        {
            GameManager.manager.TimeScaleGame(1);
            GameManager.manager.platformVelicity = new Vector2(0, 0);
            gameMenu.SetActive(false);
            tutorialWindow.SetActive(false);
            adsButton.SetActive(true);
            GameManager.manager.soundMng.bsoEmitter.Stop();
            GameManager.manager.soundMng.SetCurrentEffect(gameOver_SFX);
            int recordCoin = GameManager.manager.coins + PlayerPrefs.GetInt("Coin");
            PlayerPrefs.SetInt("Coin", recordCoin);
            // GameManager.manager.soundMng.bsoEmitter.Stop();
            StartCoroutine(GameOver());
        }
        else if (previousState == State.Revive)
        {
            GameManager.manager.platformVelicity = new Vector2(0, 0);
            gameMenu.SetActive(false);
            adsButton.SetActive(false);
            GameManager.manager.soundMng.bsoEmitter.Stop();
            GameManager.manager.soundMng.SetCurrentEffect(gameOver_SFX);
            int recordCoin = GameManager.manager.coins + PlayerPrefs.GetInt("Coin");
            PlayerPrefs.SetInt("Coin", recordCoin);
            // GameManager.manager.soundMng.bsoEmitter.Stop();
            StartCoroutine(GameOver());
        }
    }
    IEnumerator GameOver()
    {
        // for (int i = 0; i < gameOverMenu.transform.childCount; i++)
        // {
        //     if(gameOverMenu.transform.GetChild(i).GetComponent<Image>()) gameOverMenu.transform.GetChild(i).GetComponent<Image>().color = new Color(255,255,255,0);
        //     else if(gameOverMenu.transform.GetChild(i).GetComponent<Text>()) gameOverMenu.transform.GetChild(i).GetComponent<Text>().color = new Color(255,255,255,0);


        // }
        Text[] allText = gameOverMenu.transform.GetComponentsInChildren<Text>();
        Image[] allImage = gameOverMenu.transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < allText.Length; i++)
        {
            allText[i].color = new Color(255, 255, 255, 0);
        }
        for (int i = 0; i < allImage.Length; i++)
        {
            allImage[i].color = new Color(255, 255, 255, 0);
        }

        yield return new WaitForSeconds(2);

        gameOverMenu.SetActive(true);
        gameOverMenu.GetComponent<Image>().color = windowColor;
        GameManager.manager.ui.NewRecord.SetActive(false);
        for (int i = 0; i < gameOverMenu.transform.childCount; i++)
        {
            if (gameOverMenu.transform.GetChild(i).GetComponent<GEAnim>())
                gameOverMenu.transform.GetChild(i).GetComponent<GEAnim>().MoveIn();
        }
        GameManager.manager.ui.UpdateScore(GameManager.manager.distance, GameManager.manager.bestDistance, GameManager.manager.coins);
        GameManager.manager.ui.endFinalUI = false;
        
    }
    public GameObject groundReplace;
    void ReviveState()
    {
        // camManager.SetCurrentCamera(0);
        print("ReviveState");
        if (previousState == State.GameOver)
        {
            print("Revive");
            GameManager.manager.platformVelicity = new Vector2(-8, 0);
            gameMenu.SetActive(true);
            gameOverMenu.SetActive(false);
            GameObject[] obs = GameObject.FindGameObjectsWithTag("GroundPart");
            for (int i = 0; i < obs.Length; i++)
            {
                Transform oldPos = obs[i].transform;
                GameObject clone = Instantiate(groundReplace, oldPos.position, oldPos.rotation) as GameObject;
                PlatformManager.destructor.lastPoint = clone.transform.GetChild(0);
                Destroy(obs[i]); 
            }
            GameManager.manager.player.ResetPosition();
            GameManager.manager.gameOver = false;
            GameManager.manager.player.gameObject.SetActive(true);
            GameManager.manager.soundMng.SetCurrentBSO(game_BSO);
        }
    }

    IEnumerator RestartGame()
    {
        
        Camera.main.gameObject.GetComponent<Animator>().SetTrigger("Active");
        GameManager.manager.difficuly = Difficuly.Easy;
        SetCurrentState("InGame");
        yield return new WaitForSeconds(0.3F);
        if(PlatformManager.destructor.isTutorial && PlatformManager.destructor.completeTutorial) 
            SceneManager.LoadScene(2);
        else SceneManager.LoadScene(SceneManager.GetActiveScene().name);    
    }
    IEnumerator StartGame()
    {
        GameManager.manager.TimeScaleGame(1);
        Camera.main.gameObject.GetComponent<Animator>().SetTrigger("Active");
        yield return new WaitForSeconds(0.3F);                
        GameManager.manager.startGame = false;
        GameManager.manager.gameOver = false;
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        tutorialWindow.SetActive(false);
        startMenu.SetActive(true);
        GameManager.manager.ui.UpdateMainCoin();
        // SetCurrentState("Start");
        GameManager.manager.soundMng.SetCurrentBSO(start_BSO);
        SceneManager.LoadScene(1);
    }


}
