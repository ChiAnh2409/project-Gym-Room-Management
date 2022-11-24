using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace quanlyphongGym.DTO
{
    internal class accountDTO
    {
        private string _username;
        private string _password;
        public string username
        {
            get { return _username; }
            set { _username = value; }

        }
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
    
}
