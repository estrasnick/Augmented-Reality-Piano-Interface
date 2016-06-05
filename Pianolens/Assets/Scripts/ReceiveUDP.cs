using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

#if !UNITY_EDITOR
using Windows.Networking.Sockets;
#endif

public class ReceiveUDP : MonoBehaviour
{
    List<String> messages;

#if !UNITY_EDITOR
    DatagramSocket socket;

    // use this for initialization
    async void Start()
    {
        messages = new List<String>();

        Debug.Log("Waiting for a connection...");

        socket = new DatagramSocket();
        socket.MessageReceived += Socket_MessageReceived;

        try
        {
            await socket.BindEndpointAsync(null, "12345");
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Debug.Log(SocketError.GetStatus(e.HResult).ToString());
            return;
        }

        Debug.Log("exit start");
#else
    // use this for initialization
    void Start()
    {
        messages = new List<String>();

        Debug.Log("Networking disabled in editor...");

        Debug.Log("exit start");
#endif
    }

// Update is called once per frame
void Update()
    {
        GameObject metronome = GameObject.Find("MetronomeInteractive");

        foreach (String message in messages)
        {
            if (message.Contains("55,"))
            {
                metronome.SendMessageUpwards("OnSelectDown");
            }
            else if (message.Contains("57,"))
            {
                metronome.SendMessageUpwards("OnSelectUp");
            }
        }
        messages.Clear();
    }

#if !UNITY_EDITOR
    private async void Socket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender,
        Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
    {
        Debug.Log("Triggering new message!");
        //Read the message that was received from the UDP echo client.
        Stream streamIn = args.GetDataStream().AsStreamForRead();
        StreamReader reader = new StreamReader(streamIn);
        string message = await reader.ReadLineAsync();

        messages.Add(message);
        Debug.Log("MESSAGE: " + message);
        
        
    }
#endif
}