using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public float jumpStrength;
    

    bool grounded = false;

    bool jumped = false;
    bool facingRight = true;


    public float deathJumpForce;

    public Rigidbody2D rb;
    public Animator anim;
    public BoxCollider2D jumpBox;
    public GameObject fireballPosition;
    public GameObject fireball;
    public GameObject deadMario;

    private PlayerWeaponStates currentMehrio = PlayerWeaponStates.small;
    

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

        if (transform.position.x <= 0)
        {
            transform.position = new Vector2(0, transform.position.y);
        }

        if ((transform.position.y <= -1f))
        {
            Die();
        }


        //if (Input.GetAxis ("Vertical") > 0.3f)
        if (Input.GetKey(KeyCode.Z))
        {
            JumpMehrio();
        }

        if (Input.GetKey(KeyCode.X))
        {
            if (currentMehrio == PlayerWeaponStates.fireball)
            {
                FireFireball();
            }
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

        if (currentMehrio == PlayerWeaponStates.small)
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Small Layer"), 1);
            anim.SetLayerWeight(anim.GetLayerIndex("Fire Layer"), 0);
            anim.SetLayerWeight(anim.GetLayerIndex("Base Layer"), 0);
        } else if (currentMehrio.Equals(PlayerWeaponStates.fireball))
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Small Layer"), 0);
            anim.SetLayerWeight(anim.GetLayerIndex("Fire Layer"), 1);
            anim.SetLayerWeight(anim.GetLayerIndex("Base Layer"), 0);
        } else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Small Layer"), 0);
            anim.SetLayerWeight(anim.GetLayerIndex("Fire Layer"), 0);
            anim.SetLayerWeight(anim.GetLayerIndex("Base Layer"), 1);
        }
    }

    void FireFireball()
    {
        GameObject bullet = (GameObject)Instantiate(fireball, fireballPosition.transform.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().Fire(facingRight);
    }

    void FlipSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        bool temp = sr.flipX;
        sr.flipX = !temp;
        flipFireballPlace();
    }

    void flipFireballPlace()
    {
        Vector3 posToGo = Vector3.zero;

        posToGo.x = (facingRight?1:-1) * .44f;
        posToGo.y = -.2f;
        fireballPosition.transform.localPosition = posToGo;
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

    public void fireFlower()
    {
        switch (currentMehrio)
        {
            case PlayerWeaponStates.big:
                currentMehrio = PlayerWeaponStates.fireball;
                break;
            case PlayerWeaponStates.fireball:
                break;
            case PlayerWeaponStates.small:
                currentMehrio = PlayerWeaponStates.big;
                break;
        }
    }

    void Die()
    {
        Debug.Log("WELP I'm dead!");
        //Make marioclone
        GameObject corpse = (GameObject)Instantiate(deadMario, transform.position, Quaternion.identity);
        corpse.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, deathJumpForce));
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Coin")
        {
            Debug.Log("COIN GET!!!!!");
            Destroy(col.gameObject);
        }
    }
}

public enum PlayerWeaponStates
{
    fireball,
    big,
    small
}