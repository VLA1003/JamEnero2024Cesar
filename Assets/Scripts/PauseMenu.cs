using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instancePausa;
    public bool isPaused = false;
    bool puedePausar = true;

    [SerializeField]
    GameObject menuDePausa, menuControles, pantallaDeCreditos, creditosTexto, pantallaDeInicio;

    [SerializeField]
    CanvasGroup menuPausaCanvasGroup;
    // Start is called before the first frame update

    private void Awake () {
        instancePausa = this;
        DontDestroyOnLoad(gameObject);
        menuDePausa.transform.localScale = Vector3.one * .5f;
        menuPausaCanvasGroup.alpha = 0f;
        menuDePausa.SetActive(false);
        menuControles.SetActive(false);
        pantallaDeCreditos.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && puedePausar == true && menuControles.activeSelf == false) {

            if (isPaused == false) {
                puedePausar = false;
                isPaused = true;
                LeanTween.cancel(gameObject);
                Time.timeScale = 0f;
                AnimacionPausa(1f, 1f);

            } else if (isPaused == true) {
                puedePausar = false;
                LeanTween.cancel(gameObject);
                AnimacionPausa(0f, .5f);
            }
        }
    }

    public void AnimacionPausa (float visibilidadDelCanvasGroup, float nuevaEscalaCanvasPausa) {

        if (menuDePausa.activeSelf == false) {
            menuDePausa.SetActive(true);

        }
        
        if (SceneManager.GetActiveScene().name == "InitialScreen")
        {
            pantallaDeInicio.GetComponent<GraphicRaycaster>().enabled = false;
        }

        LeanTween.scale(menuDePausa, Vector3.one * nuevaEscalaCanvasPausa, .75f).setEaseOutQuad().setIgnoreTimeScale(true).setOnUpdate((float x) => {
            gameObject.GetComponent<GraphicRaycaster>().enabled = false;

            if (visibilidadDelCanvasGroup == 1f) {
                menuPausaCanvasGroup.alpha = x;

            } else {
                menuPausaCanvasGroup.alpha = 1f - x;
            }
        }).setOnComplete (()=> {

            gameObject.GetComponent<GraphicRaycaster>().enabled = true;
            UnityEngine.Debug.Log("jasndjaw");

            if (visibilidadDelCanvasGroup == 0f) {
                isPaused = false;
                menuDePausa.SetActive(false);
                GameManager.instance.GuardarDatosVolumen();
                if (SceneManager.GetActiveScene().name == "InitialScreen")
                {
                    pantallaDeInicio.GetComponent<GraphicRaycaster>().enabled = true;
                }
                Time.timeScale = 1f;
            }

            puedePausar = true;
            UnityEngine.Debug.Log("Pausa: " + isPaused);
        });
    }

    public void CerarMenuConBoton () {
        puedePausar = false;
        LeanTween.cancel(gameObject);
        AnimacionPausa(0f, .5f);
    }

    public void AbrirCerrarControles (bool abrir) {

        if (LeanTween.isTweening(gameObject) == false) {

            float visibilidadCanvas = 0f;
            gameObject.GetComponent<GraphicRaycaster>().enabled = false;

            if (abrir == true) {
                menuControles.SetActive(true);
                visibilidadCanvas = 1f;
                LeanTween.alphaCanvas(menuControles.GetComponent<CanvasGroup>(), 0f, 0f).setIgnoreTimeScale(true);
            } else {
                menuDePausa.SetActive(true);
                visibilidadCanvas = 0f;
            }

            LeanTween.alphaCanvas(menuControles.GetComponent<CanvasGroup>(), visibilidadCanvas, .5f).setEaseOutQuad().setIgnoreTimeScale(true).setOnComplete(() => {
                if (abrir == true) {
                    menuDePausa.SetActive(false);
                } else {
                    menuControles.SetActive(false);
                }
                gameObject.GetComponent<GraphicRaycaster>().enabled = true;
            });
        }
    }

    public void AbrirCreditos () {
        pantallaDeCreditos.SetActive(true);
        LeanTween.alphaCanvas(pantallaDeCreditos.GetComponent<CanvasGroup>(), 0f, 0f).setIgnoreTimeScale(true);
        gameObject.GetComponent<GraphicRaycaster>().enabled = false;
        LeanTween.alphaCanvas(pantallaDeCreditos.GetComponent<CanvasGroup>(), 1f, .5f).setEaseOutQuad().setIgnoreTimeScale(true).setOnComplete(() => {
            LeanTween.moveLocalY(creditosTexto, 1350f, 35f).setIgnoreTimeScale(true).setOnComplete(() => {
                GameManager.instance.IrPantallaDeInicio();
            });
        });

    }

    public void AbrirOpcionesDesdeElInicio ()
    {
        Time.timeScale = 0f;
        isPaused = true;
        AnimacionPausa(1f, 1f);
    }
 }
