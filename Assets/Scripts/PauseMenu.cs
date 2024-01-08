using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instancePausa;
    public bool isPaused = false;
    bool puedePausar = true;

    [SerializeField]
    GameObject menuDePausa;

    [SerializeField]
    CanvasGroup menuPausaCanvasGroup;
    // Start is called before the first frame update

    private void Awake () {
        instancePausa = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && puedePausar == true) {

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

        LeanTween.scale(menuDePausa, Vector3.one * nuevaEscalaCanvasPausa, .75f).setEaseOutQuad().setIgnoreTimeScale(true).setOnUpdate((float x) => {
            gameObject.GetComponent<GraphicRaycaster>().enabled = false;

            if (visibilidadDelCanvasGroup == 1f) {
                menuPausaCanvasGroup.alpha = x;

            } else {
                menuPausaCanvasGroup.alpha = 1f - x;
            }
        }).setOnComplete (()=> {
            gameObject.GetComponent<GraphicRaycaster>().enabled = true;

            if (visibilidadDelCanvasGroup == 0f) {
                isPaused = false;
                menuDePausa.SetActive(false);
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
 }
