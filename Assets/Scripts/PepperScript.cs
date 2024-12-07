using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PepperScript : MonoBehaviour
{

    private float minOffset = 100f;
    private float minDistanse = 50f;
    private Animator animator;
    private float timeSpawn;

    void Start()
    {
        ReplacePepper();
    }

    void Update()
    {
        
    }

    private void ReplacePepper()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            GameState.isBurst = true;
            Destroy(gameObject);
        }

    }
}
