using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMegaman2 : MonoBehaviour
{

    [SerializeField] float range;
    [SerializeField] GameObject player;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireInterval = 2;
    float nextFireAt, tamX, tamY;
    [SerializeField] int lives = 3;
    Animator myAnimator;
    bool canFire = true;
    // Start is called before the first frame update
    void Start()
    {
        tamX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        tamY = (GetComponent<SpriteRenderer>()).bounds.size.y;
        myAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //desventajas detecta todo el tiempo
        if (player != null)
        {
            if (Vector2.Distance(player.transform.position, transform.position) <= range)
                Fire();
        }

        /* if(Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Player")){
             Debug.Log("perseguir...");
         }*/

    }

    void Fire()
    {
        if (Time.time >= nextFireAt && canFire)
        {
            Vector3 spawnPos = transform.position + new Vector3(-(tamX / 2) - 0.2f, +0.08f, 0);
            GameObject bullet = Instantiate(this.bullet, spawnPos, transform.rotation);
            nextFireAt += fireInterval;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.33f);
        Gizmos.DrawSphere(transform.position, range);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            // GameObject.FindObjectOfType<GameManager>();
            lives--;
            if (lives < 1)
            {
                canFire = false;
                myAnimator.SetBool("death", true);
            }
        }
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
