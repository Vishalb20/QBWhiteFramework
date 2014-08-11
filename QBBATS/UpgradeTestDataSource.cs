using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATS
{
    public static class UpgradeTestDataSource
    {
        private static List<object[]> _data = new List<object[]>
        {
            new object[]{"Misty.qbw"},
            new object[]{"Tax"}
        };

        public static IEnumerable<object[]> TestData
        {
            get { return _data; }
        }
    }
}
