using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    public int currentHealth;

    public HealthBar healthBar;

    [SerializeField] int playerScore = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    public Color damageColor;
    public Image damageImage;
    float colorSmoothing = 6f;
    public bool isTakingDamage = false;

    private void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if(numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        //Damage effect
        if(isTakingDamage)
        {
            damageImage.color = damageColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, colorSmoothing * Time.deltaTime);
        }
        isTakingDamage = false;
    }

    private void Start()
    {
        currentHealth = playerLives;
        healthBar.SetMaxHealth(playerLives);

        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
        
        isTakingDamage = true;

    }

    public void ProcessPlayerDeath()
    {

        if(playerLives > 1)
        {
            isTakingDamage = true;
            TakeLife(); //re spawn the player in the current level
        }
        else
        {
            isTakingDamage = true;
            ResetGameSession(); //send back to the start scene
        }
    }

    private void TakeLife()
    {
        playerLives--;
        healthBar.SetHealth(playerLives);

        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);

        livesText.text = playerLives.ToString();
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(3); //send back to the start scene
        Destroy(gameObject);
    }

    public void addScore(int addScoreValue)
    {
        isTakingDamage = true;
        playerScore += addScoreValue;
        scoreText.text = playerScore.ToString();
    }
}