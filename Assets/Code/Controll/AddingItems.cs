using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
public class AddingItems: MonoBehaviour {
	
	private Inventory inv;
	public bool ItemAdded{ get; set;}

    public int NeededItem = -1;
    public int ItemNeededNum = 0;

    public int[] ItemAdding;
	public int[] ItemAddingNum;
    private Movement pl;

    public bool DontDestroy;
    

	void Start () {
	
		inv = GameObject.Find("Vasilis").GetComponent<Inventory>();
        pl = GameObject.Find("Vasilis").GetComponent<Movement>();
        if (!DontDestroy&&PlayerPrefs.GetInt(name + SceneManager.GetActiveScene().name + "Destroy") == 1) Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () 
	{
        if (pl.enter_b && pl.Getcollob().Contains(gameObject))
        {
            if (NeededItem == -1) ContainersF();
            else if (inv.CheckCorrentItem() == NeededItem && inv.showinvent && inv.CheckCorrentItemNum() >= ItemNeededNum)
            {
                ContainersF();
               if(ItemNeededNum>0) inv.RemoveSlot(inv.correntSlot);
               
            }
        }
    }

	void ContainersF()
	{
			for (int i = 0; i < ItemAdding.Length; i++) {
				inv.AddItem(ItemAdding[i],ItemAddingNum[i]);

            if (i == ItemAdding.Length - 1)
            {
                if (!DontDestroy)
                {
                    Destroy(gameObject);
                    PlayerPrefs.SetInt(name + SceneManager.GetActiveScene().name + "Destroy", 1);
                }
                inv.SaveInv();

            }
			}
	}

}