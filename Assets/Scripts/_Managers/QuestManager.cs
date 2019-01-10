using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string QuestID;
    public string QuestName;
    public int RewardPoints;
    public int TargetProgress;
    public int xpAmount;
    public int moneyAmount;
	public bool Earned;

    public Quest(string _QuestID, string _QuestName, int _RewardPoints, int _TargetProgress, int _xpAmount, int _moneyAmount, bool _Earned)
    {
		this.QuestID = _QuestID;
		this.QuestName = _QuestName;
		this.RewardPoints = _RewardPoints;
		this.TargetProgress = _TargetProgress;
		this.xpAmount = _xpAmount;
		this.moneyAmount = _moneyAmount;
		Earned = _Earned;
    }

    public bool AddProgress(int progress)
    {
        if (Earned)
        {
            return false;
        }
        RewardPoints += progress;
        if (RewardPoints >= TargetProgress)
        {
            //AchievementManager.manager.CoroutineActivator();
            Earned = true;
            return true;
        }

        return false;
    }
} 


public class QuestManager : MonoBehaviour {

    public List<Quest> questList = new List<Quest>();
    public List<Quest> questBase = new List<Quest>();
    public AudioClip EarnedSound;
    private int currentRewardPoints = 0;
    private int potentialRewardPoints = 0;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddProgressToAchievement(string achievementName, int progressAmount)
    {
        Quest achievement = GetAchievementByName(achievementName);
        if (achievement == null)
        {
            Debug.LogWarning("AchievementManager::AddProgressToAchievement() - Trying to add progress to an achievemnet that doesn't exist: " + achievementName);
            return;
        }

        if (achievement.AddProgress(progressAmount))
        {
            AchievementEarned();
        }
// espacio para salvar
    }
	private void AchievementEarned()
    {
        UpdateRewardPointTotals();
        //AudioSource.PlayClipAtPoint(EarnedSound, Camera.main.transform.position);
    }
	private void UpdateRewardPointTotals()
    {
        currentRewardPoints = 0;
        potentialRewardPoints = 0;

        foreach (Quest achievement in questList)
        {
            if (achievement.Earned)
            {
                currentRewardPoints += achievement.RewardPoints;
            }

            potentialRewardPoints += achievement.RewardPoints;
        }
    }
	private Quest GetAchievementByName(string achievementName)
    {
        //Quest ach;
        //for (int i = 0; i < Achievements.Count(); i++)
        //{
        //    if (Achievements[i].QuestID == achievementName)
        //    {
        //        ach = Achievements[i];
        //        break;
        //    }
        //}
        //return ach;
        return questList.FirstOrDefault(achievement => achievement.QuestID == achievementName);
    }

	public void ClaimReward(string achievementName)
	{
		for (int i = 0; i < questList.Count; i++)
        {
           if(questList[i].QuestID == achievementName && questList[i].Earned)
		{
			GameManager.manager.coins+= questList[i].moneyAmount;
			GameManager.manager.xp+= questList[i].xpAmount;
			if(questBase.Count > 0)
			{
				questList[i] = questBase[0];
				Quest r = questBase[0];
				questBase.Remove(r);																						
			}
		} 
        }
		
	}
}