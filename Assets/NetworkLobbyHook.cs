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

        animator.address = lobby.playerName;
        Debug.Log(lobby.name.ToString());
    }
}
