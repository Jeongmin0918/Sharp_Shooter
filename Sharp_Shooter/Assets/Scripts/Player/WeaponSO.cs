using UnityEngine;

// Scriptable Object를 쉽게 에디터에서 만들 수 있게 메뉴로 등록하는 속성
[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject // MonoBehaviour 이 아닌 다른 것을 상속
{
    public GameObject weaponPrefab; // 무기 프리팹
    public int Damage = 1; // 무기 공격력
    public float FireRate = .5f; // 연사력
    public GameObject HitVFXPrefab; // 총구 효과
    public bool IsAutomatic = false; // 기관총 전용 자동 발사
    public bool CanZoom = false; // 스나이퍼 줌 효과
    public float ZoomAmount = 10f; // 줌 값
    public float ZoomRotationSpeed = .3f; // 줌 시 감도 값
    public int MagazineSize = 12; // 탄창
}
