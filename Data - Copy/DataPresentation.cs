using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataPresentation<Data> : IDataPresentation<Data>
    {
        public abstract void Show(List<Data> data);

        protected void DrawRow(List<string> values, bool isHeader)
        {

            StringBuilder sb = new StringBuilder();
            if (isHeader)
            {
                sb.Append('=', 80);
                sb.AppendLine();
            }
            foreach (string value in values)
            {
                sb.Append("| ");
                sb.Append($"{Utility.TruncateString(value, 15),-15}");
                sb.Append(" | ");
            }

            if (isHeader)
            {
                sb.Append('=', 80);
                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
