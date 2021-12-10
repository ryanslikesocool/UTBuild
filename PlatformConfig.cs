// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR
using System;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.Build.Reporting;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace UTBuild {
    [Serializable]
    public abstract class PlatformConfig {
#if ODIN_INSPECTOR_3
        [ToggleGroup("include", "$DisplayString")] public bool include;
        [ToggleGroup("include")] public bool developmentBuild;
        [ToggleGroup("include")] public string buildName;
        [ToggleGroup("include")] public Platform platform;
        [ToggleGroup("include")] public Compression compression;
        [ToggleGroup("include")] public SceneAsset scene;

        private string DisplayString => $"{PlatformToString()} - \"{buildName}\"{DevelopmentString}";
        private string DevelopmentString => developmentBuild ? " - Development" : string.Empty;

        private string PlatformToString() => platform switch {
            Platform.Android => "Android",
            Platform.iOS => "iOS",
            Platform.Linux => "Linux",
            Platform.macOS => "macOS (Universal)",
            Platform.macOS_Intel => "macOS (Intel)",
            Platform.macOS_Silicon => "macOS (Silicon)",
            Platform.tvOS => "tvOS",
            Platform.Windows => "Windows",
            _ => "Undefined"
        };
#else
        public bool include;
        public bool developmentBuild;
        public string buildName;
        public Platform platform;
        public Compression compression;
        public SceneAsset scene;
#endif

        internal abstract void ProcessScene(Scene scene, BuildReport report);
    }
}
#endif