using SA.QuestEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.QuestEditor
{
    public class CommentNode : BaseNode
    {
        string comment = "comment";
        public override void DrawWidow()
        {
            comment = GUILayout.TextArea(comment, 200);
        }

    }
}

