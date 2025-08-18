using UnityEngine;
using Unity.Cinemachine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] LayerMask interactionLayers; // 상호작용대상 제한

    CinemachineImpulseSource impulseSource;

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO)
    {
        muzzleFlash.Play();
        impulseSource.GenerateImpulse();

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity); // VFX 효과 추가
            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>(); // 터렛에 적용하기 위해 InParent 사용
            enemyHealth?.TakeDamage(weaponSO.Damage); // enemyHealth가 존재한다면 TakeDamage를 사용해 damageAmount 전달

        }
    }
}
