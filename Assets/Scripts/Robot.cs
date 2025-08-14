using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    FirstPersonController player;
    NavMeshAgent agent;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        // 장면에 player 는 하나이므로 wake 대신 start에서 가져와도 된다.
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);
    }
}
