using System.Linq;
using UnityEngine;

public class DayNightScript : MonoBehaviour
{
    [SerializeField]
    private Material daySkyBox;
    [SerializeField]
    private Material nightSkyBox;

    private AudioSource daySound;
    private AudioSource nightSound;

    private Light[] dayLights;
    private Light[] nigthLights;
    private bool isDay;

    void Start()
    {
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
        daySound = audioSources[0];
        nightSound = audioSources[1];
        dayLights = GameObject
            .FindGameObjectsWithTag("DayLight")
            .Select(x => x.GetComponent<Light>())
            .ToArray();
        nigthLights = GameObject
            .FindGameObjectsWithTag("NightLight")
            .Select(x => x.GetComponent<Light>())
            .ToArray();
        SwitchDayNight(true);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            SwitchDayNight(!isDay);
        }
    }

    private void SwitchDayNight(bool isDay)
    {
        this.isDay = isDay;
        foreach (Light light in dayLights)
        {
            light.enabled = this.isDay;
        }
        foreach (Light light in nigthLights)
        {
            light.enabled = !this.isDay;
        }
        RenderSettings.skybox = isDay ? daySkyBox : nightSkyBox;
        RenderSettings.ambientIntensity = isDay ? 1f : 0.3f;
        if (isDay)
        {
            nightSound.Stop();
            daySound.Play();
        }
        else
        {
            daySound.Stop();
            nightSound.Play();
        }
    }
}

