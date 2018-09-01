using System;
using System.Collections.Generic;
using System.Text;

namespace LocationService.Domain.Models.IBGE
{
    public class County
    {
        public string CodeIBGE { get; set; }
        public string Gentlic { get; set; }
        public string Mayor { get; set; }
        public Population Population { get; set; }
        public WorkIncome WorkIncome { get; set; }
        public Education Education { get; set; }
        public Cheers Cheers { get; set; }
        public TerritoryEnvironment TerritoryEnvironment { get; set; }
    }

    public class Population
    {
        public string PopulationEstimated { get; set; }
        public string PopulationLastCensus { get; set; }
        public string DemographicDensity { get; set; }
    }

    public class WorkIncome
    {
        public string SalaryAverageMonth { get; set; }
        public string PeopleBusy { get; set; }
        public string PopulationBusyPercentage { get; set; }
        public string PopMonthMinWage { get; set; }
    }

    public class Education
    {
        public string SchoolingRate { get; set; }
        public string EarlyYearSchool { get; set; }
        public string FinalYearSchool { get; set; }
        public string EnrollSchoolFund { get; set; }
        public string EnrollSchoolAvg { get; set; }
        public string TeacherSchoolFund { get; set; }
        public string TeacherSchoolAvg { get; set; }
        public string InstituteSchoolFund { get; set; }
        public string InstituteSchoolAvg { get; set; }
    }

    public class Economy
    {
        public string PIB { get; set; }
        public string PercRevFontExt { get; set; }
        public string IndDesenHumWor { get; set; }
        public string AmountRecFulfilled { get; set; }
        public string AmountComExp { get; set; }
    }

    public class Cheers
    {
        public string ChildMortality { get; set; }
        public string HospDiarrhea { get; set; }
        public string HealthInsuranceSUS { get; set; }
    }

    public class TerritoryEnvironment
    {
        public string AreaUnitTerr { get; set; }
        public string AdequateSanSew { get; set; }
        public string ArbRoadsPub { get; set; }
        public string UrbRoadsPub { get; set; }
    }

}
