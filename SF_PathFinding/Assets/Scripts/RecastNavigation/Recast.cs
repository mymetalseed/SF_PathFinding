using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SF_Recast
{

    public static partial class Recast
    {
        public const int RC_SPANS_PER_POOL = 2048;
        /// An value which indicates an invalid index within a mesh.
        /// @note This does not necessarily indicate an error.
        /// @see rcPolyMesh::polys
        public const ushort RC_MESH_NULL_IDX = 0xffff;
        //PIֵ
        public const float RC_PI = 3.14159265f;
        //����rcSpan����Ҫ��bit����
        public const int RC_SPAN_HEIGHT_BITS = 13;
        //����rcSpan��ʵ�����ֵ
        public const int RC_SPAN_MAX_HEIGHT = (1 << RC_SPAN_HEIGHT_BITS) - 1;

        /// <summary>
        /// Log
        /// </summary>
        public enum rcLogCategory
        {
            RC_LOG_PROGRESS = 1,
            RC_LOG_WARNING,
            RC_LOG_ERROR,
        }

        //��������Ҫ��ʱ�����
        public enum rcTimerLabel
        {
            //�ܻ���ʱ��
            RC_TIMER_TOTAL,
            //������ʱ��
            RC_TIMER_TEMP,
            //��դ�������ε�ʱ��(: #rcRasterizeTriangle)
            RC_TIMER_RASTERIZE_TRIANGLES,
            //�����߶��򻨷�ʱ��(: #rcBuildCompactHeightField)
            RC_TIMER_BUILD_COMPACTHEIGHTTFIELD,
            //����������������ʱ��
            RC_TIMER_BUILD_CONTOURS,
            /// The time to trace the boundaries of the contours. (See: #rcBuildContours)
            RC_TIMER_BUILD_CONTOURS_TRACE,
            /// The time to simplify the contours. (See: #rcBuildContours)
            RC_TIMER_BUILD_CONTOURS_SIMPLIFY,
            /// The time to filter ledge spans. (See: #rcFilterLedgeSpans)
            RC_TIMER_FILTER_BORDER,
            /// The time to filter low height spans. (See: #rcFilterWalkableLowHeightSpans)
            RC_TIMER_FILTER_WALKABLE,
            /// The time to apply the median filter. (See: #rcMedianFilterWalkableArea)
            RC_TIMER_MEDIAN_AREA,
            /// The time to filter low obstacles. (See: #rcFilterLowHangingWalkableObstacles)
            RC_TIMER_FILTER_LOW_OBSTACLES,
            /// The time to build the polygon mesh. (See: #rcBuildPolyMesh)
            RC_TIMER_BUILD_POLYMESH,
            /// The time to merge polygon meshes. (See: #rcMergePolyMeshes)
            RC_TIMER_MERGE_POLYMESH,
            /// The time to erode the walkable area. (See: #rcErodeWalkableArea)
            RC_TIMER_ERODE_AREA,
            /// The time to mark a box area. (See: #rcMarkBoxArea)
            RC_TIMER_MARK_BOX_AREA,
            /// The time to mark a cylinder area. (See: #rcMarkCylinderArea)
            RC_TIMER_MARK_CYLINDER_AREA,
            /// The time to mark a convex polygon area. (See: #rcMarkConvexPolyArea)
            RC_TIMER_MARK_CONVEXPOLY_AREA,
            /// The total time to build the distance field. (See: #rcBuildDistanceField)
            RC_TIMER_BUILD_DISTANCEFIELD,
            /// The time to build the distances of the distance field. (See: #rcBuildDistanceField)
            RC_TIMER_BUILD_DISTANCEFIELD_DIST,
            /// The time to blur the distance field. (See: #rcBuildDistanceField)
            RC_TIMER_BUILD_DISTANCEFIELD_BLUR,
            /// The total time to build the regions. (See: #rcBuildRegions, #rcBuildRegionsMonotone)
            RC_TIMER_BUILD_REGIONS,
            /// The total time to apply the watershed algorithm. (See: #rcBuildRegions)
            RC_TIMER_BUILD_REGIONS_WATERSHED,
            /// The time to expand regions while applying the watershed algorithm. (See: #rcBuildRegions)
            RC_TIMER_BUILD_REGIONS_EXPAND,
            /// The time to flood regions while applying the watershed algorithm. (See: #rcBuildRegions)
            RC_TIMER_BUILD_REGIONS_FLOOD,
            /// The time to filter out small regions. (See: #rcBuildRegions, #rcBuildRegionsMonotone)
            RC_TIMER_BUILD_REGIONS_FILTER,
            /// The time to build heightfield layers. (See: #rcBuildHeightfieldLayers)
            RC_TIMER_BUILD_LAYERS,
            /// The time to build the polygon mesh detail. (See: #rcBuildPolyMeshDetail)
            RC_TIMER_BUILD_POLYMESHDETAIL,
            /// The time to merge polygon mesh details. (See: #rcMergePolyMeshDetails)
            RC_TIMER_MERGE_POLYMESHDETAIL,
            /// The maximum number of timers.  (Used for iterating timers.)
            RC_MAX_TIMERS


        }


        //public static void rcCalcBound(List<>)

    }
}