using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public bool isRewinding = false;

    private List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Rewinder.Instance._canRewind)
        {
            if (Input.GetKey(KeyCode.Q))
                StartRewind();
            if (Input.GetKeyUp(KeyCode.Q))
                StopRewind();
        }
        else
            StopRewind();
    }

    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    private void Rewind()
    {
        if(positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    private void Record()
    {
        positions.Insert(0, this.transform.position);
    }

    public void StartRewind()
    {
        isRewinding = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
    }
}
