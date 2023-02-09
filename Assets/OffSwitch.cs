using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSwitch : MonoBehaviour
{

    [SerializeField] Transform tableTrans;
    [SerializeField] GameObject chordButton1, chordButton2, chordButton3, chordButton4, chordButton5, chordButton6, chordButton7;
    private List<GameObject> chords = new();

    private void Start()
    {
        tableTrans = this.transform;
        chords.Add(GameObject.Find(chordButton1.name));
        chords.Add(GameObject.Find(chordButton2.name));
        chords.Add(GameObject.Find(chordButton3.name));
        chords.Add(GameObject.Find(chordButton4.name));
        chords.Add(GameObject.Find(chordButton5.name));
        chords.Add(GameObject.Find(chordButton6.name));
        chords.Add(GameObject.Find(chordButton7.name));
    }

    public void TurnOff()
    {
       // Debug.Log(this.name + " TurnOff() triggered.");
        
        foreach (GameObject chord in chords)
        {
            bool chordOn = chord.GetComponent<ChordScript>().chordOn;
           // Debug.Log("chord " + chord.name + " selected. isOn = " + chordOn);

            if (chordOn)
            {
               // Debug.Log(chord.name + " isOn selected - > ToggleOn");
                chord.GetComponent<ChordScript>().ToggleOn();
               // Debug.Log(chord.name + " Disabling Emission.");
                chord.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            }

            //else return;

        }

        foreach (Transform child in tableTrans)
        {
            PitchMaterialManager pMan = child.GetComponent<PitchMaterialManager>();
            pMan.TurnOff();
            pMan.ChordLock(false);
        }
        
    }


   
}
