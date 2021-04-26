using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotateOn { X, Z};

public class OpenBriefcase : MonoBehaviour
{
    private float maxAngle;

    public float goalAngle;
    public float speed;
    public RotateOn direction;
    private GameObject axis;
    private float startTime;
    private bool moveLid = false;
    private Quaternion startRot;
    private Quaternion endRot;
    

    // Start is called before the first frame update
    void Start()
    {
        axis = transform.parent.gameObject;

        startRot = axis.transform.rotation;

        switch (direction)
        {
            case RotateOn.X:
                endRot = Quaternion.Euler(-goalAngle, axis.transform.rotation.eulerAngles.y, axis.transform.rotation.eulerAngles.z);
                break;
            case RotateOn.Z:
                endRot = Quaternion.Euler(-goalAngle, axis.transform.rotation.eulerAngles.z, axis.transform.rotation.eulerAngles.y);
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

            Debug.Log(Quaternion.Lerp(startRot, endRot, fractionOfJourney).eulerAngles);
            axis.transform.rotation = Quaternion.Lerp(startRot, endRot, fractionOfJourney);
        }
        
    }

    public void OpenCase()
    {
        Debug.LogWarning("Case should open now");

        startTime = Time.time;
        moveLid = true;
    }
}
