using UnityEngine;

namespace ThroneRoom.Scripts
{
    public class Throne : MonoBehaviour
    {
        public ThroneArranger Arranger { get; private set; }
        public ThroneScore Score { get; private set; }
    
        public ThroneEmotion Emotion { get; private set; }
    
        private void Awake()
        {
            Arranger = GetComponent<ThroneArranger>();
            Score = GetComponent<ThroneScore>();
            Emotion = GetComponent<ThroneEmotion>();
        }
    }
}