using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.PlayerManagment
{
    public class PlayerState
    {
        public LinkedList<Dictionary<string, int>> User1 { get; set; } = new LinkedList<Dictionary<string, int>>();
        public LinkedList<Dictionary<string, int>> User2 { get; set; } = new LinkedList<Dictionary<string, int>>();
    }
}
