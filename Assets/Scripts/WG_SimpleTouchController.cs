using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class WG_SimpleTouchController : MonoBehaviour {

	// PUBLIC
	public delegate void TouchDelegate(Vector2 value);
	public event TouchDelegate TouchEvent;

	public delegate void TouchStateDelegate(bool touchPresent);
	public event TouchStateDelegate TouchStateEvent;

	public WG_Player player;

	// PRIVATE
	[SerializeField]
	private RectTransform joystickArea;
	private bool touchPresent = false;
	private Vector2 movementVector;


	public Vector2 GetTouchPosition
	{
		get { return movementVector;}
	}


	public void BeginDrag()
	{
		player.GetComponentInParent<WG_Player>().isExercise = false;
		player.GetComponentInParent<WG_Player>().playerAnim.SetBool("ISEXERCISE", false);
		touchPresent = true;
		player.playerState = WG_Player.PLAYERSTATE.WALK;
		if (TouchStateEvent != null)
		{
			TouchStateEvent(touchPresent);
		}
	}

	public void EndDrag()
	{
		touchPresent = false;
		movementVector = joystickArea.anchoredPosition = Vector2.zero;
		player.playerState = WG_Player.PLAYERSTATE.IDLE;
		if(player.isRun)
        {
			player.isRun = false;
			player.playerAnim.SetBool("ISRUN", false);
        }
		if (TouchStateEvent != null)
		{
			TouchStateEvent(touchPresent);
		}

	}

	public void OnValueChanged(Vector2 value)
	{
		if(touchPresent)
		{
			// convert the value between 1 0 to -1 +1
			
			movementVector.x = ((1 - value.x) - 0.5f) * 2f;
			movementVector.y = ((1 - value.y) - 0.5f) * 2f;
			
			if(TouchEvent != null)
			{
				TouchEvent(movementVector);
			}
		}

	}

}
