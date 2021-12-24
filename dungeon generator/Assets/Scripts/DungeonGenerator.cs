//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGenerator : MonoBehaviour
{
    
    
    public GameObject player;
    //cell information such as having been visited or not and amount of doors.
    private class Cell
    {
        public bool visited;
        public readonly bool[] status = new bool[4];
    }

    public Vector2 size;
    public int startPos;
    public GameObject room;
    public Vector2 offset;

    private List<Cell> _board;
    
    public void Awake()
    {
        //Instantiate(player);
        MazeGenerator();
    }

    private void GenerateDungeon()//creates each room that has been defined in MazeGenerator
    {
        for (var i = 0; i < size.x; i++)
        {
            for (var j = 0; j < size.y; j++)
            {

                var currentCell = _board[Mathf.FloorToInt(i + j * size.x)];
                if (!currentCell.visited) continue;
                var newRoom = Instantiate(room, new Vector3(i*offset.x,0,-j*offset.y), Quaternion.identity, transform);
                var roomBehaviour = newRoom.GetComponent<RoomBehaviour>();
                roomBehaviour.UpdateRoom(currentCell.status);

                newRoom.name += " " + i + "-" + j;

                // newRoom.SetActive(false);

            }
        }
    }

    private void MazeGenerator()
    {
        
        _board = new List<Cell>();

        for (var i = 0; i < size.x; i++)
        {
            for (var j = 0; j < size.y; j++)
            {
                _board.Add(new Cell());
            }
        }

        var currentCell = startPos; //keeps track of current position

        var path = new Stack<int>();//keeps track of the path that is made until the current cell
        //keeps track of which loop we're at so our while loops don't go on indefinitely

        while (true)//1000 is set because it's a sufficient amount for the size a bigger number could be used for bigger maze
        {
            _board[currentCell].visited = true;

            if(currentCell == _board.Count - 1)
            {
                break;
            }

            //Check the cell's neighbors
            var neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)//checks for available neighbors
            {
                if (path.Count == 0)
                {
                    break;
                }
                else//goes back to the last cell and sets current cell as last cell in the path
                {
                    currentCell = path.Pop();
                }
                
            }
            else//chooses a random cell between the neighbors
            {
                path.Push(currentCell);

                var newCell = neighbors[Random.Range(0, neighbors.Count)];

                if (newCell > currentCell)
                {
                    //down or right
                    if (newCell - 1 == currentCell)
                    {
                        _board[currentCell].status[2] = true;
                        currentCell = newCell;
                        _board[currentCell].status[3] = true;
                    }
                    else
                    {
                        _board[currentCell].status[1] = true;
                        currentCell = newCell;
                        _board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        _board[currentCell].status[3] = true;
                        currentCell = newCell;
                        _board[currentCell].status[2] = true;
                    }
                    else
                    {
                        _board[currentCell].status[0] = true;
                        currentCell = newCell;
                        _board[currentCell].status[1] = true;
                    }
                }

            }
        }
        GenerateDungeon();
        Debug.Log("generate");
    }

    private List<int> CheckNeighbors(int cell)//returns a list of all the neighbors of the current cell
    {
        var neighbors = new List<int>();

        //check up neighbor
        if (cell - size.x >= 0 && !_board[Mathf.FloorToInt(cell - size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }
        //check down neighbor
        if (cell + size.x < _board.Count && !_board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }
        //check right neighbor
        if ((cell + 1) % size.x != 0 && !_board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }
        //check left neighbor
        if (cell % size.x != 0 && !_board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbors;
    }
    public void RegenerateDungeon()
    {
        SceneManager.LoadScene(0);
    }
}
