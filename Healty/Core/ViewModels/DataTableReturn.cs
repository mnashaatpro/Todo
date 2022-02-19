using Healty.Core.Models;

using System.Collections.Generic;

namespace Healty.Controllers
{
    internal class DataTableReturn//<T> where T : class
    {
        private string _draw;
        private int _recordsTotal1;
        private int _recordsTotal2;
        private IEnumerable<Todo> _dataList;

        public DataTableReturn(string draw, int recordsTotal1, int recordsTotal2, IEnumerable<Todo> dataList)
        {
            _draw = draw;
            _recordsTotal1 = recordsTotal1;
            _recordsTotal2 = recordsTotal2;
            _dataList = dataList;
        }
    }
}