using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    
    void Update()
    {
        transform.position = playerTransform.position;
    }
}
