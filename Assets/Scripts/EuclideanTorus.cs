using UnityEngine;

public class EuclideanTorus : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

        // Teleport the game object
        if (transform.position.x > 12)
        {
            transform.position = new Vector3(-12, transform.position.y, 0);
        }
        else if (transform.position.x < -12)
        {
            transform.position = new Vector3(12, transform.position.y, 0);
        }

        else if (transform.position.y > 6)
        {
            transform.position = new Vector3(transform.position.x, -6, 0);
        }

        else if (transform.position.y < -6)
        {
            transform.position = new Vector3(transform.position.x, 6, 0);
        }
    }
}
