using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputGeom
{
    rcMeshLoaderObj m_mesh = null;
    //BVH树
    rcChunkyTriMesh m_chunkyMesh;

    int m_offMeshConCount;
    int m_volumeCount;
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
        //计算BVH树

        return true;
    }
}
