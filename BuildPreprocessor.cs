// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine.SceneManagement;

namespace UTBuild {
    internal class BuildPreprocessor : IProcessSceneWithReport {
        public int callbackOrder => int.MinValue;

        public void OnProcessScene(Scene scene, BuildReport report) {
            PlatformConfig config = BuildConfiguration.ActiveConfig;
            if (config.Equals(default(PlatformConfig))) {
                return;
            }
            config.ProcessScene(scene, report);
        }
    }
}
#endif
