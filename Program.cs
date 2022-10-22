using Lesson_DotNet_Dapper_Advanced.Repositories;
using System.Data.SqlClient;

var connectionString = "Server=localhost,1433;Database=pub-dev;User Id=sa;Password=Pass@word";

var connection = new SqlConnection(connectionString);

var userRepository = new UserReporitory(connection);

var orderRepository = new OrderReporitory(connection);

var vehicleRepository = new VehicleRepository(connection);

//Console.WriteLine("*** Get all users! ***");

//Console.WriteLine(string.Join('\n', await userRepository.GetAsync()));

//Console.WriteLine("*** Get single user!");

//Console.WriteLine(string.Join('\n', await userRepository.GetByIdAsync(1)));

//Console.WriteLine("*** Get all users with status (1-1)! ***");

//Console.WriteLine(string.Join('\n', await userRepository.GetWithStatusAsync()));

//Console.WriteLine("*** Get user with status! ***");

//Console.WriteLine(string.Join('\n', await userRepository.GetByIdWithStatusAsync(1)));

//Console.WriteLine("*** Get all orders (1-N)! ***");

//Console.WriteLine(string.Join(",\n", await orderRepository.GetAsync()));

Console.WriteLine("*** Get all vehicles (1-1-1)! ***");

Console.WriteLine(string.Join(",\n", await vehicleRepository.GetAsync()));
