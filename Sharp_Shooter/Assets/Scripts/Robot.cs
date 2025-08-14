using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    // FirstPersonController 에서 지원하는 타입 주로 플레이어 조작 담당
    FirstPersonController player;
    // 자료형 변수 선언
    NavMeshAgent agent;
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
        // 프레임마다 플레이어의 현재 위치를 읽어서 경로 갱신
        agent.SetDestination(player.transform.position);
    }
}
