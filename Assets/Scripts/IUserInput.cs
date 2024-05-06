using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour
{
    public float Dup;
	public float Dright;
	public float Dmag;
	public Vector3 Dvec;
	public float MouseX;
	public float MouseY;
	public bool inputEnable = true;
	public bool walk;
	public bool attack;
	protected bool lastAttack;
	protected float targetDup;
	protected float targetDright;
	protected float velocityDup;
	protected float velocityDright;

    protected Vector2 SquareToCircle(Vector2 input)//将平面直角坐标转换为圆形坐标
	{
		Vector2 output = Vector2.zero;

		output.x = input.x * Mathf.Sqrt(1-(input.y * input.y) / 2.0f);
		output.y = input.y * Mathf.Sqrt(1-(input.x * input.x) / 2.0f);
		return output;
	} 
}
