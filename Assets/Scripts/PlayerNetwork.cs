using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{

	private NetworkVariable<int> randomNumber = new NetworkVariable<int>(0);

	// Update is called once per frame
	void Update()
	{
		Debug.Log(randomNumber.Value);


		if (Input.GetKeyDown(KeyCode.T)) randomNumber.Value = Random.Range(0, 10);


		if (IsOwner)
		{
			Vector3 moveDir = new Vector3(0, 0, 0);

			if (Input.GetKey(KeyCode.W)) moveDir = Vector3.forward;
			if (Input.GetKey(KeyCode.S)) moveDir = Vector3.back;
			if (Input.GetKey(KeyCode.A)) moveDir = Vector3.left;
			if (Input.GetKey(KeyCode.D)) moveDir = Vector3.right;

			float moveSpeed = 3f;
			transform.position += moveDir * moveSpeed * Time.deltaTime;
		}
	}
}
