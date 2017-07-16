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

    Rigidbody2D rb;
    Animator anim;
    public BoxCollider2D jumpBox;
    public GameObject fireballPosition;
    public GameObject fireball;
    public GameObject deadMario;
    GameObject godObject;


    bool endingLevel = false;
    bool flagpoleJump = false;
    public float endTimer;
    float endCount = 0f;

    public PlayerWeaponStates currentMehrio = PlayerWeaponStates.small;
    

	// Use this for initialization
	void Start () {
        godObject = GameObject.FindGameObjectWithTag("GodObject");
        currentMehrio = godObject.GetComponent<GodScript>().lastMehrioState;
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        HitboxUpdate();

        if (!endingLevel)
        {
            ControlUpdate();
        }
        
        AnimationUpdate();

        EndingLevelUpdate();
	}

    void HitboxUpdate()
    {
        bool temp = false;
        if (!grounded) temp = false;
        //grounded = jumpBox.IsTouchingLayers(LayerMask.GetMask("Ground"));
        grounded = gameObject.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (!temp && grounded)
        {
            jumped = false;
        }        
    }

    void ControlUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if (transform.position.x <= .25f)
        {
            transform.position = new Vector2(.25f, transform.position.y);
        }

        if ((transform.position.y <= -1f))
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            godObject.GetComponent<GodScript>().RestartLevel();
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
        if (!endingLevel)
        {
            anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));


            //Facing direction
            if (facingRight && (Input.GetAxis("Horizontal") < 0))
            {
                facingRight = false;
                FlipSprite();
            }
            else if (!facingRight && (Input.GetAxis("Horizontal") > 0))
            {
                facingRight = true;
                FlipSprite();
            }
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

    void EndingLevelUpdate()
    {
        if (endingLevel)
        {
            if (transform.position.y > 2.9f)
            {
                //move down
                transform.position = new Vector2(transform.position.x, transform.position.y - 1.0f);
                if (transform.position.y < 2.9f)
                {
                    transform.position = new Vector2(transform.position.x, 2.9f);
                }
            }
            else if (!flagpoleJump)
            {
                Debug.Log("Flagpole Jump");
                transform.position = new Vector2(transform.position.x + 1.6f, transform.position.y);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                flagpoleJump = true;
            }
            else if (endCount < endTimer)
            {
                if (endCount > (endTimer - 1.0f))
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    Debug.Log("Should be moving right");
                    rb.simulated = true;
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                endCount += Time.deltaTime;
            }
            else
            {
                rb.simulated = false;
                //Give the godobject our state
                godObject.GetComponent<GodScript>().lastMehrioState = currentMehrio;
                godObject.GetComponent<GodScript>().NextLevel();
            }
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
            Vector2 jVector = new Vector2(rb.velocity.x, jumpStrength);
            rb.velocity = jVector;
        }
    }

    void mushroom()
    {
        switch (currentMehrio)
        {
            case PlayerWeaponStates.big:
            case PlayerWeaponStates.fireball:
                break;
            case PlayerWeaponStates.small:
                currentMehrio = PlayerWeaponStates.big;
                break;
        }
    }

    void fireFlower()
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
        godObject.GetComponent<GodScript>().lastMehrioState = PlayerWeaponStates.small;
        //Make marioclone
        GameObject corpse = (GameObject)Instantiate(deadMario, transform.position, Quaternion.identity);
        corpse.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, deathJumpForce));
        corpse.GetComponent<deadMario>().gs = godObject.GetComponent<GodScript>();
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Coin")
        {
            Debug.Log("COIN GET!!!!!");
            Destroy(col.gameObject);
        }

        if (col.transform.tag == "Mushroom")
        {
            mushroom();
            Destroy(col.gameObject);
        }

        if (col.transform.tag == "FireFlower")
        {
            fireFlower();
            Destroy(col.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Goal")
        {
            EndLevel();
        }
    }


    void EndLevel()
    {
        rb.velocity = Vector2.zero;
        endingLevel = true;
        rb.simulated = false;
    }
}

public enum PlayerWeaponStates
{
    fireball,
    big,
    small
}