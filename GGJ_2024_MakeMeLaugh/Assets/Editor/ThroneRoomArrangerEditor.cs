using ThroneRoom.Scripts;
using UnityEditor;
using UnityEngine;

    [CustomEditor(typeof(ThroneRoomArranger))]
    public class ThroneRoomArrangerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ThroneRoomArranger arranger = (ThroneRoomArranger) target;
            if (GUILayout.Button("Build room"))
            {
                arranger.BuildThroneRoom();
            }
        }
    }
