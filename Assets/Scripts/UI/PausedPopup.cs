using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedPopup : MonoBehaviour
{
    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _replayBtn;
    [SerializeField] private GameObject _blurBackground;


    private void Awake()
    {
        _homeBtn.onClick.AddListener(() => GoToHomePage());
        _playBtn.onClick.AddListener(() => Play());
        _replayBtn.onClick.AddListener(() => Replay());
    }

    private void Replay()
    {
        AudioManager.Instance.PlayMusic(true);
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    private void Play()
    {
        AudioManager.Instance.PlayMusic(true);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        _blurBackground.SetActive(false);
    }

    private void GoToHomePage()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlayMusic(true);
        SceneTransitionManager.Instance.OpenScene(0);
    }
}
