using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Cheats : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GameManager.Instance.TrySetHighScore(0, true);
            Debug.Log("<color=yellow>Cheat activated:</color> High Score reset.");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Player.Instance.IsInvincible = !Player.Instance.IsInvincible;
            Debug.Log("<color=yellow>Cheat activated:</color> Toggle player invincibility = " + Player.Instance.IsInvincible);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Player.Instance.ActivateTripleShot();
            Debug.Log("<color=yellow>Cheat activated:</color> Activate triple shot.");
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Player.Instance.ActivateSpeedBoost();
            Debug.Log("<color=yellow>Cheat activated:</color> Activate speed boost.");
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Player.Instance.ActivateShield();
            Debug.Log("<color=yellow>Cheat activated:</color> Activate player shield.");
        }
    }
}