using System.Web.Mvc;
using Logic;
using TextConvWeb.Models;

namespace TextConvWeb.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Creates ConversationService instance with 2 parameters:
        /// - list of format conversion objects
        /// - strategy of text parsing and sorting
        /// </summary>
        public HomeController()
        {
            ConversionService.GetInstance(
                new FormatConversion().FormatConversionList,
                new TextTransformation(StringSort.GetInstance()));
        }

        public ActionResult Index()
        {
            var model = new ViewModel()
            {
                Sort =  StringSort.GetInstance().SortNames
            };

            return View(model);
        }
    }
}
