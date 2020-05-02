using SA.QuestEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SA.QuestEditor
{
    public class QuestNode : BaseNode
    {
        #region Variables GUI
        bool compactNode = true; //GUI
        bool boxDescription = false; //GUI
        bool boxConditions = false; //GUI
        bool boxWinArea = false; //GUI
        bool boxNothingArea = false; //GUI
        bool boxLooseArea = false; //GUI
        #endregion
        Quests quest;
        string newQuestName= "";
        

        public override void DrawWidow()
        {

            // height -> 25/ligne

            #region EditorStyles
            EditorStyles.textField.wordWrap = true;
            #endregion

            #region new/load quest
            if (quest == null)
            {
                GUILayout.Label("New Quest Name");
                newQuestName = GUILayout.TextField(newQuestName, 25);
                if (GUILayout.Button("create new Quest"))
                {
                    if (newQuestName != "")
                    {
                        //TODO vérifie que le nom de quête n'existe pas déja
                        quest = ScriptableObject.CreateInstance<Quests>();
                        AssetDatabase.CreateAsset(quest, "Assets/Resources/QuestsLock/" + newQuestName + ".asset");
                        AssetDatabase.SaveAssets();
                    }
                    else
                    {
                        GUILayout.Label("A new quest need a name !");
                    }
                }
                GUILayout.Space(20f);
                GUILayout.Label("Load Quest");
                quest = (Quests)EditorGUILayout.ObjectField(quest, typeof(Quests), false);

            }
            #endregion
            #region quest
            else
            {
                #region compact button
                if (compactNode != true && GUILayout.Button("Compact the node"))
                {
                    compactNode = true;
                    boxConditions = false;
                    boxDescription = false;
                    boxLooseArea = false;
                    boxNothingArea = false;
                    boxWinArea = false;
                    windowRect.height = 125;
                }

                if (compactNode && GUILayout.Button("Decompact the node"))
                {
                    compactNode = false;
                    windowRect.height = 225;
                }

                #endregion
                #region Title 2/2
                GUILayout.Label("QuestName");
                quest.QuestName = GUILayout.TextField(quest.QuestName, 100);
                GUILayout.Label("ENQuestName");
                quest.ENQuestName = GUILayout.TextField(quest.ENQuestName, 100);
                #endregion
                if (compactNode != true)
                {
                    #region Description button
                    if (boxDescription != true && GUILayout.Button("Open Description"))
                    {
                        boxDescription = true;
                        windowRect.height += 250;
                    }

                    if (boxDescription && GUILayout.Button("Reduce Description"))
                    {
                        boxDescription = false;
                        windowRect.height -= 250;
                    }
                    #endregion
                    if (boxDescription)
                    {
                        #region Description 2/2

                        GUILayout.Label("description");
                        EditorGUILayout.TextArea(quest.description, GUILayout.Height(100));
                        //description = GUILayout.TextArea(description, 300);
                        GUILayout.Label("ENDescription");
                        quest.ENDescription = GUILayout.TextArea(quest.ENDescription, GUILayout.Height(100));
                        #endregion
                    }
                    #region Conditions button
                    if (boxConditions != true && GUILayout.Button("Open Conditions"))
                        boxConditions = true;
                    if (boxConditions && GUILayout.Button("Reduce Conditions"))
                        boxConditions = false;
                    #endregion
                    if (boxConditions)
                    {
                        #region Condition 3/4
                        GUILayout.Label("_____Conditions_____");
                        quest.AllowError = GUILayout.Toggle(quest.AllowError, "AllowError");
                        //or
                        GUILayout.Label("reputationMini" + quest.reputationMini);
                        GUILayout.Space(5f);
                        quest.reputationMini = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.reputationMini), -100, 100));
                        GUILayout.Label("experienceMini" + quest.experienceMini);
                        GUILayout.Space(5f);
                        quest.experienceMini = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.experienceMini), -100, 100));
                        #endregion
                    }
                    #region Win Area button
                    if (boxWinArea != true && GUILayout.Button("Open Win sttings"))
                        boxWinArea = true;
                    if (boxWinArea && GUILayout.Button("Reduce Win sttings"))
                        boxWinArea = false;
                    #endregion
                    if (boxWinArea)
                    {
                        #region Win Panel /empty/
                        GUILayout.Label("_____Win Panel_____");
                        #endregion
                    }
                    if (AllowError)
                    {
                        #region Nothing Area button
                        if (boxNothingArea != true && GUILayout.Button("Open Nothing sttings"))
                            boxNothingArea = true;
                        if (boxNothingArea && GUILayout.Button("Reduce Nothing sttings"))
                            boxNothingArea = false;
                        #endregion
                        if (boxNothingArea)
                        {
                            #region Nothing Panel /empty/

                            #endregion
                        }
                    }
                    #region Loose Area button
                    if (boxLooseArea != true && GUILayout.Button("Open Loose sttings"))
                        boxLooseArea = true;
                    if (boxLooseArea && GUILayout.Button("Reduce Loose sttings"))
                        boxLooseArea = false;
                    #endregion
                    if (boxLooseArea)
                    {
                        #region Loose Panel /empty/
                        #endregion
                    }
                }
            }
            #endregion


        }

        public override void DrawCurve()
        {
        }
        
    }
}

