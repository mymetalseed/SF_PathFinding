using SF_Recast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_SoloMesh
{
    public InputGeom m_geom;

    rcHeightField m_solid;
    rcCompactHeightfield m_chf;
    rcContourSet m_cset;
    rcPolyMesh m_pmesh;
    rcConfigRuntime m_cfg;
    rcPolyMeshDetail m_dmesh;

    List<WalkableEnum> m_triareas;
    public Sample_SoloMesh()
    {
        m_geom = new InputGeom();
    }

    public void handleMeshChanged(Mesh mesh)
    {
        if (m_geom == null) return;
        if (m_geom.LoadMesh(mesh))
        {

        }
    }

    /// <summary>
    /// ����NavMesh
    /// </summary>
    /// <returns></returns>
    public bool handleBuild()
    {
        cleanup();
        Vector3 bmin = m_geom.m_meshBMin;
        Vector3 bmax = m_geom.m_meshBMax;
        List<Vector3> verts = m_geom.m_mesh.getVerts();
        List<Vector3Int> tris = m_geom.m_mesh.getTris();

        rcConfig config = rcConfig.Instance;
        m_cfg = new rcConfigRuntime();
        m_cfg.cs = config.m_cellSize;
        m_cfg.ch = config.m_cellHeight;
        m_cfg.walkableSlopeAngle = config.m_agentMaxSlope;
        m_cfg.walkableHeight = Mathf.CeilToInt(config.m_agentHeight / m_cfg.ch);
        m_cfg.walkableClimb = Mathf.FloorToInt(config.m_agentMaxClimb / m_cfg.ch);
        m_cfg.walkableRadius = Mathf.CeilToInt(config.m_agentRadius / m_cfg.cs);
        m_cfg.maxEdgeLen = (int)(config.m_edgeMaxLen / config.m_cellSize);
        m_cfg.maxSimplificationError = config.m_edgeMaxError;
        m_cfg.minRegionArea = (int)Mathf.Sqrt(config.m_regionMinSize);      // Note: area = size*size
        m_cfg.mergeRegionArea = (int)Mathf.Sqrt(config.m_regionMergeSize);  // Note: area = size*size
        m_cfg.maxVertsPerPoly = (int)config.m_vertsPerPoly;
        m_cfg.detailSampleDist = config.m_detailSampleDist < 0.9f ? 0 : config.m_cellSize * config.m_detailSampleDist;
        m_cfg.detailSampleMaxError = config.m_cellHeight * config.m_detailSampleMaxError;

        //������Ҫ����navigation�ط������
        //bmin��bmax�����������mesh,���ǿ���ʹ���û��Զ����box��������Ҫ�����Ĵ�С
        m_cfg.bmin = bmin;
        m_cfg.bmax = bmax;
        RecastHelper.rcCalcGridSize(m_cfg.bmin, m_cfg.bmax, m_cfg.cs, ref m_cfg.width, ref m_cfg.height);

        //Step 2. ������Ķ���ν��й�դ��
        //heightField��BoundaryBox��cfgһ��
        m_solid = new rcHeightField();
        RecastHelper.rcCreateHeightField(m_solid,m_cfg.width,m_cfg.height,m_cfg.bmin,m_cfg.bmax,m_cfg.cs,m_cfg.ch);

        //��ǿ������ߵ�������(��tri���±���)
        m_triareas = new List<WalkableEnum>();
        RecastHelper.rcMarkWalkableTriangles(m_cfg.walkableSlopeAngle,verts,verts.Count,tris,tris.Count,m_triareas);



        return false;
    }

    /// <summary>
    /// �������ģ��
    /// </summary>
    void cleanup()
    {
        
    }
}
