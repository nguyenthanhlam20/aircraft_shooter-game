using UnityEngine;

public class AudioManager : MonoBehaviour
{

    #region Fields
    // Declaration of a public static variable named Instance of type SoundManager
    public static AudioManager Instance;

    // Serialized private fields for three AudioSources
    [SerializeField] private AudioSource _musicSource, _effectSource;

    #endregion

    #region LifeCycle Methods
    /// <summary>
    /// Call everytime the class be active
    /// </summary>
    private void Awake()
    {
        // Checking if the Instance variable is null
        if (Instance == null)
        {
            // Assigning the current instance to the Instance variable
            Instance = this;

            // Prevents the gameObject from being destroyed when a new scene is loaded
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroys the duplicate instance of the SoundManager if it already exists
            Destroy(gameObject);
        }

    }


    #endregion

    #region Public Methods

    /// <summary>
    /// Play specific sound
    /// </summary>
    /// <param name="clip">the audio clip is being played</param>
    public void PlayEffect(AudioClip clip)
    {
        // Plays a one-shot audio clip using the _effectSource AudioSource
        _effectSource.PlayOneShot(clip);
    }

    #endregion


    public void PlayMusic(bool value)
    {
        if (value)
        {
            _musicSource.Play();
        }
        else
        {
            _musicSource.Stop();
        }

    }
}