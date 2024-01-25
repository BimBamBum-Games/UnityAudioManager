using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public abstract class VolumeSettingsBase : MonoBehaviour
{
    [SerializeField] AudioMixer audioMasterMixer;
    [SerializeField] Slider soundLevelSld;
    [SerializeField] Toggle soundLevelTgl;

    private VolumeSavedSettings volumeSettings = VolumeSavedSettings.Get();
    protected abstract string SUB_MIXER_NAME { get; }
    private const float soundAmplifier = 10f;
    private const float MIN_VOLUME_THRESHOLD = 0.000001f;
    private float currentVolume = 1f;
    private bool isOn = false;

    void Start() {      
        soundLevelSld.minValue = MIN_VOLUME_THRESHOLD;
        soundLevelSld.onValueChanged.AddListener(SetVolumeLevel);
        soundLevelTgl.onValueChanged.AddListener(ToggleVolume);

        UnityJsonManager.GetFromJSON(SUB_MIXER_NAME, volumeSettings);
        soundLevelSld.value = volumeSettings.Volume;
        soundLevelTgl.isOn = volumeSettings.IsOn;
    }

    void OnDisable() {
        volumeSettings.Volume = soundLevelSld.value;
        volumeSettings.IsOn = isOn;
        UnityJsonManager.SaveToJSON(SUB_MIXER_NAME, volumeSettings);
    }

    private void SetVolumeLevel(float volume) {
        currentVolume = Mathf.Clamp(volume, MIN_VOLUME_THRESHOLD, 1f);
        Debug.LogWarning("Vol > " + currentVolume);
        isOn = currentVolume != MIN_VOLUME_THRESHOLD;
        soundLevelTgl.SetIsOnWithoutNotify(isOn);
        SetVolume_OnSlided();
    }

    private void ToggleVolume(bool toggleOn) {
        isOn = toggleOn;
        SetVolume_OnToggled();
    }

    private void SetVolume_OnToggled() {
        if (isOn == true) {
            currentVolume = Mathf.Clamp(soundLevelSld.value, MIN_VOLUME_THRESHOLD, 1f);
            if (currentVolume == MIN_VOLUME_THRESHOLD) {
                soundLevelTgl.SetIsOnWithoutNotify(false);
            }
        }
        else {
            currentVolume = MIN_VOLUME_THRESHOLD;
        }
        SetVolume(currentVolume);
    }

    private void SetVolume_OnSlided() {
        SetVolume(currentVolume);
    }

    private void SetVolume(float vol) {
        audioMasterMixer.SetFloat(SUB_MIXER_NAME, GetPascalValue(vol) * soundAmplifier);
    }

    private float GetPascalValue(float val) {
        return Mathf.Log10(val);
    }
}
