using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public Image pressureOrb;
    private Image pressureOrbFill;

    [SerializeField]
    public Image durabilityCapsule;
    private Image durabilityCapsuleFill;

    [SerializeField]
    public TextMeshProUGUI durabilityValue;

    [SerializeField]
    public Color highPressureColour;
    [SerializeField]
    public Color highMediumPressureColour;
    [SerializeField]
    public Color lowMediumPressureColour;
    [SerializeField]
    public Color lowPressureColour;

    private PlayerBase player;

    [SerializeField]
    public Button pauseResume;

    [SerializeField]
    public Button pauseMainMenu;

    [SerializeField]
    public TMP_Text scoreTxt;
    private int score;    
    
    [SerializeField]
    public TMP_Text TimerTxt;
    [SerializeField]
    private TMP_Text timerTextXtra;
    

    // Start is called before the first frame update
    void Start()
    {
        pressureOrbFill = pressureOrb.gameObject.transform.Find("Orb Fill").gameObject.GetComponent<Image>();
        durabilityCapsuleFill = durabilityCapsule.gameObject.transform.Find("Capsule Fill").gameObject.GetComponent<Image>();

        player = GameObject.Find("Player").GetComponent<PlayerBase>();

        pauseResume.gameObject.SetActive(false);
        pauseMainMenu.gameObject.SetActive(false);
        TimerTxt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePressureOrb();
        UpdateDurabilityCapsule();

        scoreTxt.text = score.ToString();

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            pauseResume.gameObject.SetActive(true);
            pauseMainMenu.gameObject.SetActive(true);
        }

        if (Time.timeScale == 1.0f)
        {
            GameHandler.instance.TogglePause(false);
            pauseResume.gameObject.SetActive(false);
            pauseMainMenu.gameObject.SetActive(false);
        }
        else
        {
            GameHandler.instance.TogglePause(true);
        }
    }

    private void UpdatePressureOrb()
    {
        float pressure = player.GetPressure();

        if(pressure > 75f)
        {
            pressureOrb.color = highPressureColour;
        }
        else if(pressure > 50.0f)
        {
            pressureOrb.color = highMediumPressureColour;
        }
        else if(pressure > 25.0f)
        {
            pressureOrb.color = lowMediumPressureColour;
        }
        else
        {
            pressureOrb.color = lowPressureColour;
        }

        pressureOrbFill.fillAmount = pressure / 100;

        if(pressure <= 0)
        {
            Death();
        }
    }

    private void UpdateDurabilityCapsule()
    {
        durabilityCapsuleFill.fillAmount = (float)player.GetDurability() / 100;

        durabilityValue.text = player.GetDurability().ToString();
    }
    public void Death()
    {
        Time.timeScale = 0;
        pauseMainMenu.gameObject.SetActive(true);
        timerTextXtra.gameObject.SetActive(true);
        TimerTxt.gameObject.SetActive(true);
    }

    public void SetScore(int s)
    {
        score = s;
    }
}
