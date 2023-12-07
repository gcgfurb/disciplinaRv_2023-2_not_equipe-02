using System;
using UnityEngine;

public class ChaoScript : MonoBehaviour
{
    
    public GameObject menu;
    public GameObject leftHand;
    public GameObject rightHand;

    public void OnCollisionEnter(Collision collision)
    {
        Console.Write(collision.GetContact(0));    
        menu.SetActive(true);
        leftHand.SetActive(true);
        rightHand.SetActive(true);
    }
}
