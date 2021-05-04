using UnityEngine;
using UnityEngine.Events;

public class MyButton : MonoBehaviour {
    [System.Serializable]
    public class ButtonEvent : UnityEvent { }

    public float pressLength;
    public bool pressed;
    public ButtonEvent downEvent;

    Vector3 startPos;
    Rigidbody rb;

    void Start() {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        // If our distance is greater than what we specified as a press
        // set it to our max distance and register a press if we haven't already
        float distance = Mathf.Abs(transform.position.y - startPos.y);
        if (distance >= pressLength && pressed == false) {
            // Prevent the button from going past the pressLength
            transform.position = new Vector3(transform.position.x, startPos.y - pressLength, transform.position.z);
            if (!pressed) {
                pressed = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;

                // If we have an event, invoke it
                downEvent?.Invoke();
                GetComponent<AudioSource>().Play();
            }
        }
        //else {
        //    // If we aren't all the way down, reset our press
        //    //pressed = false;
        //}
        // Prevent button from springing back up past its original position
        if (transform.position.y > startPos.y && pressed == false) {
            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        }
    }
}