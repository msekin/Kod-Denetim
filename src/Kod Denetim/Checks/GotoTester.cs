using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis.Checks
{
    class GotoTester : ITester
    {
        private LinkedList<Error> ErrorList;

        public GotoTester()
        {
            ErrorList = new LinkedList<Error>();
        }

        public void Test(IParseTree t)
        {
            TerminalNodeImpl terminaNode = t as TerminalNodeImpl;
            if (terminaNode != null)
            {
                if(terminaNode.Symbol.Type == CPP14Lexer.Goto)
                {
                    ErrorList.AddLast(new Error(terminaNode.Symbol.Line, "Goto kullanımı"));
                }
            }
        }

        public LinkedList<Error> GetErrors()
        {
            return ErrorList;
        }

        public void ClearErrors()
        {
            ErrorList.Clear();
        }
    }
}
