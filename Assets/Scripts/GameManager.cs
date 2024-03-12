using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Character _player;

    [SerializeField]
    private Stage _stage;

    private void OnEnable()
    {
        _player.Died += Player_Died;
    }

    private void OnDisable()
    {
        _player.Died -= Player_Died;
    }

    protected virtual void Player_Died(object sender)
    {
        GameOver();
    }

    public void GameStart()
    {
        _player.transform.position = _stage.StartPoint.transform.position;

        _player.Resurrect();
    }

    public void GameOver()
    {
        GameStart();
    }
}
