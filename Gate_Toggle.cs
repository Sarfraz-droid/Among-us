using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_Toggle : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void GateToggle()
    {
        if (anim.GetBool("Toggle"))
        {
            anim.SetBool("Toggle", false);
        }
        else
        {
            anim.SetBool("Toggle", true);
        }
    }
}
