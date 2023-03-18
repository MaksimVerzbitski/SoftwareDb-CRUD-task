using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataPresentation<T> : IDataPresentation<T> where T : Data
    {
        public abstract void Show(List<T> data);

        protected void DrawRow(List<string> values, bool isHeader)
        {

            StringBuilder sb = new StringBuilder(104);
            if (isHeader)
            {
                sb.Append('=', 104);
                sb.AppendLine();
            }



            foreach (string value in values)
            {
                sb.Append("| ");
                sb.Append($"{Utility.TruncateString(value, 15),-15}");

            }
            sb.Append(" | ");
            sb.AppendLine();
            sb.Append('=', 104);
            sb.AppendLine();

            Console.WriteLine(sb.ToString());
        }
    }
}
