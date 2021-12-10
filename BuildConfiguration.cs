// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Build.Reporting;

namespace UTBuild {
    [CreateAssetMenu(menuName = "Developed With Love/UTBuild/Build Configuration")]
    public class BuildConfiguration : ScriptableObject {
        public string buildPath = "Builds";
        public PlatformConfig[] configs = new PlatformConfig[0];

        internal static PlatformConfig ActiveConfig { get; private set; }

        internal void Build() {
            Scene previous = EditorSceneManager.GetActiveScene();
            string initialPath = previous.path;
            BuildPlayerOptions[] opts = SelectedBuildOptions();
            for (int i = 0; i < opts.Length; i++) {
                if (!configs[i].include) { continue; }

                string path = AssetDatabase.GetAssetPath(configs[i].scenes[0]);
                Scene scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
                EditorSceneManager.CloseScene(previous, true);
                previous = scene;

                ActiveConfig = configs[i];

                BuildReport report = BuildPipeline.BuildPlayer(opts[i]);
                string err = report.summary.result == BuildResult.Succeeded ? string.Empty : "See log";

                if (!string.IsNullOrEmpty(err)) {
                    throw new InvalidOperationException($"Build error: {err}");
                }
            }
            EditorSceneManager.OpenScene(initialPath, OpenSceneMode.Single);
            EditorSceneManager.CloseScene(previous, true);

            ActiveConfig = default(PlatformConfig);
        }

        private BuildPlayerOptions[] SelectedBuildOptions() => configs.Select(c => BuildOpts(c, c.platform)).ToArray();

        private BuildTarget UnityTarget(Platform t) => t switch {
            Platform.macOS_Intel => BuildTarget.StandaloneOSX,
            Platform.macOS_Silicon => BuildTarget.StandaloneOSX,
            Platform.macOS => BuildTarget.StandaloneOSX,
            Platform.iOS => BuildTarget.iOS,
            Platform.tvOS => BuildTarget.tvOS,
            Platform.Android => BuildTarget.Android,
            Platform.Windows => BuildTarget.StandaloneWindows64,
            Platform.Linux => BuildTarget.StandaloneLinux64,
            _ => throw new NotImplementedException("Target not supported")
        };

        private BuildPlayerOptions BuildOpts(PlatformConfig settings, Platform target) {
            BuildPlayerOptions o = new BuildPlayerOptions();

            o.scenes = settings.scenes.Select(scene => AssetDatabase.GetAssetPath(scene)).ToArray();
            string subfolder = target.ToString();
            o.locationPathName = Path.Combine(buildPath, subfolder);
            o.locationPathName = Path.Combine(o.locationPathName, settings.buildName);

            o.target = UnityTarget(target);
            BuildOptions opts = GetCompression(settings.compression);
            if (settings.developmentBuild) {
                opts |= BuildOptions.Development;
            }
            o.options = opts;

            if (settings.platform.HasFlag(Platform.macOS_Intel) || settings.platform.HasFlag(Platform.macOS_Silicon)) {
                string architecture = string.Empty;
                switch (settings.platform) {
                    case Platform.macOS_Intel:
                        architecture = "x64";
                        break;
                    case Platform.macOS_Silicon:
                        architecture = "ARM64";
                        break;
                    default:
                        architecture = "x64ARM64";
                        break;
                }
                EditorUserBuildSettings.SetPlatformSettings(
                    "Standalone",
                    "OSXUniversal",
                    "Architecture",
                    architecture
                );
            }

            return o;
        }

        private BuildOptions GetCompression(Compression compression) {
            switch (compression) {
                case Compression.LZ4: return BuildOptions.CompressWithLz4;
                case Compression.LZ4HC: return BuildOptions.CompressWithLz4HC;
                default: return BuildOptions.None;
            }
        }
    }
}
#endif