using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResponse : MonoBehaviour
{
    public GameObject lid;
    public int goal = 5;
    private int count = 0;
    public void ButtonTest()
    {
        print("Button Pressed");
    }

    public void ButtonPressed()
    {
        count++;
        if (count == goal)
            OpenBriefcase(lid);
    }


    public void OpenBriefcase(GameObject lid)
    {
        lid.GetComponent<OpenBriefcase>().OpenCase();
        //Debug.Log("BRIEFCASE OPENING");
        //try
        //{
        //    lid.GetComponent<OpenBriefcase>().OpenCase();
        //}
        //catch
        //{
        //    Debug.LogError("OpenBriefcase component not found");
        //}
    }
}
