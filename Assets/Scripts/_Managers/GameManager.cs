using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum playerRot { Up, Down }
public enum Difficuly { Easy, Medium, Hard }

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public int coins = 0;
    public int xp = 0;
    public int distance = 0;
    public int bestDistance = 0;
    public int timeSpeedEnchanced = 0;
    public Vector2 platformVelicity = new Vector2(-8, 0);
    public playerRot worldSide;
    public Difficuly difficuly;
    public PlayerStatus player;
    public SoundManager soundMng;
    public StateMachine state;
    public UIManager ui;
    public PlayGUIAnim animMaster;
    public QuestManager quest;
    public ShopManager shop;
    public GameObject canvasUI;
    public AudioClip switchFX;
    public bool gameOver;
    public bool startGame;
    public int nextVelocityChange = 15;
    public int nextVelocityOffSet = 15;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    // Use this for initialization
    void Start()
    {
        //Application.targetFrameRate = 60;
        if (manager == null) manager = this;
        DontDestroyOnLoad(transform.gameObject);
        

        if (!PlayerPrefs.HasKey("Record"))
            PlayerPrefs.SetInt("Record", 0);
        else bestDistance = PlayerPrefs.GetInt("Record");

        if (!PlayerPrefs.HasKey("Coin"))
            PlayerPrefs.SetInt("Coin", 0);

            coins = PlayerPrefs.GetInt("Coin");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (worldSide == playerRot.Up) worldSide = playerRot.Down;
            else if (worldSide == playerRot.Down) worldSide = playerRot.Up;
            Camera.main.gameObject.GetComponent<Animator>().SetTrigger("Stroke");
            soundMng.SetCurrentEffect(switchFX);
            ChangeWorldGravity();
        }

        if (Time.time > nextFire && !gameOver && startGame)
        {
            nextFire = Time.time + fireRate;
            distance++;
            ui.UpdateDistance(distance);
        }

        if (distance > 150) difficuly = Difficuly.Hard;
        else if (distance > 50 && distance < 150) difficuly = Difficuly.Medium;
        // ele if(distance >50) difficuly = Difficuly.Easy;

        if (distance > nextVelocityChange)
        {
            platformVelicity.x -= 0.02f;
            nextVelocityChange += nextVelocityOffSet;
        }
    }

    public void CoinAverage(int _value)
    {
        coins += _value;
        ui.UpdateCoin(coins);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void ChangeGravity()
    {
        if (worldSide == playerRot.Up) worldSide = playerRot.Down;
        else if (worldSide == playerRot.Down) worldSide = playerRot.Up;

        ChangeWorldGravity();
    }

    public void ChangeWorldGravity()
    {
        if (player != null)
            player.UpdateWorld(worldSide);
    }


    public void TimeScaleGame(float value)
    {
        Time.timeScale = value;
    }

    public void EnhacedSpeed()
    {
        StartCoroutine(EnhacedSpeed_CR());
    }
    public void EnhacedMagnet()
    {
        if(player != null) player.ActivateMagnet();
    }
    public void EnhacedMultiply()
    {
        if(player != null) player.multiplyCoins = true;
    }
    public IEnumerator EnhacedSpeed_CR()
    {
        TimeScaleGame(5);
        yield return new WaitForSeconds(timeSpeedEnchanced);
        TimeScaleGame(4);
        yield return new WaitForSeconds(1);
        TimeScaleGame(3);
        yield return new WaitForSeconds(1);
        TimeScaleGame(2);
        yield return new WaitForSeconds(1);
        TimeScaleGame(1);
    }


}
