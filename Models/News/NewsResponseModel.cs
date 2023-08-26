namespace Fochso.Models.News
{
    
public class NewsResponseModel : BaseResponseModel
{
    public NewsViewModel Data { get; set; }
}

public class NewsesResponseModel : BaseResponseModel
{
    public List<NewsViewModel> Data { get; set; }
}

}
