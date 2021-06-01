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
    public GameObject[] ends = new GameObject[2];

    private List<Quaternion> validAngles = new List<Quaternion>();

    private void Start()
    {
        validAngles.Add(Quaternion.Euler(0, 0, 90));
        validAngles.Add(Quaternion.Euler(90, 0, 90));
        validAngles.Add(Quaternion.Euler(180, 0, 90));
        validAngles.Add(Quaternion.Euler(270, 0, 90));
    }

    private void OnTriggerEnter(Collider other)
    {
        SnapCheck(other);
    }

    private void OnTriggerStay(Collider other)
    {
        SnapCheck(other);
    }

    private void SnapCheck(Collider other)
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

            snapObj.GetComponent<PipeNode>().UpdatePipePiece(gameObject);
        }
    }

    public void PickUpReset()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        if (snapObj != null)
        {
            Debug.Log("called");
            snapObj.GetComponent<PipeNode>().UpdatePipePiece(null);
            snapObj = null;

        }
    }

    //returns false if the rotation is the same, true if rotation has changed;
    private bool CheckRotation()
    {
        //snapRot = snapObj.transform.rotation;

        //return true;

        //Quaternion currentAngle = this.transform.rotation;

        //float timesDivided = Mathf.Round(currentAngle.eulerAngles.x / 90f);
        //float xRot = Mathf.Abs(timesDivided) * 90;

        //if (xRot == snapRot.eulerAngles.x)
        //    return false;
        //else
        //{
        //    snapRot = Quaternion.Euler(xRot, snapObj.transform.rotation.eulerAngles.y, snapObj.transform.rotation.eulerAngles.z);
        //    return true;
        //}

        Quaternion currentAngle = this.transform.rotation;
        //currentAngle = this.transform.ro;

        //float timesDivided = Mathf.Round(currentAngle.eulerAngles.x / 90f);
        float xRot = currentAngle.eulerAngles.x;
        Debug.Log(currentAngle.eulerAngles);
        while (xRot < 0)
        {
            xRot += 360;
        }

        while (xRot > 360)
        {
            xRot -= 360;
        }

        float angleDif = 370;

        foreach(Quaternion angle in validAngles)
        {
            float smallerDif = Mathf.Min(Mathf.Abs(360 - xRot), Mathf.Abs(xRot - angle.eulerAngles.x));

            if(smallerDif < angleDif)
            {
                angleDif = smallerDif;

                snapRot = angle;
            }
        }

        //Debug.Log(Mathf.Abs(snapRot.eulerAngles.z - 90f));
        //Debug.Log(snapRot.eulerAngles.x == 0f);

        if (Mathf.Abs(currentAngle.eulerAngles.z - 90f) > 90f && snapRot.eulerAngles.x == 0f)
        {
            snapRot = Quaternion.Euler(180, 0, 90);
        }
        Debug.Log(snapRot.eulerAngles);

        return true;
        //if (xRot == snapRot.eulerAngles.x)
        //    return false;
        //else
        //{
        //    snapRot = Quaternion.Euler(xRot, snapObj.transform.rotation.eulerAngles.y, snapObj.transform.rotation.eulerAngles.z);
        //    return true;
        //}

    }
}
