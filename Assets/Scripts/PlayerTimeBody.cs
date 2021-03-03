using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeBody : MonoBehaviour
{
    public bool isRewinding = false;

    [SerializeField] private GameObject reverseDash;

    private List<Vector3> positions = new List<Vector3>();
    private List<Vector3> dashPos = new List<Vector3>();
    private List<Vector3> dashRot = new List<Vector3>();
    private List<float> dashTimes = new List<float>();
    private float timer;

    public float Timer { get { return timer; } }

    void Start()
    {

    }


    void Update()
    {
        if(Rewinder.Instance._canRewind)
        {
            if(Input.GetKey(KeyCode.Q))
                StartRewind();
            if(Input.GetKeyUp(KeyCode.Q))
                StopRewind();
        }
        else
            StopRewind();
    }

    private void FixedUpdate()
    {

        if(isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    private void Rewind()
    {
        if(positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
            timer -= Time.deltaTime;
            if(dashTimes[0] == timer)
            {
                if(dashPos.Count > 0)
                {
                    Instantiate(reverseDash, dashPos[0], Quaternion.Euler(dashRot[0]), this.gameObject.transform);
                    dashTimes.RemoveAt(0);
                    dashPos.RemoveAt(0);
                    dashRot.RemoveAt(0);
                }
            }
        }
        else
        {
            StopRewind();
        }
    }

    private void Record()
    {
        positions.Insert(0, this.transform.position);
        timer += Time.deltaTime;
    }

    public void StartRewind()
    {
        isRewinding = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
    }

    public void AddDashEffectList(float time, Vector3 pos, Vector3 rot)
    {
        dashPos.Insert(0, pos);
        dashRot.Insert(0, rot);
        dashTimes.Insert(0, time);
    }

}
