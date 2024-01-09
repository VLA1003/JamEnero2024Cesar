using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    private GameObject currentObject = null;
    public GameObject burrow;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Food") == true && currentObject == null)
        {
            currentObject = other.gameObject;
            currentObject.transform.SetParent(transform);
            currentObject.GetComponent<Collider2D>().enabled = false;
        }
        else if (other.CompareTag("Burrow") && currentObject != null)
        {
            currentObject.transform.SetParent(null);
            currentObject.transform.position = burrow.transform.position;
            currentObject = null;
        }
    }
}
