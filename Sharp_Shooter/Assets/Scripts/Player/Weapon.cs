using UnityEngine;
using Unity.Cinemachine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash; // 총구 효과
    [SerializeField] LayerMask interactionLayers; // 상호작용대상 제한 ()

    CinemachineImpulseSource impulseSource; // 반동

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO) // 발사 시
    {
        muzzleFlash.Play(); // 총구 효과 재생
        impulseSource.GenerateImpulse(); // 반동 효과

        RaycastHit hit;
        
        // 플레이어가 총을 쏠 때
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity); // VFX 효과 추가 (사망 시)
            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>(); // 터렛에 적용하기 위해 InParent 사용
            enemyHealth?.TakeDamage(weaponSO.Damage); // enemyHealth가 존재한다면 TakeDamage를 사용해 damageAmount 전달

        }
    }
}
