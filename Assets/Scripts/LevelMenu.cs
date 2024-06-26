using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        int UnlockedLevel = PlayerPrefs.GetInt("Unlockedlevel", 3);
        for(int i=0; i<buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < UnlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }
    public void OpenLevel(int LevelId)
    {
        string LevelName = "N_Level " + LevelId;
        SceneManager.LoadScene(LevelName);
    }
}
