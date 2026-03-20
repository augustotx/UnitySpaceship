using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector2 bounds;
    public float speed = 8f;
    private float mainTimer = 1f;
    public float shootCooldown = 1f;
    public GameObject playerMissile;
    private Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mainTimer += Time.deltaTime;
        var vel = rb2d.linearVelocity; // Acessa a velocidade da raquete
        if (Input.GetKey(KeyCode.D))
        { // Velocidade da Raquete para ir para direita
            vel.x = speed;
        }
        else if (Input.GetKey(KeyCode.A))
        { // Velocidade da Raquete para ir para esquerda
            vel.x = -speed;
        }
        else
        {
            vel.x = 0; // Velociade para manter a raquete parada
        }
        if (Input.GetKey(KeyCode.W))
        { // Velocidade da Raquete para ir para direita
            vel.y = speed;
        }
        else if (Input.GetKey(KeyCode.S))
        { // Velocidade da Raquete para ir para esquerda
            vel.y = -speed;
        } else
        {
            vel.y = 0;
        }
        rb2d.linearVelocity = vel; // Atualizada a velocidade da raquete

        var pos = transform.position; // Acessa a Posição da raquete
        if (pos.x > bounds.x)
        {
            pos.x = bounds.x; // Corrige a posicao da raquete caso ele ultrapasse o limite superior
        }
        else if (pos.x < -bounds.x)
        {
            pos.x = -bounds.x; // Corrige a posicao da raquete caso ele ultrapasse o limite inferior
        }
        if (pos.y > bounds.y)
        {
            pos.y = bounds.y;
        }
        else if(pos.y < -bounds.y )
        {
            pos.y = -bounds.y;
        }
        transform.position = pos; // Atualiza a posição da raquete
        if (mainTimer >= shootCooldown && Input.GetKey(KeyCode.Space))
        {
            mainTimer = 0f;
            Instantiate(playerMissile, this.transform.position, this.transform.rotation);
        }
    }
}
