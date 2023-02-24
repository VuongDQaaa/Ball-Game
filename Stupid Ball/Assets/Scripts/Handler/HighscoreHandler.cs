using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour
{
    List<PlayerInfor> playersInfor = new List<PlayerInfor>();
    [SerializeField] int maxCount = 5;
    [SerializeField] string fileName;

    public delegate void OnHighscoreListChanged(List<PlayerInfor> list);
    public static event OnHighscoreListChanged onHighscoreListChanged;

    private void Start()
    {
        LoadHighscores();
    }

    private void LoadHighscores()
    {
        playersInfor = FileHandler.ReadFromJSON<PlayerInfor>(fileName);
        while (playersInfor.Count > maxCount)
        {
            playersInfor.RemoveAt(maxCount);
        }

        if (onHighscoreListChanged != null)
        {
            onHighscoreListChanged.Invoke(playersInfor);
        }
    }

    private void SaveHighscore()
    {
        FileHandler.SaveToJSON<PlayerInfor>(playersInfor, fileName);
    }

    public void AddHighScoreIfPossible(PlayerInfor playerInfor)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (i >= maxCount || playerInfor.points > playersInfor[i].points)
            {
                //add new high score
                playersInfor.Insert(i, playerInfor);

                while (playersInfor.Count > maxCount)
                {
                    playersInfor.RemoveAt(maxCount);
                }

                SaveHighscore();
                if (onHighscoreListChanged != null)
                {
                    onHighscoreListChanged.Invoke(playersInfor);
                }

                break;
            }
        }
    }
}
