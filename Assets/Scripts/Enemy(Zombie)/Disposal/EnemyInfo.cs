using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    // 적 시야, 청력
    public float eye_Range;
    public float ear_Range;

    // 적 능력치
    public float strength;
    public float rotSpeed;
    public float moveSpeed;
    public int hp;

    protected enum ENEMY_KINDS
	{
        NONE = 0,
        ZOMBIE,
        SPEED_ZOMBIE,
        TANK_ZOMBIE,
        NIGHT_ZOMBIE,
        SUPER_ZOMBIE
	}
    protected enum ENEMYSTATE
    {
        NONE = 0,
        IDLE,
        WALK,
        MOVE,
        ATTACK,
        DAMAGE,
        DEAD
    }
}
