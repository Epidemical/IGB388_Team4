using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeNode : MonoBehaviour
{
    public GameObject pipePiece = null;
    public GameObject[] neighbours;

    public void UpdatePipePiece(GameObject value)
    {
        //pipe piece is being removed
        if(!value)
        {
            Debug.Log("Piece is being removed");
            Manager.instance.PipePuzzleRemove(this.gameObject);
        }
        else
        {
            List<GameObject> connectionNodes = new List<GameObject>();

            foreach(GameObject endPiece in value.GetComponent<PipeSnap>().ends)
            {
                float shortestDistance = 0f;
                GameObject closestNode = null;

                foreach(GameObject neighbour in neighbours)
                {
                    float distanceTo = Vector3.Distance(endPiece.transform.position, neighbour.transform.position);

                    if (shortestDistance == 0f || distanceTo < shortestDistance)
                    {
                        shortestDistance = distanceTo;
                        closestNode = neighbour;
                    }
                }

                connectionNodes.Add(closestNode);
            }

            Manager.instance.PipePuzzleAdd(this.gameObject, connectionNodes);
        }


        pipePiece = value;
    }
}
