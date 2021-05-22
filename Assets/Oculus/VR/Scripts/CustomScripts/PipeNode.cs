using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeNode : MonoBehaviour
{
    public GameObject pipePiece = null;
    public GameObject[] neighbours;

    public void UpdatePipePiece(GameObject value)
    {
        pipePiece = value;
        //Manager.instance
    }
}
