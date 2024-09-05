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
    }
}