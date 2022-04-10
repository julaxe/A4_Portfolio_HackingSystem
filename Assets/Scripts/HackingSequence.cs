using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class HackingSequence : MonoBehaviour
{
    public float spaceBetweenTiles = 1.0f;
    
    private GameObject _tilePrefab;
    private GridOfTiles _gridRef;
    private PlayerController _playerRef;
    private int _sequenceSize = 3;
    
    private Tile[] _sequenceArray;
    private List<int2> _positionsChosen;
    
    void Start()
    {
        _tilePrefab = Resources.Load<GameObject>("Prefabs/Tile");
        _gridRef = FindObjectOfType<GridOfTiles>();
        _playerRef = FindObjectOfType<PlayerController>();

        InitializeSequence();
    }

    private void InitializeSequence()
    {
        var grid = _gridRef.grid;
        var size = _gridRef.size;
        _positionsChosen = new List<int2>();
        int currentRow = 0;
        int currentColumn = Random.Range(0, size);
        _positionsChosen.Add(new int2(currentColumn, currentRow));
        GetNextSequence(ref currentColumn, ref currentRow, false, ref _positionsChosen, size);
        GetNextSequence(ref currentColumn, ref currentRow, true, ref _positionsChosen, size);
        GetNextSequence(ref currentColumn, ref currentRow, false, ref _positionsChosen, size);
        GetNextSequence(ref currentColumn, ref currentRow, true, ref _positionsChosen, size);

        CreateSequence();

    }

    private void GetNextSequence(ref int currentColumn, ref int currentRow, bool isRow, ref List<int2> positions, int size)
    {
        if (isRow)
        {
            currentColumn = Random.Range(0, size);
            var newPos = new int2(currentColumn, currentRow);
            while (positions.Exists(x => x.Equals(newPos)))
            {
                currentColumn = Random.Range(0, size);
                newPos = new int2(currentColumn, currentRow);
            }
            positions.Add(newPos);
        }
        else
        {
            currentRow = Random.Range(0, size);
            var newPos = new int2(currentColumn, currentRow);
            while (positions.Exists(x => x.Equals(newPos)))
            {
                currentRow = Random.Range(0, size);
                newPos = new int2(currentColumn, currentRow);
            }
            positions.Add(newPos);
        }
    }

    private void CreateSequence()
    {
        _sequenceArray = new Tile[_sequenceSize];
        for (int i = 0; i < _sequenceSize; i++)
        {
            var temp = Instantiate(_tilePrefab, transform);
            temp.transform.position += Vector3.right * i * spaceBetweenTiles;
            _sequenceArray[i] = temp.GetComponent<Tile>();
            _sequenceArray[i].Code = "";
        }

        SetSequenceWithDifficulty();
    }

    private void SetSequenceWithDifficulty()
    {
        switch (_playerRef.difficulty)
        {
            case PlayerController.GameDifficulty.Easy:
                for (int i = 0; i < _sequenceSize; i++)
                {
                    _sequenceArray[i].Code = _gridRef.grid[_positionsChosen[i].x, _positionsChosen[i].y].Code;
                }
                break;
            case PlayerController.GameDifficulty.Normal:
                for (int i = 0; i < _sequenceSize; i++)
                {
                    _sequenceArray[i].Code = _gridRef.grid[_positionsChosen[i+1].x, _positionsChosen[i+1].y].Code;
                }
                break;
            case PlayerController.GameDifficulty.Hard:
                for (int i = 0; i < _sequenceSize; i++)
                {
                    _sequenceArray[i].Code = _gridRef.grid[_positionsChosen[i+2].x, _positionsChosen[i+2].y].Code;
                }
                break;
        }
    }

    public Tile[] GetSequenceArray()
    {
        return _sequenceArray;
    }
    private void DeleteSequence()
    {
        foreach (var sequence in _sequenceArray)
        {
            Destroy(sequence.gameObject);
        }
    }

    public void ResetSequence()
    {
        DeleteSequence();
        InitializeSequence();
    }
    
    
}
