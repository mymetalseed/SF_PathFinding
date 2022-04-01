using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_SoloMesh
{
    public InputGeom m_geom;

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



}
