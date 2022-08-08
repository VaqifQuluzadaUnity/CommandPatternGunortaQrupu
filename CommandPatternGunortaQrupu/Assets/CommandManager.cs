using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
  public static CommandManager instance;

	private IEnumerator moveCoroutine;

	private List<ICommand> executedCommands = new List<ICommand>();

	private bool IsUndoing = false;

	private void Awake()
	{
		if(instance!=null && instance != this)
		{
			Destroy(instance.gameObject);
		}
		instance = this;
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && !IsUndoing)
		{
			UndoAllCommands();
		}
	}

	private void UndoAllCommands()
	{
		IsUndoing = true;
		StartCoroutine(UndoAllCommandsCoroutine());
	}

	IEnumerator UndoAllCommandsCoroutine()
	{
		for(int i = executedCommands.Count - 1; i >= 0; i--)
		{
			executedCommands[i].Undo();

			yield return new WaitForSeconds(executedCommands[i].GetCommandEndTime());
		}

		executedCommands.Clear();

		IsUndoing = false;


	}

	public void AddCommandToList(ICommand command)
	{
		executedCommands.Add(command);
	}

	public void MoveObject(GameObject movedObject,Vector3 destinationPoint, float speed)
	{
		if (moveCoroutine != null)
		{
			StopCoroutine(moveCoroutine);
		}

		moveCoroutine = MoveObjectCoroutine(movedObject, destinationPoint, speed);


		StartCoroutine(moveCoroutine);
	}


  IEnumerator MoveObjectCoroutine(GameObject _movedObject, Vector3 _destinationPoint, float _speed)
	{
		while (_movedObject.transform.position != _destinationPoint)
		{
      _movedObject.transform.position =
        Vector3.MoveTowards(_movedObject.transform.position, _destinationPoint,_speed*Time.deltaTime);

      yield return new WaitForEndOfFrame();
		}
	}


}


public interface ICommand
{
  public void Execute();

  public void Undo();

	public float GetCommandEndTime();
}
