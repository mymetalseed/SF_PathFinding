using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 解决List中结构体必须被拷贝索引
/// </summary>
/// <typeparam name="K"></typeparam>
public struct StructListRef<K> where K : struct
{
    private int i;
    private List<K> refList;
    public StructListRef(int i, List<K> refList)
    {
        this.refList = refList;
        this.i = i;
    }
}

/// <summary>
/// AABB包围盒,三角形在XZ平面的投影包围盒
/// </summary>
public struct BoundsItem
{
    public float[] bmin;
    public float[] bmax;
    public int i;//由于会对包围盒排序,所以这个i记录了在原始列表中的位置
    /// <summary>
    /// C#不允许值类型中的值赋值,间接赋值
    /// </summary>
    /// <param name="val"></param>
    public void SetI(int val)
    {
        this.i = val;
    }
    /// <summary>
    /// X轴
    /// 排序用,正常情况下,y大于x返回大于0
    /// y=x返回0
    /// y小于x返回-1
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static int CompareX(BoundsItem x, BoundsItem y)
    {
        if (x.bmin[0] < y.bmin[0]) return -1;
        if (x.bmin[0] > y.bmin[0]) return 1;
        return 0;
    }
    /// <summary>
    /// Y轴排序用(在XZ包围盒下是Z)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static int CompareY(BoundsItem x, BoundsItem y)
    {
        if (x.bmin[1] < y.bmin[1]) return -1;
        if (x.bmin[1] > y.bmin[1]) return 1;
        return 0;
    }

    public BoundsItem(int i = 0)
    {
        this.bmin = new float[2];
        this.bmax = new float[2];
        this.i = 0;
    }
}

/// <summary>
/// BVH树Chunky节点
/// </summary>
public struct rcChunkyTriMeshNode {
    public float[] bmin;//包含区域内所有三角形的包围盒
    public float[] bmax;
    public int i;//i为正表示为叶子节点,i为负表示非叶子节点,大小等于在数组内的覆盖范围
    public int h;//节点内包含的三角形个数

    public rcChunkyTriMeshNode(int i=0,int n=0)
    {
        this.bmin = new float[2];
        this.bmax = new float[2];
        this.i = 0;
        this.h = 0;
    }
}

/// <summary>
/// 三角形组成的BVH树
/// </summary>
public class rcChunkyTriMesh
{
    public List<rcChunkyTriMeshNode> nodes;
    public int nnodes = 0;
    public List<int> tris;//按区域划分后三角形排序集合
    public int ntris = 0;   //按区域划分后三角形个数
    public int maxTrisPerChunk = 0; //叶子节点最多包含的三角形数

    /// <summary>
    /// 取到包围盒后的细分
    /// </summary>
    /// <param name="items">AABB包围盒(按照三角形顺序)</param>
    /// <param name="nitems">(总共有N个AABB包围盒)</param>
    /// <param name="imin"></param>
    /// <param name="imax"></param>
    /// <param name="trisPerChunk">每个BVH节点的三角形个数</param>
    /// <param name="curNode">当前的BVH节点(ref)</param>
    /// <param name="nodes">所有的BVH节点列表</param>
    /// <param name="maxNodes">最大的节点个数</param>
    /// <param name="curTri">当前的三角形个数(ref)</param>
    /// <param name="outTris"></param>
    /// <param name="inTris"></param>
    public static void subdivide(
        List<BoundsItem> items, int nitems, int imin, int imax,
        int trisPerChunk, ref int curNode, List<rcChunkyTriMeshNode> nodes, int maxNodes,
        ref int curTri, List<int> outTris, List<int> inTris
        )
    {
        int inum = imax - imin;
        int icur = curNode;
        
        if (curNode >= maxNodes) return;
        StructListRef<rcChunkyTriMeshNode> node = new StructListRef<rcChunkyTriMeshNode>(curNode++,nodes);
        
        //如果inum不大于每个Chunk的三角形个数,即是叶子节点
        if(inum <= trisPerChunk)
        {
            //
        }
        else  //否则需要细分(继续拆分)
        {
  
        }
    }
    /// <summary>
    /// 创建一个新的由三角形组成的BVH
    /// </summary>
    /// <param name="verts">顶点集合</param>
    /// <param name="tris">三角形集合(每三个一组)</param>
    /// <param name="ntris">三角形个数</param>
    /// <param name="trisPerChunk">每个BVH块有多少三角(即一个AABB包围盒里的三角形个数)</param>
    /// <param name="cm">返回的Mesh</param>
    /// <returns></returns>
    public static bool rcCreateChunkyTriMesh(
        List<float> verts, List<int> tris, int ntris,
        int trisPerChunk, out rcChunkyTriMesh cm)
    {
        //计算总共有多少BVH节点,向上取整
        int nchunks = (ntris + trisPerChunk - 1) / trisPerChunk;
        //分配Chunky内存
        cm = new rcChunkyTriMesh();
        cm.nodes = new List<rcChunkyTriMeshNode>(nchunks*4);
        if (cm.nodes == null) return false;
        cm.tris = new List<int>(ntris*3);
        if (cm.tris == null) return false;
        cm.ntris = ntris;

        //开始构建BVH树
        //先申请AABB包围盒内存,一个三角形一个包围盒
        List<BoundsItem> items = new List<BoundsItem>(ntris);
        
        //计算第i个三角形的AABB包围盒
        for(int i = 0; i < ntris; ++i)
        {
            //对应的三角形的开始下标(和VBO一样,三个顶点坐标为一个三角形)
            int tId = i * 3;
            items[i].SetI(i);
            //计算三角形的XZ边界
            //缓存X边界(当前三角形第一个坐标)
            items[i].bmin[0] = items[i].bmax[0] = verts[tris[tId] * 3 + 0];
            //缓存Y边界(当前三角形第一个坐标)
            items[i].bmin[1] = items[i].bmax[1] = verts[tris[tId] * 3 + 2];
            //遍历三角形另外两个顶点(当前三角形第二、三个坐标)
            for (int j = 1; j < 3; ++j)
            {
                //找到顶点坐标
                //因为顶点是每三个float代表一个顶点
                //全部线性存在一个列表中的,所以*3
                int vId = tris[tId + j] * 3;
                if (verts[vId] < items[i].bmin[0]) items[i].bmin[0] = verts[vId];
                if (verts[vId + 2] < items[i].bmin[1]) items[i].bmin[1] = verts[vId + 2];

                if (verts[vId] > items[i].bmax[0]) items[i].bmax[0] = verts[vId];
                if (verts[vId + 2] > items[i].bmax[1]) items[i].bmax[1] = verts[vId + 2];
            }
        }

        //到这里找到了所有三角形在XZ面上的AABB包围盒
        int curTri = 0;
        int curNode = 0;
        //细分
        subdivide(items,ntris,0,ntris,trisPerChunk,ref curNode,cm.nodes,nchunks*4,ref curTri,cm.tris,tris);


        return false;
    }

    /// <summary>
    /// 返回与输入的AABB包围盒碰撞的BVH节点索引
    /// </summary>
    /// <param name="cm">三角形组成的Chunky</param>
    /// <param name="bmin"></param>
    /// <param name="bmax"></param>
    /// <param name="ids"></param>
    /// <param name="maxIds"></param>
    /// <returns></returns>
    public static int rcGetChunksOverlappingRect(
        ref rcChunkyTriMesh cm, float[] bmin, 
        float[] bmax, List<int> ids, int maxIds)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 返回与输入的线段碰撞的BVH节点索引
    /// </summary>
    /// <param name="cm"></param>
    /// <param name="p"></param>
    /// <param name="q"></param>
    /// <param name="ids"></param>
    /// <param name="maxIds"></param>
    /// <returns></returns>
    public static int rcGetChunksOverlappingSegment(
        ref rcChunkyTriMesh cm, float[] p,
        float[] q, List<int> ids, int maxIds)
    {
        throw new NotImplementedException();
    }
    

    

}
