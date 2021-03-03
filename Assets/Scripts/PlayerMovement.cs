using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 5;

    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        myRigidbody.AddForce(0, 0, forwardSpeed * Time.deltaTime);
        Move();
    }

    void Move()
    {
        #region Keyboard Input Control
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.position.x < 0) { return; }
            transform.position += Vector3.left * 2.5f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.x > 0) { return; }
            transform.position += Vector3.right * 2.5f;
        }
        #endregion

        #region Touch Input Control
        if (SwipeManager.IsSwipingLeft())
        {
            if (transform.position.x < 0) { return; }
            transform.position += Vector3.left * 2.5f;
        }

        if (SwipeManager.IsSwipingRight())
        {
            if (transform.position.x > 0) { return; }
            transform.position += Vector3.right * 2.5f;
        }
        #endregion
    }
}
