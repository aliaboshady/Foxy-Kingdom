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
	string layerCollectable		= "Collectable";
	string layerEnemy			= "Enemy";

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
		LayersCollide(layerPlayer, layerGround);
		LayersCollide(layerPlayer, layerCollectable);
		LayersCollide(layerPlayer, layerEnemy);
		LayersCollide(layerGround, layerEnemy);
	}

	void LayersCollide(string layer1, string layer2)
	{
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(layer1), LayerMask.NameToLayer(layer2), false);
	}

}
