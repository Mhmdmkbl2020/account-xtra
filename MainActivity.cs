using Android.App;
using Android.OS;
using Android.Widget;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DirectSQLConnectionApp
{
    [Activity(Label = "SQL Server App", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private string connectionString = "Data Source=192.168.1.2,1477;Initial Catalog=ALALANMAJDID2024;User ID=sa;Password=xtra2020;";
        private ListView listView;
        private Button fetchButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            listView = FindViewById<ListView>(Resource.Id.listView);
            fetchButton = FindViewById<Button>(Resource.Id.fetchButton);

            fetchButton.Click += FetchTables;
        }

        private void FetchTables(object sender, System.EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'", connection);
                    SqlDataReader reader = command.ExecuteReader();

                    var tableNames = new List<string>();
                    while (reader.Read())
                    {
                        tableNames.Add(reader["TABLE_NAME"].ToString());
                    }

                    listView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, tableNames);
                }
            }
            catch (SqlException ex)
            {
                Toast.MakeText(this, $"Database Error: {ex.Message}", ToastLength.Long).Show();
            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, $"Error: {ex.Message}", ToastLength.Long).Show();
            }
        }
    }
}
