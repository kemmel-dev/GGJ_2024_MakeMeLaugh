using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ThroneRoom.Scripts
{
    public class ThroneEmotion : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private List<Sprite> _emotionSprites;
        [SerializeField] private int _timeShownMiliseconds;

        public async Task ShowEmotion(int num)
        {
            _image.enabled = true;
            _image.sprite = _emotionSprites[num];
            await Task.Delay(_timeShownMiliseconds);
            _image.enabled = false;
        }
    }
}