using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
	[SerializeField] private Button serverButton;
	[SerializeField] private Button hostButton;
	[SerializeField] private Button clientButton;
	[SerializeField] private Button disconnectButton;

	private void Awake()
	{
		serverButton.onClick.AddListener(() =>
		{
			NetworkManager.Singleton.StartServer();
		});
		clientButton.onClick.AddListener(() =>
		{
			NetworkManager.Singleton.StartClient();
		});
		hostButton.onClick.AddListener(() =>
		{
			NetworkManager.Singleton.StartHost();
		});
		disconnectButton.onClick.AddListener(() =>
		{
			NetworkManager.Singleton.Shutdown();
		});
	}
}
