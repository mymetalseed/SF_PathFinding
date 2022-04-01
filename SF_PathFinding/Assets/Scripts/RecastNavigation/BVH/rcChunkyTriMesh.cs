using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���List�нṹ����뱻��������
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
/// AABB��Χ��,��������XZƽ���ͶӰ��Χ��
/// </summary>
public struct BoundsItem
{
    public float[] bmin;
    public float[] bmax;
    public int i;//���ڻ�԰�Χ������,�������i��¼����ԭʼ�б��е�λ��
    /// <summary>
    /// C#������ֵ�����е�ֵ��ֵ,��Ӹ�ֵ
    /// </summary>
    /// <param name="val"></param>
    public void SetI(int val)
    {
        this.i = val;
    }
    /// <summary>
    /// X��
    /// ������,���������,y����x���ش���0
    /// y=x����0
    /// yС��x����-1
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
    /// Y��������(��XZ��Χ������Z)
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
/// BVH��Chunky�ڵ�
/// </summary>
public struct rcChunkyTriMeshNode {
    public float[] bmin;//�������������������εİ�Χ��
    public float[] bmax;
    public int i;//iΪ����ʾΪҶ�ӽڵ�,iΪ����ʾ��Ҷ�ӽڵ�,��С�����������ڵĸ��Ƿ�Χ
    public int h;//�ڵ��ڰ����������θ���

    public rcChunkyTriMeshNode(int i=0,int n=0)
    {
        this.bmin = new float[2];
        this.bmax = new float[2];
        this.i = 0;
        this.h = 0;
    }
}

/// <summary>
/// ��������ɵ�BVH��
/// </summary>
public class rcChunkyTriMesh
{
    public List<rcChunkyTriMeshNode> nodes;
    public int nnodes = 0;
    public List<int> tris;//�����򻮷ֺ����������򼯺�
    public int ntris = 0;   //�����򻮷ֺ������θ���
    public int maxTrisPerChunk = 0; //Ҷ�ӽڵ�����������������

    /// <summary>
    /// ȡ����Χ�к��ϸ��
    /// </summary>
    /// <param name="items">AABB��Χ��(����������˳��)</param>
    /// <param name="nitems">(�ܹ���N��AABB��Χ��)</param>
    /// <param name="imin"></param>
    /// <param name="imax"></param>
    /// <param name="trisPerChunk">ÿ��BVH�ڵ�������θ���</param>
    /// <param name="curNode">��ǰ��BVH�ڵ�(ref)</param>
    /// <param name="nodes">���е�BVH�ڵ��б�</param>
    /// <param name="maxNodes">���Ľڵ����</param>
    /// <param name="curTri">��ǰ�������θ���(ref)</param>
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
        
        //���inum������ÿ��Chunk�������θ���,����Ҷ�ӽڵ�
        if(inum <= trisPerChunk)
        {
            //
        }
        else  //������Ҫϸ��(�������)
        {
  
        }
    }
    /// <summary>
    /// ����һ���µ�����������ɵ�BVH
    /// </summary>
    /// <param name="verts">���㼯��</param>
    /// <param name="tris">�����μ���(ÿ����һ��)</param>
    /// <param name="ntris">�����θ���</param>
    /// <param name="trisPerChunk">ÿ��BVH���ж�������(��һ��AABB��Χ����������θ���)</param>
    /// <param name="cm">���ص�Mesh</param>
    /// <returns></returns>
    public static bool rcCreateChunkyTriMesh(
        List<float> verts, List<int> tris, int ntris,
        int trisPerChunk, out rcChunkyTriMesh cm)
    {
        //�����ܹ��ж���BVH�ڵ�,����ȡ��
        int nchunks = (ntris + trisPerChunk - 1) / trisPerChunk;
        //����Chunky�ڴ�
        cm = new rcChunkyTriMesh();
        cm.nodes = new List<rcChunkyTriMeshNode>(nchunks*4);
        if (cm.nodes == null) return false;
        cm.tris = new List<int>(ntris*3);
        if (cm.tris == null) return false;
        cm.ntris = ntris;

        //��ʼ����BVH��
        //������AABB��Χ���ڴ�,һ��������һ����Χ��
        List<BoundsItem> items = new List<BoundsItem>(ntris);
        
        //�����i�������ε�AABB��Χ��
        for(int i = 0; i < ntris; ++i)
        {
            //��Ӧ�������εĿ�ʼ�±�(��VBOһ��,������������Ϊһ��������)
            int tId = i * 3;
            items[i].SetI(i);
            //���������ε�XZ�߽�
            //����X�߽�(��ǰ�����ε�һ������)
            items[i].bmin[0] = items[i].bmax[0] = verts[tris[tId] * 3 + 0];
            //����Y�߽�(��ǰ�����ε�һ������)
            items[i].bmin[1] = items[i].bmax[1] = verts[tris[tId] * 3 + 2];
            //����������������������(��ǰ�����εڶ�����������)
            for (int j = 1; j < 3; ++j)
            {
                //�ҵ���������
                //��Ϊ������ÿ����float����һ������
                //ȫ�����Դ���һ���б��е�,����*3
                int vId = tris[tId + j] * 3;
                if (verts[vId] < items[i].bmin[0]) items[i].bmin[0] = verts[vId];
                if (verts[vId + 2] < items[i].bmin[1]) items[i].bmin[1] = verts[vId + 2];

                if (verts[vId] > items[i].bmax[0]) items[i].bmax[0] = verts[vId];
                if (verts[vId + 2] > items[i].bmax[1]) items[i].bmax[1] = verts[vId + 2];
            }
        }

        //�������ҵ���������������XZ���ϵ�AABB��Χ��
        int curTri = 0;
        int curNode = 0;
        //ϸ��
        subdivide(items,ntris,0,ntris,trisPerChunk,ref curNode,cm.nodes,nchunks*4,ref curTri,cm.tris,tris);


        return false;
    }

    /// <summary>
    /// �����������AABB��Χ����ײ��BVH�ڵ�����
    /// </summary>
    /// <param name="cm">��������ɵ�Chunky</param>
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
    /// ������������߶���ײ��BVH�ڵ�����
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
