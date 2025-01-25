using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object persistent across scenes (optional)
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartBubbleSplit(Vector3 position, Vector3 velocity, int counter, GameObject bubblePrefab)
    {
        GameObject tempSplitter = new GameObject("BubbleSplitter");
        BubbleSplitter splitterScript = tempSplitter.AddComponent<BubbleSplitter>();
        splitterScript.Initialize(position, velocity, counter, bubblePrefab);
    }
}
