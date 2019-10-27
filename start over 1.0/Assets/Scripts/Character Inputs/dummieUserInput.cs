using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummieUserInput : UserInput
{
    // Start is called before the first frame update
    public GameObject cameraPos;
    public cameraController cc;

    void Start()
    {
        cc = cameraPos.GetComponent<cameraController>();
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if (cc.lockStateAI)
        {
            //run = true;
            if(gameObject.GetComponent<StateManager>().isLowLevel)
            {
                Dup = 1.0f;
            }else if(gameObject.GetComponent<StateManager>().isInvisible)
            {
                Dup = 1.5f;
            }
            
            
            if (cc.attackAct)
            {
                mouseL = true;
            }
            else if(!cc.attackAct)
            {
                mouseL = false;
            }
            if (gameObject.GetComponent<StateManager>().isAttack)
            {
                Dup = 0.0f;
            }
        }
        else
        {
            mouseL = false;
            Dup = 0f;
        }

        if (gameObject.GetComponent<StateManager>().isDie)
        {
            Dup = 0;
        }
        UpdateDmagDvec(Dup, Dright);
    }
}
