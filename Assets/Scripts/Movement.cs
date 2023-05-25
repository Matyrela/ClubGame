using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] private float force = 0;
    public bool canLaunch = true;
    public bool playerClick = false;

    [SerializeField] private Slider forceSlider;
    
    [SerializeField] private Rigidbody2D characterRigid;
    [SerializeField] private Rigidbody2D hammerBottomRigid;
    [SerializeField] private Rigidbody2D hammerUpRigid;

    [SerializeField] private GameObject character;
    [SerializeField] private GameObject hammer;
    void FixedUpdate()
    {
        if (playerClick && Input.GetMouseButton(0))
        {
            playerClick = false;
            characterRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            hammerUpRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            hammerBottomRigid.constraints = RigidbodyConstraints2D.None;
            
            //----------------
            characterRigid.velocity = Vector3.zero;
            hammerBottomRigid.angularVelocity = 0;
            hammerBottomRigid.velocity = Vector3.zero;
            characterRigid.angularVelocity = 0;
            hammerUpRigid.velocity = Vector3.zero;
            hammerUpRigid.angularVelocity = 0;
            //----------------

            Vector3 direction = character.transform.position - hammer.transform.position;
            direction.Normalize();

            Vector3 force2 = direction * (force * 200);
            characterRigid.AddForce(force2, ForceMode2D.Impulse);
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
                    launchHammer();
                }
            }
            else
            {
                force = 0;
            }
        }
        
        forceSlider.value = force;
    }

    void launchHammer()
    {
        characterRigid.constraints = RigidbodyConstraints2D.FreezeAll;
        
        Debug.Log("Applied Troque to Hammer: " + force * 5000);
        
        Vector2 torq = (hammerBottomRigid.transform.right * (force * 5000));
        hammerBottomRigid.AddForce(torq * getTorqueDir());
    }
    
    void launchPlayer()
    {
        if (!canLaunch)
        {
            playerClick = true;
            hammerUpRigid.constraints = RigidbodyConstraints2D.FreezeAll;
        
            Debug.Log("Applied Troque to Player: " + force * 5000);
        
            Vector2 torq = (characterRigid.transform.up * (force * 5000));
            characterRigid.AddForce(torq * (getTorqueDir() * -1));
        }
    }

    public void RebotePared()
    {
        if (!playerClick)
        {
            Vector2 inverseForce = -hammerBottomRigid.velocity;
            hammerBottomRigid.AddForce(inverseForce, ForceMode2D.Impulse);
        }
        
    }

    public void onHammerHit(Collider2D asd)
    {
        characterRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        Vector3 triggerPosition = asd.gameObject.transform.position;
        Vector3 newPosition = new Vector3(triggerPosition.x, triggerPosition.y + 0.01f, triggerPosition.z);
        hammer.transform.position = newPosition;
        launchPlayer();
    }
    
    public void onPlayerHit()
    {
        canLaunch = true;
        playerClick = false;
        force = 0;
    }
    
    int getTorqueDir()
    {
        if (hammer.transform.position.x > character.transform.position.x)
        {
            return -1;
        }
        return 1;
    }
}
