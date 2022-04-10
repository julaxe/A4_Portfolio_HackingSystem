using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingPlayerSkill : MonoBehaviour
{

    private Slider _slider;
    private HackingTimer _timerRef;
    private PlayerController _playerRef;

    private int _maximumAmountToAdd = 15;
    void Start()
    {
        _slider = GetComponent<Slider>();
        _timerRef = FindObjectOfType<HackingTimer>();
        _playerRef = FindObjectOfType<PlayerController>();
        _slider.onValueChanged.AddListener(delegate{UpdateTimer();});
    }

    public int GetPlayerSkillValueToAdd()
    {
        return (int) ((_slider.value / 100.0f) * _maximumAmountToAdd);
    }

    private void UpdateTimer()
    {
        if (_playerRef.GameStarted()) return;
        _timerRef.ChangeInitialTimer(GetPlayerSkillValueToAdd());
    }
    
    
}
