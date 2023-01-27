using System.Collections.Generic;
using UnityEngine;

public class ChordScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject Pitch1;
    [SerializeField] GameObject Pitch2;
    [SerializeField] GameObject Pitch3;
    [SerializeField] Material idle, hovered;
    public bool isHovered, isOn;
    private List<GameObject> pitches = new();
    public bool chordOn = false;


    void Start()
    {
        GameObject firstP = GameObject.Find(Pitch1.name);
        GameObject secondP = GameObject.Find(Pitch2.name);
        GameObject thirdP = GameObject.Find(Pitch3.name);
        
        pitches.Add(firstP);
        pitches.Add(secondP);
        pitches.Add(thirdP);
    }

    public void ToggleOn()
    {
        Debug.Log(this.name + " ToggleOn() triggered. chordOn = " + chordOn + " . isHovered = " + isHovered + ". chordOn = " + chordOn + ". 1");

        if (!chordOn)
        {
            chordOn = true;
            Debug.Log(this.name + " chordOn set to " + chordOn + ". 2");
            this.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            Debug.Log(this.name + " _EMISSION set to " + this.GetComponent<MeshRenderer>().material.IsKeywordEnabled("_EMISSION") + ". 3");
        }

        else if (chordOn)
        {
            Debug.Log(this.name + " chordOn set to true");
            chordOn = false;
            this.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }

        foreach (GameObject pitch in pitches)
        {
            Debug.Log(this.name + " foreach loop triggered. 4");
            bool pitchOn = pitch.GetComponent<PitchMaterialManager>().GetIsOn();
            Debug.Log(pitch.name + " pitchOn = " + pitchOn);

            if (chordOn)
            {
                Debug.Log(this.name + " if (chordOn) triggered.");
                pitch.GetComponent<PitchMaterialManager>().ChordLock(true);
                if (!pitchOn) pitch.GetComponent<PitchMaterialManager>().ToggleOn();
            }

            else if (!chordOn)
            {
                bool tempHov = pitch.GetComponent<PitchMaterialManager>().isHovered;
                Debug.Log(this.name + " if (!chordOn) triggered.");
                pitch.GetComponent<PitchMaterialManager>().ChordLock(false);
                //NEED TO CHANGE BELOW TO MAKE IT SO PITCH BLOCK GETS CONVERTED TO HOVERED INSTEAD OF STAYING BLUE
                if (pitchOn && !tempHov) pitch.GetComponent<PitchMaterialManager>().ToggleOn();
            }
        }

    }

    public void ToggleHovered()
    {
        //Debug.Log("toggleHovered() triggered while isHovered is " + isHovered + "." );

        if (!isHovered && !chordOn)
        {
            //Debug.Log("isHovered being set to " + isHovered + ".");
            isHovered = true;
            this.GetComponent<MeshRenderer>().material = hovered;
        }

        else if (isHovered && !chordOn)
        {
            //Debug.Log("isHovered being set to " + isHovered + ".");
            isHovered = false;
            this.GetComponent<MeshRenderer>().material = idle;
        }
    }

    public void EmissionOn()
    {
        Debug.Log("Turning Emission on.");
        this.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
    }

    public void EmissionOff()
    {
        Debug.Log("Turning Emission off.");
        this.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
    }
}
