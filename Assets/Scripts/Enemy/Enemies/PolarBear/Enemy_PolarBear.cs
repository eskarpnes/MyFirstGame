﻿public class Enemy_PolarBear : Enemy
{
	private float baseSpeed = 1.5f;
	private float speedRange = 1f;

	protected override float GetBaseSpeed()
	{
		return baseSpeed;
	}

	protected override float GetSpeedRange()
	{
		return speedRange;
	}
}
