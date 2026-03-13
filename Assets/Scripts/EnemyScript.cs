using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Game Logic")]
    public int enemyType = -1;
    public float speed = 1f;

    [Header("Circle Enemy")]
    public float circleRadius = 5f;
    [SerializeField]
    private Vector3 circleCenter;
    private float angle = 0f;

    void Start()
    {
        circleCenter = transform.position;
    }

    void Update()
    {
        switch (enemyType)
        {
            case 0: // circle
                MoveCircle();
                return;
            case 1: // square
                MoveSquare();
                return;
            default:
                return;
        }
    }

    void MoveCircle()
    {
        // angle in radians; speed controls angular velocity
        angle += speed * Time.deltaTime;

        float x = circleCenter.x + Mathf.Cos(angle) * circleRadius;
        float y = circleCenter.y + Mathf.Sin(angle) * circleRadius;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    void MoveSquare()
    {
        // reuse 'angle' variable: convert angular phase to a 0..4 phase around the square
        // (angle / (2π)) * 4  => multiply by 2/π
        angle += speed * Time.deltaTime;
        float phase = Mathf.Repeat(angle * (2f / Mathf.PI), 4f); // 0..4

        float half = circleRadius; // reuse circleRadius as half side-length
        Vector3 p0 = new Vector3(circleCenter.x + half, circleCenter.y + half, transform.position.z); // top-right
        Vector3 p1 = new Vector3(circleCenter.x - half, circleCenter.y + half, transform.position.z); // top-left
        Vector3 p2 = new Vector3(circleCenter.x - half, circleCenter.y - half, transform.position.z); // bottom-left
        Vector3 p3 = new Vector3(circleCenter.x + half, circleCenter.y - half, transform.position.z); // bottom-right

        int side = Mathf.FloorToInt(phase); // 0,1,2,3
        float t = phase - side; // progress along current side 0..1

        Vector3 pos;
        switch (side)
        {
            case 0: pos = Vector3.Lerp(p0, p1, t); break; // top edge, right -> left
            case 1: pos = Vector3.Lerp(p1, p2, t); break; // left edge, top -> bottom
            case 2: pos = Vector3.Lerp(p2, p3, t); break; // bottom edge, left -> right
            default: pos = Vector3.Lerp(p3, p0, t); break; // right edge, bottom -> top
        }

        transform.position = pos;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}