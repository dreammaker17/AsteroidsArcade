using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject asteroid;

    private int score;
    private int hiscore;
    private int asteroidsRemaining;
    private int lives;
    private int wave;
    private int increaseEachWave = 3;
    private float spawnDistance = 8f;

    public Text scoreText;
    public Text livesText;
    public Text waveText;
    public Text hiscoreText;

    private void Start()
    {
        Cursor.visible = false;
        hiscore = PlayerPrefs.GetInt("hiscore", 0);
        asteroid.SetActive(false);
        BeginGame();
    }

    private void Update()
    {
        // Quit if player presses escape
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void BeginGame()
    {
        score = 0;
        lives = 3;
        wave = 1;

        // Prepare the HUD
        scoreText.text = "SCORE: " + score;
        hiscoreText.text = "HISCORE: " + hiscore;
        livesText.text = "LIVES: " + lives;
        waveText.text = "WAVE: " + wave;

        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        DestroyExistingAsteroids();

        asteroidsRemaining = (wave * increaseEachWave);

        for (int i = 0; i < asteroidsRemaining; i++)
        {
            Vector3 offset = Random.onUnitSphere;

            offset.z = 0;

            offset = offset.normalized * spawnDistance;
            // Spawn an asteroid
            GameObject asteroidsClone = Instantiate(asteroid, transform.position + offset, Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));
            //GameObject asteroidsClone = Instantiate(asteroid, new Vector3(Random.Range(-12.0f, 12.0f), Random.Range(-9.0f, 9.0f), 0), Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));
            asteroidsClone.SetActive(true);
            asteroidsClone.GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(-50.0f, 150.0f));
            asteroidsClone.GetComponent<Rigidbody2D>().AddForce(transform.right * Random.Range(-50.0f, 150.0f));
            asteroidsClone.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-0.0f, 90.0f);
        }

        waveText.text = "WAVE: " + wave;
    }

    public void IncrementScore()
    {
        score++;

        scoreText.text = "SCORE: " + score;

        if (score > hiscore)
        {
            hiscore = score;
            hiscoreText.text = "HISCORE: " + hiscore;

            // Save the new hiscore
            PlayerPrefs.SetInt("hiscore", hiscore);
        }

        // Has player destroyed all asteroids?
        if (asteroidsRemaining < 1)
        {
            // Start next wave
            wave++;
            SpawnAsteroids();
        }
    }

    public void DecrementLives()
    {
        lives--;
        livesText.text = "LIVES: " + lives;

        DestroyExistingEnemyShips();

        // Has player run out of lives?
        if (lives < 1)
        {
            // Restart the game
            BeginGame();
        }
    }

    public void DecrementAsteroids()
    {
        asteroidsRemaining--;
    }

    public void SplitAsteroid()
    {
        // Two extra asteroids
        // - big one
        // + 3 little ones
        // = 2
        asteroidsRemaining += 2;
    }

    void DestroyExistingAsteroids()
    {
        DestroyGameObject("Large Asteroid");
        DestroyGameObject("Small Asteroid");
    }

    void DestroyExistingEnemyShips()
    {
        DestroyGameObject("Enemy Ship");
    }

    void DestroyGameObject(string tag)
    {
        GameObject[] asteroidsArray = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject current in asteroidsArray)
        {
            GameObject.Destroy(current);
        }
    }
}
