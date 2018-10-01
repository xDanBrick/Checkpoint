using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour {

    [SerializeField] private float fadeSpeed = 0.006f;

    private UnityEngine.UI.RawImage fadeOutUIImage;
    private bool isFading = false;
    private string sceneFadeChange;
    private float fadeMultiplier = 1.0f;
    // Use this for initialization
    void Start () {
        fadeOutUIImage = GetComponent<UnityEngine.UI.RawImage>();
    }

    public void StartFade(string sceneName, float multiplier)
    {
        sceneFadeChange = sceneName;
        fadeOutUIImage.enabled = true;
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, 0.0f);
        isFading = true;
        fadeMultiplier = multiplier;
    }

    private void FixedUpdate()
    {
        if (isFading)
        {
            float alpha = fadeOutUIImage.color.a + (fadeSpeed * fadeMultiplier);
            if (alpha < 1.0f)
            {
                fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneFadeChange);
            }
        }
    }
}
