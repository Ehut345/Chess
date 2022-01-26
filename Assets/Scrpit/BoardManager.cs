using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    private Quaternion orientaion = Quaternion.Euler(0, 180, 0);

    private void Start()
    {
        SpawnAllChessmans();
    }
    private void Update()
    {
        UpdateSelection();
        DrawnChessboard();
    }
    private void UpdateSelection()
    {
        if (!Camera.main)
            return;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }
    private void DrawnChessboard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 hightLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + hightLine);
            }
        }
        //draw the selection
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }
    private void SpawnChessman(int index, Vector3 position)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], position, orientaion) as GameObject;
        go.transform.SetParent(transform);
        activeChessman.Add(go);
    }
    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFFSET;
        if ((x == 3 && y == 0) || (x == 3 && y == 7))
            origin.y += (TILE_SIZE) + 0.2f;
        else if ((x == 4 && y == 0) || (x == 4 && y == 7))
            origin.y += (TILE_SIZE);
        else if (y == 1 || y == 6)
            origin.y += TILE_OFFFSET;
        else
            origin.y += (TILE_SIZE) - 0.2f;

        origin.z += (TILE_SIZE * y) + TILE_OFFFSET;
        return origin;
    }
    private void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();
        //white
        //king
        SpawnChessman(0, GetTileCenter(3, 0));
        //queen
        SpawnChessman(1, GetTileCenter(4, 0));
        //rooks
        SpawnChessman(2, GetTileCenter(0, 0));
        SpawnChessman(2, GetTileCenter(7, 0));
        //bishops
        SpawnChessman(3, GetTileCenter(2, 0));
        SpawnChessman(3, GetTileCenter(5, 0));
        //knights
        SpawnChessman(4, GetTileCenter(1, 0));
        SpawnChessman(4, GetTileCenter(6, 0));
        //pawns
        for (int i = 0; i < 8; i++)
            SpawnChessman(5, GetTileCenter(i, 1));
        //black
        //king
        SpawnChessman(6, GetTileCenter(3, 7));
        //queen
        SpawnChessman(7, GetTileCenter(4, 7));
        //rooks
        SpawnChessman(8, GetTileCenter(0, 7));
        SpawnChessman(8, GetTileCenter(7, 7));
        //bishops
        SpawnChessman(9, GetTileCenter(2, 7));
        SpawnChessman(9, GetTileCenter(5, 7));
        //knights
        SpawnChessman(10, GetTileCenter(1, 7));
        SpawnChessman(10, GetTileCenter(6, 7));
        //pawns
        for (int i = 0; i < 8; i++)
            SpawnChessman(11, GetTileCenter(i, 6));
    }
}