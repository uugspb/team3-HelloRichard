using System;
using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.Networking;

public class InputRemoute : InputBase
{
    private UdpClient client;

    Thread receiveThread;

    // public
    // public string IP = "127.0.0.1"; default local
    public int port; // define > init


    // start from unity3d
    public void Start()
    {
        Application.runInBackground = true;
        init();
    }


    public void Update()
    {
    }
    // init
    private void init()
    {

        // define port
        port = 8051;


        // ----------------------------
        // Abhören
        // ----------------------------
        // Lokalen Endpunkt definieren (wo Nachrichten empfangen werden).
        // Einen neuen Thread für den Empfang eingehender Nachrichten erstellen.
        receiveThread = new Thread(
            new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

    }

    // receive thread
    private void ReceiveData()
    {
        IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
        IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
        client = new UdpClient(server);
        while (true)
        {

            try
            {
                // Bytes empfangen.
                
                byte[] data = client.Receive(ref anyIP);
                if (data.Length > 0)
                {
                    // Bytes mit der UTF8-Kodierung in das Textformat kodieren.
                    string text = Encoding.UTF8.GetString(data);
                    InputPacket = JsonUtility.FromJson<InputPacket>(text);
                    // Den abgerufenen Text anzeigen.
                    //print(">> " + text);

                    // latest UDPpacket
                    
                }

                Thread.Sleep(10);


            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    // getLatestUDPPacket
    // cleans up the rest

    public void OnDestroy()
    {
        if (receiveThread.IsAlive)
        {
            receiveThread.Abort();
            client.Close();
        }
    }

    public void OnApplicationQuit()
    {
        if (receiveThread.IsAlive)
        {
            receiveThread.Abort();
            client.Close();
        }
    }
}
