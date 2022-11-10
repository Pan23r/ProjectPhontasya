using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyJoystick;

public class Player_Manager : MonoBehaviour
{
    [SerializeField] private Camera _gameplayCamera;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed;
    
    private float _speedRun;

    public float yPosition;
    private Animator _myAnim;
    private Rigidbody _myrigidBody;
    private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        if (ChoosePlayer_Manager.selectPlayerFamale)
            GameObject.Find("MalePlayer").SetActive(false);
        else
            GameObject.Find("FamalePlayer").SetActive(false);

        _myAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        _myrigidBody = GetComponent<Rigidbody>();
        _myrigidBody.freezeRotation = true;
        _speedRun = _speed * 3;
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = _joystick.Horizontal();
        float zMovement = _joystick.Vertical();

        Vector3 direction = new Vector3(xMovement, 0, zMovement).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _gameplayCamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            _speed = (xMovement >= 0.6f || zMovement >= 0.6f || xMovement <= -0.6f || zMovement <= -0.6f) ? _speedRun : _speedRun * 0.3f;
           
            _myAnim.SetFloat("SpeedAnim", _speed);
            transform.position += moveDir.normalized * _speed * Time.deltaTime; 
        }
        else
        {
            _myAnim.SetFloat("SpeedAnim", 0);
        }
    }
}
