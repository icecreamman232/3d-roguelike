using UnityEngine;

public class ArenaGenerator : MonoBehaviour
{
    [SerializeField] private GameObject m_arenaPrefab;
    [SerializeField] private int m_xSize;
    [SerializeField] private int m_ySize;

    [ContextMenu("Generate Arena")]
    private void CreateArena()
    {
        for (int x = 0; x < m_xSize; x++)
        {
            for (int y = 0; y < m_ySize; y++)
            {
                Instantiate(m_arenaPrefab, new Vector3(x*12, 0, y*12), Quaternion.identity,transform);
            }
        }
    }

}
