using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;

    public Transform jumpCheck;   
    bool jumpPressed = false;    

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(horizontalInput * 5.0f, rigidbody.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(rigidbody.velocity.x));

        if ((rigidbody.velocity.x > 0 && transform.localScale.x < 0) ||
            (rigidbody.velocity.x < 0 && transform.localScale.x > 0))
        {
            Vector3 newScale = new Vector3(-1f * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
        if (jumpPressed &&
            Physics2D.OverlapCircle(jumpCheck.position, 0.2f, LayerMask.GetMask("Ground")))
        {
            rigidbody.AddForce(new Vector2(0f, 300f));   
            jumpPressed = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
    }
}
