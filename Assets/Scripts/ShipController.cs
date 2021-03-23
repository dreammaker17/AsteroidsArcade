using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameObject bullet;
    public AudioClip shoot;

    float rotationSpeed = 180f;
    float thrustForce = 6f;

    private GameController gameController;
    private float maxSpeed = 3.5f;
    private Rigidbody2D rb;

    private void Start()
    {
        // Get a reference to the game controller object and the script
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        rb = GetComponent<Rigidbody2D>();
        bullet.SetActive(false);
    }

    private void FixedUpdate()
    {
        ControlRocket();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            Shoot();
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        // Anything except a bullet is an asteroid
        if (c.gameObject.tag != "Bullet")
        {
            // Move the ship to the centre of the screen
            transform.position = new Vector3(0, 0, 0);

            // Remove all velocity from the ship
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);

            gameController.DecrementLives();
        }
    }

    private void ControlRocket()
    {
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        rb.AddForce(transform.up * thrustForce * Input.GetAxis("Vertical"));
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
    }

    void Shoot()
    {
        GameObject bulletClone = Instantiate(bullet, new Vector2(bullet.transform.position.x, bullet.transform.position.y), transform.rotation);
        bulletClone.SetActive(true);
        bulletClone.GetComponent<BulletController>().KillOldBullet();
        bulletClone.GetComponent<Rigidbody2D>().AddForce(transform.up * 350);

        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
    }
}
