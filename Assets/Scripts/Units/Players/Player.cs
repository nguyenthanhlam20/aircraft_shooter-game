using UnityEngine;

public class Player : MonoBehaviour
{
    #region Fields

    public float _speed = 2f;
    private bool _isDead = false;
    private PlayerAudio playerAudio;
    private PlayerAnimation playerAnimation;

    #endregion


    #region Properties
    public bool IsDead { get { return _isDead; } }
    public float Speed { get { return _speed; } }

    #endregion

    private void Awake()
    {
        playerAudio = GetComponent<PlayerAudio>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    public void Explode()
    {
         _isDead = true;
        ScreenShakeManager.Instance.StartShake(0.2f, 0.1f);
        playerAudio.PlayExplosionSound();
        playerAnimation.PlayDead();
    }

    public void ShowGameOverPopup()
    {
        gameObject.SetActive(false);
        PopupManager.Instance.ShowGameOverPopup();
    }

}
