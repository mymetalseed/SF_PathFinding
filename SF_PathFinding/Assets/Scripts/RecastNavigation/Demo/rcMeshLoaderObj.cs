using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class rcMeshLoaderObj
{
    Mesh m_Mesh;
    float m_scale;
    List<float> m_verts;
    List<int> m_tris;
    List<float> m_normals;
    int m_vertCount;
    int m_triCount;
    public rcMeshLoaderObj() {
        m_scale = 1.0f;
        m_verts = new List<float>();
        m_tris = new List<int>();
        m_normals = new List<float>();
    }
    public List<float> getVerts() { return m_verts; }
    public List<float> getNormals() { return m_normals; }
    public List<int> getTris() { return m_tris; }
    public int getVertCount() { return m_vertCount; }
    public int getTriCount() { return m_triCount; }
    
    /// <summary>
    /// 向顶点列表中添加顶点
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="cap"></param>
    private void addVertex(float x,float y,float z,ref int cap)
    {
        if (m_vertCount + 1 > cap)
        {//重新申请空间
            cap = cap==0 ? 8 : cap * 2;
            m_verts.Clear();
            float[] temp = new float[cap * 3];
            m_verts.AddRange(temp);
        }
        int id = m_vertCount * 3;
        m_verts[id++] = x * m_scale;
        m_verts[id++] = y * m_scale;
        m_verts[id++] = z * m_scale;
        m_vertCount++;
    }

    /// <summary>
    /// 添加一个三角形
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="cap"></param>
    private void addTraingle(int a, int b, int c, ref int cap)
    {
        if(m_triCount + 1 > cap)
        {
            cap = cap == 0 ? 8 : cap * 2;
            m_tris.Clear();
            int[] temp = new int[cap * 3];
            m_tris.AddRange(temp);
        }
        int id = m_triCount * 3;
        m_tris[id++] = a;
        m_tris[id++] = b;
        m_tris[id++] = c;
        m_triCount++;
    }

    /// <summary>
    /// 加载Mesh,把实际的的Mesh数据加载成rc需要的格式
    /// </summary>
    /// <param name="mesh"></param>
    /// <returns></returns>
    public bool load(Mesh mesh)
    {
        int[] face = new int[32];
        float x, y, z;
        int nv;
        int vcap = 0;
        int tcap = 0;

        for(int i = 0; i < mesh.vertices.Length; ++i)
        {
            addVertex(mesh.vertices[i].x, mesh.vertices[i].y, mesh.vertices[i].z,ref vcap);
        }
        for (int i = 0; i < mesh.triangles.Length; i+=3)
        {
            addTraingle(mesh.triangles[i], mesh.triangles[i+1], mesh.triangles[i+2], ref tcap);
        }

        m_normals.Clear();
        float[] temp = new float[m_triCount * 3];
        m_normals.AddRange(temp);
        for(int i =0; i < m_triCount * 3; i+=3)
        {
            int v0 = m_tris[i] * 3; 
            int v1 = m_tris[i+1] * 3; 
            int v2 = m_tris[i+2] * 3;
            //两个向量
            float[] e0=new float[3], e1 = new float[3];
            for(int j = 0; j < 3; ++j)
            {
                e0[j] = m_verts[v1 + j] - m_verts[v0 + j];
                e1[j] = m_verts[v2 + j] - m_verts[v0 + j];
            }
            int nId = i;
            m_normals[nId] = e0[1] * e1[2] - e0[2] * e1[1];
            m_normals[nId+1] = e0[2] * e1[0] - e0[0] * e1[2];
            m_normals[nId+2] = e0[0] * e1[1] - e0[1] * e1[0];
            float d = Mathf.Sqrt(m_normals[nId] * m_normals[nId] + m_normals[nId+1] * m_normals[nId+1] + m_normals[nId+2] * m_normals[nId+2]);
            if (d > 0)
            {
                d = 1.0f / d;
                m_normals[nId+0] *= d;
                m_normals[nId+1] *= d;
                m_normals[nId+2] *= d;
            }
        }
        return true;
    }
}
