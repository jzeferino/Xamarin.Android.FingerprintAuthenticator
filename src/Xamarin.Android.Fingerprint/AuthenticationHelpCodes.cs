//
// AuthenticationHelpCodes.cs
//
// Author:
//  jzeferino jorgevalentzeferino@gmail.com
//
// Copyright 2017 jzeferino
//
namespace Xamarin.Android.Fingerprint
{
    // From documentation https://developer.android.com/reference/android/hardware/fingerprint/FingerprintManager.html#FINGERPRINT_ACQUIRED_GOOD
    /// <summary>
    /// Authentication help codes.
    /// </summary>
    public enum AuthenticationHelpCodes
    {
        /// <summary>
        /// The image acquired was good.
        /// </summary>
        AcquiredGood = 0,

        /// <summary>
        /// Only a partial fingerprint image was detected. During enrollment, 
        /// the user should be informed on what needs to happen to resolve this problem, e.g. "press firmly on sensor."
        /// </summary>
        AcquiredPartial = 1,

        /// <summary>
        /// The fingerprint image was too noisy to process due to a detected
        /// condition (i.e. dry skin) or a possibly dirty sensor <see cref="AcquiredImagerDirty"/>.
        /// </summary>
        AcquiredInsufficient = 2,

        /// <summary>
        /// The fingerprint image was too noisy due to suspected or detected dirt on the sensor.
        /// For example, it's reasonable return this after multiple <see cref="AcquiredInsufficient"/>
        /// or actual detection of dirt on the sensor (stuck pixels, swaths, etc.).
        /// The user is expected to take action to clean the sensor when this is returned.
        /// </summary>
        AcquiredImagerDirty = 3,

        /// <summary>
        /// The fingerprint image was unreadable due to lack of motion.
        /// This is most appropriate for linear array sensors that require a swipe motion.
        /// </summary>
        AcquiredTooSlow = 4,

        /// <summary>
        /// The fingerprint image was incomplete due to quick motion. While mostly appropriate for linear array sensors,
        ///  this could also happen if the finger was moved during acquisition.
        /// The user should be asked to move the finger slower (linear) or leave the finger on the sensor longer.
        /// </summary>
        AcquiredTooFast = 5,
    }
}
