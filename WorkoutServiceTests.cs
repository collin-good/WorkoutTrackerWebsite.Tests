#define TESTING
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
        CollectionAssert.AreEqual(_context._testDB, workouts);
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

    [TestMethod]
    public void ServiceGetOutOfBounds()
    {
        WorkoutService service = new(_context);

        Workout? workout = service.Get(100);

        Assert.IsNull(workout);
    }

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

    [TestMethod]
    public void ServiceUpdateOutOfBoundsTest()
    {
        WorkoutService service = new(_context);
        Workout workoutToUpdate = new Workout(100, "something", date, 0, 0, 0);

        try
        {
            service.Update(workoutToUpdate);
        }
        catch
        {
            //nothing should happen if the workout to update does not exist
            Assert.Fail();
        }
    }

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

    [TestMethod]
    public void SortWorkoutByTypeTest()
    {
        WorkoutService service = new(_context);

        //updating the sorting method and getting the list
        service.UpdateSortMethod(WorkoutSortMethod.workoutType);
        List<Workout> workouts = service.GetSortedWorkouts();

        //making sure that the workouts are now sorted by workout type
        Assert.AreEqual(_context._testDB[1], workouts[0]);
        Assert.AreEqual(_context._testDB[0], workouts[1]);
        Assert.AreEqual(_context._testDB[2], workouts[2]);
    }
}