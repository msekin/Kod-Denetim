using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis
{
    public class ProgramStruct
    {
        public List<ClassStruct> classes = new List<ClassStruct>();

        public void Walk(IParseTree t)
        {
            if (t is IErrorNode)
            {
                return;
            }
            if (t is CPP14Parser.ClassspecifierContext)
            {
                ClassStruct c = new ClassStruct();
                c.WalkClass(t);
                classes.Add(c);
                return;
            }
            if (t is CPP14Parser.FunctiondefinitionContext)
            {
                FunctionStruct f = new FunctionStruct();
                f.WalkFunctionDefinition(t);
                ClassStruct c = GetClassWithName(f.className);
                if (c != null)
                {
                    f.fields.RemoveAll(s => !c.fields.Contains(s));
                    c.AddMethod(f);
                }

                return;
            }
            for (int i = 0; i < t.ChildCount; i++)
            {
                Walk(t.GetChild(i));
            }
        }

        public ClassStruct GetClassWithName(string classname)
        {
            return classes.FirstOrDefault(c => c.name == classname);
        }

        public void AddClass(ClassStruct c)
        {
            foreach (var item in classes)
            {
                if(item.name == c.name)
                {
                    return;
                }
            }
            classes.Add(c);
        }
    }
}
