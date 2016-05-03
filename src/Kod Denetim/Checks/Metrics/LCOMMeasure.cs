using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis.Checks.Metrics
{
    public class LCOMMeasure
    {
        public static LinkedList<Error> Test(ProgramStruct prog)
        { 
            // LCOM2
            //
            // m	number of procedures (methods) in class
            // a	number of variables (attributes) in class
            // mA	number of methods that access a variable (attribute)
            // sum(mA)	sum of mA over attributes of a class
            //
            // LCOM2 = 1 - sum(mA)/(m*a)
            //
            // http://www.aivosto.com/project/help/pm-oo-cohesion.html#LCOM4

            LinkedList<Error> errors = new LinkedList<Error>();

            foreach (var item in prog.classes)
	        {
                int m = item.methods.Count;
                int a = item.fields.Count;
                int mA = item.methods.Count(method => method.fields.Count > 0);
                //int summA = SumofUsedFields(item);
                int summA = item.methods.Sum(method => method.fields.Count);

                float lcom2 = 1 - (float)summA / (m * a);
                errors.AddLast(new Error(-1, string.Format("{0} sınıfına ait LCOM değeri {1:0.##}", item.name, lcom2)));
	        }

            return errors;
        }

        private static int SumofUsedFields(ClassStruct c)
        {
            int i = 0;
            foreach (var item in c.fields)
	        {
		        if(c.methods.Any(m => m.fields.Contains(item)))
                {
                    i++;
                }
	        }
            return i;
        }
    }
}
