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
        gameManager.AdjustEnemiesLeft(1); // 시작 적 1
    }

    // 데미지
    public void TakeDamage(int amount)
    {
        currentHealth -= amount; // 총에 따라 데미지가 다름

        if (currentHealth <= 0) // 체력이 0 아래면
        {
            SelfDestruct();
        }
    }

    // 로봇과 충돌 시 폭발
    public void SelfDestruct()
    {
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity); // 프리팹이나 오브젝트를 새로 복제하는 함수
        Destroy(this.gameObject);
        gameManager.AdjustEnemiesLeft(-1); // 남은 적 수 -1
    }
}