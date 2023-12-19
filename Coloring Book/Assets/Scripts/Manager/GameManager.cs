using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float timeWaitLoadInitial;

    [SerializeField] private int indexLobby;

    [SerializeField] private int indexIngame;

    [SerializeField] private UiGlobalManager uiGlobalManager;

    [SerializeField] private DataManager dataManager;

    [SerializeField] private SoundManager soundManager;

    [SerializeField] private VibrateManager vibrateManager;

    [SerializeField] private LoadingManager loadingManager;

    public UiGlobalManager UiGlobalManager => uiGlobalManager;

    public DataManager DataManager => dataManager;

    public SoundManager SoundManager => soundManager;

    public VibrateManager VibrateManager => vibrateManager;

    public LoadingManager LoadingManager => loadingManager;


    private void Awake()
    {
        Application.targetFrameRate = 60;

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitLoadInitial());
    }

    IEnumerator WaitLoadInitial()
    {
        yield return new WaitForSeconds(timeWaitLoadInitial);

        LoadInitialGame();
    }

    public void LoadScene(int indexScene)
    {
        Resources.UnloadUnusedAssets();

        SceneManager.LoadScene(indexScene, LoadSceneMode.Single);
    }

    public void LoadLobby()
    {
        LoadScene(indexLobby);
    }

    public void LoadGame()
    {
        LoadScene(indexIngame);
    }

    public void LoadInitialGame()
    {
        LoadLobby();
    }
}