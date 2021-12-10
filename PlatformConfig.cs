// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.Build.Reporting;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace UTBuild {
    [Serializable]
    public abstract class PlatformConfig : ScriptableObject {
        [SerializeField] internal bool developmentBuild = false;
        [SerializeField] internal string buildName = "Build";
        [SerializeField] internal Platform platform;
        [SerializeField] internal Compression compression = Compression.LZ4;
        [SerializeField] internal SceneAsset[] scenes = new SceneAsset[0];

        public abstract void ProcessScene(Scene scene, BuildReport report);
    }
}
#endif