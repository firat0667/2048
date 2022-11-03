using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyGrid
{
	public class GridManager : MonoBehaviour
	{
		public static GridManager Instance { get; private set; }
		[SerializeField] private AxisType axisType;
		[SerializeField] private TileController tileController;
		[SerializeField] private Vector2Int size = new Vector2Int(3, 3);
		[SerializeField, Range(0, 5f)] private float distance = 1.25f;
		[SerializeField] private bool changeDistance;

		public List<List<TileController>> GetPriorityTile(Direction direction)
        {
			var result = new List<List<TileController>>();
		
			var isMax = direction == Direction.Up || direction == Direction.Right;
			var isVertical = direction == Direction.Up || direction == Direction.Down;
			int current = isMax ? 3 : 0;
			for(int i = 0; i < 4; i++)
            {
				var list = new List<TileController>();

				foreach(var tile in listTile)
                {
					var coordinate = isVertical ? tile.coordinate.y : tile.coordinate.x;
                    if (coordinate==current)
						list.Add(tile);
                   
					
                }
				result.Add(list);
				current += isMax ? -1 : 1;

               
				
			}
			return result;
		}
        


		

		private void Awake()
		{
			Instance = this;
		}

		public List<TileController> Tiles => listTile;
	    public List<TileController> listTile;

        

        public TileController GetTile(Vector2Int coordinate)
		{
			return listTile.Find(item => item.coordinate == coordinate);
		}
		public List<TileController> GetListEmptyTile()
        {
			var result = new List<TileController>();
			foreach (var tile in listTile)
            {
                if (!tile.numberController)
                {
					result.Add(tile);
                }
            }
			return result;
        }


		public void SetTiles(List<TileController> tiles)
		{
			listTile = tiles;
		}
	}
}