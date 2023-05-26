using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public AudioSource soundPlayer;
    public static bool EstaPausado=false;

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("MenuOpciones");
    }

    public void MenuNivel1()
    {
        SceneManager.LoadScene("Nivel_1 1");
    }
    public void Water()
    {
        SceneManager.LoadScene("water");
    }
    public void Explosion()
    {
        SceneManager.LoadScene("explosion");
    }
    public void MenuOpciones()
    {
        SceneManager.LoadScene("MenuSonido");
    }

    public void MenuSalir()
    {
        Application.Quit();
    }
    public void SonidoBotones()
    {
        soundPlayer.Play();
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EstaPausado)
            {
                Continuar();
            }
            else 
            {
                Pausa();
            }
        }*/
    }

    void Continuar()
    {
        Time.timeScale = 1f;
        EstaPausado = false;
       
    }

    void Pausa()
    {
        SceneManager.LoadScene("MenuSonido");
        soundPlayer.Stop();
        Time.timeScale = 0f;
        EstaPausado = true;
    }
}
