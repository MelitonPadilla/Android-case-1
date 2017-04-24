using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PadillaMelitonCase1
{
    [Activity(Label = "InitSortedList")]
    public class InitSortedList : Activity
    {
        private int currentIndex = 0;
        bool Initarray = false;
        private SortedList<String, Employee> EmployeeListSortedByLastName =
            new SortedList<String, Employee>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SortedList);

            // Connect layout
            Button btnInitalizeArray = (Button)FindViewById(Resource.Id.btnInitalizeArray);
            Button btnViewArray = (Button)FindViewById(Resource.Id.btnViewArray);
            Button btnBackToMenu = (Button)FindViewById(Resource.Id.btnBackToMenu);

            // Create methods
            btnInitalizeArray.Click += btnInitalizeArray_Click;
            btnViewArray.Click += btnViewArray_Click;
            btnBackToMenu.Click += btnBackToMenu_Click;

        }

        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            int initRan = Intent.GetIntExtra("initRan",1);
            // Send data back to main menu
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(EmployeeListSortedByLastName);
            Intent activityIntent = new Intent(this, typeof(MainActivity));
            activityIntent.PutExtra("EmployeeSortedList", output);
            activityIntent.PutExtra("initRan", initRan);
            StartActivity(activityIntent);
        }

        protected void ShowStudentCurrent(int index)
        {
            Employee e;
            KeyValuePair<string, Employee> keyPair;
            keyPair = EmployeeListSortedByLastName.ElementAt(index);
            e = EmployeeListSortedByLastName[keyPair.Key];
            TextView txtDisplaySortedList = (TextView)FindViewById(Resource.Id.txtDisplaySortedList);

            txtDisplaySortedList.Text += "EmployeeID: " + e.EmployeeID.ToString()+ "\n";
            txtDisplaySortedList.Text += "FirstName: " + e.FirstName + "\n";
            txtDisplaySortedList.Text += "LastName: " + e.LastName + "\n";
            txtDisplaySortedList.Text += "HourlyWage: " + e.HourlyWage.ToString() + "\n";
            txtDisplaySortedList.Text += "HoursWorked: " + e.HoursWorked.ToString() + "\n";
            txtDisplaySortedList.Text += "TotalPayroll: " + e.TotalPayroll.ToString() + "\n";
            txtDisplaySortedList.Text += "\n";

        }
        private void btnViewArray_Click(object sender, EventArgs args)
        {
            // Test to make sure Array has been init
            if (Initarray == true)
            {
                for (int x = 0; x < 100; x++)
                {
                    ShowStudentCurrent(x);
                }
            }
            else
            {
                // Inform user that they must init first
                TextView txtDisplaySortedList = (TextView)FindViewById(Resource.Id.txtDisplaySortedList);
                txtDisplaySortedList.Text = "Array must be initalized first!";

            }
            
        }

        private void btnInitalizeArray_Click(object sender, EventArgs args)
        {
            // Set flag to true
            Initarray = true;
            for (int x = 0; x < 100; x++)
            {
                Employee e = Employee.createEmployee();
                EmployeeListSortedByLastName.Add(e.LastName, Employee.createEmployee());
            }
            TextView txtDisplaySortedList = (TextView)FindViewById(Resource.Id.txtDisplaySortedList);
            txtDisplaySortedList.Text = "Array has been successfully initalized!";
        }
    }
}