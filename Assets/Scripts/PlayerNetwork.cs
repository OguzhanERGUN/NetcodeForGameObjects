using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{

	[SerializeField] private Transform spawnSample;
	Transform spawnedObjectTransform;

	private NetworkVariable<MyCustomData> randomNumber = new NetworkVariable<MyCustomData>

	(new MyCustomData { number = 0, numberFloat = 0f }

	, NetworkVariableReadPermission.Everyone
	, NetworkVariableWritePermission.Owner);

	public struct MyCustomData : INetworkSerializable
	{
		public int number;
		public float numberFloat;

		public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
		{
			serializer.SerializeValue(ref number);
			serializer.SerializeValue(ref numberFloat);
		}
	}


	public override void OnNetworkSpawn()
	{
		randomNumber.OnValueChanged += (MyCustomData previosValue, MyCustomData newValue) =>
		{
			Debug.Log(OwnerClientId + "+" + newValue.number + " " + newValue.numberFloat);

		};
	}

	// Update is called once per frame
	void Update()
	{
		if (!IsOwner) return;

		if (Input.GetKeyDown(KeyCode.T))
		{
			SpawnServerRpc();
			TestServerRpc();
			//randomNumber.Value = new MyCustomData
			//{
			//	number = randomNumber.Value.number + 1,
			//	numberFloat = randomNumber.Value.numberFloat + 1.5f,
			//	name = "a"
			//};
		}


		Vector3 moveDir = new Vector3(0, 0, 0);

		if (Input.GetKey(KeyCode.W)) moveDir = Vector3.forward;
		if (Input.GetKey(KeyCode.S)) moveDir = Vector3.back;
		if (Input.GetKey(KeyCode.A)) moveDir = Vector3.left;
		if (Input.GetKey(KeyCode.D)) moveDir = Vector3.right;

		float moveSpeed = 3f;
		transform.position += moveDir * moveSpeed * Time.deltaTime;
	}

	[ServerRpc]
	private void TestServerRpc()
	{
		Debug.Log("Server Rpc worked" + OwnerClientId);

	}
	[ServerRpc]
	private void SpawnServerRpc()
	{
		spawnedObjectTransform = Instantiate(spawnSample);
		spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);
	}
	[ClientRpc]
	private void TestClientRpc()
	{
		Debug.Log("Client Rpc Worked" + OwnerClientId);
	}
}
