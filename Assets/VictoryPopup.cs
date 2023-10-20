using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryPopup : MonoBehaviour
{

    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button _replayBtn;


    private void Awake()
    {
        _homeBtn.onClick.AddListener(() => GoToHomePage());
        _replayBtn.onClick.AddListener(() => Replay());
    }

    private void Replay()
    {
        AudioManager.Instance.PlayMusic(true);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    private void GoToHomePage()
    {
        AudioManager.Instance.PlayMusic(true);

        SceneTransitionManager.Instance.OpenScene(0);
    }
}
