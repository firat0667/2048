using System.Collections;
using System.Collections.Generic;
using MyGrid;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _isLeft => Input.GetKeyDown(KeyCode.A);
    private bool _isRight => Input.GetKeyDown(KeyCode.D);
    private bool _isUp => Input.GetKeyDown(KeyCode.W);
    private bool _isDown => Input.GetKeyDown(KeyCode.S);
    [SerializeField]
    NumberController numberPrefabs;
    private void Start()
    {
        Spawn();
    }


    private void Update()
    {
        if (_isLeft)
            Move(Direction.Left);
        if (_isRight)
            Move(Direction.Right);
        if (_isUp)
            Move(Direction.Up);
        if (_isDown)
            Move(Direction.Down);


    }
    
    private void Move(Direction direction)
    {
        foreach(var tile in GridManager.Instance.listTile)
        {
            tile.IsNew = false;
        }
        Debug.Log(direction);
        var result = GridManager.Instance.GetPriorityTile(direction);
        List<GameObject> listDestroy = new List<GameObject>();
           Debug.Log(result);
       foreach(var listTile in result )
        {
            foreach(var tile in listTile)
            {
                if (tile.numberController)
                {

                    bool isMerge = false;
                    TileController target = tile;
                    TileController next = tile;
                 for(int i=0; i<4;i++)
                    {
                         next = next.GetNeighbour(direction);
                        if (next == null) break;
                        if (next.numberController)
                        {
                            if (next.numberController.Number == tile.numberController.Number&& !next.IsNew)
                            {
                                isMerge = true;
                                target = next;
                            }
                            break;
                        }
                        target = next;
                    }
                    if (isMerge)
                    {
                        var value = tile.numberController.Number;
                        listDestroy.Add(tile.numberController.gameObject);
                        listDestroy.Add(target.numberController.gameObject);
                        //  Destroy(tile.numberController.gameObject);
                        //   Destroy(target.numberController.gameObject);
                        tile.numberController = null;
                        target.numberController = null;
                        value++;
                        Spawn(target,value);
                        continue;
                    }
                    if (target == tile) continue;
                    target.numberController = tile.numberController;
                    tile.numberController = null;
                    target.numberController.transform.position = target.transform.position;

                }
            }
        }
       foreach(var item in listDestroy)
        {
            Destroy(item);
        }
        Spawn();

    }

    public void Spawn(TileController tile,int numberValue)
    {
        var number = Instantiate(numberPrefabs);
        number.Number = numberValue;
        number.transform.position = tile.transform.position;
        tile.numberController = number;
        tile.IsNew = true;
    }

    [ContextMenu(nameof(Spawn))]
    public void Spawn()
    {
        var number = Instantiate(numberPrefabs);
        var ListemptyTile = GridManager.Instance.GetListEmptyTile();
        var IsOne = Random.value < .75;
        number.Number = IsOne ? 1 : 2;
    
        if (ListemptyTile.Count != 0)
        {
         
            var index = Random.Range(0, ListemptyTile.Count);
            var tile = ListemptyTile[index];
            tile.numberController = number;
            number.transform.position = tile.transform.position;
        }
        else
        {
            Debug.Log("Game over");
        }
      
    } 
}
