using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheatleyAPI.DataTransferObjects
{
    public class SavedWordsDto // модель даних, що повертаються 
    {
        public List<string> saved_words { get; set; }    
    }
}
