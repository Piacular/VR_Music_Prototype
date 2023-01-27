using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] bool turningOff = false;
    [SerializeField] GameObject table;
    [SerializeField] OffSwitch offScript;
    [SerializeField] Animator buttonAnim;

    private void Start()
    {
        offScript = table.GetComponent<OffSwitch>();
        buttonAnim = GetComponentInParent<Animator>();
    }

    public void TurnOff()
    {
        Debug.Log("TurnOff() called in ButtonScript");
        
        if (turningOff) return;
        turningOff = true;
        buttonAnim.Play("Button Pressed");
        offScript.TurnOff();
        turningOff = false;
    }
}
