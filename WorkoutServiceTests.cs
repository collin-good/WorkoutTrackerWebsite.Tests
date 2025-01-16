using WorkoutTrackerWebsite.Data;
using WorkoutTrackerWebsite.Models;
using WorkoutTrackerWebsite.Services;

namespace WorkoutTrackerWebsite.Tests;

[TestClass]
public class WorkoutServiceTests
{
    WorkoutTestContext _context = new();
    DateOnly date = new DateOnly(2025, 1, 16);

    [TestMethod]
    public void ServiceGetAllTest()
    {
        WorkoutService service = new WorkoutService(_context);

        List<Workout> workouts = service.GetSortedWorkouts();

        Assert.IsNotNull(workouts);
        Assert.AreEqual(_context._testDB, workouts);
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(2)]
    public void ServiceGetTest(int getIndex)
    {
        WorkoutService service = new(_context);

        Workout? workout = service.Get(getIndex);

        Assert.IsNotNull(workout);
        Assert.AreEqual(_context._testDB[getIndex], workout);
    }

    //TODO: add a get test that calls out of bounds values

    [TestMethod]
    public void ServiceAddTest()
    {
        WorkoutService service = new(_context);
        Workout workoutToAdd = new Workout(3, "Push Ups", date, 0, 3, 12);

        service.Add(workoutToAdd);

        Workout? addedWorkout = service.Get(3);

        Assert.IsNotNull(addedWorkout);
        Assert.AreEqual(workoutToAdd, addedWorkout);
    }

    [TestMethod]
    public void ServiceUpdateTest()
    {
        WorkoutService service = new(_context);
        Workout workoutToUpdate = new Workout(1, "Bicep Curls", date, 25, 3, 12);

        service.Update(workoutToUpdate);

        Workout? updatedWorkout = service.Get(1);

        Assert.IsNotNull(updatedWorkout);
        Assert.AreEqual(workoutToUpdate, updatedWorkout);
    }

    //TODO: Add an update test that tries to update an out of bounds index

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(2)]
    public void ServiceDeleteTest(int workoutToDelete)
    {
        WorkoutService service = new(_context);

        service.Detete(workoutToDelete);

        Assert.IsNull(service.Get(workoutToDelete));
    }
}