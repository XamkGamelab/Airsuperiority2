using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CoreLoader : Singleton<CoreLoader>
{
    [Tooltip("Every essential Core Manager prefabs to be instantiated when the game starts. Each one is Singleton instance.")]
    [Header("Core Manager Prefabs")]
    [SerializeField] private GameObject[] coreManagers;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
