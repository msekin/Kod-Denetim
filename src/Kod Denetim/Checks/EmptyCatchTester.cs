using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis.Checks
{
    class EmptyCatchTester : ITester
    {
        private LinkedList<Error> ErrorList;

        public EmptyCatchTester()
        {
            ErrorList = new LinkedList<Error>();
        }

        public void Test(IParseTree t)
        {
            CPP14Parser.HandlerContext ctx = t as CPP14Parser.HandlerContext;
            if(ctx != null)
            {
                var compoundStatement = ctx.GetChild<CPP14Parser.CompoundstatementContext>(0);
                if(compoundStatement.ChildCount == 2)
                {
                    ErrorList.AddLast(new Error(ctx.Start.Line, "Boş catch bloğu. Exception yutulmamalı"));
                }
            }
        }

        public LinkedList<Error> GetErrors()
        {
            return ErrorList;
        }
    }
}
