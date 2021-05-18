using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSnap : MonoBehaviour
{
    public Material displayMat;
    private GameObject displayObj = null;
    private GameObject snapObj = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PipeNode")
        {
            if(snapObj == null && displayObj == null)
            {
                snapObj = other.gameObject;
                CreateDisplayObj(snapObj);
            }
            else
            {
                //if this node is closer than the previously found one
                if(Vector3.Distance(transform.position, other.transform.position) < Vector3.Distance(transform.position, snapObj.transform.position))
                {
                    snapObj = other.gameObject;
                    CreateDisplayObj(snapObj);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PipeNode")
        {
            if (snapObj == null && displayObj == null)
            {
                snapObj = other.gameObject;
                CreateDisplayObj(snapObj);
            }
            else
            {
                //if this node is closer than the previously found one
                if (Vector3.Distance(transform.position, other.transform.position) < Vector3.Distance(transform.position, snapObj.transform.position))
                {
                    snapObj = other.gameObject;
                    CreateDisplayObj(snapObj);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PipeNode")
        {
            if (displayObj != null)
            {
                Destroy(displayObj);
                displayObj = null;
            }

            if (snapObj != null)
                snapObj = null;
        }
    }

    private void CreateDisplayObj(GameObject node)
    {
        if (displayObj != null)
            Destroy(displayObj);

        displayObj = Instantiate(this.gameObject, node.transform.position, node.transform.rotation);

        //disable scripts
        Destroy(GetComponent<OVRGrabbable>());
        Destroy(GetComponent<PipeSnap>());
        Destroy(GetComponent<Outline>());
        Destroy(GetComponent<BoxCollider>());
        Destroy(GetComponent<Rigidbody>());

        //change mats
        MeshRenderer[] meshes = displayObj.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer mesh in meshes)
        {
            mesh.material = displayMat;
        }

    }

    public void SnapToNode()
    {
        if(snapObj != null)
        {
            this.transform.position = snapObj.transform.position;
            //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void PickUpReset()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
