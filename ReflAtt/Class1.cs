using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasDependency;

namespace ReflAtt
{
    public class Class1
    {
        [DependentProperty("GroupAlpha")]
        [DependentProperty("GroupBeta")]
        public int MyIntProperty { get; set; }

        [DependentProperty]
        public string MyStringProperty { get; set; }

        [DependentProperty("GroupBeta")]
        public DateTime MyDateTimeProperty { get; set; }

        public List<string> MyInnocentProperty { get; set; }

        [DependentProperty("GroupBeta")]
        public List<int> MyListProperty { get; set; }

        [DependentProperty("GroupAlpha")]
        public List<int> MyAlphaListProperty { get; set; }


        public void OnPropertyChanged(string name)
        {
            // simulation
            System.Diagnostics.Debug.WriteLine("OnPropertyChanged:" + name);
        }
    }
}
