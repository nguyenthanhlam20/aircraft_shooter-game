using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private TextMeshProUGUI scoreDisplayer;

    private int _score = 0;

    public int Score { get { return _score; } }

    private void Awake()
    {
        scoreDisplayer = GetComponent<TextMeshProUGUI>();
        // Checking if the Instance variable is null
        if (Instance == null)
        {
            // Assigning the current instance to the Instance variable
            Instance = this;
        }
        else
        {
            // Destroys the duplicate instance of the SoundManager if it already exists
            Destroy(gameObject);
        }

    }
    public void UpdateScore(int value)
    {
        _score += value;
        scoreDisplayer.text = $"Score: {_score}";

        if(_score == 16)
        {
            PopupManager.Instance.ShowVictoryPopup();
        }
    }
  
}
