using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAnimations : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (ObjectPickup.pickupInstance.isDelivered == true)
        {
            anim.SetBool("isDelivered", true); 
        }
    }
}
