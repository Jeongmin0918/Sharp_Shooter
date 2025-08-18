using UnityEngine;
using StarterAssets;
using Unity.Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeapon;
    [SerializeField] CinemachineCamera playerFollowCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] LayerMask muzzleOnlyMask;

    WeaponSO currentWeaponSO;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    Weapon currentWeapon; // 현재 무기

    const string SHOOT_STRING = "Shoot";

    // 발사 쿨타임 설정
    float timeSinceLastShot = 0f;
    float defaultFOV;
    float defaultRotationSpeed;
    int currentAmmo; // 현재 탄약 수
    int originalWeaponMask;

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        defaultRotationSpeed = firstPersonController.RotationSpeed;

        if (playerFollowCamera)
            defaultFOV = playerFollowCamera.Lens.FieldOfView;
        else
            Debug.LogError("ActiveWeapon: CinemachineCamera 참조가 없습니다. 인스펙터에 할당하거나 씬에 존재하는지 확인하세요.");
            
        if (weaponCamera != null)
            originalWeaponMask = weaponCamera.cullingMask;
    }

    void Start()
    {
        SwitchWeapon(startingWeapon); // 선택한 무기 장착 + 탄약
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;

        // 정해진 탄약대로 보충
        if (currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize;
        }

        ammoText.text = currentAmmo.ToString("D2"); // 항상 두 자리 숫자로 전달 ex) 03
    }

    // 무기를 바꾼경우
    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.currentWeaponSO = weaponSO;
        AdjustAmmo(currentWeaponSO.MagazineSize); // 탄약 새로 보충
    }

    void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime; // 발사 쿨타임 더하기

        if (!starterAssetsInputs.shoot) return;

        // 쿨타임이 FireRate 보다 크면 다시 발사할 수 있도록 && 탄약이 있다면
        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            currentWeapon.Shoot(currentWeaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0f;
            AdjustAmmo(-1); // 탄약 감소
        }

        if (!currentWeaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false); // 클릭 후 다시 false
        }
    }

    // 줌
    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return;

        var lens = playerFollowCamera.Lens; // struct: 복사-수정-되돌려넣기
        bool zooming = starterAssetsInputs != null && starterAssetsInputs.zoom;

        if (zooming)
        {
            lens.FieldOfView = currentWeaponSO.ZoomAmount; // 설정한 줌만큼 확대
            playerFollowCamera.Lens = lens;

            // weaponCamera 작동중이면 (줌 중 총 발사할 때 총구 없애기 작업)
            if (weaponCamera != null)
            {
                weaponCamera.enabled = true;                 // 끄지 말고
                weaponCamera.cullingMask = muzzleOnlyMask;   // 줌 중엔 '머즐만' 렌더
            }

            if (zoomVignette != null) zoomVignette.SetActive(true);
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            lens.FieldOfView = defaultFOV;
            playerFollowCamera.Lens = lens;

            if (weaponCamera != null)
            {
                weaponCamera.enabled = true;                       // 평소엔 켜두고
                weaponCamera.cullingMask = originalWeaponMask;     // 원래 마스크 복원(무기+머즐)
                weaponCamera.fieldOfView = defaultFOV;  
            }

            if (zoomVignette != null) zoomVignette.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }
}
