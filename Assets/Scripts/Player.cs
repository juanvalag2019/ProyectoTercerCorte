using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //define que la variable es privada aunque puede ser editada desde el editor de unity
    [SerializeField] int speed = 5;

    [SerializeField] GameObject bullet;

    [SerializeField] float fireInterval = 2;
    float minX, minY, maxX, maxY, tamX, tamY, nextFireAt;

    // Start is called before the first frame update
    void Start()
    {
        nextFireAt = fireInterval;
        tamX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        tamY = (GetComponent<SpriteRenderer>()).bounds.size.y;
        // busca dentro de la escena el objeto camara con la etiqueta MainCamera. Afecta el rendimiento
        Vector2 esquinaSupDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        maxX = esquinaSupDer.x - tamX / 2;
        maxY = esquinaSupDer.y - tamY / 2;

        Vector2 esquinaInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        minX = esquinaInfIzq.x + tamX / 2;
        minY = esquinaInfIzq.y + 5;


        // Debug.Log(minY);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Fire();
    }

    void Movement()
    {
        /* if (Input.GetKey(KeyCode.RightArrow))
             transform.Translate(new Vector2(0.1f, 0));

         if (Input.GetKey(KeyCode.LeftArrow))
             transform.Translate(new Vector2(-0.1f, 0));

         if (Input.GetKey(KeyCode.UpArrow))
             transform.Translate(new Vector2(0, 0.1f));

         if (Input.GetKey(KeyCode.DownArrow))
             transform.Translate(new Vector2(0, -0.1f));*/

        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(movH * Time.deltaTime * speed, movV * Time.deltaTime * speed));
        // Time.deltaTime --> tiempo de ejecución del ultimo frame (regula el número que le multipliquemos para que solo una parte del número multiplicado sea aplicado en la ejecución de un frame)

        float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        float newY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector2(newX, newY);
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireAt)
        {
            Instantiate(bullet, transform.position - new Vector3(0, tamY / 2, 0), transform.rotation);
            nextFireAt += fireInterval;
        }
    }
}
