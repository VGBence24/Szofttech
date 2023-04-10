using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notch : MonoBehaviour
{
    public int money = 0;
    public int moneyChange = 0;
    public int population = 0;
    public int populationChange = 0;

    public float happiness = 1;
    public Sprite[] happinessSprites;
    public float[] happinessThresholds;

    private Color _great = new Color32(128, 255, 0, 255);
    private Color _bad = new Color32(255, 32, 0, 255);

    private GameObject _moneyTextBox;
    private GameObject _moneyChangeTextBox;
    private GameObject _happinessImage;
    private GameObject _populationTextBox;
    private GameObject _populationChangeTextBox;

    // Start is called before the first frame update
    void Start()
    {
        _moneyTextBox = transform.Find("MoneyPanel").Find("MoneyTextBox").gameObject;
        _moneyChangeTextBox = transform.Find("MoneyPanel").Find("MoneyChangeTextBox").gameObject;
        _happinessImage = transform.Find("HappinessImage").gameObject;
        _populationTextBox = transform.Find("PopulationPanel").Find("PopulationTextBox").gameObject;
        _populationChangeTextBox = transform.Find("PopulationPanel").Find("PopulationChangeTextBox").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        _moneyTextBox.GetComponent<TextMeshProUGUI>().text = "$" + money.ToString("N0");
        _moneyChangeTextBox.GetComponent<TextMeshProUGUI>().text = "$" + moneyChange.ToString("N0");
        _moneyChangeTextBox.GetComponent<TextMeshProUGUI>().color = moneyChange >= 0 ? _great : _bad;
        
        int i = 0;
        while (i < happinessThresholds.Length && happiness < happinessThresholds[i]) { i++; }
        _happinessImage.GetComponent<Image>().sprite = happinessSprites[i];

        _populationTextBox.GetComponent<TextMeshProUGUI>().text = population.ToString("N0");
        _populationChangeTextBox.GetComponent<TextMeshProUGUI>().text = populationChange.ToString("N0");
        _populationChangeTextBox.GetComponent<TextMeshProUGUI>().color = populationChange >= 0 ? _great : _bad;
    }
}