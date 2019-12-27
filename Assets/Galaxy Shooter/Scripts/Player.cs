using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0F;

    // Start is called before the first frame update
    void Start()
    {
        // current pos = new position
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

        // if player on the y is greater than 0 set player position on the Y to 0
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2F)
        {
            transform.position = new Vector3(transform.position.x, -4.2F, 0);
        }

        // if player on the x is greater than 9.5, position on the X needs to be -9.5
        if (transform.position.x > 9.5F)
        {
            transform.position = new Vector3(-9.5F, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5F)
        {
            transform.position = new Vector3(9.5F, transform.position.y, 0);
        }
    }
}
