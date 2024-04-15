using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisappear : MonoBehaviour
{
    public float fadeDuration = 2f;
    private float timer = 0f;

    private Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            float alpha = Mathf.Lerp(1f, 0f, (timer - 5f) / fadeDuration);
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);

            if (alpha <= 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
