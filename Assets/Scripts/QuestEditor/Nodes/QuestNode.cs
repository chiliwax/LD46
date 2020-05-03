using SA.QuestEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SA.QuestEditor
{
    public class QuestNode : BaseNode
    {
        #region Variables GUI
        bool compactNode = true; //GUI
        bool boxDescription = false; //GUI
        int height_boxDescription = 250;
        bool boxConditions = false; //GUI
        int height_boxConditions = 150;
        bool boxWinArea = false; //GUI
        int height_boxWinArea = 500;
        bool boxNothingArea = false; //GUI
        int height_boxNothingArea = 0;
        bool boxLooseArea = false; //GUI
        int height_boxLooseArea = 0;
        #endregion
        Quests quest;
        Quests AddLinkquest;
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
                        Debug.LogError("A new quest need a name !");
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
                        windowRect.height += height_boxDescription;
                    }

                    if (boxDescription && GUILayout.Button("Reduce Description"))
                    {
                        boxDescription = false;
                        windowRect.height -= height_boxDescription;
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
                    {
                        boxConditions = true;
                        windowRect.height += height_boxConditions;
                    }
                        
                    if (boxConditions && GUILayout.Button("Reduce Conditions"))
                    {
                        boxConditions = false;
                        windowRect.height -= height_boxConditions;
                    }
                        
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
                    {
                        boxWinArea = true;
                        windowRect.height += height_boxWinArea;
                        windowRect.height += (25 * quest.WinQuestlock.Count);
                        windowRect.height += (25 * quest.WinQuestUnlock.Count);
                    }
                        
                    if (boxWinArea && GUILayout.Button("Reduce Win sttings"))
                    {
                        boxWinArea = false;
                        windowRect.height -= (25 * quest.WinQuestlock.Count);
                        windowRect.height -= (25 * quest.WinQuestUnlock.Count);
                        windowRect.height -= height_boxWinArea;
                    }
                        
                    #endregion
                    if (boxWinArea)
                    {
                        #region Win Panel 9/9
                        GUILayout.Label("WinDescription");
                        EditorGUILayout.TextArea(quest.WinDescription, GUILayout.Height(100));
                        GUILayout.Label("ENWinDescription");
                        EditorGUILayout.TextArea(quest.ENWinDescription, GUILayout.Height(100));
                        GUILayout.Label("WinReputation" + quest.WinReputation);
                        quest.WinReputation = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.WinReputation), -100, 100));
                        GUILayout.Space(5f);
                        GUILayout.Label("WinExperience" + quest.WinExperience);
                        quest.WinExperience = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.WinExperience), -100, 100));
                        GUILayout.Space(5f);
                        GUILayout.Label("WinOr" + quest.WinOr);
                        quest.WinOr = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.WinOr), -100, 100));
                        GUILayout.Space(5f);
                        GUILayout.Label("WinQuestUnlock [" + quest.WinQuestUnlock.Count+"]");
                        #region WinQuestUnlock pannel + addButton
                        for (int i = 0; i < quest.WinQuestUnlock.Count; i++)
                        {
                            quest.WinQuestUnlock[i] = (Quests)EditorGUILayout.ObjectField(quest.WinQuestUnlock[i], typeof(Quests), false);
                            if (quest.WinQuestUnlock[i] == null) quest.WinQuestUnlock.RemoveAt(i);
                        }
                        GUILayout.Label("add quest to unlock when win");
                        AddLinkquest = (Quests)EditorGUILayout.ObjectField(AddLinkquest, typeof(Quests), false);
                        if (GUILayout.Button("add"))
                        {
                            quest.WinQuestUnlock.Add(AddLinkquest);//AddLinkquest
                        }

                        #endregion
                        #region WinQuestlock pannel + addButton
                        for (int i = 0; i < quest.WinQuestlock.Count; i++)
                        {
                            quest.WinQuestlock[i] = (Quests)EditorGUILayout.ObjectField(quest.WinQuestlock[i], typeof(Quests), false);
                            if (quest.WinQuestlock[i] == null) quest.WinQuestlock.RemoveAt(i);
                        }
                        GUILayout.Label("add quest to lock when win");
                        AddLinkquest = (Quests)EditorGUILayout.ObjectField(AddLinkquest, typeof(Quests), false);
                        if (GUILayout.Button("add"))
                        {
                            quest.WinQuestlock.Add(AddLinkquest);//AddLinkquest
                        }

                        #endregion
                        quest.WinPlayAfter = (Quests)EditorGUILayout.ObjectField(quest.WinPlayAfter, typeof(Quests), false);
                        quest.WinGameOver = (GameOver)EditorGUILayout.ObjectField(quest.WinGameOver, typeof(GameOver), false);
                        #endregion
                    }
                    if (quest.AllowError)
                    {
                        #region Nothing Area button
                        if (boxNothingArea != true && GUILayout.Button("Open Nothing sttings"))
                            boxNothingArea = true;
                        if (boxNothingArea && GUILayout.Button("Reduce Nothing sttings"))
                            boxNothingArea = false;
                        #endregion
                        if (boxNothingArea)
                        {
                            #region Nothing Panel 9/9
                            GUILayout.Label("NHDescription");
                            EditorGUILayout.TextArea(quest.NHDescription, GUILayout.Height(100));
                            GUILayout.Label("ENNHDescription");
                            EditorGUILayout.TextArea(quest.ENNHDescription, GUILayout.Height(100));
                            GUILayout.Label("NHReputation" + quest.NHReputation);
                            quest.NHReputation = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.NHReputation), -100, 100));
                            GUILayout.Space(5f);
                            GUILayout.Label("NHExperience" + quest.NHExperience);
                            quest.NHExperience = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.NHExperience), -100, 100));
                            GUILayout.Space(5f);
                            GUILayout.Label("NHOr" + quest.NHOr);
                            quest.NHOr = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.NHOr), -100, 100));
                            GUILayout.Space(5f);
                            GUILayout.Label("NHQuestUnlock [" + quest.NHQuestUnlock.Count + "]");
                            #region NHQuestUnlock pannel + addButton
                            for (int i = 0; i < quest.NHQuestUnlock.Count; i++)
                            {
                                quest.NHQuestUnlock[i] = (Quests)EditorGUILayout.ObjectField(quest.NHQuestUnlock[i], typeof(Quests), false);
                                if (quest.NHQuestUnlock[i] == null) quest.NHQuestUnlock.RemoveAt(i);
                            }
                            GUILayout.Label("add quest to unlock when nothing");
                            AddLinkquest = (Quests)EditorGUILayout.ObjectField(AddLinkquest, typeof(Quests), false);
                            if (GUILayout.Button("add"))
                            {
                                quest.NHQuestUnlock.Add(AddLinkquest);//AddLinkquest
                            }

                            #endregion
                            #region NHQuestlock pannel + addButton
                            for (int i = 0; i < quest.NHQuestlock.Count; i++)
                            {
                                quest.NHQuestlock[i] = (Quests)EditorGUILayout.ObjectField(quest.NHQuestlock[i], typeof(Quests), false);
                                if (quest.NHQuestlock[i] == null) quest.NHQuestlock.RemoveAt(i);
                            }
                            GUILayout.Label("add quest to lock when nothing");
                            AddLinkquest = (Quests)EditorGUILayout.ObjectField(AddLinkquest, typeof(Quests), false);
                            if (GUILayout.Button("add"))
                            {
                                quest.NHQuestlock.Add(AddLinkquest);//AddLinkquest
                            }

                            #endregion
                            quest.NHPlayAfter = (Quests)EditorGUILayout.ObjectField(quest.NHPlayAfter, typeof(Quests), false);
                            quest.NHGameOver = (GameOver)EditorGUILayout.ObjectField(quest.NHGameOver, typeof(GameOver), false);
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
                        #region Loose Panel 9/9
                        GUILayout.Label("LooseDescription");
                        EditorGUILayout.TextArea(quest.LooseDescription, GUILayout.Height(100));
                        GUILayout.Label("ENLooseDescription");
                        EditorGUILayout.TextArea(quest.ENLooseDescription, GUILayout.Height(100));
                        GUILayout.Label("LooseReputation" + quest.LooseReputation);
                        quest.LooseReputation = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.LooseReputation), -100, 100));
                        GUILayout.Space(5f);
                        GUILayout.Label("LooseExperience" + quest.LooseExperience);
                        quest.LooseExperience = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.LooseExperience), -100, 100));
                        GUILayout.Space(5f);
                        GUILayout.Label("LooseOr" + quest.LooseOr);
                        quest.LooseOr = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.LooseOr), -100, 100));
                        GUILayout.Space(5f);
                        GUILayout.Label("LooseQuestUnlock [" + quest.LooseQuestUnlock.Count + "]");
                        #region LooseQuestUnlock pannel + addButton
                        for (int i = 0; i < quest.LooseQuestUnlock.Count; i++)
                        {
                            quest.LooseQuestUnlock[i] = (Quests)EditorGUILayout.ObjectField(quest.LooseQuestUnlock[i], typeof(Quests), false);
                            if (quest.LooseQuestUnlock[i] == null) quest.LooseQuestUnlock.RemoveAt(i);
                        }
                        GUILayout.Label("add quest to unlock when nothing");
                        AddLinkquest = (Quests)EditorGUILayout.ObjectField(AddLinkquest, typeof(Quests), false);
                        if (GUILayout.Button("add"))
                        {
                            quest.LooseQuestUnlock.Add(AddLinkquest);//AddLinkquest
                        }

                        #endregion
                        #region LooseQuestlock pannel + addButton
                        for (int i = 0; i < quest.LooseQuestlock.Count; i++)
                        {
                            quest.LooseQuestlock[i] = (Quests)EditorGUILayout.ObjectField(quest.LooseQuestlock[i], typeof(Quests), false);
                            if (quest.LooseQuestlock[i] == null) quest.LooseQuestlock.RemoveAt(i);
                        }
                        GUILayout.Label("add quest to lock when nothing");
                        AddLinkquest = (Quests)EditorGUILayout.ObjectField(AddLinkquest, typeof(Quests), false);
                        if (GUILayout.Button("add"))
                        {
                            quest.LooseQuestlock.Add(AddLinkquest);//AddLinkquest
                        }

                        #endregion
                        quest.LoosePlayAfter = (Quests)EditorGUILayout.ObjectField(quest.LoosePlayAfter, typeof(Quests), false);
                        quest.LooseGameOver = (GameOver)EditorGUILayout.ObjectField(quest.LooseGameOver, typeof(GameOver), false);
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

