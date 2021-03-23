using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    private float maxSpeed = 2f;

    Transform player;

    public float rotSpeed = 90f;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (player == null)
        {
            // Find the player's ship!
            GameObject go = GameObject.FindGameObjectWithTag("Ship");

            if (go != null)
            {
                player = go.transform;
            }
        }

        if (player == null)
            return; // Try again next frame!

        RotationToPlayer();

        MoveToPlayer();
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        // Destroy the object
        Destroy(c.gameObject);

        // Destroy the current ship
        Destroy(gameObject);
    }

    void RotationToPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
    }

    void MoveToPlayer()
    {
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);

        pos += transform.rotation * velocity;

        transform.position = pos;
    }
}
