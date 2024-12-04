using Naninovel;

namespace MemoryGame
{
    [CommandAlias("startMemoryGame")]
    public class StartMemoryGameCommand : Command
    {
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var memoryGameService = Engine.GetService<IMemoryGameService>();
            memoryGameService.StartGame();
            await memoryGameService.WaitForGameEndAsync();
        }
    }
}