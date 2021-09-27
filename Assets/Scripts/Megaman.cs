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

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        Saltar();
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
        if (isGrounded())
        {
            myAnimator.SetBool("falling", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAnimator.SetTrigger("takeof");
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            }
        }
        else
        {
            myAnimator.SetBool("falling", true);
        }
    }

    bool isGrounded()
    {
        return pies.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
