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
        var keys = perkRegistry.Keys.ToArray();
        int index1 = Random.Range(0, keys.Length);
        int index2;
        
        do
        {
            index2 = Random.Range(0, keys.Length);
        } while (index2 == index1);

        return (perkRegistry[keys[index1]], perkRegistry[keys[index2]]);
    }
}
