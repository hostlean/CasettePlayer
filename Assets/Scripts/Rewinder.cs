using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewinder : MonoBehaviour
{
    private static Rewinder _instance;

    public static Rewinder Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Rewinder is NULL");
            return _instance;
        }
    }

    [SerializeField] GameObject _camera;

    AudioSource audioSource;

    public bool isRewinding = false;

    public float rewindLimit = 6.0f;

    public bool _canRewind = false;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        audioSource = _camera.GetComponent<AudioSource>();
    }


    void Update()
    {
        RewindLimit();

        if(_canRewind)
        {
            if(Input.GetKey(KeyCode.Q))
                StartRewind();
            if(Input.GetKeyUp(KeyCode.Q))
                StopRewind();
        }
        else
            StopRewind();

    }

    private void RewindLimit()
    {
        if(rewindLimit < 0.0)
            rewindLimit = 0.0f;
        if(rewindLimit < 5.0f && !isRewinding)
            rewindLimit += Time.deltaTime;
        if(rewindLimit > 5.0f)
            rewindLimit = 5.0f;
        if(rewindLimit <= 5 && rewindLimit > 0)
            _canRewind = true;
        else _canRewind = false;
    }

    private void StartRewind()
    {
        isRewinding = true;
        audioSource.pitch = -1;
        rewindLimit -= Time.deltaTime;
        
    }

    private void StopRewind()
    {
        isRewinding = false;
        audioSource.pitch = 1;
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

}
