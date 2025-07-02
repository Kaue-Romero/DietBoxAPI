using System.Net.Http;
using System.Net.Http.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using DietBoxAPI;
using Microsoft.EntityFrameworkCore;                  
using DietBoxAPI.DB;                                   
using DietBoxAPI.Services;                             
using DietBoxAPI.DTO;                                  
using DietBoxAPI.DB.Models;

public class PatientServicesTests
{
    private readonly IPatientService _service;
    private readonly DietBoxDbContext _context;

    public PatientServicesTests()
    {
        var options = new DbContextOptionsBuilder<DietBoxDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DietBoxDbContext(options);
        _service = new PatientService(_context);
    }

    [Fact]
    public async Task CreateAsync_AddsPatient()
    {
        var dto = new PatientCreateDto { Name = "Test Patient" };

        var result = await _service.CreateAsync(dto);

        Assert.NotNull(result);
        Assert.Equal("Test Patient", result.Name);

        var dbPatient = await _context.Patients.FindAsync(result.Id);
        Assert.NotNull(dbPatient);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsPatients()
    {
        _context.Patients.Add(new Patient { Name = "Alice" });
        _context.Patients.Add(new Patient { Name = "Bob" });
        await _context.SaveChangesAsync();

        var result = await _service.GetAllAsync();

        Assert.Equal(2, result.Count());
    }
}
