using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] Text counter;
    [SerializeField] WonGame wonGame;

    // Update is called once per frame
    void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        counter.text = enemyCount.ToString();
        if (enemyCount == 0)
        {
            Hide();
            wonGame.SetUp();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);

    }
}
