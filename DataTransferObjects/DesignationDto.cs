using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheatleyAPI.DataTransferObjects
{
    public class DesignationDto // модель даних, що повертаються 
    {
        public string dictionary { get; set; }
        public string word { get; set; }
        public string designation { get; set; }
        public string fl { get; set; }
        public List<List<string>> synonyms { get; set; }
        public List<List<string>> antonyms { get; set; }
        public bool offensive { get; set; }
        public string example { get; set; }

    }
}
