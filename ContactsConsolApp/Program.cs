
using System;
using System.Data;
using ContactBusinessLayer;
using ContactsBusinessLayer;


namespace ContactsConsolApp
{
    internal class Program
    {
        static void testFindContact(int ID)

        {
            clsContact Contact1 = clsContact.Find(ID);
                                  

            if (Contact1 != null)
            {
                Console.WriteLine(Contact1.FirstName+ " " + Contact1.LastName);
                Console.WriteLine(Contact1.Email);
                Console.WriteLine(Contact1.Phone);
                Console.WriteLine(Contact1.Address);
                Console.WriteLine(Contact1.DateOfBirth);
                Console.WriteLine(Contact1.CountryID);
                Console.WriteLine(Contact1.ImagePath);
            }
            else 
            {
                Console.WriteLine("Contact [" + ID + "] Not found!");   
            }
        }



        static void testAddNewContact()
        {
            clsContact Contact1 = new clsContact();

            Contact1.FirstName = "Fadi";
            Contact1.LastName = "Maher";
            Contact1.Email = "A@a.com";
            Contact1.Phone = "010010";
            Contact1.Address = "address1";
            Contact1.DateOfBirth = new DateTime(1977, 11, 6, 10, 30, 0);
            Contact1.CountryID = 1;
            Contact1.ImagePath = "";
          
           if (Contact1.Save())
            {

                Console.WriteLine("Contact Added Successfully with id=" + Contact1.ID);
            }

        }

        static void testUpdateContact(int ID)
        {
            clsContact Contact1 = clsContact.Find(ID);

            if (Contact1 != null)
            {
                //update whatever info you want
                Contact1.FirstName = "Lina";
                Contact1.LastName = "Maher";
                Contact1.Email = "A2@a.com";
                Contact1.Phone = "2222";
                Contact1.Address = "222";
                Contact1.DateOfBirth = new DateTime(1977, 11, 6, 10, 30, 0);
                Contact1.CountryID = 1;
                Contact1.ImagePath = "";

                if (Contact1.Save())
                {

                    Console.WriteLine("Contact updated Successfully ");
                }

            }
            else
            {
                Console.WriteLine("Not found!");
            }
        }

        static void testDeleteContact(int ID)

        {

            if (clsContact.isContactExist(ID))

                if (clsContact.DeleteContact(ID))

                    Console.WriteLine("Contact Deleted Successfully.");
                else
                    Console.WriteLine("Faild to delete contact.");

            else
                Console.WriteLine("The contact with id = " + ID + " is not found");

        }

        static void ListContacts()
        {

            DataTable dataTable = clsContact.GetAllContacts();
           
            Console.WriteLine("Contacts Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["ContactID"]},  {row["FirstName"]} {row["LastName"]}");
            }

        }


        static void testIsContactExist(int ID)

        {

            if (clsContact.isContactExist(ID))

                Console.WriteLine("Yes, Contact is there.");

            else
                Console.WriteLine("No, Contact Is not there.");

        }

        static void testFindCountryByID(int ID)
        {
            clsCountry Country = clsCountry.FindCountryByID(ID);

            if (Country != null)
            {
                Console.WriteLine("Country Name : " + Country.CountryName);
                Console.WriteLine("Code : " + Country.Code);
                Console.WriteLine("Phone Code : " + Country.PhoneCode);
            }
            else
                Console.WriteLine("Country [" + ID + "] is not exist");
            
        }

        static void testFindCountryByName(string CountryName)
        {
            clsCountry Country = clsCountry.FindCountryByName(CountryName);

            if (Country != null)
            {
                Console.WriteLine("Country [" + Country.CountryName + "] is found with id = " + Country.ID);
                Console.WriteLine("Code : " + Country.Code);
                Console.WriteLine("Phone Code : " + Country.PhoneCode);
            }
            else
                Console.WriteLine("Country is not exist");
        }

        static void testIsCountryExistByID(int ID)
        {
            if (clsCountry.IsCountryExistByID(ID))
                Console.WriteLine("Yes , Country is there");
            else
                Console.WriteLine("No , Country is not there");
        }

        static void testIsCountryExistsByName(string CountryName)
        {
            if (clsCountry.IsCountryExistByName(CountryName))
                Console.WriteLine("Yes , Country is there");
            else
                Console.WriteLine("No , Country is not there");
        }

        static void testAddNewCountry()
        {
            clsCountry Country1 = new clsCountry();

           
            Country1.CountryName = "Qatar";
            Country1.Code = "049";
            Country1.PhoneCode = "91277";

            if (Country1.Save())
            {
                Console.WriteLine("Country Saved Successfully");
            }
            else
                Console.WriteLine("Country doesn't Saved");
        }

        static void testUpdateCountry(int ID)
        {
            clsCountry Country1 = clsCountry.FindCountryByID(ID);

            if (Country1 != null)
            {
                Country1.CountryName = "Russia";

                if (Country1.Save())
                    Console.WriteLine("Country Updated Successfully");
                else
                    Console.WriteLine("Country failed to Update");
            }
            else
                Console.WriteLine("Not Found!");
            
        }

        static void testDeleteCountry(int ID)
        {
            if (clsCountry.IsCountryExistByID(ID))
            {
                if (clsCountry.DeleteCountry(ID))
                    Console.WriteLine("Country Deleted Successfully");
                else
                    Console.WriteLine("Country failed to delete");

            }
            else
                Console.WriteLine("Country is not found!");
        }

        static void ListCountries()
        {
            DataTable dt = clsCountry.ListCountries();

            Console.WriteLine("Countries Data : ");

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["CountryID"]} , {row["CountryName"]}");
            }

        }

        static void Main(string[] args)
        {

             // testFindContact(1);

            //testAddNewContact();


           // testUpdateContact(1);

           // testIsContactExist(1);
            //testIsContactExist(100);

           //  testDeleteContact(1);

             //ListContacts();

            //testFindCountryByID(2);
            //testFindCountryByName("Canada");

            //testIsCountryExistByID(14);

            // testIsCountryExistsByName("Canada");

            //testAddNewCountry();

            //testUpdateCountry(7);

            //testDeleteCountry(5);

            //ListCountries();

            Console.ReadKey();

        }
    }
}
