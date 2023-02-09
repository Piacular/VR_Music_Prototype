using System.Collections.Generic;
using UnityEngine;

public class ChordScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject Pitch1;
    [SerializeField] GameObject Pitch2;
    [SerializeField] GameObject Pitch3;
    [SerializeField] Material idle, hovered, emitting;
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
        //.Log(this.name + " ToggleOn() triggered. Reporting Status 01: *******************");
       // this.ReportStatus();

        if (!chordOn)
        {
            chordOn = true;
            //Debug.Log(this.name + " chordOn set to " + chordOn + ". 2");
            this.GetComponent<MeshRenderer>().material = emitting;
            //Debug.Log(this.name + " _EMISSION set to " + this.GetComponent<MeshRenderer>().material.IsKeywordEnabled("_EMISSION") + ". 3");

        }

        else if (chordOn)
        {
            //Debug.Log(this.name + " chordOn set to false");
            chordOn = false;
            this.GetComponent<MeshRenderer>().material = hovered;
        }

       // Debug.Log(this.name + " updated status 01:");
       // this.ReportStatus();

        foreach (GameObject pitch in pitches)
        {
           // Debug.Log(pitch.name + " foreach loop triggered. Reporting Status 02:");
            bool pitchOn = pitch.GetComponent<PitchMaterialManager>().GetIsOn();
            //pitch.GetComponent<PitchMaterialManager>().ReportStatus();
            //Debug.Log(pitch.name + " pitchOn = " + pitchOn + ". 5");

            if (chordOn)
            {
               // Debug.Log(pitch.name + " if (chordOn) triggered. Reporting Status 03:");
               // pitch.GetComponent<PitchMaterialManager>().ReportStatus();

                pitch.GetComponent<PitchMaterialManager>().ChordLock(true);
                if (!pitchOn) pitch.GetComponent<PitchMaterialManager>().ToggleOn();

               // Debug.Log(pitch.name + " updated status 03:");
                //pitch.GetComponent<PitchMaterialManager>().ReportStatus();
            }

            else if (!chordOn)
            {
               // Debug.Log(pitch.name + " if (!chordOn) triggered. Reporting Status 04:");
               // pitch.GetComponent<PitchMaterialManager>().ReportStatus();
                
                pitch.GetComponent<PitchMaterialManager>().ChordLock(false);

               // Debug.Log(pitch.name + " updated Status 04:");
               // pitch.GetComponent<PitchMaterialManager>().ReportStatus();

               
                /**if (pitchOn && !tempHov)*/
                pitch.GetComponent<PitchMaterialManager>().ToggleOn();
            }

           // Debug.Log(pitch.name + " updated status 02:");
           // pitch.GetComponent<PitchMaterialManager>().ReportStatus();
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
        //Debug.Log("Turning Emission on.");
        this.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
    }

    public void EmissionOff()
    {
        //Debug.Log("Turning Emission off.");
        this.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");

   
    }

    public void ReportStatus()
    {
        Debug.Log(this.name + " isHovered = " + isHovered + ". isOn = " + isOn + ". chordOn = " + chordOn);
    }
}
