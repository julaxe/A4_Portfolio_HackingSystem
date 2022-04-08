using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool _isActive;
    private bool _isSelected;
    private string _code;
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            UpdateColor();
        }
    }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            UpdateColor();
        }
    }

    public string Code
    {
        get => _code;
        set
        {
            _code = value;
            SetText();
        }
    }

    private SpriteRenderer _spriteRenderer;
    private Color _currentColor;
    private readonly string _hashSelectedColor = "#FF0300";
    private readonly string _hashActiveColor = "#6D6D6D";
    private readonly string _hashDefaultColor = "#000000";

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateColor()
    {
        if (IsSelected)
        {
            SetColor(_hashSelectedColor);
            return;
        }
        if (IsActive)
        {
            SetColor(_hashActiveColor);
            return;
        }
        SetColor(_hashDefaultColor);
    }

    private void SetColor(string htmlColor)
    {
        if (ColorUtility.TryParseHtmlString(htmlColor, out _currentColor))
        {
            _spriteRenderer.color = _currentColor;
        }
    }

    private void SetText()
    {
        transform.Find("Code").GetComponent<TMP_Text>().text = _code;
    }
}
