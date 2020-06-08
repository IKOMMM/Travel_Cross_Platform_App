using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Travel_Cross_Platform_App.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travel_Cross_Platform_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            using(SQLiteConnection connect = new SQLiteConnection(App.DatabaseLocation))
            {
                //Counting Posts 
                var postTable = connect.Table<Post>().ToList();

                //Linq to post posts into ProfilePage
                var categories = (from p in postTable
                                  orderby p.CategoryID
                                  select p.CategoryName).Distinct().ToList();

                Dictionary<string, int> categoriesCount = new Dictionary<string, int>();
                foreach(var category in categories)
                {
                    var count = (from post in postTable
                                 where post.CategoryName == category
                                 select post).ToList().Count;

                    categoriesCount.Add(category, count);
                }

                categoryList.ItemsSource = categoriesCount;


                postCounter.Text = postTable.Count.ToString();
            }
        }
    }
}