// Draw lines to the connected game objects that a script has.
// If the target object doesnt have any game objects attached
// then it draws a line from the object to (0, 0, 0).
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
#if UNITY_EDITOR

[CustomEditor(typeof(TargetScript))]


class ConnectLineHandleExampleScript : Editor
{
 

    void OnSceneGUI()
    {
        TargetScript connectedObjects = target as TargetScript;
        if (connectedObjects.nextTarget == null)
            return;

        Vector3 center = connectedObjects.transform.position;
        for (int i = 0; i < connectedObjects.nextTarget.Length; i++)
        {
            GameObject connectedObject = connectedObjects.nextTarget[i];
            if (connectedObject)
            {
                Handles.DrawLine(center, connectedObject.transform.position);
            }
            else
            {
                Handles.DrawLine(center, Vector3.zero);
            }
        }
    }
}
#endif