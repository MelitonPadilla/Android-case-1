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
    [Activity(Label = "ModifyEmployeeList")]
    public class ModifyEmployeeList : Activity
    {
        private int currentIndex = 0;
        private SortedList<String, Employee> EmployeeListSortedByLastName =
            new SortedList<String, Employee>();
        EditText editSortedEmployeeID;
        EditText editSortedFirstName;
        EditText editSortedLastName;
        EditText editSortedHourlyWage;
        EditText editSortedHoursWorked;
        EditText editSortedTotalPayroll;
        TextView txtDisplayError;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Modify);

            // Connect layout
            TextView txtSelectEmployee = (TextView)FindViewById(Resource.Id.txtSelectEmployee);
            Button btnSortedPrev = (Button)FindViewById(Resource.Id.btnSortedPrev);
            Button btnSortedNext = (Button)FindViewById(Resource.Id.btnSortedNext);
            Button btnSortedFindByID = (Button)FindViewById(Resource.Id.btnSortedFindByID);
            editSortedEmployeeID = (EditText)FindViewById(Resource.Id.editSortedEmployeeID);
            editSortedFirstName = (EditText)FindViewById(Resource.Id.editSortedFirstName);
            editSortedLastName = (EditText)FindViewById(Resource.Id.editSortedLastName);
            editSortedHourlyWage = (EditText)FindViewById(Resource.Id.editSortedHourlyWage);
            editSortedHoursWorked = (EditText)FindViewById(Resource.Id.editSortedHoursWorked);
            editSortedTotalPayroll = (EditText)FindViewById(Resource.Id.editSortedTotalPayroll);
            Button btnAddEmployee = (Button)FindViewById(Resource.Id.btnAddEmployee);
            Button btnUpdateEmployee = (Button)FindViewById(Resource.Id.btnUpdateEmployee);
            Button btnRemoveEmployee = (Button)FindViewById(Resource.Id.btnRemoveEmployee);
            Button btnBackToMenu = (Button)FindViewById(Resource.Id.btnBackToMenu);
            txtDisplayError = (TextView)FindViewById(Resource.Id.txtDisplayError);

            // Grab Sorted array from extra
            string txtFromActivity = Intent.GetStringExtra("EmployeeSortedList");
            EmployeeListSortedByLastName = Newtonsoft.Json.JsonConvert.DeserializeObject<SortedList<string, Employee>>(txtFromActivity);

            // Create methods
            btnSortedPrev.Click += btnSortedPrev_Click;
            btnSortedNext.Click += btnSortedNext_Click;
            btnSortedFindByID.Click += btnSortedFindByID_Click;
            btnAddEmployee.Click += btnAddEmployee_Click;
            btnUpdateEmployee.Click += btnUpdateEmployee_Click;
            btnRemoveEmployee.Click += btnRemoveEmployee_Click;
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

        private void btnRemoveEmployee_Click(object sender, EventArgs e)
        {
            // Remove at current item
            EmployeeListSortedByLastName.RemoveAt(currentIndex);
            // display success
            txtDisplayError.Text = "Employee has been successfully removed";

        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            Employee u;
            KeyValuePair<string, Employee> keyPair;
            keyPair = EmployeeListSortedByLastName.ElementAt(currentIndex);
            u = EmployeeListSortedByLastName[keyPair.Key];

            // Test to make sure data is correct
            bool resultOp1 = int.TryParse(editSortedEmployeeID.Text, out u.EmployeeID);
            bool resultOp2 = double.TryParse(editSortedHourlyWage.Text, out u.HourlyWage);
            bool resultOp3 = double.TryParse(editSortedHoursWorked.Text, out u.HoursWorked);
            bool resultOp4 = double.TryParse(editSortedTotalPayroll.Text, out u.TotalPayroll);
            u.FirstName = editSortedFirstName.Text;
            u.LastName = editSortedFirstName.Text;

            // Test for hourly wage range
            if (u.HourlyWage >= 9.00 && u.HourlyWage <= 180.00)
            {
                resultOp1 = true;
            }
            else
                resultOp1 = false;
            // Test for hours worked range
            if (u.HoursWorked >= 0 && u.HoursWorked <= 100)
            {
                resultOp2 = true;
            }
            else
                resultOp2 = false;

            if (resultOp1 == true && resultOp2 == true && resultOp3 == true && resultOp4 == true)
            {
                // Test if all feilds are filled out
                if (u.FirstName != null && u.LastName != null)
                {
                    // Update employee
                    txtDisplayError.Text = "Employee has been successfully updated";
                }
                else
                {
                    txtDisplayError.Text = "All input feilds must be filled out";
                }

            }
            else
            {
                txtDisplayError.Text = "Invalid range or missing input: HourlyWage must be 9.00 to 180.00 and HoursWorked 0 to 100";
            }
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            // Create new object
            Employee s = new Employee();
            // Test to make sure data is correct
            bool resultOp1 = int.TryParse(editSortedEmployeeID.Text, out s.EmployeeID);
            bool resultOp2 = double.TryParse(editSortedHourlyWage.Text, out s.HourlyWage);
            bool resultOp3 = double.TryParse(editSortedHoursWorked.Text, out s.HoursWorked);
            bool resultOp4 = double.TryParse(editSortedTotalPayroll.Text, out s.TotalPayroll);
            s.FirstName = editSortedFirstName.Text;
            s.LastName = editSortedFirstName.Text;

            // Test for hourly wage range
            if (s.HourlyWage >= 9.00 && s.HourlyWage <= 180.00)
            {
                resultOp1 = true;
            }
            else
                resultOp1 = false;
            // Test for hours worked range
            if (s.HoursWorked >= 0 && s.HoursWorked <= 100)
            {
                resultOp2 = true;
            }
            else
                resultOp2 = false;

            if (resultOp1 == true && resultOp2 == true && resultOp3 == true && resultOp4 == true)
            {
                // Test if all feilds are filled out
                if(s.FirstName != null && s.LastName != null )
                {
                    // Add employee
                    EmployeeListSortedByLastName.Add(s.LastName, Employee.createEmployee());
                    txtDisplayError.Text = "Employee has been successfully added";
                }
                else
                {
                    txtDisplayError.Text = "All input feilds must be filled out";
                }
          
            }
            else
            {
                txtDisplayError.Text = "Invalid range or missing input: HourlyWage must be 9.00 to 180.00 and HoursWorked 0 to 100";
            }


        }

        private void btnSortedFindByID_Click(object sender, EventArgs e)
        {
            // Get input from editText
            EditText editEmployeeIdSearch = (EditText)FindViewById(Resource.Id.editEmployeeIdSearch);

            // search all records
            // Current index
            int index = 0;
            double compare = 0;
            bool foundItem = false;
            bool resultOp1 = double.TryParse(editEmployeeIdSearch.Text, out compare);


            // Search all for min hours/max
            Employee find;

            // Test to make sure data taken in is valid
            if (resultOp1 == true)
            {
                for (int x = 0; x < 100; x++)
                {
                    KeyValuePair<string, Employee> keyPair;
                    keyPair = EmployeeListSortedByLastName.ElementAt(index);
                    find = EmployeeListSortedByLastName[keyPair.Key];

                    // Test if id matchs the searched item
                    if (find.EmployeeID == compare)
                    {
                        currentIndex = index;
                        foundItem = true;
                    }

                    index++;
                }
                if (foundItem == false)
                {
                    txtDisplayError.Text = "No employee ID found";
                }
                else
                {
                    // go to item
                    ShowEmployeeCurrent(currentIndex);
                }
            }

        }

        private void btnSortedNext_Click(object sender, EventArgs args)
        {
            if (currentIndex >= EmployeeListSortedByLastName.Count - 1)
            {
                txtDisplayError.Text = "No next employee";
            }
            else
            {
                currentIndex++;
                ShowEmployeeCurrent(currentIndex);
                txtDisplayError.Text = "";
            }
        }

        private void btnSortedPrev_Click(object sender, EventArgs args)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                ShowEmployeeCurrent(currentIndex);
                txtDisplayError.Text = "";
            }
            else
            {
                txtDisplayError.Text = "No previous employee";
            }
        }

        protected void ShowEmployeeCurrent(int index)
        {
            Employee e;
            KeyValuePair<string, Employee> keyPair;
            keyPair = EmployeeListSortedByLastName.ElementAt(index);
            e = EmployeeListSortedByLastName[keyPair.Key];

            editSortedEmployeeID.Text = e.EmployeeID.ToString();
            editSortedFirstName.Text = e.FirstName;
            editSortedLastName.Text = e.LastName;
            editSortedHourlyWage.Text = e.HourlyWage.ToString();
            editSortedHoursWorked.Text = e.HoursWorked.ToString();
            editSortedTotalPayroll.Text = e.TotalPayroll.ToString();

        }
    }
}