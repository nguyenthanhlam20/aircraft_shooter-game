using UnityEngine;

public class PopupManager : MonoBehaviour
{

    // Declaration of a public static variable named Instance of type SoundManager
    public static PopupManager Instance;


    [SerializeField] private GameObject _pausedPopup;
    [SerializeField] private GameObject _victoryPopup;
    [SerializeField] private GameObject _gameoverPopup;
    [SerializeField] private GameObject _blurBackground;

    [SerializeField] private Vector2 centerPosition;
    [SerializeField] private AudioClip _victorySound;
    [SerializeField] private AudioClip _gameOverSound;

    private void Awake()
    {
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
        _blurBackground.SetActive(false);
        _pausedPopup.SetActive(false);
        _victoryPopup.SetActive(false);
        _gameoverPopup.SetActive(false);
    }

    public void ShowPausedPopup()
    {
        _blurBackground.SetActive(true);
        _pausedPopup.SetActive(true);
        LeanTween.moveLocal(_pausedPopup, centerPosition, 1f).setEase(LeanTweenType.easeOutExpo).setOnComplete(() =>
        {
            AudioManager.Instance.PlayMusic(false);
            Time.timeScale = 0f;
        });
    }
    public void ShowVictoryPopup()
    {
        AudioManager.Instance.PlayMusic(false);
        AudioManager.Instance.PlayEffect(_victorySound);
        _blurBackground.SetActive(true);
        _victoryPopup.SetActive(true);
        LeanTween.moveLocal(_victoryPopup, centerPosition, 1f).setEase(LeanTweenType.easeOutExpo);
    }

    public void ShowGameOverPopup()
    {
        AudioManager.Instance.PlayMusic(false);
        AudioManager.Instance.PlayEffect(_gameOverSound);
        _blurBackground.SetActive(true);
        _gameoverPopup.SetActive(true);

        LeanTween.moveLocal(_gameoverPopup, centerPosition, 1f).setEase(LeanTweenType.easeOutExpo);
    }


}
