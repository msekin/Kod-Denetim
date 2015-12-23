using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis.Checks
{
    class ParamCountTester : ITester
    {
        private LinkedList<Error> ErrorList;

        public ParamCountTester()
        {
            ErrorList = new LinkedList<Error>();
        }

        public void Test(IParseTree t)
        {
            CPP14Parser.ParametersandqualifiersContext ctx = t as CPP14Parser.ParametersandqualifiersContext;
            if(ctx != null)
            {
                int count = Walk(t);
                if (count > Settings.MaxParamCount)
                {
                    ErrorList.AddLast(new Error(ctx.Start.Line, "Çok fazla parametre"));
                }
            }
        }
        public LinkedList<Error> GetErrors()
        {
            return ErrorList;
        }

        private int Walk(IParseTree t)
        {
            ParserRuleContext ctx = t as ParserRuleContext;
            if(ctx == null)
            {
                return 0;
            }
            if(ctx.RuleIndex == CPP14Parser.RULE_parameterdeclaration)
            {
                return 1;
            }
            int j = 0;
            for (int i = 0; i < ctx.ChildCount; i++)
            {
                j += Walk(ctx.children[i]);
            }
            return j;
        }
    }
}
