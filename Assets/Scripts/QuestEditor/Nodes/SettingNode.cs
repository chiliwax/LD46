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
            if (!compactOptionNode)
            {

                GUILayout.BeginHorizontal();
                GUILayout.Label("Load campaign");
                pHcampaign = (Campaign)EditorGUILayout.ObjectField(pHcampaign, typeof(Campaign), false);
                if (GUILayout.Button("open"))
                {
                    QuestEditor.AddExistingQuest(pHcampaign, 0, 0);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Load all quest");
                if (GUILayout.Button("open"))
                {
                    //ouvre toute les quêtes
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Clean all other nodes");
                if (GUILayout.Button("clean"))
                {
                    OptionNode optionNodeMaster = null;
                    foreach (OptionNode on in Resources.FindObjectsOfTypeAll(typeof(OptionNode)))
                    {
                        if (on.nodeLink == this)
                        {
                            optionNodeMaster = on;
                            break;
                        }
                    }
                    foreach (BaseNode q in Resources.FindObjectsOfTypeAll(typeof(BaseNode)))
                    {
                        if (q != this & q != optionNodeMaster)
                        {
                            QuestEditor.windows.Remove(q);
                            DestroyImmediate(q);
                        }

                    }
                }
                GUILayout.EndHorizontal();
            }
        }
    }

    
}

