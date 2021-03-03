using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelTurner : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float zAngle;
    private const string LEFTSLIDER = "Slider Left";
    private const string RIGHTSLIDER = "Slider Right";
    private float _angleValue = 0.00338f;
    private float _sliderValue = 0.30508f;
    private float _timer;
    private float _seconds;

    //_sliderValue = 0.0101f * Time.deltaTime;
    //    _angleValue = 0.00011f * Time.deltaTime;

    private void Awake()
    {

    }

    private void Start()
    {
        Debug.Log(Rewinder.Instance.GetAudioSource().clip.length);
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;

        SliderMovement();

        TurnWheels();

        StopWheels();
    }

    private void StopWheels()
    {
        if(Rewinder.Instance.GetAudioSource().isPlaying)
            this.transform.Rotate(0, 0, zAngle);
        else transform.Rotate(0, 0, 0);
    }

    private void SliderMovement()
    {
        if(slider.name == RIGHTSLIDER)
        {
            _sliderValue = -Mathf.Abs(_sliderValue);
            _angleValue = -Mathf.Abs(_angleValue);

        }
        if(slider.name == LEFTSLIDER)
        {
            _sliderValue = Mathf.Abs(_sliderValue);
            _angleValue = Mathf.Abs(_angleValue);
        }
    }

    private void TurnWheels()
    {
        if(_timer > 1.0f)
        {
            _timer -= 1.0f;
            _seconds += 1;
            if(Rewinder.Instance.isRewinding == false)
            {
                if(zAngle > 0)
                    zAngle *= -1;
            }
            else
            {
                if(zAngle < 0)
                    zAngle *= -1;
                _sliderValue *= -1;
                _angleValue *= -1;

            }
            slider.value += _sliderValue;
            zAngle += _angleValue;

        }
    }
}
