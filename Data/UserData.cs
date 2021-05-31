using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheatleyAPI.Data
{  
    public class UserData // модель збережених даних у json
    {
        public List<User> users { get; set; }
    }

    public class User
    {
        public string user_id { get; set; }
        public string user_surname { get; set; }
        public string user_name { get; set; }

        public List<string> saved_words { get; set; }
    }
}
