using Lesson_DotNet_Dapper.Entities;
using Lesson_DotNet_Dapper_Advanced.Repositories;
using System.Data.SqlClient;

var connectionString = "Server=localhost,1433;Database=pub-dev;User Id=sa;Password=Pass@word";

var connectionStringMars = "Server=localhost,1433;Database=pub-dev;User Id=sa;Password=Pass@word;MultipleActiveResultSets=True";

var connection = new SqlConnection(connectionString);

var connectionMars = new SqlConnection(connectionStringMars);

async Task UserAsync()
{
    var userRepository = new UserReporitory(connection);

    Console.WriteLine("*** Get all users! ***");

    Console.WriteLine(string.Join(",\n-----------------\n", await userRepository.GetAsync()));

    Console.WriteLine("*** Get single user! ***");

    Console.WriteLine(string.Join(",\n-----------------\n", await userRepository.GetByIdAsync(1)));

    Console.WriteLine("*** Get all users with status (1-1)! ***");

    Console.WriteLine(string.Join(",\n-----------------\n", await userRepository.GetWithStatusAsync()));

    Console.WriteLine("*** Get user with status! ***");

    Console.WriteLine(string.Join(",\n-----------------\n", await userRepository.GetByIdWithStatusAsync(1)));

    Console.WriteLine("*** Get all users using Multi! ***");

    Console.WriteLine(string.Join(",\n-----------------\n", await userRepository.GetAllUsingMultiAsync()));

    Console.WriteLine("*** Insert Users One by one ***");

    var newUser1 = new User()
    {
        Name = "Tiago Totti",
        Email = "tiago.totti@pubdev.com"
    };

    await userRepository.InsertAsync(newUser1);

    var newUser2 = new User()
    {
        Name = "Peterson Silva",
        Email = "peterson.silva@pubdev.com"
    };

    await userRepository.InsertAsync(newUser2);

    Console.WriteLine(string.Join(",\n-----------------\n", await userRepository.GetWithStatusAsync()));

    Console.WriteLine("*** Insert Users using foreach***");

    var users = new List<User>
    {
        new User()
        {
            Name = "Matheus Vieira",
            Email = "matheus.vieira@pubdev.com"
        },
        new User()
        {
            Name = "Euller Araujo",
            Email = "euller.araujo@pubdev.com"
        }
    };

    foreach (var user in users)
    {
        await userRepository.InsertAsync(user);
    }

    Console.WriteLine("*** Insert Users with Enumerable ***");

    await userRepository.InsertAsync(users);

    Console.WriteLine(string.Join(",\n-----------------\n", await userRepository.GetWithStatusAsync()));

    Console.WriteLine("*** Insert Users Pipelined ***");

    await userRepository.InsertPipelinedAsync(users);
}

async Task VehicleAsync()
{
    var vehicleRepository = new VehicleRepository(connection);

    Console.WriteLine("*** Get all vehicles (1-1-1)! ***");

    Console.WriteLine(string.Join(",\n-----------------\n", await vehicleRepository.GetAsync()));

    Console.WriteLine("*** Get vehicle (1-1-1)! ***");

    Console.WriteLine(string.Join(",\n-----------------\n", await vehicleRepository.GetByIdAsync(1)));
}

async Task OrderAsync()
{
    var orderRepository = new OrderReporitory(connection);

    Console.WriteLine("*** Get all orders (1-N)! ***");

    Console.WriteLine(string.Join(",\n-----------------\n", await orderRepository.GetAsync()));
}


await UserAsync();

await VehicleAsync();

await OrderAsync();

Console.ReadKey();