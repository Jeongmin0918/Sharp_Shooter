using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    FirstPersonController player; // FirstPersonController 에서 지원하는 타입 주로 플레이어 조작 담당
    NavMeshAgent agent; // 자료형 변수 선언

    const string PLAYER_STRING = "Player";
    void Awake()
    {
        // NavMeshAgent 찾아 변수에 넣기
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        // 장면에 player 는 하나이므로 wake 에서 일찍 가져오는 대신 start에서 가져와도 된다.
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update()
    {
        if (!player) return; // 플레이어가 사라졌다면
        // 프레임마다 플레이어의 현재 위치를 읽어서 경로 갱신
        agent.SetDestination(player.transform.position);
    }

    // Player의 Trigger 범위에 들어가면 폭발
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            enemyHealth.SelfDestruct();
        }
    }
}
