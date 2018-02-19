using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCase
{
    class Program
    {
        static void Main(string[] args)
        {
            bool allPassed = true;
            if ( !TestForZeroInputs(NewCalculator()) |
                 !TestMismatchedNegativeSymbols(NewCalculator()) |
                 TestForValidInputs() );

            MikesLib.Calculator c = NewCalculator();
            bool IsError;
            double result = c.Add("5.22089, 10.9289, 323.2311,-0.38089", out IsError);
            double result2 = c.Add("-5.5, -10.5, -4", out IsError);
            Console.WriteLine("Result = " + result + " " + result2);
            if (!allPassed)
            {
                Console.WriteLine("Some tests failed");
            }
        }

        static MikesLib.Calculator NewCalculator()
        {
            return new MikesLib.Calculator();
        }

        static string RequestInput()
        {
            Console.WriteLine("Enter numbers separated by commas then press enter.");
            string userInput = Console.ReadLine();
            return userInput;
        }

        static bool TestForZeroInputs(MikesLib.Calculator c, string input = "")
        {
            string ExceptionThrown = "No Exception";
            bool IsError = false;
            double result = 0;
            try
            {
                result = c.Add(input, out IsError);
            }
            catch (ApplicationException ex)
            {
                ExceptionThrown = "ApplicationException";
            }

            return PrintTestResult("1", "Test for zero inputs", ExceptionThrown, "ApplicationException", "true", IsError.ToString());
        }

        static bool TestForPositiveInput(MikesLib.Calculator c, int amount, bool ExpectedFail,string id)
        {
            Random rnd = new Random();
            string input = "";
            double total = 0;
            for(int i = 0; i < amount; i++)
            {
                int random = rnd.Next(0,1000000);
                total += random;
                input += random.ToString();
                if (i != amount - 1)
                    input += ",";
            }
            //Console.WriteLine(input);
            double results = c.Add(input, out bool IsError);
            return PrintTestResult(id, $"Testing for correction addition for {amount} numbers", results.ToString(), total.ToString(), ExpectedFail.ToString(), IsError.ToString()); 
        }


        static bool PrintTestResult(string Id, string Description, string RealResult, string ExpectedResult, string ExpectedError, string ActualError)
        {
            string passfail = "PASS";
            if (ExpectedError != ActualError)
                passfail = "FAIL";
            if (ExpectedResult != RealResult)
                passfail = "FAIL";

            Console.WriteLine($"{Id} {Description} {RealResult} {ExpectedResult} {ExpectedError} {ActualError} < {passfail} >");
            return passfail == "PASS";
        }

        static bool TestForValidInputs()
        {
            int id = 1;
            bool passed = true;
            for(int i = 2; i <= 10; i++)
            {
                if(!TestForPositiveInput(NewCalculator(), i, false, id++.ToString()))
                {
                    passed = false;
                }
            }
            return passed;
        }

   
        static bool TestMismatchedNegativeSymbols(MikesLib.Calculator c, string input = "")
        {
            if (input == "")
                input = "1-,23-,53-,14-,541-,44-,555-";

            double results = c.Add(input, out bool IsError);
            return PrintTestResult("3", "Mismatching negative symbols", "0",  results.ToString(), "True", IsError.ToString());
        }
    }
}
