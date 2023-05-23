using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HammerDetector : MonoBehaviour
{
    [SerializeField] Movement player;
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Col");
        player.onHammerHit();
    }
}
