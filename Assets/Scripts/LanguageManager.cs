using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using TMPro;

public class LanguageManager : MonoBehaviour
{
    public string [] languages;
    int idiomaActual = 0;

    [SerializeField]
    TextMeshProUGUI textoIdioma;

    // Start is called before the first frame update

    private void Awake ()
    {
        CambiarIdioma();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IdiomaIzquierda ()
    {
        if (idiomaActual == 0)
        {
            idiomaActual = languages.Length - 1;
            CambiarIdioma();
        } else
        {
            idiomaActual--;
            CambiarIdioma();
        }
    }

    public void IdiomaDerecha ()
    {
        if (idiomaActual == languages.Length - 1)
        {
            idiomaActual = 0;
            CambiarIdioma();
        } else
        {
            idiomaActual++;
            CambiarIdioma();
        }
    }

    public void CambiarIdioma ()
    {
        textoIdioma.text = languages [idiomaActual];
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[idiomaActual];
    }
}
