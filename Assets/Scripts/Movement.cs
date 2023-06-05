using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float force = 0;
    public bool canLaunch = true;
    public bool playerClick = false;

    [SerializeField] private Rigidbody2D characterRigid;
    [SerializeField] private Rigidbody2D hammerBottomRigid;
    [SerializeField] private Rigidbody2D hammerUpRigid;

    [SerializeField] private GameObject character;
    [SerializeField] private GameObject hammer;

    private void FixedUpdate()
    {
        if (playerClick && Input.GetMouseButton(0))
        {
            ResetRigidbody(characterRigid);
            ResetRigidbody(hammerBottomRigid);
            ResetRigidbody(hammerUpRigid);

            ResetConstraints(characterRigid);
            ResetConstraints(hammerUpRigid);

            Vector3 direction = character.transform.position - hammer.transform.position;
            direction.Normalize();
            Vector3 forceVector = direction * (force * 200);
            characterRigid.AddForce(forceVector, ForceMode2D.Impulse);

            force = 0;
            playerClick = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (canLaunch)
            {
                if (force < 1)
                {
                    force += 0.025f;
                }
                else
                {
                    force = 1;
                }
            }
        }
        else
        {
            if (force > 0)
            {
                if (canLaunch)
                {
                    canLaunch = false;
                    playerClick = false;
                    LaunchHammer();
                }
            }
            else
            {
                force = 0;
            }
        }
    }

    private void LaunchHammer()
    {
        characterRigid.constraints = RigidbodyConstraints2D.FreezeAll;

        Debug.Log("Applied Torque to Hammer: " + force * 5000);

        Vector2 torque = hammerBottomRigid.transform.right * (force * 5000);
        hammerBottomRigid.AddForce(torque * GetTorqueDir());
    }

    private void ResetRigidbody(Rigidbody2D rigidbody)
    {
        rigidbody.angularVelocity = 0;
        rigidbody.velocity = Vector2.zero;
    }

    private void ResetConstraints(Rigidbody2D rigidbody)
    {
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hammer"))
        {
            characterRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            LaunchPlayer();
        }
    }

    private void LaunchPlayer()
    {
        if (!canLaunch)
        {
            playerClick = true;
            hammerUpRigid.constraints = RigidbodyConstraints2D.FreezeAll;

            Debug.Log("Applied Torque to Player: " + force * 5000);

            Vector2 torque = characterRigid.transform.up * (force * 5000);
            characterRigid.AddForce(torque * -GetTorqueDir());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall") && !playerClick)
        {
            Vector2 inverseForce = -hammerBottomRigid.velocity;
            hammerBottomRigid.AddForce(inverseForce, ForceMode2D.Impulse);
        }
    }
    private int GetTorqueDir()
    {
        if (hammer.transform.position.x > character.transform.position.x)
        {
            return -1;
        }
        return 1;
    }
    
    public void BounceOffWall()
    {
        if (!playerClick)
        {
            Vector2 inverseForce = -hammerBottomRigid.velocity;
            hammerBottomRigid.AddForce(inverseForce, ForceMode2D.Impulse);
        }
    }

    public void OnHammerHit(Collider2D collider)
    {
        characterRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        LaunchPlayer();
    }

    public void OnPlayerHit()
    {
        canLaunch = true;
        playerClick = false;
        hammerUpRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        force = 0;
    }
}
