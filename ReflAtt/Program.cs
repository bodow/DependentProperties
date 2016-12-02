using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PasDependency;

namespace ReflAtt
{
    class Program
    {
        static void Main(string[] args)
        {
            var c1 = new Class1();
            c1.MyIntProperty = 12;
            System.Diagnostics.Debug.WriteLine("----------All---------------");
            Notifier.Notify(c1);
            System.Diagnostics.Debug.WriteLine("");

            System.Diagnostics.Debug.WriteLine("----------GroupAlpha---------------");
            Notifier.Notify(c1, "GroupAlpha");
            System.Diagnostics.Debug.WriteLine("");

            System.Diagnostics.Debug.WriteLine("----------GroupBeta---------------");
            Notifier.Notify(c1, "GroupBeta");
            System.Diagnostics.Debug.WriteLine("");

            System.Diagnostics.Debug.WriteLine("----------GroupGamma---------------");
            Notifier.Notify(c1, "GroupGamma");
            System.Diagnostics.Debug.WriteLine("");

            Console.ReadLine();
        }
    }
}
