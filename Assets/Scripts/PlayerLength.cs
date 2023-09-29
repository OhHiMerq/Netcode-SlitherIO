
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerLength : NetworkBehaviour
{
    [SerializeField] private GameObject tailPrefab;
    public NetworkVariable<ushort> length = new(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server); // default read:everyone, write:server
    private List<GameObject> _tails;
    private Transform _lastTail;
    private Collider2D _collider2D;

    private void LengthChanged(ushort previousValue, ushort newValue)
    {
        Debug.Log("LengthChanged Callback");
        InstantiateTail();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        _tails = new List<GameObject>();
        _lastTail = transform;
        _collider2D = GetComponent<Collider2D>();
        if (!IsServer) length.OnValueChanged += LengthChanged;
    }

    // Called by the server
    [ContextMenu("Add Length")]
    private void AddLength()
    {
        length.Value += 1;
        InstantiateTail();
    }



    private void InstantiateTail()
    {
        GameObject tailObj = Instantiate(tailPrefab, transform.position, Quaternion.identity);
        tailObj.GetComponent<SpriteRenderer>().sortingOrder = -length.Value;
        if (tailObj.TryGetComponent(out Tail tail))
        {
            tail.networkedOwner = transform;
            tail.followTransform = _lastTail;
            _lastTail = tailObj.transform;
            Physics2D.IgnoreCollision(tailObj.GetComponent<Collider2D>(), _collider2D);

        }
        _tails.Add(tailObj);
    }

}
