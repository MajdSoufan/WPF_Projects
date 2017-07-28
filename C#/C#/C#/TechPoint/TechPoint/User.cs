using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechPoint
{
    class User
    {
        public string userID { get; set; }
        public int coinsNum  { get; set; }
        public int attemptsNum { get; set; }

        // Constructors 
        public User()
        {
            coinsNum = 0;
            attemptsNum = 0;
        }

        public User(string userID)
        {
            this.userID = userID;
            coinsNum = 0;
            attemptsNum = 0;
        }

        // public Methods
        public void incrementAttempts()
        {
            attemptsNum++;
        }

        public void incrementCoins()
        {
            coinsNum++;
        }

    }
}
