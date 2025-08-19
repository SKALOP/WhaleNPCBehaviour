using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotOnAStick : MonoBehaviour
{
Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    //make the object move around the scene, turning 90 degrees every time it hits a wall
    rb.velocity = -this.transform.right * 1;
        if (Physics.Raycast(this.transform.position, -this.transform.right, 3.4f))
				{
					this.transform.Rotate(0, this.transform.rotation.y + 90, 0);
				}
				
    }
}
