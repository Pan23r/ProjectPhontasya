using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayCamera : MonoBehaviour
{
    [SerializeField] private FixedTouchField _touchField;

    public Transform playerTransform;
    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float smootFactcor = 0.5f;

    public static float rotationSpeed = 20f; //MAX 50 (cambiare in Options_Manager "_MaxCameraVelocity")
    public static bool invertYTouch = true;
    private Quaternion _camTurnAngleX, _camTurnAngleY;
    private const float _YviewChange = 1.3f;
    
    private float _minPosCamera = 0.5f;
    private float _maxPosCamera = 1.7f;

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - playerTransform.position;
    }

    private Vector3 ForwardDir(bool positiveForward)
    {
        return (positiveForward) ? Vector3.forward : -Vector3.forward; 
    }

    private Vector3 LeftDir(bool positiveLeft)
    {
        return (positiveLeft) ? Vector3.left : -Vector3.left;
    }

    private void LateUpdate()
    {
        float touchY = _touchField.TouchDist.y;
        _camTurnAngleX = Quaternion.AngleAxis(_touchField.TouchDist.x * Time.deltaTime * rotationSpeed, Vector3.up);

        _camTurnAngleY = (transform.rotation.eulerAngles.y >= 240 && transform.rotation.eulerAngles.y < 300)? 
            Quaternion.AngleAxis(touchY * Time.deltaTime * rotationSpeed, ForwardDir(!invertYTouch)) :
            (transform.rotation.eulerAngles.y >= 60 && transform.rotation.eulerAngles.y < 120)? 
            Quaternion.AngleAxis(touchY * Time.deltaTime * rotationSpeed, ForwardDir(invertYTouch)):
            (transform.rotation.eulerAngles.y < 60 || transform.rotation.eulerAngles.y >= 300) ?
            Quaternion.AngleAxis(touchY * Time.deltaTime * rotationSpeed, LeftDir(invertYTouch)) :
            Quaternion.AngleAxis(touchY * Time.deltaTime * rotationSpeed, LeftDir(!invertYTouch));

        Vector3 _offesetWithY = _camTurnAngleX * _camTurnAngleY * _cameraOffset;
        Vector3 _offesetOnlyX = _camTurnAngleX * _cameraOffset;
        
        if(!invertYTouch)
            _cameraOffset = (_cameraOffset.y < _minPosCamera) ? ((touchY < 0) ? _offesetOnlyX : _offesetWithY) : (_cameraOffset.y > _maxPosCamera) ? ((touchY > 0) ? _offesetOnlyX : _offesetWithY) : _offesetWithY;
        else
            _cameraOffset = (_cameraOffset.y < _minPosCamera) ? ((touchY > 0) ? _offesetOnlyX : _offesetWithY) : (_cameraOffset.y > _maxPosCamera) ? ((touchY < 0) ? _offesetOnlyX : _offesetWithY) : _offesetWithY;

        Vector3 newPos = playerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, smootFactcor);
        transform.LookAt(new Vector3(playerTransform.position.x, playerTransform.position.y + _YviewChange, playerTransform.position.z));
    }

    void OnlyXCamera()
    {
        _camTurnAngleX = Quaternion.AngleAxis(_touchField.TouchDist.x * rotationSpeed, Vector3.up);

        _cameraOffset = _camTurnAngleX * _cameraOffset;

        Vector3 newPos = playerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, smootFactcor);
        transform.LookAt(new Vector3(playerTransform.position.x, playerTransform.position.y + _YviewChange, playerTransform.position.z));

        //Debug.Log(transform.eulerAngles);   
    }

    // OLD CHANGE FORWARD-LEFT SYSTEM

    /*if (transform.rotation.eulerAngles.y >= 240 && transform.rotation.eulerAngles.y < 300)
        _camTurnAngleY = Quaternion.AngleAxis(touchY * Time.deltaTime * rotationSpeed, ForwardDir(!invertYTouch));
    else if(transform.rotation.eulerAngles.y >= 60 && transform.rotation.eulerAngles.y < 120)
        _camTurnAngleY = Quaternion.AngleAxis(touchY * Time.deltaTime * rotationSpeed, ForwardDir(invertYTouch));
    else if(transform.rotation.eulerAngles.y < 60 || transform.rotation.eulerAngles.y >= 300)
        _camTurnAngleY = Quaternion.AngleAxis(touchY * Time.deltaTime * rotationSpeed, LeftDir(invertYTouch));
    else
        _camTurnAngleY = Quaternion.AngleAxis(touchY * Time.deltaTime * rotationSpeed, LeftDir(!invertYTouch));*/

}
