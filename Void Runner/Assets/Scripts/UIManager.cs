using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Link PlayUI, Figure out Retry button

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject deathCanvas;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject UI;
    private void Start()
    {
        InitializeUI();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!pauseCanvas.activeInHierarchy) {
                Pause();
            } else {
                Resume();
            }
        }
    }

    public void InitializeUI()
    {
        deathCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        winCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        UI.SetActive(false);
        startCanvas.SetActive(true);
        Time.timeScale = 0; 
    }

    public void Settings()
    {
        settingsCanvas.SetActive(true);
        startCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        deathCanvas.SetActive(false);
        winCanvas.SetActive(false);
    }

    public void Play()
    {
        startCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        deathCanvas.SetActive(false);
        winCanvas.SetActive(false);
        UI.SetActive(true);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        pauseCanvas.SetActive(true);
        UI.SetActive(false);
        Time.timeScale = 0;
    }

    public void SettingsToStart()
    {
        settingsCanvas.SetActive(false);
        startCanvas.SetActive(true);
    }

    public void Death()
    {
        deathCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void Win()
    {
        winCanvas.SetActive(true);
        Time.timeScale = 0;
    }
}