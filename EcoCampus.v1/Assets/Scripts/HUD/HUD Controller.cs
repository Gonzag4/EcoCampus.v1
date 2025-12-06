using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class HUDController : MonoBehaviour
{

    [Header("Items")]
    [SerializeField] private Image waterUIBar;
    [SerializeField] private Image woodUIBar;
    [SerializeField] private Image carrotUIBar;

    [Header("Tools")]
    //[SerializeField] private Image axeUI;
    //[SerializeField] private Image shovelUi;
    //[SerializeField] private Image bucketUI;

    //tentar fazer mais pretico em forma de lista 
    public List<Image> toolsUI = new List<Image>(); // -> lista de imagens para as ferramentas

    [SerializeField] private Color selectColor;
    [SerializeField] private Color alphaColor;

    private PlayerItems playerItems;
    private Player player;

    private void Awake()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        player = playerItems.GetComponent<Player>();
    }   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waterUIBar.fillAmount = 0f;
        woodUIBar.fillAmount = 0f;
        carrotUIBar.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        waterUIBar.fillAmount = playerItems.currentWater / playerItems.waterLimit;
        woodUIBar.fillAmount = playerItems.totalWood / playerItems.woodLimit;
        carrotUIBar.fillAmount = playerItems.carrots / playerItems.carrotsLimit;

        // toolsUI[player.handleObj].color = selectColor;

        // pelo escopo do projeto não tem problema usar um for dentro do update
        for (int i = 0; (i) < toolsUI.Count; (i)++)
        {
            if (i == player.handleObj)
            {
                toolsUI[i].color = selectColor;
            }
            else
            {
                toolsUI[i].color = alphaColor;
            }
        }
    }
}
