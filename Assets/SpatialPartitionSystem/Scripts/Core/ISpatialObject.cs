﻿using UnityEngine;

namespace SpatialPartitionSystem.Core
{
    public interface ISpatialObject<TBounds> where TBounds : struct
    {
        TBounds Bounds { get; }
    }
}
