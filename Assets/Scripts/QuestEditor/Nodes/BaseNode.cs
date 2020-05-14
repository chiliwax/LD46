using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.QuestEditor
{
    public abstract class BaseNode : ScriptableObject
    {
        public Rect windowRect;
        public string windowTitle;
        public bool compactOptionNode = false; //GUI
        public virtual void DrawWidow()
        {

        }
        public virtual void DrawCurve()
        {

        }

    }
}

