using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f; // 폭발 범위
    [SerializeField] int damage = 3; // 폭발 데미지

    void Start()
    {
        Explode(); // 시작 시 폭발 가능하도록 실행
    }

    // view 에서 보이는 시각화형 도형을 그리는 함수
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius); // 폭발 범위 표시
    }

    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius); // 어떤 Collider 가 폭발 범위 안에 있는지 확인

        foreach (Collider hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();

            if (!playerHealth) continue; // hitCollider 가 player 인지 확인

            playerHealth.TakeDamage(damage); // player 라면 데미지 부여

            break;
        }
    }
}
