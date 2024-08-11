using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        this.GameObject().SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }
    }

    private void GoBack()
    {
        this.gameObject.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
