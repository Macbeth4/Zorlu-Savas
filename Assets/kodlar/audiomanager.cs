using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class audiomanager : MonoBehaviour
{
    
 public TextMeshProUGUI volumeamount;

    public Image image;
    public Sprite sesvar;
    public Sprite sesyok;
    public Slider slider;

    private void Start() {
        loadaudio();
    }

    public void SetAudio(float value)
    {
        AudioListener.volume = value;
        volumeamount.text = ((int)(value * 100)).ToString();
        saveaudio();
    }

    private void saveaudio()
    {
        PlayerPrefs.SetFloat("audioVolume",AudioListener.volume);
    }

    private void loadaudio()
    {
        if(PlayerPrefs.HasKey("audioVolume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("audioVolume");
            if(null != slider){
                slider.value = PlayerPrefs.GetFloat("audioVolume");
            }
            
        }
        else{
            PlayerPrefs.SetFloat("audioVolume",0.5f);
            AudioListener.volume = PlayerPrefs.GetFloat("audioVolume");
            slider.value = PlayerPrefs.GetFloat("audioVolume");
        }
        
    }

    private void FixedUpdate() {
        if(slider.value == 0){image.sprite = sesyok;}
        else{image.sprite = sesvar;}
    }
}
