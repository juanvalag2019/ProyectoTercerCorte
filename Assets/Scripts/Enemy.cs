using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool direction;
    private float maxX, minX;

    // Start is called before the first frame update
    void Start()
    {
        float tamX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        float tamY = (GetComponent<SpriteRenderer>()).bounds.size.y;
        Vector2 esquinaInfDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
        maxX = esquinaInfDer.x;
        Vector2 esquinalnflzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        minX = esquinalnflzq.x;
        // Update is called once per frame

    }
    void Update()
    {
        if (direction)
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        else
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));

        if (transform.position.x >= maxX)
        {
            direction = false;
        }
        else if (transform.position.x <= minX)
        {
            direction = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            GameObject.FindObjectOfType<GameManager>().enemyCount--;
            Destroy(this.gameObject);
        }
    }
}
