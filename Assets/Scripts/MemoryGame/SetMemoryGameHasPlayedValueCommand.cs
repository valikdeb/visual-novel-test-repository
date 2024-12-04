using Naninovel;

namespace MemoryGame
{
    [CommandAlias("setMemoryGameHasPlayed")]
    public class SetMemoryGameHasPlayedValueCommand: Command
    {
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var gameService = Engine.GetService<IMemoryGameService>();
            gameService.SetGameHasBeenPlayed();
            await UniTask.Yield();
        }
    }
}