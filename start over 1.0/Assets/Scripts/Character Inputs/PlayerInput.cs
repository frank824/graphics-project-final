using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : UserInput
{
    [Header("====== Mouse Settings ======")]
    public bool mouseEnable = true;
    public float mouseSensitivityX;
    public float mouseSensitivityY;

    [Header("====== Key Settings ======")]
    public string keyUp;
    public string keyDown;
    public string keyLeft;
    public string keyRight;

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;
    public string keyE;

    public string keyJright;
    public string keyJleft;
    public string keyJdown;
    public string keyJup;
    

    public MyButton buttonA = new MyButton();
    public MyButton buttonB = new MyButton();
    public MyButton buttonC = new MyButton();
    public MyButton buttonD = new MyButton();
    public MyButton buttonE = new MyButton();

    // Update is called once per frame
    void Update()
    {
        buttonA.Tick(Input.GetKey(keyA));
        buttonB.Tick(Input.GetKey(keyB));
        buttonC.Tick(Input.GetKey(keyC));
        buttonD.Tick(Input.GetKey(keyD));
        buttonE.Tick(Input.GetKey(keyE));

        if (mouseEnable == true)
        {
            Jup = Input.GetAxis("Mouse Y") *3f* mouseSensitivityY;
            Jright = Input.GetAxis("Mouse X") *2.5f* mouseSensitivityX;
        }
        else
        {
            Jup = (Input.GetKey(keyJup) ? 1.0f : 0) - (Input.GetKey(keyJdown) ? 1.0f : 0);
            Jright = (Input.GetKey(keyJright) ? 1.0f : 0) - (Input.GetKey(keyJleft) ? 1.0f : 0);
        }

        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f:0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if (inputEnabled == false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

        
        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;

        run = buttonA.IsPressing;
        mouseR = buttonD.IsPressing;
        jump = buttonB.OnPressed;
        
        mouseL = buttonC.OnPressed;
        lockon = buttonE.OnPressed;
    }

    
}
