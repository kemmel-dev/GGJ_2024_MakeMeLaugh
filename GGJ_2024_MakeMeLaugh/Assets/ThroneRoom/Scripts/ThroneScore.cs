using TMPro;
using UnityEngine;

namespace ThroneRoom.Scripts
{
    public class ThroneScore : MonoBehaviour
    {
        private int _score = 0;
        [SerializeField] private TextMeshPro _textMeshPro;

        public bool IncrementScore(int amount)
        {
            _score += amount;
            _textMeshPro.text = _score.ToString();
            return _score > 15;
        }

        public void SetScore(int points)
        {
            _score = points;
            _textMeshPro.text = _score.ToString();
        }
    }
}
