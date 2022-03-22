﻿using UnityEngine;

namespace SpatialPartitionSystem.Core.Series.Trees
{
    public sealed class Octree<TObject> : BaseTree<TObject, AABB3D, Vector3> where TObject : class
    {
        public Octree(AABB3D rootBounds, int maxLeafObjects, int maxDepth, int initialObjectsCapacity)
            : base(Dimension.Three, rootBounds, maxLeafObjects, maxDepth, initialObjectsCapacity) { }
    }
}
