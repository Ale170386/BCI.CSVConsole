using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVConsole.DTOs
{
    public class ProcessLogDTO
    {
        public string Name { get; set; } = string.Empty;
        public DateTime ProcessDate { get; set; }
        public bool Error { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
