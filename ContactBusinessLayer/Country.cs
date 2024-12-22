using System;
using System.Data;
using ContactsDataAccessLayer;


namespace ContactBusinessLayer
{
    public class clsCountry
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode Mode = enMode.AddNew;


        public int ID { set; get; }
        public string CountryName { set; get; }
        public string Code { set; get; }
        public string PhoneCode { set; get; }

        private clsCountry(int ID, string CountryName , string Code , string PhoneCode)
        {
            this.ID = ID;
            this.CountryName = CountryName;
            this.Code = Code;
            this.PhoneCode = PhoneCode;


            Mode = enMode.Update;
        }

        public clsCountry()
        {
            this.ID = -1;
            this.CountryName = "";
           

            Mode = enMode.AddNew;
        }


        public static clsCountry FindCountryByID(int ID)
        {
            string CountryName = "" , Code = "" , PhoneCode = "";
            

            if (clsCountryData.FindCountryByID(ID, ref CountryName , ref Code , ref PhoneCode))
            {
                return new clsCountry(ID, CountryName , Code , PhoneCode);
            }
            else
                return null;
        }

        public static clsCountry FindCountryByName(string CountryName)
        {
            int CountryID = 0;
            string Code = "", PhoneCode = "";

            if (clsCountryData.FindCountryByName(ref CountryID , CountryName , ref Code , ref PhoneCode))
                return new clsCountry(CountryID, CountryName , Code ,PhoneCode);
            else
                return null;
        }

        public static bool IsCountryExistByID(int ID)
        {
            return clsCountryData.IsCountryExistByID(ID);
        }

        public static bool IsCountryExistByName(string CountryName)
        {
            return clsCountryData.IsCountryExistsByName(CountryName);
        }

        private bool _AddNewCountry()
        {
            this.ID = clsCountryData.AddNewCountry(this.CountryName , this.Code , this.PhoneCode);

            return (this.ID != -1);
        }

        private bool _UpdateCountry()
        {
            return clsCountryData.UpdateCountry(this.ID,this.CountryName , this.Code , this.PhoneCode);
        }

        public bool Save()
        {
           switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:

                    return _UpdateCountry();
            }

            return false;
        }

        public static bool DeleteCountry(int ID)
        {
            return clsCountryData.DeleteCountry(ID);
        }

        public static DataTable ListCountries()
        {
            return clsCountryData.ListCountries();
        }

    }
}
