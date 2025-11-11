namespace Game.Interfaces
{
    public interface IGameUI
    {
        string? ReadUserInput();
        void ErrorColor();
        void InformationColor();
        void StandartColor();
        void ClearUI();
        void WaitForUser();
        void PrintToUI(string content);
        void PrintInLineToUI(string content);
    }
}
