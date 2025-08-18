using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab; // 발사체
    [SerializeField] Transform turretHead; // 터렛 머리
    [SerializeField] Transform playerTargetPoint; // 플레이어
    [SerializeField] Transform projectileSpawnPoint; // 총알 스폰 위치
    [SerializeField] float fireRate = 2f; // 발사 간격
    [SerializeField] int damage = 2; // 총알 데미지

    PlayerHealth player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(FireRoutine()); // 주기적으로 발사
    }

    void Update()
    {
        turretHead.LookAt(playerTargetPoint); // 플레이어 위치 업데이트
    }

    // 총알 발사 간격
    IEnumerator FireRoutine()
    {
        while (player)
        {
            yield return new WaitForSeconds(fireRate);
            Projectile newProejctile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProejctile.transform.LookAt(playerTargetPoint); // 생성된 총알의 앞부분이 플레이어를 향하게
            newProejctile.Init(damage); 
        }
    }
}
