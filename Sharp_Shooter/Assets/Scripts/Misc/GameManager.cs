using TMPro;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem; 
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

    void Update()
    {
        // O키 누르면 NextLevel UI 띄우기
        if (Keyboard.current != null && Keyboard.current.oKey.wasPressedThisFrame)
        {
            playerHealth.ShowNextLevel(true);
        }
    }

    public void AdjustEnemiesLeft(int amount) // 남은 적 수 업데이트
    {
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();

        int current = SceneManager.GetActiveScene().buildIndex;
        if (enemiesLeft <= 0 && current <= 1) // 만약 적이 없다면 승리 UI + Game Over UI
        {
            youWinText.SetActive(true);
            playerHealth.ShowNextLevel(true);
        }
        else if (enemiesLeft <= 0 && current == 2) // level 3 라면
        {
            playerHealth.ShowGameClear(true);
        }
    }

    public void NextLevelButton() // 다음 레벨 버튼
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        int next = current + 1;

        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
        else
            SceneManager.LoadScene(0);
    }

    public void RestartLevelButton() // 재시작 버튼
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex; // 현재 단계가 어디인지 scene을 저장
        SceneManager.LoadScene(currentScene); // 재시작
    }

    public void Level1Button() // 레벨 1부터 시작
    {
        SceneManager.LoadScene(1); // 재시작
    }

    public void QuitButton() // 게임 종료 버튼
    {
        Debug.LogWarning("Does not work in the Unity Editor! You silly goose!");
        Application.Quit();
    }
}
