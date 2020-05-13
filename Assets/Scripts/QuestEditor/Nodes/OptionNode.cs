using SA.QuestEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SA.QuestEditor
{
    public class OptionNode : BaseNode
    {
        public BaseNode nodeLink;

        public override void DrawWidow()
        {
            windowRect.x = nodeLink.windowRect.x-60;
            windowRect.y = nodeLink.windowRect.y;
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("x", GUILayout.Height(25), GUILayout.Width(25))) {
                QuestEditor.windows.Remove(nodeLink);
                DestroyImmediate(nodeLink);
            }
            if (GUILayout.Button("-", GUILayout.Height(25), GUILayout.Width(25))) { 
                //compact quest in link
            }
            GUILayout.EndHorizontal();

            if (!QuestEditor.windows.Contains(nodeLink)) QuestEditor.windows.Remove(this); 

        }

    }
}

