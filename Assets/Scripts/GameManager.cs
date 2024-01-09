using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int nivelActual = 0;
    public Scene [] niveles;
    private void Awake () {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SiguienteNivel (int nivelACargar) {
        SceneManager.LoadScene(niveles [nivelACargar].name);
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
}
