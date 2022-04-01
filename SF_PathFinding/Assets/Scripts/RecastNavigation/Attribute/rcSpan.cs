using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SF_Recast
{
    /// <summary>
    /// 高度场的Span(障碍物?)
    /// </summary>
    public class rcSpan
    {
        public ushort smin;//当前span界限最小值
        public ushort smax;//当前span界限最大值
        public byte area;  //当前span面积
        public rcSpan? next = null; //更高跨度下一个的Span
    }
}