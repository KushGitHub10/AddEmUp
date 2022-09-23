using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddEmUp.Model
{
    public class Player
    {
        public string Name { get; set; }
        public List<string> Cards { get; set; }
        public int Score { get; set; }
    }
}
