using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject deathCanvas;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] public GameObject winCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject UI;
    private GameObject lastCanvas;
    private bool isFullscreen;
    private bool showFPS_UI;
    public GameObject FPS_UI;
    private InputAction pauseAction;
    private bool hasWon = false;
    
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
    private void Start() {
        isFullscreen = true;
        showFPS_UI = false;
        InitializeUI();
    }

    void Update() {
        int cp = CheckpointManager.Instance.GetCurrentCheckpoint();
        if (cp == 7 && !hasWon) {
            Win();
        }
    }

    private void OnEnable () {
        pauseAction = InputSystem.actions.FindAction("Pause", throwIfNotFound: true);
        pauseAction.performed += OnPausePerformed;
    }

    private void OnDisable() {
        pauseAction.performed -= OnPausePerformed;
    }

    private void OnPausePerformed(InputAction.CallbackContext ctx)
    {
        if (!pauseCanvas.activeInHierarchy) {
            Pause();
        } else {
            Resume();
        }
    }

    public void InitializeUI() {
        lastCanvas = null;
        DeactivateAllCanvases();
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

    public void FPSToggle() {
        showFPS_UI = !showFPS_UI;
        FPS_UI.SetActive(showFPS_UI);
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
        UI.SetActive(false);
        Time.timeScale = 0;
        lastCanvas = deathCanvas;
        Powers.Instance.currPower = Power.Default;
    }

    public void Win() {
        if (hasWon) return;

        DeactivateAllCanvases();
        winCanvas.SetActive(true);
        Time.timeScale = 0;
        lastCanvas = winCanvas;
        hasWon = true;
    }

    private void DeactivateAllCanvases() {
        deathCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        winCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        UI.SetActive(false);
        startCanvas.SetActive(false);
    }
}