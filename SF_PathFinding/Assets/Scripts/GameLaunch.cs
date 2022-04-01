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

    Sample_SoloMesh soloMesh;
    public MeshFilter caculateMesh;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        soloMesh = new Sample_SoloMesh();
        soloMesh.handleMeshChanged(caculateMesh.mesh);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
