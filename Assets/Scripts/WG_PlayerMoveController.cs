using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class WG_PlayerMoveController : MonoBehaviour {

	// PUBLIC
	public WG_SimpleTouchController leftController;
	public Transform headTrans;
	public float speedMovements = 5f;
	public GameObject player;

	// PRIVATE
	private Rigidbody _rigidbody;

	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		// move
		if (!player.GetComponentInParent<WG_Player>().isDead)
		{
			_rigidbody.MovePosition(transform.position + (transform.forward * leftController.GetTouchPosition.y * Time.deltaTime * speedMovements) +
				(transform.right * leftController.GetTouchPosition.x * Time.deltaTime * speedMovements));

			if (leftController.GetTouchPosition.x != 0)
			{
				if (Mathf.Atan2(leftController.GetTouchPosition.y, leftController.GetTouchPosition.x) * Mathf.Rad2Deg < 0)
				{
					player.transform.rotation = Quaternion.Euler(0f, 90 - (360 + Mathf.Atan2(leftController.GetTouchPosition.y, leftController.GetTouchPosition.x) * Mathf.Rad2Deg), 0f);
				}
				else
				{
					player.transform.rotation = Quaternion.Euler(0f, 90 - Mathf.Atan2(leftController.GetTouchPosition.y, leftController.GetTouchPosition.x) * Mathf.Rad2Deg, 0f);
				}
			}
		}
	}

}
