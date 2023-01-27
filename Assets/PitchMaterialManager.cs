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
        if (!isHovered || chordLocked) return;

        if (isHovered)
        {
            isHovered = false;
            
            if (!isOn)
            {
                this.GetComponent<MeshRenderer>().material = offIdle;
                sound.Pause();
                isPlaying = false;
            }

            else if (isOn)
            {
                this.GetComponent<MeshRenderer>().material = onIdle;
            }
        }
    }

    public void EnterHover()
    {
        if (isHovered ||chordLocked) return;

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
        if (!isOn)
        {
            isOn = true;
            if (!isPlaying) 
            { 
                sound.Play();
                isPlaying = false;
            }
            this.GetComponent<MeshRenderer>().material = onHovered;
        }

        else if (isOn && !chordLocked)
        {
            isOn = false;
            sound.Pause();
            isPlaying = false;

            //change material to hovered
            if (isHovered)
            {
                this.GetComponent<MeshRenderer>().material = offHovered;
            }

            //change material to idle
            else this.GetComponent<MeshRenderer>().material = offIdle;

        }
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
        Debug.Log(this.name + " chordLocked = " + chordLocked);
    }
}
