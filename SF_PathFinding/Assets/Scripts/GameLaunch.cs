using SF_Recast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLaunch : MonoBehaviour
{
    static GameLaunch instance;
    public static GameLaunch Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            return null;
        }
    }

    public SerializeNav navEntity;

    Sample_SoloMesh soloMesh;
    public MeshFilter caculateMesh;

    public GameLaunchInfo launchInfo;

    public rcConfig rcConfig;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rcConfig.Init();
        soloMesh = new Sample_SoloMesh();
        soloMesh.handleMeshChanged(caculateMesh.mesh);


        StoreNavData();
    }

    
    void StoreNavData()
    {
        navEntity.bmin = soloMesh.m_geom.m_meshBMin;
        navEntity.bmax = soloMesh.m_geom.m_meshBMax;
    }

    private void OnDrawGizmos()
    {
        if (launchInfo.showAABB)
        {
            Vector3 c = navEntity.bmax + navEntity.bmin;
            Gizmos.DrawWireCube(c/2, (navEntity.bmax-navEntity.bmin));
        }
    }

}
