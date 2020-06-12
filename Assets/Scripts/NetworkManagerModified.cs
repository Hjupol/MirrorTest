using UnityEngine;
using Mirror;

[AddComponentMenu("")]
public class NetworkManagerModified : NetworkManager
{
        public Transform leftSpawn;
        public Transform rightSpawn;
        GameObject ball;//

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            Transform start = numPlayers == 0 ? leftSpawn : rightSpawn;
            GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
            NetworkServer.AddPlayerForConnection(conn, player);

            if (numPlayers == 2)
            {
            PauseManager.pauseOn = true;
            }
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
        if (PauseManager.pauseOn == true)
            PauseManager.pauseOn = false;

            base.OnServerDisconnect(conn);
        }
    
}
