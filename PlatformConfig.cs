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
#if ODIN_INSPECTOR_3
        [ToggleGroup("include", "$DisplayString")] internal bool include;
        [ToggleGroup("include")] internal bool developmentBuild;
        [ToggleGroup("include")] internal string buildName;
        [ToggleGroup("include")] internal Platform platform;
        [ToggleGroup("include")] internal Compression compression;
        [ToggleGroup("include")] internal SceneAsset[] scenes;

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
        internal bool include;
        internal bool developmentBuild;
        internal string buildName;
        internal Platform platform;
        internal Compression compression;
        internal SceneAsset scene;
#endif

        public abstract void ProcessScene(Scene scene, BuildReport report);
    }
}
#endif