using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.UI;
using StarterAssets;

public class BossHealth : MonoBehaviour
{
    [SerializeField] Image[] hpIcons; // 하트 아이콘 배열
    [SerializeField] int maxHealth = 100;
    [SerializeField] GameObject BossAttackVFX; 
    [SerializeField] GameObject robotExplosionVFX;
    int currentHealth;
    GameManager gameManager;
    void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.AdjustEnemiesLeft(1); // 시작 적 1
        UpdateHPUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // 0 밑으로 안내려가게
        UpdateHPUI();

        if (currentHealth <= 0) // 체력이 0 아래면
        {
            SelfDestruct();
        }
    }

    void UpdateHPUI()
    {
        // 하트 몇 개를 켤지 계산 (10 HP = 하트 1개)
        float ratio = (float)currentHealth / maxHealth;

        // 표시할 하트 개수 = 9칸 기준
        int heartsToShow = Mathf.CeilToInt(ratio * 9);

        for (int i = 0; i < hpIcons.Length; i++)
        {
            hpIcons[i].gameObject.SetActive(i < heartsToShow);
        }
    }

    public void SelfDestruct()
    {
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity); // 프리팹이나 오브젝트를 새로 복제하는 함수
        Destroy(this.gameObject);
        gameManager.AdjustEnemiesLeft(-1); // 남은 적 수 -1
    }
    
    public void BossAttack()
    {
        Instantiate(BossAttackVFX, transform.position, Quaternion.identity); // 프리팹이나 오브젝트를 새로 복제하는 함수
    }
}