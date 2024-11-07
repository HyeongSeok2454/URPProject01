using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;
    public static event Action OnGameOver;

    private int score = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            score = +10;
            OnScoreChanged?.Invoke(score); ;
        }
        if (score >= 100)
        {
            OnGameOver?.Invoke();
        }
    }
}
