//
// AuthenticationErrorCodes.cs
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
    /// Authentication error codes.
    /// </summary>
    public enum AuthenticationErrorCodes
    {
        /// <summary>
        /// The hardware is unavailable. Try again later.
        /// </summary>
        ErrorHwUnavailable = 1,

        /// <summary>
        /// Error state returned when the sensor was unable to process the current image.
        /// </summary>
        ErrorUnableToProcess = 2,

        /// <summary>
        /// Error state returned when the current request has been running too long.
        /// This is intended to prevent programs from waiting for the fingerprint sensor indefinitely.
        /// The timeout is platform and sensor-specific, but is generally on the order of 30 seconds.
        /// </summary>
        ErrorTimeout = 3,

        /// <summary>
        /// Error state returned for operations like enrollment;
        /// the operation cannot be completed because there's not enough storage remaining to complete the operation.
        /// </summary>
        ErrorNoSpace = 4,

        /// <summary>
        /// The operation was canceled because the fingerprint sensor is unavailable.
        /// For example, this may happen when the user is switched, the device is locked or another pending operation prevents or disables it.
        /// </summary>
        ErrorCanceled = 5,

        /// <summary>
        /// The operation was canceled because the API is locked out due to too many attempts.
        /// </summary>
        ErrorLockout = 7,

        /// <summary>
        /// Unexpected error.
        /// </summary>
        Unexpected = 8
    }
}
