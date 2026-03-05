using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CoreLoader : Singleton<CoreLoader>
{
    [Tooltip("Every essential Core Manager prefabs to be instantiated when the game starts. Each one is Singleton instance.")]
    [Header("Core Manager Prefabs")]
    [SerializeField] private GameObject[] coreManagers;

    [Tooltip("Settings for loading the main menu scene after initializing Core Managers.")]
    [Header("Loading settings")]
    [SerializeField] private string mainMenuSceneName = "MainMenuScene";
    [SerializeField] private CanvasGroup loadingScreen;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(InitializeCore());
    }

    private IEnumerator InitializeCore()
    {
        //Load Monobehaviour prefabs
        foreach (var prefab in coreManagers)
        {
            if (prefab != null)
            {
                var type = prefab.GetComponent<MonoBehaviour>().GetType();
                if (FindFirstObjectByType(type) == null)
                    Instantiate(prefab);
            }
        }
        yield return null;

        //Initialize Non-Monobehaviour singletons by accessing their instance property
        var settingsData = SettingsData.Instance;
        var gameData = GameData.Instance;
        //Add other Non-Mono singletons initialization here as needed

        //Initialize loading screen
        if (loadingScreen != null)
        {
            loadingScreen.alpha = 1f;
            loadingScreen.blocksRaycasts = true;
        }

        //Asynchronously load the main menu scene
        yield return LoadMainMenuAsync();
    }

    private IEnumerator LoadMainMenuAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuSceneName);
        while (!asyncLoad.isDone)
        {
            //If needed, Add progress bar update logic here using asyncLoad.progress
            yield return null;
        }
        //Fade out loading screen
        if (loadingScreen != null)
        {
            float fadeDuration = 1f;
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                loadingScreen.alpha = 1f - (elapsedTime / fadeDuration);
                yield return null;
            }
            loadingScreen.alpha = 0f;
            loadingScreen.blocksRaycasts = false;
        }
    }
}