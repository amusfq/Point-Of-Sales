using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point_Of_Sales.Model
{
    class Barang
    {
        public String id_barang { get; set; }
        public String nama_barang { get; set; }
        public String harga { get; set; }
        public String disc { get; set; }
        public String qty { get; set; }
        public String subTotal { get; set; }

        public static implicit operator ObservableCollection<object>(Barang v)
        {
            throw new NotImplementedException();
        }
    }
}
