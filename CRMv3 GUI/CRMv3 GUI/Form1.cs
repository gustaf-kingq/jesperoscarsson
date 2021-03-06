﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//TODO: Fix lower panel and all related material. Check random ID's so 2 objects can't have the same one
namespace CRMv3_GUI
{
    public partial class Form1 : Form
    {
        //Holds all Person objects (Customer, Employee & Supplier)
        System.Collections.ArrayList peopleList;
        Random random;
        //Global variables
        static int registeredCustomerCounter = 0;
        static int registeredEmployeeCounter = 0;
        static int registeredSupplierCounter = 0;
        static int employeeIDCounter = 0;
        //Used for storing object info inbetween functions
        Person objectHolder;

        //Used to set a customer ID range (Max amount of customers)
        static int customerMinIDVal = 1;
        static int customerMaxIDVal = 500;       

        //TODO: Check if customerID has already been taken

        public Form1()
        {
            InitializeComponent();
            peopleList = new System.Collections.ArrayList();
            random = new Random();
            //Fills dropdown list with options
            dropDownList.Items.Add("Kund"); dropDownList.Items.Add("Anställd"); dropDownList.Items.Add("Leverantör");
        }
        //This triggers when user selects an option from the combobox
        private void dropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (dropDownList.Text)
            {
                case "Anställd":
                    //Show relevant extra fields
                    lblTitel.Visible = true;
                    txtBoxTitel.Visible = true;
                    lblSalary.Visible = true;
                    txtBoxSalary.Visible = true;
                    //Hide others
                    lblCompany.Visible = false;
                    txtBoxCompany.Visible = false;
                    break;
                case "Leverantör":
                    //Show relevant extra fields
                    lblCompany.Visible = true;
                    txtBoxCompany.Visible = true;
                    //Hide others
                    lblTitel.Visible = false; txtBoxTitel.Visible = false;
                    lblSalary.Visible = false; txtBoxSalary.Visible = false;
                    break;
                default:
                    break;
            }
        }
        //This triggers when user presses cancel in upper panel
        private void btnRegisterNewUserCancel_Click(object sender, EventArgs e)
        {
            clearTextBoxes();
            //Set focus to first name
            txtBoxFName.Focus();
        }
        //Creates new object of selected type and saves it to arrayList, updates counters
        private void btnRegisterNewUserSave_Click(object sender, EventArgs e)
        {
            switch (dropDownList.Text)
            {
                case "Kund":
                    peopleList.Add(new Customer { firstName = txtBoxFName.Text, lastName = txtBoxLName.Text, telephoneNumber = txtBoxNumber.Text, customerID = getRndNumb(customerMinIDVal, customerMaxIDVal) });
                    registeredCustomerCounter++;
                    break;
                case "Anställd":
                    employeeIDCounter++;
                    peopleList.Add(new Employee { firstName = txtBoxFName.Text, lastName = txtBoxLName.Text, telephoneNumber = txtBoxNumber.Text, EmployeeNumber = employeeIDCounter, titel = txtBoxTitel.Text, salary = Convert.ToDouble(txtBoxSalary.Text) });
                    registeredEmployeeCounter++;
                    break;
                case "Leverantör":
                    peopleList.Add(new Supplier { firstName = txtBoxFName.Text, lastName = txtBoxLName.Text, telephoneNumber = txtBoxNumber.Text, supplierCompany = txtBoxCompany.Text });
                    registeredSupplierCounter++;
                    break;
                default:
                    break;
            }

            listBoxUpdate();

        }
        //For lower panel, check who has been selected and updates txtBoxes to show correct info
        private void listBoxRegisteredUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            objectHolder = (Person)listBoxRegisteredUsers.SelectedItem;
            clearTextBoxes();

            if (listBoxRegisteredUsers.SelectedItem is Customer)
            {
                Customer tempCustomer = (Customer)listBoxRegisteredUsers.SelectedItem;
                
                txtBoxID_Display.Text = tempCustomer.customerID.ToString();
                txtBoxRegedFName.Text = tempCustomer.firstName;
                txtBoxRegedLName.Text = tempCustomer.lastName;
                txtBoxRegedNumb.Text = tempCustomer.telephoneNumber;

                //Hide unesseccary boxes
                txtBoxRegedSalary.Visible = false;
                lblRegedSalary.Visible = false;
                txtBoxRegedTitel.Visible = false;
                lblRegedTitel.Visible = false;
                txtBoxRegedCompany.Visible = false;
                lblRegedCompany.Visible = false;
            }

            if (listBoxRegisteredUsers.SelectedItem is Employee)
            {
                Employee tempEmployee = (Employee)listBoxRegisteredUsers.SelectedItem;
                //Display additional txtBoxes
                txtBoxRegedSalary.Visible = true;
                lblRegedSalary.Visible = true;
                txtBoxRegedTitel.Visible = true;
                lblRegedTitel.Visible = true;
                //Hide others
                txtBoxRegedSalary.Visible = false;
                lblRegedSalary.Visible = false;
                txtBoxRegedTitel.Visible = false;
                lblRegedTitel.Visible = false;

                txtBoxID_Display.Text = tempEmployee.EmployeeNumber.ToString();
                txtBoxRegedFName.Text = tempEmployee.firstName;
                txtBoxRegedLName.Text = tempEmployee.lastName;
                txtBoxRegedNumb.Text = tempEmployee.telephoneNumber;
                txtBoxRegedSalary.Text = tempEmployee.salary.ToString();
                txtBoxRegedTitel.Text = tempEmployee.titel;
            }

            if (listBoxRegisteredUsers.SelectedItem is Supplier)
            {
                Supplier tempSupplier = (Supplier)listBoxRegisteredUsers.SelectedItem;
                //Display additional txtBoxes
                txtBoxRegedCompany.Visible = true;
                lblRegedCompany.Visible = true;
                //Hide others
                txtBoxRegedCompany.Visible = false;
                lblRegedCompany.Visible = false;

                txtBoxID_Display.Text = string.Empty;
                txtBoxRegedFName.Text = tempSupplier.firstName;
                txtBoxRegedLName.Text = tempSupplier.lastName;
                txtBoxRegedNumb.Text = tempSupplier.telephoneNumber;
                txtBoxRegedCompany.Text = tempSupplier.supplierCompany;
            }
        }
        //For lower panel, clears txtBoxes and refills them with info from selected listBox item
        private void btnRegisteredUsersCancelChanges_Click(object sender, EventArgs e)
        {
            clearTextBoxes();

            if (objectHolder is Customer)
            {
                Customer tempCustomer = (Customer)objectHolder;
                txtBoxID_Display.Text = tempCustomer.customerID.ToString();
                txtBoxRegedFName.Text = tempCustomer.firstName;
                txtBoxRegedLName.Text = tempCustomer.lastName;
                txtBoxRegedNumb.Text = tempCustomer.telephoneNumber;
            }

            if (objectHolder is Employee)
            {
                Employee tempEmployee = (Employee)objectHolder;
                txtBoxID_Display.Text = tempEmployee.EmployeeNumber.ToString();
                txtBoxRegedFName.Text = tempEmployee.firstName;
                txtBoxRegedLName.Text = tempEmployee.lastName;
                txtBoxRegedNumb.Text = tempEmployee.telephoneNumber;
                txtBoxRegedSalary.Text = tempEmployee.salary.ToString();
                txtBoxRegedTitel.Text = tempEmployee.titel;
            }
            if (objectHolder is Supplier)
            {
                Supplier tempSupplier = (Supplier)objectHolder;
                txtBoxID_Display.Text = string.Empty;
                txtBoxRegedFName.Text = tempSupplier.firstName;
                txtBoxRegedLName.Text = tempSupplier.lastName;
                txtBoxRegedNumb.Text = tempSupplier.telephoneNumber;
                txtBoxRegedCompany.Text = tempSupplier.supplierCompany;
            }
        }
        //For lower panel, updates info in selected object with newly entered info
        private void btnRegisteredUsersSaveChanges_Click(object sender, EventArgs e)
        {
            if (listBoxRegisteredUsers.SelectedItem is Customer)
            {
                Customer tempCustomer = (Customer)objectHolder;

                tempCustomer.firstName = txtBoxRegedFName.Text;
                tempCustomer.lastName = txtBoxRegedLName.Text;
                tempCustomer.telephoneNumber = txtBoxRegedNumb.Text;
            }

            if (listBoxRegisteredUsers.SelectedItem is Employee)
            {
                Employee tempEmployee = (Employee)objectHolder;

                tempEmployee.firstName = txtBoxRegedFName.Text;
                tempEmployee.lastName = txtBoxRegedLName.Text;
                tempEmployee.telephoneNumber = txtBoxRegedNumb.Text;
                tempEmployee.salary = Convert.ToDouble(txtBoxRegedSalary.Text);
                tempEmployee.titel = txtBoxRegedTitel.Text;
            }

            if (listBoxRegisteredUsers.SelectedItem is Supplier)
            {
                Supplier tempSupplier = (Supplier)objectHolder;

                tempSupplier.firstName = txtBoxRegedFName.Text;
                tempSupplier.lastName = txtBoxRegedLName.Text;
                tempSupplier.telephoneNumber = txtBoxRegedNumb.Text;
                tempSupplier.supplierCompany = txtBoxRegedCompany.Text;
            }

            listBoxUpdate();

        }
        
        //Updates listBox and counter label 
        public void listBoxUpdate()
        {
            //Update label
            lblTellHowManyRegistered.Text = string.Format("Du har registrerat {0} kunder, {1} anställda och {2} leverantörer.", registeredCustomerCounter, registeredEmployeeCounter, registeredSupplierCounter);
            //Update listBox
            listBoxRegisteredUsers.Items.Clear();
            foreach (var item in peopleList)
            {
                if (item is Customer)
                {
                    listBoxRegisteredUsers.Items.Add(item);
                    listBoxRegisteredUsers.DisplayMember = "fullName";
                }
            }
            foreach (var item in peopleList)
            {
                if (item is Employee)
                {
                    listBoxRegisteredUsers.Items.Add(item);
                    listBoxRegisteredUsers.DisplayMember = "fullName";
                }
            }

            foreach (var item in peopleList)
            {
                if (item is Supplier)
                {
                    listBoxRegisteredUsers.Items.Add(item);
                    listBoxRegisteredUsers.DisplayMember = "fullName";
                }
            }

            //Clear txtBoxes where user left input
            clearTextBoxes();
        }
        //Clears textBoxes where user left input
        public void clearTextBoxes()
        {
            txtBoxFName.Text = string.Empty;
            txtBoxLName.Text = string.Empty;
            txtBoxNumber.Text = string.Empty;
            txtBoxTitel.Text = string.Empty;
            txtBoxSalary.Text = string.Empty;
            txtBoxCompany.Text = string.Empty;

            txtBoxRegedCompany.Text = string.Empty;
            txtBoxRegedSalary.Text = string.Empty;
            txtBoxRegedTitel.Text = string.Empty;
            txtBoxRegedFName.Text = string.Empty;
            txtBoxRegedLName.Text = string.Empty;
            txtBoxRegedNumb.Text = string.Empty;
        }
        //Gives a random value between an interval
        public int getRndNumb(int minVal, int maxVal)
        {
            int rndNumber = random.Next(minVal, (maxVal+1));

            return rndNumber;
        }
    }
}