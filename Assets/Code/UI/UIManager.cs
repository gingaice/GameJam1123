using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public Image pressureOrb;
    private Image pressureOrbFill;

    [SerializeField]
    public Color lowPressureColour;
    [SerializeField]
    public Color lowMediumPressureColour;
    [SerializeField]
    public Color highMediumPressureColour;
    [SerializeField]
    public Color highPressureColour;

    private PlayerBase player;

    [SerializeField]
    public Button pauseResume;

    [SerializeField]
    public Button pauseMainMenu;


    // Start is called before the first frame update
    void Start()
    {
        pressureOrbFill = pressureOrb.gameObject.transform.Find("Orb Fill").gameObject.GetComponent<Image>();

        player = GameObject.Find("Player").GetComponent<PlayerBase>();

        pauseResume.gameObject.SetActive(false);
        pauseMainMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePressureOrb();

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            pauseResume.gameObject.SetActive(true);
            pauseMainMenu.gameObject.SetActive(true);
        }

        if (Time.timeScale == 1.0f)
        {
            pauseResume.gameObject.SetActive(false);
            pauseMainMenu.gameObject.SetActive(false);
        }
    }

    private void UpdatePressureOrb()
    {
        float pressure = player.GetPressure();

        if(pressure < 25f)
        {
            pressureOrb.color = lowPressureColour;
        }
        else if(pressure < 50.0f)
        {
            pressureOrb.color = lowMediumPressureColour;
        }
        else if(pressure < 75.0f)
        {
            pressureOrb.color = highMediumPressureColour;
        }
        else
        {
            pressureOrb.color = highPressureColour;
        }

        pressureOrbFill.fillAmount = pressure / 100;
    }
}
