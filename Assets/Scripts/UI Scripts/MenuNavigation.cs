using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] GameObject activeMenu;
    [SerializeField] GameObject[] availableMenus;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void ChangeCurrentMenu(GameObject newActiveMenu)
    {
        foreach(GameObject menu in availableMenus)
        {
            menu.SetActive(false);
        }

        newActiveMenu.SetActive(true);
    }


    public void EnableGameplayMenu()
    {
        ChangeCurrentMenu(availableMenus[0]);
    }
    public void EnableVideoMenu()
    {
        ChangeCurrentMenu(availableMenus[1]);
    }
    public void EnableAudioMenu()
    {
        ChangeCurrentMenu(availableMenus[3]);
    }
    public void EnableExitPrompt()
    {
        ChangeCurrentMenu(availableMenus[4]);
    }


}
