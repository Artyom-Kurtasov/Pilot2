using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
