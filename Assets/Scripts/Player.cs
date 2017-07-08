using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public float jumpStrength;

    bool grounded = false;

    bool jumped = false;
    bool facingRight = true;

    public Rigidbody2D rb;
    public Animator anim;
    public BoxCollider2D jumpBox;
    public GameObject fireballPosition;

    private PlayerWeaponStates currentMehrio = PlayerWeaponStates.big;
    

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        HitboxUpdate();

        ControlUpdate();
        
        AnimationUpdate();
	}

    void HitboxUpdate()
    {
        bool temp = false;
        if (!grounded) temp = false;
        grounded = jumpBox.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (!temp && grounded)
        {
            jumped = false;
        }        
    }

    void ControlUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if (Input.GetAxis ("Vertical") > 0.3f)
        {
            JumpMehrio();
        }
    }

    void AnimationUpdate()
    {
        anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        
        //Facing direction
        if (facingRight && (Input.GetAxis("Horizontal") < 0))
        {
            facingRight = false;
            FlipSprite();
        } else if (!facingRight && (Input.GetAxis("Horizontal") > 0))
        {
            facingRight = true;
            FlipSprite();
        }

        anim.SetBool("grounded", grounded);
    }

    void FlipSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        bool temp = sr.flipX;
        sr.flipX = !temp;
    }

    void JumpMehrio()
    {
        if (grounded && !jumped)
        {
            jumped = true;
            Vector2 jVector = new Vector2(0, jumpStrength);
            rb.AddForce(jVector);
        }
    }
}

public enum PlayerWeaponStates
{
    fireball,
    big,
    small
}