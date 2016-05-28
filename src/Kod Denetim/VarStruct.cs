using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeAnalysis
{
    public class VarStruct
    {
        public string type;
        public string name;
        public object initialValue;

        public VarStruct()
        {

        }

        public VarStruct(string type, string name)
        {
            this.type = type;
            this.name = name;
        }

        public VarStruct(string type, string name, object initialValue)
        {
            this.type = type;
            this.name = name;
            this.initialValue = initialValue;
        }
    }
}
