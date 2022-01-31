using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVConsole.DTOs
{
    public class Response
    {
        /// <summary>
        /// Si error es True no es feriado, si es False es feriado
        /// </summary>
        public bool error { get; set; } = false;
        /// <summary>
        /// Mensaje que devuelve api
        /// </summary>
        public string message { get; set; } = String.Empty;
        /// <summary>
        /// Nombre del feriado
        /// </summary>
        public string nombre { get; set; } = String.Empty ;
        /// <summary>
        /// Fecha del feriado
        /// </summary>
        public string fecha { get; set; } = String.Empty;
        /// <summary>
        /// Proceso terminado correctamente
        /// </summary>
        public bool Succeeded { get; set; }

    }
}
