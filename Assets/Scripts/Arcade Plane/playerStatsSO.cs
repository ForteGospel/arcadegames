using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Arcade Plane/Player Stats")]
public class playerStatsSO : ScriptableObject
{

    int healthPoint, boost, bombs, score;
    [SerializeField]
    int startingHealth = 100, startingBoost = 100, startingBombs = 3, startingScore = 0;

    public int HealthPoints
    {
        get { return healthPoint; }
        set { healthPoint = value; }
    }

    public int Boost
    {
        get { return boost; }
        set { boost = value; }
    }

    public int Bombs
    {
        get { return bombs; }
        set { bombs = value; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    private void Awake()
    {
        startGame();
    }

    void startGame()
    {
        healthPoint = startingHealth;
        boost = startingBoost;
        bombs = startingBombs;
        score = startingScore;
    }
}
