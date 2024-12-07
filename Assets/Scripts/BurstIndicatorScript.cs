using UnityEngine;
using UnityEngine.UI;

public class BurstIndicatorScript : MonoBehaviour
{
    private Image image;
    private CharacterScript character;
    void Start()
    {
        image = GetComponent<Image>();    
        character = GameObject.Find("Character").GetComponent<CharacterScript>();
    }

    void Update()
    {
        image.fillAmount = character.burstLevel;
    }
}
