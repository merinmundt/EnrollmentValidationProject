using System;

namespace EnrollmentValidation
{
    public class Enrollment
    {
        public Enrollment(string firstName, string lastName, DateTime dOB, string plan, DateTime effectiveDate)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DOB = dOB;
            this.Plan = plan;
            this.EffectiveDate = effectiveDate;

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Plan { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string Status {get; set;}

        public override string ToString(){
            return $"{Status}, {FirstName}, {LastName}, {DOB:MMddyyyy}, {Plan}, {EffectiveDate:MMddyyyy}";
        }
    }
}