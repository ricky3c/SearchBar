using Android.App;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.OS;
using Android.Widget;

namespace SearchBar
{
	[Activity(Label = "SearchView Ejemplo", MainLauncher = true, Icon = "@drawable/icon",
		Theme = "@style/Theme.AppCompat.Light")]
	public class SearchViewActivity : ActionBarActivity
	{
		private Android.Support.V7.Widget.SearchView _searchView;
		private ListView _listView;
		private ArrayAdapter _adapter;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			var Names = new[]
			{
				"Ricardo Romo Gonzalez","Alejandro Ruiz Varela","Christian Salvador Arenas",
				"Armando Balleza","Jorge Vazquez","Enrique Torres","Angel Alberto Mejia","Juan Manuel Perez",
				"Ruben Perez","Pedro Navajas","Jose Alfredo Jimenez","Antonio Aguilar"
			};

			_listView = FindViewById<ListView>(Resource.Id.listView);
			_adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Names);
			_listView.Adapter = _adapter;
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.main, menu);

			var item = menu.FindItem(Resource.Id.action_search);
			MenuItemCompat.SetOnActionExpandListener(item, new SearchViewExpandListener(_adapter));

			var searchItem = MenuItemCompat.GetActionView(item);
			_searchView = searchItem.JavaCast<Android.Support.V7.Widget.SearchView>();

			_searchView.QueryTextChange += (s, e) => _adapter.Filter.InvokeFilter(e.NewText);

			_searchView.QueryTextSubmit += (s, e) =>
			{
				//TODO: Do something fancy when search button on keyboard is pressed
				Toast.MakeText(this, "Searched for: " + e.Query, ToastLength.Short).Show();
				e.Handled = true;
			};

			return true;
		}
	}
}