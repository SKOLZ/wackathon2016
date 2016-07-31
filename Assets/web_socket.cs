using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using WebSocketSharp;
using SimpleJSON;

public class web_socket : MonoBehaviour {
	public Text speed;
	public Text distance;
	public Text time;
	public Text coins;

	private float prev_raw_time = 0;
	private float prev_distance = 0;

	private float value_speed = 0;
	private string data_speed = "0";
	private string data_distance = "0";
	private string data_time = "00:00";
	private string data_coins = "0";
	private Boolean data_resting = false;
	// Use this for initialization
	void Start () {
		//using (var ws = new WebSocket ("ws://10.0.0.22:8080")) {
		var ws = new WebSocket ("ws://10.0.0.165:8080");
			ws.OnOpen += (sender, e) => {
				Debug.Log("Open: ");
			};
			ws.OnMessage += (sender, e) => {
				var message = JSON.Parse(e.Data);
				data_speed = Math.Round(value_speed)  + " KM/H";
				var raw_time = float.Parse(message["time"]);
				if(prev_raw_time == 0) {
					prev_raw_time = raw_time;
					prev_distance = float.Parse(message["distance"]);
				}
				float distance = float.Parse(message["distance"]);
				double r_distance = Math.Round(distance - prev_distance);
				data_distance = r_distance + " KM";
				data_coins = Math.Floor(r_distance / 10).ToString();
				raw_time = raw_time - prev_raw_time;
				var seconds = Math.Round(raw_time / 60).ToString("00");
				var minutes = Math.Round(raw_time % 60).ToString("00");
				data_time = seconds + ":" + minutes;
				data_resting = message["resting"] == "true";
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
		if(speed.text != data_speed) {
			speed.text = data_speed;
		}
		if(distance.text != data_distance) {
			distance.text = data_distance;
		}
		if(time.text != data_time) {
			time.text = data_time;
		}
		if(coins.text != data_coins) {
			coins.text = data_coins;
		}
	}

	public float getSpeed() {
		return Math.Min(value_speed * 0.8f, 80);
	}

	public Boolean getResting() {
		return data_resting;
	}
}
