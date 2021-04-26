using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResponse : MonoBehaviour
{
    public void ButtonTest()
    {
        print("Button Pressed");
    }

    public void OpenBriefcase(GameObject lid)
    {
        lid.GetComponent<OpenBriefcase>().OpenCase();

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
