using Unity.VisualScripting;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public float speed = 1f;
    private GameManager gameManager;

    private Quaternion rot;
    private Vector2 vel;
    private Rigidbody2D rb2d;
    public float timeScale = 1f;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            gameManager.LoseLives(1);
            Destroy(this.gameObject);
        }
        this.transform.rotation = rot;
        rb2d.linearVelocity = vel;
    }

    void Start()
    {
        gameManager = Camera.main.gameObject.GetComponent<GameManager>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rot = this.transform.rotation;
        vel = rb2d.linearVelocity;
        vel.x = -speed * timeScale;
        vel.y = 0;
        rb2d.linearVelocity = vel;
    }
}
