using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "BallTypes", menuName = "Scriptable Objects/Balls/BallTypes")]
public class BallTypes : ScriptableObject
{
    [SerializeField] public BallInfo[] info;
    
    [Serializable]
    public struct BallInfo
    {
        public Ball instance;
        public float chance;
    }
}
