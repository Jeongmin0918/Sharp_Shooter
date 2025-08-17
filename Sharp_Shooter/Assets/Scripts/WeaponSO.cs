using UnityEngine;

// CreateAssetMenu란?
[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject // Monobehaviour 이 아닌 다른 것을 상속
{
    public int Damage = 1;
    public float FireRate = .5f;
    public GameObject HitVFXPrefab; // 총구 효과
    public bool IsAutomatic = false; // 기관총 마우스 버튼 활용
}
