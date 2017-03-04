//
// FinderprintDialogFragment.cs
//
// Author:
//  jzeferino jorgevalentzeferino@gmail.com
//
// Copyright 2017 jzeferino
//
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AlertDialog = Android.Support.V7.App.AlertDialog;

namespace Xamarin.Android.Fingerprint.Sample
{
    public class FinderprintDialogFragment : AppCompatDialogFragment, FingerprintAuthenticator.IFingerprintAuthenticatorCallbacks
    {
        private TextView _fingerprintStatus;
        private ImageView _imgFingerprintStatus;
        private FingerprintAuthenticator _fingerprintAuthenticator;

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            _fingerprintAuthenticator = new FingerprintAuthenticator(Activity, this);

            View v = Activity.LayoutInflater.Inflate(Resource.Layout.fingerprintdialog, null);
            _fingerprintStatus = v.FindViewById<TextView>(Resource.Id.fingerprint_status);
            _imgFingerprintStatus = v.FindViewById<ImageView>(Resource.Id.img_fingerprint_status);

            var dialog = new AlertDialog.Builder(Activity)

                .SetCancelable(false)
                .SetTitle("Sign in")
                .SetNegativeButton("Cancel", (sender, e) => Dismiss())
                .SetView(v)
                .Create();

            dialog.SetCanceledOnTouchOutside(false);

            return dialog;
        }

        public override void OnResume()
        {
            base.OnResume();
            _fingerprintAuthenticator.StartAuthentication();
        }

        public override void OnPause()
        {
            base.OnPause();
            _fingerprintAuthenticator.StopAuthentication();
        }

        public void FingerprintNotSupported() => UpdateUI("Fingerprint not supported.", Resource.Drawable.ic_info_black);

        public void FingerprintsNotEnrolled() => UpdateUI("No fingerprints registered.", Resource.Drawable.ic_info_black);

        public void AuthenticationSucceded() => UpdateUI("Authenticaticated.", Resource.Drawable.ic_check_circle_black);

        public void FingerprintNotRecognized() => UpdateUI("Wrong fingerprint. Please try again.", Resource.Drawable.ic_info_black);

        public void AuthenticationError(AuthenticationErrorCodes errorCode, string humanReadMessage) => UpdateUI($"{errorCode.ToString()} {humanReadMessage}", Resource.Drawable.ic_info_black);

        public void AuthenticationHelp(AuthenticationHelpCodes helpCode, string humanReadMessage) => UpdateUI($"{helpCode.ToString()} {humanReadMessage}", Resource.Drawable.ic_info_black);

        private void UpdateUI(string text, int imageResource = Resource.Drawable.ic_fingerprint_black)
        {
            _fingerprintStatus.Text = text;
            _imgFingerprintStatus.SetImageResource(imageResource);
        }
    }
}