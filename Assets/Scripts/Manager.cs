using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject lid;
    public GameObject door;
    public int goal = 5;
    private int count = 0;

    public int[] SSPatternOrder = new int[6];
    private int SSPatternIndex = 0;
    private MyButton[] SSButtons;


    private void Start()
    {
        GameObject buttonCollection = GameObject.Find("SimonSaysButtons");
        if (buttonCollection)
        {
            SSButtons = buttonCollection.GetComponentsInChildren<MyButton>();
        }
        else
        {
            Debug.LogWarning("simon says buttons not found");
        }
    }

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


    private void OpenBriefcase(GameObject lid)
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

    public void OpenDoor()
    {
        door.GetComponent<Door>().Open();
    }

    public void SimonSays(int buttonIndex)
    {
        Debug.Log(buttonIndex);
        Debug.Log(SSPatternOrder[SSPatternIndex]);
        if(buttonIndex == SSPatternOrder[SSPatternIndex])
        {
            SSPatternIndex++;
        }
        else
        {
            foreach(MyButton button in SSButtons)
            {
                button.ResetButton();
                SSPatternIndex = 0;
            }
        }

        if(SSPatternIndex == SSPatternOrder.Length - 1)
        {
            Debug.Log("SIMON SAYS COMPLETE");
        }
    }
}
