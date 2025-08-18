using UnityEngine;
using System.Collections;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] GameObject robotPrefab; // 소환 할 로봇
    [SerializeField] float spawnTime = 5f; // 로봇 소환 시간
    [SerializeField] Transform spawnPoint; // 소환 위치

    PlayerHealth player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(SpawnRoutine()); // 로봇 소환 시작
    }

    IEnumerator SpawnRoutine() // 매 프레임 (Update) 마다 쓸 필요는 없어서 Coroutine (IEnumerator) 사용
    {
        // 플레이어가 존재할때만
        while (player)
        {
            Instantiate(robotPrefab, spawnPoint.position, transform.rotation); // (로봇을 스폰 위치에서 회전해서 생성)
            yield return new WaitForSeconds(spawnTime); // 대기
        }
    }
}
