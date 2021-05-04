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
        // set it to our max distance and register a switch if we haven't already
        float distance = Mathf.Abs(transform.position.x - startPos.x);
        if (distance >= pushLength)
        {
            // Prevent the switch from going past the pressLength
            if(movingRight)
                transform.position = new Vector3(startPos.x + pushLength, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(startPos.x - pushLength, transform.position.y, transform.position.z);

            if (!pressed)
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
        if (transform.position.x < startPos.x && movingRight)
        {
            transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > startPos.x && !movingRight)
        {
            transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);
        }

    }
}
