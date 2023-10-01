
using Unity.Netcode;
using UnityEngine;

public class ConnectionApprovalHandler : MonoBehaviour
{
    private const int MaxPlayers = 2;
    private void Start()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        // might need to ponder this more if we want to assign a different kind of prefab https://docs-multiplayer.unity3d.com/netcode/current/basics/connection-approval/
        Debug.Log("Connect Approval");
        response.Approved = true; // true by default
        response.CreatePlayerObject = true;
        response.PlayerPrefabHash = null; // default prefab will be used
        if (NetworkManager.Singleton.ConnectedClients.Count >= MaxPlayers)
        {
            response.Approved = false;
            response.Reason = "Server is Full";
        }
        response.Pending = false; // done 
    }
}
