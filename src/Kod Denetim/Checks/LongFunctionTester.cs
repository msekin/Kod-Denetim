using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis.Checks
{
    class LongFunctionTester : ITester
    {
        private LinkedList<Error> ErrorList;

        public LongFunctionTester()
        {
            ErrorList = new LinkedList<Error>();
        }

        public void Test(IParseTree t)
        {
            CPP14Parser.FunctionbodyContext ctx = t as CPP14Parser.FunctionbodyContext;
            if (ctx != null)
            {
                int gap = ctx.Stop.Line - ctx.Start.Line + 1;
                if (gap > Properties.Settings.Default.FunctionMaxLength)
                {
                    ErrorList.AddLast(new Error(ctx.Start.Line, "Uzun fonksiyon"));
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
