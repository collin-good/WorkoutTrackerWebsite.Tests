using WorkoutTrackerWebsite.Data;
using WorkoutTrackerWebsite.Models;
using WorkoutTrackerWebsite.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutTrackerWebsite.Tests;
[TestClass]
public class WorkoutControllerTest
{
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
        WorkoutController controller = new(_context);
        Workout workoutToAdd = new Workout(3, "test", date, 0, 0, 0);

        controller.Create(workoutToAdd);

        Workout? retrievedWorkout = controller.Get(3).Value;

        Assert.IsNotNull(retrievedWorkout);
        Assert.AreEqual(workoutToAdd, retrievedWorkout);
    }

    [TestMethod]
    public void UpdateTest()
    {
        WorkoutController controller = new(_context);
        Workout updatedWorkout = new Workout(0, "test", date, 0, 0, 0);

        controller.Update(0, updatedWorkout);

        Assert.AreEqual(updatedWorkout, controller.Get(0).Value);
    }

    [TestMethod]
    public void UpdateMismatchIdTest()
    {
        WorkoutController controller = new(_context);
        Workout testWorkout = _context._testDB[0];

        try
        {
            controller.Update(1, testWorkout);
        }
        catch
        {
            //no errors should be thrown
            Assert.Fail();
        }

        //database should not be changed
        Assert.AreEqual(testWorkout, controller.Get(0).Value);
    }

    [TestMethod]
    public void UpdateOutOfBoundsTest()
    {
        WorkoutController controller = new(_context);
        Workout updateWorkout = new Workout(5, "test", date, 0, 0, 0);

        try
        {
            controller.Update(0, updateWorkout);
        }
        catch
        {
            //no errors should be thrown
            Assert.Fail();
        }

        //no workout should be added to the database
        Assert.IsNull(controller.Get(5).Value);
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(2)]
    public void DeleteTest(int index)
    {
        WorkoutController controller = new(_context);

        controller.Delete(index);

        Assert.IsNull(controller.Get(index).Value);
    }
}