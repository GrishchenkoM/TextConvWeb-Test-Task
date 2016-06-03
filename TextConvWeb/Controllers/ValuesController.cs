using System.Collections.Generic;
using System.Web.Http;
using Logic;
using TextConvWeb.Models;

namespace TextConvWeb.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpPost]
        public IEnumerable<string> ReturnXml([FromBody]ViewModel model)
        {
            return (IEnumerable<string>)ConversionService.GetConversionService
                .Convert(model.Text, "XML", model.SelectedSort);
        }

        [HttpPost]
        public IEnumerable<string> ReturnCsv([FromBody]ViewModel model)
        {
            return (IEnumerable<string>)ConversionService.GetConversionService
                .Convert(model.Text, "CSV", model.SelectedSort);
        }
    }
}
