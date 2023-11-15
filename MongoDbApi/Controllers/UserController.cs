using Domain;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace MongoDbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserController(IMongoService mongoService)
        {
            _userCollection = mongoService.database.GetCollection<User>(nameof(User));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            await _userCollection.InsertOneAsync(user);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userCollection.Find(_ => true).ToListAsync();
            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> Put(User user)
        {
            var users = await _userCollection.ReplaceOneAsync(x => x.Id == user.Id,user);
            return Ok(users);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(User user)
        {
            var users = await _userCollection.DeleteOneAsync(x => x.Id == user.Id);
            return Ok(users);
        }
    }
}
