using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputField nameInput;
    [SerializeField] private string fileName;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private HighscoreHandler highscoreHandler;
    List<PlayerInfor> playerInfor = new List<PlayerInfor> ();

    private void Start() 
    {
        playerInfor = FileHandler.ReadFromJSON<PlayerInfor> (fileName);
    }

    public void AddToList ()
    {
        playerInfor.Add(new PlayerInfor(nameInput.text, PlayerController.instance.playerScore));
        FileHandler.SaveToJSON<PlayerInfor> (playerInfor, fileName);
        highscoreHandler.AddHighScoreIfPossible(new PlayerInfor(nameInput.text, PlayerController.instance.playerScore));

        finishPanel.SetActive(false);
        SceneManager.LoadScene("Game Play");
    }
}
