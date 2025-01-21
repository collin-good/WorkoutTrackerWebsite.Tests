using WorkoutTrackerWebsite.Data;
using WorkoutTrackerWebsite.Models;
using WorkoutTrackerWebsite.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutTrackerWebsite.Tests;
[TestClass]
public class WorkoutControllerTest
{
    TestControllerBase _controllerBase = new();
    WorkoutTestContext _context = new();
    DateOnly date = new DateOnly(2025, 1, 16);

    [TestMethod]
    public void GetAllTest()
    {
        WorkoutController controller = new(_context);

        List<Workout>? workouts = controller.GetAll().Value;

        Assert.IsNotNull(workouts);
        Assert.AreEqual(_context._testDB, workouts);
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(2)]
    public void GetTest(int index) 
    { 
        WorkoutController controller = new(_context);

        Workout? workout = controller.Get(index).Value;

        Assert.IsNotNull(workout);
        Assert.AreEqual(_context._testDB[index], workout);
    }

    [TestMethod]
    public void GetOutOfBoundsTest()
    {
        WorkoutController controller = new(_context);

        Assert.IsNull(controller.Get(100).Value);
    }

    [TestMethod]
    public void AddTest()
    {

    }

    [TestMethod]
    public void UpdateTest()
    {

    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(2)]
    public void DeleteTest()
    {

    }
}

//not sure if this is needed but I'm going to leave it for now
public class TestControllerBase : ControllerBase
{
    public IActionResult GetNotFound() => NotFound();
    public ActionResult<Workout> GetWorkoutNotFound() => NotFound();

    public IActionResult GetNoContent() => NoContent();

    public IActionResult GetBadRequest() => BadRequest();
}