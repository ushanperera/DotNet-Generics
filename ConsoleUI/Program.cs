using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using ConsoleUI;


class Program
{
    static void Main(string[] args)
    {
        //List<int> ages = new List<int>();

        int[] array = new int[10]; // Array - fixed size
        array[0] = 100;

        //list are referency type so it takes more memory in heap
        List<int> list = new List<int>(); // List - dynamic size
        list.Add(100);

        //as this is a referency type it also takes more memory in heap and conversion (boxing and unboxing) is needed
        ArrayList arrayList = new ArrayList();// ArrayList - dynamic size
        arrayList.Add(100);

        Person person = new Person { FirstName = "kamal", LastName = "perera", IsAlive = true };


        //check the type and value of the variable
        TypeChecker(list);
        TypeChecker(arrayList);
        TypeChecker(person);

        //Checked using Generics - Methods
        Console.WriteLine(areEqual(1, 2));
        Console.WriteLine(areEqual(2.33, 2.33));
        Console.WriteLine(areEqual("Kamal", "Nimal"));


        //Checked using Generics - Classes ( this is easy to read)
        Console.WriteLine(sampleClass<int>.areEqual(1, 2));
        Console.WriteLine(sampleClass<double>.areEqual(2.1, 2.1));
        Console.WriteLine(sampleClass<string>.areEqual("Kamal", "Nimal"));


        Console.ReadLine();

        DemonstrateTextFileStorage();

        Console.WriteLine();
        Console.Write("Press enter...");
        Console.ReadLine();
    }


    public static bool areEqual<T>(T value1, T value2)
    {
        return value1.Equals(value2);
    }

    private static void TypeChecker<T>(T value)
    {
        Console.WriteLine("Type: " + typeof(T));
        Console.WriteLine("Value: " + value);
    }

    private static void DemonstrateTextFileStorage()
    {
        List<Person> people = new List<Person>();
        List<LogEntry> logs = new List<LogEntry>();

        string peopleFile = @"C:\Temp\people.csv";
        string logFile = @"C:\Temp\logs.csv";

        PopulateLists(people, logs);

        /* New way of doing things - generics */
        GenericTextFileProcessor.SaveToTextFile<Person>(people, peopleFile);
        GenericTextFileProcessor.SaveToTextFile<LogEntry>(logs, logFile);

        var newPeople = GenericTextFileProcessor.LoadFromTextFile<Person>(peopleFile);

        foreach (var p in newPeople)
        {
            Console.WriteLine($"{p.FirstName} {p.LastName} (IsAlive = {p.IsAlive})");
        }

        var newLogs = GenericTextFileProcessor.LoadFromTextFile<LogEntry>(logFile);

        foreach (var log in newLogs)
        {
            Console.WriteLine($"{log.ErrorCode}: {log.Message} at {log.TimeOfEvent.ToShortTimeString()}");
        }

        /* Old way of doing things - non-generics */

        //OriginalTextFileProcessor.SaveLogs(logs, logFile);

        //var newLogs = OriginalTextFileProcessor.LoadLogs(logFile);

        //foreach (var log in newLogs)
        //{
        //    Console.WriteLine($"{ log.ErrorCode }: { log.Message } at { log.TimeOfEvent.ToShortTimeString() }");
        //}

        //OriginalTextFileProcessor.SavePeople(people, peopleFile);

        //var newPeople = OriginalTextFileProcessor.LoadPeople(peopleFile);

        //foreach (var p in newPeople)
        //{
        //    Console.WriteLine($"{ p.FirstName } { p.LastName } (IsAlive = { p.IsAlive })");
        //}
    }

    private static void PopulateLists(List<Person> people, List<LogEntry> logs)
    {
        people.Add(new Person { FirstName = "aaa", LastName = "111" });
        people.Add(new Person { FirstName = "bbb", LastName = "222", IsAlive = false });
        people.Add(new Person { FirstName = "ccc", LastName = "333" });

        logs.Add(new LogEntry { Message = "I blew up", ErrorCode = 9999 });
        logs.Add(new LogEntry { Message = "I'm too awesome", ErrorCode = 1337 });
        logs.Add(new LogEntry { Message = "I was tired", ErrorCode = 2222 });
    }
}


public static class sampleClass<T>
{    public static bool areEqual(T value1, T value2)
    {
        return value1.Equals(value2);
    }
}
