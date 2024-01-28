using UnityEngine;
using UnityEngine.UI;

namespace JoinScene
{
    public class MiniGamePreview : MonoBehaviour
    {
        public Image highlight;
        public Image previewImage;
        public Sprite previewSprite;

        public void Highlight(Color color)
        {
            highlight.color = color;
        }

        private void OnValidate()
        {
            previewImage.sprite = previewSprite;
        }
    }
}
