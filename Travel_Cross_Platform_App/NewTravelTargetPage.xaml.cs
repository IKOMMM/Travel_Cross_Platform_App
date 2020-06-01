using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Cross_Platform_App.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace Travel_Cross_Platform_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelTargetPage : ContentPage
    {
        public NewTravelTargetPage()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Post post = new Post()
            {
                Experience = experienceEntry.Text
            };

            using (SQLiteConnection connect = new SQLiteConnection(App.DatabaseLocation)) {
                connect.CreateTable<Post>();
                int rows = connect.Insert(post);

                if (rows > 0)
                    DisplayAlert("Succes", "Experience succesfully insertded", "Ok");
                else
                    DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
            };                

           
        }
    }
}