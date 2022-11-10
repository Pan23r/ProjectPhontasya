using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ChoosePlayer_Manager : MonoBehaviour
{
    [SerializeField] FixedTouchField FamalePlayer, MalePlayer;

    public static bool selectPlayerFamale = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        selectPlayerFamale = FamalePlayer.Pressed? PlayerSelect(true) : selectPlayerFamale;
        selectPlayerFamale = MalePlayer.Pressed ? PlayerSelect(false) : selectPlayerFamale;
    }

    bool PlayerSelect(bool playerFamale)
    {
        FMOD_Sound_Manager.GameSoundtrackParameter(1);
        SceneManager.LoadScene("TestScene");
        return playerFamale;
    }
}
