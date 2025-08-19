using UnityEngine;
using StarterAssets;
using Unity.Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeapon; // 시작 무기
    [SerializeField] CinemachineCamera playerFollowCamera; // 플레이어 카메라
    [SerializeField] Camera weaponCamera; // 무기 전용 카메라
    [SerializeField] GameObject zoomVignette; // 줌 시 효과
    [SerializeField] TMP_Text ammoText; // 탄약 이미지
    [SerializeField] LayerMask muzzleOnlyMask; // 줌 시 총구만 보이게 하기

    WeaponSO currentWeaponSO; // 현재 무기 데이터 (탄약 보충을 위해)
    Weapon currentWeapon; // 현재 무기 오브젝트 (무기마다 효과가 다르기 때문에)
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;

    const string SHOOT_STRING = "Shoot";

    float timeSinceLastShot = 0f;    // 발사 쿨타임
    float defaultFOV; // 기본 시야각
    float defaultRotationSpeed; // 기본 회전 감도
    int currentAmmo; // 현재 탄약 수
    int originalWeaponMask; // 무기 카메라 기본 마스크

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
        AdjustAmmo(currentWeaponSO.MagazineSize); // 탄창 할당
    }

    void Update()
    {
        HandleShoot(); // 발사 입력 처리
        HandleZoom(); // 줌 처리
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
            Destroy(currentWeapon.gameObject); // 현재 무기 파괴
        }

        // 새 무기로 교체
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
