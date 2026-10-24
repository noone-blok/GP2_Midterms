using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{

    [SerializeField] private Button retryButton;

    [SerializeField] private Button exitButton;

    void Start()
    {
        retryButton.onClick.AddListener(OnRetry);
        exitButton.onClick.AddListener(OnExit);
    }

    void OnRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnExit()
    {
        Application.Quit();
    }
}
