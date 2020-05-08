using SA.QuestEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SA.QuestEditor
{
    public class SettingsNode : BaseNode
    {
        
        Campaign PHcampaign;
        public override void DrawWidow()
        {

            GUILayout.BeginHorizontal();
            GUILayout.Label("Load campaign");
            PHcampaign = (Campaign)EditorGUILayout.ObjectField(PHcampaign, typeof(Campaign), false);
            if (GUILayout.Button("open"))
            {
                //ouvre toute les quêtes lié a la campagne
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Load all quest");
            if (GUILayout.Button("open"))
            {
                //ouvre toute les quêtes
            }
            GUILayout.EndHorizontal();
            
        }
        

    }
}

