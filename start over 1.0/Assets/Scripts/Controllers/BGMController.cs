using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMController : MonoBehaviour
{
    public Slider BGMSlider;
    public AudioSource BGMSource;

    void Update()
    {
        BGMSource.volume = BGMSlider.value;

    }
}
