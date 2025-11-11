namespace Game.GameData
{
    public class GameState
    {
        public Dictionary<string, int> UsersState = new();
        public Dictionary<char, int> CharOfStartWord = new();
        public List<string> UsedWords = new();
        public string? Player1Nickname {  get; set; }
        public string? Player2Nickname {  get; set; }
        public string? Input { get; set; }
        public string? StartWord { get; set; }
        public string? Winner { get; set; }
        public string? NameOfCurrentPlayer { get; set; }
        public int Player1Score { get; set; } = 0;
        public int Player2Score { get; set; } = 0;
        public  bool CurrentPlayer { get; set; } = false;
    }
}
