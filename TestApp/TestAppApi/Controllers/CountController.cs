using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace TestAppApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatDbContext _dbContext;

        public ChatController(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/messages")]
        public ActionResult<ChatMessage[]> Get()
        {
            return ChatRepository.Instance.Messages.ToArray();
        }

        [HttpGet("/counter")]
        public async Task<ActionResult<int>> Get(int iterations, CancellationToken token=default)
        {
            var task = await WorkerTallyCounter.TallyCounterAsync(iterations, () => true);

            return task;
        }

        [HttpGet("/users")]
        public ActionResult<GetUsersResponse> Get([FromBody] GetUsersRequest request)
        {
            var response = new GetUsersResponse
            {
                Users = _dbContext.Users
                    .GroupBy(user => user.UserName)
                    .Select(grouping => new GetUsersResponseItem { Name = grouping.Key, Count = grouping.Count() })
                    .ToArray()
                    .Where(user => user.Name.Contains(request.MatchingName))
                    .OrderBy(user => user.Count)
            };

            return response;
        }

        [HttpPost("/users")]
        public ActionResult<int> Create(string userName)
        {
            var newUser = new ChatUser
            {
                UserName = userName,
            };

            _dbContext.Users.Add(newUser);

            _dbContext.SaveChanges();

            return newUser.Id;
        }
    }
}
