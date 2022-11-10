using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMusicChange : MonoBehaviour
{
    [SerializeField] private int _setParameterEnter, _setParameterExit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCollider")
        {
            FMOD_Sound_Manager.GameSoundtrackParameter(_setParameterEnter);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCollider")
        {
            FMOD_Sound_Manager.GameSoundtrackParameter(_setParameterExit);
        }
    }
}
