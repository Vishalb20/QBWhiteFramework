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
            new object[]{"Sync.qbw"},
            new object[]{"falcon"},
            new object[]{"Force"},
            new object[]{"Misty"},
        };

        public static IEnumerable<object[]> TestData
        {
            get { return _data; }
        }
    }
}
