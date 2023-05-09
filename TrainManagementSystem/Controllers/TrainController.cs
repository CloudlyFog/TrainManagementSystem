using BankSystem7.Extensions;
using BankSystem7.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TrainManagementSystem.Extensions;
using TrainManagementSystem.Models;
using BankSystem7.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TrainManagementSystem.Controllers;

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
    public IActionResult Create(Train train)
    {
        return HandleResults(_trainRepository.Create(train));
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Train train)
    {
        var updOrder = _trainRepository.Get(x => x.Id == id);
        return HandleResults(_trainRepository.Update(train.SetValuesTo(updOrder)));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var train = _trainRepository.Get(x => x.Id == id);
        return HandleResults(_trainRepository.Delete(train));
    }

    private IActionResult HandleResults(ExceptionModel result)
    {
        return result switch
        {
            ExceptionModel.Ok or ExceptionModel.Successfully => Ok(),
            ExceptionModel.Restricted => Forbid(),
            ExceptionModel.EntityIsNull or ExceptionModel.EntityNotExist => NotFound(),
            _ => BadRequest()
        };
    }
}
