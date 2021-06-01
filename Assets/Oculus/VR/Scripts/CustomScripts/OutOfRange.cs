using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfRange : MonoBehaviour
{
    public float timeOut = 3f;

    Dictionary<GameObject, float> currentObjects = new Dictionary<GameObject, float>();

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if(other.tag == "Grabbable")
        {
            if (!currentObjects.ContainsKey(other.gameObject))
            {
                currentObjects.Add(other.gameObject, 0f);
            }
            else
            {
                if(currentObjects[other.gameObject] >= timeOut)
                {
                    other.gameObject.GetComponent<OVRGrabbable>().ReturnToStart();
                    currentObjects.Remove(other.gameObject);
                }
                else
                    currentObjects[other.gameObject] += Time.deltaTime;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Grabbable")
        {
            if (!currentObjects.ContainsKey(other.gameObject))
            {
                currentObjects.Add(other.gameObject, 0f);
            }
            else
            {
                if (currentObjects[other.gameObject] >= timeOut)
                {
                    other.gameObject.GetComponent<OVRGrabbable>().ReturnToStart();
                    currentObjects.Remove(other.gameObject);
                }
                else
                    currentObjects[other.gameObject] += Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Grabbable")
        {
            if (currentObjects.ContainsKey(other.gameObject) && !other.GetComponent<OVRGrabbable>().isGrabbed)
            {
                
                    other.gameObject.GetComponent<OVRGrabbable>().ReturnToStart();
                    currentObjects.Remove(other.gameObject);

            }
        }
    }
}
