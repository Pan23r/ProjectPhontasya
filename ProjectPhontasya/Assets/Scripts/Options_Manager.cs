using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Options_Manager : MonoBehaviour
{
    [SerializeField] private FixedButton _leftArrowGraphicButton, _rightArrowGraphicButton, _exitButton, _buttonInvertYCam;
    [SerializeField] private GameObject _volumePosition, _volumeBarLenght, _cameraVelPosition, _cameraVelLenght, objectsToScroll, _checkMarkInvertYCam;
    [SerializeField] private TextMeshProUGUI _graphicText;
    [SerializeField] private FixedTouchField _scrollPanel;

    public static bool isActive = false;
    
    private float _widthVolumeBar, _widthCameraVelBar;
    private FixedTouchField _volumeTouch, _cameraVelTouch;
    private Vector2[] _graphicArray = new Vector2[3];
    public static int currentGraphicSet = 2;

    private float _MaxVolume = 1;
    private float _MaxCameraVelocity = 50;

    private float _optionsStartPosY = 1080;
    private float _optionsMaxPosY;

    private float _timerForScroll = 0f;
    private float _maxTimerforScroll = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        _volumeTouch = _volumePosition.GetComponent<FixedTouchField>();
        _cameraVelTouch = _cameraVelPosition.GetComponent<FixedTouchField>();
        _widthVolumeBar = _volumeBarLenght.GetComponent<RectTransform>().rect.width;
        _widthCameraVelBar = _cameraVelLenght.GetComponent<RectTransform>().rect.width;

        _volumePosition.transform.localPosition = new Vector3(((_widthVolumeBar * FMOD_Sound_Manager.volume) / _MaxVolume) + _volumeBarLenght.transform.localPosition.x,_volumePosition.transform.localPosition.y, _volumePosition.transform.localPosition.z);

        _cameraVelPosition.transform.localPosition = new Vector3(((_widthCameraVelBar* GameplayCamera.rotationSpeed) / _MaxCameraVelocity) + _cameraVelLenght.transform.localPosition.x, _cameraVelPosition.transform.localPosition.y, _cameraVelPosition.transform.localPosition.z);

        InitGraphicArray();
        SetGraphicResolution();

        _checkMarkInvertYCam.SetActive(!GameplayCamera.invertYTouch);
        gameObject.SetActive(false);
    }

    void SetGraphicsText()
    {
        _graphicText.text = (currentGraphicSet == 0) ? Translator_Manager.GriphicLow : (currentGraphicSet == 1) ? Translator_Manager.GriphicMedium : (currentGraphicSet == 2) ? Translator_Manager.GriphicHigh : null;
    }

    protected void SetGraphicResolution()
    { 
        Screen.SetResolution((int)_graphicArray[currentGraphicSet].x, (int)_graphicArray[currentGraphicSet].y, Screen.fullScreenMode);
        SetGraphicsText();
        //SET MENU POSITION
        _optionsStartPosY = (int)_graphicArray[currentGraphicSet].y;
        _optionsMaxPosY = _optionsStartPosY + (_optionsStartPosY * (objectsToScroll.GetComponent<RectTransform>().rect.height * 0.5f) / 1080);
        _timerForScroll = _maxTimerforScroll;
    }

    // Update is called once per frame
    void Update()
    {
        if (_leftArrowGraphicButton.Pressed)
        {
            _leftArrowGraphicButton.Pressed = false;
            currentGraphicSet = (currentGraphicSet - 1 < 0)? 2 : currentGraphicSet - 1;
            SetGraphicResolution();
        }
        else if (_rightArrowGraphicButton.Pressed)
        {
            _rightArrowGraphicButton.Pressed = false;
            currentGraphicSet = (currentGraphicSet + 1 > 2) ? 0 : currentGraphicSet + 1;
            SetGraphicResolution();
        }
        
        if (_volumeTouch.Pressed) //SET VOLUME
        {
            //WidthBar : 100 = position : x  
            float proportion = ((_volumePosition.transform.localPosition.x - _volumeBarLenght.transform.localPosition.x) * _MaxVolume) / _widthVolumeBar;
            FMOD_Sound_Manager.SetVolume(proportion);
            float returnX = (proportion >= 0 && proportion <= _MaxVolume) ? _volumeTouch.TouchDist.x : 0;
            _volumePosition.transform.localPosition += new Vector3(returnX, 0, 0);
            float checkStressX = (_volumePosition.transform.localPosition.x < _volumeBarLenght.transform.localPosition.x) ? _volumeBarLenght.transform.localPosition.x : (_volumePosition.transform.localPosition.x > _widthVolumeBar + _volumeBarLenght.transform.localPosition.x) ? _widthVolumeBar + _volumeBarLenght.transform.localPosition.x : _volumePosition.transform.localPosition.x;            
            _volumePosition.transform.localPosition = new Vector3(checkStressX, _volumePosition.transform.localPosition.y, _volumePosition.transform.localPosition.z);
        }

        if (_cameraVelTouch.Pressed) //SET CAMERA VELOCITY 
        {
            //WidthBar : 50 = position : x  widthbar*veloc / maxcamera
            float speedCameraAtPos = ((_cameraVelPosition.transform.localPosition.x - _cameraVelLenght.transform.localPosition.x) * _MaxCameraVelocity) / _widthCameraVelBar;
            GameplayCamera.rotationSpeed = speedCameraAtPos;
            float returnX = (speedCameraAtPos >= 0 && speedCameraAtPos <= _MaxCameraVelocity) ? _cameraVelTouch.TouchDist.x : 0;
            _cameraVelPosition.transform.localPosition += new Vector3(returnX, 0, 0);
            float checkStressX = (_cameraVelPosition.transform.localPosition.x < _cameraVelLenght.transform.localPosition.x) ? _cameraVelLenght.transform.localPosition.x : (_cameraVelPosition.transform.localPosition.x > _widthCameraVelBar + _cameraVelLenght.transform.localPosition.x) ? _widthCameraVelBar + _cameraVelLenght.transform.localPosition.x : _cameraVelPosition.transform.localPosition.x;
            _cameraVelPosition.transform.localPosition = new Vector3(checkStressX, _cameraVelPosition.transform.localPosition.y, _cameraVelPosition.transform.localPosition.z);
        }

        if (_buttonInvertYCam.Pressed)
        {
            _buttonInvertYCam.Pressed = false;
            GameplayCamera.invertYTouch = !GameplayCamera.invertYTouch;
            _checkMarkInvertYCam.SetActive(!GameplayCamera.invertYTouch);
        }
        
        if (_exitButton.Pressed)
        {
            _exitButton.Pressed = false;
            objectsToScroll.transform.position = new Vector2(objectsToScroll.transform.position.x, _optionsStartPosY);
            isActive = false;
            SaveOptions_Manager.OverrideSaveOptions();
            gameObject.SetActive(false);
        }

        //NON MINORE DI 1080 E NON MAGGIORE DI 1080+width(1500) = 2580
        if (_timerForScroll < 0)
        {
            objectsToScroll.transform.position = (objectsToScroll.transform.position.y >= _optionsStartPosY && objectsToScroll.transform.position.y <= _optionsMaxPosY) ?
                new Vector3(objectsToScroll.transform.position.x, objectsToScroll.transform.position.y + _scrollPanel.TouchDist.y) :
                (objectsToScroll.transform.position.y < _optionsStartPosY) ? new Vector3(objectsToScroll.transform.position.x, _optionsStartPosY) :
                (objectsToScroll.transform.position.y > _optionsMaxPosY) ? new Vector3(objectsToScroll.transform.position.x, _optionsMaxPosY) :
                objectsToScroll.transform.position;
        }

        _timerForScroll -= Time.deltaTime;
    }

    void InitGraphicArray()
    {
        _graphicArray[0] = new Vector2(640,360);
        _graphicArray[1] = new Vector2(1280,720);
        _graphicArray[2] = new Vector2(1920,1080);
    }
       
    /*if (objectsToScroll.transform.position.y >= _optionsStartPosY && objectsToScroll.transform.position.y <= _optionsMaxPosY)
    {
        objectsToScroll.transform.position = new Vector2(objectsToScroll.transform.position.x, objectsToScroll.transform.position.y + _scrollPanel.TouchDist.y);
    }
    else if (objectsToScroll.transform.position.y < _optionsStartPosY)
    {
        objectsToScroll.transform.position = new Vector2(objectsToScroll.transform.position.x, _optionsStartPosY);
    }
    else if (objectsToScroll.transform.position.y > _optionsMaxPosY)
    {
        objectsToScroll.transform.position = new Vector2(objectsToScroll.transform.position.x, _optionsMaxPosY);
    }*/
}
