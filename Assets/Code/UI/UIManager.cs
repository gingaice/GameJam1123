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
    public Color highPressureColour;
    [SerializeField]
    public Color highMediumPressureColour;
    [SerializeField]
    public Color lowMediumPressureColour;
    [SerializeField]
    public Color lowPressureColour;

    private PlayerBase player;

    // Start is called before the first frame update
    void Start()
    {
        pressureOrbFill = pressureOrb.gameObject.transform.Find("Orb Fill").gameObject.GetComponent<Image>();

        player = GameObject.Find("Player").GetComponent<PlayerBase>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePressureOrb();
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
    }
}
