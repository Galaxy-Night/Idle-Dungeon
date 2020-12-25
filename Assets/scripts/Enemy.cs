using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
	int damageInterval;
	int coinValue;
	int difficultyLevel;

	public Enemy(int _maxHealth, int _damage, 
		int _damageInterval, int _coinValue, int _difficultyLevel) :
		base(_maxHealth, _damage) {
		damageInterval = _damageInterval;
		coinValue = _coinValue;
		difficultyLevel = _difficultyLevel;
	}
}
