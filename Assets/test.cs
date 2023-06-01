using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed; // spin speed
    float angle1; // angle1 will be negative to normal angle
    public Rigidbody2D rb;
    float angle;     
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate ()
    {
        faceMouse();
    }
    
    void faceMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);     
        angle = Mathf.Atan2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y) * 180 / Mathf.PI; //Get mouse angle
        rb.rotation %= 360; // dont remember why i did this but better dont remove it
        angle = (angle + rb.rotation); // Sum up rigidbody and mouse angle
        if (angle < 0) angle1 = 360.0f + angle;
        else angle1 = 360.0f - angle; // calculates negative angle
        if (Mathf.Abs(angle) > Mathf.Abs(angle1) && angle < 0)
            angle = angle1;
        if (Mathf.Abs(angle) > Mathf.Abs(angle1) && angle > 0)
            angle = angle1 * -1;
        rb.AddTorque(-angle / 180 * speed);
    }
}
