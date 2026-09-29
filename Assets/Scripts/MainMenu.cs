using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button classicRunButton;
    [SerializeField] private Button powerRunButton;
    [SerializeField] private string gameplaySceneName = "Game";

    private void OnEnable()
    {
        if (classicRunButton != null)
            classicRunButton.onClick.AddListener(StartClassicRun);

        if (powerRunButton != null)
            powerRunButton.onClick.AddListener(StartPowerRun);
    }

    private void OnDisable()
    {
        if (classicRunButton != null)
            classicRunButton.onClick.RemoveListener(StartClassicRun);

        if (powerRunButton != null)
            powerRunButton.onClick.RemoveListener(StartPowerRun);
    }

    public void StartClassicRun()
    {
        StartRun(GameMode.ClassicRun);
    }

    public void StartPowerRun()
    {
        StartRun(GameMode.PowerRun);
    }

    private void StartRun(GameMode gameMode)
    {
        GameModeSettings.CurrentMode = gameMode;
        SceneManager.LoadScene(gameplaySceneName);
    }
}
