using UnityEngine;
using System;

public class Radio : MonoBehaviour
{
    public AudioSource static1;
    public AudioSource static2;
    public AudioSource instructionAudio;

    private bool on = false;
    private Switch radioSwitch;

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
            instructionAudio.volume = Convert.ToSingle(-.5 * Mathf.Cos((4/6)*progress * 2 * Mathf.PI) + .5);

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
        }
        else
        {
            static1.Stop();
            static2.Stop();
            instructionAudio.Stop();
        }
    }
}
