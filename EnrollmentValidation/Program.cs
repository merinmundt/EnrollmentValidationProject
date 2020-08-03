using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace EnrollmentValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Enrollment> Enrollments = new List<Enrollment>();
            string inPath = "EnrollmentApplications.txt";
            string[] readtext = File.ReadAllLines(inPath);
            
            foreach (var section in readtext){
                string[] line = section.Split(',');
                //validating all regions are entered
                if(line.Length != 5){
                    Console.WriteLine("A record in the file failed validation.  Processing has stopped. spot 1");
                    return;
                }

                //validating entries
                for(int i = 2; i < 5; i++){
                    if(i == 3){
                        if(!ValidatePlan(line[i].Trim())){
                            Console.WriteLine("A record in the file failed validation.  Processing has stopped. spot 2");
                            return;
                        }
                    }
                    else{
                        //added .trim in case there is a space within entry
                        if(!ValidateDate(line[i].Trim())){
                            Console.WriteLine("A record in the file failed validation.  Processing has stopped. spot 3");
                            return;
                        }
                    }
                }
                //validating extra business rules
                string status;  
                if(!ValidateAge(line[2].Trim()) || !Validate30DayLimit(line[4].Trim())){
                    status = "Rejected";
                }
                else{
                    status = "Accepted";
                }

                //string-->DateTime format
                var cultureInfo = new CultureInfo("en-US");
                string[] format = {"MMddyyyy"};
                DateTime birthday = (DateTime.ParseExact(line[2].Trim(), format, cultureInfo));
                DateTime effectiveDate = (DateTime.ParseExact(line[2].Trim(), format, cultureInfo));
                
                //creating a new enrollment to be stored
                Enrollment newEnrollment = new Enrollment(line[0],line[1],birthday,line[3],effectiveDate);
                newEnrollment.Status = status;

                //storing the accepted/rejected enrollments
                Enrollments.Add(newEnrollment);
                //attempted encapsulation and abstraction through this
                Console.WriteLine(newEnrollment.ToString());

            } 
            
        }
        //validates the entered date as an actual date
        static Boolean ValidateDate(string DOB){
            //Boolean verified = false;
            CultureInfo enUS = new CultureInfo("en-US");

            DateTime outDate;
            string[] format = {"MMddyyyy"};

            return DateTime.TryParseExact(DOB, format, enUS, DateTimeStyles.None, out outDate);  
        }
        
        //validates the entered date as an actual date
        static Boolean ValidatePlan(string plan){
            if(plan == "HSA" || plan == "HRA" || plan == "FSA"){
                return true;
            }
            return false;
        }
        //validating age of atleast 18
        static Boolean ValidateAge(string date){
            var cultureInfo = new CultureInfo("en-US");
            string[] format = {"MMddyyyy"};
            DateTime birthday = (DateTime.ParseExact(date, format, cultureInfo));
            int age = DateTime.Today.Year - birthday.Year -1;
            if(age < 18){
                return false;
            } 
            return true;
        }
        //validating the 30 day max rule
        static Boolean Validate30DayLimit(string date){
            var cultureInfo = new CultureInfo("en-US");
            string[] format = {"MMddyyyy"};
            DateTime EffectiveDate = (DateTime.ParseExact(date, format, cultureInfo));
            double timeTillEffectiveDate = (EffectiveDate - DateTime.Today).TotalDays;
            if(timeTillEffectiveDate > 30){
                return false;
            }
            return true;
        }
    }
}
