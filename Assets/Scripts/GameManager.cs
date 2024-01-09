using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int nivelActual = 0, puntos = 0;
    public string [] niveles;
    public GameObject menuDePausa;
    private void Awake () {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SiguienteNivel (int nivelACargar) {
        if (PauseMenu.instancePausa.isPaused == true || Time.timeScale != 1f)
        {
            PauseMenu.instancePausa.isPaused = false;
            Time.timeScale = 1f;
        }
        SceneManager.LoadScene(niveles [nivelACargar]);
    }

    public void TerminarNivel (int puntosDelNivel) {
        if (puntosDelNivel == 0) {
            nivelActual = 0;
            SceneManager.LoadScene("PantallaFinal");
        } else {
            if (nivelActual == niveles.Length - 1) {
                SceneManager.LoadScene("PantallaFinal");
            } else {
                nivelActual++;
                SiguienteNivel(nivelActual);
            }
        }
    }

    public void IrPantallaDeInicio () {
        if (PauseMenu.instancePausa.isPaused == true || Time.timeScale != 1f)
        {
            PauseMenu.instancePausa.isPaused = false;
            Time.timeScale = 1f;
        }
        SceneManager.LoadScene(0);
        Destroy(menuDePausa);
        Destroy(gameObject);
    }

    public void SalirDelJuego ()
    {
        Application.Quit();
    }
}
