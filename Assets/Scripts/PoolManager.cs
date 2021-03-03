using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager _instance;
    public static PoolManager Instance
    {
        get
        {
            if(_instance == null)
                Debug.LogError("Pool Manager is NULL.");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] private GameObject[] _blockPrefabs;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private List<GameObject> _blockPool;
    [SerializeField] private List<GameObject> _coinPool;
    [SerializeField] private GameObject _blockContainer;
    [SerializeField] private GameObject _coinContainer;
    [SerializeField] private int _blockSpawnCount = 10;
    [SerializeField] private int _coinSpawnCount = 10;
    [SerializeField] private Vector3 _blockSpawnPos;
    [SerializeField] private Vector3 _blockSpawnRot;
    [SerializeField] private Vector3 _coinSpawnPos;
    [SerializeField] private Vector3 _coinSpawnRot;

    void Start()
    {
        _blockPool = GenerateBlocks();
        _coinPool = GenerateBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<GameObject> GenerateBlocks()
    {
        for(int i = 0; i < _blockPrefabs.Length; i++)
        {
            for(int b = 0; b < _blockSpawnCount; b++)
            {
                GameObject block = Instantiate(_blockPrefabs[i],
                    _blockSpawnPos,
                    Quaternion.Euler(_blockSpawnRot),
                    _blockContainer.transform);
                block.SetActive(false);
                _blockPool.Add(block);
                return _blockPool;
            }
        }
        return null;
    }

    private List<GameObject> GenerateCoins()
    {
        for(int i = 0; i < _coinSpawnCount; i++)
        {
            GameObject coin = Instantiate(_coinPrefab,
                _coinSpawnPos,
                Quaternion.Euler(_coinSpawnRot),
                _blockContainer.transform);
            coin.SetActive(false);
            _coinPool.Add(coin);
            return _coinPool;
        }
        return null;
    }




}
