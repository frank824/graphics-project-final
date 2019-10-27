using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : ActorManagerInterface
{
    private Collider weaponColL;
    private Collider weaponColR;

    public GameObject whL;
    public GameObject whR;

    public weaponController wcL;
    public weaponController wcR;

    void Start()
    {
        whL = transform.DeepFind("weaponHandleL").gameObject;
        whR = transform.DeepFind("weaponHandleR").gameObject;

        wcL = BindWeaponController(whL);
        wcR = BindWeaponController(whR);

        weaponColL = whL.GetComponentInChildren<Collider>();
        weaponColR = whR.GetComponentInChildren<Collider>();

        weaponDisable();
        //print(transform.DeepFind("weaponHandleR"));
    }

    public weaponController BindWeaponController(GameObject targetObj)
    {
        weaponController tempWc;
        tempWc = targetObj.GetComponent<weaponController>();
        if (tempWc == null)
        {
            tempWc = targetObj.AddComponent<weaponController>();
        }
        tempWc.wm = this;

        return tempWc;
    }
    
    public void weaponEnable()
    {
        if (am.ac.checkStateTag("attackL")){
            weaponColL.enabled = true;
        }
        else
        {
            weaponColR.enabled = true;
        }     
    }
    public void weaponDisable()
    {
        weaponColL.enabled = false;
        weaponColR.enabled = false; 
    }
}
