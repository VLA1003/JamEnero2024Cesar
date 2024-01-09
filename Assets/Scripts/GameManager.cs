using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int puntuacionDeLaPartida = 0;
    float volumenMaestro, volumenFX, volumenMusica;
    public int nivelActual = 0, puntos = 0;
    public string [] niveles;
    public GameObject menuDePausa;
    public AudioMixer mezcladorDeSonido;
    TextMeshProUGUI textoPuntosFinales, textoPuntuacionMaxima;
    private void Awake () {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        CargarDatos();
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
            IrAPantallaFinal();

        } else {
            if (nivelActual == niveles.Length - 1) {
                IrAPantallaFinal();

            } else {
                nivelActual++;
                SiguienteNivel(nivelActual);
            }
        }
    }

    public void IrPantallaDeInicio () {

        if (menuDePausa != null)
        {
            if (PauseMenu.instancePausa.isPaused == true || Time.timeScale != 1f)
            {
                PauseMenu.instancePausa.isPaused = false;
                Time.timeScale = 1f;
            }
            Destroy(menuDePausa);
        }

        GuardarDatosVolumen();


        PlayerPrefs.SetInt("PuntuacionPartida", 0);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void SalirDelJuego ()
    {
        Application.Quit();
    }

    public void IrAPantallaFinal ()
    {
        int nuevoRecord;
        PlayerPrefs.SetInt("PuntuacionPartida", puntuacionDeLaPartida);
        nuevoRecord = PlayerPrefs.GetInt("PuntuacionMaxima", 0);

        if (puntuacionDeLaPartida > nuevoRecord)
        {
            PlayerPrefs.SetInt("PuntuacionMaxima", nuevoRecord);
        }

        GuardarDatosVolumen();

        SceneManager.LoadScene("PantallaFinal");
        Destroy(menuDePausa);
        Destroy(gameObject);
    }

    public void CargarDatos ()
    {
        if (SceneManager.GetActiveScene().name != "PantallaFinal")
        {
            float volumen;

            volumen = PlayerPrefs.GetFloat("VolMaestro", 0f);
            mezcladorDeSonido.ClearFloat("VolumenMaestro");
            mezcladorDeSonido.SetFloat("VolumenMaestro", volumen);

            volumen = PlayerPrefs.GetFloat("VolMusica", 0f);
            mezcladorDeSonido.ClearFloat("VolumenMusica");
            mezcladorDeSonido.SetFloat("VolumenMusica", volumen);

            volumen = PlayerPrefs.GetFloat("VolEfectos", 0f);
            mezcladorDeSonido.ClearFloat("VolumenEfectos");
            mezcladorDeSonido.SetFloat("VolumenEfectos", volumen);

        } else
        {
            puntuacionDeLaPartida = PlayerPrefs.GetInt("PuntuacionPartida", 0);
            textoPuntosFinales = GameObject.Find("textoPuntos").GetComponent<TextMeshProUGUI>();
            textoPuntosFinales.text = puntuacionDeLaPartida.ToString() + " pts.";
            textoPuntuacionMaxima = GameObject.Find("textoMaximaPuntuacion").GetComponent<TextMeshProUGUI>();
            int puntuacionMaxima;
            puntuacionMaxima = PlayerPrefs.GetInt("PuntuacionMaxima", puntuacionDeLaPartida);
            textoPuntuacionMaxima.text = "MP: " + puntuacionMaxima.ToString();
        } 
    }

    public void GuardarDatosVolumen ()
    {
        float volumen;

        mezcladorDeSonido.ClearFloat("VolumenMaestro");
        mezcladorDeSonido.GetFloat("VolumenMaestro", out volumen);
        PlayerPrefs.SetFloat("VolMaestro", volumen);

        mezcladorDeSonido.ClearFloat("VolumenMusica");
        mezcladorDeSonido.GetFloat("VolumenMusica", out volumen);
        PlayerPrefs.SetFloat("VolMusica", volumen);

        mezcladorDeSonido.ClearFloat("VolumenEfectos");
        mezcladorDeSonido.GetFloat("VolumenEfectos", out volumen);
        PlayerPrefs.SetFloat("VolEfectos", volumen);
    }
}
