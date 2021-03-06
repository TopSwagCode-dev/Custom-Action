using System; // Namespace for Console output
using System.Configuration; // Namespace for ConfigurationManager
using System.Threading.Tasks; // Namespace for Task
using Azure.Storage.Queues; // Namespace for Queue storage types
using Azure.Storage.Queues.Models; // Namespace for PeekedMessage
using System.Collections;

Console.WriteLine("Hello, World!");
Console.WriteLine("Args are:");
Console.WriteLine(args);
foreach (var s in args)
{
    Console.WriteLine(s);
}

// https://github.com/dotnet/samples/blob/main/github-actions/DotNet.GitHubAction/action.yml
// https://docs.microsoft.com/en-us/dotnet/devops/create-dotnet-github-action#explore-the-app
// GITHUB_REPOSITORY = TopSwagCode-dev/Custom-Action-Usage
// GITHUB_SHA = da05a6db388b5d67e2a181b0582ae6b639c93bb9 <-- Commit sha. So we know what is currently released
// GITHUB_REF_NAME = master

Console.WriteLine("GetEnvironmentVariables: ");
foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
    Console.WriteLine("  {0} = {1}", de.Key, de.Value);

return;
// Team, Repository, Time, Version, Enviroment (dev,test,preprod,prod) <-- Fail on invalid values.

var queueName = "custom-action-queue";
var connectionString = "";

// Instantiate a QueueClient which will be used to manipulate the queue
QueueClient queueClient = new QueueClient(connectionString, queueName);

// Create the queue if it doesn't already exist
await queueClient.CreateIfNotExistsAsync();

if (await queueClient.ExistsAsync())
{
    Console.WriteLine($"Queue '{queueClient.Name}' created");
}
else
{
    Console.WriteLine($"Queue '{queueClient.Name}' exists");
}

// Async enqueue the message
await queueClient.SendMessageAsync("Hello, World");
Console.WriteLine($"Message added");


//-------------------------------------------------
// Create the queue service client
//-------------------------------------------------
static void CreateQueueClient(string queueName, string connectionString)
{
    // Instantiate a QueueClient which will be used to create and manipulate the queue
    QueueClient queueClient = new QueueClient(connectionString, queueName);
}

//-------------------------------------------------
// Create a message queue
//-------------------------------------------------
static bool CreateQueue(string queueName, string connectionString)
{
    try
    {
        // Instantiate a QueueClient which will be used to create and manipulate the queue
        QueueClient queueClient = new QueueClient(connectionString, queueName);

        // Create the queue
        queueClient.CreateIfNotExists();

        if (queueClient.Exists())
        {
            Console.WriteLine($"Queue created: '{queueClient.Name}'");
            return true;
        }
        else
        {
            Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");
            return false;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex.Message}\n\n");
        Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");
        return false;
    }
}

//-------------------------------------------------
// Insert a message into a queue
//-------------------------------------------------
static void InsertMessage(string queueName, string message, string connectionString)
{
    // Instantiate a QueueClient which will be used to create and manipulate the queue
    QueueClient queueClient = new QueueClient(connectionString, queueName);

    // Create the queue if it doesn't already exist
    queueClient.CreateIfNotExists();

    if (queueClient.Exists())
    {
        // Send a message to the queue
        queueClient.SendMessage(message);
    }

    Console.WriteLine($"Inserted: {message}");
}

// https://docs.microsoft.com/en-us/azure/storage/queues/storage-dotnet-how-to-use-queues?tabs=dotnet