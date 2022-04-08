using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridOfTiles : MonoBehaviour
{
    public int size = 5;
    public float spaceBetweenTiles = 1.0f;
    public Tile[,] grid;

    private string[] _poolOfCodes;
    private GameObject _tilePrefab;
    private int _currentRow = 0;
    private int _currentColumn = 0;
    private bool _isRowActive = true;
    void Start()
    {
        transform.position += new Vector3(-size * 0.5f, size * 0.5f);
        _tilePrefab = Resources.Load<GameObject>("Prefabs/Tile");
        InitializePoolOfCodes();
        InitializeGrid();
    }

    public bool IsRowActive() => _isRowActive;
    
    public void MoveSelectorVertical(int value)
    {
        if (!_isRowActive)
        {
            MoveSelector(ref _currentRow, value);
        }
    }
    public void MoveSelectorHorizontal(int value)
    {
        if (_isRowActive)
        {
            MoveSelector(ref _currentColumn, value);
        }
    }
    private void MoveSelector(ref int selector, int value)
    {
        grid[_currentColumn, _currentRow].IsSelected = false;
        selector += value;
        if (selector < 0) selector = 0;
        if (selector >= size) selector = size - 1;
        grid[_currentColumn, _currentRow].IsSelected = true;
    }
    public void ChangeCurrentColumnActiveState(bool state)
    {
        _isRowActive = !state;
        for (int y = 0; y < size; y++)
        {
            grid[_currentColumn, y].IsActive = state;
        }
    }

    public void ChangeCurrentRowActiveState(bool state)
    {
        _isRowActive = state;
        for (int x = 0; x < size; x++)
        {
            grid[x, _currentRow].IsActive = state;
        }
    }

    private void InitializeGrid()
    {
        grid = new Tile[size, size];
        
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                var temp = Instantiate(_tilePrefab, transform);
                temp.transform.position += new Vector3(x * spaceBetweenTiles, y * -spaceBetweenTiles);
                grid[x, y] = temp.GetComponent<Tile>();
                grid[x, y].Code = _poolOfCodes[Random.Range(0, _poolOfCodes.Length)];
            }
        }

        grid[_currentColumn, _currentRow].IsSelected = true;
        ChangeCurrentRowActiveState(true);
    }

    private void InitializePoolOfCodes()
    {
        string pool = "ABCDEFGHI0123456789";
        _poolOfCodes = new string[size - 1];
        for (int i = 0; i < _poolOfCodes.Length; i++)
        {
            char first = pool[Random.Range(0, pool.Length)];
            char second = pool[Random.Range(0, pool.Length)];
            string code = first.ToString() + second;
            while (Array.Exists(_poolOfCodes, x => x == code))
            {
                first = pool[Random.Range(0, pool.Length)];
                second = pool[Random.Range(0, pool.Length)];
                code = first.ToString() + second;
            }
            _poolOfCodes[i] = code;
        }
    }
}
