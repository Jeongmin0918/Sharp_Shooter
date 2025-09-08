using UnityEngine;

public class HealPickup : Pickup
{
    [SerializeField] int healAmount = 10; // 힐 양

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        PlayerHealth playerHealth = activeWeapon.GetComponentInParent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount); // 체력 회복
        }
    }
}
