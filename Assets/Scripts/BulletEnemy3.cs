using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy3 : MonoBehaviour
{
    Animator myAnimator;
    public bool direction;
    private float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!myAnimator.GetBool("explode"))
        {
            if (direction)
                transform.Translate(new Vector2(speed * Time.deltaTime, speed * Time.deltaTime));
            else
                transform.Translate(new Vector2(-speed * Time.deltaTime, speed * Time.deltaTime));
        }

    }

    void setDirection(bool dir)
    {
        direction = dir;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        myAnimator.SetBool("explode", true);
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
