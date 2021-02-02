using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    int speed;
    Rigidbody2D rb;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        rb.velocity = transform.right * 10;
	}
    void OnTriggerEnter2D(Collider2D cd)
    {
        if(cd.gameObject.tag=="Enemy")
        {
            Destroy(cd.gameObject);
        }
    }
}
