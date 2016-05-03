using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis
{
    public class ClassStruct
    {
        public string name;
        public List<string> fields = new List<string>();
        public List<FunctionStruct> methods = new List<FunctionStruct>();

        public void WalkClass(IParseTree t)
        {
            if (t is CPP14Parser.ClassnameContext)
            {
                CPP14Parser.ClassnameContext t2 = t as CPP14Parser.ClassnameContext;
                name = t2.Identifier().Symbol.Text;
            }
            if (t is CPP14Parser.MemberdeclarationContext)
            {
                IParseTree t2 = t.GetChild(1);
                while (true)
                {
                    if (t2.ChildCount > 1)
                    {
                        break;
                    }
                    if (t2 is TerminalNodeImpl)
                    {
                        TerminalNodeImpl t3 = t2 as TerminalNodeImpl;
                        fields.Add(t3.Symbol.Text);
                        break;
                    }
                    t2 = t2.GetChild(0);
                }
            }
            for (int i = 0; i < t.ChildCount; i++)
            {
                WalkClass(t.GetChild(i));
            }
        }

        public FunctionStruct GetMethodWithName(string functionname)
        {
            return methods.FirstOrDefault(f => f.name == functionname);
        }

        public void AddMethod(FunctionStruct f)
        {
            if(f.className == null || f.className == "")
            {
                return;
            }
            if(f.className != name)
            {
                return;
            }
            if (f.name != null && f.name != "")
            {
                foreach (var item in methods)
                {
                    if (item.name == null || item.name == "")
                    {
                        continue;
                    }
                    if (item.name == f.name)
                    {
                        return;
                    }
                }
            }
            methods.Add(f);
        }

        public void AddField(string field)
        {
            foreach (var item in fields)
            {
                if(item == field)
                {
                    return;
                }
            }
            fields.Add(field);
        }
    }
}
