using TMPro;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text enemiesLeftText; // 남은 적 수 UI
    [SerializeField] GameObject youWinText; // 승리 시 UI

    PlayerHealth playerHealth;

    int enemiesLeft = 0; // 남은 적 수

    const string ENEMIES_LEFT_STRING = "Enemies Left : ";

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    public void AdjustEnemiesLeft(int amount) // 남은 적 수 업데이트
    {
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();

        if (enemiesLeft <= 0) // 만약 적이 없다면 승리 UI + Game Over UI
        {
            youWinText.SetActive(true);
            playerHealth.ShowGameOver(true);
        }
    }
    public void RestartLevelButton() // 재시작 버튼
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex; // 현재 단계가 어디인지 scene을 저장
        SceneManager.LoadScene(currentScene); // 재시작
    }

    public void QuitButton() // 게임 종료 버튼
    {
        Debug.LogWarning("Does not work in the Unity Editor! You silly goose!");
        Application.Quit();
    }
}
