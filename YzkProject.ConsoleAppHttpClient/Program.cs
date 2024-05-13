
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

string JsonStr = await File.ReadAllTextAsync("data.json");
var model = JsonConvert.DeserializeObject<MainDto>(JsonStr);
//Console.WriteLine(JsonStr);

foreach (var mmproverb in model.Tbl_MMProverbs)
{
    Console.WriteLine(mmproverb.ProverbName);
}

Console.ReadLine();


public class MainDto
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
