// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.Build.Reporting;

namespace UTBuild {
    [Serializable, CreateAssetMenu(menuName = "Developed With Love/UTBuild/Generic Platform Config")]
    public class PlatformConfig : ScriptableObject {
        [SerializeField] internal bool developmentBuild = false;
        [SerializeField] internal string buildName = "Build";
        [SerializeField] internal Platform platform;
        [SerializeField] internal Compression compression = Compression.LZ4;
        [SerializeField] internal SceneAsset[] scenes = new SceneAsset[0];

#if ODIN_INSPECTOR_3
        public virtual string DisplayName => $"{buildName} ({platform})";
#endif

        public virtual void PreprocessApplication() { }

        public virtual void ProcessScene(Scene scene, BuildReport report) { }
    }
}
#endif