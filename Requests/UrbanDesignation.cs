using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheatleyAPI.Requests
{
    public class UrbanDesignation // модель отриманих даних від публ.АРІ
    {
        public List<List> list { get; set; }
    }

    public class List
    {
        public string definition { get; set; }
        public string word { get; set; }
        public string example { get; set; }
    }

}
