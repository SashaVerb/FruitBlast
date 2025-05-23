using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PerkSystem : MonoBehaviour
{
    public static PerkSystem Instance { get; private set; }

    private static Dictionary<Type, Perk> perkRegistry = new();
    
    private void Awake()
    {
        Instance = this;
    }

    public void AddPerk<T>(T perk) where T : Perk
    {
        perkRegistry[typeof(T)] = perk;
    }

    public static T GetPerk<T>() where T : Perk
    {
        if (perkRegistry.TryGetValue(typeof(T), out Perk perk))
        {
            return perk as T;
        }
        else 
            return null;
    }

    public void UpgradePerk<T>() where T : Perk
    {
        if (perkRegistry.TryGetValue(typeof(T), out Perk perk))
        {
            perk.Upgrade();
        }
    }
    
    public void UpgradePerk(Type type)
    {
        if (perkRegistry.TryGetValue(type, out Perk perk))
        {
            perk.Upgrade();
        }
    }

    public (Perk, Perk) GetTwoPerks()
    {
        var keys = perkRegistry.Keys.ToList();
        var firstPerk = keys[Random.Range(0, keys.Count)];
        keys.Remove(firstPerk);
        var secondPerk = keys[Random.Range(0, keys.Count)];

        return (perkRegistry[firstPerk], perkRegistry[secondPerk]);
    }
}
