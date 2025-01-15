using WorkoutTrackerWebsite.Data;
using WorkoutTrackerWebsite.Models;
using WorkoutTrackerWebsite.Services;

namespace WorkoutTrackerWebsite.Tests;

[TestClass]
public class WorkoutServiceTests
{
    WorkoutTestContext context = new();

    [TestMethod]
    public void ServiceGetAllTest()
    {
        WorkoutService service = new WorkoutService(context);

        List<Workout> workouts = service.GetSortedWorkouts();

        Assert.IsNotNull(workouts);
        Assert.AreEqual<List<Workout>>(context._testDB, workouts);
    }

    [TestMethod]
    public void ServiceGetTest()
    {
        WorkoutService service = new(context);

        Workout? workout = service.Get(0);

        Assert.IsNotNull(workout);
        Assert.AreEqual<Workout>(context._testDB, workout);
    }
    
    [TestMethod]
    public void ServiceAddTest()
    {

    }

    [TestMethod]
    public void ServiceUpdateTest()
    {

    }

    [TestMethod]
    public void ServiceDeleteTest()
    {
        
    }
}