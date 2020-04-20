using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndMission : MonoBehaviour
{
    public QuestManager Quest_manager;

    public TextMeshProUGUI MissionName;
    public TextMeshProUGUI SendFromPlayer;
    public TextMeshProUGUI IsWin;
    public TextMeshProUGUI IsDead;
    public TextMeshProUGUI Expected;
    public TextMeshProUGUI NotExpected;

    private void reset_values()
    {
        SendFromPlayer.text = "";
        Expected.text = "";
        NotExpected.text = "";
    }
    public void UpdateRecap()
    {
        reset_values();
        MissionName.text = Quest_manager.activeQuest.QuestName;
        foreach (var item in Quest_manager.activeQuest.PlayerAnswer)
        {
            SendFromPlayer.text += item.objectName + "\n";
        }
        if (Quest_manager.activeQuest.IsWin)
        {
            IsWin.text = "YES";
            IsDead.text = "NO";
        }
        else
        {
            IsWin.text = "NO";
            if (Quest_manager.activeQuest.IsDead)
            {
                IsDead.text = "YES";
            }
            else
            {
                IsDead.text = "NO";
            }
        }
        foreach (var item in Quest_manager.activeQuest.ToAnswer)
        {
            Expected.text += item.objectName + "\n";
        }
        foreach (var item in Quest_manager.activeQuest.ToNotAnswer)
        {
            NotExpected.text += item.objectName + "\n";
        }
    }
}
