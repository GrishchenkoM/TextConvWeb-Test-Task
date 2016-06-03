using System.Collections.Generic;

namespace Logic.Interfaces
{
    public interface ISort
    {
        void Sort(ref IEnumerable<string> array, string sortName = null);
    }
}
