[![Build Status](https://www.bitrise.io/app/aa2cdd4116e4874c/status.svg?token=MEumgtBjN7gHtLRGPeVHiA&branch=master)](https://www.bitrise.io/app/aa2cdd4116e4874c)
[![NuGet](https://img.shields.io/nuget/v/Xamarin.Android.FingerprintAuthenticator.svg?label=NuGet)](https://www.nuget.org/packages/Xamarin.Android.FingerprintAuthenticator/)

Xamarin.Android.FingerprintAuthenticator
===================

<p align="center">
  <img src="https://github.com/jzeferino/Xamarin.Android.FingerprintAuthenticator/blob/master/art/icon.png?raw=true"/>
</p>

Xamarin.Android.FingerprintAuthenticator is a library that allows you to use the fingerprint sensor in a quick and practical way for android Marshmallow and above.

## Demo
<p align="center">
  <img src="https://github.com/jzeferino/Xamarin.Android.FingerprintAuthenticator/blob/master/art/sample.gif?raw=true"/>
</p>

## Usage
(see the [sample](https://github.com/jzeferino/Xamarin.Android.FingerprintAuthenticator/tree/master/src/Xamarin.Android.Fingerprint.Sample) project for a detailed working example)

### Step 1

Install NuGet [package](https://www.nuget.org/packages/Xamarin.Android.FingerprintAuthenticator/).

### Step 2
Add fingerprint permission in manifest.
```xml
<uses-permission android:name="android.permission.USE_FINGERPRINT" />
```
### Step 3

In your Activity or Dialog implement the interface `FingerprintAuthenticator.IFingerprintAuthenticatorCallbacks`  :
```c#

public class FinderprintDialogFragment : AppCompatDialogFragment, FingerprintAuthenticator.IFingerprintAuthenticatorCallbacks 
{

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
```

### Step 4
Create an instance of FingerprintAuthenticator:
```c#
_fingerprintAuthenticator = new FingerprintAuthenticator(Activity, this);
```

### Step 5
Start and stop listening for fingerprint sensor:
```c#
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
```
### Compatibility 

Since Fingerprint support was added in Android 6.0 (Marshmallow) this library only works in Android 6.0 and above.

### Documentation
[Documentation](https://github.com/jzeferino/Xamarin.Android.FingerprintAuthenticator/wiki/Documentation) 

### License
[MIT Licence](LICENSE) 
