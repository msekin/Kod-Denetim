using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAnalysis.Checks;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace CodeAnalysis
{
    public class ProcessManager
    {
        private List<ITester> Testers = new List<ITester>();

        public string Path { get; set; }

        public bool IsException { get; private set; }

        public Exception ParserException { get; private set; }

        private LinkedList<Error> tempError = new LinkedList<Error>();

        public ProcessManager()
        {
            PrepTesters();
            IsException = false;
        }

        private void PrepTesters()
        {
            //Testers = new List<ITester>();
            Testers.Clear();
            Testers.Add(new GotoTester());
            Testers.Add(new ParamCountTester());
            Testers.Add(new NestedIfTester());
            Testers.Add(new LongFunctionTester());
            //Testers.Add(new NoCommentTester());
            Testers.Add(new AssignmentInIfTester());
            Testers.Add(new EmptyCatchTester());
        }

        public bool AddTester(ITester tester)
        {
            Type t = tester.GetType();
            foreach (ITester item in Testers)
            {
                Type u = item.GetType();
                if(t == u)
                    return false;
            }
            Testers.Add(tester);
            return true;
        }

        public void RemoveTester(ITester tester)
        {
            for (int i = 0; i < Testers.Count; i++)
			{
			    if(Testers[i] == tester)
                {
                    Testers.RemoveAt(i);
                    break;
                }
			}
        }

        public void RemoveTesterAt(int i)
        {
            if (i < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (i > Testers.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            Testers.RemoveAt(i);
        }

        public void ClearTesters()
        {
            Testers.Clear();
        }

        public void ClearErrors()
        {
            foreach (var item in Testers)
	        {
		        item.ClearErrors();
	        }
        }

        public LinkedList<Error> GetErrors()
        {
            LinkedList<Error> errors = new LinkedList<Error>();
            foreach (var item in Testers)
            {
                LinkedList<Error> tmp = item.GetErrors();
                if (tmp != null)
                {
                    //errors.Concat(tmp);
                    foreach (var item2 in tmp)
                        errors.AddLast(new Error(item2.LineNumber, item2.Message));
                }
            }
            //errors.Concat(tempError);
            foreach (var item in tempError)
                errors.AddLast(new Error(item.LineNumber, item.Message));
            return errors;
        }

        public void Parse()
        {
            IsException = false;
            ParserException = null;
            tempError.Clear();
            ClearErrors();

            using (FileStream filestream = new FileStream(Path, FileMode.Open))
            {
                AntlrInputStream inputstream = new AntlrInputStream(filestream);
                CPP14Lexer lexer = new CPP14Lexer(inputstream);
                CommonTokenStream tokens = new CommonTokenStream(lexer);
                CPP14Parser parser = new CPP14Parser(tokens);
                CPP14Parser.TranslationunitContext translationunit = parser.translationunit();

                if (translationunit.exception != null)
                {
                    IsException = true;
                    ParserException = translationunit.exception;
                    return;
                }

                Walk(translationunit);

                ProgramStruct prog = new ProgramStruct();
                prog.Walk(translationunit);

                tempError = Checks.Metrics.LCOMMeasure.Test(prog);
                var a = Checks.Metrics.CBOMeasure.Test(prog);
                foreach (var item in a)
                {
                    tempError.AddLast(item);
                }
            }
        }

        public void Parse(string filename)
        {
            this.Path = filename;
            Parse();
        }

        private void Walk(IParseTree t)
        {
            if (t is IErrorNode)
            {
                return;
            }
            Testers.ForEach(tester => tester.Test(t));
            for (int i = 0; i < t.ChildCount; i++)
            {
                Walk(t.GetChild(i));
            }
        }
    }
}
