//
// IFingerprintAuthenticatorCallbacks.cs
//
// Author:
//  jzeferino jorgevalentzeferino@gmail.com
//
// Copyright 2017 jzeferino
//

namespace Xamarin.Android.Fingerprint
{
    partial class FingerprintAuthenticator
    {
        /// <summary>
        /// Fingerprint authenticator callbacks.
        /// </summary>
        public interface IFingerprintAuthenticatorCallbacks
        {
            /// <summary>
            /// Called when no fingerprint hardware available or unsuported OS.
            /// </summary>
            void FingerprintNotSupported();

            /// <summary>
            /// Callend when no fingerprints are registered in the system.
            /// </summary>
            void FingerprintsNotEnrolled();

            /// <summary>
            /// Called on authentication sucess.
            /// </summary>
            void AuthenticationSucceded();

            /// <summary>
            /// Called when a fingerprint is valid but not recognized.
            /// </summary>
            void FingerprintNotRecognized();

            /// <summary>
            /// Called when an unrecoverable error has been encountered and the operation is complete.
            /// No further callbacks will be made on this object.
            /// </summary>
            /// <param name="errorCode">Error code.</param>
            /// <param name="humanReadableMessage">Human readable message.</param>
            void AuthenticationError(AuthenticationErrorCodes errorCode, string humanReadableMessage);

            /// <summary>
            /// Called when a recoverable error has been encountered during authentication.
            /// The help string is provided to give the user guidance for what went wrong, such as "Sensor dirty, please clean it."
            /// </summary>
            /// <param name="helpCode">Help code.</param>
            /// <param name="humanReadableMessage">Human readable message.</param>
            void AuthenticationHelp(AuthenticationHelpCodes helpCode, string humanReadableMessage);
        }
    }

}
