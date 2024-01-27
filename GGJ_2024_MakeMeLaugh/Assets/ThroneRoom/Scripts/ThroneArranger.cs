using UnityEngine;

namespace ThroneRoom.Scripts
{
    public class ThroneArranger : MonoBehaviour
    {
        [SerializeField] private Transform _floorPiece;
        [SerializeField] private float _stepDeltaY;

        public void Arrange(int numSteps)
        {
            _floorPiece.localPosition += new Vector3(0, 3 - numSteps * _stepDeltaY - 0.01f, 0);
        }
    }
}
