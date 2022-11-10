using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

enum GameMenuEnum {preview, selectGame, loadMenu, optionsMenu}
public class GameMenu_Manager : MonoBehaviour
{
    [SerializeField] private FixedButton _panelContinueButton, _newGameButton, _loadButton, _optionButton;
    [SerializeField] private Camera _menuCamera;
    [SerializeField] private GameObject _optionsMenuObj, _titleObj, _continueText;

    private float _speed = 2f;
    private float _timerPressContinue = 0f;

    GameMenuEnum GamemenuCounter;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreenMode);
        GamemenuCounter = GameMenuEnum.preview;
        //newGameButton.gameObject.SetActive(false);
        //loadButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (GamemenuCounter)
        {
            case GameMenuEnum.preview:
                Preview();
                break;

            case GameMenuEnum.selectGame:
                SelectGame();
                break;

            case GameMenuEnum.optionsMenu:
                OptionsMenu();
                break;
        }

        _menuCamera.transform.rotation = Quaternion.Euler(0, _menuCamera.transform.eulerAngles.y + (_speed * Time.deltaTime), 0f);
    }

    void Preview()
    {

        if(_titleObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            _panelContinueButton.gameObject.SetActive(true);
        }
        
        if (_panelContinueButton.Pressed)
        {
            _panelContinueButton.gameObject.SetActive(false);
            _newGameButton.gameObject.SetActive(true);
            _loadButton.gameObject.SetActive(true);
            _optionButton.gameObject.SetActive(true);
            GamemenuCounter = GameMenuEnum.selectGame;
        }

        if (_timerPressContinue >= 0.5f)
        {
            _continueText.SetActive(!_continueText.activeSelf);
            _timerPressContinue = 0;
        }

        _timerPressContinue += Time.deltaTime;
    }

    void SelectGame() //Menu principale base
    {
        if (_newGameButton.Pressed)
        {
            SceneManager.LoadScene("ChoisePlayer");
        }

        if (_loadButton.Pressed)
        {
            _loadButton.Pressed = false;
            Debug.Log("LOAD MENU");
        }

        if (_optionButton.Pressed)
        {
            _optionButton.Pressed = false;
            Options_Manager.isActive = true;
            GamemenuCounter = GameMenuEnum.optionsMenu;
            GameMenuSetActive(false);
            _optionsMenuObj.SetActive(true);
        }
    }

    void OptionsMenu() //Quando il menu opzioni è aperto
    {
        if (!Options_Manager.isActive)
        {
            GamemenuCounter = GameMenuEnum.selectGame;
            GameMenuSetActive(true);
            _titleObj.GetComponent<Animator>().enabled = false;
        }
    }

    void GameMenuSetActive(bool setActive) //imposta quando il game menu si attiva o disattiva (es apertura optzioni)
    {
        _newGameButton.gameObject.SetActive(setActive);
        _loadButton.gameObject.SetActive(setActive);
        _optionButton.gameObject.SetActive(setActive);
        _titleObj.SetActive(setActive);
    }
}
