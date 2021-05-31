using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSnap : MonoBehaviour
{
    public Material displayMat;
    private GameObject displayObj = null;
    private GameObject snapObj = null;
    public bool pickedUp = false;

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
        if (other.tag == "JournalNode" && pickedUp)
        {
            Debug.Log("journal node found");
            bool goAhead = CorrectNode(other.gameObject);

            if (goAhead && snapObj == null && displayObj == null)
            {
                snapObj = other.gameObject;
                CreateDisplayObj(snapObj);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "JournalNode")
        {
            bool goAhead = CorrectNode(other.gameObject);

            if (goAhead && displayObj != null)
            {
                Destroy(displayObj);
                displayObj = null;
                snapObj = null;
            }
        }
    }

    private bool CorrectNode(GameObject other)
    {
        string objName = this.gameObject.name;
        bool goAhead = false;

        switch (objName)
        {
            case "TornRightPiece":
                if (other.name == "nodeRight")
                    goAhead = true;
                break;
            case "TornMiddlePiece":
                if (other.name == "nodeMiddle")
                    goAhead = true;
                break;
            case "TornLeftPiece":
                if (other.name == "nodeLeft")
                    goAhead = true;
                break;
        }

        return goAhead;
    }

    private void CreateDisplayObj(GameObject node)
    {
        //Debug.Log("new display obj");

        if (displayObj != null)
            Destroy(displayObj);

        displayObj = Instantiate(this.gameObject, node.transform.position, node.transform.rotation);
        displayObj.transform.localScale = this.transform.localScale;

        //disable scripts
        Destroy(displayObj.GetComponent<OVRGrabbable>());
        Destroy(displayObj.GetComponent<PaperSnap>());
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
            this.transform.rotation = snapObj.transform.rotation;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            Destroy(this.GetComponent<OVRGrabbable>());
            Destroy(this.GetComponent<Outline>());
            //Destroy(this.GetComponent<BoxCollider>());
        }
    }
}
