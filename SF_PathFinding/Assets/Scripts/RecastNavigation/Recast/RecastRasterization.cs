using SF_Recast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecastRasterization
{
    /// <summary>
    /// 对三角形在三维空间内光栅化
    /// </summary>
    /// <returns></returns>
    public static bool rasterizeTri(Vector3 v0, Vector3 v1, Vector3 v2,
            WalkableEnum area,rcHeightField hf,Vector3 bmin,Vector3 bmax,
            float cs,float ics,float ich,int flagMergeThr
        )
    {
        int w = hf.width;
        int h = hf.height;
        Vector3 tmin, tmax;
        float by = bmax.y - bmin.y;

        //求当前三角形的AABB
        //三个顶点的最小和最大
        tmin.x = Mathf.Min(v0.x, v1.x, v2.x);
        tmin.y = Mathf.Min(v0.y, v1.y, v2.y);
        tmin.z = Mathf.Min(v0.z, v1.z, v2.z);

        tmax.x = Mathf.Max(v0.x, v1.x, v2.x);
        tmax.y = Mathf.Max(v0.y, v1.y, v2.y);
        tmax.z = Mathf.Max(v0.z, v1.z, v2.z);

        if (!overlapBounds(bmin, bmax, tmin, tmax))
        {
            return true;    //三角形不完全在最大包围盒之内,跳过该三角形
        }

        //计算三角形在xz平面中的体素场上可以映射到的个数
        //y0是距离全局包围盒最近的体素盒的下标
        //y1是距离全局包围盒最远的体素盒的下标
        int y0 = (int)((tmin.z - bmin.z) * ics); //ics = 1/cs
        int y1 = (int)((tmax.z - bmin.z) * ics);
        y0 = Mathf.Clamp(y0, 0, h - 1);
        y1 = Mathf.Clamp(y1, 0, h - 1);

        //裁剪出所有被这个三角形覆盖的方块
        List<Vector3> inBuffer = new List<Vector3>();
        List<Vector3> inRow = new List<Vector3>();
        List<Vector3> p1 = new List<Vector3>();
        List<Vector3> p2 = new List<Vector3>();

        inBuffer.Add(v0); inBuffer.Add(v1); inBuffer.Add(v2);
        //对xz平面上的体素场进行遍历,判断当前体素是否需要加入到场中
        for(int y = y0; y <= y1; ++y)
        {
            //z轴上第y个cell的开始位置
            float cz = bmin.z + y * cs;

            //沿z=cs+cz划分三角形
            dividePoly(inBuffer, inRow, p1, cz + cs, 2);
            SwapRef(p1, inBuffer);
            if (inRow.Count < 3) continue;  //未被包含,直接查找下一个

            //寻找xz平面体素场中的水平边界

            //根据y轴上的距离寻找高度场
        }

        return true;
    }

    /// <summary>
    /// 判定包围盒是否在另一个包围盒之内
    /// </summary>
    /// <param name="amin"></param>
    /// <param name="amax"></param>
    /// <param name="bmin"></param>
    /// <param name="bmax"></param>
    /// <returns></returns>
    public static bool overlapBounds(Vector3 amin, Vector3 amax, Vector3 bmin, Vector3 bmax)
    {
        bool overlap = true;
        overlap = (amin.x > bmax.x || amax.x < bmin.x) ? false : overlap;
        overlap = (amin.y > bmax.y || amax.y < bmin.y) ? false : overlap;
        overlap = (amin.z > bmax.z || amax.z < bmin.z) ? false : overlap;

        return overlap;
    }

    /// <summary>
    /// 将凸多边形沿一条直线分为两个凸多边形
    /// </summary>
    /// <param name="_in"></param>
    /// <param name="out1"></param>
    /// <param name="out2"></param>
    /// <param name="x"></param>
    /// <param name="axis"></param>
    public static void dividePoly(List<Vector3> _in,List<Vector3> out1, List<Vector3> out2,float x,int axis)
    {
        List<float> d = new List<float>();
        //获取定点列表中在axis轴上的所有值(缓存)
        for(int i= 0; i < _in.Count; ++i)
        {
            d[i] = x - GetAxis(_in[i],axis);
        }

        for(int i=0,j = _in.Count - 1; i < _in.Count; j = i, ++i)
        {
            bool ina = d[j] >= 0;
            bool inb = d[i] >= 0;

            if (ina != inb)
            {//both side
                //计算第j和第i
                float s = d[j] / (d[j] - d[i]);
                Vector3 ot1;
                ot1.x = _in[j].x + (_in[i].x - _in[j].x) * s;
                ot1.y = _in[j].y + (_in[i].y - _in[j].y) * s;
                ot1.z = _in[j].z + (_in[i].z - _in[j].z) * s;
                out1.Add(ot1);
                out2.Add(ot1);

                if (d[i] > 0)
                {
                    out1.Add(_in[i]);
                }else if (d[i] < 0)
                {
                    out2.Add(_in[i]);
                }
            }
            else
            {//same side 
                if (d[i] >= 0)
                {
                    out1.Add(_in[i]);
                    if (d[i] != 0)
                        continue;
                }
                out2.Add(_in[i]);
            }
        }
    }

    
    public static float GetAxis(Vector3 vec,int axis)
    {
        switch (axis)
        {
            case 0:
                return vec.x;
            case 1:
                return vec.y;
            case 2:
                return vec.z;
        }
        return vec.x;
    }

    /// <summary>
    /// 交换引用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    public static void SwapRef<T>(T a,T b)
    {
        T c = a;
        a = b;
        b = c;
    }
}
