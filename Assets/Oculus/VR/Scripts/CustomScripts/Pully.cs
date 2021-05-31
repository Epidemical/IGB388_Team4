using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pully : MonoBehaviour
{
    public GameObject anchor;
    private LineRenderer LR;

    public float goalDistance;
    private bool called = false;

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
        LR.SetPosition(1, this.gameObject.transform.position);

        if(Vector3.Distance(anchor.transform.position, this.gameObject.transform.position) > goalDistance && !called)
        {
            called = true;
            Manager.instance.DropKey();

            //play sound and haptics here
            gameObject.GetComponent<OVRGrabbable>().grabbedBy.GetComponent<OculusHaptics>().Vibrate(VibrationForce.Hard);
        }
    }
}
