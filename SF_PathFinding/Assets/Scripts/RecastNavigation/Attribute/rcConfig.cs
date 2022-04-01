using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SF_Recast
{
    /// <summary>
    /// 当Recast开始构建Navmesh时的配置信息
    /// </summary>
    public class rcConfig
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
        public float[] bmin = new float[3];
        //每个场的AABB盒的最大边界
        public float[] bmax = new float[3];
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