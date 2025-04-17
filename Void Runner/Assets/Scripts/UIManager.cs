using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject deathCanvas;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject UI;
    private GameObject lastCanvas;
    private bool isFullscreen = true;
    
    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
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
        lastCanvas = startCanvas;
    }

    public void ScreenMode() {
        isFullscreen =!isFullscreen;
        Screen.fullScreen = isFullscreen;

        if (isFullscreen) {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.MaximizedWindow);
        } else {
            Screen.SetResolution(800, 600, FullScreenMode.Windowed);
        }
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
        lastCanvas = null;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
        UI.SetActive(true);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        pauseCanvas.SetActive(true);
        UI.SetActive(false);
        Time.timeScale = 0;
        lastCanvas = pauseCanvas;
    }

    public void BackFromSettings()
    {
        settingsCanvas.SetActive(false);
        lastCanvas.SetActive(true);

    }


    public void Death()
    {
        deathCanvas.SetActive(true);
        Time.timeScale = 0;
        lastCanvas = deathCanvas;
        Powers.Instance.currPower = Power.Default;
    }

    public void Win()
    {
        winCanvas.SetActive(true);
        Time.timeScale = 0;
        lastCanvas = winCanvas;
    }
}