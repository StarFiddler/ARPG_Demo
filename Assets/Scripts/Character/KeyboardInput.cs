using UnityEngine;
using System.Collections;
using System.Linq;
public class KeyboardInput: IUserInput
{
	public string keyUp = "w";
	public string keyDown = "s";
	public string keyLeft = "a";
	public string keyRight = "d";
	public string keyWalk;
	public string keyJump;
	public string keyDash;
	public string keyAttack1;
	public string keyAttack2;
	public string keyLockOn;
	public string keySowrdMaster;
	public string keyGunslinger;
	public string keyWhip;
	public string keySamurai;
	public string keySheath;
	// public float Dup;
	// public float Dright;
	// public float Dmag;
	// public Vector3 Dvec;
	// public float MouseX;
	// public float MouseY;
	// public bool inputEnable = true;
	// public bool walk;
	// public bool attack;
	// private bool lastAttack;
	// private float targetDup;
	// private float targetDright;
	// private float velocityDup;
	// private float velocityDright;
	void Start()
	{

	}
	
	void Update()
	{
		MouseX = Input.GetAxis("Mouse X");
		MouseY = Input.GetAxis("Mouse Y");
		walk = Input.GetKey(keyWalk);
		lockon = Input.GetKeyDown(keyLockOn);//每次按下key后，lockon变为true
	}

	void FixedUpdate()
	{
		targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
		targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);
		if(inputEnable == false)
		{
			targetDup = 0;
			targetDright = 0;
		}
		//对输入方向向量做线性插值
		Dup = Mathf.SmoothDamp (Dup, targetDup, ref velocityDup, 0.2f);
		Dright = Mathf.SmoothDamp (Dright, targetDright, ref velocityDright, 0.2f);

		Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
		float Dright2 = tempDAxis.x;
		float Dup2 = tempDAxis.y;

		Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
		Dvec = Dright2 * transform.right + Dup2 * transform.forward;
	}

	// private Vector2 SquareToCircle(Vector2 input)//将平面直角坐标转换为圆形坐标
	// {
	// 	Vector2 output = Vector2.zero;

	// 	output.x = input.x * Mathf.Sqrt(1-(input.y * input.y) / 2.0f);
	// 	output.y = input.y * Mathf.Sqrt(1-(input.x * input.x) / 2.0f);
	// 	return output;
	// } 
}