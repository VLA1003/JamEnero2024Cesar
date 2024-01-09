using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBurrow : MonoBehaviour
{
    public bool onBurrow = false;

    private void OnTriggerEnter2D(Collider2D entry)
    {
        if (entry.CompareTag("Burrow"))
        {
            onBurrow = true;
            Debug.Log("Ha entrado en la madriguera.");
        }
    }

    private void OnTriggerExit2D(Collider2D exit)
    {
        if (exit.CompareTag("Burrow"))
        { 
            onBurrow = false;
            Debug.Log("Ha salido de la madriguera.");
        }
    }

}
