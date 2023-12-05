using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecApplication
{
    public class GameAttempt
    {
        public DateTime Timestamp { get; set; }
        public int TimeTakenInSeconds { get; set; }
        public string Difficulty { get; set; }
    }
}
