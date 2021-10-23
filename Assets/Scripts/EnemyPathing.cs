using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    public Transform startMark;
    public Transform endMark;

    public float speed = 1.0F;

    private float startTime;

    private float journeyLength;


    // Start is called before the first frame update
    void Start()
    {

        startTime = Time.time;

        journeyLength = Vector2.Distance(startMark.position, endMark.position);
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;

        float fracJourney = distCovered / journeyLength;

        transform.position = Vector2.Lerp(startMark.position, endMark.position, Mathf.PingPong(fracJourney, 1));

    }


}
