using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject _Menu;
    private bool FlipFlop;
    void Start()
    {
        FlipFlop = false;
    }
    void Update()
    {
        if (Input.GetAxis("Menu") != 0 && FlipFlop == false)
        {
            FlipFlop = true;
            _Menu.SetActive(!_Menu.activeSelf);
        }
        if (Input.GetAxis("Menu") == 0 && FlipFlop == true)
        {
            FlipFlop = false;
        }
    }
}
