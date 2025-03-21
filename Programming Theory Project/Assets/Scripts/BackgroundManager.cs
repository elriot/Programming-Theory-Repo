using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public Image backgroundImage;
    public Texture2D backgroundTexture; 

    void Start()
    {
        if (backgroundImage != null && backgroundTexture != null)
        {
            // Texture2D → Sprite 변환
            Sprite newSprite = Sprite.Create(backgroundTexture, new Rect(0, 0, backgroundTexture.width, backgroundTexture.height), new Vector2(0.5f, 0.5f));
            backgroundImage.sprite = newSprite;
        }
    }
}