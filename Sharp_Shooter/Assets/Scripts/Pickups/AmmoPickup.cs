using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] int ammoAmount = 100; // 줍는 탄약 양
    
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.AdjustAmmo(ammoAmount); // 탄약 수가 넘더라도 수량 제한
    }
}
