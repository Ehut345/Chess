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
    private List<GameObject> activeChessman = new List<GameObject>();

    private void Start()
    {
        SpawnChessman(0,GetTileCenter(3,1));
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
        GameObject go = Instantiate(chessmanPrefabs[index], position, Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        activeChessman.Add(go);
    }
    private Vector3 GetTileCenter(int x,int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFFSET;
        return origin;
    }
}