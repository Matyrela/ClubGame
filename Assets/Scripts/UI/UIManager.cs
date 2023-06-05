using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Movement dataIMovement;

    [SerializeField] private Slider forceSlider;
    [SerializeField] private Image clickSign;
    [SerializeField] private TMP_Text forceText;
    void Update()
    {
        clickSign.color = dataIMovement.playerClick ? Color.green : Color.red;
        forceText.text = dataIMovement.force + "";
        forceSlider.value = dataIMovement.force;
    }
}
