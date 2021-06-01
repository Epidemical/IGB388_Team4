using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotateOn { X, Z};

public class OpenLid : MonoBehaviour
{
    private float maxAngle;

    public float goalAngle;
    public float speed;
    public RotateOn direction;
    //private GameObject axis;
    private float startTime;
    private bool moveLid = false;
    private Quaternion startRot;
    private Quaternion endRot;
    private Rigidbody rb;
    

    // Start is called before the first frame update
    void Start()
    {
        startRot = transform.rotation;
        rb = this.GetComponent<Rigidbody>();

        switch (direction)
        {
            case RotateOn.X:
                endRot = Quaternion.Euler(-goalAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                break;
            case RotateOn.Z:
                endRot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, goalAngle);
                break;
        }
        //endRot = Quaternion.Euler(-goalAngle, axis.transform.rotation.eulerAngles.z, axis.transform.rotation.eulerAngles.y);
        //OpenCase();
    }

    // Update is called once per frame
    void Update()
    {
        //check if upper angle reached
        if (moveLid)
        {
            float progress = (Time.time - startTime) * speed;
            float fractionOfJourney = progress / goalAngle;

            //Debug.Log(Quaternion.Lerp(startRot, endRot, fractionOfJourney).eulerAngles);
            this.transform.rotation = Quaternion.Lerp(startRot, endRot, fractionOfJourney);

            if(fractionOfJourney >= 0.95)
            {
                moveLid = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }

        }
        
    }

    public void OpenCase()
    {
        switch (direction)
        {
            case RotateOn.X:
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
                break;
            case RotateOn.Z:
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                break;
        }

        startTime = Time.time;
        moveLid = true;
        GetComponent<AudioSource>().Play();

        MyButton[] buttons = this.gameObject.GetComponentsInChildren<MyButton>();

        foreach(MyButton button in buttons)
        {
            button.alwaysReset = false;
            button.pressed = true;
        }

        Rigidbody[] rbs = this.gameObject.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody body in rbs)
        {
            body.constraints = RigidbodyConstraints.FreezeAll;
        }

    }
}
