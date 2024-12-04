using DTT.MinigameMemory;
using Naninovel;
using UnityEngine;

namespace MemoryGame
{
    [InitializeAtRuntime]
    public class MemoryGameService : IMemoryGameService
    {
        private CustomVariableManager _variableManager;
        private MemoryGameManager _gamePrefab;
        private MemoryGameManager _gameManager;
        private bool _isGameActive;
        private bool _hasGamePlayed;
        private UniTaskCompletionSource _gameCompletionSource;

        private const string GameHasBeenPlayedVariableName = "hasMemoryGamePlayed";
        private const string GamePrefabPath = "MemoryGame";
        private const string GameSettingsPath = "Demo Easy";

        public bool IsGameActive => _isGameActive;

        public async UniTask InitializeServiceAsync()
        {
            _variableManager = Engine.GetService<CustomVariableManager>();
            _gamePrefab = Resources.Load<MemoryGameManager>(GamePrefabPath);
            await UniTask.Yield();
        }

        public void ResetService()
        {
            _isGameActive = false;
        }

        public void DestroyService()
        {
            if (_gameManager != null)
            {
                GameObject.Destroy(_gameManager.gameObject);
                _gameManager = null;
            }
        }

        public void StartGame()
        {
            if (_isGameActive) return;

            _gameManager = GameObject.Instantiate(_gamePrefab);
            _gameManager.Finish += HandleGameFinished;

            var gameSettings = Resources.Load<MemoryGameSettings>(GameSettingsPath);
            _gameManager.StartGame(gameSettings);

            _isGameActive = true;
            _gameCompletionSource = new UniTaskCompletionSource();
        }

        public UniTask WaitForGameEndAsync()
        {
            return _gameCompletionSource?.Task ?? UniTask.CompletedTask;
        }

        public void SetGameHasBeenPlayed()
        {
            _variableManager.SetVariableValue(GameHasBeenPlayedVariableName, _hasGamePlayed.ToString());
        }

        private void HandleGameFinished(MemoryGameResults results)
        {
            if (!_isGameActive) return;

            _isGameActive = false;
            _hasGamePlayed = true;
            _gameCompletionSource?.TrySetResult();

            DestroyService();
        }
    }
}
