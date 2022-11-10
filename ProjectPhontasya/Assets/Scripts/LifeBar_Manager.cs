using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar_Manager : MonoBehaviour
{
    public static int playerLife = 100;

    private RectTransform _myTransform;
    private float _originalMyWidth;

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = GetComponent<RectTransform>();
        _originalMyWidth = _myTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        //100 : width = attualLife : X;
        _myTransform.sizeDelta = new Vector2((_originalMyWidth * playerLife)/100, _myTransform.rect.height);
    }
}
