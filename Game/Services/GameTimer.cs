using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.GameData;
using Game.Interfaces;

namespace Game.Services
{
    public class GameTimer : IGameTimer
    {
        private readonly int _turnTimeLimit;
        private readonly int _timerIntervalMs;
        private Task? _timer;
        public bool IsTimerUp => _timer?.IsCompleted ?? false;

        public GameTimer()
        {
            _turnTimeLimit = GameConstants.TurnTimeLimit;
            _timerIntervalMs = GameConstants.TimerIntervalMs;
        }

        public void StartTimer()
        {
            _timer = TimerAsync(_turnTimeLimit);
        }
        private async Task TimerAsync(int seconds)
        {
            var tcs = new TaskCompletionSource<bool>();
            using var timer = new System.Timers.Timer(_timerIntervalMs);

            timer.Elapsed += (s, e) =>
            {
                seconds--;
                if (seconds <= 0)
                {
                    timer.Stop();
                    tcs.TrySetResult(true);
                }
            };

            timer.Start();
            await tcs.Task;
        }
    }
}
