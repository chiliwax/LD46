using SA.QuestEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SA.QuestEditor
{
    public class SettingNode : BaseNode
    {
        Campaign pHcampaign;
        public override void DrawWidow()
        {

            GUILayout.BeginHorizontal();
            GUILayout.Label("Load campaign");
            pHcampaign = (Campaign)EditorGUILayout.ObjectField(pHcampaign, typeof(Campaign), false);
            if (GUILayout.Button("open"))
            {
                //ouvre toute les quêtes lié a la campagne
                foreach (Quests q in pHcampaign.GetQuests()){
                    QuestEditor.AddExistingQuest(q);
                }
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

