using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pepperPrefab;
    [SerializeField]
    private float timeout = 5f;
    private float leftTime;

    void Start()
    {
        GameState.AddListener(nameof(GameState.isBurst), OnBurstChanged);
        leftTime = timeout;
    }

    void Update()
    {
        if (leftTime > 0f)
        {
            leftTime -= Time.deltaTime;
            if (leftTime <= 0f)
            {
                SpawnPepper();

            }
        }
    }

    private void SpawnPepper()
    {
        var pepper = GameObject.Instantiate(pepperPrefab);
        //pepper.transform.position = new Vector3(161.91f, 15f, 161.74f);
    }

    private void OnBurstChanged(string ignored)
    {
        if (!GameState.isBurst)
        {
            leftTime = timeout;
        }
    }
    private void OnDestroy()
    {
        GameState.RemoveListener(nameof(GameState.isBurst), OnBurstChanged);
    }
}
