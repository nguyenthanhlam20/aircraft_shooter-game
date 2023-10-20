
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    private static SceneTransitionManager _instance;

    public static SceneTransitionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneTransitionManager>();
                if (_instance != null)
                {
                    _instance = new GameObject("SceneTransitionManager").AddComponent<SceneTransitionManager>();
                }
            }
            return _instance;
        }

    }

    [SerializeField] private RectTransform transitionImage;
    [SerializeField] private float transitionDuration;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        transitionImage.gameObject.SetActive(true);

        LeanTween.alpha(transitionImage, 1, 0f);
        LeanTween.alpha(transitionImage, 0, transitionDuration).setOnComplete(() =>
        {
            transitionImage.gameObject.SetActive(false);
        });
    }


    public void OpenScene(int sceneIndex)
    {
        Time.timeScale = 1f;

        transitionImage.gameObject.SetActive(true);
        LeanTween.alpha(transitionImage, 0, 0f);
        LeanTween.alpha(transitionImage, 1, transitionDuration).setOnComplete(() =>
        {
            SceneManager.LoadScene((int)sceneIndex);
        });
    }

    public void OpenSceneByName(string sceneName)
    {
        transitionImage.gameObject.SetActive(true);
        LeanTween.alpha(transitionImage, 0, 0f);
        LeanTween.alpha(transitionImage, 1, transitionDuration).setOnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
