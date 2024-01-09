using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBurrow : MonoBehaviour
{
    public bool onBurrow = false;

    public AudioSource burrowSound;  
    public AudioClip burrowClip;


    private void Start()
    {
        
        burrowSound = gameObject.AddComponent<AudioSource>();
        burrowSound.playOnAwake = false;
        burrowSound.clip = burrowClip;
    }

    private void OnTriggerEnter2D(Collider2D entry)
    {
        if (entry.CompareTag("Burrow"))
        {
            onBurrow = true;
            //Debug.Log("Ha entrado en la madriguera.");
            burrowSound.Play();

        }
    }

    private void OnTriggerExit2D(Collider2D exit)
    {
        if (exit.CompareTag("Burrow"))
        { 
            onBurrow = false;
            //Debug.Log("Ha salido de la madriguera.");
        }
    }
}
