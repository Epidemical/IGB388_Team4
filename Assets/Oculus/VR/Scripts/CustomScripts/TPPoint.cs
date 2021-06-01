using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPoint : MonoBehaviour
{
    public Material validMat;
    public Material invalidMat;

    public void Highlighted()
    {
        GetComponent<MeshRenderer>().material = validMat;
    }

    public void NotHighlighted()
    {
        GetComponent<MeshRenderer>().material = invalidMat;
    }
}
