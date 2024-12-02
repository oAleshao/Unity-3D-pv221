using UnityEngine;

public class PaperScript : MonoBehaviour
{

    public CharacterScript character;

    [SerializeField]
    private GameObject pepperPrefab;
    private float minOffset = 100f;
    private float minDistanse = 50f;
    private Animator animator;
    private float timeSpawn;

    void Start()
    {
        animator = GetComponent<Animator>();
        timeSpawn = 0;
        ReplacePepper();
        pepperPrefab.transform.GetChild(0).gameObject.SetActive(true);
    }

    void Update()
    {
        timeSpawn += Time.deltaTime;
        if (System.Math.Floor(timeSpawn) == 30)
        {
            timeSpawn = 0;
            ReplacePepper();
        }
    }
    

private void spawnPepper()
    {
        GameObject peper = GameObject.Instantiate(pepperPrefab);
        ReplacePepper();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            if(character != null)
                character.gotPepper = true;
            ReplacePepper();
            timeSpawn = 0;
        }
    }

    public void ReplacePepper()
    {
        Vector3 newPosition;
        do
        {
            newPosition = this.transform.position + new Vector3(
            UnityEngine.Random.Range(-minDistanse, minDistanse),
            this.transform.position.y,
            UnityEngine.Random.Range(-minDistanse, minDistanse));
        } while (Vector3.Distance(newPosition, this.transform.position) < minDistanse
        || newPosition.x < minOffset
        || newPosition.x > 1000 - minOffset
        || newPosition.z < minOffset
        || newPosition.z > 1000 - minOffset);

        float terraitHeight = Terrain.activeTerrain.SampleHeight(newPosition);
        newPosition.y = terraitHeight + UnityEngine.Random.Range(2f, 18f);
        this.transform.position = newPosition;

        this.gameObject.SetActive(true);
    }
}
