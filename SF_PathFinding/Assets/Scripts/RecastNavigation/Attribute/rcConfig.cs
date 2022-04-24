using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SF_Recast
{
    /// <summary>
    /// 当Recast开始构建Navmesh时的配置信息
    /// </summary>
    [CreateAssetMenu(menuName = "寻路工具/寻路配置")]
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
    //场内 x轴的宽度
    public int width;
    //场内 z轴的高度
    public int height;
    //xz平面上每个瓷砖的宽度/高度
    public int tileSize;
    //不可穿行的高度场的边界大小
    public int borderSize;
    //xz平面上每个单独场元素的大小
    public float cs;
    //y轴上每个单独场元素的大小
    public float ch;
    //每个场的AABB盒的最小边界
    public Vector3 bmin;
    //每个场的AABB盒的最大边界
    public Vector3 bmax;
    //可以行走范围的最高倾斜角度
    public float walkableSlopeAngle;
    //能越过的最高高度
    public int walkableHeight;
    //
    public int walkableClimb;
    //TODO: 不知道是啥
    public int walkableRadius;
    //生成的mesh边界中每个边最长的长度
    public int maxEdgeLen;
    //TODO:不知道啥
    public double maxSimplificationError;
    //允许生成的区域的最小面积
    public int minRegionArea;
    //如果一个区域小于这个值将会被合并
    public int mergeRegionArea;
    //在Coutour中生成的每个多边形的最大顶点数
    public int maxVertsPerPoly;
    //在构建可移动mesh区域时的采样距离
    public float detailSampleDist;
    //最大可偏移距离
    public float detailSampleMaxError;
}