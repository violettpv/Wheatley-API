using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheatleyAPI.Requests
{
    public class ThesaurusDesignation // модель отриманих даних від публ.АРІ
    {
        public Meta meta { get; set; }
        public string fl { get; set; }
        public List<string> shortdef { get; set; }
    }

    public class Meta
    {
        public string id { get; set; }
        public List<List<string>> syns { get; set; }
        public List<List<string>> ants { get; set; }
        public bool offensive { get; set; }
    }

}
