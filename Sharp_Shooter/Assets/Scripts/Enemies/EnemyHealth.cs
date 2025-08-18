using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject robotExplosionVFX; // 로봇 HP 0시 폭발
    [SerializeField] int startingHealth = 3; // 로봇 HP

    int currentHealth;

    GameManager gameManager;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>(); 
        gameManager.AdjustEnemiesLeft(1); // 남은 적 수
    }

    // 데미지
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            gameManager.AdjustEnemiesLeft(-1);
            SelfDestruct();
        }
    }

    // 로봇 파괴
    public void SelfDestruct()
    {
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity); // 오브젝트를 새로 복제하는 함수
        Destroy(this.gameObject);
    }
}