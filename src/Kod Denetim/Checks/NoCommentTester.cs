using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis.Checks
{
    class NoCommentTester
    {
        private LinkedList<Error> ErrorList;
        private bool hasComments = false;

        public NoCommentTester()
        {
            ErrorList = new LinkedList<Error>();
        }

        public void Test(CPP14Lexer lex)
        {
            hasComments = false;
            ErrorList.Clear();
            do
            {
                IToken t = lex.NextToken();
                if(t.Type == CPP14Lexer.LineComment || t.Type == CPP14Lexer.BlockComment)
                {
                    hasComments = true;
                    break;
                }
            } while (!lex.HitEOF);
            if(!hasComments)
            {
                ErrorList.AddLast(new Error(-1, "Yorum yok."));
            }
            //lex.Reset();
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
