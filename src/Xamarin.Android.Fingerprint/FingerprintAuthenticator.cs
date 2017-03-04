//
// FingerprintAuthenticator.cs
//
// Author:
//  jzeferino jorgevalentzeferino@gmail.com
//
// Copyright 2017 jzeferino
//
using System;
using Android.Content;
using Android.Support.V4.Hardware.Fingerprint;
using Android.Hardware.Fingerprints;
using CancellationSignal = Android.Support.V4.OS.CancellationSignal;

namespace Xamarin.Android.Fingerprint
{
    /// <summary>
    /// Fingerprint authenticator.
    /// </summary>
    public sealed partial class FingerprintAuthenticator
    {
        private FingerprintManagerCompat _fingerprintManager;
        private IFingerprintAuthenticatorCallbacks _authenticationCallback;
        private CancellationSignal mCancellationSignal;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Xamarin.Android.Fingerprint.FingerprintAuthenticator"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="callbacks">Callbacks.</param>
        public FingerprintAuthenticator(Context context, IFingerprintAuthenticatorCallbacks callbacks)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (callbacks == null)
            {
                throw new ArgumentNullException(nameof(callbacks));
            }

            _authenticationCallback = callbacks;
            _fingerprintManager = FingerprintManagerCompat.From(context);
        }

        /// <summary>
        /// Check if fingerprint is available.
        /// </summary>
        /// <returns><c>true</c>, if fingerprint available, <c>false</c> otherwise.</returns>
        /// <param name="context">Context.</param>
        public static bool IsFingerprintAvailable(Context context)
        {
            var fingerprintManager = FingerprintManagerCompat.From(context);
            return fingerprintManager.HasEnrolledFingerprints && fingerprintManager.IsHardwareDetected;
        }

        /// <summary>
        /// Check the fingerprint is available.
        /// </summary>
        /// <returns><c>true</c>, if fingerprint is available, <c>false</c> otherwise.</returns>
        private bool InternalIsFingerprintAvailable()
        {
            // As said in documentation
            // If we are bellow android M this returns true.
            if (!_fingerprintManager.IsHardwareDetected)
            {
                _authenticationCallback.FingerprintNotSupported();
                return false;
            }
            if (!_fingerprintManager.HasEnrolledFingerprints)
            {
                _authenticationCallback.FingerprintsNotEnrolled();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Starts the authentication.
        /// </summary>
        public void StartAuthentication()
        {
            // Stop any previous scan.
            StopAuthentication();

            if (!InternalIsFingerprintAvailable())
            {
                return;
            }

            try
            {
                mCancellationSignal = new CancellationSignal();
                _fingerprintManager.Authenticate(CryptoObjectBuilder.Build(),
                                                (int)FingerprintAuthenticationFlags.None,
                                                mCancellationSignal,
                                                new AuthenticationCallbacks(this),
                                                null);
            }
            catch (Exception ex)
            {
                _authenticationCallback.AuthenticationError(AuthenticationErrorCodes.Unexpected, ex.Message);
            }
        }

        internal class AuthenticationCallbacks : FingerprintManagerCompat.AuthenticationCallback
        {
            private FingerprintAuthenticator _fingerprintAuthenticator;

            public AuthenticationCallbacks(FingerprintAuthenticator fingerprintAuthenticator)
            {
                _fingerprintAuthenticator = fingerprintAuthenticator;
            }

            public override void OnAuthenticationError(int errMsgId, Java.Lang.ICharSequence errString)
            {
                base.OnAuthenticationError(errMsgId, errString);

                var error = (AuthenticationErrorCodes)errMsgId;
                if (error != AuthenticationErrorCodes.ErrorCanceled)
                {
                    _fingerprintAuthenticator._authenticationCallback.AuthenticationError(error, errString.ToString());
                }
            }

            public override void OnAuthenticationHelp(int helpMsgId, Java.Lang.ICharSequence helpString)
            {
                base.OnAuthenticationHelp(helpMsgId, helpString);
                _fingerprintAuthenticator._authenticationCallback.AuthenticationHelp((AuthenticationHelpCodes)helpMsgId, helpString.ToString());
            }

            public override void OnAuthenticationFailed()
            {
                base.OnAuthenticationFailed();
                _fingerprintAuthenticator._authenticationCallback.FingerprintNotRecognized();
            }

            public override void OnAuthenticationSucceeded(FingerprintManagerCompat.AuthenticationResult result)
            {
                try
                {
                    // Calling DoFinal on the Cipher ensures that the encryption worked.
                    result.CryptoObject.Cipher.DoFinal(new byte[] { 1, 2, 3 });
                    _fingerprintAuthenticator._authenticationCallback.AuthenticationSucceded();
                }
                catch (Exception ex)
                {
                    _fingerprintAuthenticator._authenticationCallback.AuthenticationError(AuthenticationErrorCodes.Unexpected, ex.Message);
                }
            }
        }

        /// <summary>
        /// Stops the authentication.
        /// </summary>
        public void StopAuthentication()
        {
            if (mCancellationSignal != null)
            {
                mCancellationSignal.Cancel();
                mCancellationSignal = null;
            }
        }
    }
}

