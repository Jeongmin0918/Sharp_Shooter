using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f; // 회전 속도 (감도)
    const string PLAYER_STRING = "Player";

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f); // 아이템 회전 시키기
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING)) // other trigger에 Player가 충돌했을 때
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>(); // 무기 상태 
            // GetComponentInChildren<ActiveWeapon>() => 자식들 중 ActiveWeapon을 찾아라
            OnPickup(activeWeapon); // 아이템을 먹었을 때 자식에서 구현하는 역할을 나눔
            Destroy(this.gameObject); // 제거
        }
    }

    // pickup 을 상속받았을 때 반드시 구현
    protected abstract void OnPickup(ActiveWeapon activeWeapon);
}
