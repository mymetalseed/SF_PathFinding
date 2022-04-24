using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class rcMeshLoaderObj
{
    Mesh m_Mesh;
    float m_scale;
    List<Vector3> m_verts;
    List<Vector3Int> m_tris;
    List<Vector3> m_normals;
    int m_vertCount;
    int m_triCount;
    public rcMeshLoaderObj() {
        m_scale = 1.0f;
        m_verts = new List<Vector3>();
        m_tris = new List<Vector3Int>();
        m_normals = new List<Vector3>();
    }
    public List<Vector3> getVerts() { return m_verts; }
    public List<Vector3> getNormals() { return m_normals; }
    public List<Vector3Int> getTris() { return m_tris; }
    public int getVertCount() { return m_vertCount; }
    public int getTriCount() { return m_triCount; }
    
    /// <summary>
    /// 向顶点列表中添加顶点
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="cap"></param>
    private void addVertex(Vector3 vert)
    {
        m_verts.Add(vert);
        m_vertCount = m_verts.Count;
    }

    /// <summary>
    /// 添加一个三角形
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="cap"></param>
    private void addTraingle(Vector3Int tri)
    {
        m_tris.Add(tri);
        m_triCount = m_tris.Count;
    }

    /// <summary>
    /// 加载Mesh,把实际的的Mesh数据加载成rc需要的格式
    /// </summary>
    /// <param name="mesh"></param>
    /// <returns></returns>
    public bool load(Mesh mesh)
    {
        for(int i = 0; i < mesh.vertices.Length; ++i)
        {
            addVertex(new Vector3(mesh.vertices[i].x, mesh.vertices[i].y, mesh.vertices[i].z));
        }
        for (int i = 0; i < mesh.triangles.Length; i+=3)
        {
            addTraingle(new Vector3Int(mesh.triangles[i], mesh.triangles[i+1], mesh.triangles[i+2]));
        }

        m_normals.Clear();
        for(int i =0; i < m_triCount; i++)
        {
            m_normals.Add(Vector3.zero);
            Vector3Int tri = m_tris[i];
            //两个向量
            Vector3 e0= m_verts[tri.y]- m_verts[tri.x], e1= m_verts[tri.z] - m_verts[tri.x];

            m_normals[i].Set(
                e0.y * e1.z - e0.z * e1.y,
                e0.z * e1.x - e0.x * e1.z,
                e0.x * e1.y - e0.y * e1.x
                );
                
            float d = Mathf.Sqrt(m_normals[i].x * m_normals[i].x + m_normals[i].y * m_normals[i].y + m_normals[i].z * m_normals[i].z);
            if (d > 0)
            {
                d = 1.0f / d;
                m_normals[i].Set(
                    m_normals[i].x*d,
                    m_normals[i].y*d,
                    m_normals[i].z*d
                    );
            }
        }
        return true;
    }
}
