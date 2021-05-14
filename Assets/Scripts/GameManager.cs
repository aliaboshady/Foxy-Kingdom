using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	string layerDefault			= "Default";
	string layerTransparentFX	= "TransparentFX";
	string layerIgnoreRaycast	= "Ignore Raycast";
	string layerWater			= "Water";
	string layerUI				= "UI";
	string layerPlayer			= "Player";
	string layerGround			= "Ground";

	void Awake()
	{
		DisableAllPhysicsLayers();
		EnableSpecifiedPhysicsLayers();
	}

	void DisableAllPhysicsLayers()
	{
		for (int i = 0; i < 32; i++)
		{
			for (int j = 0; j < 32; j++)
			{
				Physics2D.IgnoreLayerCollision(i, j, true);
			}
		}
	}

	void EnableSpecifiedPhysicsLayers()
	{
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(layerPlayer), LayerMask.NameToLayer(layerGround), false);
	}

}
