using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void HandEvent();
    public static event HandEvent OnHandSpawn;

    public delegate void CharacterEvent(GameObject hitChar, string cellCoor);
    public static event CharacterEvent OnHeroPlaced;

    public delegate void InitEvent();
    public static event InitEvent OnBoardInit;

    public delegate void DestroyEvent();
    public static event DestroyEvent OnDestroyHeroes;

    public delegate void ScoreEvent(int score);
    public static event ScoreEvent OnScore;

    public delegate void GameOverEvent();
    public static event GameOverEvent OnGameOver;

    public delegate void ResetEvent();
    public static event ResetEvent OnResetGame;

    public static void TriggerHandSpawn()
    {
        if (OnHandSpawn != null)
        {
            OnHandSpawn();
        }
    }

    public static void TriggerCharSnapped(GameObject hitChar, string cellCoor)
    {
        if (OnHeroPlaced != null)
        {
            OnHeroPlaced(hitChar, cellCoor);
        }
    }

    public static void TriggerInitBoard()
    {
        if(OnBoardInit != null)
        {
            OnBoardInit();
        }
    }

    public static void TriggerDestroyHeroes()
    {
        if (OnDestroyHeroes != null)
        {
            OnDestroyHeroes();
        }            
    }

    public static void TriggerOnScore(int score)
    {
        if (OnScore != null)
        {
            OnScore(score);
        }            
    }

    public static void TriggerGameOver()
    {
        if(OnGameOver != null)
        {
            OnGameOver();
        }
    }

    public static void TriggerResetGameState()
    {
        if (OnResetGame != null)
        {
            OnResetGame();
        }
    }
}
