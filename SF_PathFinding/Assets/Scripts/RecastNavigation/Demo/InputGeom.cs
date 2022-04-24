using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputGeom
{
    public rcMeshLoaderObj m_mesh = null;
    //BVH树
    rcChunkyTriMesh m_chunkyMesh;

    int m_offMeshConCount;
    int m_volumeCount;

    public Vector3 m_meshBMin, m_meshBMax; 

    public bool LoadMesh(Mesh mesh)
    {
        if (m_mesh!=null)
        {
            m_chunkyMesh = new rcChunkyTriMesh();
            m_mesh = null;
        }
        this.m_mesh = new rcMeshLoaderObj();
        this.m_offMeshConCount = 0;
        this.m_volumeCount = 0;

        if (!m_mesh.load(mesh))
        {
            Debug.LogError("加载Mesh失败");
            return false;
        }
        string greenStr = ColorUtility.ToHtmlStringRGB(Color.green);
        Debug.Log("<color=#"+ greenStr + ">加载Mesh成功!</color>");

        //计算边界
        rcCalcBounds();
        //计算BVH树

        return true;
    }

    /// <summary>
    /// 计算AABB包围盒
    /// </summary>
    void rcCalcBounds()
    {
        List<Vector3> verts = m_mesh.getVerts();
        m_meshBMin = verts[0];
        m_meshBMax = verts[0];
        foreach(Vector3 v in verts)
        {
            m_meshBMin = rcMin(v, m_meshBMin);
            m_meshBMax = rcMax(v, m_meshBMax);
        }

    }

    Vector3 rcMin(Vector3 a,Vector3 b)
    {
        a.x = Mathf.Min(a.x, b.x);
        a.y = Mathf.Min(a.y, b.y);
        a.z = Mathf.Min(a.z, b.z);
        return a;
    }
    Vector3 rcMax(Vector3 a, Vector3 b)
    {
        a.x = Mathf.Max(a.x, b.x);
        a.y = Mathf.Max(a.y, b.y);
        a.z = Mathf.Max(a.z, b.z);
        return a;
    }

}
