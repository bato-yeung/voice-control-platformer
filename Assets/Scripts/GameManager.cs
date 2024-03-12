using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private Character _player;

    [SerializeField]
    private GameObject _startPoint;

    [SerializeField]
    private Goal _goal;

    [SerializeField]
    private SettingsPanel _settingsPanel;

    [SerializeField]
    private GameOverPanel _gameOverPanel;

    [SerializeField]
    private ResultPanel _resultPanel;

    private void OnEnable()
    {
        _player.Died += Player_Died;
        _goal.PlayerEntered += Goal_PlayerEntered;

        _settingsPanel.Confirmed += SettingsPanel_Confirmed;
        _gameOverPanel.Clicked += GameOverPanel_Clicked;
    }

    private void OnDisable()
    {
        _player.Died -= Player_Died;
        _goal.PlayerEntered -= Goal_PlayerEntered;

        _settingsPanel.Confirmed -= SettingsPanel_Confirmed;
        _gameOverPanel.Clicked -= GameOverPanel_Clicked;
    }

    protected virtual void Player_Died(object sender)
    {
        GameOver();
    }

    protected virtual void Goal_PlayerEntered(object sender, Character character)
    {
        Time.timeScale = 0f;

        _resultPanel.gameObject.SetActive(true);
    }

    protected virtual void SettingsPanel_Confirmed(object sender)
    {
        _settingsPanel.gameObject.SetActive(false);

        GameStart();
    }

    protected virtual void GameOverPanel_Clicked(object sender)
    {
        _gameOverPanel.gameObject.SetActive(false);

        GameStart();
    }

    protected override void Awake()
    {
        base.Awake();

        _player.Die();

        _settingsPanel.gameObject.SetActive(true);
    }

    public void GameStart()
    {
        _player.transform.position = _startPoint.transform.position;

        _player.Resurrect();
    }

    public void GameOver()
    {
        _gameOverPanel.gameObject.SetActive(true);
    }
}
