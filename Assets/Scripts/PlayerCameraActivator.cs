using Mirror;
using UnityEngine;

public class PlayerCameraActivator : NetworkBehaviour
{
    [SerializeField] Camera cam; 

    public override void OnStartLocalPlayer()
    {
        cam.enabled = true;     
    }

    void Awake()
    {
        cam.enabled = false;
    }
}
