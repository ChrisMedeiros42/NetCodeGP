using Unity.Netcode;
using UnityEngine;

public class NetworkVariableTest : NetworkBehaviour
{
    private NetworkVariable<float> ServerNetworkVariable = new NetworkVariable<float>();
    private NetworkVariable<float> ClientNetworkVariable = new NetworkVariable<float>();
    // private NetworkVariable<float> ClientNetworkVariable = new NetworkVariable<float>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private float last_t = 0.0f;

    public override void OnNetworkSpawn() {
        if (IsServer) {
            ServerNetworkVariable.Value = 0.0f;
            Debug.Log("Server's var initialized to: " + ServerNetworkVariable.Value);
        } else if (IsClient && IsOwner) {
            // ClientNetworkVariable.Value = 0.0f;
            StartClientVariableServerRPC();
            Debug.Log("Client's var initialized to: " + ClientNetworkVariable.Value);
        }
    }

    private void Update()
    {
        var t_now = Time.time;

        if (IsServer) {
            ServerNetworkVariable.Value = ServerNetworkVariable.Value + 0.1f;
            if (t_now - last_t > 0.5f) {
                last_t = t_now;
                Debug.Log("Server set its var to: " + ServerNetworkVariable.Value + ", has client var at: " + ClientNetworkVariable.Value);
            }
        } else if (IsClient && IsOwner) {
            // ClientNetworkVariable.Value = ClientNetworkVariable.Value + 0.1f;
            UpdateClientVariableServerRPC();
            if (t_now - last_t > 0.5f) {
                last_t = t_now;
                Debug.Log("Client set its var to: " + ClientNetworkVariable.Value + ", has server var at: " + ServerNetworkVariable.Value);
            }
        }
    }
    [ClientRpc]
    void RequestServerVariableClientRPC(ClientRpcParams rpcParams = default) {
        ClientNetworkVariable.Value = GetServerNetworkVariableValue();
    }

    float GetServerNetworkVariableValue() {
        return ServerNetworkVariable.Value;
    }

    [ServerRpc]
    void StartClientVariableServerRPC(ServerRpcParams rpcParams = default) {
        ClientNetworkVariable.Value = ServerNetworkVariable.Value;
    }

    [ServerRpc]
    void UpdateClientVariableServerRPC(ServerRpcParams rpcParams = default) {
        ClientNetworkVariable.Value = GetUpdatedClientVariable(0.1f);
    }

    float GetUpdatedClientVariable(float value) {
        return ClientNetworkVariable.Value += value;
    }
}
