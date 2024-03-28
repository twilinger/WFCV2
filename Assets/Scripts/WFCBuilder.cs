using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFCBuilder : MonoBehaviour
{
    //������ �����
    [SerializeField] private int Width;
    [SerializeField] private int Height;
    
    //������ ������� ����� ������� �����
    private WFC[,] _grid;

    public List<WFC> Nodes = new List<WFC>();

    private List<Vector2Int> _toCollapse = new List<Vector2Int>();
}
