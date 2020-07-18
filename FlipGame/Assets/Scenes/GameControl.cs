using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;

public class GameControl : MonoBehaviour {
    [SerializeField]
    private Transform[] DIno;

    [SerializeField]
    private GameObject winText;

    public static bool youWin;

    private ClientSocket clientSock ;

    void Start()
    {
        clientSock = new ClientSocket() ;
        Debug.Log("Starting");
        clientSock.setupSocket();
        winText.SetActive(false);
    	youWin = false;
    }

    void Update()
    {
    	if (DIno[0].rotation.z == 0 && 
    		DIno[1].rotation.z == 0 &&
    		DIno[2].rotation.z == 0 &&
    		DIno[3].rotation.z == 0 &&
    		DIno[4].rotation.z == 0 &&
    		DIno[5].rotation.z == 0 )
    	{
            if (!youWin) {
               clientSock.sendMessage("You won!");
               Debug.Log("success");
    		   youWin = true;
        }
    	winText.SetActive(true);
   	}
    }
}


public class ClientSocket {
 
    bool socketReady = false;
    TcpClient mySocket;
    public NetworkStream theStream;
    StreamWriter theWriter;
    StreamReader theReader;
    public String Host = "10.0.1.10";
    public Int32 Port = 5000; 
 
    public void setupSocket() {
                                // Socket setup here
        try {
            mySocket = new TcpClient(Host, Port);
            theStream = mySocket.GetStream();
            theWriter = new StreamWriter(theStream);
            theReader = new StreamReader(theStream);
            socketReady = true;
        }
        catch (Exception e) {
            Debug.Log("Socket error:" + e);
            throw e;           
        }
     }


     public void sendMessage(string message)
    {
     if (socketReady)
     {
             theWriter.Write(message);
             theWriter.Flush();
     }
 }
}

