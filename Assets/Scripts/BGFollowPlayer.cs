using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    
    void LateUpdate()
    {
        transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y, 0);
    }
}
