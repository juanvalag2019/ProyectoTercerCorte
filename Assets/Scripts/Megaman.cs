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
    [SerializeField] GameObject deathParticles;
    [SerializeField] AudioClip deathAudio;
    bool pause = false;
    [SerializeField] GameObject bullet;

    [SerializeField] float fireInterval = 2;
    float nextFireAt, tamX, tamY;
    bool lastDirection = true;

    SpriteRenderer myRenderer;
    BoxCollider2D myCollider;
    float layerTime = 2;
    int jump;
    float dash;
    bool pressZ;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        dash = 0;
        pressZ = true;
        tamX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        tamY = (GetComponent<SpriteRenderer>()).bounds.size.y;
        StartCoroutine(ShowTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            Mover();
            Saltar();
            Falling();
            Fire();
            Dash();
        }
    }

    IEnumerator ShowTime()
    {
        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            count++;
            Debug.Log("Tiempo: " + count);
        }
    }
    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.X) && Time.time >= nextFireAt)
        {
            myAnimator.SetLayerWeight(1, 1);
            layerTime = 5;
            Vector3 spawnPos = transform.position + new Vector3(lastDirection ? tamX / 2 : -tamX / 2, +0.08f, 0);
            GameObject bullet = Instantiate(this.bullet, spawnPos, transform.rotation);
            bullet.GetComponent<BulletMegaman>().direction = lastDirection;
            nextFireAt += fireInterval;
        }
        else
        {
            layerTime -= 0.5f * Time.deltaTime;
            if (layerTime <= 0)
            {
                myAnimator.SetLayerWeight(1, 0);
                layerTime = 2;
            }
        }
    }

   void Mover()
    {
        float mov = Input.GetAxis("Horizontal");
        if (mov != 0)
        {
            myAnimator.SetBool("running", true);
            lastDirection = mov > 0;
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
            myAnimator.SetBool("grounded", true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAnimator.SetBool("grounded", false);
                myAnimator.SetTrigger("takeof");
                myAnimator.SetBool("jumping", true);
                if (myAnimator.GetBool("dash"))
                {
                    myBody.AddForce(new Vector2(0, jumpSpeed + jumpSpeed / 2), ForceMode2D.Impulse);
                }
                else
                {
                    myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                }
                jump = 1;
            }

        }
        if (myAnimator.GetBool("jumping") && !isGrounded())
        {
            if (myAnimator.GetBool("jumping") && jump == 1 && Input.GetKeyDown(KeyCode.Space))
            {
                myAnimator.SetTrigger("takeof");
                myAnimator.SetBool("jumping", true);
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                jump = 0;
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
    void Dash()
    {

        if (Input.GetKey(KeyCode.Z) && dash <= 0.3 && pressZ == true)
        {
            myAnimator.SetBool("dash", true);
            if (lastDirection)
            {
                transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            }
            else
            {
                transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
            }
            dash = dash + 0.5f * Time.deltaTime;
            if (dash >= 0.3f)
            {
                pressZ = false;
                dash = 1;
            }
        }
        else
        {
            if (dash > 0)
            {
                dash = dash - 1 * Time.deltaTime;
                myAnimator.SetBool("dash", false);
            }
            if (dash <= 0)
            {
                pressZ = true;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        pause = true;
        myAnimator.SetBool("death", true);
        myBody.isKinematic = true;
        yield return new WaitForSeconds(1);
        Instantiate(deathParticles, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
