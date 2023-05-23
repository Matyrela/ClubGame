using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] Movement player;

    private void OnCollisionEnter2D(Collision2D col)
    {
        player.onPlayerHit();
    }
}
