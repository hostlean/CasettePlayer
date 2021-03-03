using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _frameArray;
    private int _currentFrame;
    private float _timer;
    [SerializeField]
    private float _frameRatePerSec = .1f;
    private float _newFrame;
    [SerializeField]
    private bool _loop = true;
    [SerializeField]
    private bool _destroyOnLoop = false;
    private SpriteRenderer  _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _newFrame = 100 / (_frameRatePerSec * 100);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer >= _newFrame)
        {
            _timer -= _newFrame;

            if (_loop)
                _currentFrame = (_currentFrame + 1) % _frameArray.Length;

            _spriteRenderer.sprite = _frameArray[_currentFrame];


            if (_destroyOnLoop == true)
            {
                if (_currentFrame + 1 >= _frameArray.Length)
                {
                    Destroy(this.gameObject);
                }                
            }
            else return;
               
        }
    }

}
