using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] Text counter;

    // Update is called once per frame
    void Update()
    {
        counter.text = GameObject.FindGameObjectsWithTag("Enemy").Length.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);

    }
}
