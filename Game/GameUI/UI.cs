using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameUI
{
    public class UI : IGameUI
    {
        public string? ReadUserInput() => Console.ReadLine()?.ToLower() ?? "";
        public void ClearUI() => Console.Clear();
        public void WaitForUser() => Console.ReadKey();
        public void PrintToUI(string content) => Console.WriteLine(content);
        public void PrintInLineToUI(string content) => Console.Write(content);
    }
}
