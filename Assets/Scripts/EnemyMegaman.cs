using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;


public class EnemyMegaman : MonoBehaviour
{

    [SerializeField] float range;
    [SerializeField] GameObject player;
    [SerializeField] int lives = 3;
    Animator myAnimator;
    private GameObject healthBar;

    float toDecrease;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.Find("HealthBar").gameObject;
        myAnimator = gameObject.GetComponent<Animator>();
        toDecrease = 1f / (float)lives;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(player.transform.position, transform.position) <= range)
                gameObject.GetComponent<AIPath>().canMove = true;
            else{
                gameObject.GetComponent<AIPath>().canMove = false;
            }
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
                gameObject.isStatic = true;
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
        barT.localScale = new Vector2(x - toDecrease, 1);
    }
}