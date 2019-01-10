using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HeaderAttribute("Menu UI")]
    //public Text bestDistance;
    public Text coinCountUI;

    [SpaceAttribute]
    [HeaderAttribute("InGame UI")]
    public Text distance;
    public Text coin;
    public Text finalScore;
    public Text finalCoin;

    [SpaceAttribute]
    [HeaderAttribute("Extras")]
    public bool endFinalUI;
    public GameObject NewRecord;
    public GameObject gravityButton;
    public GameObject uiTutorial;
    public AudioClip coinFX;
    public Animator startAnim;

    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    private bool updateScore;
    private bool updateCoint;
    private int score;
    private int maxScore;
    private int coinCount;
    private int maxCoin;
    private int bestScore;

    void Start()
    {
        //bestDistance.text = PlayerPrefs.GetInt("Record").ToString();
        coinCountUI.text = PlayerPrefs.GetInt("Coin").ToString();
    }



    void Update()
    {
        if (updateScore && Time.time > nextFire && score < maxScore)
        {
            nextFire = Time.time + fireRate;
            score++;
            GameManager.manager.soundMng.SetCurrentEffect(coinFX);
            finalScore.text = score.ToString();
        }
        // if (score == maxScore)

        if (updateCoint && Time.time > nextFire && coinCount < maxCoin)
        {
            nextFire = Time.time + fireRate;
            coinCount++;
            GameManager.manager.soundMng.SetCurrentEffect(coinFX);
            finalCoin.text = coinCount.ToString();
        }

        if (updateScore && bestScore < maxScore && score == maxScore && !endFinalUI)
        {
            StartCoroutine(NewScore_CO());
        }

    }

    public void OpenCredits(bool value)
    {
        startAnim.SetBool("credit",value);
    }
    public void OpenCharacters(bool value)
    {
        startAnim.SetBool("selection",value);
    }
    IEnumerator NewScore_CO()
    {
        yield return new WaitForSeconds(0.3F);
        NewRecord.SetActive(true);
        NewRecord.GetComponent<GEAnim>().MoveIn(eGUIMove.Self);
        PlayerPrefs.SetInt("Record", maxScore);
        endFinalUI = true;
    }

    public void UpdateDistance(int value)
    {
        distance.text = value.ToString();
    }

    public void UpdateMainCoin()
    {
       coinCountUI.text = GameManager.manager.coins.ToString();
    }
    
    public void UpdateCoin(int value)
    {
        coin.text = value.ToString();
    }
    public void UpdateScore(int value, int maxValue, int _coins)
    {
        finalScore.text = "0";
        finalCoin.text = "0";
        score = 0;
        coinCount = 0;
        maxScore = value;
        maxCoin = _coins;
        bestScore = maxValue;
        StartCoroutine(UpdateScore_CO());
    }

    public void UpdateGaimCoin(int _coins)
    {
        maxCoin = _coins;
        updateCoint = true;
    }

    public IEnumerator UpdateScore_CO()
    {
        yield return new WaitForSeconds(1);
        updateScore = true;
        yield return new WaitForSeconds(0.5F);
        updateCoint = true;
    }

    public void CancelUpdates()
    {
        updateScore = false;
        updateCoint = false;
    }
}
