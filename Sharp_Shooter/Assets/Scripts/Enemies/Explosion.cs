using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int damage = 3; // 폭발 데미지

    void Start()
    {
        Explode();
    }

    // view 에서 보이는 시각화형 도형을 그리는 함수
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();

            if (!playerHealth) continue; // hitCollider 가 player 인지 확인

            playerHealth.TakeDamage(damage);

            break;
        }
    }
}
