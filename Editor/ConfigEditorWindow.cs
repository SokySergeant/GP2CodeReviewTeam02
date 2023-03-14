using System.Collections.Generic;
using GameSystems.Configuration_Settings;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Editor {
    public class ConfigEditorWindow : EditorWindow {
    
        [FormerlySerializedAs("UXMLFile")] [SerializeField] private VisualTreeAsset _uxmlFile;
        private List<VisualElement> _changableElements = null;
        [FormerlySerializedAs("Config")] private Configuration _config = null;

        [MenuItem(("Tools/Config Editor"))]
        public static void ShowWindow() {
            ConfigEditorWindow window = GetWindow<ConfigEditorWindow>();
            window.titleContent = new GUIContent("Configuration Settings");
        }

        private void OnEnable() {
            _config = AssetDatabase.LoadAssetAtPath<Configuration>("Assets/Config Files/GameConfig.asset");
            var serializedObj = new SerializedObject(_config);
            rootVisualElement.Bind(serializedObj);
        }
        private void CreateGUI() {
            _uxmlFile.CloneTree(rootVisualElement);
            if (_changableElements == null) {
                _changableElements = rootVisualElement.Query(className: "ConfigureWindowElement").ToList();
            }
            BindElements();
        }

   
        private void BindElements() {
            foreach (var element in _changableElements) {
                if (element == null) {
                    continue;
                }
                BindElement(element);
            }
      
        }
        private void BindElement(VisualElement element) {
            ScriptableObject objRef = _config.FindSOVariable(element.name);
            if (objRef == null) {
                Debug.Log(" This element was null " + element.name);
                return;
            }
            var serializedObject = new SerializedObject(_config.FindSOVariable(element.name));
            element.Bind(serializedObject);
        }  
    }
}
