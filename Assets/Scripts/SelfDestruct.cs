using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float timeTillDestroy = 3f;
    void Start()
    {
        Destroy(gameObject, timeTillDestroy);
    }
}
