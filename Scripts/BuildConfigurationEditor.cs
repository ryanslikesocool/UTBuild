// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR && !ODIN_INSPECTOR_3
using UnityEngine;
using UnityEditor;

namespace UTBuild {
    [CustomEditor(typeof(BuildConfiguration))]
    internal class BuildConfigurationEditor : Editor {
        private BuildConfiguration configuration = null;

        public void OnEnable() {
            configuration = (BuildConfiguration)target;
        }

        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            if (GUILayout.Button("Build", GUILayout.Height(30))) {
                configuration.Build();
            }
        }
    }
}
#endif