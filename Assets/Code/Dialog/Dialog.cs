using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour {
	public  List<TextA> LinesRu;
	public  List<TextA> LinesEn;
	public Texture[] Face;

	public bool CollisionCase  =true;


	private Rect rectlable = new Rect(0,0,Screen.width-100,100);
	private GUISkin skin;


	private string[] texB;
	private int finalLine = 1;
	private int finalLinePl = 1;
	private int CorrentLine = 0;

	public bool PlayIn = false;

	public string DialogPartName{ get; set;}
	private Movement pl;
	//public bool Picked{ get; set;}
	private float MinDialogTime;
    private List<GameObject> coll_obj = new List<GameObject>();
    private bool enter;

    public bool NoEnter = false;
    public bool InMind = false;
    public bool PlayOnes = false;
    private int SkinNum = 0;
    private float fheight, fwidth, ymove;
    private Rect pos;
    private Rect dialogpos;
    private float move_dialog = Screen.width;

    private void Start () {
        PlayerPrefs.SetInt("Language",-1);
       // MinDialogTime = -1;
        skin = Resources.Load<GUISkin> ("UI/Slot");
	
		if(GameObject.Find("Player")!=null)
			pl = GameObject.Find("Player").GetComponent<Movement> ();
        
       // PlayerPrefs.DeleteAll();
    }

	private void Update()
	{
        
        enter = pl.enter_b;
        coll_obj = pl.Getcollob();
        

       

        if (LinesRu!=null&& PlayerPrefs.GetInt("Language") == -1) {

            if (PlayerPrefs.GetInt("Day") < LinesRu.Count) texB = LinesRu[PlayerPrefs.GetInt("Day")].line;
            else texB = LinesRu[LinesRu.Count - 1].line;
        }
        if (LinesEn !=null&& PlayerPrefs.GetInt("Language") == 1)
        {
            
            if (PlayerPrefs.GetInt("Day")< LinesEn.Count) texB = LinesEn[PlayerPrefs.GetInt("Day")].line;
            else texB = LinesEn[LinesEn.Count-1].line;
            
        }
		
		if (CollisionCase) {

            if (coll_obj.Contains(gameObject))
            {

                if (enter && MinDialogTime < Time.fixedTime&&!NoEnter&& pl.DayFinish>=1&&texB!=null)
                {

                    if (PlayIn && CorrentLine < texB.Length)
                    {
                        CorrentLine++;
                        MinDialogTime = Time.fixedTime + 0.2f;
                    }

                    if (!PlayIn)
                    {
                        PlayIn = true;
                        MinDialogTime = Time.fixedTime + 0.2f;
                    }

                    if (PlayIn && CorrentLine == texB.Length)
                    {
                        PlayIn = false;

                        CorrentLine = 0;
                        MinDialogTime = Time.fixedTime + 0.2f;
                    }

                }

                if (NoEnter)
                {
                    if (PlayOnes)
                    {
                        if (PlayerPrefs.GetInt(name + SceneManager.GetActiveScene().name) != 1)
                        {
                            PlayIn = true;

                        }
                    }
                    else
                        PlayIn = true;


                    Save();
                }
            
            
            

                // if((pl._horizontal!=0|| pl._vertical != 0)&& PlayIn&& MinDialogTime < Time.fixedTime) PlayIn = false;


            }
            else 
				if(PlayIn)
				PlayIn = false;
				

				
			


		
		}
	}
	

	// Update is called once per frame
	private void OnGUI () {

        if (LinesRu != null && PlayerPrefs.GetInt("Language") == -1)
        {

            if (PlayerPrefs.GetInt("Day") < LinesRu.Count) texB = LinesRu[PlayerPrefs.GetInt("Day")].line;
            else texB = LinesRu[LinesRu.Count - 1].line;
        }
        if (LinesEn != null && PlayerPrefs.GetInt("Language") == 1)
        {

            if (PlayerPrefs.GetInt("Day") < LinesEn.Count) texB = LinesEn[PlayerPrefs.GetInt("Day")].line;
            else texB = LinesEn[LinesEn.Count - 1].line;

        }

        if (PlayIn == true&&CorrentLine<texB.Length) {

            float XPosD = Camera.main.WorldToScreenPoint(transform.position).x - 100;
            float YPosD = Screen.height - Camera.main.WorldToScreenPoint(transform.position).y - 200;
            if (YPosD < 0) YPosD = 0;
            

            SkinNum = 2;
            if (InMind)
            {
                SkinNum = 4;
                
                XPosD = Camera.main.WorldToScreenPoint(pl.transform.position).x-150;

                if (Screen.height - Camera.main.WorldToScreenPoint(pl.transform.position).y - 450 > 0)
                    YPosD = Screen.height - Camera.main.WorldToScreenPoint(pl.transform.position).y - 450;
                else YPosD = 0;
                pos = new Rect(fheight + move_dialog, 0, fwidth, fheight);
                rectlable = new Rect(XPosD, YPosD, 300, 150);
            }

           rectlable = new Rect(XPosD, YPosD, fwidth, fheight);
            DrawLines();
            
				
			    }


	}
    void PosM(float stringslength)
    {
        if ((17 * stringslength / 16) >= 75)
            fheight = 35 + 17 * stringslength / 16;
        else
            fheight = 100;
        fwidth = 25 * 10;
        

    }
   
    void DrawLines()
    {

       
        if (texB.Length > 0)
                {PosM(texB[PlayerPrefs.GetInt(name + "Dialog")].Length);

                 GUI.Box(rectlable, texB[CorrentLine], skin.customStyles[SkinNum]);
                }

        if (Face.Length > 1)
        {
            if (CorrentLine % 2 == 0)
            {
                GUI.DrawTexture(new Rect(rectlable.x + rectlable.width, 0f, rectlable.height, rectlable.height), Face[0]);
            }
            else GUI.DrawTexture(new Rect(rectlable.x + rectlable.width, 0f, rectlable.height, rectlable.height), Face[1]);
        }


    }
    public Texture[] GetFace()
	{return Face;}

	public Texture GetFace(int i)
	{return Face[i];}


    void Save()
    {
        PlayerPrefs.SetInt(name + SceneManager.GetActiveScene().name, 1);
    }
public void SetFinalLine(int FL)
	{
		finalLine = FL;
	}
	
public void SetFinalLinePl(int FL)
	{
		finalLinePl = FL;
	}

public void SetDialogPartName(string PL)
	{
		DialogPartName = PL;
	}



public int GetFinalLine()
	{
		return finalLine;
	}


public void SetTextField(Rect Field)
	{
		rectlable = Field;
	}

		
}
