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
    private GameOverPanel _gameOverPanel;

    private void OnEnable()
    {
        _player.Died += Player_Died;
        _gameOverPanel.Clicked += GameOverPanel_Clicked;
    }

    private void OnDisable()
    {
        _player.Died -= Player_Died;
    }

    protected virtual void Player_Died(object sender)
    {
        GameOver();
    }

    protected virtual void GameOverPanel_Clicked(object sender)
    {
        _gameOverPanel.gameObject.SetActive(true);

        GameStart();
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
