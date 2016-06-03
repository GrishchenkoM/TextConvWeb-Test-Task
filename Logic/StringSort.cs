using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces;

namespace Logic
{
    /// <summary>
    /// Simple class of text sorting
    /// </summary>
    public class StringSort : ISort
    {
        private StringSort()
        {
            SortNames = new[]
            {
                "Ascending", "Descending", "None"
            };
        }

        public static StringSort GetInstance()
        {
            if (_instance == null)
                lock (_object)
                    if (_instance == null)
                        _instance = new StringSort();

            return _instance;
        }

        public void Sort(ref IEnumerable<string> array, string sortName = null)
        {
            IEnumerable<string> tempArray = null;

            if (sortName != null && !sortName.ToLower().Equals(SortNames[2].ToLower()))
            {
                CreateOrderedArray(array, sortName, ref tempArray);
            }
            else
                return;
            
            array = tempArray;
        }

        private void CreateOrderedArray(IEnumerable<string> array, string sortName, ref IEnumerable<string> tempArray)
        {
            try
            {
                if (sortName.ToLower().Equals(SortNames[0].ToLower()))
                    tempArray = array.OrderBy(x => x);

                if (sortName.ToLower().Equals(SortNames[1].ToLower()))
                    tempArray = array.OrderByDescending(x => x);
            }
            catch
            {
                // ignored
            }
        }

        public string[] SortNames { get; set; }
        
        private static StringSort _instance;
        private static readonly object _object = new object();
    }
}
