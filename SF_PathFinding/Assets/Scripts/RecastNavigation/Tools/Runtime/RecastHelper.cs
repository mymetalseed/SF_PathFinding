using SF_Recast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WalkableEnum
{
    RC_UNWALKABLE_AREA = 62,
    RC_WALKABLE_AREA = 63,
}

public class RecastHelper
{


    /// <summary>
    /// 根据AABB包围盒和cellSize,计算每个Cell的大小
    /// </summary>
    /// <param name="bmin"></param>
    /// <param name="bmax"></param>
    /// <param name="cs"></param>
    /// <param name="w"></param>
    /// <param name="h"></param>
    public static void rcCalcGridSize(Vector3 bmin, Vector3 bmax, float cs, ref int w, ref int h)
    {
        w = (int)((bmax.x - bmin.x) / cs + 0.5f);
        h = (int)((bmax.z - bmin.z) / cs + 0.5f);
    }

    public static void rcCreateHeightField(rcHeightField hf, int width, int height, Vector3 bmin, Vector3 bmax, float cs, float ch)
    {
        hf.width = width;
        hf.height = height;
        hf.bmin = bmin;
        hf.bmax = bmax;
        hf.cs = cs;
        hf.ch = ch;
        hf.spans = new List<rcSpan>();
    }

    public static void rcMarkWalkableTriangles(float walkableSlopeAngle, List<Vector3> verts, int nv, List<Vector3Int> tris, int nt, List<WalkableEnum> area)
    {
        //角度转弧度,可前进的最大角度
        float walkableThr = Mathf.Cos(walkableSlopeAngle / 180.0f * Mathf.PI);
        Vector3 norm = Vector3.zero;
        for (int i = 0; i < tris.Count; ++i)
        {
            calcTriNormal(verts[tris[i].x], verts[tris[i].y], verts[tris[i].z], ref norm);
            //只需要计算y的角度是否可以移动即可
            if (norm.y > walkableThr)
                area.Add(WalkableEnum.RC_WALKABLE_AREA);
            else area.Add(WalkableEnum.RC_UNWALKABLE_AREA);
        }

    }

    /// <summary>
    /// 光栅化
    /// </summary>
    /// <param name="verts"></param>
    /// <param name="tris"></param>
    /// <param name="area"></param>
    /// <param name="solid"></param>
    /// <param name="flagMergeThr"></param>
    public static void rcRasterizeTriangles(List<Vector3> verts, List<Vector3Int> tris, List<WalkableEnum> area, rcHeightField solid, int flagMergeThr)
    {
        int nv = verts.Count;
        int nt = tris.Count;

        //归一化cellSize和cellHeight
        //xz轴上每个单独场元素面积
        float ics = 1.0f / solid.cs;
        //y轴上每个单独场元素的高度
        float ich = 1.0f / solid.ch;

        //逐三角形光栅化(三维)
        for (int i = 0; i < nt; ++i)
        {
            Vector3 v1 = verts[tris[i].x];
            Vector3 v2 = verts[tris[i].y];
            Vector3 v3 = verts[tris[i].z];
            rasterizeTri(v1, v2, v3, area[i], solid, solid.bmin, solid.bmax, solid.cs, ics, ich, flagMergeThr);
        }

    }


    /// <summary>
    /// 计算三角形的法线
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <param name="norm"></param>
    public static void calcTriNormal(Vector3 v1, Vector3 v2, Vector3 v3, ref Vector3 norm)
    {
        Vector3 e0 = v2 - v1;
        Vector3 e1 = v3 - v1;
        norm = Vector3.Cross(e0, e1).normalized;
    }

}
