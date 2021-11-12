using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMegaman : MonoBehaviour
{

    [SerializeField] float range;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //desventajas detecta todo el tiempo
        /*if (Vector2.Distance(player.transform.position, transform.position) <= range)
            Debug.Log("perseguir...");
            */
        /* if(Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Player")){
             Debug.Log("perseguir...");
         }*/

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.33f);
        Gizmos.DrawSphere(transform.position, range);
    }
}
