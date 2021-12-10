// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR
using System;
using UnityEngine;

namespace UTBuild {
    [Flags]
    public enum Platform {
        [InspectorName("macOS (Intel)")] macOS_Intel = 1 << 0,
        [InspectorName("macOS (Apple)")] macOS_Silicon = 1 << 1,
        macOS = macOS_Intel | macOS_Silicon,
        [InspectorName("iOS")] iOS = 1 << 2,
        [InspectorName("tvOS")] tvOS = 1 << 3,
        Windows = 1 << 4,
        Android = 1 << 5,
        Linux = 1 << 6,
    }

    public enum Compression {
        Default,
        LZ4,
        [InspectorName("LZ4HC")] LZ4HC
    }
}
#endif