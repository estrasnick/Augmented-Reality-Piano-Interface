using UnityEngine;
using System;
using System.IO;

#if !UNITY_EDITOR
using Windows.Networking.Sockets;
#endif

public class UDP_Midi : MonoBehaviour
{
#if !UNITY_EDITOR
    DatagramSocket socket;

    // use this for initialization
    async 
#endif
    void Start()
    {
#if !UNITY_EDITOR
        Debug.Log("Waiting for a connection...");

        socket = new DatagramSocket();
        socket.MessageReceived += Socket_MessageReceived;

        try
        {
            await socket.BindEndpointAsync(new Windows.Networking.HostName("70.213.10.245"), "12345");
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Debug.Log(SocketError.GetStatus(e.HResult).ToString());
            return;
        }

        Debug.Log("exit start");
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }

#if !UNITY_EDITOR
    private async void Socket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender,
        Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
    {
        //Read the message that was received from the UDP echo client.
        Stream streamIn = args.GetDataStream().AsStreamForRead();
        StreamReader reader = new StreamReader(streamIn);
        string message = await reader.ReadLineAsync();

        Debug.Log("MESSAGE: " + message);
        GameObject button = GameObject.Find("PlayPause");
        button.SendMessageUpwards("OnSelect");
    }
#endif
}