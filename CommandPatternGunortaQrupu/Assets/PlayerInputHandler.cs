using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
	[SerializeField] private float speed=1f;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit = new RaycastHit();

			if (Physics.Raycast(mouseRay,out hit))
			{
				MoveCommand command = new MoveCommand(gameObject,transform.position,hit.point,speed);

				command.Execute();

				CommandManager.instance.AddCommandToList(command);
			}



		}
	}
}
