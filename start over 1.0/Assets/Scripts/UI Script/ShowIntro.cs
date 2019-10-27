using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIntro : MonoBehaviour
{
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(true);
        StartCoroutine("WaitForSec");
    }


    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        text.SetActive(false);
    }
}
