using UnityEngine;

public class BoarderCheck : MonoBehaviour
{
    //Y: 18.8 , -16.8
    //X: 31.2 ,-31.8

    private Vector2 yRange = new Vector2(18.8f, -16.8f);
    private Vector2 xRange = new Vector2(31.2f, -31.8f);

    private void FixedUpdate()
    {
        if (transform.position.y < yRange.y)
            transform.position = new Vector2(transform.position.x, 18.7f);

        if (transform.position.y > yRange.x)
            transform.position = new Vector2(transform.position.x, -16.7f);

        if (transform.position.x < xRange.y)
            transform.position = new Vector2(31.1f, transform.position.y);

        if (transform.position.x > xRange.x)
            transform.position = new Vector2(-31.7f, transform.position.y);

    }
}
