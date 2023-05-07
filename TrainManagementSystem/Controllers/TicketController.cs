using BankSystem7.Extensions;
using BankSystem7.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TrainManagementSystem.Extensions;
using TrainManagementSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrainManagementSystem.Controllers;

[Route("api/tickets")]
[ApiController]
public class TicketController : ControllerBase
{
    private readonly IRepository<Ticket> _ticketRepository;
    public TicketController(IRepository<Ticket> ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    [HttpGet]
    public string All()
    {
        return _ticketRepository.All.Serialize();
    }


    [HttpGet("{id}")]
    public string Get(Guid id)
    {
        return _ticketRepository.Get(x => x.Id == id).Serialize();
    }


    [HttpPost]
    public void Create(Ticket ticket)
    {
        _ticketRepository.Create(ticket);
    }


    [HttpPut("{id}")]
    public void Update(Guid id, Ticket ticket)
    {
        var updTicket = _ticketRepository.Get(x => x.Id == id);
        _ticketRepository.Update(ticket.SetValuesTo(ticket));
    }


    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        var ticket = _ticketRepository.Get(x => x.Id == id);
        _ticketRepository.Delete(ticket);
    }
}
