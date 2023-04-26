using UnityEngine.SceneManagement;
using AxGrid;
using AxGrid.Base;
using UnityEngine;

public class GameEnd : MonoBehaviourExt
{
    [SerializeField] private GameObject _gameEndText;
    [SerializeField] private GameObject _restartButton;

    [OnStart]
    private void start()
    {
        _gameEndText.SetActive(false);
        _restartButton.SetActive(false);
        Settings.Model.EventManager.AddAction("OnRestartButtonClick", () => SceneManager.LoadScene(0));
        Settings.Model.EventManager.AddAction("GameLose", () => Settings.Model.EventManager.AddAction("FadeAnimatingEnded", GameLose));
        Settings.Model.EventManager.AddAction("GameWin", () => Settings.Model.EventManager.AddAction("FadeAnimatingEnded", GameWin));
    }

    private void GameLose()
    {
        Model.Set("GameEndText", "Game over, you lose!");
        _gameEndText.SetActive(true);
        _restartButton.SetActive(true);
    }

    private void GameWin()
    {
        Model.Set("GameEndText", "Game over, you win!");
        _gameEndText.SetActive(true);
        _restartButton.SetActive(true);
    }
}
