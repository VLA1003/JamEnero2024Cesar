using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class BarrasVolumen : MonoBehaviour
{

    [SerializeField]
    string nombreDeLaVariableDelVolumen;

    [SerializeField]
    AudioMixerGroup mezcladorDeVolumen;

    [SerializeField]
    TextMeshProUGUI equisDelBotonDeSilenciar;

    bool silenciarPista = false;

    // Start is called before the first frame update
    void Start()
    {
        float volumen;
        mezcladorDeVolumen.audioMixer.ClearFloat(nombreDeLaVariableDelVolumen);
        mezcladorDeVolumen.audioMixer.GetFloat(nombreDeLaVariableDelVolumen, out volumen);

        switch (volumen)
        {
            case -80f:
                gameObject.GetComponent<Slider>().value = -30f;
                equisDelBotonDeSilenciar.text = "X";
                break;

            default:
                gameObject.GetComponent<Slider>().value = volumen;
                break;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Slider>().value == -30f && equisDelBotonDeSilenciar.text != "X")
        {
            equisDelBotonDeSilenciar.text = "X";
        }
    }

    public void CambiarVolumen ()
    {
        silenciarPista = false;
        equisDelBotonDeSilenciar.text = "";
        mezcladorDeVolumen.audioMixer.ClearFloat(nombreDeLaVariableDelVolumen);
        
        float volumen = gameObject.GetComponent<Slider>().value;
        switch (volumen)
        {
            case -30f:
                mezcladorDeVolumen.audioMixer.SetFloat(nombreDeLaVariableDelVolumen, -80f);
                break;

            default:
                mezcladorDeVolumen.audioMixer.SetFloat(nombreDeLaVariableDelVolumen, volumen);
                break;
        }        
    }

    public void SilenciarAudio ()
    {
        silenciarPista = !silenciarPista;

        switch (silenciarPista)
        {
            case true:
                equisDelBotonDeSilenciar.text = "X";
                mezcladorDeVolumen.audioMixer.ClearFloat(nombreDeLaVariableDelVolumen);
                mezcladorDeVolumen.audioMixer.SetFloat(nombreDeLaVariableDelVolumen, -80f);
                break;

            case false:
                equisDelBotonDeSilenciar.text = "";
                mezcladorDeVolumen.audioMixer.ClearFloat(nombreDeLaVariableDelVolumen);
                mezcladorDeVolumen.audioMixer.SetFloat(nombreDeLaVariableDelVolumen, gameObject.GetComponent<Slider>().value);
                break;
        }
    }
}
