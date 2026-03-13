using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;

    [Header("Gfx")]
    public float parallaxEffect;
    void Start()
    {
        //length = GetComponent<SpriteRenderer>().bounds.size.x;
        length = 10.24f;
    }

    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * parallaxEffect;
        if (transform.position.x < -length)
        {
            transform.position = new Vector3(length, transform.position.y, transform.position.z);
        }
    }
}
