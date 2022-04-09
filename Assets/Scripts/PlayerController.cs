using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum GameDifficulty
    {
        Easy,
        Normal,
        Hard
    }

    public GameDifficulty difficulty = GameDifficulty.Easy;
    
    private GridOfTiles _gridRef;
    private HackingBuffer _bufferRef;
    private HackingSequence _sequenceRef;

    private void Start()
    {
        _gridRef = FindObjectOfType<GridOfTiles>();
        _bufferRef = FindObjectOfType<HackingBuffer>();
        _sequenceRef = FindObjectOfType<HackingSequence>();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_gridRef.CurrentIsInBuffer())
            {
                //Get code back from buffer,
                _gridRef.GetCodeBackFromBuffer(_bufferRef.PullCodeFromBuffer());
            }
            else
            {
                _bufferRef.PushCodeToBuffer(_gridRef.MoveCurrentCodeToBuffer());
            }
            _gridRef.SwitchBetweenRowAndColumn();
        }
    }
    void Update()
    {
        HandleInput();
    }
}
