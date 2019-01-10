using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour {

	// public string QuestName;

	public int questArrayPos;
	public Text questName;
	public Text questProgress;

	public string questNameText;
	public GameObject claimButton;
	public AudioClip rewardSFX;
	public bool isPausePanel;

	// Use this for initialization
	void Start () {
		questName.text = GameManager.manager.quest.questList[questArrayPos].QuestName;
		questNameText = GameManager.manager.quest.questList[questArrayPos].QuestID;
	}
	
	// Update is called once per frame
	void Update () {
         questProgress.text = GameManager.manager.quest.questList[questArrayPos].RewardPoints + " of " + GameManager.manager.quest.questList[questArrayPos].TargetProgress;
		 if(GameManager.manager.quest.questList[questArrayPos].Earned && !isPausePanel) claimButton.SetActive(true);;
	}

	public void ClaimQuest()
	{
		if(rewardSFX != null) GameManager.manager.soundMng.SetCurrentEffect(rewardSFX);
		GameManager.manager.quest.ClaimReward(questNameText);
		claimButton.SetActive(false);
		GameManager.manager.ui.UpdateGaimCoin(GameManager.manager.coins);
		StartCoroutine(UpdateQuest());
	}

	IEnumerator UpdateQuest()
	{
		Text[] allText = transform.GetComponentsInChildren<Text>();
        Image[] allImage = transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < allText.Length; i++)
        {
            allText[i].color = new Color(255, 255, 255, 0);
        }
        for (int i = 0; i < allImage.Length; i++)
        {
            allImage[i].color = new Color(255, 255, 255, 0);
        }
		questName.text = GameManager.manager.quest.questList[questArrayPos].QuestName;
		questNameText = GameManager.manager.quest.questList[questArrayPos].QuestID;
		
		yield return new WaitForSeconds(0.5f);
		GetComponent<GEAnim>().MoveIn();
	} 
}
