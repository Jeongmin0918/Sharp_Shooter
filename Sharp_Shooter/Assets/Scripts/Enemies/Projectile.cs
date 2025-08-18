using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 30f; // 총알 속도
    [SerializeField] GameObject projectileHitVFX; // 폭발 효과

    Rigidbody rb;

    int damage;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.linearVelocity = transform.forward * speed; // 방향 * 속도
    }

    public void Init(int damage)
    {
        this.damage = damage; // 데미지를 다시 할당
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamage(damage); // null 이 아닐 때

        Instantiate(projectileHitVFX, transform.position, Quaternion.identity); // 폭발 효과
        Destroy(this.gameObject); // 총알 제거
    }
}
