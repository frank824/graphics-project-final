using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class advancedInput : UserInput
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
            if (gameObject.GetComponent<StateManager>().isAttack)
            {
                Dup = 0.0f;
                run = false;
            }
            else if (!gameObject.GetComponent<StateManager>().isAttack)
            {
                if (cc.charge)
                {
                    Dup = 1.5f;
                    run = true;
                }
                else
                {
                    Dup = 1.0f;
                    run = false;
                }
                
            }
            //run = true;

            if (cc.attackAct)
            {
                mouseL = true;
            }
            else if (!cc.attackAct)
            {
                mouseL = false;

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
