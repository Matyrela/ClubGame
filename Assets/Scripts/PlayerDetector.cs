using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] Movement player;

    private void OnTriggerEnter2D(Collider2D col)
    {
        player.OnPlayerHit();
    }
}
