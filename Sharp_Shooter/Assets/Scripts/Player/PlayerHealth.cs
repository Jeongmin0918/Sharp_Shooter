using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.UI;
using StarterAssets;

public class PlayerHealth : MonoBehaviour
{
    [Range(1, 10)] // 체력범위 1 ~ 10
    [SerializeField] int startingHealth = 5; // 플레이어 시작 HP
    [SerializeField] CinemachineCamera deathVirtualCamera; // 플레이어가 사망 시 카메라
    [SerializeField] Transform weaponCamera; // 무기 카메라 (벽을 뚫는 경우가 있어서)
    [SerializeField] Image[] shieldBars; // 체력바 이미지
    [SerializeField] GameObject gameoverContainer; //Game Over 화면
    [SerializeField] GameObject nextlevelContainer; //Next Level 화면
    [SerializeField] GameObject gameclearContainer; //Game Clear 화면

    int currentHealth; // 현재 체력
    int gameOverVirtualCameraPriority = 20; // 카메라 우선순위를 활용 해 사망 시 카메라로

    void Awake()
    {
        currentHealth = startingHealth; // 체력 초기화
        AdjustShieldUI(); // 초기 체력바
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        AdjustShieldUI();

        if (currentHealth <= 0)
        {
            PlayerGameOver();
        }
    }

    void PlayerGameOver()
    {
        weaponCamera.parent = null;
        deathVirtualCamera.Priority = gameOverVirtualCameraPriority; // 죽었을경우 카메라 우선순위 변경
        ShowGameOver(true); // GameOver 화면 띄우기
        Destroy(this.gameObject); // 자폭
    }


    public void ShowGameOver(bool show)
    {
        gameoverContainer.SetActive(show); // Game Over Container 띄우기
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(false); // 커서 숨기기 false
    }

    public void ShowNextLevel(bool show)
    {
        nextlevelContainer.SetActive(show); // Next Level Container 띄우기
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(false); // 커서 숨기기 false
    }

    public void ShowGameClear(bool show)
    {
        gameclearContainer.SetActive(show); // Game Clear Container 띄우기
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(false); // 커서 숨기기 false
    }

    // ShieldBar 이미지 조정
    void AdjustShieldUI()
    {
        for (int i = 0; i < shieldBars.Length; i++)
        {
            if (i < currentHealth)
            {
                shieldBars[i].gameObject.SetActive(true);
            }
            else
            {
                shieldBars[i].gameObject.SetActive(false);
            }
        }
    }
}
