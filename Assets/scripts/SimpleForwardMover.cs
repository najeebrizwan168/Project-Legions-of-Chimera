using UnityEngine;

public class SimpleForwardMover : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
