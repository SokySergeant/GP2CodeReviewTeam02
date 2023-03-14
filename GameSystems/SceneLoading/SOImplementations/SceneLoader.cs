using System;
using System.Collections;
using GameSystems.GameEventLogic;
using GameSystems.UpdateLoopsUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystems.SceneLoading.SOImplementations {
    [CreateAssetMenu(menuName = "Programmer Tools/New SceneManager", fileName = "Scene Manager")]

    public class SceneLoader : ScriptableObject
    {
        private float _loadProgress = 0f;
        [SerializeField] private UpdateManager _update = null;

        private SceneManager _sceneManager = null;
        [SerializeField] private GameEvent _onNonMenuSceneLoaded;
        
        
        public void LoadMainMenu() {
            EnsureSceneManager();
            var buildIndex = (int)BuildIndex.MainMenu;
            var level = SceneManager.GetSceneByBuildIndex(buildIndex);

            if (!IsLoaded((int)BuildIndex.MainMenu)) {
                TriggerLoadSceneCoroutine(buildIndex, true);
            }
        }
        
        public void LoadScene(int buildIndex)
        {
            EnsureSceneManager();
            var level = SceneManager.GetSceneByBuildIndex(buildIndex);
            if (!IsLoaded(buildIndex)) {
                TriggerLoadSceneCoroutine(buildIndex, true);
            }
        }

        private void TriggerLoadSceneCoroutine(int index, bool activate = false) {
            _update.StartCoroutine(
                LoadScene(index, activate)
            );
        }
        
        private IEnumerator LoadScene(int sceneIndex, bool activate) {
            var async = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

            while (!async.isDone) {
                if (Math.Abs(async.progress - _loadProgress) > 0.01) {
                    _loadProgress = async.progress / 0.9f;
                }
                yield return null;
            }
            UnloadOtherScenes(sceneIndex);
            if (activate) SetActive(sceneIndex);
        }
        
        private void TriggerUnloadSceneCoroutine(int index){
            _update.StartCoroutine(
                UnloadScene(index)
            );
        }
        private IEnumerator UnloadScene(int index){
            var async = SceneManager.UnloadSceneAsync(index);

            while (!async.isDone){
                _loadProgress = async.progress;
                yield return null;
            }
        }

        private void UnloadOtherScenes(int sceneIndex) {
            if (sceneIndex != (int)BuildIndex.MainMenu && IsLoaded(1))
            {
                TriggerUnloadSceneCoroutine(1);
            }
            if (sceneIndex != (int)BuildIndex.Game && IsLoaded(2))
            {
                TriggerUnloadSceneCoroutine(2);
            }
        }
        private void SetActive(int index) {
            var scene = SceneManager.GetSceneByBuildIndex(index);
            if (!scene.IsValid()) return;
            SceneManager.SetActiveScene(scene);
        }
        
        private void EnsureSceneManager() {
            _sceneManager ??= new SceneManager();
        }
        public bool IsLoaded(int index) {
            var scene = SceneManager.GetSceneByBuildIndex(index);
            return scene.IsValid();
        }
        private enum BuildIndex {
            MainMenu = 1,
            Game = 2,
        }
    }
}
