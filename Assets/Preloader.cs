using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class Preloader : MonoBehaviour
{
    public UnityEvent OnServerEvent;
    private void Start()
    {
        Application.targetFrameRate = 60;
        string[] args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-launch-as-server")
            {
                OnServerEvent.Invoke();
                NetworkManager.Singleton.StartServer();
            }
        }
    }
}
