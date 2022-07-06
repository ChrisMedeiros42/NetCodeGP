using Unity.Netcode;
using UnityEngine;

namespace HelloWorld {
    public class HelloWorldPlayer : NetworkBehaviour {

        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

        public override void OnNetworkSpawn() {
            Move();
        }

        public void Move() {
            if (NetworkManager.Singleton.IsServer) {
                var randomPosition = GetRandomPositionOnPlane();
                transform.position = randomPosition;
                Position.Value = randomPosition;
            } else if (IsOwner) {
                SubmitPositionRequestServerRPC();
            }
        }

        [ServerRpc]
        void SubmitPositionRequestServerRPC(ServerRpcParams rpcParams = default) {
            Position.Value = GetRandomPositionOnPlane();
        }

        static Vector3 GetRandomPositionOnPlane() {
            return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
        }

        private void Update() {
            transform.position = Position.Value;
        }
    }
}
