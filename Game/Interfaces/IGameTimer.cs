namespace Game.Interfaces
{
    public interface IGameTimer
    {
        void StartTimer();
        bool IsTimerUp { get; }
    }
}
