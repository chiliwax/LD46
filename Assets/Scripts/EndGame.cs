using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public QuestManager Quest_manager;

    public GameObject winScreen;
    public GameObject looseScreen;

    public TextMeshProUGUI Win;
    public TextMeshProUGUI deadPeople;
    public TextMeshProUGUI complet_mission;
    public TextMeshProUGUI Win_mission;

    public void UpdateRecap()
    {
        int win = 0;
        int loose = 0;
        int done = 0;
        int death = 0;

        foreach (var item in Quest_manager.quests)
        {
            if (item.IsWin) {win += 1;}else {loose += 1;}
        }
        foreach (var item in Quest_manager.quests)
        {
            if (item.end) {done +=1;}
        }
        foreach (var item in Quest_manager.quests)
        {
            if (item.IsDead) {death +=1;}
        }

        complet_mission.text = done.ToString();
        Win_mission.text = win.ToString();
        deadPeople.text = death.ToString();

        if (death >= 5 || loose >= win) {
            Win.text = "GameOver !";
            looseScreen.SetActive(true);
        } else {
            Win.text = "Congretulation !";
            winScreen.SetActive(true);
        }
    }

    public void gotohome() {
        SceneManager.LoadScene("Splash_screen");
    }
}
