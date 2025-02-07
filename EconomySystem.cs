using UnityEngine;
using System.Collections.Generic;

public class EconomySystem : MonoBehaviour
{
    private List<Currency> currencies = new List<Currency>();
    private List<TradeRoute> tradeRoutes = new List<TradeRoute>();

    void Start()
    {
        // Initialize the economy system
        currencies = new List<Currency>();
        tradeRoutes = new List<TradeRoute>();
    }

    void Update()
    {
        // Update the economy system
        foreach (Currency currency in currencies)
        {
            // Update the currency's value
            currency.value += currency.inflationRate * Time.deltaTime;
        }

        foreach (TradeRoute tradeRoute in tradeRoutes)
        {
            // Update the trade route's prices
            tradeRoute.buyPrice += tradeRoute.inflationRate * Time.deltaTime;
            tradeRoute.sellPrice += tradeRoute.inflationRate * Time.deltaTime;
        }
    }

    public void AddCurrency(Currency currency)
    {
        // Add a new currency to the economy system
        currencies.Add(currency);
    }

    public void AddTradeRoute(TradeRoute tradeRoute)
    {
        // Add a new trade route to the economy system
        tradeRoutes.Add(tradeRoute);
    }
}

[System.Serializable]
public class Currency
{
    public string name;
    public float value;
    public float inflationRate;
}

[System.Serializable]
public class TradeRoute
{
    public string name;
    public float buyPrice;
    public float sellPrice;
    public float inflationRate;
}
