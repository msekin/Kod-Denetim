using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis.Checks
{
    interface ITester
    {
        void Test(IParseTree t);
        LinkedList<Error> GetErrors();
    }
}
