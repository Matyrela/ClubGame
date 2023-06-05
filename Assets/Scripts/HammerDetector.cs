using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HammerDetector : MonoBehaviour
{
    [SerializeField] Movement player;
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Hammer Detect");
        player.OnHammerHit(col);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Boing");
        player.BounceOffWall();
    }
}
