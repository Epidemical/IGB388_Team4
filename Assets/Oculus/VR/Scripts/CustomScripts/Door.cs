using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float distance;
    bool called = false;
    float journeyTime = 3f;
    float timeProgress = 0f;

    Vector3 startPos;
    Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (called)
        {
            if (timeProgress >= journeyTime)
                called = false;
            else
                timeProgress += Time.deltaTime;

            float progress = timeProgress / journeyTime;

            transform.position = Vector3.Lerp(startPos, endPos, progress);
        }
    }

    public void Open()
    {
        called = true;
    }
}
