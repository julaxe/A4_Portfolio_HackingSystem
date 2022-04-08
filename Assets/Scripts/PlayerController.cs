using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GridOfTiles _gridRef;

    private void Start()
    {
        _gridRef = FindObjectOfType<GridOfTiles>();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))//up
        {
            _gridRef.MoveSelectorVertical(-1);
        }
        if (Input.GetKeyDown(KeyCode.S))//down
        {
            _gridRef.MoveSelectorVertical(1);
        }
        if (Input.GetKeyDown(KeyCode.D))//right
        {
            _gridRef.MoveSelectorHorizontal(1);
        }
        if (Input.GetKeyDown(KeyCode.A))//left
        {
            _gridRef.MoveSelectorHorizontal(-1);
        }
    }
    void Update()
    {
        HandleInput();
    }
}
