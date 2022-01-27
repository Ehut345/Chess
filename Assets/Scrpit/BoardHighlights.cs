using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlights : MonoBehaviour
{
    public static BoardHighlights Instance { set; get; }

    public GameObject HighlightPrefab;
    private List<GameObject> highlites;

    private void Start()
    {
        Instance = this;
        highlites = new List<GameObject>();
    }

    private GameObject GetHighlightObject()
    {
        GameObject go = highlites.Find(g => !g.activeSelf);
        if (go == null)
        {
            go = Instantiate(HighlightPrefab);
            highlites.Add(go);
        }
        return go;
    }

    public void HighlightAllowedMoves(bool[,] moves)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j])
                {
                    GameObject go = GetHighlightObject();
                    go.SetActive(true);
                    go.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);
                }
            }
        }
    }

    public void HideHighlithes()
    {
        foreach (GameObject go in highlites)
            go.SetActive(false);
    }
}
