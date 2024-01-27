using TMPro;
using UnityEngine;

namespace ThroneRoom.Scripts
{
    public class ThroneScore : MonoBehaviour
    {
        private int _score = 0;
        [SerializeField] private TextMeshPro _textMeshPro;

        public void IncrementScore(int amount)
        {
            _textMeshPro.text = (_score += amount).ToString();
        }

        public void SetScore(int points)
        {
            _score = points;
            _textMeshPro.text = _score.ToString();
        }
    }
}
