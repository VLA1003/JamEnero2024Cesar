using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public float tiempo;
    int tiempoSegundos, tiempoMinutos;
    public int frutasAGenerar;
    public int frutasRestantes;

    int puntos = 0;
    TextMeshProUGUI textoTiempo;

    [SerializeField]
    GameObject [] puntosDeCreacion;

    [SerializeField]
    GameObject [] frutasPool;

    private void Awake () {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        textoTiempo = GameObject.Find("Time Text").GetComponent<TextMeshProUGUI>();
        frutasRestantes = frutasAGenerar;
        GenerarFrutas();

    }

    // Update is called once per frame
    void Update()
    {
        if (tiempoSegundos > 0f) {
            tiempo -= Time.deltaTime;
        } else {
            FinalizarPantalla(tiempo);
        }

        tiempoSegundos = Mathf.RoundToInt(tiempo);
        tiempoMinutos = tiempoSegundos / 60;
        tiempoSegundos = tiempoMinutos % 60;
        textoTiempo.text = tiempoMinutos + ":" + tiempoSegundos;

        if (frutasRestantes == 0) {
            FinalizarPantalla(tiempo);
        }
    }

    public void GenerarFrutas () {
        int [] puntosUsados = {-1};
        int [] frutaCreada = {-1};

        while (frutasAGenerar != 0) {
            int puntoAUsar = Mathf.RoundToInt(Random.Range(0, puntosDeCreacion.Length - 1));
            int frutaACrear = Mathf.RoundToInt(Random.Range(0, frutasPool.Length - 1));

            foreach (int puntosCrear in puntosUsados) {

                if (puntosCrear == puntoAUsar) {
                    puntoAUsar++;
                }
            }

            puntosUsados [puntosUsados.Length] = puntoAUsar;

            foreach (int frutaActivar in frutaCreada) {

                if (frutaActivar == frutaACrear) {
                    frutaACrear++;
                }
            }

            frutaCreada [frutaCreada.Length] = frutaACrear;

            frutasPool [frutaACrear].SetActive(true);
            frutasPool [frutaACrear].transform.position = puntosDeCreacion [puntoAUsar].transform.position;
            frutasAGenerar--;            
        }
    }

    public void FinalizarPantalla (float tiempoRestante) {
        puntos += tiempoSegundos;
        GameManager.instance.TerminarNivel(puntos);
    }
}
