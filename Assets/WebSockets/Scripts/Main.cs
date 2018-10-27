using UnityEngine;
using WebSocketSharp;

using System.Threading;

public class Main : MonoBehaviour {
    private WebSocket ws;

    void Start() {
		ws = new WebSocket("ws://localhost:9001/");

        //ws.OnOpen += OnOpenHandler;
        ws.OnMessage += OnMessageHandler;
        ws.OnClose += OnCloseHandler;

        ws.ConnectAsync();        
    }

    private void OnOpenHandler(object sender, System.EventArgs e) {
        Debug.Log("WebSocket connected!");
        Thread.Sleep(3000);
        ws.SendAsync("connected!", OnSendComplete);
    }

    private void OnMessageHandler(object sender, MessageEventArgs e) {
        Debug.Log("WebSocket server said: " + e.Data);
		float x = float.Parse (e.Data);
        //Thread.Sleep(3000);
        //ws.CloseAsync();
    }

    private void OnCloseHandler(object sender, CloseEventArgs e) {
        Debug.Log("WebSocket closed with reason: " + e.Reason);
    }

    private void OnSendComplete(bool success) {
        Debug.Log("Message sent successfully? " + success);
    }
}
