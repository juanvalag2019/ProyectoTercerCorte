using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaman : MonoBehaviour
{
    // Start is called before the first frame update
    Animator myAnimator;
    [SerializeField] float speed;
    [SerializeField] BoxCollider2D pies;
    [SerializeField] Sprite idleSprite;
    [SerializeField] Sprite fallingSprite;
    [SerializeField] Rigidbody2D myBody;
    [SerializeField] float jumpSpeed;

    SpriteRenderer myRenderer;
    BoxCollider2D myCollider;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        Saltar();
        Falling();
        Fire();
    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.X))
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    void Mover()
    {
        float mov = Input.GetAxis("Horizontal");
        if (mov != 0)
        {
            myAnimator.SetBool("running", true);
            transform.localScale = new Vector2(Mathf.Sign(mov), 1);
            transform.Translate(new Vector2(mov * speed * Time.deltaTime, 0));
        }
        else
        {
            myAnimator.SetBool("running", false);
        }
    }
    void Saltar()
    {

        /*
        si no usaramos animaciones:
        if (isGrounded)
        {
            myRenderer.sprite = idleSprite;
        }
        else
        {
            myRenderer.sprite = fallingSprite;
        }*/

        if (isGrounded() && !myAnimator.GetBool("jumping"))
        {
            myAnimator.SetBool("falling", false);
            myAnimator.SetBool("jumping", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAnimator.SetTrigger("takeof");
                myAnimator.SetBool("jumping", true);
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            }
        }
    }

    bool isGrounded()
    {
        RaycastHit2D myRaycast = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, myCollider.bounds.extents.y + 0.2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(myCollider.bounds.center, new Vector2(0, (myCollider.bounds.extents.y + 0.2f) * -1), Color.cyan);
        return myRaycast.collider != null;
        // return pies.IsTouchingLayers(LayerMask.GetMask("Ground"));

    }

    void AfterTakeOfEvent()
    {
        myAnimator.SetBool("jumping", false);
        myAnimator.SetBool("falling", true);
    }

    void Falling()
    {
        if (myBody.velocity.y < 0 && !myAnimator.GetBool("jumping"))
        {
            myAnimator.SetBool("falling", true);
        }
    }
}
