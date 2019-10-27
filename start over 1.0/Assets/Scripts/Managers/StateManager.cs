using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StateManager : ActorManagerInterface
{
    public float HP = 15.0f;
    public float HPmax = 15.0f;
	public GameObject character;

    [Header("1sr order state flags")]
    public bool isGround;
    public bool isJump;
    public bool isFall;
    public bool isRoll;
    public bool isJab;
    public bool isAttack;
    public bool isHit;
    public bool isDie;
    public bool isBlock;
    public bool isDefense;
    public bool isLowLevel;
    public bool isCurseOfBeauty;
    public bool isInvisible;
    public bool isPlayer;
    public bool isBoss;
    public Image uiBar;

    [Header("2nd order state flag")]
    public bool isAllowDefense;
    public bool isImmortal;

    // Start is called before the first frame update
    void Start()
    {
        HP = HPmax;
        BarUpdate();
    }

    public void AddHp(float value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, HPmax);
        BarUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        isGround = am.ac.checkState("ground");
        isJump = am.ac.checkState("jump");
        isFall = am.ac.checkState("fall");
        isRoll = am.ac.checkState("roll");
        isJab = am.ac.checkState("jab");
        isAttack = am.ac.checkStateTag("attackR") || am.ac.checkStateTag("attackL");
        isHit = am.ac.checkState("hit");
        isDie = am.ac.checkState("die");
        isBlock = am.ac.checkState("block");


        isAllowDefense = isGround || isBlock;
        isDefense = isAllowDefense && am.ac.checkState("defense1h", "defense");
        isImmortal = isRoll || isJab;

		if (character.tag == "Player"){
			if (isDie) {
				StartCoroutine("WaitForSec1");
				
			}
		}
		else if (character.tag == "Boss") {
			if (isDie) {
				StartCoroutine("WaitForSec2");
				
			}
		}
		
    }

    private void BarUpdate()
    {
        uiBar.fillAmount = HP / HPmax;
    }

	IEnumerator WaitForSec1 () {
        Cursor.lockState = CursorLockMode.None;
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene(2);
		
	}

	IEnumerator WaitForSec2 () {
        Cursor.lockState = CursorLockMode.None;
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene(3);
	}
}
