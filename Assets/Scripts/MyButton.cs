using UnityEngine;
using UnityEngine.Events;

public enum Axis
{
    X,
    Y,
    Z
}

public class MyButton : MonoBehaviour {
    [System.Serializable]
    public class ButtonEvent : UnityEvent { }

    public float pressLength;
    public bool pressed;
    public ButtonEvent downEvent;
    public Axis direction = Axis.Y;
    public bool movingToNegative = true;

    Vector3 startPos;
    Rigidbody rb;

    void Start() {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();

        ResetButton();
    }

    void Update() {
        // If our distance is greater than what we specified as a press
        // set it to our max distance and register a press if we haven't already
        float distance = 0f;

        switch (direction)
        {
            case Axis.X:
                distance = Mathf.Abs(transform.position.x - startPos.x);
                break;
            case Axis.Y:
                distance = Mathf.Abs(transform.position.y - startPos.y);
                break;
            case Axis.Z:
                distance = Mathf.Abs(transform.position.z - startPos.z);
                break;
        }

        if (distance >= pressLength && !pressed) {
            // Prevent the button from going past the pressLength
            //transform.position = new Vector3(transform.position.x, startPos.y - pressLength, transform.position.z);
            switch (direction)
            {
                case Axis.X:
                    if (movingToNegative)
                    {
                        transform.position = new Vector3(startPos.x - pressLength, transform.position.y, transform.position.z);
                    }
                    else
                    {
                        transform.position = new Vector3(startPos.x + pressLength, transform.position.y, transform.position.z);
                    }
                    break;
                case Axis.Y:
                    transform.position = new Vector3(transform.position.x, startPos.y - pressLength, transform.position.z);
                    break;
                case Axis.Z:
                    if (movingToNegative)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z - pressLength);
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z + pressLength);
                    }
                    break;
            }

            if (!pressed) {
                pressed = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;

                // If we have an event, invoke it
                downEvent?.Invoke();
                GetComponent<AudioSource>().Play();
            }
        }

        //ugly code, consider revisiting
        // Prevent button from springing back up past its original position
        if (!pressed)
        {

            if (direction == Axis.X && transform.position.x > startPos.x && movingToNegative)
            {
                transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);
            }
            else if (direction == Axis.X && transform.position.x < startPos.x && !movingToNegative)
            {
                transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);
            }

            else if (direction == Axis.Y && transform.position.y > startPos.y)
            {
                transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
            }

            else if (direction == Axis.Z && transform.position.z > startPos.z && movingToNegative)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z);
            }
            else if (direction == Axis.Z && transform.position.z < startPos.z && !movingToNegative)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z);
            }
        }

    }

    public void ResetButton()
    {
        pressed = false;
        transform.position = startPos;

        // set the rigidbody constraints based on the direction
        switch (direction)
        {
            case Axis.X:
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                break;
            case Axis.Y:
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                break;
            case Axis.Z:
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                break;
        }
    }
}