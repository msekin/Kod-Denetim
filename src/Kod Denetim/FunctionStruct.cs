﻿using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis
{
    public class FunctionStruct
    {
        public string name;
        public string className;
        public List<string> fields = new List<string>();

        public void WalkFunctionDefinition(IParseTree t)
        {
            if (t is CPP14Parser.ClassnameContext)
            {
                CPP14Parser.ClassnameContext t2 = t as CPP14Parser.ClassnameContext;
                className = t2.Identifier().Symbol.Text;
            }

            if (t is CPP14Parser.FunctionbodyContext)
            {
                WalkFunctionBody(t);
                return;
            }

            for (int i = 0; i < t.ChildCount; i++)
            {
                WalkFunctionDefinition(t.GetChild(i));
            }
        }

        public void WalkFunctionBody(IParseTree t)
        {
            if(t is CPP14Parser.UnqualifiedidContext)
            {
                CPP14Parser.UnqualifiedidContext t2 = t as CPP14Parser.UnqualifiedidContext;
                fields.Add(t2.Identifier().Symbol.Text);
            }

            for (int i = 0; i < t.ChildCount; i++)
            {
                WalkFunctionBody(t.GetChild(i));
            }
        }

        public void AddField(string field)
        {
            foreach (var item in fields)
            {
                if (item == field)
                {
                    return;
                }
            }
            fields.Add(field);
        }
    }
}