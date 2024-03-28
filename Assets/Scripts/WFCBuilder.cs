using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFCBuilder : MonoBehaviour
{
    //размер грида
    [SerializeField] private int Width;
    [SerializeField] private int Height;
    
    //массив который будет хранить тайлы
    private WFC[,] _grid;

    public List<WFC> Nodes = new List<WFC>();

    private List<Vector2Int> _toCollapse = new List<Vector2Int>();
}
