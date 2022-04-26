using SF_Recast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecastRasterization
{
    /// <summary>
    /// ������������ά�ռ��ڹ�դ��
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

        //��ǰ�����ε�AABB
        //�����������С�����
        tmin.x = Mathf.Min(v0.x, v1.x, v2.x);
        tmin.y = Mathf.Min(v0.y, v1.y, v2.y);
        tmin.z = Mathf.Min(v0.z, v1.z, v2.z);

        tmax.x = Mathf.Max(v0.x, v1.x, v2.x);
        tmax.y = Mathf.Max(v0.y, v1.y, v2.y);
        tmax.z = Mathf.Max(v0.z, v1.z, v2.z);

        if (!overlapBounds(bmin, bmax, tmin, tmax))
        {
            return true;    //�����β���ȫ������Χ��֮��,������������
        }

        //������������xzƽ���е����س��Ͽ���ӳ�䵽�ĸ���
        //y0�Ǿ���ȫ�ְ�Χ����������غе��±�
        //y1�Ǿ���ȫ�ְ�Χ����Զ�����غе��±�
        int y0 = (int)((tmin.z - bmin.z) * ics); //ics = 1/cs
        int y1 = (int)((tmax.z - bmin.z) * ics);
        y0 = Mathf.Clamp(y0, 0, h - 1);
        y1 = Mathf.Clamp(y1, 0, h - 1);

        //�ü������б���������θ��ǵķ���
        List<Vector3> inBuffer = new List<Vector3>();
        List<Vector3> inRow = new List<Vector3>();
        List<Vector3> p1 = new List<Vector3>();
        List<Vector3> p2 = new List<Vector3>();

        inBuffer.Add(v0); inBuffer.Add(v1); inBuffer.Add(v2);
        //��xzƽ���ϵ����س����б���,�жϵ�ǰ�����Ƿ���Ҫ���뵽����
        for(int y = y0; y <= y1; ++y)
        {
            //z���ϵ�y��cell�Ŀ�ʼλ��
            float cz = bmin.z + y * cs;

            //��z=cs+cz����������
            dividePoly(inBuffer, inRow, p1, cz + cs, 2);
            SwapRef(p1, inBuffer);
            if (inRow.Count < 3) continue;  //δ������,ֱ�Ӳ�����һ��

            //Ѱ��xzƽ�����س��е�ˮƽ�߽�

            //����y���ϵľ���Ѱ�Ҹ߶ȳ�
        }

        return true;
    }

    /// <summary>
    /// �ж���Χ���Ƿ�����һ����Χ��֮��
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
    /// ��͹�������һ��ֱ�߷�Ϊ����͹�����
    /// </summary>
    /// <param name="_in"></param>
    /// <param name="out1"></param>
    /// <param name="out2"></param>
    /// <param name="x"></param>
    /// <param name="axis"></param>
    public static void dividePoly(List<Vector3> _in,List<Vector3> out1, List<Vector3> out2,float x,int axis)
    {
        List<float> d = new List<float>();
        //��ȡ�����б�����axis���ϵ�����ֵ(����)
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
                //�����j�͵�i
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
    /// ��������
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
