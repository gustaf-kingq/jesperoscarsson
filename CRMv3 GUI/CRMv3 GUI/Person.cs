﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMv3_GUI
{
    public abstract class Person
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string telephoneNumber { get; set; }
        //Must have values in first & lastname
        public string fullName
        {
            get { return string.Format("{0} {1}", firstName, lastName); }
        }

    }

    public class Customer : Person
    {
        private int UniqueCustomerID;

        public int customerID
        {
            get { return UniqueCustomerID; }
            set { UniqueCustomerID = value; }
        }
    }

    public class Employee : Person
    {

    }

    public class Supplier : Person
    {
        public string supplierCompany { get; set; }
    }
}