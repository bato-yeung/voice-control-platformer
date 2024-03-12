using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private Character _player;

    [SerializeField]
    private Stage _stage;

    [SerializeField]
    private SettingsPanel _settingsPanel;

    [SerializeField]
    private GameOverPanel _gameOverPanel;

    private void OnEnable()
    {
        _player.Died += Player_Died;
        _settingsPanel.Confirmed += SettingsPanel_Confirmed;
        _gameOverPanel.Clicked += GameOverPanel_Clicked;
    }

    private void OnDisable()
    {
        _player.Died -= Player_Died;
        _settingsPanel.Confirmed -= SettingsPanel_Confirmed;
        _gameOverPanel.Clicked -= GameOverPanel_Clicked;
    }

    protected virtual void Player_Died(object sender)
    {
        GameOver();
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
        _player.transform.position = _stage.StartPoint.transform.position;

        _player.Resurrect();
    }

    public void GameOver()
    {
        _gameOverPanel.gameObject.SetActive(true);
    }
}
