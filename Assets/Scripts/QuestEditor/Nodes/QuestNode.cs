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
        int height_boxBase = 225;
        int height_boxBaseClose = 125;
        bool compactNode = true; //GUI
        bool boxDescription = false; //GUI
        int height_boxDescription = 275;
        bool boxConditions = false; //GUI
        int height_boxConditions = 75;
        bool boxWinArea = false; //GUI
        int height_boxWinArea = 450;
        bool boxNothingArea = false; //GUI
        int height_boxNothingArea = 450;
        bool boxLooseArea = false; //GUI
        int height_boxLooseArea = 450;
        #endregion
        public Quests quest;
        Quests AddLinkquest;
        string newQuestName= "";
        Rect compactLineRect;
        
        void OnGUI()
        {

        }
        public override void DrawWidow()
        {

            // height -> 25/ligne

            #region EditorStyles
            EditorStyles.textField.wordWrap = true;
            #endregion
            if (!compactOptionNode)
            {
                #region new/load quest
                if (quest == null)
                {
                    GUILayout.Label("Create new Quest");
                    GUILayout.BeginHorizontal();
                    newQuestName = GUILayout.TextField(newQuestName, 57);
                    if (GUILayout.Button("Create"))
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
                    GUILayout.EndHorizontal();
                    GUILayout.Space(20f);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Load Quest");
                    quest = (Quests)EditorGUILayout.ObjectField(quest, typeof(Quests), false);
                    GUILayout.EndHorizontal();
                    if (quest != null) windowRect.height = height_boxBaseClose;

                }
                #endregion
                #region quest
                else
                {
                    this.windowTitle = (quest.name);
                    #region compact button
                    if (compactNode != true && GUILayout.Button("-",
                        GUILayout.Height(25), GUILayout.Width(25), GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true)))
                    {
                        compactNode = true;
                        boxConditions = false;
                        boxDescription = false;
                        boxLooseArea = false;
                        boxNothingArea = false;
                        boxWinArea = false;
                        windowRect.height = height_boxBaseClose;
                    }

                    if (compactNode && GUILayout.Button("+",
                        GUILayout.Height(25), GUILayout.Width(25), GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true)))
                    {
                        compactNode = false;
                        windowRect.height = height_boxBase;
                    }

                    #endregion
                    #region Load another quest
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Load Another Quest");
                    quest = (Quests)EditorGUILayout.ObjectField(quest, typeof(Quests), false);
                    GUILayout.EndHorizontal();
                    #endregion
                    #region Title 2/2
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("TitleFR");
                    quest.QuestName = GUILayout.TextField(quest.QuestName, 19);
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("TitleEN");
                    quest.ENQuestName = GUILayout.TextField(quest.ENQuestName, 19);
                    GUILayout.EndHorizontal();
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
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Campagne");
                            quest.campagne = (Campaign)EditorGUILayout.ObjectField(quest.campagne, typeof(Campaign), false);
                            GUILayout.EndHorizontal();
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
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("reput ");
                            quest.reputationMini = int.Parse(GUILayout.TextField(Convert.ToString(quest.reputationMini), GUILayout.Width(35f)));
                            quest.reputationMini = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.reputationMini), -100, 100, GUILayout.Width(75f)));
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("xp ");
                            quest.experienceMini = int.Parse(GUILayout.TextField(Convert.ToString(quest.experienceMini), GUILayout.Width(35f)));
                            quest.experienceMini = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.experienceMini), -100, 100, GUILayout.Width(75f)));
                            GUILayout.EndHorizontal();
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

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Gain Reput ");
                            quest.WinReputation = int.Parse(GUILayout.TextField(Convert.ToString(quest.WinReputation), GUILayout.Width(35f)));
                            quest.WinReputation = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.WinReputation), -100, 100, GUILayout.Width(75f)));
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Gain xp ");
                            quest.WinOr = int.Parse(GUILayout.TextField(Convert.ToString(quest.WinExperience), GUILayout.Width(35f)));
                            quest.WinOr = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.WinExperience), -100, 100, GUILayout.Width(75f)));
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Gain or ");
                            quest.WinExperience = int.Parse(GUILayout.TextField(Convert.ToString(quest.WinOr), GUILayout.Width(35f)));
                            quest.WinExperience = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.WinOr), -100, 100, GUILayout.Width(75f)));
                            GUILayout.EndHorizontal();

                            GUILayout.Label("WinQuestUnlock [" + quest.WinQuestUnlock.Count + "]");
                            #region WinQuestUnlock pannel + addButton
                            for (int i = 0; i < quest.WinQuestUnlock.Count; i++)
                            {
                                quest.WinQuestUnlock[i] = (Quests)EditorGUILayout.ObjectField(quest.WinQuestUnlock[i], typeof(Quests), false);
                                if (quest.WinQuestUnlock[i] == null) quest.WinQuestUnlock.RemoveAt(i);
                            }
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("add to unlock");
                            AddLinkquest = (Quests)EditorGUILayout.ObjectField(AddLinkquest, typeof(Quests), false);
                            if (GUILayout.Button("add"))
                            {
                                quest.WinQuestUnlock.Add(AddLinkquest);//AddLinkquest
                            }
                            GUILayout.EndHorizontal();
                            #endregion
                            GUILayout.Label("WinQuestlock [" + quest.WinQuestlock.Count + "]");
                            #region WinQuestlock pannel + addButton
                            for (int i = 0; i < quest.WinQuestlock.Count; i++)
                            {
                                quest.WinQuestlock[i] = (Quests)EditorGUILayout.ObjectField(quest.WinQuestlock[i], typeof(Quests), false);
                                if (quest.WinQuestlock[i] == null) quest.WinQuestlock.RemoveAt(i);
                            }
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("add to lock");
                            AddLinkquest = (Quests)EditorGUILayout.ObjectField(AddLinkquest, typeof(Quests), false);
                            if (GUILayout.Button("add"))
                            {
                                quest.WinQuestlock.Add(AddLinkquest);//AddLinkquest
                            }
                            GUILayout.EndHorizontal();
                            #endregion
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Next Quest:");
                            quest.WinPlayAfter = (Quests)EditorGUILayout.ObjectField(quest.WinPlayAfter, typeof(Quests), false);
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("EndGame:");
                            quest.WinGameOver = (GameOver)EditorGUILayout.ObjectField(quest.WinGameOver, typeof(GameOver), false);
                            GUILayout.EndHorizontal();
                            #endregion

                        }
                        if (quest.AllowError)
                        {
                            #region Nothing Area button
                            if (boxNothingArea != true && GUILayout.Button("Open Nothing sttings"))
                            {
                                windowRect.height += (25 * quest.NHQuestlock.Count);
                                windowRect.height += (25 * quest.NHQuestUnlock.Count);
                                windowRect.height += height_boxNothingArea;
                                boxNothingArea = true;
                            }

                            if (boxNothingArea && GUILayout.Button("Reduce Nothing sttings"))
                            {
                                windowRect.height -= (25 * quest.NHQuestlock.Count);
                                windowRect.height -= (25 * quest.NHQuestUnlock.Count);
                                windowRect.height -= height_boxNothingArea;
                                boxNothingArea = false;
                            }

                            #endregion
                            if (boxNothingArea)
                            {
                                #region Nothing Panel 9/9
                                GUILayout.Label("NHDescription");
                                EditorGUILayout.TextArea(quest.NHDescription, GUILayout.Height(100));
                                GUILayout.Label("ENNHDescription");
                                EditorGUILayout.TextArea(quest.ENNHDescription, GUILayout.Height(100));
                                GUILayout.BeginHorizontal();
                                GUILayout.Label("Gain Reput ");
                                quest.WinReputation = int.Parse(GUILayout.TextField(Convert.ToString(quest.NHReputation), GUILayout.Width(35f)));
                                quest.WinReputation = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.NHReputation), -100, 100, GUILayout.Width(75f)));
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("Gain xp ");
                                quest.NHExperience = int.Parse(GUILayout.TextField(Convert.ToString(quest.NHExperience), GUILayout.Width(35f)));
                                quest.NHExperience = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.NHExperience), -100, 100, GUILayout.Width(75f)));
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("Gain or ");
                                quest.NHOr = int.Parse(GUILayout.TextField(Convert.ToString(quest.NHOr), GUILayout.Width(35f)));
                                quest.NHOr = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.NHOr), -100, 100, GUILayout.Width(75f)));
                                GUILayout.EndHorizontal();
                                GUILayout.Label("NHQuestUnlock [" + quest.NHQuestUnlock.Count + "]");
                                #region NHQuestUnlock pannel + addButton
                                GUILayout.BeginHorizontal();
                                for (int i = 0; i < quest.NHQuestUnlock.Count; i++)
                                {
                                    quest.NHQuestUnlock[i] = (Quests)EditorGUILayout.ObjectField(quest.NHQuestUnlock[i], typeof(Quests), false);
                                    if (quest.NHQuestUnlock[i] == null) quest.NHQuestUnlock.RemoveAt(i);
                                }
                                GUILayout.Label("add to unlock");
                                AddLinkquest = (Quests)EditorGUILayout.ObjectField(AddLinkquest, typeof(Quests), false);
                                if (GUILayout.Button("add"))
                                {
                                    quest.NHQuestUnlock.Add(AddLinkquest);//AddLinkquest
                                }
                                GUILayout.EndHorizontal();
                                #endregion
                                GUILayout.Label("NHQuestlock [" + quest.NHQuestlock.Count + "]");
                                #region NHQuestlock pannel + addButton
                                GUILayout.BeginHorizontal();
                                for (int i = 0; i < quest.NHQuestlock.Count; i++)
                                {
                                    quest.NHQuestlock[i] = (Quests)EditorGUILayout.ObjectField(quest.NHQuestlock[i], typeof(Quests), false);
                                    if (quest.NHQuestlock[i] == null) quest.NHQuestlock.RemoveAt(i);
                                }
                                GUILayout.Label("add to lock");
                                AddLinkquest = (Quests)EditorGUILayout.ObjectField(AddLinkquest, typeof(Quests), false);
                                if (GUILayout.Button("add"))
                                {
                                    quest.NHQuestlock.Add(AddLinkquest);//AddLinkquest
                                }
                                GUILayout.EndHorizontal();
                                #endregion
                                quest.NHPlayAfter = (Quests)EditorGUILayout.ObjectField(quest.NHPlayAfter, typeof(Quests), false);
                                quest.NHGameOver = (GameOver)EditorGUILayout.ObjectField(quest.NHGameOver, typeof(GameOver), false);
                                #endregion
                            }
                        }
                        #region Loose Area button
                        if (boxLooseArea != true && GUILayout.Button("Open Loose sttings"))
                        {
                            windowRect.height += (25 * quest.LooseQuestlock.Count);
                            windowRect.height += (25 * quest.LooseQuestUnlock.Count);
                            windowRect.height += height_boxLooseArea;
                            boxLooseArea = true;
                        }
                        if (boxLooseArea && GUILayout.Button("Reduce Loose sttings"))
                        {
                            windowRect.height -= (25 * quest.LooseQuestlock.Count);
                            windowRect.height -= (25 * quest.LooseQuestUnlock.Count);
                            windowRect.height -= height_boxLooseArea;
                            boxLooseArea = false;
                        }

                        #endregion
                        if (boxLooseArea)
                        {
                            #region Loose Panel 9/9
                            GUILayout.Label("LooseDescription");
                            EditorGUILayout.TextArea(quest.LooseDescription, GUILayout.Height(100));
                            GUILayout.Label("ENLooseDescription");
                            EditorGUILayout.TextArea(quest.ENLooseDescription, GUILayout.Height(100));
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Gain Reput ");
                            quest.WinReputation = int.Parse(GUILayout.TextField(Convert.ToString(quest.LooseReputation), GUILayout.Width(35f)));
                            quest.WinReputation = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.LooseReputation), -100, 100, GUILayout.Width(75f)));
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Gain xp ");
                            quest.LooseExperience = int.Parse(GUILayout.TextField(Convert.ToString(quest.LooseExperience), GUILayout.Width(35f)));
                            quest.LooseExperience = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.LooseExperience), -100, 100, GUILayout.Width(75f)));
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Gain or ");
                            quest.LooseOr = int.Parse(GUILayout.TextField(Convert.ToString(quest.LooseOr), GUILayout.Width(35f)));
                            quest.LooseOr = Convert.ToInt32(GUILayout.HorizontalSlider(Convert.ToSingle(quest.LooseOr), -100, 100, GUILayout.Width(75f)));
                            GUILayout.EndHorizontal();
                            GUILayout.Label("LooseQuestUnlock [" + quest.LooseQuestUnlock.Count + "]");
                            #region LooseQuestUnlock pannel + addButton
                            GUILayout.BeginHorizontal();
                            for (int i = 0; i < quest.LooseQuestUnlock.Count; i++)
                            {
                                quest.LooseQuestUnlock[i] = (Quests)EditorGUILayout.ObjectField(quest.LooseQuestUnlock[i], typeof(Quests), false);
                                if (quest.LooseQuestUnlock[i] == null) quest.LooseQuestUnlock.RemoveAt(i);
                            }
                            GUILayout.Label("add to unlock");
                            AddLinkquest = (Quests)EditorGUILayout.ObjectField(AddLinkquest, typeof(Quests), false);
                            if (GUILayout.Button("add"))
                            {
                                quest.LooseQuestUnlock.Add(AddLinkquest);//AddLinkquest
                            }
                            GUILayout.EndHorizontal();
                            #endregion
                            GUILayout.Label("LooseQuestlock [" + quest.LooseQuestlock.Count + "]");
                            #region LooseQuestlock pannel + addButton
                            GUILayout.BeginHorizontal();
                            for (int i = 0; i < quest.LooseQuestlock.Count; i++)
                            {
                                quest.LooseQuestlock[i] = (Quests)EditorGUILayout.ObjectField(quest.LooseQuestlock[i], typeof(Quests), false);
                                if (quest.LooseQuestlock[i] == null) quest.LooseQuestlock.RemoveAt(i);
                            }
                            GUILayout.Label("add to lock");
                            AddLinkquest = (Quests)EditorGUILayout.ObjectField(AddLinkquest, typeof(Quests), false);
                            if (GUILayout.Button("add"))
                            {
                                quest.LooseQuestlock.Add(AddLinkquest);//AddLinkquest
                            }
                            GUILayout.EndHorizontal();
                            #endregion
                            quest.LoosePlayAfter = (Quests)EditorGUILayout.ObjectField(quest.LoosePlayAfter, typeof(Quests), false);
                            quest.LooseGameOver = (GameOver)EditorGUILayout.ObjectField(quest.LooseGameOver, typeof(GameOver), false);
                            #endregion
                        }
                    }
                }
                #endregion

            }
            else if (quest != null)GUILayout.Label(quest.ENQuestName) ;


        }
        public override void DrawCurve()
        {
            #region curve
            if (quest != null)
            {
                QuestEditor.LookForQuestNodeAndDrawCurve
                    (this, quest.WinPlayAfter, Color.green);
                QuestEditor.LookForQuestNodeAndDrawCurve
                    (this, quest.WinQuestUnlock, new Color(0, .7f, 0, 1));
                QuestEditor.LookForQuestNodeAndDrawCurve
                    (this, quest.NHPlayAfter, new Color(0, 0, 0, 1));
                QuestEditor.LookForQuestNodeAndDrawCurve
                    (this, quest.NHQuestUnlock, new Color(.3f, 0, 0, 1));
                QuestEditor.LookForQuestNodeAndDrawCurve
                    (this, quest.LoosePlayAfter, Color.red);
                QuestEditor.LookForQuestNodeAndDrawCurve
                    (this, quest.LooseQuestUnlock, new Color(.7f, 0, 0, 1));

            }

            #endregion
        }

    }
}

