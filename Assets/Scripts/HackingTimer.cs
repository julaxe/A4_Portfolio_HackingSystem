using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HackingTimer : MonoBehaviour
{

    public int initialValue = 30;
    private int _timer;

    private int Timer
    {
        get => _timer;
        set
        {
            _timer = value;
            UpdateText();
        }
    }
    private TextMeshProUGUI _text;
    private bool _timerDone = false;
    private bool _timerStarted = false;
    private float _timerHelper;
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        Timer = initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_timerStarted) return;
        if (_timerHelper >= 1.0f) //1 second has passed
        {
            _timerHelper = 0.0f;
            Timer -= 1;
            if (Timer <= 0.0f)
            {
                _timerDone = true;
            }
        }

        _timerHelper += Time.deltaTime;
    }

    void UpdateText()
    {
        _text.text = _timer.ToString();
    }

    public bool TimerDone() => _timerDone;

    public void StartTimer()
    {
        _timerStarted = true;
    }

    public void ChangeInitialTimer(int value)
    {
        Timer = initialValue + value;
    }
}
