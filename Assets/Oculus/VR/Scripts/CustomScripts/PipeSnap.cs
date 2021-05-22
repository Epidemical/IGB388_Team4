using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSnap : MonoBehaviour
{
    public Material displayMat;
    private GameObject displayObj = null;
    private GameObject snapObj = null;
    private Quaternion snapRot = new Quaternion();
    public bool pickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PipeNode" && pickedUp)
        {
            if (other.GetComponent<PipeNode>().pipePiece == null)
            {
                if (snapObj == null && displayObj == null)
                {
                    snapObj = other.gameObject;
                    CheckRotation();
                    CreateDisplayObj(snapObj);
                }
                else
                {
                    //if this node is closer than the previously found one
                    if (Vector3.Distance(transform.position, other.transform.position) < Vector3.Distance(transform.position, snapObj.transform.position))
                    {
                        snapObj = other.gameObject;
                        CheckRotation();
                        CreateDisplayObj(snapObj);
                    }
                    else if (CheckRotation())
                    {
                        CreateDisplayObj(snapObj);
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PipeNode" && pickedUp)
        {
            if (other.GetComponent<PipeNode>().pipePiece == null)
            {
                if (snapObj == null && displayObj == null)
                {
                    snapObj = other.gameObject;
                    CheckRotation();
                    CreateDisplayObj(snapObj);
                }
                else
                {
                    //if this node is closer than the previously found one
                    if (Vector3.Distance(transform.position, other.transform.position) < Vector3.Distance(transform.position, snapObj.transform.position))
                    {
                        snapObj = other.gameObject;
                        CheckRotation();
                        CreateDisplayObj(snapObj);
                    }
                    else if (CheckRotation())
                    {
                        CreateDisplayObj(snapObj);
                    }
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

            //if (snapObj != null)
            //    snapObj = null;
        }
    }

    private void CreateDisplayObj(GameObject node)
    {
        //Debug.Log("new display obj");

        if (displayObj != null)
            Destroy(displayObj);

        displayObj = Instantiate(this.gameObject, node.transform.position, snapRot);

        //disable scripts
        Destroy(displayObj.GetComponent<OVRGrabbable>());
        Destroy(displayObj.GetComponent<PipeSnap>());
        Destroy(displayObj.GetComponent<Outline>());
        Destroy(displayObj.GetComponent<BoxCollider>());
        Destroy(displayObj.GetComponent<Rigidbody>());

        //change mats
        MeshRenderer[] meshes = displayObj.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.material = displayMat;
        }

    }

    public void SnapToNode()
    {
        if (snapObj != null)
        {
            this.transform.position = snapObj.transform.position;
            this.transform.rotation = snapRot;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            snapObj.GetComponent<PipeNode>().pipePiece = this.gameObject;
        }
    }

    public void PickUpReset()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        if (snapObj != null)
        {
            Debug.Log("called");
            snapObj.GetComponent<PipeNode>().pipePiece = null;
            snapObj = null;

        }
    }

    //returns false if the rotation is the same, true if rotation has changed;
    private bool CheckRotation()
    {
        Quaternion currentAngle = this.transform.rotation;

        float timesDivided = Mathf.Round(currentAngle.eulerAngles.y / 90f);
        float yRot = Mathf.Abs(timesDivided) * 90;

        if (yRot == snapRot.eulerAngles.y)
            return false;
        else
        {
            snapRot = Quaternion.Euler(0, yRot, 0);
            return true;
        }
    }
}
