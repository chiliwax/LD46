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
        private float saveHeight = 200;

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
                if (nodeLink.compactOptionNode)
                {
                    nodeLink.compactOptionNode = false;
                    nodeLink.windowRect.height = saveHeight;
                    nodeLink.windowRect.width = 200;
                }
                else
                {
                    nodeLink.compactOptionNode = true;
                    saveHeight = nodeLink.windowRect.height;
                    nodeLink.windowRect.height = 50;
                    nodeLink.windowRect.width = 100;
                }
            }
            GUILayout.EndHorizontal();

            if (!QuestEditor.windows.Contains(nodeLink)) QuestEditor.windows.Remove(this); 

        }

    }
}

