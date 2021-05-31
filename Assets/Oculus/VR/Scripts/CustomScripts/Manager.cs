using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    //singleton code
    public static Manager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }


    public GameObject lid;
    public GameObject door;
    public GameObject radioSwitch;
    public GameObject key;

    public int goal = 5;
    private int count = 0;

    //simon says variables
    public int[] SSPatternOrder = new int[6];
    private int SSPatternIndex = 0;
    private MyButton[] SSButtons;

    // pipe puzzle variables
    // <node the piece is on, nodes the piece connects to>
    private Dictionary<GameObject, List<GameObject>> pipeConnections = new Dictionary<GameObject, List<GameObject>>();
    private List<GameObject> searchedPipeKeys = new List<GameObject>();
    public GameObject startNode;
    public GameObject startNodeConnection;
    public GameObject endNode;
    public GameObject endNodeConnection;
    public GameObject middleNode;
    public List<GameObject> middleConnections;

    // briefcase variables
    public Text leftText;
    public Text middleText;
    public Text rightText;
    public int briefcaseCode = 001;

    //note appearance
    public GameObject note1;
    public GameObject note2;
    public GameObject note3;


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

        pipeConnections.Add(startNode, new List<GameObject> { startNodeConnection });
        pipeConnections.Add(endNode, new List<GameObject> { endNodeConnection });
        pipeConnections.Add(middleNode, middleConnections);
    }

    public void ButtonTest()
    {
        print("Button Pressed");
    }

    public void CheckCode()
    {
        int digL = Mathf.FloorToInt(briefcaseCode / 100);
        int digM = Mathf.FloorToInt((briefcaseCode % 100) / 10);
        int digR = Mathf.FloorToInt((briefcaseCode % 100) % 10);

        if (digL == int.Parse(leftText.text) 
            && digM == int.Parse(middleText.text)
            && digR == int.Parse(rightText.text)
            && note1.activeSelf
            && note2.activeSelf
            && note3.activeSelf)
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
            note2.SetActive(true);
        }
    }

    public void RadioButtonPress()
    {
        radioSwitch.GetComponent<Radio>().ChangeState();
    }

    public void PipePuzzleAdd(GameObject nodeFrom, List<GameObject> nodesTo)
    {
        Debug.Log(nodesTo);
        pipeConnections.Add(nodeFrom, nodesTo);
        bool complete = PipePuzzleSearch(startNode, null);
        searchedPipeKeys = new List<GameObject>();

        if (complete)
            note1.SetActive(true);
        else
            Debug.Log("PUZZLE INCOMPLETE");
    }

    public void PipePuzzleRemove(GameObject node)
    {
 
        pipeConnections.Remove(node);
        //Debug.Log(pipeConnections.ContainsKey(node));
    }

    //searches the collection of nodes for a valid path
    // recursive, call starting with the start node
    private bool PipePuzzleSearch(GameObject searchNode, GameObject nodeFrom)
    {
        bool validPath = false;
        searchedPipeKeys.Add(searchNode);
        List<GameObject> connectedNodes = pipeConnections[searchNode];

        //if the node being searched is the first starting node
        if(searchNode == startNode)
        {
            //check that the connected node has a key in the dictionary ie a piece is placed in the connected node
            if (pipeConnections.ContainsKey(connectedNodes[0]))
            {
                validPath = PipePuzzleSearch(connectedNodes[0], searchNode);
            }
        }
        //if node being searched is the end node
        else if(searchNode == endNode)
        {
            //search completed successfully, end search
            validPath = true;
        }
        //node being searched is somewhere inbetween
        else
        {
            GameObject nextNode = null;
            bool connectionFound = false;
            //check that this piece connects to the piece we just came from
            foreach(GameObject node in connectedNodes)
            {
                if (node.Equals(nodeFrom))
                    connectionFound = true;
                else
                    nextNode = node;
            }

            if (!connectionFound)
                validPath = false;
            else if (pipeConnections.ContainsKey(nextNode) && !searchedPipeKeys.Contains(nextNode))
            {
                validPath = PipePuzzleSearch(nextNode, searchNode);
            }
            else
                validPath = false;
        }


        return validPath;
    }

    public void DropKey()
    {
        key.SetActive(true);
    }

    public void ChangeBriefcaseNumberLeft(int direction)
    {
        int newNumber = int.Parse(leftText.text) + direction;

        if (newNumber > 9)
            newNumber = 0;
        else if (newNumber < 0)
            newNumber = 9;

        leftText.text = newNumber.ToString();

        CheckCode();
    }

    public void ChangeBriefcaseNumberMiddle(int direction)
    {
        int newNumber = int.Parse(middleText.text) + direction;

        if (newNumber > 9)
            newNumber = 0;
        else if (newNumber < 0)
            newNumber = 9;

        middleText.text = newNumber.ToString();

        CheckCode();

    }

    public void ChangeBriefcaseNumberRight(int direction)
    {
        int newNumber = int.Parse(rightText.text) + direction;

        if (newNumber > 9)
            newNumber = 0;
        else if (newNumber < 0)
            newNumber = 9;

        rightText.text = newNumber.ToString();

        CheckCode();

    }
}
