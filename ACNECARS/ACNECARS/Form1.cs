﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACNECARS
{
    public partial class Form1 : Form
    {

        System.Collections.ArrayList carsArray;

        public Form1()
        {
            InitializeComponent();
            carsArray = new System.Collections.ArrayList();
            //Initialize with 3 premade vehicles
            carsArray.Add(new Car() { model = "Volvo s60", make = "Volvo", color = "Red", available = true});
            carsArray.Add(new Car() { model = "Saab 900 Turbo", make = "Saab", color = "Blue", available = true});
            carsArray.Add(new Car() { model = "Volvo 240", make = "Volvo", color = "Green", available = true });
            //Add premade cars to listbox and update available cars counter
            foreach (Car item in carsArray)
            {
                listBoxAvailableCarsList.Items.Add(item);
                listBoxAvailableCarsList.DisplayMember = "model";
            }
            updateAvailableCarsCounter();
        }

        private void btnShowCars_Click(object sender, EventArgs e)
        {
            pnlShowCars.Visible = !pnlShowCars.Visible;
            pnlAddCar.Visible = false;
            updateListBox();
        }

        private void listBoxAvailableCarsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlCarInfo.Visible = true;
            btnBook.Visible = true;

            //Create temp object so we can update carInfo
            Car carTemp = (Car)listBoxAvailableCarsList.SelectedItem;
            lblModel.Text = carTemp.model; lblMake.Text = carTemp.make; lblColor.Text = carTemp.color;
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            Car carTemp = (Car)listBoxAvailableCarsList.SelectedItem;
            carTemp.available = false; //Sets status to 'hired' for selected car
            //Hides panels and pnlCarInfo labels
            pnlCarInfo.Visible = false;
            btnBook.Visible = false;

            lblColor.Text = string.Empty; lblModel.Text = string.Empty; lblMake.Text = string.Empty;
            lblCarsAvailable.Text = string.Empty;
            //Updates listbox so the booked car disappears
            updateListBox();
            updateAvailableCarsCounter();
        }
        
        private void updateListBox()
        {
            listBoxAvailableCarsList.Items.Clear();
            foreach (Car item in carsArray)
            {
                if (item.available)
                {
                    listBoxAvailableCarsList.Items.Add(item);
                    listBoxAvailableCarsList.DisplayMember = "model";
                }
            }
        }

        private void btnShowAddCar_Click(object sender, EventArgs e)
        {
            pnlAddCar.Visible = !pnlAddCar.Visible;
            pnlShowCars.Visible = false;
        }

        private void btnCarAdd_Click(object sender, EventArgs e)
        {
            txtBoxMake.Focus();
            //Adds new car object
            carsArray.Add(new Car() { model = txtBoxModel.Text, make = txtBoxMake.Text, color = txtBoxColor.Text, available = true });
            updateListBox();
            updateAvailableCarsCounter();
            txtBoxColor.Text = string.Empty; txtBoxModel.Text = string.Empty; txtBoxMake.Text = string.Empty;
        }

        private void updateAvailableCarsCounter()
        {
            int counter = 0;

            foreach (Car Item in carsArray)
            {
                if (Item.available)
                {
                    counter++;
                }
            }

            lblCarsAvailable.Text = counter.ToString();
        }
    }
}
