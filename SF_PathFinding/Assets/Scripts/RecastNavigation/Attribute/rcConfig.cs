using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SF_Recast
{
    /// <summary>
    /// ��Recast��ʼ����Navmeshʱ��������Ϣ
    /// </summary>
    [CreateAssetMenu(menuName = "Ѱ·����/Ѱ·����")]
    public class rcConfig :ScriptableObject
    {
        private static rcConfig instance;
        public static rcConfig Instance
        {
            get
            {
                return instance;
            }
        }

        public float m_cellSize;
        public float m_cellHeight;
        public float m_agentHeight;
        public float m_agentRadius;
        public float m_agentMaxClimb;
        public float m_agentMaxSlope;
        public float m_regionMinSize;
        public float m_regionMergeSize;
        public float m_edgeMaxLen;
        public float m_edgeMaxError;
        public float m_vertsPerPoly;
        public float m_detailSampleDist;
        public float m_detailSampleMaxError;
        public int m_partitionType;

        public bool m_filterLowHangingObstacles;
        public bool m_filterLedgeSpans;
        public bool m_filterWalkableLowHeightSpans;
        public rcConfig() {
            instance = this;
        }

        public void Init()
        {
            instance = this;
        }
    }
}

public class rcConfigRuntime
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
    public Vector3 bmin;
    //ÿ������AABB�е����߽�
    public Vector3 bmax;
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
}