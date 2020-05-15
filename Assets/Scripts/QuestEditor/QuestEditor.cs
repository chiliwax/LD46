using System.Collections;
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
        bool clickedOnWindow;
        BaseNode selectedNode;
        bool settingOpen;
        //color
        public static GUIStyle buttonStyleGreen;
        public static GUIStyle buttonStyleGreenL;
        public static GUIStyle buttonStyleGrey;
        public static GUIStyle buttonStyleRed;
        public static GUIStyle buttonStyleRedL;
        #endregion

        public enum UserActions
        {
            addQuest, OptionNode, deleteNode, openSettings
        }

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

            //color
            buttonStyleGreen = new GUIStyle(GUI.skin.button);
            buttonStyleGreen.normal.textColor = Color.green;
            buttonStyleGreenL = new GUIStyle(GUI.skin.button);
            buttonStyleGreenL.normal.textColor = new Color(0, .7f, 0, 1);
            buttonStyleGrey = new GUIStyle(GUI.skin.button);
            buttonStyleGrey.normal.textColor = new Color(.3f, 0, 0, 1);
            buttonStyleRed = new GUIStyle(GUI.skin.button);
            buttonStyleRed.normal.textColor = Color.red;
            buttonStyleRedL = new GUIStyle(GUI.skin.button);
            buttonStyleRedL.normal.textColor = new Color(.7f, 0, 0, 1);
        }

        private void OnEnable() //clear editor
        {
            //windows.Clear();
            foreach (BaseNode q in Resources.FindObjectsOfTypeAll(typeof(BaseNode)))
            {
                if (!windows.Contains(q))
                DestroyImmediate(q);
            }
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
            if(e.button == 1)
            {
                if(e.type == EventType.MouseDown)
                {
                    RightClick(e);
                }
            }

            if (e.button == 0)
            {
                if (e.type == EventType.MouseDown)
                {
                    
                }
            }
        }

        void RightClick(Event e)
        {
            selectedNode = null;
            clickedOnWindow = false;
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
                    Quests empty = null;
                    AddExistingQuest(empty, mousePosition.x, mousePosition.y);
                    DestroyImmediate(empty);


                    break;
                case UserActions.OptionNode:
                    break;
                case UserActions.openSettings:

                    SettingNode settingNode = ScriptableObject.CreateInstance<SettingNode>();
                    settingNode.windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 110);
                    settingNode.windowTitle = "Quest Editor";
                    windows.Add(settingNode);
                    OptionNode optionNode2 = ScriptableObject.CreateInstance<OptionNode>();
                    optionNode2.windowRect = new Rect(mousePosition.x, mousePosition.y - 50, 60, 50);
                    optionNode2.windowTitle = "Setting";
                    optionNode2.nodeLink = settingNode;
                    windows.Add(optionNode2);

                    break;
                case UserActions.deleteNode:
                    if (selectedNode != null)
                    {
                        windows.Remove(selectedNode);
                        DestroyImmediate(selectedNode);
                    }

                    break;
            }
        }

        #endregion

        #region Helper Draw curveMethods

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
        public static void LookForQuestNodeAndDrawCurve(QuestNode host, Quests q, Color color)
        {
            if (q != null)
            {
                List<QuestNode> find = new List<QuestNode>();
                bool findNode = false;
                foreach (QuestNode qNode in Resources.FindObjectsOfTypeAll(typeof(QuestNode)))
                {
                    if (qNode.quest == q)
                    {
                        findNode = true;
                        float positionCurve = (.5f  + (color.r - color.g)/4);
                        #region position depart curve
                        Rect recthost = host.windowRect;
                        recthost.y += host.windowRect.height * positionCurve;
                        recthost.x += host.windowRect.width;
                        recthost.width = 1;
                        recthost.height = 1;
                        #endregion
                        #region position arrivé curve
                        find.Add(qNode);
                        Rect rect = qNode.windowRect;
                        rect.y += qNode.windowRect.height * positionCurve;
                        rect.width = 1;
                        rect.height = 1;
                        #endregion
                        #region decalage depart/arrivé curve en fonction de la couleur

                        #endregion
                        DrawNodeCurve(recthost, rect, false, color);
                    }
                    if (!findNode)
                    {
                        float positionCurve = (.5f + (color.r - color.g) / 4);
                        #region position depart curve
                        Rect recthost = host.windowRect;
                        recthost.y += host.windowRect.height * positionCurve;
                        recthost.x += host.windowRect.width;
                        recthost.width = 1;
                        recthost.height = 1;
                        #endregion
                        #region position arrivé curve
                        Rect rect = new Rect
                            (recthost.x+5, recthost.y, recthost.width, recthost.height);
                        #endregion

                        DrawNodeCurve(recthost, rect, false, color);
                    }
                }
            }

        }
        public static void LookForQuestNodeAndDrawCurve(QuestNode host, List<Quests> qListe, Color color){
            foreach (Quests q in qListe)
                QuestEditor.LookForQuestNodeAndDrawCurve(host, q, color);}

        #endregion
        #region Helper add Questnode
        public static void AddExistingQuest(Quests q, float x, float y)
        {
            QuestNode questNode = ScriptableObject.CreateInstance<QuestNode>();
            questNode.windowRect = new Rect(x, y, 200, 225);
            questNode.windowTitle = "Empty";
            windows.Add(questNode);
            questNode.quest = q;
            if (q != null) EditorUtility.SetDirty(q); //IMPORTANT, sinon les modification ne sont pas sauvegardé
            OptionNode optionNode = ScriptableObject.CreateInstance<OptionNode>();
            optionNode.windowRect = new Rect(x, y - 50, 60, 50);
            optionNode.windowTitle = "Quest";
            optionNode.nodeLink = questNode;
            windows.Add(optionNode);
        }
        public static void AddExistingQuest(Campaign campaign, float x, float y)
        {
            foreach (Quests q in Resources.LoadAll<Quests>(""))
            {
                string camp = "";
                if (q.campagne != null) camp = q.campagne.name;
                Debug.Log(q.name+" / "+camp);
                if (q.campagne != null)
                {
                    if (q.campagne == campaign)
                    {
                        AddExistingQuest(q, x, y);
                    }
                }
                
            }
        }
        #endregion
    }
}

