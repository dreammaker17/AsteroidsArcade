using UnityEngine;

public class AsteroidContoller : MonoBehaviour
{
    private GameController gameController;
    private float maxRotation;
    private float rotationX;
    private float rotationY;
    private Rigidbody2D rb;

    public GameObject smallAsteroid;
    public AudioClip destroy;

    void Start()
    {
        // Get a reference to the game controller object and the script
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        maxRotation = 25f;
        rotationX = Random.Range(-maxRotation, maxRotation);
        rotationY = Random.Range(-maxRotation, maxRotation);
        rb = smallAsteroid.GetComponent<Rigidbody2D>();

        //// Push the asteroid in the direction it is facing
        rb.AddForce(transform.up * Random.Range(-50.0f, 150.0f));

        // Give a random angular velocity/rotation
        rb.angularVelocity = Random.Range(-0.0f, 90.0f);
    }

    void Update()
    {
        smallAsteroid.transform.Rotate(new Vector3(rotationX, rotationY, 0) * Time.deltaTime);
        float dynamicMaxSpeed = 3f;
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -dynamicMaxSpeed, dynamicMaxSpeed), Mathf.Clamp(rb.velocity.y, -dynamicMaxSpeed, dynamicMaxSpeed));
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag.Equals("Bullet") || c.gameObject.tag.Equals("Enemy Ship"))
        {
            // Destroy the bullet
            Destroy(c.gameObject);

            // If large asteroid spawn new ones
            if (tag.Equals("Large Asteroid"))
            {
                // Spawn small asteroids
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x - .5f,
                        transform.position.y - .5f, 0),
                        Quaternion.Euler(0, 0, 90));

                // Spawn small asteroids
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x + .5f,
                        transform.position.y + .0f, 0),
                        Quaternion.Euler(0, 0, 0));

                // Spawn small asteroids
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x + .5f,
                        transform.position.y - .5f, 0),
                        Quaternion.Euler(0, 0, 270));

                gameController.SplitAsteroid(); // +2
            }
            else
            {
                // Just a small asteroid destroyed
                gameController.DecrementAsteroids();
            }

            // Play a sound
            AudioSource.PlayClipAtPoint(destroy, Camera.main.transform.position);

            // Add to the score
            gameController.IncrementScore();

            // Destroy the current asteroid
            Destroy(gameObject);
        }
    }
}
