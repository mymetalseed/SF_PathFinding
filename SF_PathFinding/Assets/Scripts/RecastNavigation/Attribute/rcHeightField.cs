using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SF_Recast
{
    /// <summary>
    /// һ����̬�ĸ߶ȳ�,�����赲�Ŀռ�(?)
    /// </summary>
    public class rcHeightField
    {
        public int width;   //x����
        public int height;  //z��߶�
        public Vector3 bmin; //����ռ��µ���С�߽�
        public Vector3 bmax; //����ռ��µ����߽�
        public float cs;    //ÿ��Ԫ�صĴ�С
        public float ch;    //ÿ��Ԫ�صĸ߶�
        public List<rcSpan> spans;
        public rcSpanPool? pools = null;
        public rcSpan? freelist = null;//��һ�����ɵ�span
    }

    /// <summary>
    /// ���յ����ϰ��Ŀռ�
    /// </summary>
    public class rcCompactHeightfield
    {
        public int width;
        public int height;
        public int spanCount;
        public int walkableHeight;
        public int walkableClimb;
        public int borderSize;
        public ushort maxDistance;
        public ushort maxRegions;
        public float[] bmin = new float[3];
        public float[] bmax = new float[3];
        public float cs;
        public float ch;
        public rcCompactCell[]? cells = null;
        public rcCompactSpan[]? spans = null;
        public ushort[]? dist = null;
        public byte[]? areas = null;
    }
    /// Provides information on the content of a cell column in a compact heightfield. 
    public struct rcCompactCell
    {
        public uint index;// : 24;	//< Index to the first span in the column.
        public ushort count;// : 8;		//< Number of spans in the column.
    };

    /// Represents a span of unobstructed(���ϰ�) space within a compact heightfield.
    public struct rcCompactSpan
    {
        public ushort y;            //< The lower extent of the span. (Measured from the heightfield's base.)
        public ushort reg;          //< The id of the region the span belongs to. (Or zero if not in a region.)
        public uint con;// : 24;		//< Packed neighbor connection data.
        public ushort h;// : 8;			//< The height of the span.  (Measured from #y.)
    };

    /// Represents a heightfield layer within a layer set.
    /// @see rcHeightfieldLayerSet
    public class rcHeightfieldLayer
    {
        public float[] bmin = new float[3];
        public float[] bmax = new float[3];
        public float cs;
        public float ch;
        public int width;
        public int height;
        public int minx;
        public int maxx;
        public int miny;
        public int maxy;
        public int hmin;
        public int hmax;
        public byte[]? heights;
        public byte[]? areas;
        public byte[]? cons;
    }

    /// Represents a set of heightfield layers.
    /// @ingroup recast
    /// @see rcAllocHeightfieldLayerSet, rcFreeHeightfieldLayerSet 
    public class rcHeightfieldLayerSet
    {
        public rcHeightfieldLayer[]? layers = null;         //< The layers in the set. [Size: #nlayers]
        public int nlayers;                     //< The number of layers in the set.
    };



}