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
    //список в котором все ноды
    public List<WFC> Nodes = new List<WFC>();
    //список который хранит позиции тайлов которые будем коллапсить
    private List<Vector2Int> _toCollapse = new List<Vector2Int>();
    //массив оффсетов чтобы проверять соседей
    private Vector2Int[] offsets = new Vector2Int[]
    {
        new Vector2Int(0, 1), //вверх
        new Vector2Int(0, -1), //низ
        new Vector2Int(-1, 0), //слево
        new Vector2Int(1, 0), //справа
    };
    private void Start()
    {
        _grid = new WFC[Width, Height];

        CollapseWorld();
    }
    //функция коллапса
    private void CollapseWorld()
    {
        _toCollapse.Clear();

        _toCollapse.Add(new Vector2Int(Width / 2, Height / 2));

        while (_toCollapse.Count > 0)
        {
            int x = _toCollapse[0].x;
            int y = _toCollapse[0].y;

            List<WFC> potentialNodes = new List<WFC>(Nodes);

            for (int i = 0; i < offsets.Length; i++)
            {
                Vector2Int neighbour = new Vector2Int(x + offsets[i].x, y + offsets[i].y);

                if(IsInsideGrid(neighbour))
                {
                    WFC neighbourNode = _grid[neighbour.x, neighbour.y];

                    if (neighbourNode != null)
                    {

                        switch (i)
                        {
                            case 0:
                                WhittleNodes(potentialNodes, neighbourNode.Bottom.CompatibleNodes);
                                break;
                            case 1:
                                WhittleNodes(potentialNodes, neighbourNode.Top.CompatibleNodes);
                                break;
                            case 2:
                                WhittleNodes(potentialNodes, neighbourNode.Right.CompatibleNodes);
                                break;
                            case 3:
                                WhittleNodes(potentialNodes, neighbourNode.Left.CompatibleNodes);
                                break;
                        }

                    }
                    else
                    {
                        if(!_toCollapse.Contains(neighbour)) _toCollapse.Add(neighbour);
                    }
                }

            }

            if (potentialNodes.Count < 1) 
            {
                _grid[x, y] = Nodes[0];
                Debug.Log("Попытка коллапсить на " + x + ", " + y + " не нашла подходящих нодов и поставила дефолтный");
            }
            else
            {
                _grid[x, y] = potentialNodes[Random.Range(0, potentialNodes.Count)];
            }

            GameObject newNode = Instantiate(_grid[x, y].Prefab, new Vector2(x,y), Quaternion.identity);

            _toCollapse.RemoveAt(0);
        }
    }
    //Проверка списка потенциальных нодов с тем, которые подходят и убирает все неподходящие из списка потенциальных нодов
    private void WhittleNodes(List<WFC> potentialNodes, List<WFC> validNodes)
    {
        for (int i = potentialNodes.Count - 1; i > -1; i--) 
        {
            if (!validNodes.Contains(potentialNodes[i]))
            {
                potentialNodes.RemoveAt(i);
            }
        }
    }

    private bool IsInsideGrid (Vector2Int v2int)
    {
        if (v2int.x > -1 && v2int.x < Width && v2int.y > -1 && v2int.y < Height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
