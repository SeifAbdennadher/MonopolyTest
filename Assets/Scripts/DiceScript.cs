using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour {

	static Rigidbody rb;
	public static Vector3 diceVelocity;
	private Vector3 initPosition;

	private System.Boolean thrown;
	private System.Boolean hasLanded;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		initPosition = transform.position;
		rb.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		diceVelocity = rb.velocity;

		if (Input.GetKeyDown(KeyCode.Space))
        {
			RollDice();
        }
		if (rb.IsSleeping() && !hasLanded && thrown)
        {
			hasLanded = true;
			rb.useGravity = false;
        }
	}

	void RollDice()
    {
		if (!thrown && !hasLanded)
		{
			rb.isKinematic = false;
			thrown = true;
			rb.useGravity = true;

			float dirX = Random.Range(0, 500);
			float dirY = Random.Range(0, 500);
			float dirZ = Random.Range(0, 500);

			Vector3 fromPosition = transform.position;
			Vector3 toPosition = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
			Vector3 direction = toPosition - fromPosition;
			direction = new Vector3(direction.x, direction.y + 8, direction.z);
			rb.AddForce(direction * 30, ForceMode.Acceleration);
			rb.AddTorque(dirX, dirY, dirZ);
        } else
        {
			Reset();
        }
    }

    private void Reset()
    {
		transform.position = new Vector3(initPosition.x, initPosition.y, Random.Range(-4, 4));
		transform.rotation = Quaternion.Euler(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.isKinematic = true;
		thrown = false;
		hasLanded = false;
		rb.useGravity = false;
    }
}
