using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : ActorManagerInterface
{
	private CapsuleCollider defCol;
    private float bossDmg = 13f;
    private float playerDmg = 8f;
    private float normalDmg = 6f;
    private float advanceDmg = 10f;
    private float invisibleDmg = 4f;

    void Start()
	{
        defCol = GetComponent<CapsuleCollider>();
        defCol.center = Vector3.up*0.6f;
        defCol.height = 1.2f;
        defCol.radius = 0.5f;
        defCol.isTrigger = true;
	}


    
    void OnTriggerEnter(Collider col)
    {
        
        weaponController targetWc = col.GetComponentInParent<weaponController>();
        
        GameObject attacker = targetWc.wm.am.gameObject;
        
        GameObject receiver = am.gameObject;
        
        Vector3 attackingDir = receiver.transform.position - attacker.transform.position;

        float attackingAngle1 = Vector3.Angle(attacker.transform.forward, attackingDir);

        if(attackingAngle1 <= 45)
        {
            if (gameObject.name == "sensor")
            {
                if (col.tag == "bossWeapon")
                {
                    am.TryDoDamage(bossDmg);
                }
                else if (col.tag == "normalWeapon")
                {
                    am.TryDoDamage(normalDmg);
                }
                else if (col.tag == "advancedWeapon")
                {
                    am.TryDoDamage(advanceDmg);
                }else if (col.tag == "invisibleWeapon")
                {
                    am.TryDoDamage(invisibleDmg);
                }
            }else if(gameObject.name == "npcSensor")
            {
                if(col.tag == "playerWeapon")
                {
                    am.TryDoDamage(playerDmg);
                }
            }
            
        }
    }
}
