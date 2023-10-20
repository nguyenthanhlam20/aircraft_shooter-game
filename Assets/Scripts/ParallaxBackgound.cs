using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgound : MonoBehaviour
{

    #region Feilds
    private float startPosY; // Starting position of the sprite in the Y direction
    private float lengthY; // Starting position of the sprite in the Y direction
    [SerializeField] private float parallaxSpeedY = 5f; // Parallax speed in the Y direction
    #endregion

    #region LifeCycle Methods

    private void Start()
    {
        // Store the initial position and length of the sprite
        startPosY = transform.position.y;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;

    }

    private void Update()
    {
        // Update the position of the sprite based on the parallax effect
        transform.position = Vector2.Lerp(transform.position,
             new Vector2(transform.position.x, transform.position.y - parallaxSpeedY),
             0.3f * Time.deltaTime);

        startPosY = transform.position.y;

        if (startPosY <= -lengthY)
        {
            transform.position = new Vector2(0f, lengthY);
        }
    }


    #endregion
}