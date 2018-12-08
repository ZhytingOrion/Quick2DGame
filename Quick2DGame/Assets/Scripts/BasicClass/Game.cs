using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    Anim,
    Play,
    Pause,
    End,
}

public class Game{

    private static Game _instance;
    public static Game Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new Game();
            }
            return _instance;
        }
    }

    Game()
    {
        gameState = GameState.Start;
    }

    public GameState gameState
    {
        get; set;
    }

    public int Role1CandyCount
    {
        get; set;
    }

    public int Role2CandyCount
    {
        get; set;
    }
}
