using System;
using System.IO;
using System.Collections;

namespace MyApp 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Result.ReadingMyData();
        }
    }

    interface ITrade
    {
        //...
        double Value { get; } //indicates the transaction amount in dollars
        string ClientSector { get; } //indicates the client ́s sector which can be "Public" or "Private"
        DateTime NextPaymentDate { get; } //indicates when the next payment from the client to the bank is expected
        //...
    }

    class Trade : ITrade
    {
        public Trade(double value, string clientSector, DateTime nextPaymentDate, DateTime referenceDate){
            this.value = value;
            this.clientSector = clientSector;
            this.nextPaymentDate = nextPaymentDate;
            this.referenceDate = referenceDate;            
        }
        private DateTime referenceDate;
        public DateTime ReferenceDate{
            get => referenceDate;
        }
        private double value;
        public double Value{
            get => value;
        }
        private string clientSector;
        public string ClientSector{
            get => clientSector;
        }
        private DateTime nextPaymentDate;
        public DateTime NextPaymentDate{
            get => nextPaymentDate;
        }
        //regras de classificação
        public String Classification(){
            if(nextPaymentDate.AddDays(30)<referenceDate){
               return "EXPIRED";
            }
            if(value>1000000 && clientSector=="Private"){
               return "HIGHRISK";
            }
            if(value>1000000 && clientSector=="Public"){
               return "MEDIUMRISK";
            }
            return "OTHER";
        }
    }

    class Result
    {
        //lendo dados
        //Sample input
        public static void ReadingMyData(){
            String line;
            try
            {
                StreamReader sr = new StreamReader("C:\\Users\\Public\\Sample.txt");
                line = sr.ReadLine();
                DateTime myReferenceDate =  DateTime.ParseExact(line, "MM/dd/yyyy", null);
                line = sr.ReadLine();
                int myNumberOfTrades =  Int32.Parse(line);
                
                while (line != null)
                {
                    //classificando os dados
                        line = sr.ReadLine();
                        if(line != null){
                            string[] words = line.Split(' ');
                            Trade t = 
                                new Trade(
                                    Double.Parse(words[0]), words[1], 
                                    DateTime.ParseExact(words[2], "MM/dd/yyyy", null), 
                                    myReferenceDate
                                );
                            Console.WriteLine(t.Classification());
                        }
                        
                }
                sr.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                
            }
        }
    }
    
}