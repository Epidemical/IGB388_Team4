using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Key")
        {
            Manager.instance.OpenBox();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Key")
        {
            Manager.instance.OpenBox();
        }
    }
}
