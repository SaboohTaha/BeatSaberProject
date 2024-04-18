using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class LevelController : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject completionMenu;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private XRInteractorLineVisual leftLineVisual; 
    [SerializeField] private XRInteractorLineVisual rightLineVisual;
    private int score;

    public AudioSource music;
    private bool levelCompleted = false;
    // Start is called before the first frame update
    void Start()
    {
        if (music == null)
        {
            Debug.LogError("Audio Source is not assigned.");
            return;
        }
        score = 0;
        scoreText.text = "Score: " + score;
    }
    public void OpenMainMenu()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
        }
        SceneManager.LoadScene("Main Menu 1");
    }


    void pauseGame()
    {
        if (music.isPlaying)
        {
            music.Pause();
        }
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);
        leftLineVisual.enabled = true;
        rightLineVisual.enabled = true;
    }

    public void resumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            if (!music.isPlaying)
            {
                music.UnPause();
            }
            isPaused = false;
            pauseMenu.SetActive(false);
            leftLineVisual.enabled = false;
            rightLineVisual.enabled = false;
        }
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void IncreaseScore()
    {
        score = score + 1;
        scoreText.text = "Score: " + score;
    }
    // Update is called once per frame
    void Update()
    {
        if(!isPaused && !levelCompleted)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.JoystickButton6) || Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.JoystickButton8) || Input.GetKeyDown(KeyCode.JoystickButton9) || Input.GetKeyDown(KeyCode.JoystickButton10) || Input.GetKeyDown(KeyCode.JoystickButton11) || Input.GetKeyDown(KeyCode.JoystickButton12) || Input.GetKeyDown(KeyCode.JoystickButton13) || Input.GetKeyDown(KeyCode.JoystickButton14) || Input.GetKeyDown(KeyCode.JoystickButton15) || Input.GetKeyDown(KeyCode.JoystickButton16) || Input.GetKeyDown(KeyCode.JoystickButton17) || Input.GetKeyDown(KeyCode.JoystickButton18) || Input.GetKeyDown(KeyCode.JoystickButton19))
            {
                pauseGame();
            }
        }
        if (!levelCompleted && !music.isPlaying && music.time >= music.clip.length)
        {
            levelCompleted = true;
            completionMenu.SetActive(true);
            rightLineVisual.enabled = true;
            leftLineVisual.enabled = true;
        }
    }
}
