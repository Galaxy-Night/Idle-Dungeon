using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClick : MonoBehaviour
{
	public Vector3 clickPosition { get; private set; }

	private void OnMouseDown()
	{
		Vector3 rawPosition = Input.mousePosition;
		rawPosition = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(rawPosition);
		clickPosition = new Vector3(rawPosition.x, rawPosition.y, 0);

		gameObject.GetComponent<Game>().ClickEnemyDamage();
	}
}
