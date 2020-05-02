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
        static List<BaseNode> windows = new List<BaseNode>();
        Vector3 mousePosition;
        bool makeTransition;
        bool clickedOnWindow;
        BaseNode selectedNode;

        public enum UserActions
        {
            addQuest, addTransitionNode, deleteNode,addComment
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

        void AddNewNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Add Quest"), false, ContextCallback, UserActions.addQuest);
            menu.AddItem(new GUIContent("Add Comment"), false, ContextCallback, UserActions.addComment);

            menu.ShowAsContext();
            e.Use();
        }

        void ModifyNode(Event e)
        {      
            GenericMenu menu = new GenericMenu();
            if (selectedNode is QuestNode)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.deleteNode);
            }

            if (selectedNode is CommentNode)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.deleteNode);
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
                    questNode.windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 150);
                    questNode.windowTitle = "Quest";
                    windows.Add(questNode);

                    break;
                case UserActions.addTransitionNode:
                    break;
                case UserActions.addComment:
                    CommentNode commentNode = ScriptableObject.CreateInstance<CommentNode>();
                    commentNode.windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 100);
                    commentNode.windowTitle = "Comment";
                    windows.Add(commentNode);

                    break;
                default:
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
        #endregion
    }
}

