using UnityEditor;
using UnityEngine;

public class MeshCombine : MonoBehaviour
{
    [SerializeField] private MeshFilter[] m_meshToCombines;
    [SerializeField] private MeshFilter m_combinedMeshFilter;

    [ContextMenu("Combine Meshes")]
    private void CombineMeshes()
    {
        var combine = new CombineInstance[m_meshToCombines.Length];
        for (int i = 0; i < m_meshToCombines.Length; i++)
        {
            combine[i].mesh = m_meshToCombines[i].sharedMesh;
            combine[i].transform = m_meshToCombines[i].transform.localToWorldMatrix;
        }
        
        var mesh = new Mesh();
        mesh.CombineMeshes(combine);
        
        m_combinedMeshFilter.mesh = mesh;
        
        SaveMesh(m_combinedMeshFilter.sharedMesh,gameObject.name,false,true);
    }

    private void SaveMesh(Mesh mesh, string name, bool makeNewInstance, bool optimizeMesh)
    {
        string path = EditorUtility.SaveFilePanel("Save separate meshes", "Assets/", name, "asset");
        if (string.IsNullOrEmpty(path)) return;
        path = FileUtil.GetProjectRelativePath(path);
        Mesh meshToSave = makeNewInstance ? Object.Instantiate(mesh) as Mesh : mesh;
        if (optimizeMesh)
        {
            MeshUtility.Optimize(meshToSave);
        }
        
        AssetDatabase.CreateAsset(meshToSave, path);
        AssetDatabase.SaveAssets();
    }
}
