using UnityEngine;

public class ClockScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI clock;
    private float gameTime;
    void Start()
    {
        gameTime = 0f;   
        clock = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        gameTime += Time.deltaTime;
    }

    private void LateUpdate()
    {
        int t = (int)gameTime;

        int hour = t / 3600;
        int minute = (t % 3600) / 60;
        int seconds = t % 60;

        clock.text = $"{hour:D2}:{minute:D2}:{seconds:D2}";
    }
}
