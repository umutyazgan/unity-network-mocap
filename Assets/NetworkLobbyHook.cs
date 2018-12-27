using UnityEngine;
using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook 
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        NeuronAnimatorInstance animator = gamePlayer.GetComponent<NeuronAnimatorInstance>();
        Debug.Log("NetworkLobbyHook");
        Debug.Log(animator.enabled);
        animator.address = lobby.playerName;
        Debug.Log(animator.address);
        //animator.enabled = true;
        //gameObject VRCam = 
        //animator.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        Debug.Log(lobby.name.ToString());
    }
}
