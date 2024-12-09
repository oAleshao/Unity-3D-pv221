using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour

{
    private GameObject content;

    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private Slider effectsVolumeSlider;
    [SerializeField]
    private Slider ambientVolumeSlider;
    [SerializeField]
    private Toggle masterVolumeToggle;

    void Start()
    {
        content = transform.Find("SettingsContent").gameObject;
        content.SetActive(!content.activeInHierarchy);
        Time.timeScale = content.activeInHierarchy ? 0.0f : 1.0f;
        OnEffectsSlider(effectsVolumeSlider.value);
        OnAmbientSlider(ambientVolumeSlider.value);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = content.activeInHierarchy ? 1.0f : 0.0f;
            content.SetActive(!content.activeInHierarchy);

        }
    }

    public void OnEffectsSlider(float value)
    {
        float dB = Mathf.Lerp(-80.0f, 10.0f, value);
        mixer.SetFloat("EffectsVolume", dB);
    }
    public void OnAmbientSlider(float value)
    {
        float dB = Mathf.Lerp(-80.0f, 10.0f, value);
        mixer.SetFloat("AmbientVolume", dB);
    }

    public void OnMusicSlider(float value)
    {
        float dB = Mathf.Lerp(-80.0f, 10.0f, value);
        mixer.SetFloat("MusicVolume", dB);
    }
    public void OnMuteAll(float value)
    {
        float dB = Mathf.Lerp(-80.0f, 10.0f, value);
        mixer.SetFloat("MasterVolume", dB);
    }
}

// 10 - 10^1
// 20 - 10^2
// -80 - 10^-8
