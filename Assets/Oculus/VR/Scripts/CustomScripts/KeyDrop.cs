using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDrop : MonoBehaviour
{
    public GameObject anchor;
    private LineRenderer LR;

    public bool pickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        LR = anchor.GetComponent<LineRenderer>();
        LR.SetPosition(0, anchor.transform.position);
        LR.SetPosition(1, this.gameObject.transform.position);
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
        Destroy(LR);
    }
}
