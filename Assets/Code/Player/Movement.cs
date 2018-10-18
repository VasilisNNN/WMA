using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {

    public static Movement Instance { get; private set; }

    private List<GameObject> coll_obj = new List<GameObject>();
    private Inventory Inv;
    public float speed = 0.1f;
    public float _normalHSpeed { get; set; }
    public float _normalVSpeed { get; set; }
    public float DayFinish { get; set; }
    public string EndDayLocation{get;set;}

    private float speednormal; 
    private bool isFacingRight = true;
    private Animator anim;

	public bool Col{get;set;}
	public bool VerMove;

	public bool MovePers{ get; set;}

	public bool flip = true;
	private AudioSource Au;
	public float NextFoot;
	private float soundtimer,SpeedCountTimer;
	public bool draw = true;


	public bool DrawDialog{ get; set; }
    
	private bool menu_b;
	public bool inventory_b{ get; set;}
	
	public bool enter_b{ get; set;}
	public bool exit_b{ get; set;}
	public float _horizontal { get; set; }
	public float _vertical { get; set; }
	private bool joystick;
	private float InvTimer;
	private CharacterController2D _controller;
	private Vector3 CorrentPos, ExPos,camx;
    
	public bool steps = true;
    private BoxCollider2D _boxcollider, PlayerBox;
    private Transform _transform;
    private string StartLayerName, ForG;

    void Awake()
	{
        EndDayLocation = null;
        StartLayerName = "Default";
        ForG = "FG";
        _transform = transform;

        GameObject VA = GameObject.Find("PlayerA");

        if (VA.GetComponent<CollList>() == null)
            VA.AddComponent<CollList>();

        if (VA.GetComponent<Rigidbody2D>() == null) VA.AddComponent<Rigidbody2D>();
        if (VA.GetComponent<BoxCollider2D>() == null) VA.AddComponent<BoxCollider2D>();

        VA.GetComponent<BoxCollider2D>().size = new Vector2(0.7f,1.8f);
        VA.GetComponent<BoxCollider2D>().isTrigger = true;
        VA.GetComponent<Rigidbody2D>().gravityScale = 0;

        if (GameObject.Find ("Steps(Clone)") == null&&steps) {
			GameObject Stepss  = new GameObject();
			Stepss = (GameObject)Instantiate(Resources.Load("PrefabObjects/Steps"));
			Stepss.transform.parent = GameObject.Find("PlayerA").transform;
			GameObject.Find ("Steps(Clone)").transform.position = new Vector3(transform.position.x+0.3f,transform.position.y,0);
		}
        
        Inv = GetComponent<Inventory> ();
        
		_controller = GetComponent<CharacterController2D>();
        
        speednormal = speed;
        DayFinish = 1.2f;
        
    }

    void Start()
    {
		
		
		if(GameObject.Find("PlayerA").GetComponent<SpriteRenderer>()!=null)GameObject.Find("PlayerA").GetComponent<SpriteRenderer>().enabled = draw;
		GetComponent<BoxCollider2D>().enabled = draw;

		
		Au = gameObject.GetComponent<AudioSource>(); 
		if (Au != null)
						Au.playOnAwake  =true;


		MovePers = true;
        anim = GameObject.Find("PlayerA").GetComponent<Animator>();

		if (PlayerPrefs.GetInt ("FaceVector") == 1)
			isFacingRight = true;
		else if(PlayerPrefs.GetInt ("FaceVector") == -1)
			isFacingRight = false;

        if (flip)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= PlayerPrefs.GetInt("FaceVector");
            transform.localScale = theScale;
        }
        

        PlayerPrefs.SetString ("CorrLevel", Application.loadedLevelName);

        if (SceneManager.GetActiveScene().name == PlayerPrefs.GetString("CorrLoadingLevel") &&
            GameObject.Find(PlayerPrefs.GetString("PrevLoadingLevel") + "Exit") != null)
        {
            transform.position = GameObject.Find(PlayerPrefs.GetString("PrevLoadingLevel") + "Exit").transform.position;
        }

        

        
    }

	void Update()
    {
        if (Input.GetKeyDown("=") && PlayerPrefs.GetInt("Day") < 8)
            PlayerPrefs.SetInt("Day", PlayerPrefs.GetInt("Day") + 1);
        if (Input.GetKeyDown("-") && PlayerPrefs.GetInt("Day") > 0)
            PlayerPrefs.SetInt("Day", PlayerPrefs.GetInt("Day") - 1);

        coll_obj = GameObject.Find("PlayerA").GetComponent<CollList>().Getcollob();

        LayerMove();

        for (int i = 0; i<Input.GetJoystickNames ().Length; i++) {
			if (Input.GetJoystickNames () [i] != "")
				joystick = true;
			else
				joystick = false;
		}
		InputSets();


		if(isFacingRight)PlayerPrefs.SetInt ("FaceVector",1);
		else PlayerPrefs.SetInt ("FaceVector",-1);

	float move  = Input.GetAxis("Horizontal");

		Controls();
		//Cursor.visible = false;
    
    }

    private void LayerMove()
    {
        PlayerBox = GetComponent<BoxCollider2D>();

        for (int ob = 0; ob < GameObject.FindGameObjectsWithTag("LayerFlip").Length; ob++)
        {
            _boxcollider = GameObject.FindGameObjectsWithTag("LayerFlip")[ob].GetComponent<BoxCollider2D>();

            _transform = GameObject.FindGameObjectsWithTag("LayerFlip")[ob].transform;


           
                if (_boxcollider.bounds.min.y > PlayerBox.bounds.min.y)
                    _transform.GetComponent<SpriteRenderer>().sortingLayerName = StartLayerName;
                else _transform.GetComponent<SpriteRenderer>().sortingLayerName = ForG;

            
            for (int i = 0; i < _transform.childCount; i++)
            {
                if (_transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                {
                    if (_boxcollider.bounds.min.y > PlayerBox.bounds.min.y)
                        _transform.GetChild(i).GetComponent<SpriteRenderer>().sortingLayerName = StartLayerName;
                    else _transform.GetChild(i).GetComponent<SpriteRenderer>().sortingLayerName = ForG;

                }

                
            }
        }
    }


    public void Controls()
	{
		//PersDialog ();
	
		if (inventory_b && Inv!=null)
        {
            Inv.showinvent = !Inv.showinvent;

        if (Inv.showinvent) MovePers = false;
        else MovePers = true;
        }

        if (Input.GetKeyDown ("l")) {
			PlayerPrefs.SetString ("CorrLevel", Application.loadedLevelName);

			if (Input.GetKeyDown ("n"))
			Application.LoadLevel(PlayerPrefs.GetString ("CorrLevel"));

		}

		

		if (MovePers) {
			CountSpeed ();



            
                _normalHSpeed = _horizontal;
                _normalVSpeed = _vertical;
            


            if (speed > 0) {
				if (_normalVSpeed != 0 || _normalHSpeed != 0)
					anim.SetBool ("Move", true);
				else
					anim.SetBool ("Move", false);
				
				

				if (_normalHSpeed > 0 && !isFacingRight)

					Flip ();
				else if (_normalHSpeed < 0 && isFacingRight)
					Flip ();
				
			}



			if (_horizontal != 0 && _vertical != 0)
				speed = speednormal / 1.28f;
			else
				speed = speednormal;

		
		
	
	     
			/*if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();*/
		} else {
			_normalHSpeed = _normalVSpeed = 0;
			anim.SetBool ("Move", false);
		}
		_controller.SetHorizontalForce (
			Mathf.Lerp (_controller.Velocity.x,
		            _normalHSpeed * speed, 10));
		
		if (VerMove)
			_controller.SetVerticalForce (Mathf.Lerp (_controller.Velocity.y, _normalVSpeed * speed, 10));

	}
    
    private void Flip()
    {
		if (flip) {

						//меняем направление движения персонажа
						isFacingRight = !isFacingRight;
						//получаем размеры персонажа
						Vector3 theScale = transform.localScale;
						//зеркально отражаем персонажа по оси Х
						theScale.x *= -1;
						//задаем новый размер персонажа, равный старому, но зеркально отраженный
						transform.localScale = theScale;
						//transform.localPosition(Vector3());
				}
    }

    private void OnGUI()
    {
        if (DayFinish < 1.2f)
        {
            MovePers = false;
            Texture BlackFG = Resources.Load<Texture>("ItemIcons/GunPower");
            Color guiColor = GUI.color; // Save the current GUI color
            GUI.color = new Color(1, 1, 1, DayFinish);
            DayFinish += 0.007f;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BlackFG);
            GUI.color = guiColor; // Get back to previous GUI color


            if (DayFinish >=1)
            {
                PlayerPrefs.SetInt("Day", PlayerPrefs.GetInt("Day") + 1);
                PlayerPrefs.SetFloat("DayStart", 1);
                if(EndDayLocation==null)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                else SceneManager.LoadScene(EndDayLocation);
                DayFinish = 1.3f;
            }


        }

        //print(PlayerPrefs.GetFloat("DayStart"));
        if (PlayerPrefs.GetFloat("DayStart")> 0)
        {
            
            Texture BlackFG = Resources.Load<Texture>("ItemIcons/GunPower");
            Color guiColor = GUI.color; // Save the current GUI color
            GUI.color = new Color(1, 1, 1, PlayerPrefs.GetFloat("DayStart"));
            PlayerPrefs.SetFloat("DayStart", PlayerPrefs.GetFloat("DayStart") - 0.0025f);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BlackFG);
            GUI.color = guiColor; // Get back to previous GUI color


           

        }
    }
    public void SetVasInLevel()
	{	
		if (GameObject.Find (PlayerPrefs.GetString ("PrevName") + "Exit") != null) {
			Vector3 t = GameObject.Find (PlayerPrefs.GetString ("PrevName") + "Exit").transform.position;
			transform.position = new Vector3 (t.x, t.y, 0f);
		}

	}
	                    




	void InputSets()
	{
		
		if (!joystick) {
			_horizontal = Input.GetAxis ("Horizontal");
			_vertical = Input.GetAxis ("Vertical");
			//atack_b = Input.GetButtonDown ("Atack");
			enter_b = Input.GetButtonDown ("Enter");
		
			exit_b = Input.GetButtonDown ("Exit");
			menu_b = Input.GetButton ("Exit");
			inventory_b = Input.GetButtonDown ("Inventory");

			
		} else {
			_horizontal = Input.GetAxis ("Horizontal_J");
			_vertical = Input.GetAxis ("Vertical_J");
			//atack_b = Input.GetKeyDown(KeyCode.JoystickButton2);
			enter_b = Input.GetKeyDown (KeyCode.JoystickButton2);

			exit_b = Input.GetKey (KeyCode.JoystickButton1);
			menu_b = Input.GetKey (KeyCode.JoystickButton9);
			inventory_b = Input.GetButtonDown ("Inventory_J");
		
			
		}
	}
		

	void CountSpeed()
	{
		if (SpeedCountTimer < Time.fixedTime) 
		{
			CorrentPos = ExPos;
			ExPos = transform.position;

			if (CorrentPos.x < ExPos.x + 0.3f && CorrentPos.x > ExPos.x - 0.3f )
				_normalHSpeed = 0;
				if(CorrentPos.y > ExPos.y - 0.3f && CorrentPos.y < ExPos.y + 0.3f)
				_normalVSpeed = 0;
			SpeedCountTimer = Time.fixedTime + 0.4f;
		}


		

	}

	public void SetDraw(bool dr)
	{
		draw = dr;
	}
    public void Save()
    {
        Inv.SaveInv();
    }


    /*private void OnTriggerStay2D(Collider2D c)
	{
		
		if(!coll_obj.Contains(c.gameObject))
		{
			coll_obj.Add(c.gameObject);
		}
		
	}
	
	private void OnTriggerExit2D(Collider2D c)
	{
		
		if(coll_obj.Contains(c.gameObject))
		{
			coll_obj.Remove(c.gameObject);
		}
		
	}*/
    public List<GameObject> Getcollob()
	{
		return coll_obj;
	}


}

