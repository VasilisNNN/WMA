using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DoorCol : MonoBehaviour {
	//public bool EnterToTheDoor{ get; set; }
	public string LevelName;
	private bool DoorColl;
	public bool LoadLocation = true;
	private Inventory Inv;
    

	private bool SW;

	private Movement pl;
    private List<GameObject> coll_obj = new List<GameObject>();
    private bool enter;
    public int NeededItemm = -1;
    public bool NoEnter;
    private void Start()
	{
		if (GameObject.Find ("Player") != null) {
			Inv = GameObject.Find ("Player").GetComponent<Inventory> ();
			pl = GameObject.Find("Player").GetComponent<Movement> ();
		}

        
    }

    void Update()
    {

        //	print ("s "  + PlayerPrefs.GetString ("CorrLevel"));
       
        
            enter = pl.enter_b;
            coll_obj = pl.Getcollob();
        

        if (LevelName != null)
        {
            if (!NoEnter)
            {
                if (enter && coll_obj.Contains(gameObject))
                {
                    if (NeededItemm > -1)
                    {
                        if (Inv.CheckCorrentItem() == NeededItemm && Inv.showinvent)
                            Location();
                    }
                    else if (LoadLocation) Location();

                }
            }
            else
            {
                if (coll_obj.Contains(gameObject))
                {
                    if (NeededItemm > -1)
                    {
                        if (Inv.CheckCorrentItem() == NeededItemm && Inv.showinvent)
                            Location();
                    }
                    else if (LoadLocation) Location();

                }

            }
        }


    }
    void Location()
    {
        pl.Save();
        SavePrev();
        SceneManager.LoadScene(LevelName);
        //Cursor.SetCursor (ExitDoor, Vector2.zero, CursorMode.Auto);
        PlayerPrefs.SetString("CorrLoadingLevel", LevelName);

    }

    void SavePrev()
    {
        PlayerPrefs.SetString("PrevLoadingLevel", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("XPos", pl.transform.position.x);
        PlayerPrefs.SetFloat("YPos", pl.transform.position.y);
    }



}
