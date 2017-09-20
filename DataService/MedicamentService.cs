using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCore;
using DapperLayer;

namespace DataService
{
    public class MedicamentService : IMedicamentService
    {
        public async Task<IEnumerable<Medicament>> get()
        {
           RepoDapperLayer _myrepo = new RepoDapperLayer();
           return await _myrepo.getMedicament();
        }

        public void set()
        {
            throw new NotImplementedException();
        }
    }
}
