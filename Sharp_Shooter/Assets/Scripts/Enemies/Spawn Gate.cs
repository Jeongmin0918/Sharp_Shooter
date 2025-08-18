using UnityEngine;
using System.Collections;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] GameObject robotPrefab;
    [SerializeField] float spawnTime = 5f; // 로봇 소환 시간
    [SerializeField] Transform spawnPoint;

    PlayerHealth player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        // 플레이어가 존재할때만
        while (player)
        {
            Instantiate(robotPrefab, spawnPoint.position, transform.rotation); // 오브젝트를 새로 복제하는 함수
            yield return new WaitForSeconds(spawnTime); // 대기
        }
    }
}
