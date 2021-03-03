using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoving : MonoBehaviour
{
    [SerializeField]
    private float waitSecondsToMove = 1.0f;
    [SerializeField]
    private float moveSpeed = 1.0f;

    private float timer;

    [SerializeField]
    private float seconds = 0;

    BoxCollider2D boxCollider2D;

    private Vector3 startPos;

    private TimeBody tb;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
    }

    void Start()
    {
        startPos = this.transform.position;
        tb = GetComponent<TimeBody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(seconds >= waitSecondsToMove)
        {
            if (this.transform.position.x >= -35)
            {
                boxCollider2D.enabled = true;
                this.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            }
            else
            {
                boxCollider2D.enabled = false;
                this.transform.Translate(Vector3.zero);
            }
            
        }

        if (tb.isRewinding)
        {
            if(this.transform.position.x > -35)
                boxCollider2D.enabled = true;
            if (seconds > 0)
                seconds -= Time.deltaTime;
            
            if (seconds < 0)
                seconds = 0.0f;
        }
        else
        {
            seconds += Time.deltaTime;
        }


    }


}
