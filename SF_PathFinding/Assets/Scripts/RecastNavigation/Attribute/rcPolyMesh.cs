using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace SF_Recast
{
    /// 表示用于构建Navmesh的多边形网格
    /// @ingroup recast
    public class rcPolyMesh
    {
        public ushort[]? verts = null;  //< The mesh vertices. [Form: (x, y, z) * #nverts]
        public ushort[]? polys = null;  //< Polygon and neighbor data. [Length: #maxpolys * 2 * #nvp]
        public ushort[]? regs = null;   //< The region id assigned to each polygon. [Length: #maxpolys]
        public ushort[]? flags = null;  //< The user defined flags for each polygon. [Length: #maxpolys]
        public byte[]? areas = null;    //< The area id assigned to each polygon. [Length: #maxpolys]
        public int nverts;              //< The number of vertices.
        public int npolys;              //< The number of polygons.
        public int maxpolys;            //< The number of allocated polygons.
        public int nvp;             //< The maximum number of vertices per polygon.
        public float[] bmin = new float[3];         //< The minimum bounds in world space. [(x, y, z)]
        public float[] bmax = new float[3];         //< The maximum bounds in world space. [(x, y, z)]
        public float cs;                //< The size of each cell. (On the xz-plane.)
        public float ch;                //< The height of each cell. (The minimum increment along the y-axis.)
        public int borderSize;          //< The AABB border size used to generate the source data from which the mesh was derived.
        public float maxEdgeError;      //< The max error of the polygon edges in the mesh.

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("bmin: " + bmin[0] + " " + bmin[1] + " " + bmin[2]);
            sb.AppendLine("bmax: " + bmax[0] + " " + bmax[1] + " " + bmax[2]);
            sb.AppendLine("cs: " + cs);
            sb.AppendLine("ch: " + ch);
            sb.AppendLine("bordersize: " + borderSize);
            sb.AppendLine("maxEdgeError: " + maxEdgeError);

            sb.AppendLine("nverts: " + nverts);
            for (int i = 0; i < nverts; ++i)
            {
                int vIndex = i * 3;
                sb.AppendLine("\tverts[" + i + "]: x:" + verts![vIndex] + " y:" + verts[vIndex + 1] + " z:" + verts[vIndex + 2]);
            }
            sb.AppendLine("\tmaxpolys: " + maxpolys);
            sb.AppendLine("\tnvp: " + nvp);
            sb.AppendLine("\tnpolys: " + npolys);
            for (int i = 0; i < maxpolys; ++i)
            {
                int vIndex = i * nvp;
                sb.Append("\t\tpolys[" + i + "]: ");
                for (int j = 0; j < nvp; ++j)
                {
                    sb.Append(" " + j + ":" + polys![vIndex + j]);
                }

                vIndex += nvp;
                sb.AppendLine();
                sb.Append("\t\tneighbor[" + i + "]: ");
                for (int j = 0; j < nvp; ++j)
                {
                    sb.Append(" " + j + ":" + polys![vIndex + j]);
                }
                sb.AppendLine();
            }

            for (int i = 0; i < maxpolys; ++i)
            {
                sb.AppendLine("regs[" + i + "]: " + regs![i]);
            }
            sb.AppendLine();
            for (int i = 0; i < flags!.Length; ++i)
            {
                sb.AppendLine("flags[" + i + "]: " + flags[i]);
            }

            return sb.ToString();
        }

        public string ToObj()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("# Recast Navmesh");
            sb.AppendLine("o NavMesh");

            sb.AppendLine();

            for (int i = 0; i < nverts; ++i)
            {
                //ushort* v = &pmesh.verts[i*3];
                int vIndex = i * 3;
                float x = bmin[0] + verts![vIndex + 0] * cs;
                float y = bmin[1] + (verts[vIndex + 1] + 1) * ch + 0.1f;
                float z = bmin[2] + verts[vIndex + 2] * cs;
                //ioprintf(io, "v %f %f %f\n", x,y,z);
                sb.AppendLine("v " + x + " " + y + " " + z);
            }

            sb.AppendLine();

            for (int i = 0; i < npolys; ++i)
            {
                //const unsigned short* p = &pmesh.polys[i*nvp*2];
                int pIndex = i * nvp * 2;
                for (int j = 2; j < nvp; ++j)
                {
                    if (polys![pIndex + j] == Recast.RC_MESH_NULL_IDX)
                        break;
                    //ioprintf(io, "f %d %d %d\n", p[0]+1, p[j-1]+1, p[j]+1); 
                    int a = polys[pIndex] + 1;
                    int b = polys[pIndex + j - 1] + 1;
                    int c = polys[pIndex + j] + 1;
                    sb.AppendLine("f " + a + " " + b + " " + c);
                }
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// 提供了多边形详细的三角片元和高度信息
    /// </summary>
    public class rcPolyMeshDetail
    {
        public uint[]? meshes = null;   //< The sub-mesh data. [Size: 4*#nmeshes] 
        public float[]? verts = null;           //< The mesh vertices. [Size: 3*#nverts] 
        public byte[]? tris = null; //< The mesh triangles. [Size: 4*#ntris] 
        public int nmeshes;         //< The number of sub-meshes defined by #meshes.
        public int nverts;              //< The number of vertices in #verts.
        public int ntris;               //< The number of triangles in #tris.

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("nmeshes: " + nmeshes);
            for (int i = 0; i < nmeshes; ++i)
            {
                int vIndex = i * 4;
                sb.AppendLine("\tmeshes[" + i + "]: a:" + meshes![vIndex] + " b:" + meshes[vIndex + 1] + " c:" + meshes[vIndex + 2] + " d:" + meshes[vIndex + 3]);
            }
            sb.AppendLine("nverts: " + nverts);
            for (int i = 0; i < nverts; ++i)
            {
                int vIndex = i * 3;
                sb.AppendLine("\tverts[" + i + "]: x:" + verts![vIndex] + " y:" + verts[vIndex + 1] + " z:" + verts[vIndex + 2]);
            }
            sb.AppendLine("ntris: " + ntris);
            for (int i = 0; i < ntris; ++i)
            {
                int vIndex = i * 4;
                sb.AppendLine("\ttris[" + i + "]: a:" + tris![vIndex] + " b:" + tris[vIndex + 1] + " c:" + tris[vIndex + 2] + " d:" + tris[vIndex + 3]);
            }

            return sb.ToString();
        }

        public string ToObj()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("# Recast C# Navmesh\n");
            sb.AppendLine("o NavMesh\n");

            sb.AppendLine("\n");

            for (int i = 0; i < nverts; ++i)
            {
                int vIndex = i * 3;
                sb.AppendLine("v " + verts![vIndex + 0] + " " + verts[vIndex + 1] + " " + verts[vIndex + 2]);
            }

            sb.AppendLine();

            for (int i = 0; i < nmeshes; ++i)
            {
                //uint* m = &dmesh.meshes[i*4];
                int mIndex = i * 4;
                uint bverts = meshes![mIndex + 0];
                uint btris = meshes[mIndex + 2];
                uint _ntris = meshes[mIndex + 3];
                uint trisIndex = btris * 4;
                for (uint j = 0; j < _ntris; ++j)
                {
                    sb.AppendLine("f "
                                    + ((int)(bverts + tris![trisIndex + j * 4 + 0]) + 1) + " "
                                    + ((int)(bverts + tris[trisIndex + j * 4 + 1]) + 1) + " "
                                    + ((int)(bverts + tris[trisIndex + j * 4 + 2]) + 1) + " ");
                }
            }

            return sb.ToString();
        }
    }

}