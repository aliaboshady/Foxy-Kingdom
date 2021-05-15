using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
	[SerializeField] HUDManager hudManager;
	public int cherriesCount;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Collectable")
		{
			if(collision.name.Contains("Cherry"))
			{
				cherriesCount += 1;
				hudManager.UpdateCherries(cherriesCount);
			}
		}
	}
}
