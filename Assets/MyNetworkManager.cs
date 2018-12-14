using UnityEngine;
using UnityEngine.Networking;
public class MyNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player.GetComponent<NeuronAnimatorInstance>().address = "127.0.0.1";
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}