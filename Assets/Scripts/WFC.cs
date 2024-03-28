using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "WFC", menuName = "WFC/Node")]
public class WFC : ScriptableObject
{
    public string Name;
    public GameObject Prefab;
    public WFC_Connection Top;
    public WFC_Connection Bottom;
    public WFC_Connection Left;
    public WFC_Connection Right;


}

[System.Serializable]
public class WFC_Connection
{
    public List<WFC> CompatibleNodes = new List<WFC>();
}


