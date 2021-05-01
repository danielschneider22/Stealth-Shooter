using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class DestroyBullet : NetworkBehaviour
{
    private float instantiateTimer;
    public float deathTime = 3f;

    // Update is called once per frame
    void Update()
    {
        instantiateTimer += Time.deltaTime;
        if(instantiateTimer >= deathTime)
        {
            if (!NetworkManager.Singleton.IsServer) return;
            DespawnBulletServerRpc();
        }
    }
    
    // - network RPCs
    [ServerRpc(Delivery = RpcDelivery.Reliable)]
    public void DespawnBulletServerRpc()
    {
        GetComponent<NetworkObject>().Despawn(true);
    }
}
