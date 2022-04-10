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
    public GameObject WinningScreen;
    public GameObject LosingScreen;
    
    private GridOfTiles _gridRef;
    private HackingBuffer _bufferRef;
    private HackingSequence _sequenceRef;
    private HackingTimer _timerRef;
    private bool _gameStarted = false;
    private bool _gameFinished = false;

    private void Start()
    {
        _gridRef = FindObjectOfType<GridOfTiles>();
        _bufferRef = FindObjectOfType<HackingBuffer>();
        _sequenceRef = FindObjectOfType<HackingSequence>();
        _timerRef = FindObjectOfType<HackingTimer>();
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
            if (!_gameStarted)
            {
                _gameStarted = true;
                _timerRef.StartTimer();
            }
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
        if (_gameFinished) return;
        HandleInput();
        if (_bufferRef.IsBufferFull())
        {
            if (IsSequenceInBuffer())
            {
                WinningScreen.SetActive(true);
            }
            else
            {
                LosingScreen.SetActive(true);
            }
            _timerRef.PauseTimer();
            _gameFinished = true;
        }

        if (_timerRef.TimerDone())
        {
            LosingScreen.SetActive(true);
            _timerRef.PauseTimer();
            _gameFinished = true;
        }
    }

    private bool IsSequenceInBuffer()
    {
        var sequence = _sequenceRef.GetSequenceArray();
        var buffer = _bufferRef.GetBufferArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            if (buffer[i].Code == sequence[0].Code)
            {
                bool founded = true;
                for (int k = 1; k <= 2; k++)
                {
                    if (i+k >= buffer.Length)
                    {
                        return false;
                    }
                    if (buffer[i+k].Code != sequence[k].Code)
                    {
                        founded = false;
                        break;
                    }
                }

                if (founded) return true;
            }
        }

        return false;
    }

    public bool GameStarted() => _gameStarted;

    private void ResetGame()
    {
        _gameStarted = false;
        _gridRef.ResetGrid();
        _sequenceRef.ResetSequence();
        _bufferRef.ResetBuffer();
        _timerRef.ResetTimer();
    }


    public void RestartButton()
    {
        ResetGame();
        WinningScreen.SetActive(false);
        LosingScreen.SetActive(false);
        _gameFinished = false;
    }
    public void EasyDifficulty()
    {
        difficulty = GameDifficulty.Easy;
        ResetGame();
    }
    public void NormalDifficulty()
    {
        difficulty = GameDifficulty.Normal;
        ResetGame();
    }
    public void HardDifficulty()
    {
        difficulty = GameDifficulty.Hard;
        ResetGame();
    }
}
