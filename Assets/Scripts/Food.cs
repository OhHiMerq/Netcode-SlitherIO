using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Food : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        if (!NetworkManager.Singleton.IsServer) return;

        if (col.TryGetComponent(out PlayerLength playerLength))
        {
            // called by the server
            playerLength.AddLength();
        }
        else if (col.TryGetComponent(out Tail tail))
        {
            tail.networkedOwner.GetComponent<PlayerLength>().AddLength();
        }

        NetworkObject.Despawn();
    }
}
