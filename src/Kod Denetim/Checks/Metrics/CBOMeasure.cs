using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeAnalysis.Checks.Metrics
{
    public class CBOMeasure
    {
        public static LinkedList<Error> Test(ProgramStruct prog)
        {
            // CBO = number of classes to which a class is coupled
            //
            // http://www.aivosto.com/project/help/pm-oo-ck.html

            LinkedList<Error> errors = new LinkedList<Error>();

            // class - function - var
            // variablelar hangi classları içeriyor

            foreach (var item1 in prog.classes)
            {
                int i = 0;
                foreach (var item2 in item1.methods)
                {
                    foreach (var item3 in item2.variables.GroupBy(test => test.type).Select(grp => grp.First()))
                    {
                        if (prog.classes.Any(cls => cls.name == item3.type))
                        {
                            i++;
                        }
                    }
                }
                if (i > 0 && i >= Properties.Settings.Default.CBO_Threshold)
                {
                    errors.AddLast(new Error(-1, string.Format("{0} sınıfına ait CBO değeri {1:#}", item1.name, i)));
                }
            }

            return errors;
        }
    }
}
