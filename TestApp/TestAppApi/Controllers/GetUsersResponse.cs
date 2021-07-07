using System.Collections.Generic;

namespace TestAppApi.Controllers
{
    public class GetUsersResponse
    {
        public IEnumerable<GetUsersResponseItem> Users { get; set; }
    }
}