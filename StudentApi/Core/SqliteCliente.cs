using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Core
{
    internal class SqliteCliente
    {
        private string connectionString;

        public SqliteCliente(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
