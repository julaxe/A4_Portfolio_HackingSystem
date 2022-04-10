using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingBuffer : MonoBehaviour
{
    public float spaceBetweenTiles = 1.0f;
    
    private GameObject _tilePrefab;
    private Tile[] _bufferArray;
    private PlayerController _playerRef;
    private int _currentBuffer = 0;
    void Start()
    {
        _tilePrefab = Resources.Load<GameObject>("Prefabs/Tile");
        _playerRef = FindObjectOfType<PlayerController>();
        InitializeBuffer();
    }

    public void PushCodeToBuffer(string code)
    {
        _bufferArray[_currentBuffer].Code = code;
        _currentBuffer += 1;
    }

    public string PullCodeFromBuffer()
    {
        if (_currentBuffer == 0) return "-1"; // error
        var code = _bufferArray[_currentBuffer - 1].Code;
        _bufferArray[_currentBuffer - 1].Code = "";
        _currentBuffer -= 1;
        return code;
    }

    private void InitializeBuffer()
    {
        _currentBuffer = 0;
        if (_playerRef.difficulty == PlayerController.GameDifficulty.Hard)
        {
            // 5 slots
            CreateBufferWithSize(5);
        }
        else if (_playerRef.difficulty == PlayerController.GameDifficulty.Normal)
        {
            // 3 slots
            CreateBufferWithSize(4);
        }
        else
        {
            // 3 slots
            CreateBufferWithSize(3);
        }
    }

    private void CreateBufferWithSize(int size)
    {
        _bufferArray = new Tile[size];
        for (int i = 0; i < size; i++)
        {
            var temp = Instantiate(_tilePrefab, transform);
            temp.transform.position += Vector3.right * i * spaceBetweenTiles;
            _bufferArray[i] = temp.GetComponent<Tile>();
            _bufferArray[i].Code = "";
        }
    }

    public Tile[] GetBufferArray()
    {
        return _bufferArray;
    }

    private void DeleteBuffer()
    {
        foreach (var buffer in _bufferArray)
        {
            Destroy(buffer.gameObject);
        }
    }

    public void ResetBuffer()
    {
        DeleteBuffer();
        InitializeBuffer();
    }

    public bool IsBufferFull()
    {
        return _currentBuffer == _bufferArray.Length;
    }
}
