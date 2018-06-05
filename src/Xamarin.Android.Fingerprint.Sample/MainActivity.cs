using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content.PM;
using Android.Views;

namespace Xamarin.Android.Fingerprint.Sample
{
    [Activity(
        Label = "Xamarin.Android.Fingerprint.Sample",
        MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        Theme = "@style/Theme.AppCompat.Light")]
    public class MainActivity : AppCompatActivity, View.IOnClickListener
    {
        public void OnClick(View v) => new FinderprintDialogFragment().Show(SupportFragmentManager, null);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main);
            FindViewById<Button>(Resource.Id.button1).SetOnClickListener(this);
        }
    }
}

