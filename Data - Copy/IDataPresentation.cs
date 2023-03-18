using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDataPresentation<T>
    {
        void Show(List<T> data);
    }
}
