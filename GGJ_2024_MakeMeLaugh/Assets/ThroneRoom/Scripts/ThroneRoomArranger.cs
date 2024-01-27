using System;
using UnityEngine;

namespace ThroneRoom.Scripts
{
    public class ThroneRoomArranger : MonoBehaviour
    {
        
        [SerializeField] private int _playerCount = 4;

        [SerializeField] private GameObject _throneRoomSlicePrefab;
        [SerializeField] private GameObject _floorSlicePrefab;
        [SerializeField] private GameObject _planePrefab;
        
        [SerializeField] private float _sliceOffsetX;
        [SerializeField] private int numSteps;
        
        [Header("Hardcoded values, no touchy!")]
        [SerializeField] private float _floorOffsetY;
        [SerializeField] private float _highestStepY;
        [SerializeField] private float _offsetPlane;
        
        private Transform _parent;

        public void BuildThroneRoom()
        {
            DeleteExistingRoom();
            BuildSlices();
        }

        private void DeleteExistingRoom()
        {
            // Delete existing room and refresh parent
            if (transform.childCount != 0)
            {
                _parent = transform.GetChild(0);
                DestroyImmediate(_parent.gameObject);
            }

            // Create new parent
            _parent = new GameObject().transform;
            _parent.parent = transform;
        }

        private void BuildSlices()
        {
            for (int i = 0; i < _playerCount; i++)
            {
                var go = Instantiate(_throneRoomSlicePrefab);
                go.transform.position += i * new Vector3(_sliceOffsetX, 0f, 0f);
                go.transform.SetParent(_parent);

                var floor = BuildFloor(go.transform);
                if (i == 0)
                {
                    var plane = Instantiate(_planePrefab, floor);
                    plane.transform.localPosition += new Vector3(0, _offsetPlane, 0);
                }
            }
        }

        private Transform BuildFloor(Transform parent)
        {
            var go = Instantiate(_floorSlicePrefab, parent);
            go.transform.localPosition = new Vector3(0, _highestStepY - (numSteps * _floorOffsetY), 0);
            return go.transform;
        }
    }
}
