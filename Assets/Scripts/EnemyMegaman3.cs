using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMegaman3 : MonoBehaviour
{

    [SerializeField] float range;
    [SerializeField] GameObject player;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet2;
    [SerializeField] float fireInterval = 2;
    float nextFireAt, tamX, tamY;
    [SerializeField] int lives = 3;
    float toDecrease;
    Animator myAnimator;
    bool canFire = true;
    private GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {
        tamX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        tamY = (GetComponent<SpriteRenderer>()).bounds.size.y;
        myAnimator = gameObject.GetComponent<Animator>();
        healthBar = transform.Find("HealthBar").gameObject;
        toDecrease = 1f / (float)lives;
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
            myAnimator.SetBool("fire", true);
            Vector3 spawnPos = transform.position + new Vector3(-(tamX / 2)+0.9f, 0.2f, 0);
            GameObject bullet = Instantiate(this.bullet, spawnPos, transform.rotation);
            bullet.GetComponent<BulletEnemy3>().direction = false;
            GameObject bullet2 = Instantiate(this.bullet2, spawnPos + new Vector3(1.8f, 0, 0), transform.rotation);
            bullet2.GetComponent<BulletEnemy3>().direction = true;
            nextFireAt += fireInterval;
            
        }
        else{
            myAnimator.SetBool("fire", false);
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
            if (lives >= 1)
            {
                DecreaseHealthBar();
                lives--;
            }
            if (lives == 0)
            {
                healthBar.SetActive(false);
                canFire = false;
                myAnimator.SetBool("death", true);
            }
        }
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void DecreaseHealthBar()
    {
        Transform barT = healthBar.transform.Find("Bar");
        float x = barT.localScale.x;
        barT.localScale = new Vector3(x - toDecrease, 1);
        Debug.Log(toDecrease);
    }
}
