using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreUI : MonoBehaviour
{
    [SerializeField] GameObject highscoreUIElementPrefab;
    [SerializeField] Transform elementWrapper;

    List<GameObject> uiElements = new List<GameObject> ();

    private void OnEnable() 
    {
        HighscoreHandler.onHighscoreListChanged += UpdateUI;    
    }

    private void OnDisable() 
    {
        HighscoreHandler.onHighscoreListChanged -= UpdateUI;
    }
    public void MainMenuButtoon()
    {
        SceneManager.LoadScene("Menu");
    }

    private void UpdateUI(List<PlayerInfor> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            PlayerInfor el = list[i];

            if (el != null && el.points > 0)
            {
                if (i >= uiElements.Count)
                {
                    // instantiate new entry
                    var inst = Instantiate(highscoreUIElementPrefab, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(elementWrapper, false);

                    uiElements.Add(inst);
                }

                // write or overwrite name & points
                var texts = uiElements[i].GetComponentsInChildren<Text>();
                texts[0].text = el.playerName;
                texts[1].text = el.points.ToString();
            }
        }
    }
}
