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

    public GameObject hungerBar;
    public GameObject happinessBar;
    public GameObject hygieneBar;

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

    public void buttonBehaviour(int i)                                                                  // Onclick Buttons
    {
        switch (i)
        {
            case (0):
            default:
                hungerBar.SetActive(!hungerBar.activeInHierarchy);                                      // Toggle HungerBar 
                happinessBar.SetActive(false);                                                          // Chiudi il pannello Happiness
                hygieneBar.SetActive(false);                                                            // Chiudi il pannello Hygiene
                break;
            case (1):
                happinessBar.SetActive(!happinessBar.activeInHierarchy);                                // Toggle HappinessBar
                hungerBar.SetActive(false);                                                             // Chiudi il pannello Hunger
                hygieneBar.SetActive(false);                                                            // Chiudi il pannello Hygiene
                break;
            case (2):
                hygieneBar.SetActive(!hygieneBar.activeInHierarchy);                                    // Toggle HygieneBar                                      
                hungerBar.SetActive(false);                                                             // Chiudi il pannello Hunger
                happinessBar.SetActive(false);                                                          // Chiudi il pannello Happiness
                break;
            case (3):

                break;
            case (4):
                                                           

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
