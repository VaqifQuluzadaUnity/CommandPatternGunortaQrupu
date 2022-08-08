using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
	public GameObject movedObject;

	public Vector3 initialPos;

	public Vector3 destinationPos;

	public float speed;

	public MoveCommand(GameObject _movedObject, Vector3 _initialPos, Vector3 _destinationPos, float _speed)
	{
		movedObject = _movedObject;

		initialPos = _initialPos;

		destinationPos = _destinationPos;

		speed = _speed;
	}

	public void Execute()
	{
		CommandManager.instance.MoveObject(movedObject, destinationPos, speed);
	}

	public float GetCommandEndTime()
	{
		float distance = Vector3.Distance(initialPos, destinationPos);

		float estimatedTime = distance / speed;

		return estimatedTime;
	}

	public void Undo()
	{
		CommandManager.instance.MoveObject(movedObject, initialPos, speed);
	}
}
