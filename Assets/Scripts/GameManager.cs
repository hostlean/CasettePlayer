using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
                Debug.LogError("Game Manager is NULL.");
            return _instance;
        }
    }

    private float _timer;
    private int _seconds;
    [SerializeField] private GameObject _holder;
    [SerializeField] private GameObject _startButton;
    Rigidbody2D _rbHolder;

    private void Awake()
    {
        _instance = this;
        Camera.main.GetComponent<AudioSource>().Stop();
        Time.timeScale = 0;
    }

    void Start()
    {
        _rbHolder = _holder.GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if(_timer > 1.0f)
        {
            _timer -= 1.0f;
            _seconds += 1;
            //Debug.Log($"It has been {_seconds} seconds.");
        }

        if(_seconds == 5)
            _rbHolder.bodyType = RigidbodyType2D.Static;

    }

    public void StartGame()
    {
        Time.timeScale = 1;
        Camera.main.GetComponent<AudioSource>().Play();
        _startButton.SetActive(false);
    }

    public int GetSeconds()
    {
        return _seconds;
    }

}
