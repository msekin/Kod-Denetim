using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis.Checks
{
    class AssignmentInIfTester : ITester
    {
        private LinkedList<Error> ErrorList;

        public AssignmentInIfTester()
        {
            ErrorList = new LinkedList<Error>();
        }

        public void Test(IParseTree t)
        {
            CPP14Parser.ConditionContext ctx = t as CPP14Parser.ConditionContext;
            if(ctx != null)
            {
                if(Walk(t))
                {
                    ErrorList.AddLast(new Error(ctx.Start.Line, "if içerisinde atama."));
                }
            }
        }

        public LinkedList<Error> GetErrors()
        {
            return ErrorList;
        }

        private bool Walk(IParseTree t)
        {
            ITerminalNode terminalNode = t as ITerminalNode;
            if (terminalNode != null)
            {
                return terminalNode.Symbol.Type == CPP14Lexer.Assign;
            }
            bool result = false;
            for (int i = 0; i < t.ChildCount; i++)
            {
                result = Walk(t.GetChild(i));
                if (result)
                    break;
            }
            return result;
        }
    }
}
