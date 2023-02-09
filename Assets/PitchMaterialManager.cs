using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchMaterialManager : MonoBehaviour
{
    public bool isOn, isHovered, isPlaying, chordLocked;
    //public bool 
    public Material offIdle, offHovered, onIdle, onHovered, held;
    public AudioSource sound;

    private void Start()
    {
        // Get AudioSource
        sound = this.GetComponent<AudioSource>();
    }

    public void ExitHover()
    {
        if (!isHovered) return;

        if (isHovered)
        {
            isHovered = false;
            

            if (!isOn)
            {
                this.GetComponent<MeshRenderer>().material = offIdle;
                sound.Pause();
                isPlaying = false;
            }

            else if (isOn && !chordLocked)
            {
                this.GetComponent<MeshRenderer>().material = onIdle;
            }

            else if (isOn && chordLocked)
            {
                this.GetComponent<MeshRenderer>().material = onIdle;
            }
        }
    }

    public void EnterHover()
    {
        if (isHovered) return;

        else if (!isHovered)
        {
            isHovered = true;

            if (isOn)
            {
                this.GetComponent<MeshRenderer>().material = onHovered;
            }

            else
            {
                this.GetComponent<MeshRenderer>().material = held;
                sound.Play();
                isPlaying = true;
            }
        }
    }

   

    public void ToggleOn()
    {
        //Debug.Log(this.name + " ToggleOn() Reporting status:");
        //this.ReportStatus();

        if (!isOn)
        {
            //Debug.Log(this.name + " if !isOn triggered");
            isOn = true;
            if (!isPlaying) 
            {
                isPlaying = true;
                sound.Play();
            }
            this.GetComponent<MeshRenderer>().material = onIdle;
        }

        else if (isOn && !chordLocked)
        {
            //change material to hovered
            if (isHovered)
            {
                /**/               //this.GetComponent<MeshRenderer>().material = offHovered;
                isOn = false;
                chordLocked = false;
                this.GetComponent<MeshRenderer>().material = held;
            }

            //change material to idle
            else
            {
                isOn = false;
                sound.Pause();
                isPlaying = false;
                this.GetComponent<MeshRenderer>().material = offIdle;
            }
        }

        //Debug.Log(this.name + "End ToggleOn() Reporting Status:");
    }

    public void TurnOff()
    {
        isOn = false;
        isHovered = false;
        this.GetComponent<MeshRenderer>().material = offIdle;
        sound.Pause();
        isPlaying = false;
    }

    public bool GetIsOn()
    {
        return isOn;
    }

    public void ChordLock(bool newstate)
    {
        chordLocked = newstate;
        //Debug.Log(this.name + " chordLocked = " + chordLocked);
    }

    public void ReportStatus()
    {
        Debug.Log(this.name + " isOn = " + isOn + ". isHovered = " + isHovered + ". isPlaying = " + isPlaying + ". chordLocked = " + chordLocked);
    }

    public void ChordtoHovered()
    {
        chordLocked = false;
        isPlaying = true;
        isHovered = true;
        isOn = false;
    }
}
