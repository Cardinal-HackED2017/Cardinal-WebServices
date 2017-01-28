using System.Collections.Generic;

namespace cardinal_webservices.Data 
{
    public interface ICardinalDataService 
    {
        IEnumerable<Test> GetTestItems();
    }
}