using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SF_Recast
{
    /// <summary>
    /// ��Recast��ʼ����Navmeshʱ��������Ϣ
    /// </summary>
    public class rcConfig
    {
        //���� x��Ŀ��
        public int width;
        //���� z��ĸ߶�
        public int height;
        //xzƽ����ÿ����ש�Ŀ��/�߶�
        public int tileSize;
        //���ɴ��еĸ߶ȳ��ı߽��С
        public int borderSize;
        //xzƽ����ÿ��������Ԫ�صĴ�С
        public float cs;
        //y����ÿ��������Ԫ�صĴ�С
        public float ch;
        //ÿ������AABB�е���С�߽�
        public float[] bmin = new float[3];
        //ÿ������AABB�е����߽�
        public float[] bmax = new float[3];
        //�������߷�Χ�������б�Ƕ�
        public float walkableSlopeAngle;
        //��Խ������߸߶�
        public int walkableHeight;
        //
        public int walkableClimb;
        //TODO: ��֪����ɶ
        public int walkableRadius;
        //���ɵ�mesh�߽���ÿ������ĳ���
        public int maxEdgeLen;
        //TODO:��֪��ɶ
        public double maxSimplificationError;
        //�������ɵ��������С���
        public int minRegionArea;
        //���һ������С�����ֵ���ᱻ�ϲ�
        public int mergeRegionArea;
        //��Coutour�����ɵ�ÿ������ε���󶥵���
        public int maxVertsPerPoly;
        //�ڹ������ƶ�mesh����ʱ�Ĳ�������
        public float detailSampleDist;
        //����ƫ�ƾ���
        public float detailSampleMaxError;
        public rcConfig() { }
        public rcConfig(rcConfig other)
        {
            width = other.width;
            height = other.height;
            tileSize = other.tileSize;
            borderSize = other.borderSize;
            cs = other.cs;
            ch = other.ch;
            for(var i = 0; i < 3; i++)
            {
                bmin[i] = other.bmin[i];
                bmax[i] = other.bmax[i];
            }
            walkableSlopeAngle = other.walkableSlopeAngle;
            walkableHeight = other.walkableHeight;
            walkableClimb = other.walkableClimb;
            walkableRadius = other.walkableRadius;
            maxEdgeLen = other.maxEdgeLen;
            maxSimplificationError = other.maxSimplificationError;
            minRegionArea = other.minRegionArea;
            mergeRegionArea = other.mergeRegionArea;
            maxVertsPerPoly = other.maxVertsPerPoly;
            detailSampleDist = other.detailSampleDist;
            detailSampleMaxError = other.detailSampleMaxError;
        }
    }
}