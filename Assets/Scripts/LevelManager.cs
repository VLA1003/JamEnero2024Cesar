using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using TMPro.Examples;

public class LevelManager : MonoBehaviour
{
    public static LevelManager controladorNiveles;
    public float tiempo;
    int tiempoSegundos;
    int frutasAGenerar;
    public int frutasRestantes;

    public int puntos = 0;
    TextMeshProUGUI textoTiempo, textoPuntos, textoComida;

    [SerializeField]
    GameObject [] puntosDeCreacion;

    [SerializeField]
    GameObject [] frutasPool;

    private void Awake () {
        controladorNiveles = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        textoTiempo = GameObject.Find("Time Text").GetComponent<TextMeshProUGUI>();
        textoPuntos = GameObject.Find("Points Text").GetComponent<TextMeshProUGUI>();
        textoComida = GameObject.Find("Comida Restante Texto").GetComponent<TextMeshProUGUI>();
        puntos = GameManager.instance.puntos;
        frutasAGenerar = puntosDeCreacion.Length;
        frutasRestantes = frutasAGenerar;
        CambiarLetreroPuntos();
        GenerarFrutas();

    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.instancePausa.isPaused == false)
        {
            if (tiempo > 0f) {
                tiempo -= Time.deltaTime;
            } else {
                FinalizarPantalla(tiempo);
            }

            tiempoSegundos = Mathf.RoundToInt(tiempo);
            textoTiempo.text = "Tiempo: " + tiempoSegundos.ToString();

            if (frutasRestantes == 0) {
                FinalizarPantalla(tiempo);
            }
        }
    }

    public void GenerarFrutas () {

        foreach (GameObject spawn in puntosDeCreacion)
        {
            int frutaACrear;
            frutaACrear = Mathf.RoundToInt(Random.Range(0f, frutasPool.Length - 1));
            Instantiate(frutasPool [frutaACrear], spawn.transform.position, Quaternion.identity);
        }
    }

    public void FinalizarPantalla (float tiempoRestante) {
        puntos += tiempoSegundos;
        GameManager.instance.TerminarNivel(puntos);
    }

    public void CambiarLetreroPuntos ()
    {
        textoPuntos.text = "Puntos: " + puntos.ToString();
        textoComida.text = "Faltan: " + frutasRestantes.ToString();
    }
}
  