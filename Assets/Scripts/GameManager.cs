using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private TextBox TB;
    private PlayerController PC;

    public GameObject hungerText;
    public GameObject happinessText;
    public GameObject healthText;
    public GameObject actionsCountText;

    public GameObject foodBar;
    public GameObject healthBar;

    //public Sprite foodIcons;

    public GameObject player;

    public static bool active = true;

    void Start()
    {
        TB = FindObjectOfType<TextBox>();
        PC = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        // MOSTRA I VALORI NUMERICI DELLE NECESSITA'

        hungerText.GetComponent<Text>().text = player.GetComponent<PlayerController>()._hunger.ToString();
        happinessText.GetComponent<Text>().text = player.GetComponent<PlayerController>()._happiness.ToString();
        healthText.GetComponent<Text>().text = player.GetComponent<PlayerController>()._hygiene.ToString();
        actionsCountText.GetComponent<Text>().text = player.GetComponent<PlayerController>()._actionsCount.ToString();


        // MOSTRA IL BOX CON I TESTI   

        if ((PC._hunger < 50 && PC._hunger > 10) && active == true)
        {
            TB.ShowBar("Sei molto affamata");
            active = false;
        }
    }

    void FixedUpdate() { Screen.SetResolution(480, 800, true); }

    public void buttonBehaviour(int i)
    {
        switch (i)
        {
            case (0):
            default:

                break;
            case (1):

                break;
            case (2):
                foodBar.SetActive(!foodBar.activeInHierarchy);
                break;
            case (3):

                break;
            case (4):
                healthBar.SetActive(!healthBar.activeInHierarchy);
                /*player.GetComponent<PlayerController>().SavePlayer();
                Application.Quit();*/
                break;
        }
    }

    /*public void HealthPanel()
    {
        Toggle(healthPanel);
    }*/

    /*public void SelectFood(int i)
    {
        //player.GetComponent<Player>().UpdateHunger();
        //Toggle(foodPanel);                                                                          // Chiudi il pannello cibo
    }*/

    public void Toggle(GameObject g)
    {
        if (g.activeInHierarchy)
        {
            g.SetActive(false);
        }
    }
}
