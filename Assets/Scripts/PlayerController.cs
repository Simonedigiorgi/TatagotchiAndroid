using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;

    private TextBox textBox;
    private GameManager gameManager;

    [BoxGroup("Bisogni")] [SerializeField] private int hunger;                          // Fame
    [BoxGroup("Bisogni")] [SerializeField] private int happiness;                       // Felicità
    [BoxGroup("Bisogni")] [SerializeField] private int hygiene;                         // Igiene

    // Popup

    [BoxGroup("Oggetti")] [SerializeField] private int poo;                                                   // Cacca

    [BoxGroup("Generali")] [SerializeField] private int actionsCount;                                          // Quante azioni puoi fare
    [BoxGroup("Generali")] public bool isActive;

    private int hungerValue;
    private int happinessValue;
    private int hygieneValue;

    // BISOGNI / VALORI NEL TEMPO

    [BoxGroup("Bisogni, valore nel tempo")] public bool isNecessitiesActive = true;

    [BoxGroup("Bisogni, valore nel tempo")] public int hungerForTime;
    [BoxGroup("Bisogni, valore nel tempo")] public int happinessForTime;
    [BoxGroup("Bisogni, valore nel tempo")] public int hygieneForTime;

    //private int pooValue;

    private bool serverTime;


    void Start()
    {

        isActive = true;

        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        textBox = FindObjectOfType<TextBox>();

        //PlayerPrefs.SetString("then", "02/24/2018 21:00:00");
        updateStatus();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && isActive == true)
        {
            Vector2 v = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(v), Vector2.zero);

            if (hit == true)
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    // Fai succedere qualcosa
                }
            }
        }
    }

    public void updateStatus()
    {

        #region STATUS
        if (!PlayerPrefs.HasKey("hunger"))
        {
            hunger = 100;
            PlayerPrefs.SetInt("hunger", hunger);
        }
        else
        {
            hunger = PlayerPrefs.GetInt("hunger");
        }

        // FELICITA'

        if (!PlayerPrefs.HasKey("happiness"))
        {
            happiness = 100;
            PlayerPrefs.SetInt("happiness", happiness);
        }
        else
        {
            happiness = PlayerPrefs.GetInt("happiness");
        }

        // IGENE

        if (!PlayerPrefs.HasKey("hygiene"))
        {
            hygiene = 100;
            PlayerPrefs.SetInt("hygiene", hygiene);
        }
        else
        {
            hygiene = PlayerPrefs.GetInt("hygiene");
        }

        // CACCA (Ogni TOT ore attiva l'oggetto in scena)

        if (!PlayerPrefs.HasKey("poo"))
        {
            poo = 4;
            PlayerPrefs.SetInt("poo", poo);
        }
        else
        {
            poo = PlayerPrefs.GetInt("poo");
        }

        // CONTA DELLE AZIONI 

        if (!PlayerPrefs.HasKey("actionsCount"))
        {
            actionsCount = 5;
            PlayerPrefs.SetInt("actionsCount", actionsCount);
        }
        else
        {
            actionsCount = PlayerPrefs.GetInt("actionsCount");
        }

        // PRENDI L'ORA CORRENTE

        if (!PlayerPrefs.HasKey("then"))
        {
            PlayerPrefs.SetString("then", getStringTime());
        }
        #endregion

        #region STATISTICHE

        TimeSpan ts = getTimeSpan();

        #region HUNGER
        if(isNecessitiesActive == true)
        {
            hunger -= (int)(ts.TotalMinutes * hungerForTime);
        }
                                                                     // Sottrae ogni ora
        if (hunger >= 100)
        {
            hunger = 100;
        }
        else if (hunger <= 0)
        {
            hunger = 0;
            Debug.Log("fine partita");
        }
        #endregion

        #region HAPPINESS
        if(isNecessitiesActive == true)
        {
            happiness -= (int)(ts.TotalMinutes * happinessForTime);
        }
                                                            // Sottrae ogni ora
        if (happiness >= 100)
        {
            happiness = 100;
        }
        else if (happiness <= 0)
        {
            happiness = 0;
            Debug.Log("sei infelice");
        }
        #endregion

        #region HYGIENE
        if(isNecessitiesActive == true)
        {
            hygiene -= (int)(ts.TotalMinutes * hygieneForTime);
        }
                                                                     // Sottrae ogni ora
        if (hygiene >= 100)
        {
            hygiene = 100;
        }
        else if (hygiene <= 0)
        {
            hygiene = 0;
            Debug.Log("stai puzzando");
        }
        #endregion

        #region POO
        poo -= (int)(ts.TotalSeconds * 1);                                                             // Sottrae ogni ora
        if (poo < 0)
        {
            poo = 0;
            gameManager.objectButtons[0].gameObject.SetActive(true);
        }
        #endregion

        #region ACTIONS
        actionsCount += (int)(ts.TotalSeconds * 1);
        if (actionsCount >= 5)
        {
            actionsCount = 5;
        }
        else if (actionsCount <= 0)
        {
            actionsCount = 0;
        }
        #endregion

        #endregion

        #region SERVER TIME
        if (serverTime)
        {
            updateServer();
        }
        else
        {
            InvokeRepeating("updateDevice", 0f, 30f);
        }
        #endregion

    }

    #region UPDATE DEVICE (ONLY DEVICE)
    private void updateServer()
    {
        // Update server
    }

    private void updateDevice()
    {
        PlayerPrefs.SetString("then", getStringTime());
    }

    TimeSpan getTimeSpan()
    {
        if (serverTime)
        {
            return new TimeSpan();
        }
        else
        {
            return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("then"));
        }
    }

    private string getStringTime()
    {
        DateTime now = DateTime.Now;
        return now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
    }
    #endregion

    #region GET & SET
    public int _hunger
    {
        get { return hunger; }
        set { hunger = value; }
    }

    public int _happiness
    {
        get { return happiness; }
        set { happiness = value; }
    }

    public int _hygiene
    {
        get { return hygiene; }
        set { hygiene = value; }
    }

    public int _poo
    {
        get { return poo; }
        set { poo = value; }
    }

    public int _actionsCount
    {
        get { return actionsCount; }
        set { actionsCount = value; }
    }
    #endregion

    // AGGIORNA FAME

    public void UpdateHunger()
    {
        if (isActive == true && hungerValue != 0)
        {
            if (hungerValue == 5)
            {
                StartCoroutine(HungerAnimation("Eat_0", 1.2f));                                    // Nome Animazione, Tempo di attesa, Valore dell'oggetto
            }

            if (hungerValue == 3)
            {
                StartCoroutine(HungerAnimation("Eat_1", 1.2f));                                    // Nome Animazione, Tempo di attesa, Valore dell'oggetto
            }
        }

        SavePlayer();
    }

    // VALORI DEL CIBO

    public void HungerValue_0()                                                                            // Valore della mela (CIBO)
    {
        hungerValue = 5;
        gameManager.hungerBar.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = "Mangia una mela";
    }

    public void HungerValue_1()                                                                           // Valore del caffè (CIBO)
    {
        hungerValue = 3;
        gameManager.hungerBar.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = "Prendi un caffè";
    }

    public void HungerValue_2()                                                                           // Valore del caffè (CIBO)
    {
        hungerValue = 3;
        gameManager.hungerBar.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = "Mangia qualcosa ";
    }

    // AGGIORNA FELICITA'

    public void UpdateHappiness()
    {
        if (isActive == true && happinessValue != 0)
        {
            if (happinessValue == 5)
            {
                StartCoroutine(HappinessAnimation("Eat_0", 1.2f));                                    // Nome Animazione, Tempo di attesa, Valore dell'oggetto
            }

            if (happinessValue == 3)
            {
                StartCoroutine(HappinessAnimation("Eat_1", 1.2f));                                    // Nome Animazione, Tempo di attesa, Valore dell'oggetto
            }
        }

        SavePlayer();
    }

    // VALORI DELLA FELICITA'

    public void HappinessValue_0()
    {
        happinessValue = 5;
        gameManager.happinessBar.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = "Balla";
    }

    public void HappinessValue_1()
    {
        happinessValue = 3;
        gameManager.happinessBar.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = "Gioca";
    }

    public void HappinessValue_2()
    {
        happinessValue = 3;
        gameManager.happinessBar.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = "Fai";
    }

    // AGGIORNA HYGIENE (Hygiene)

    public void UpdateHygiene()                                                                     
    {
        if (isActive == true && hygieneValue != 0)
        {
            if(hygieneValue == 5)
            {
                StartCoroutine(HygieneAnimation("Eat_0", 1.2f));                                    // Nome Animazione, Tempo di attesa, Valore dell'oggetto
            }

            if (hygieneValue == 3)
            {
                StartCoroutine(HygieneAnimation("Eat_1", 1.2f));                                    // Nome Animazione, Tempo di attesa, Valore dell'oggetto
            }

        }

        SavePlayer();
    }

    // VALORI DELL/IGIENE (Hygiene)

    public void HygieneValue_0()
    {
        hygieneValue = 5;
        gameManager.hygieneBar.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = "Lavati con la spugna";
    }

    public void HygieneValue_1()
    {
        hygieneValue = 3;
        gameManager.hygieneBar.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = "Lavati con la pompa dell'acqua";
    }

    public void HygieneValue_2()
    {
        hygieneValue = 3;
        gameManager.hygieneBar.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = "Lavati con il sapone";
    }

    // AGGIORNA CACCA (POO)

    public void UpdatePoo()
    {
        poo = 4;                                                                                    // Resetta (poo)  
        gameManager.objectButtons[0].gameObject.SetActive(false);                                   // Disattiva il Tasto (Poo)
        happiness += 10;                                                                            // Aumenta la felicità
        hygiene += 10;                                                                              // Aumenta l'igiene

        SavePlayer();                                                                               // SALVA IL GIOCATORE
    }

    // SALVA IL GIOCATORE

    public void SavePlayer()
    {
        if (!serverTime)
        {
            updateDevice();
            PlayerPrefs.SetInt("hunger", hunger);
            PlayerPrefs.SetInt("happiness", happiness);
            PlayerPrefs.SetInt("hygiene", hygiene);

            PlayerPrefs.SetInt("poo", poo);

            PlayerPrefs.SetInt("actionsCount", actionsCount);
        }
    }

    #region COROUTINES (HUNGER, HAPPINESS, HYGIENE)

    public IEnumerator HungerAnimation(string name, float time)
    {
        if(hunger < 100 && actionsCount <= 5 && actionsCount > 0)
        {
            // NOME ANIMAZIONE
            // TEMPO DI ATTESA

            hunger += hungerValue;                                                                        // Valore del cibo
            actionsCount -= 1;

            anim.SetTrigger(name);

            isActive = false;
            yield return new WaitForSeconds(time);
            isActive = true;
        }
        else
        {
            anim.SetTrigger("No");
            textBox.ShowBar("No! non mi va");
            isActive = false;
            yield return new WaitForSeconds(time);
            isActive = true;
        }
    }

    public IEnumerator HappinessAnimation(string name, float time)
    {
        if (happiness < 100 && actionsCount <= 5 && actionsCount > 0)
        {
            // NOME ANIMAZIONE
            // TEMPO DI ATTESA

            happiness += happinessValue;
            actionsCount -= 1;

            anim.SetTrigger(name);

            isActive = false;
            yield return new WaitForSeconds(time);
            isActive = true;
        }
        else
        {
            anim.SetTrigger("No");
            textBox.ShowBar("No! sono troppo stanca");
            isActive = false;
            yield return new WaitForSeconds(time);
            isActive = true;
        }
    }

    public IEnumerator HygieneAnimation(string name, float time)
    {
        if (hygiene < 100 && actionsCount <= 5 && actionsCount > 0)
        {
            // NOME ANIMAZIONE
            // TEMPO DI ATTESA

            hygiene += hygieneValue;
            actionsCount -= 1;

            anim.SetTrigger(name);

            isActive = false;
            yield return new WaitForSeconds(time);
            isActive = true;
        }
        else
        {
            anim.SetTrigger("No");
            textBox.ShowBar("No! sono pulita anche dove pensavo non fosse possibile");
            isActive = false;
            yield return new WaitForSeconds(time);
            isActive = true;
        }
    }

    #endregion
}
