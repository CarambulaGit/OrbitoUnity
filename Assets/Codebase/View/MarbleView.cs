using System;
using IdleGame.Utilities;
using Logic;
using UnityEngine;
using UnityEngine.UI;

public class MarbleView : MonoBehaviour
{
    [SerializeField] private PairsList<Marble.Type, MarbleConfig> marbleConfigs;
    [SerializeField] private Image marbleImage;

    public void Initialize() { }

    public void Show(Marble marble) => 
        marbleImage.color = marbleConfigs[marble.MarbleType].Color;
}

[Serializable]
public class MarbleConfig
{
    [field: SerializeField] public Color Color { get; private set; }
}