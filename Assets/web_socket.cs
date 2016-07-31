﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using WebSocketSharp;
using SimpleJSON;

public class web_socket : MonoBehaviour {
	public Text text;
	// Use this for initialization
	void Start () {
		Debug.Log("start");
		//using (var ws = new WebSocket ("ws://10.0.0.22:8080")) {
		var ws = new WebSocket ("ws://10.0.0.59:8080");
			Debug.Log("ws");
			ws.OnOpen += (sender, e) => {
				Debug.Log("Open: ");
			};
			ws.OnMessage += (sender, e) => {
				var message = JSON.Parse(e.Data);
				Debug.Log("Speed: " + message["speed"]);
				Debug.Log("Speed: " + message["speed"]);
				Debug.Log("Speed: " + message["speed"]);
			};
			ws.OnClose += (sender, e) => {
				Debug.Log("Close:" + e.Code);
			};
			ws.OnError += (sender, e) => {
				Debug.Log("Error: " + e.Message);
			};
		ws.ConnectAsync();
		//}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
