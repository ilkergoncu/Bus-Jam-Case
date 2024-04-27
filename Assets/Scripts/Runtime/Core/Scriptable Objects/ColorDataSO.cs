using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The scriptable object class for the color date of buses and the characters.

[CreateAssetMenu(fileName = "ColorData", menuName = "Assets/ColorData")]
public class ColorDataSO : ScriptableObject
{
    public List<ColorData> ColorDatas = new List<ColorData>();
}


[Serializable]
public class ColorData
{
    public ColorType ColorType;
    public Color Color;
    public Material Material;
    public Material outLineMaterial;
}

