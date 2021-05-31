using UnityEngine;
using System;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    public AudioSource static1;
    public AudioSource static2;
    public AudioSource instructionAudio;

    private bool on = false;
    private Switch radioSwitch;

    public GameObject textObj;
    private float textStart = 80f;
    private float textRange = 80f;


    // Start is called before the first frame update
    void Start()
    {
        static1.Stop();
        static2.Stop();
        instructionAudio.Stop();

        radioSwitch = GetComponent<Switch>();

        //RadioButtonPress();
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            float progress = radioSwitch.PercentThrough();

            static1.volume =  Convert.ToSingle(.5 * Mathf.Cos(progress * 4 * Mathf.PI) + .5);
            static2.volume =  Convert.ToSingle(.5 * Mathf.Sin(progress * 2 * Mathf.PI) + .5);
            instructionAudio.volume = Convert.ToSingle(-.5 * Mathf.Cos((4/3)*progress * Mathf.PI) + .5);

            float newText = textStart + (progress * textRange);
            newText = (float) Math.Round(newText, 2);
            textObj.GetComponent<Text>().text = newText.ToString();
        }
    }

    public void ChangeState()
    {
        on = !on;

        if (on)
        {
            static1.Play();
            static2.Play();
            instructionAudio.Play();

            textObj.SetActive(true);
        }
        else
        {
            static1.Stop();
            static2.Stop();
            instructionAudio.Stop();

            textObj.SetActive(false);
        }
    }
}
