using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public float tiempo;
    int tiempoSegundos;
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

            //puntosUsados [puntosUsados.Length - 1] = puntoAUsar;
            puntosUsados.Append(puntoAUsar);

            foreach (int frutaActivar in frutaCreada) {

                if (frutaActivar == frutaACrear) {
                    frutaACrear++;
                }
            }

            //frutaCreada [frutaCreada.Length] = frutaACrear;
            frutaCreada.Append(frutaACrear);

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
