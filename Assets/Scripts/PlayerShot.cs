using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public GameManager gameManager;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Enemy"))
        {
            var es = coll.gameObject.GetComponent<EnemyScript>();
            if (es != null)
            {
                es.Die();
            }
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
