using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private SpriteRenderer _sr;
    private WaitForSeconds _flashDelay;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _flashDelay = new WaitForSeconds(0.083f);
    }

    

    public IEnumerator HideShield()
    {
        for (int i = 0; i < 10; i++)
        {
            // Disable the sprite renderer
            _sr.enabled = false;
            yield return _flashDelay;

            // Enable the sprite renderer
            _sr.enabled = true;
            yield return _flashDelay;
        }
        gameObject.SetActive(false);
    }

}
