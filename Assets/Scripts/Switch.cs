using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [System.Serializable]
    public class SwitchEvent : UnityEvent { }

    public float pushLength;
    public bool pressed;
    public SwitchEvent downEvent;
    public bool movingRight;
    public bool isRadio = false;

    Vector3 startPos;
    Rigidbody rb;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // If our distance is greater than what we specified as a switch
        // set it to our max distance and register a hit if we haven't already
        float distance = Mathf.Abs(transform.position.z - startPos.z);
        if (distance >= pushLength)
        {
            // Prevent the switch from going past the pressLength
            if(movingRight)
                transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z + pushLength);
            else
                transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z - pushLength);

            if (!pressed && !isRadio)
            {
                pressed = true;
                // If we have an event, invoke it
                downEvent?.Invoke();
                GetComponent<AudioSource>().Play();

                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
        //else
        //{
        //    // If we aren't all the way down, reset our press
        //    pressed = false;
        //}
        // Prevent button from springing back up past its original position
        if (transform.position.z < startPos.z && movingRight)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z);
        }
        else if (transform.position.z > startPos.z && !movingRight)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z);
        }

    }

    public float PercentThrough()
    {
        float distanceTravelled = 0;

        if (movingRight)
            distanceTravelled = transform.position.z - startPos.z;
        else
            distanceTravelled = startPos.z - transform.position.z;

        return distanceTravelled / pushLength;
    }
}
