using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBriefcase : MonoBehaviour
{
    private float maxAngle;

    // Start is called before the first frame update
    void Start()
    {
        maxAngle = GetComponent<HingeJoint>().limits.max;

        GetComponent<Rigidbody>().AddForce(new Vector3(0f, 10f, 0f));
        GetComponent<HingeJoint>().useMotor = true;

    }

    // Update is called once per frame
    void Update()
    {
        //check if upper angle reached
        if (this.transform.rotation.z >= maxAngle)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<HingeJoint>().useMotor = false;

        }
    }

    public void OpenCase()
    {
        Debug.LogWarning("Case should open now");
        GetComponent<Rigidbody>().AddForce(new Vector3(0f, 10f, 0f));
    }
}
