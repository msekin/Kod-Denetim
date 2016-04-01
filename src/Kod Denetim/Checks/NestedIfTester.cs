using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis.Checks
{
    class NestedIfTester : ITester
    {
        private LinkedList<Error> ErrorList;
        private int depth = 0;

        public NestedIfTester()
        {
            ErrorList = new LinkedList<Error>();
        }

        public void Test(IParseTree t)
        {
            depth = 0;
            CPP14Parser.SelectionstatementContext ctx = t as CPP14Parser.SelectionstatementContext;
            if (ctx != null)
            {
                Walk(t, 0);
                if (depth > Properties.Settings.Default.NestedIfDepth)
                {
                    ErrorList.AddLast(new Error(ctx.Start.Line, "İç içe yuvalanmış \"if\""));
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

        private void Walk(IParseTree t, int level)
        {
            ParserRuleContext ctx = t as ParserRuleContext;
            if (ctx == null)
            {
                return;
            }            
            if (ctx.RuleIndex == CPP14Parser.RULE_statement
                || ctx.RuleIndex == CPP14Parser.RULE_labeledstatement
                || ctx.RuleIndex == CPP14Parser.RULE_compoundstatement
                || ctx.RuleIndex == CPP14Parser.RULE_statementseq
                || ctx.RuleIndex == CPP14Parser.RULE_selectionstatement
                || ctx.RuleIndex == CPP14Parser.RULE_iterationstatement
                || ctx.RuleIndex == CPP14Parser.RULE_forinitstatement
                || ctx.RuleIndex == CPP14Parser.RULE_jumpstatement
                || ctx.RuleIndex == CPP14Parser.RULE_declarationstatement
                || ctx.RuleIndex == CPP14Parser.RULE_blockdeclaration)
            {
                if (ctx.RuleIndex == CPP14Parser.RULE_selectionstatement)
                {
                    level++;
                    depth = Math.Max(depth, level);
                }
                for (int i = 0; i < ctx.ChildCount; i++)
                {
                    Walk(ctx.children[i], level);
                }
            }            
        }
    }
}
