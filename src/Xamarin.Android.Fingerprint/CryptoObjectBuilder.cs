//
// CryptoObjectBuilder.cs
//
// Author:
//  jzeferino jorgevalentzeferino@gmail.com
//
// Copyright 2017 jzeferino
//
using Android.Security.Keystore;
using Android.Support.V4.Hardware.Fingerprint;
using Java.Security;
using Javax.Crypto;

namespace Xamarin.Android.Fingerprint
{
    /// <summary>
    /// This class encapsulates the creation of a CryptoObject based on a javax.crypto.Cipher.
    /// </summary>
    /// <remarks>Each invocation of BuildCryptoObject will instantiate a new CryptoObjet. </remarks>
    internal class CryptoObjectBuilder
    {
        private readonly string KeyName = typeof(CryptoObjectBuilder).Name;
        private const string AndroidKeystore = "AndroidKeyStore";
        private const string KeyAlgorithm = KeyProperties.KeyAlgorithmAes;
        private const string BlockMode = KeyProperties.BlockModeCbc;
        private const string EcryptionPadding = KeyProperties.EncryptionPaddingPkcs7;

        private const string Transformation = KeyAlgorithm + "/" +
                                                BlockMode + "/" +
                                                EcryptionPadding;

        private readonly KeyStore _keystore;

        private CryptoObjectBuilder()
        {
            _keystore = KeyStore.GetInstance(AndroidKeystore);
            _keystore.Load(null);
        }

        public static FingerprintManagerCompat.CryptoObject Build()
        {
            return new FingerprintManagerCompat.CryptoObject(new CryptoObjectBuilder().CreateCipher());
        }

        /// <summary>
        ///     Creates the cipher.
        /// </summary>
        /// <returns>The cipher.</returns>
        /// <param name="retry">If set to <c>true</c>, recreate the key and try again.</param>
        private Cipher CreateCipher(bool retry = true)
        {
            var key = CreateKey();
            var cipher = Cipher.GetInstance(Transformation);
            cipher.Init(CipherMode.EncryptMode, key);

            return cipher;
        }

        /// <summary>
        ///     Creates the Key for fingerprint authentication.
        /// </summary>
        private IKey CreateKey()
        {
            var keyGen = KeyGenerator.GetInstance(KeyProperties.KeyAlgorithmAes, AndroidKeystore);
            keyGen.Init(new KeyGenParameterSpec.Builder(KeyName, KeyStorePurpose.Encrypt | KeyStorePurpose.Decrypt)
                    .SetBlockModes(BlockMode)
                    .SetEncryptionPaddings(EcryptionPadding)
                    .SetUserAuthenticationRequired(true)
                    .Build());
            return keyGen.GenerateKey();
        }
    }
}
