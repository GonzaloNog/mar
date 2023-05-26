using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SonidoGeneral : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
      

        if (!PlayerPrefs.HasKey("Musicvolume"))
        {
            PlayerPrefs.SetFloat("Musicvolume", 1);
            Load();
            
        }
        else
        {
            Load();
        }
    }

   public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

   

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Musicvolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Musicvolume", volumeSlider.value);
    }
}

