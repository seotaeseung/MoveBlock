using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject whiteTile;
    public GameObject blackTile;
    public GameObject wallPrefab;
    public GameObject goalPrefab;

    public string[] mapLayout = {
    "111111111111",
    "100000100001",
    "100000100001",
    "111100000101",
    "1000000001G1",
    "111111111111"
    };

    [ContextMenu("Generate Map From Layout")]
    public void GenerateByText()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        for (int z = 0; z < mapLayout.Length; z++)
        {
            for (int x = 0; x < mapLayout[z].Length; x++)
            {
                Vector3 pos = new Vector3(x, 0, mapLayout.Length - 1 - z);
                char type = mapLayout[z][x];

                if (type == '0')
                {
                    GameObject floorPrefab = (x + z) % 2 == 0 ? whiteTile : blackTile;
                    Instantiate(floorPrefab, pos, Quaternion.identity, transform);
                }
                else if (type == 'G')
                {
                    Instantiate(goalPrefab, pos + Vector3.up * 0.01f, Quaternion.identity, transform);
                }
                else if (type == '1') // º® »ý¼º
                {
                    Instantiate(wallPrefab, pos + Vector3.up * 0.7f, Quaternion.identity, transform);
                }
            }
        }
    }
}
