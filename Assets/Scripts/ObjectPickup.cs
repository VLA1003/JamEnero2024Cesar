using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class ObjectPickup : MonoBehaviour
{
    private GameObject currentObject = null;
    public GameObject burrow;
    public bool isDelivered = false;

    public AudioSource pickupSound;  
    public AudioClip pickupClip;     


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        pickupSound = gameObject.AddComponent<AudioSource>();
        pickupSound.playOnAwake = false;  

        
        pickupSound.clip = pickupClip;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Food") == true && currentObject == null)
        {
            isDelivered = false;
            currentObject = other.gameObject;
            currentObject.transform.SetParent(transform);
            currentObject.GetComponent<Collider2D>().enabled = false;

            pickupSound.Play();

        }
        else if (other.CompareTag("Burrow") && currentObject != null)
        {
            isDelivered = true;
            currentObject.transform.SetParent(null);
            currentObject.transform.position = other.transform.position;
            currentObject = null;
        }
    }
}
