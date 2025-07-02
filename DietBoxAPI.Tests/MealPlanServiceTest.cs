using DietBoxAPI.DB;
using DietBoxAPI.DB.Models;
using DietBoxAPI.DTO;
using DietBoxAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

public class MealPlanServiceTests
{
    private readonly DietBoxDbContext _context;
    private readonly MealPlanService _service;

    public MealPlanServiceTests()
    {
        // Use InMemory database for testing
        var options = new DbContextOptionsBuilder<DietBoxDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique DB per test class
            .Options;

        _context = new DietBoxDbContext(options);

        _service = new MealPlanService(_context);
    }

    [Fact]
    public async Task CreateAsync_AddsNewMealPlan()
    {
        var patient = new Patient { Name = "Test Patient" };
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        var dto = new MealPlanCreateDto
        {
            Name = "Test Plan", 
            Date = DateOnly.FromDateTime(DateTime.Today),
            PatientId = patient.Id,
            FoodIds = new System.Collections.Generic.List<int>()
        };

        var created = await _service.CreateAsync(dto);

        Assert.NotNull(created);
        Assert.Equal(dto.Name, created.Name);
        Assert.Equal(dto.PatientId, created.PatientId);
    }
}
