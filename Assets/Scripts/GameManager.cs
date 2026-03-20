using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int points = 0;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI pointText;
    public TextMeshProUGUI slowText;
    public float enemySpawnTimer = 4.5f;
    private float mainTimer = 0f;
    private float slowTimer = 30f;
    public float slowCooldown = 30f;
    private float resumeTimer = 0f;
    public float resumeCooldown = 10f;
    public GameObject circleEnemy;
    public GameObject squareEnemy;
    public Vector2 enemySpawnGrid;
    public Vector2 enemySpawnGridLowerEnd;

    public GameObject victory;
    public GameObject defeat;


    private float timeScale = 1f;

    void Start()
    {
        UpdateLifeText();
        UpdatePointText();
    }

    void Update()
    {
        UpdateSlowText();
        mainTimer += Time.deltaTime;
        slowTimer += Time.deltaTime;
        resumeTimer += Time.deltaTime;
        if (mainTimer >= enemySpawnTimer) SpawnEnemy();
        if (Input.GetKey(KeyCode.LeftShift) && slowTimer >= slowCooldown) SlowDownTime();
        if (resumeTimer >= resumeCooldown) ResumeTime();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
        GameObject[] enemyShots = GameObject.FindGameObjectsWithTag ("EnemyShot");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyScript>().timeScale = timeScale;
        }
        for (int i = 0; i < enemyShots.Length; i++)
        {
            enemyShots[i].GetComponent<EnemyShot>().timeScale = timeScale;
        }
    }

    void SlowDownTime()
    {
        slowTimer = 0f;
        resumeTimer = 0f;
        timeScale = 0.5f;
    }

    void ResumeTime()
    {
        resumeTimer = 0f;
        timeScale = 1f;   
    }

    void UpdateSlowText()
    {
        if (slowTimer >= slowCooldown)
        {
            slowText.text = "Shift";
        } else
        {
            slowText.text = $"{30 - (int)slowTimer}";
        }
    }

    void VictoryImg()
    {
        Instantiate(victory, new Vector3(0,0, -1), this.transform.rotation);
        Destroy(GameObject.FindGameObjectsWithTag ("Player")[0]);
        Destroy(lifeText);
        Destroy(pointText);
        Destroy(slowText);
    }

    
    void DefeatImg()
    {
        Instantiate(defeat, new Vector3(0,0, -1), this.transform.rotation);
        Destroy(GameObject.FindGameObjectsWithTag ("Player")[0]);
        Destroy(lifeText);
        Destroy(pointText);
        Destroy(slowText);
    }

    public void AddPoints(int p)
    {
        points += p;
        if(points % 1000 == 0) AddLives(1);
        UpdatePointText();
        if(points >= 2000) VictoryImg();
    }

    public void AddLives(int l)
    {
        lives += l;
        UpdateLifeText();
    }

    public void LoseLives(int l)
    {
        lives -= l;
        UpdateLifeText();
        if(lives <= 0) DefeatImg();
    }

    void UpdateLifeText()
    {
        lifeText.text = $"Vidas: {lives}";
    }

    void UpdatePointText()
    {
        pointText.text = $"Pontos: {points}";
    }
    

    void SpawnEnemy()
    {
        mainTimer = 0f;
        enemySpawnTimer -= 0.05f;
        int id = UnityEngine.Random.Range(0,2);
        Vector3 randSpawnLocation = new Vector3(UnityEngine.Random.Range(enemySpawnGridLowerEnd.x,enemySpawnGrid.x),UnityEngine.Random.Range(enemySpawnGridLowerEnd.y,enemySpawnGrid.y),0);
        switch (id)
        {
            case 0: // c
                Instantiate(circleEnemy, randSpawnLocation, this.transform.rotation);
                break;
            case 1: // s
                Instantiate(squareEnemy, randSpawnLocation, this.transform.rotation);
                break;
            default:
                break;
        }
    }
}