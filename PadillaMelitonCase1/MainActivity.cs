using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;

namespace PadillaMelitonCase1
{
    [Activity(Label = "PadillaMelitonCase1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        // Test to make sure init is done first before using other options
        int initRan;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Connect layout
            Button btnInitEmployeeSortedList = (Button)FindViewById(Resource.Id.btnInitEmployeeSortedList);
            Button btnModifyEmployeeSortedList = (Button)FindViewById(Resource.Id.btnModifyEmployeeSortedList);
            Button btnEmployeeStatistics = (Button)FindViewById(Resource.Id.btnEmployeeStatistics);

            // Create methods
            btnInitEmployeeSortedList.Click += btnInitEmployeeSortedList_Click;
            btnModifyEmployeeSortedList.Click += btnModifyEmployeeSortedList_Click;
            btnEmployeeStatistics.Click += btnEmployeeStatistics_Click;

            // Grab init data
            initRan = Intent.GetIntExtra("initRan", 0);
        }

        private void btnEmployeeStatistics_Click(object sender, System.EventArgs e)
        {
            // test to make sure array has been init
            ;
            if (initRan == 1)
            {
                // Grab data to send to next activity
                string txtFromActivity = Intent.GetStringExtra("EmployeeSortedList");

                // Send to next activity
                Intent activityIntent = new Intent(this, typeof(Statistics));
                activityIntent.PutExtra("EmployeeSortedList", txtFromActivity);
                StartActivity(activityIntent);
            }
            else
            {
                // Display error
                TextView txtDisplayError = (TextView)FindViewById(Resource.Id.txtDisplayError);
                txtDisplayError.Text = "Need to Initialize Employee SortedList first";
            }
        }

        private void btnModifyEmployeeSortedList_Click(object sender, System.EventArgs e)
        {
            // test to make sure array has been init
            if (initRan == 1)
            {
                // Grab data to send to next activity
                string txtFromActivity = Intent.GetStringExtra("EmployeeSortedList");

                // Send to next activity
                Intent activityIntent = new Intent(this, typeof(ModifyEmployeeList));
                activityIntent.PutExtra("EmployeeSortedList", txtFromActivity);
                StartActivity(activityIntent);
            }
            else
            {
                // Display error
                TextView txtDisplayError = (TextView)FindViewById(Resource.Id.txtDisplayError);
                txtDisplayError.Text = "Need to Initialize Employee SortedList first";
            }
        }

        private void btnInitEmployeeSortedList_Click(object sender, System.EventArgs args)
        {
            // Set flag to true
            initRan = 1;
            Intent activityIntent = new Intent(this, typeof(InitSortedList));
            activityIntent.PutExtra("InitRan", 0);
            StartActivity(activityIntent);
        }
    }
}

