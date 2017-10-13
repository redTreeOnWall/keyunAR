using UnityEngine;
using System.Collections;

public class TestWayPoint : MonoBehaviour {

	public TestWayPoint nextPoint;


	void Start () {
	
	}
	
	void OnDrawGizmosSelect()
	{
		if (nextPoint == null)
			return;
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, nextPoint.transform.position);
	}
}
