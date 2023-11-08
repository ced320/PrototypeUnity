using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    [SerializeField] private Vector3 spawnPos = new Vector3(0, 5, 20);
   [SerializeField]  private float startDelay = 2;
    [SerializeField] private float repeatDelay = 2;
    private PlayerController playerControllerScript;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public AudioSource levelMusic;
    public bool isMuted = false;


    private List<GameObject> addedEnemies = new List<GameObject>(); 
    // Start is called before the first frame update
    void Start()
    {
        levelMusic.Play();
        InvokeRepeating("SpawnObstacle", startDelay, repeatDelay);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update() 
    {
        destroyOutOfBoundsEnemies();
        if(playerControllerScript.gameOver)
        {
            GameOver();
        }
    }

    void GameOver() 
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        levelMusic.Stop();
        // Delete all enemies form game
        foreach(GameObject enemie in addedEnemies.Reverse<GameObject>())
        {
            addedEnemies.Remove(enemie);
            Destroy(enemie);
        }
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SpawnObstacle()
    {
        if (!playerControllerScript.gameOver) 
        { 
            spawnPos = RandomSpawnPos();
            var clone = Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
            addedEnemies.Add(clone);
            //Destroy(clone, 3.0f);
        }
    }

    private void destroyOutOfBoundsEnemies()
    {   
        //Not optimal but fine
        foreach(GameObject enemie in addedEnemies.Reverse<GameObject>())
        {
            if(enemie.transform.position.z < -10)
            {
                addedEnemies.Remove(enemie);
                Destroy(enemie);
            }
        }
    }

    private Vector3 RandomSpawnPos()
    {
        float randY = Random.Range(2,10);
        float randZ = Random.Range(17,23); 
        return new Vector3(0,randY,randZ);
    }
}
