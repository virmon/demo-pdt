using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [NonSerialized] public bool noActionFlag = false;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        noActionFlag = true;
        // Scene Transistion
    }

    public void GameClear()
    {
        noActionFlag = true;
        // Scene Transistion
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
