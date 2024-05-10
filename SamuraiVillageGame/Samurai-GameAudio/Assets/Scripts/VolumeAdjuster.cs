using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class VolumeAdjuster : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER,
        MUSIC,
        AMBIENCE,
        SFX
    }

    [Header("Type")]
    [SerializeField] private VolumeType volumeType;
    private Slider volumeSlider;
    public AudioManager audioManager;

    private void Awake()
    {
        volumeSlider = this.GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                volumeSlider.value = audioManager.masterVolume;
                break;
            case VolumeType.MUSIC:
                volumeSlider.value = audioManager.musicVolume;
                break;
            case VolumeType.AMBIENCE:
                volumeSlider.value = audioManager.ambienceVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = audioManager.SFXVolume;
                break;
            default:
                Debug.LogWarning("Volume Type not supported: " + volumeType);
                break;
        }
    }

    public void OnSliderValueChanged()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                audioManager.masterVolume = volumeSlider.value;
                break;
            case VolumeType.MUSIC:
                audioManager.musicVolume = volumeSlider.value;
                break;
            case VolumeType.AMBIENCE:
                audioManager.ambienceVolume = volumeSlider.value;
                break;
            case VolumeType.SFX:
                audioManager.SFXVolume = volumeSlider.value;
                break;
            default:
                Debug.LogWarning("Volume Type not supported: " + volumeType);
                break;
        }
    }

    
}
