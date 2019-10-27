using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraController : MonoBehaviour
{
    private UserInput pi;
    public float horizontalSpeed;
    public float verticalSpeed;
    public float cameraDampValue;
    public bool isAI = false;
    public Image lockDot;
    public bool lockState;
    public bool lockStateAI;
    public bool attackAct = false;
    public bool charge = false;
    public GameObject uiObject;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    [HideInInspector]
    private GameObject model;
    private GameObject cam;
    private Vector3 cameraDampVelocity;
    //[SerializeField]
    private LockTarget lockTarget;
    


    // Start is called before the first frame update

    void Start()
    {
        if(gameObject.name == "bossCam" || gameObject.name == "npcCam")
        {
            uiObject.SetActive(false);
        }
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
        ActorController ac = playerHandle.GetComponent<ActorController>();
        model = ac.model;
        pi = ac.pi;

        if (!isAI)
        {
            cam = Camera.main.gameObject;
            lockDot.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        lockState = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lockTarget == null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;

            playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
            tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
            tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
            cameraHandle.transform.localEulerAngles = new Vector3(
                tempEulerX, 0, 0);

            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;


            cameraHandle.transform.LookAt(lockTarget.obj.transform);
        }

        if (!isAI)
        {
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, transform.position, ref cameraDampVelocity, cameraDampValue);
            cam.transform.LookAt(cameraHandle.transform);
        } 
    }

    private void Update()
    {

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var player = GameObject.Find("playerHandle");
        var boss = GameObject.Find("Boss");
        foreach (var enemy in enemies)
        {
            if (Vector3.Distance(enemy.transform.position, player.transform.position) < 7.0f)
            {
                LockProcessB(new LockTarget(player, 0.6f), true, true, isAI);
                if (enemy.gameObject.GetComponent<StateManager>().isLowLevel)
                {
                    if (Vector3.Distance(enemy.transform.position, player.transform.position) >= 1.0f)
                    {
                        attackAct = false;
                    }
                    else if (Vector3.Distance(enemy.transform.position, player.transform.position) < 1.0f)
                    {
                        attackAct = true;
                    }
                } else if (enemy.gameObject.GetComponent<StateManager>().isCurseOfBeauty)
                {
                    if (Vector3.Distance(enemy.transform.position, player.transform.position) > 3.0f)
                    {
                        charge = false;
                        attackAct = false;
                    }
                    else if (Vector3.Distance(enemy.transform.position, player.transform.position) < 3.0f)
                    {
                        if (Vector3.Distance(enemy.transform.position, player.transform.position) >= 1.5f)
                        {
                            charge = true;
                            attackAct = false;
                        }
                        else if (Vector3.Distance(enemy.transform.position, player.transform.position) < 1.5f)
                        {
                            attackAct = true;
                        }
                    }
                } else if (enemy.gameObject.GetComponent<StateManager>().isInvisible)
                {
                    if (Vector3.Distance(enemy.transform.position, player.transform.position) >= 1.5f)
                    {
                        attackAct = false;
                    }
                    else if (Vector3.Distance(enemy.transform.position, player.transform.position) < 1.5f)
                    {
                        attackAct = true;
                    }
                }
            }
        }

        if (boss != null) { 
            if (Vector3.Distance(boss.transform.position, player.transform.position) < 15.0f)
            {
                boss.GetComponent<bossInput>().cameraPos.GetComponent<cameraController>().uiObject.SetActive(true);
                LockProcessB(new LockTarget(player, 0.6f), true, true, isAI);
                if (Vector3.Distance(boss.transform.position, player.transform.position) >= 3.0f)
                {
                    attackAct = false;
                }
                else if (Vector3.Distance(boss.transform.position, player.transform.position) < 3.0f)
                {
                    attackAct = true;
                }
            }
        }


        if (lockTarget != null)
        {
            if (lockTarget.am != null && lockTarget.am.sm.isDie)
            {
                if(lockTarget.obj.GetComponent<StateManager>().isLowLevel || lockTarget.obj.GetComponent<StateManager>().isInvisible)
                {   
                    lockTarget.dui.cameraPos.GetComponent<cameraController>().uiObject.SetActive(false);
                    
                }
                else if (lockTarget.obj.GetComponent<StateManager>().isCurseOfBeauty)
                {
                    lockTarget.avi.cameraPos.GetComponent<cameraController>().uiObject.SetActive(false);
                }else if (lockTarget.obj.GetComponent<StateManager>().isPlayer)
                {
                    LockProcessB(null, false, false, isAI);
                }
                LockProcessA(null, false, false, isAI);
            }
            //print(lockTarget.halfHeight);
            if (!isAI && lockTarget != null)
            {
                if(lockTarget.obj.tag == "Enemy")
                {
                    if (lockTarget.obj.GetComponent<StateManager>().isLowLevel || lockTarget.obj.GetComponent<StateManager>().isInvisible)
                    {
                        lockTarget.dui.cameraPos.GetComponent<cameraController>().uiObject.SetActive(true);
                    }else if(lockTarget.obj.GetComponent<StateManager>().isCurseOfBeauty)
                    {
                        lockTarget.avi.cameraPos.GetComponent<cameraController>().uiObject.SetActive(true);
                    }
                }

                lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0, lockTarget.halfHeight, 0));

                if(lockTarget.obj.tag == "Enemy")
                {
                    if (Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 7.0f)
                    {
                        if (lockTarget.obj.GetComponent<StateManager>().isLowLevel || lockTarget.obj.GetComponent<StateManager>().isInvisible)
                        {
                            lockTarget.dui.cameraPos.GetComponent<cameraController>().uiObject.SetActive(false);
                        }
                        else if (lockTarget.obj.GetComponent<StateManager>().isCurseOfBeauty)
                        {
                            lockTarget.avi.cameraPos.GetComponent<cameraController>().uiObject.SetActive(false);
                        }
                        LockProcessA(null, false, false, isAI);
                    }
                }else if(lockTarget.obj.tag == "Boss")
                {
                    if(Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 12.0f)
                    {
                        LockProcessA(null, false, false, isAI);
                    }
                }
                

            }
            else if(isAI && lockTarget != null)
            {
                if (gameObject.name == "bossCam")
                {
                    if (Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 15.0f)
                    {
                        boss.GetComponent<bossInput>().cameraPos.GetComponent<cameraController>().uiObject.SetActive(false);
                        LockProcessB(null, false, false, isAI);
                    }
                }else if(gameObject.name == "npcCam")
                {
                    if (Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 7.0f)
                    {
                        LockProcessB(null, false, false, isAI);
                    }
                }
                
            }
        }
    }

    private void LockProcessA(LockTarget _lockTarget, bool _lockDotEnable, bool _lockState, bool _isAI)
    {
        if (!isAI)
        {
            lockTarget = _lockTarget;
            lockDot.enabled = _lockDotEnable;
            lockState = _lockState;
        }
    }

    private void LockProcessB(LockTarget _lockTarget, bool _lockDotEnable, bool _lockState, bool _isAI)
    {
        if (isAI)
        {
            lockTarget = _lockTarget;
            lockStateAI = _lockState;
        }
    }

    public void LockUnlock()
    {

        //try to lock
        Vector3 modelOrigin1 = model.transform.position;
        Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
        Vector3 boxCentre = modelOrigin2 + model.transform.forward * 5.0f;
        Collider[] cols = Physics.OverlapBox(boxCentre, new Vector3(0.5f, 0.5f, 5f),
            model.transform.rotation, LayerMask.GetMask(isAI?"Player": "Enemy"));

        
        if (cols.Length == 0)
        {
            LockProcessA(null, false, false, isAI);
            LockProcessB(null, false, false, isAI);
        }
        else
        {
            foreach (var col in cols)
            {
                if (lockTarget != null && lockTarget.obj == col.gameObject)
                {
                    if(lockTarget.obj.GetComponent<StateManager>().isLowLevel || lockTarget.obj.GetComponent<StateManager>().isInvisible)
                    {
                        lockTarget.dui.cameraPos.GetComponent<cameraController>().uiObject.SetActive(false);
                    }else if(lockTarget.obj.GetComponent<StateManager>().isCurseOfBeauty)
                    {
                        lockTarget.avi.cameraPos.GetComponent<cameraController>().uiObject.SetActive(false);
                    }
                    LockProcessA(null, false, false, isAI);
                    break;
                }
                LockProcessA(new LockTarget(col.gameObject, col.bounds.extents.y), true, true, isAI);
                
                break;

            }
        }
    }

    private class LockTarget
    {
        public GameObject obj;
        public float halfHeight;
        public ActorManager am;
        public StateManager sm;
        public dummieUserInput dui = null;
        public advancedInput avi = null;

        public LockTarget(GameObject _obj, float _halfHeight)
        {
            obj = _obj;
            halfHeight = _halfHeight;
            am = _obj.GetComponent<ActorManager>();
            sm = _obj.GetComponent<StateManager>();
            dui = _obj.GetComponent<dummieUserInput>();
            avi = _obj.GetComponent<advancedInput>();
        }
    }
}
