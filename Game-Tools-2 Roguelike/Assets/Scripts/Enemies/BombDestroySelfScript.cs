using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDestroySelfScript : MonoBehaviour
{
    private void DestroySelf()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
