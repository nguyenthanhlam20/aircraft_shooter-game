using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeManager : MonoBehaviour
{


    #region Fields
    public static ScreenShakeManager Instance; // Static reference to the ScreenShakeManager class

    private float shakeTimeRemaining; // represents the remaining duration of the screen shake effect
    private float shakeForce; // determines the intensity of the screen shake.
    private float shakeFadeTime; // controls how quickly the screen shake effect fades out.
    private float shakeRotation; // represents the rotational component of the screen shake effect.


    private float xAmount;
    private float yAmount;

    [SerializeField] private float rotationMultiplier = 7.5f; // determines the multiplier applied to the shake rotation.
    #endregion

    #region LifeCycle Methods

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }


    /// <summary>
    /// The method is called after all Update methods have been called.
    /// </summary>
    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0f)
        {
            // Reduce shake time 
            shakeTimeRemaining -= Time.deltaTime;

            // Random position of x and y axis
            xAmount = Random.Range(-1f, 1f) * shakeForce;
            yAmount = Random.Range(-1f, 1f) * shakeForce;

            // Move camera to those above random positions
            transform.position += new Vector3(xAmount, yAmount, 0f);

            // Mathf.MoveTowards: move the value from current value to expected value
            shakeForce = Mathf.MoveTowards(shakeForce, 0f, shakeFadeTime * Time.deltaTime);
            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        } 
        else
        {
            transform.position = new Vector3(0f, 0f, -10f);
        }

        // Rotation the camera based on the shake rotation value
        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// This method start shake screen based on duration and force
    /// </summary>
    /// <param name="duration">representing the duration of the screen shake effect</param>
    /// <param name="force">representing the intensity of the shake</param>
    public void StartShake(float duration, float force)
    {
        // assigns the value of duration to the shakeTimeRemaining field
        shakeTimeRemaining = duration;

        // assigns the value of force to the shakeForce field, 
        shakeForce = force;

        // calculates the value of shakeFadeTime by dividing force by duration
        shakeFadeTime = force / duration;

        // Calculates the value of shakeRotation by multiplying force with rotationMultiplier,
        shakeRotation = force * rotationMultiplier;
    }

    #endregion
}