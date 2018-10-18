using UnityEngine;
using System.Collections;

public class CameraBor : MonoBehaviour {

	private float timer = 0;

	private Transform PlayerV;
	public static CameraBor Instance{get;private set;}
	
	public Vector2 Smoothin = new Vector2(2, 2);
    public BoxCollider2D _bounds{ get; set;}
	private GameObject[] CamBounds;
	private Vector3
		_min,
		_max;
	public Vector2 Margin = new Vector2 (3, 3);
	private bool isFollowing;
	public bool UpdateBounds = false; 
	public bool StartPos{ get; set;}

	private float x;
	private float y;
    public float YPlus = 0;

	private void Awake()
	{
		CamBounds = GameObject.FindGameObjectsWithTag ("CameraBound");
		if (Margin.x == 0) {
			Margin.x = 0.2f;
			Margin.y = 0.2f;
		}
      
        /*Smoothing.x = 2f;
		Smoothing.y = 2f;*/
		isFollowing = true;
	}
	public void Start()
	{

		PlayerV = GameObject.Find ("Player").GetComponent<Transform> ();
		x = PlayerV.position.x;
		y = PlayerV.position.y + YPlus;
	 
	  isFollowing = true;
	  Application.targetFrameRate = 60;
		//if(Player!=null) 
	
	

	}


	public void LateUpdate()
	{
		foreach (GameObject c in CamBounds) {
			if(c.GetComponent<BoxCollider2D> ().enabled)
			_bounds = c.GetComponent<BoxCollider2D> ();
		}

		//if (UpdateBounds) {
			SetVasPos();
		//}

		_min = _bounds.bounds.min;
		_max = _bounds.bounds.max;


		if(isFollowing)
		{
			if(Mathf.Abs(x-PlayerV.position.x)>Margin.x)
				x = Mathf.Lerp(x,PlayerV.position.x, Smoothin.x * Time.deltaTime);
			
			if(Mathf.Abs(y-PlayerV.position.y)>Margin.y)
				y = Mathf.Lerp(y,PlayerV.position.y+ YPlus, Smoothin.y * Time.deltaTime);
			
		}
		
		var cameraHalfWidth = Camera.main.orthographicSize * ((float)Screen.width/Screen.height);
      //  var cameraHalfHeight = Camera.main.orthographicSize * ((float)Screen.height / Screen.height);

        x = Mathf.Clamp (x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
			y = Mathf.Clamp (y, _min.y + Camera.main.orthographicSize, _max.y - Camera.main.orthographicSize);
       
		transform.position = new Vector3(x,y,-21);
		
	}


	public void Set_UpdateBounds()
	{
		timer = Time.fixedTime+0.08f;

	}

	public void SetVasPos()
	{


		if (timer > Time.fixedTime) 
		{
			x = PlayerV.position.x;
			y = PlayerV.position.y;
		}

	}

}
