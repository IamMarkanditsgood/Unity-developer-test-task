using System.Linq;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private BasicScreen[] _screens;

    [Header("Popups")]
    [SerializeField] private HintPopup _hintsPopup;

    private void Start()
    {
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        UIEvents.OnLevelButtonPressed += ShowGameScreen;
        UIEvents.OnLevelsScreenOpenPressed += ShowLevelsScreen;
        UIEvents.OnHintPressed += OpenHintPopup;
    }

    private void Unsubscribe()
    {
        UIEvents.OnLevelButtonPressed -= ShowGameScreen;
        UIEvents.OnLevelsScreenOpenPressed -= ShowLevelsScreen;
        UIEvents.OnHintPressed -= OpenHintPopup;
    }

    public void InitUI()
    {
        ShowLevelsScreen();
    }

    public void ShowLevelsScreen()
    {
        CloseScreens();
        LevelsScreen levelsScreen = _screens.OfType<LevelsScreen>().FirstOrDefault();
        levelsScreen.Show();
    }

    public void ShowGameScreen(int levelIndex)
    {
        CloseScreens();
        GameScreen gameScreen = _screens.OfType<GameScreen>().FirstOrDefault();
        gameScreen.Show();
        gameScreen.StartLevel(levelIndex);
    }

    private void CloseScreens()
    {
        foreach(var screen in _screens)
        {
            screen.Hide();
        }
    }

    private void OpenHintPopup(LevelData currentLevelData)
    {
        _hintsPopup.Show(currentLevelData);
    }
}