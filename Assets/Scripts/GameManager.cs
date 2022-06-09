using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public ParticleSystem explosion;
    public GameObject gameOverUI;
    
    
    public int score { get; private set; }
    public Text scoreText;

    public int lives { get; private set; }
    public Text livesText;

    private void Start()
    {  
        NewGame();
    }
    
    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.R))
        {  
            NewGame();
        }
        if(Input.GetKeyDown(KeyCode.Q))
            Application.Quit();
            
        if(Input.GetKeyDown(KeyCode.M))
            SceneManager.LoadScene(0);
        
    }

    public void NewGame()
    {   
        
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
        }

        gameOverUI.SetActive(false);

        SetScore(0);
        SetLives(3);
        Respawn();
    }

    public void EnemyDestroyed(Enemy enemy)
    {
        explosion.transform.position = enemy.transform.position;
        explosion.Play();

        if (enemy.size < 0.7f)
        {
            SetScore(score + 100); // small enemy
        }
        else if (enemy.size < 1.4f)
        {
            SetScore(score + 50); // medium
        }
        else
        {
            SetScore(score + 25); // large
        }
    }

    public void PlayerDeath(PlayerController player)
    {
        explosion.transform.position = player.transform.position;
        explosion.Play();

        SetLives(lives - 1);

        if (lives <= 0)
        {
            GameOver();
        }
        else
            {Invoke(nameof(Respawn), player.respawnTime);}
    }

    private void Respawn()
    {  
         player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }

}
