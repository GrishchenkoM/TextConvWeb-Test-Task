using System.Collections.Generic;

namespace TextConvWeb.Models
{
    public class ViewModel
    {
        public string Text { get; set; }
        public IEnumerable<string> Sort { get; set; }
        public string SelectedSort { get; set; }
    }
}