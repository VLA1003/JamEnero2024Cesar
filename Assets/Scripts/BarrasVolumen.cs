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
        gameObject.GetComponent<Slider>().value = volumen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CambiarVolumen ()
    {
        silenciarPista = false;
        equisDelBotonDeSilenciar.text = "";
        mezcladorDeVolumen.audioMixer.ClearFloat(nombreDeLaVariableDelVolumen);
        mezcladorDeVolumen.audioMixer.SetFloat(nombreDeLaVariableDelVolumen, gameObject.GetComponent<Slider>().value);
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
