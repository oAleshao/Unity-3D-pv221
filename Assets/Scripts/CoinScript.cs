using System.Linq;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private float minOffset = 100f;
    private float minDistanse = 50f;
    private Animator animator;
    private Collider[] colliders;
    void Start()
    {
        animator = GetComponent<Animator>();
        colliders = GetComponents<Collider>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            animator.SetBool("IsCollected", true);
            Debug.Log("+1 Coin");
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
        animator.SetBool("IsCollected", false);
    }
}
