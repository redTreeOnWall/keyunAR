using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    // Use this for initialization
    public Transform[] points;
    public int nextIndex;

    void Start () {
		nextIndex = 0;
	}
    Vector3 ignoreY(Vector3 v3)
    {
        return new Vector3(v3.x, 0, v3.z);
    }
    // Update is called once per frame
    void Update ()
    {
        Debug.Log(Vector3.Distance(ignoreY(points[nextIndex % points.Length].position), ignoreY(transform.position)));
        if (Vector3.Distance(ignoreY(points[nextIndex % points.Length].position), ignoreY(transform.position)) > 0.8f)
        {
            Vector3 direction = (ignoreY(points[nextIndex % points.Length].position) - ignoreY(transform.position)).normalized;
            transform.forward = Vector3.Lerp(transform.forward, direction, 0.1f);
            // CheckState(RobotSate.Walk);
            transform.Translate(transform.forward * Time.deltaTime * 10f);
            // transform.GetComponent<CharacterController>().SimpleMove(transform.forward  * 5f);
            nextIndex++;
        }
        else if(nextIndex<points.Length)
        {
            
               // nextIndex++;
           
        }
       
        //else
        //{
        //    //    nextIndex = 0;
        //    //}
        }

    }

