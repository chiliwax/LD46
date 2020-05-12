﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace SA.QuestEditor
{
    public class QuestEditor : EditorWindow
    {
        #region Variables
        public static List<BaseNode> windows = new List<BaseNode>();
        Vector3 mousePosition;
        bool makeTransition;
        bool clickedOnWindow;
        BaseNode selectedNode;


        public enum UserActions
        {
            addQuest, addTransitionNode, deleteNode, openSettings
        }

        #endregion

        #region Init
        [MenuItem("Quest Editor/Editor")]
        static void ShowEditor()
        {
            QuestEditor editor = EditorWindow.GetWindow<QuestEditor>();
            editor.minSize = new Vector2(800, 600);
        }
        #endregion

        #region GUI Methods
        private void OnGUI()
        {
            Event e = Event.current;
            mousePosition = e.mousePosition;
            UserInput(e);
            DrawWindows();
        }

        private void OnEnable() //clear editor STATUE OFF
        {
            //windows.Clear();
        }

        void DrawWindows()
        {
            BeginWindows();
            foreach (BaseNode n in windows)
            {
                n.DrawCurve();
            }
            for (int i = 0; i < windows.Count; i++)
            {
                windows[i].windowRect = GUI.Window(i, windows[i].windowRect, DrawNodeWindow, windows[i].windowTitle);
            }


            EndWindows();
        }

        void DrawNodeWindow(int id)
        {
            windows[id].DrawWidow();
            GUI.DragWindow();
        }

        void UserInput(Event e)
        {
            if(e.button == 1 && !makeTransition)
            {
                if(e.type == EventType.MouseDown)
                {
                    RightClick(e);
                }
            }

            if (e.button == 0 && !makeTransition)
            {
                if (e.type == EventType.MouseDown)
                {
                    
                }
            }
        }

        void RightClick(Event e)
        {
            selectedNode = null;
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].windowRect.Contains(e.mousePosition))
                {
                    clickedOnWindow = true;
                    selectedNode = windows[i];
                    break;
                }
            }

            if (!clickedOnWindow)
            {
                AddNewNode(e);
                
            }
            else
            {
                ModifyNode(e);
            }
        }

        public void AddNewNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Add Quest"), false, ContextCallback, UserActions.addQuest);
            menu.AddItem(new GUIContent("OpenSettings"), false, ContextCallback, UserActions.openSettings);

            menu.ShowAsContext();
            e.Use();
        }
        public static void AddExistingQuest(Quests q)
        {
            Debug.Log("add quest ");
        }
        void ModifyNode(Event e)
        {      
            GenericMenu menu = new GenericMenu();
            if (selectedNode is QuestNode)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Close quest"), false, ContextCallback, UserActions.deleteNode);
            }

            if (selectedNode is SettingNode)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Close Settings"), false, ContextCallback, UserActions.deleteNode);
            }

            menu.ShowAsContext();
            e.Use();
        }

        void ContextCallback(object o) // effet des actions
        {
            UserActions a = (UserActions)o;
            switch (a)
            {
                case UserActions.addQuest:
                    QuestNode questNode = ScriptableObject.CreateInstance<QuestNode>();
                    questNode.windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 125);
                    questNode.windowTitle = "Quest";
                    windows.Add(questNode);

                    break;
                case UserActions.addTransitionNode:
                    break;
                case UserActions.openSettings:
                    SettingNode SettingNode = ScriptableObject.CreateInstance<SettingNode>();
                    SettingNode.windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 100);
                    SettingNode.windowTitle = "Settings";
                    windows.Add(SettingNode);

                    break;
                case UserActions.deleteNode:
                    if (selectedNode != null)
                    {
                        windows.Remove(selectedNode);
                    }

                    break;
            }
        }

        #endregion

        #region Helper Methods

        public static void DrawNodeCurve(Rect start, Rect end, bool left, Color color)
        {
            if (color == null) color = Color.black;
            Vector3 startPos = new Vector3(
                (left) ? start.x + start.width : start.x,
                start.y + (start.height *.5f),
                0);
            Vector3 endPos = new Vector3(end.x + (end.width * .5f), end.y + (end.height * .5f), 0);
            Vector3 startTan = startPos + Vector3.right * 50;
            Vector3 endTan = endPos + Vector3.left * 50;


            Color shadow = new Color(0, 0, 0, 0.06f);
            for (int i = 0; i < 3; i++)
            {
                Handles.DrawBezier(startPos, endPos, startTan, endTan, shadow, null, (i+1)*.5f);
            }
            Handles.DrawBezier(startPos, endPos, startTan, endTan, color, null, 3);
        }
        public static void LookForQuestNodeAndDrawCurve(Rect host, Quests q, Color color)
        {
            if (q != null)
            {
                List<QuestNode> find = new List<QuestNode>();
                foreach (QuestNode qNode in windows)
                {
                    if (qNode.quest == q)
                    {
                        find.Add(qNode);
                        DrawNodeCurve(host, qNode.windowRect, false, color);
                    }
                }
            }

        }
        public static void LookForQuestNodeAndDrawCurve(Rect host, List<Quests> qListe, Color color){
            foreach (Quests q in qListe)
                QuestEditor.LookForQuestNodeAndDrawCurve(host, q, color);}
        public static void MajCurve()
        {
            foreach (QuestNode qn in windows)
            {

                qn.majCurve = true;
            }
        }

        #endregion
    }
}

