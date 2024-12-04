using Naninovel;

namespace MemoryGame
{
    public interface IMemoryGameService: IEngineService
    {
        /// <summary>
        /// Starts a new memory game.
        /// </summary>
        void StartGame();

        /// <summary>
        /// Waits asynchronously until the memory game ends.
        /// </summary>
        /// <returns>A task that completes when the memory game ends.</returns>
        UniTask WaitForGameEndAsync();

        /// <summary>
        /// Sets a variable indicating that the memory game has been played.
        /// </summary>
        void SetGameHasBeenPlayed();
    }
}