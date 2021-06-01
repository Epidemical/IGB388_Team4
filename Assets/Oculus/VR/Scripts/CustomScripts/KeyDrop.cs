using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDrop : MonoBehaviour
{
    public GameObject anchor;
    public Material LRMat;
    private LineRenderer LR;

    public bool pickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        //setup line renderer (string)
        LR = anchor.GetComponent<LineRenderer>();

        if (LR == null)
        {
            LR = anchor.AddComponent<LineRenderer>();

            if (this.name == "Key")
            {
                LR.endWidth = 0.01f;
                LR.startWidth = 0.01f;
                LR.material = LRMat;
            }
            else
            {
                LR.endWidth = 0;
                LR.startWidth = 0;
            }
                
        }
        LR.SetPosition(0, anchor.transform.position);
        LR.SetPosition(1, this.gameObject.transform.position);

        //setup object itself
        SpringJoint SJ = this.gameObject.GetComponent<SpringJoint>();
        if(SJ == null)
        {
            SJ = this.gameObject.AddComponent<SpringJoint>();
            SJ.connectedBody = anchor.GetComponent<Rigidbody>();
            

            if (this.name == "Key")
            {
                SJ.spring = 15;
                SJ.damper = 1;
                SJ.tolerance = 0;
            }
            else
            {
                SJ.spring = 10;
                SJ.damper = 10;
                SJ.tolerance = 0.025f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!pickedUp)
        {
            LR.SetPosition(1, this.gameObject.transform.position);
        }
    }

    public void DisconnectKey()
    {
        pickedUp = true;
        Destroy(this.GetComponent<SpringJoint>());
        //this.GetComponent<SpringJoint>().connectedBody = null;
        Destroy(LR);
        Destroy(this);
    }
}
