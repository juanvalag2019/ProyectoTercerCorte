using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    public int enemyCount;
    private void Awake()
    {
        int managers = GameObject.FindObjectsOfType<GameManager>().Length;
        if (managers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        enemyCount = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount < 1)
        {
            Time.timeScale = 0;
            Debug.Log("crack");
            //GameObject.Find("Canvas").SetActive(true);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject.Find("Canvas").SetActive(false);
        CreateEnemies();
    }

    public void StartGame()
    {
        // carga de la lista de escenas del build settings puede ser el nombre de la escena o el número (buena práctica)
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    void CreateEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            float minX = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
            float maxX = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;
            float x = Random.Range(minX, maxX);
            Instantiate(enemy, new Vector2(x, -1.2f), Quaternion.identity);

        }
    }
}
