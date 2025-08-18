using TMPro;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text enemiesLeftText;
    [SerializeField] GameObject youWinText;

    PlayerHealth playerHealth;

    int enemiesLeft = 0;

    const string ENEMIES_LEFT_STRING = "Enemies Left : ";

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();

        if (enemiesLeft <= 0)
        {
            youWinText.SetActive(true);
            playerHealth.ShowGameOver(true);
        }
    }
    public void RestartLevelButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitButton()
    {
        Debug.LogWarning("Does not work in the Unity Editor! You silly goose!");
        Application.Quit();
    }
}
