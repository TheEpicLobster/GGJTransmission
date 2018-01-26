using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor {

    PathCreator creator;

    void OnSceneGUI()
    {
        Input();
        Draw();
    }

    void Input()
    {
        Event guiEvent = Event.current;
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            Undo.RecordObject(creator, "Add point");
            creator.AddSegment(mousePos);
        }
    }

    void Draw()
    {
        
        for (int i = 0; i < creator.points.Count; i++)
        {
            Handles.color = Color.red;
            Vector2 newPos = Handles.FreeMoveHandle(creator.points[i], Quaternion.identity, .1f, Vector2.zero, Handles.CylinderHandleCap);
            if ( creator.points[i] != newPos )
            {
                creator.MoveSegment(i, newPos);
            }
            Handles.color = Color.black;
            if (i > 0)
            {
                Handles.DrawLine(creator.points[i-1], creator.points[i]);
            }
        }
    }

    private void OnEnable()
    {
        creator = (PathCreator)target;
    }

}
