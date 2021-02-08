using System;
using System.IO;
using System.Linq;

namespace SleepData
{
    class Program
    {
        static void Main(string[] args)
        {
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            if (resp == "1")
            {
                // create data file

                 // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());

                // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));

                // random number generator
                Random rnd = new Random();

                // create file
                StreamWriter sw = new StreamWriter("data.txt");

                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                // TODO: parse data file

                // check if file exists
                if (File.Exists("data.txt")) {

                    StreamReader sr = new StreamReader("data.txt");

                    while (!sr.EndOfStream) {

                        // Convert string array to "DateTime" data type
                        string line = sr.ReadLine();
                        String[] array = line.Split(',');
                        DateTime parsedDate = DateTime.Parse(array[0]);

                        // Display week headings
                        Console.WriteLine($"Week of {parsedDate:MMM, dd, yyyy}");
                        Console.WriteLine($"{"Mo",3}{"Tu",3}{"We",3}{"Th",3}{"Fr",3}{"Sa",3}{"Su",3}{"Tot",4}{"Avg",4}");
                        Console.WriteLine($"{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"---",4}{"---",4}");

                        // using Linq - convert array to int
                        String[] array1 = array[1].Split('|');
                        int[] arr = array1.Select(int.Parse).ToArray();

                        // week average and total
                        int arrTotal = arr.Sum();
                        double average = arrTotal / 7.0;
                        double arrAverage = Math.Round(average, 1);

                        // display hours of sleep, average, and total
                        Console.WriteLine($"{array1[0],3}{array1[1],3}{array1[2],3}{array1[3],3}{array1[4],3}{array1[5],3}{array1[6],3}{arrTotal,4}{arrAverage,4}");
                    }
                    sr.Close();
                } else {
                    Console.WriteLine("This file does not exist");
                }

            }
        }
    }
}
