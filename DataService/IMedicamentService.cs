using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCore;

namespace DataService
{
    interface IMedicamentService
    {
        void  set();
        Task<IEnumerable<Medicament>> get();

        
    }
}
