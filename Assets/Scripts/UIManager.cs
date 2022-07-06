using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text pointCounter;

    [SerializeField]
    private Text diamondCounter;

    public int points = 0;
    public int diamonds = 0;


    // Use this for initialization
    void Start()
    {
        // setup as listner for events
        EventManager.AddListener<AddPointEvent>(updatePoints);
        EventManager.AddListener<AddDiamondEvent>(updateDiamonds);
        EventManager.AddListener<BonusPoint>(updateCurrentPointValue);
    }

    // Update is called once per frame
    void Update()
    {
        pointCounter.text = points.ToString();
        diamondCounter.text = diamonds.ToString();
    }


    public void updatePoints(AddPointEvent evt)
    {
        points += GameCostants.current_point_value;
    }

    public void updateDiamonds(AddDiamondEvent evt)
    {
        diamonds ++;
    }

    public void updateCurrentPointValue(BonusPoint evt)
    {
        GameCostants.current_point_value = GameCostants.current_point_value * 2;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<AddPointEvent>(updatePoints);
        EventManager.RemoveListener<AddDiamondEvent>(updateDiamonds);
        EventManager.RemoveListener<BonusPoint>(updateCurrentPointValue);
    }

}
