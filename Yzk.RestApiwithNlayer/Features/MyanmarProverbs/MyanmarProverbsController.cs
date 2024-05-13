using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Yzk.RestApiwithNlayer.Features.MyanmarProverbs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyanmarProverbsController : ControllerBase
    {
         
        private async Task<MyanmarProverbs> GetDataAsync()
        {
            string JsonStr = await System.IO.File.ReadAllTextAsync("data.json");
            var model = JsonConvert.DeserializeObject<MyanmarProverbs>(JsonStr);
            return model;

        }

        [HttpGet("TitleId")]

        public async Task<IActionResult> Tbl_Mmproverbstitle()
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_MMProverbsTitle);
        }

        [HttpGet]

        public async Task<IActionResult> Tbl_Mmproverbs()
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_MMProverbs);
        }
    }


    public class MyanmarProverbs
    {
        public Tbl_Mmproverbstitle[] Tbl_MMProverbsTitle { get; set; }
        public Tbl_Mmproverbs[] Tbl_MMProverbs { get; set; }
    }

    public class Tbl_Mmproverbstitle
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
    }

    public class Tbl_Mmproverbs
    {
        public int TitleId { get; set; }
        public int ProverbId { get; set; }
        public string ProverbName { get; set; }
        public string ProverbDesp { get; set; }
    }

}
