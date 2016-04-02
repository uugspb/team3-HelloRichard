using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class SendControllers : MonoBehaviour
{
    private static int localPort;

    // prefs
    private string IP = "127.0.0.1"; // define in init
    private int port = 8051; // define in init


    public void ChangeIP(string ip)
    {
        IP = ip;
        init();
    }

    // "connection" things
    IPEndPoint remoteEndPoint;
    UdpClient client;

    // start from unity3d
    public void Start()
    {
        IP = PlayerPrefs.GetString("IP", "172.31.3.115");
        init();
    }

    InputPacket InputP = new InputPacket();

    void Update()
    {
        InputP.Clear();
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                var tch = Input.GetTouch(i);
                InputP.Touches.Touches.Add(new TouchRemoute()
                {
                    TouchId = tch.fingerId,
                    Position = tch.position
                });
            }
        }

        
        if (Input.gyro.enabled)
        {
            InputP.Gyroscope.Aceleration = Input.gyro.userAcceleration;
            InputP.Gyroscope.Gravity = Input.gyro.gravity;
            InputP.Gyroscope.Attitude = Input.gyro.attitude;
        }

        sendString(JsonUtility.ToJson(InputP));

        //print(">> " + JsonUtility.ToJson(InputP));
    }

    // init
    public void init()
    {
        if (client != null)
        {
            client.Close();
        }

        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();
        client.Connect(remoteEndPoint);
        // status
        print("Connect to " + IP + " : " + port);
    }

    // sendData
    private void sendString(string message)
    {
        try
        {
            //if (message != "")
            //{

            // Daten mit der UTF8-Kodierung in das Binärformat kodieren.
            byte[] data = Encoding.UTF8.GetBytes(message);

            // Den message zum Remote-Client senden.
            client.Send(data, data.Length);
            //}
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }

    public void OnDestroy()
    {
        client.Close();
    }
}