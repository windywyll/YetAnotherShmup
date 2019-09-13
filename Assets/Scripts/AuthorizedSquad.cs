using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthorizedSquad : MonoBehaviour {

    [SerializeField]
    private Spawn authorizedSpawn;

    [SerializeField]
    private bool throughScreen;

    public Spawn GetAuthorizedSpawn()
    {
        return authorizedSpawn;
    }

    public bool IsGoingThroughScreen()
    {
        return throughScreen;
    }
}
