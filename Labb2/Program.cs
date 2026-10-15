using Labb2.BussinessLogic.Concrete.GameLogic;

LevelData level = new LevelData();

level.Load(Path.Combine(
    AppContext.BaseDirectory,
    "Level1.txt"));

Game game = new Game(level);
game.Run();