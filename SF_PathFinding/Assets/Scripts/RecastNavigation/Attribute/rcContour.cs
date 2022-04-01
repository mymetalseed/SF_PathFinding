using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
namespace SF_Recast
{
    /// <summary>
    /// 不重叠的可通行轮廓
    /// </summary>
    public class rcContour 
    {
        public int[]? verts;            //< 轮廓顶点和连接数据. [Size: 4 * #nverts]
        public int nverts;          //< 顶点数 
        public int[]? rverts;       //< 原始的轮廓顶点和连接数据. [Size: 4 * #nrverts]
        public int nrverts;     //< 原始的轮廓顶点数
        public ushort reg;  //< 轮廓范围的id
        public byte area;	//< 当前轮廓的面积

        /// <summary>
        /// 转换成文本
        /// </summary>
        /// <param name="stream"></param>
        public void dumpToText(StreamWriter stream)
        {
            stream.WriteLine("\treg: " + reg);
            stream.WriteLine("\tarea: " + area);
            stream.WriteLine("\tnverts: " + nverts);
            for (int i = 0; i < nverts; ++i)
            {
                int vIndex = i * 4;
                stream.WriteLine("\t\tverts[" + i + "]: x:" + verts![vIndex] + " y:" + verts[vIndex + 1] + " z:" + verts[vIndex + 2] + " ?:" + verts[vIndex + 3]);
            }
            stream.WriteLine("\tnrverts: " + nrverts);
            for (int i = 0; i < nrverts; ++i)
            {
                int vIndex = i * 4;
                stream.WriteLine("\t\trverts[" + i + "]: x:" + rverts![vIndex] + " y:" + rverts[vIndex + 1] + " z:" + rverts[vIndex + 2] + " ?:" + rverts[vIndex + 3]);
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\treg: " + reg);
            sb.AppendLine("\tarea: " + area);
            sb.AppendLine("\tnverts: " + nverts);
            for (int i = 0; i < nverts; ++i)
            {
                int vIndex = i * 4;
                sb.AppendLine("\t\tverts[" + i + "]: x:" + verts![vIndex] + " y:" + verts[vIndex + 1] + " z:" + verts[vIndex + 2] + " ?:" + verts[vIndex + 3]);
            }
            sb.AppendLine("\tnrverts: " + nrverts);
            for (int i = 0; i < nrverts; ++i)
            {
                int vIndex = i * 4;
                sb.AppendLine("\t\trverts[" + i + "]: x:" + rverts![vIndex] + " y:" + rverts[vIndex + 1] + " z:" + rverts[vIndex + 2] + " ?:" + rverts[vIndex + 3]);
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// 表示一组相关的轮廓
    /// </summary>
    public class rcContourSet
    {
        public rcContour[]? conts = null;   //< An array of the contours in the set. [Size: #nconts]
        public int nconts;          //< The number of contours in the set.
        public float[] bmin = new float[3];     //< The minimum bounds in world space. [(x, y, z)]
        public float[] bmax = new float[3];     //< The maximum bounds in world space. [(x, y, z)]
        public float cs;            //< The size of each cell. (On the xz-plane.)
        public float ch;            //< The height of each cell. (The minimum increment along the y-axis.)
        public int width;           //< The width of the set. (Along the x-axis in cell units.) 
        public int height;          //< The height of the set. (Along the z-axis in cell units.) 
        public int borderSize;      //< The AABB border size used to generate the source data from which the contours were derived.
        public float maxError;     //< The max edge error that this contour set was simplified with.
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("nconts: " + nconts);
            sb.AppendLine("bmin: " + bmin[0] + " " + bmin[1] + " " + bmin[2]);
            sb.AppendLine("bmax: " + bmax[0] + " " + bmax[1] + " " + bmax[2]);
            sb.AppendLine("cs: " + cs);
            sb.AppendLine("ch: " + ch);
            sb.AppendLine("width: " + width);
            sb.AppendLine("height: " + height);
            sb.AppendLine("bordersize: " + borderSize);
            sb.AppendLine("maxError: " + maxError);

            for (int i = 0; i < nconts; ++i)
            {
                sb.Append("contour[" + i + "]: ");
                sb.AppendLine(conts![i].ToString());
            }

            return sb.ToString();
        }
    }


}