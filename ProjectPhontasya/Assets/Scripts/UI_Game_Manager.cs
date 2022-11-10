using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum UIGameState { baseUI, options}
public class UI_Game_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _baseUI, _optionsUI;
    [SerializeField] private FixedButton _optionsButton;

    private UIGameState UIstate;

    // Start is called before the first frame update
    void Start()
    {
        UIstate = UIGameState.baseUI;
    }

    // Update is called once per frame
    void Update()
    {
        switch (UIstate)
        {
            case UIGameState.baseUI:
                BaseUI();
                break;

            case UIGameState.options:
                Options();
                break;
        }
    }

    void BaseUI()
    {
        if (_optionsButton.Pressed)
        {
            _optionsButton.Pressed = false;
            Options_Manager.isActive = true;
            UIstate = UIGameState.options;
            _baseUI.SetActive(false);
            _optionsUI.SetActive(true);
        }
    }

    void Options()
    {
        if (!Options_Manager.isActive)
        {
            UIstate = UIGameState.baseUI;
            _baseUI.SetActive(true);
            _optionsUI.SetActive(false);
        }
    }
}
