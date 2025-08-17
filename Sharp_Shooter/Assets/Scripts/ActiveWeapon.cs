using UnityEngine;
using StarterAssets;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon; // 현재 무기

    const string SHOOT_STRING = "Shoot";

    // 발사 쿨타임 설정
    float timeSinceLastShot = 0f;

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime; // 발사 쿨타임 더하기
        HandleShoot();
    }

    // 무기를 바꾼경우
    public void SwitchWeapon(WeaponSO weaponSO)
    {
        Debug.Log("Player picked up " + weaponSO.name);
    }

    void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;

        // 쿨타임이 FireRate 보다 크면 다시 발사할 수 잇도록
        if (timeSinceLastShot >= weaponSO.FireRate)
        {
            currentWeapon.Shoot(weaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0f;
        }

        if (!weaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false); // 클릭 후 다시 false
        }
    }
}
