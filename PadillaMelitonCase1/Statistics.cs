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
    [Activity(Label = "Statistics")]
    public class Statistics : Activity
    {
        private int currentIndex = 0;
        private SortedList<String, Employee> EmployeeListSortedByLastName =
            new SortedList<String, Employee>();
        TextView txtDisplayResults;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.StatisticsUI);

            // Grab Sorted array from extra
            string txtFromActivity = Intent.GetStringExtra("EmployeeSortedList");
            EmployeeListSortedByLastName = Newtonsoft.Json.JsonConvert.DeserializeObject<SortedList<string, Employee>>(txtFromActivity);

            // Connect layout
            Button btnMin = (Button)FindViewById(Resource.Id.btnMin);
            Button btnMax = (Button)FindViewById(Resource.Id.btnMax);
            Button btnAverage = (Button)FindViewById(Resource.Id.btnAverage);
            Button btnTotal = (Button)FindViewById(Resource.Id.btnTotal);
            Button btnBackToMenu = (Button)FindViewById(Resource.Id.btnBackToMenu);
            txtDisplayResults = (TextView)FindViewById(Resource.Id.txtDisplayResults);

            // create methods
            btnMin.Click += btnMin_Click;
            btnMax.Click += btnMax_Click;
            btnAverage.Click += btnAverage_Click;
            btnTotal.Click += btnTotal_Click;
            btnBackToMenu.Click += btnBackToMenu_Click;

        }

        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            int initRan = Intent.GetIntExtra("initRan", 1);
            // Send data back to main menu
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(EmployeeListSortedByLastName);
            Intent activityIntent = new Intent(this, typeof(MainActivity));
            activityIntent.PutExtra("EmployeeSortedList", output);
            activityIntent.PutExtra("initRan", initRan);
            StartActivity(activityIntent);
        }

        private void btnTotal_Click(object sender, EventArgs e)
        {
            // Current index
            int index = 0;
            double payrollTotal = 0;

            // Search all for min hours/max
            Employee total;

            for (int x = 0; x < 100; x++)
            {
                KeyValuePair<string, Employee> keyPair;
                keyPair = EmployeeListSortedByLastName.ElementAt(index);
                total = EmployeeListSortedByLastName[keyPair.Key];

                // Add all wages and hours
                payrollTotal += total.TotalPayroll;
                
            }

            // Output results
            txtDisplayResults.Text = "";
            txtDisplayResults.Text += "Total payroll for all records is: " + payrollTotal + "\n";
        }

        private void btnAverage_Click(object sender, EventArgs e)
        {
            // Current index
            int index = 0;
            double wageTotal = 0;
            double hoursTotal = 0;
            // Search all for min hours/max
            Employee average;

            for (int x = 0; x < 100; x++)
            {
                KeyValuePair<string, Employee> keyPair;
                keyPair = EmployeeListSortedByLastName.ElementAt(index);
                average = EmployeeListSortedByLastName[keyPair.Key];

                // Add all wages and hours
                wageTotal += average.HourlyWage;
                hoursTotal += average.HoursWorked;
                
            }
            // Divide by total number of people to get average
            wageTotal = wageTotal / 100;
            hoursTotal = hoursTotal / 100;

            // Output results
            txtDisplayResults.Text = "";
            txtDisplayResults.Text += "Average Hours worked is: " + hoursTotal + "\n";
            txtDisplayResults.Text += "Average hourly wage is: " + wageTotal + "\n";
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            // Current index
            int index = 0;
            double maxHours = 0;
            double maxWage = 9.00;
            // Search all for min hours/max
            Employee max;

            for (int x = 0; x < 100; x++)
            {
                KeyValuePair<string, Employee> keyPair;
                keyPair = EmployeeListSortedByLastName.ElementAt(index);
                max = EmployeeListSortedByLastName[keyPair.Key];

                // get first values
                if (max.HourlyWage > maxWage)
                {
                    maxWage = max.HourlyWage;
                }
                if (max.HoursWorked > maxHours)
                {
                    maxHours = max.HoursWorked;
                }
                index++;
            }

            // output results
            txtDisplayResults.Text = "";
            txtDisplayResults.Text += "Max Hours worked is: " + maxHours + "\n";
            txtDisplayResults.Text += "Max hourly wage is: " + maxWage + "\n";
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            // Current index
            int index = 0;
            double minHours = 100;
            double minWage = 180;
            // Search all for min hours/max
            Employee min;

            for (int x = 0; x < 100; x++)
            {
                KeyValuePair<string, Employee> keyPair;
                keyPair = EmployeeListSortedByLastName.ElementAt(index);
                min = EmployeeListSortedByLastName[keyPair.Key];

                // get first values
                if(min.HourlyWage < minWage)
                {
                    minWage = min.HourlyWage;
                }
                if(min.HoursWorked < minHours)
                {
                    minHours = min.HoursWorked;
                }
                index++;
            }

            // output results
            txtDisplayResults.Text = "";
            txtDisplayResults.Text += "Min Hours worked is: " + minHours + "\n";
            txtDisplayResults.Text += "Min hourly wage is: " + minWage + "\n";
        }
    }
}