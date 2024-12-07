using System.Linq;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private static int countCoin;
    private float minOffset = 100f;
    private float minDistanse = 50f;
    private Animator animator;
    private Collider[] colliders;
    private AudioSource catchSound;

    [SerializeField]
    private TMPro.TextMeshProUGUI coinTMP;
    void Start()
    {
        animator = GetComponent<Animator>();
        colliders = GetComponents<Collider>();
        catchSound = GetComponent<AudioSource>();
        countCoin = 0;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            if (colliders[0].bounds.Intersects(other.bounds))
            {
                animator.SetInteger("CoinState", 2);
                catchSound.Play();
                countCoin++;
                coinTMP.text = countCoin.ToString();
                ReplaceCoin();
            }
            else
            {
                animator.SetInteger("CoinState", 1);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Character")
        {
            if (colliders[1].bounds.Intersects(other.bounds))
            {
                animator.SetInteger("CoinState", 0);
            }
           
        }
    }
    public void ReplaceCoin()
    {
        Vector3 newPosition;
        do
        {
            newPosition = this.transform.position + new Vector3(
            Random.Range(-minDistanse, minDistanse),
            this.transform.position.y,
            Random.Range(-minDistanse, minDistanse));
        } while (Vector3.Distance(newPosition, this.transform.position) < minDistanse
        || newPosition.x < minOffset
        || newPosition.x > 1000 - minOffset
        || newPosition.z < minOffset
        || newPosition.z > 1000 - minOffset);

        float terraitHeight = Terrain.activeTerrain.SampleHeight(newPosition);
        newPosition.y = terraitHeight + Random.Range(2f, 18f);
        this.transform.position = newPosition;
        animator.SetInteger("CoinState", 0);
    }
}
