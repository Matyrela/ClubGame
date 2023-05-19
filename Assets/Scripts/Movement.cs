using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] private float force = 0;
    public bool canLaunch = true;

    [SerializeField] private Slider forceSlider;
    
    [SerializeField] private Rigidbody2D characterRigid;
    [SerializeField] private Rigidbody2D hammerRigid;

    [SerializeField] private GameObject character;
    [SerializeField] private GameObject hammer;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!canLaunch)
            {
                return;
            }
            
            if (force < 1)
            {
                force += 0.0005f;
                forceSlider.value = force;
            }
            else
            {
                force = 1;
            }
        }
        else
        {
            if (force > 0)
            {
                if (canLaunch)
                {
                    launchHammer();
                    canLaunch = false;
                }

                force -= 0.005f;
                forceSlider.value = force;
            }
            else
            {
                force = 0;
            }
        }
    }

    void launchHammer()
    {
        characterRigid.constraints = RigidbodyConstraints2D.FreezeAll;
        
        Debug.Log("Applied Troque: " + force * 5000);
        
        Vector2 torq = (hammerRigid.transform.right * (force * 5000));
        hammerRigid.AddForce(torq * getTorqueDir());
    }

    int getTorqueDir()
    {
        if (hammer.transform.position.x > character.transform.position.x)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
