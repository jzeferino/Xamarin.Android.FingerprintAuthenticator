[![Build status](https://ci.appveyor.com/api/projects/status/nfel4y6r45wwnggt?svg=true)](https://ci.appveyor.com/project/jzeferino/xamarin-android-fingerprint/)   [![NuGet](https://img.shields.io/nuget/v/Xamarin.Android.FingerprintAuthenticator.svg?label=NuGet)](https://www.nuget.org/packages/Xamarin.Android.FingerprintAuthenticator/)

Xamarin.Android.FingerprintAuthenticator
===================

<p align="center">
  <img src="https://github.com/jzeferino/Xamarin.Android.FingerprintAuthenticator/blob/master/art/icon.png?raw=true"/>
</p>

Xamarin.Android.FingerprintAuthenticator is a library that allows you to use the fingerprint sensor in a quick and practical way for android marshmallow and above.

## Demo
<p align="center">
  <img src="https://github.com/jzeferino/Xamarin.Android.FingerprintAuthenticator/blob/master/art/sample.gif?raw=true"/>
</p>

## Usage
(see the [sample](https://github.com/jzeferino/Xamarin.Android.FingerprintAuthenticator/tree/master/src/Xamarin.Android.Fingerprint.Sample) project for a detailed working example)

### Step 1

Install NuGet [package](https://www.nuget.org/packages/Xamarin.Android.FingerprintAuthenticator/).

### Step 2

In your activity or dialog implement the interface `FingerprintAuthenticator.IFingerprintAuthenticatorCallbacks`  :
```c#
public class FinderprintDialogFragment : AppCompatDialogFragment, FingerprintAuthenticator.IFingerprintAuthenticatorCallbacks
```

### Step 3

Implement the callbacks
```c#
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
```

### Step 4
Create instance of FingerprintAuthenticator:
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

### License
[MIT Licence](LICENSE) 
