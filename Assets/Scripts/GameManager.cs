using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject bubblePrefab; // The single prefab to spawn
    public Transform[] spawnPositions; // Array of transforms for spawn points
    private int currentLevel = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartLevel(currentLevel);
    }

    public void StartLevel(int levelIndex)
    {
        currentLevel = levelIndex;
        SpawnBubbles();
    }

    private void SpawnBubbles()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            Instantiate(bubblePrefab, spawnPositions[i].position, Quaternion.identity);
        }
    }

    public void StartBubbleSplit(Vector3 scale, Vector3 position, Vector3 velocity, int counter, GameObject bubblePrefab)
    {
        GameObject tempSplitter = new GameObject("BubbleSplitter");
        BubbleSplitter splitterScript = tempSplitter.AddComponent<BubbleSplitter>();
        splitterScript.Initialize(scale, position, velocity, counter, bubblePrefab);
    }
}
