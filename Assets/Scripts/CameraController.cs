using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform limitX;
    [SerializeField] Transform offsetStartX;
    [SerializeField] float offsetSpeed = 5f;
    [SerializeField] float offsetLimitY = 5f;
    [SerializeField] Canvas HUD;
    public static float offsetY = 0f;
    
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + offsetY, transform.position.z);
        if(transform.position.x > limitX.position.x)
		{
            transform.position = new Vector3(limitX.position.x, playerTransform.position.y + offsetY, transform.position.z);
        }
        if(transform.position.x > offsetStartX.position.x && transform.position.y <= playerTransform.position.y + offsetLimitY)
		{
            offsetY += offsetSpeed * Time.deltaTime;
            HUD.gameObject.SetActive(false);
		}
    }
}
