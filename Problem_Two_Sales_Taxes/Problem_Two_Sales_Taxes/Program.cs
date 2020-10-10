/*
Name: Avinash Kumar
Email: avinash.cse10@gmail.com
Description: This program reads a text file from the given path and calulate the item in shopping cart and returns
a text file with as output with calculated amount including all the taxes.
You need to set your path for the input file as well as change the name if you are using some different name for you input test case
then change the input filename in line 27.
After executing a new file will be created in same path with name as output.txt (if you want to change the output filename,
change it in line 66).
*/
using System;

namespace Problem_Two_Sales_Taxes
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initializing data and path of input and output file.
            double TotalTax = 0.0;
            double totalValue = 0.0;
            string outputText = "";
            //Path for Input File and output file. I have use read/write from and into a text file respectively
            //and the path should be different for each computer, later we can use it as an application if we take the
            //path as command line argument and publish it.
            string path = @"/Users/avinashkumar/Projects/Problem_Two_Sales_Taxes/";
            string[] lines = System.IO.File.ReadAllLines(path+"input3.txt"); //input file name, I have used input1.txt, input2.txt and input3.txt
           
            foreach (string line in lines)
            {
                string[] words = line.Split(' ');
                double value = 0.0;
                double ImportTax = 0.0;
                double SalesTax = 0.0;
                value = double.Parse(words[words.Length - 1]);
                //Add import tax to imported item.
                if (Array.Exists(words, element => element == "Imported"))
                    ImportTax = (importTax(value)); //Function created to calculate import tax
                //ignore books, foods and medical products from sales taxes
                if (Array.Exists(words, element => element == "chocolates"))
                    SalesTax = 0;
                else if (Array.Exists(words, element => element == "Chocolate"))
                    SalesTax = 0;
                else if (Array.Exists(words, element => element == "Book"))
                    SalesTax = 0;
                else if (Array.Exists(words, element => element == "pills"))
                    SalesTax = 0;
                else
                {
                    SalesTax = (salesTax(value));   //Function created to calculate sales tax
                }//add sales taxes to all the items excluding sales tax exempt products
                    
                value += ImportTax + SalesTax;
                TotalTax+= ImportTax + SalesTax;
                totalValue += value;
                value = Math.Round(value, 2);
                TotalTax = Math.Round(TotalTax, 2);
                totalValue = Math.Round(totalValue, 2);
                for(int i=1;i<words.Length-2;i++)
                    outputText = outputText+words[i]+" ";
                outputText = outputText +": "+value.ToString()+ "\n";
            }
            outputText+=("Sales Taxes: "+TotalTax.ToString()+"\n"); 
            outputText+=("Total: "+totalValue.ToString());
            //Re-formatting the output or final bill so that same product can be calculated in a single line.
            System.IO.File.WriteAllText(path + "output.txt",FormatOutputText(outputText));

            //Console.WriteLine(FormatOutputText(outputText));
        }
        static string FormatOutputText(string text)     //Function to re-formatting the final text file
        {
            string[] lines = text.Split('\n');      
            string newText = "";

            for(int i = 0; i < lines.Length - 1; i++)
            {
                int count = 1;
                for(int j = i + 1; j < lines.Length; j++)
                {
                    if (lines[i].Equals(lines[j]))
                    {
                        count++;
                        lines[j] = "";
                    }
                }
                if (count == 1)
                {
                    newText += lines[i]+"\n";
                }
                else if (lines[i].Equals(""))
                    continue;
                else
                {
                    string[] words = lines[i].Split(':');
                    newText += words[0] + ": ";
                    
                    newText += Math.Round(double.Parse(words[1]) * count, 2).ToString() + "(" +
                        count.ToString() + " @" + words[1] + ")\n";
                }
            }
            return newText+lines[lines.Length-1];
        }
        static double importTax(double x)   //Function to calculate import tax
        {
            x = (int)(x * (5));
                x=Math.Ceiling(x / 5) * 5; 
            return (x/100.0);
        }
        static double salesTax(double x)    //Function to calculate sales tax
        {
            x = (int)(x * (10));
            x = Math.Ceiling(x / 5) * 5;
            return (x / 100.0);
        }
    }
}
