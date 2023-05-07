using BankSystem7.Extensions;
using BankSystem7.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mysqlx.Crud;
using TrainManagementSystem.Extensions;
using TrainManagementSystem.Models;

namespace TrainManagementSystem.Controllers
{
    [Route("api/trains")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly IRepository<Train> _trainRepository;
        public TrainController(IRepository<Train> trainRepository)
        {
            _trainRepository = trainRepository;
        }

        [HttpGet]
        public string All()
        {
            return _trainRepository.All.Serialize();
        }

        [HttpGet("{id}")]
        public string Get(Guid id)
        {
            return _trainRepository.Get(x => x.Id == id).Serialize();
        }

        [HttpPost]
        public void Create(Train train)
        {
            _trainRepository.Create(train);
        }

        [HttpPut]
        public void Update(Guid id, Train train)
        {
            var updOrder = _trainRepository.Get(x => x.Id == id);
            _trainRepository.Update(train.SetValuesTo(updOrder));
        }

        [HttpDelete]
        public void Delete(Guid id)
        {
            var train = _trainRepository.Get(x => x.Id == id);
            _trainRepository.Delete(train);
        }
    }
}
