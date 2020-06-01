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
    public partial class PostDetailPage : ContentPage
    {
        Post selectedPost;
        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();
            this.selectedPost = selectedPost;
            experienceLabel.Text = selectedPost.Experience;
        }

        private void updateButton_Clicked(object sender, EventArgs e)
        {
            selectedPost.Experience = experienceLabel.Text;

            using (SQLiteConnection connect = new SQLiteConnection(App.DatabaseLocation))
            {
                connect.CreateTable<Post>();
                int rows = connect.Update(selectedPost);

                if (rows > 0)
                    DisplayAlert("Succes", "Experience succesfully updated", "Ok");
                else
                    DisplayAlert("Failure", "Experience failed to be updated", "Ok");
            };
        }

        private void deleteButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection connect = new SQLiteConnection(App.DatabaseLocation))
            {
                connect.CreateTable<Post>();
                int rows = connect.Delete(selectedPost);

                if (rows > 0)
                    DisplayAlert("Succes", "Experience succesfully deleted", "Ok");
                else
                    DisplayAlert("Failure", "Experience failed to be deleted", "Ok");
            };
        }
    }
}