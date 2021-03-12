﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x0200027F RID: 639
	[ComVisible(true)]
	public sealed class RSACryptoServiceProvider : RSA, ICspAsymmetricAlgorithm
	{
		// Token: 0x060022A3 RID: 8867
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void DecryptKey(SafeKeyHandle pKeyContext, [MarshalAs(UnmanagedType.LPArray)] byte[] pbEncryptedKey, int cbEncryptedKey, [MarshalAs(UnmanagedType.Bool)] bool fOAEP, ObjectHandleOnStack ohRetDecryptedKey);

		// Token: 0x060022A4 RID: 8868
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void EncryptKey(SafeKeyHandle pKeyContext, [MarshalAs(UnmanagedType.LPArray)] byte[] pbKey, int cbKey, [MarshalAs(UnmanagedType.Bool)] bool fOAEP, ObjectHandleOnStack ohRetEncryptedKey);

		// Token: 0x060022A5 RID: 8869 RVA: 0x0007C75D File Offset: 0x0007A95D
		[SecuritySafeCritical]
		public RSACryptoServiceProvider() : this(0, new CspParameters(24, null, null, RSACryptoServiceProvider.s_UseMachineKeyStore), true)
		{
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0007C777 File Offset: 0x0007A977
		[SecuritySafeCritical]
		public RSACryptoServiceProvider(int dwKeySize) : this(dwKeySize, new CspParameters(24, null, null, RSACryptoServiceProvider.s_UseMachineKeyStore), false)
		{
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x0007C791 File Offset: 0x0007A991
		[SecuritySafeCritical]
		public RSACryptoServiceProvider(CspParameters parameters) : this(0, parameters, true)
		{
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x0007C79C File Offset: 0x0007A99C
		[SecuritySafeCritical]
		public RSACryptoServiceProvider(int dwKeySize, CspParameters parameters) : this(dwKeySize, parameters, false)
		{
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0007C7A8 File Offset: 0x0007A9A8
		[SecurityCritical]
		private RSACryptoServiceProvider(int dwKeySize, CspParameters parameters, bool useDefaultKeySize)
		{
			if (dwKeySize < 0)
			{
				throw new ArgumentOutOfRangeException("dwKeySize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this._parameters = Utils.SaveCspParameters(CspAlgorithmType.Rsa, parameters, RSACryptoServiceProvider.s_UseMachineKeyStore, ref this._randomKeyContainer);
			this.LegalKeySizesValue = new KeySizes[]
			{
				new KeySizes(384, 16384, 8)
			};
			this._dwKeySize = (useDefaultKeySize ? 1024 : dwKeySize);
			if (!this._randomKeyContainer || Environment.GetCompatibilityFlag(CompatibilityFlag.EagerlyGenerateRandomAsymmKeys))
			{
				this.GetKeyPair();
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0007C834 File Offset: 0x0007AA34
		[SecurityCritical]
		private void GetKeyPair()
		{
			if (this._safeKeyHandle == null)
			{
				lock (this)
				{
					if (this._safeKeyHandle == null)
					{
						Utils.GetKeyPairHelper(CspAlgorithmType.Rsa, this._parameters, this._randomKeyContainer, this._dwKeySize, ref this._safeProvHandle, ref this._safeKeyHandle);
					}
				}
			}
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0007C8A0 File Offset: 0x0007AAA0
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
			{
				this._safeKeyHandle.Dispose();
			}
			if (this._safeProvHandle != null && !this._safeProvHandle.IsClosed)
			{
				this._safeProvHandle.Dispose();
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x0007C8F4 File Offset: 0x0007AAF4
		[ComVisible(false)]
		public bool PublicOnly
		{
			[SecuritySafeCritical]
			get
			{
				this.GetKeyPair();
				byte[] array = Utils._GetKeyParameter(this._safeKeyHandle, 2U);
				return array[0] == 1;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x0007C91A File Offset: 0x0007AB1A
		[ComVisible(false)]
		public CspKeyContainerInfo CspKeyContainerInfo
		{
			[SecuritySafeCritical]
			get
			{
				this.GetKeyPair();
				return new CspKeyContainerInfo(this._parameters, this._randomKeyContainer);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x0007C934 File Offset: 0x0007AB34
		public override int KeySize
		{
			[SecuritySafeCritical]
			get
			{
				this.GetKeyPair();
				byte[] array = Utils._GetKeyParameter(this._safeKeyHandle, 1U);
				this._dwKeySize = ((int)array[0] | (int)array[1] << 8 | (int)array[2] << 16 | (int)array[3] << 24);
				return this._dwKeySize;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x0007C977 File Offset: 0x0007AB77
		public override string KeyExchangeAlgorithm
		{
			get
			{
				if (this._parameters.KeyNumber == 1)
				{
					return "RSA-PKCS1-KeyEx";
				}
				return null;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x0007C98E File Offset: 0x0007AB8E
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x0007C995 File Offset: 0x0007AB95
		// (set) Token: 0x060022B2 RID: 8882 RVA: 0x0007C9A1 File Offset: 0x0007ABA1
		public static bool UseMachineKeyStore
		{
			get
			{
				return RSACryptoServiceProvider.s_UseMachineKeyStore == CspProviderFlags.UseMachineKeyStore;
			}
			set
			{
				RSACryptoServiceProvider.s_UseMachineKeyStore = (value ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x0007C9B4 File Offset: 0x0007ABB4
		// (set) Token: 0x060022B4 RID: 8884 RVA: 0x0007CA1C File Offset: 0x0007AC1C
		public bool PersistKeyInCsp
		{
			[SecuritySafeCritical]
			get
			{
				if (this._safeProvHandle == null)
				{
					lock (this)
					{
						if (this._safeProvHandle == null)
						{
							this._safeProvHandle = Utils.CreateProvHandle(this._parameters, this._randomKeyContainer);
						}
					}
				}
				return Utils.GetPersistKeyInCsp(this._safeProvHandle);
			}
			[SecuritySafeCritical]
			set
			{
				bool persistKeyInCsp = this.PersistKeyInCsp;
				if (value == persistKeyInCsp)
				{
					return;
				}
				if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
				{
					KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
					if (!value)
					{
						KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Delete);
						keyContainerPermission.AccessEntries.Add(accessEntry);
					}
					else
					{
						KeyContainerPermissionAccessEntry accessEntry2 = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Create);
						keyContainerPermission.AccessEntries.Add(accessEntry2);
					}
					keyContainerPermission.Demand();
				}
				Utils.SetPersistKeyInCsp(this._safeProvHandle, value);
			}
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x0007CA90 File Offset: 0x0007AC90
		[SecuritySafeCritical]
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			this.GetKeyPair();
			if (includePrivateParameters && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Export);
				keyContainerPermission.AccessEntries.Add(accessEntry);
				keyContainerPermission.Demand();
			}
			RSACspObject rsacspObject = new RSACspObject();
			int blobType = includePrivateParameters ? 7 : 6;
			Utils._ExportKey(this._safeKeyHandle, blobType, rsacspObject);
			return RSACryptoServiceProvider.RSAObjectToStruct(rsacspObject);
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x0007CAF6 File Offset: 0x0007ACF6
		[SecuritySafeCritical]
		[ComVisible(false)]
		public byte[] ExportCspBlob(bool includePrivateParameters)
		{
			this.GetKeyPair();
			return Utils.ExportCspBlobHelper(includePrivateParameters, this._parameters, this._safeKeyHandle);
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0007CB10 File Offset: 0x0007AD10
		[SecuritySafeCritical]
		public override void ImportParameters(RSAParameters parameters)
		{
			if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
			{
				this._safeKeyHandle.Dispose();
				this._safeKeyHandle = null;
			}
			RSACspObject cspObject = RSACryptoServiceProvider.RSAStructToObject(parameters);
			this._safeKeyHandle = SafeKeyHandle.InvalidHandle;
			if (RSACryptoServiceProvider.IsPublic(parameters))
			{
				Utils._ImportKey(Utils.StaticProvHandle, 41984, CspProviderFlags.NoFlags, cspObject, ref this._safeKeyHandle);
				return;
			}
			if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Import);
				keyContainerPermission.AccessEntries.Add(accessEntry);
				keyContainerPermission.Demand();
			}
			if (this._safeProvHandle == null)
			{
				this._safeProvHandle = Utils.CreateProvHandle(this._parameters, this._randomKeyContainer);
			}
			Utils._ImportKey(this._safeProvHandle, 41984, this._parameters.Flags, cspObject, ref this._safeKeyHandle);
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0007CBE6 File Offset: 0x0007ADE6
		[SecuritySafeCritical]
		[ComVisible(false)]
		public void ImportCspBlob(byte[] keyBlob)
		{
			Utils.ImportCspBlobHelper(CspAlgorithmType.Rsa, keyBlob, RSACryptoServiceProvider.IsPublic(keyBlob), ref this._parameters, this._randomKeyContainer, ref this._safeProvHandle, ref this._safeKeyHandle);
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x0007CC10 File Offset: 0x0007AE10
		public byte[] SignData(Stream inputStream, object halg)
		{
			int calgHash = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
			HashAlgorithm hashAlgorithm = Utils.ObjToHashAlgorithm(halg);
			byte[] rgbHash = hashAlgorithm.ComputeHash(inputStream);
			return this.SignHash(rgbHash, calgHash);
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0007CC3C File Offset: 0x0007AE3C
		public byte[] SignData(byte[] buffer, object halg)
		{
			int calgHash = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
			HashAlgorithm hashAlgorithm = Utils.ObjToHashAlgorithm(halg);
			byte[] rgbHash = hashAlgorithm.ComputeHash(buffer);
			return this.SignHash(rgbHash, calgHash);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0007CC68 File Offset: 0x0007AE68
		public byte[] SignData(byte[] buffer, int offset, int count, object halg)
		{
			int calgHash = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
			HashAlgorithm hashAlgorithm = Utils.ObjToHashAlgorithm(halg);
			byte[] rgbHash = hashAlgorithm.ComputeHash(buffer, offset, count);
			return this.SignHash(rgbHash, calgHash);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0007CC98 File Offset: 0x0007AE98
		public bool VerifyData(byte[] buffer, object halg, byte[] signature)
		{
			int calgHash = Utils.ObjToAlgId(halg, OidGroup.HashAlgorithm);
			HashAlgorithm hashAlgorithm = Utils.ObjToHashAlgorithm(halg);
			byte[] rgbHash = hashAlgorithm.ComputeHash(buffer);
			return this.VerifyHash(rgbHash, calgHash, signature);
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x0007CCC8 File Offset: 0x0007AEC8
		public byte[] SignHash(byte[] rgbHash, string str)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (this.PublicOnly)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NoPrivateKey"));
			}
			int calgHash = X509Utils.NameOrOidToAlgId(str, OidGroup.HashAlgorithm);
			return this.SignHash(rgbHash, calgHash);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x0007CD0C File Offset: 0x0007AF0C
		[SecuritySafeCritical]
		internal byte[] SignHash(byte[] rgbHash, int calgHash)
		{
			this.GetKeyPair();
			if (!this.CspKeyContainerInfo.RandomlyGenerated && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Sign);
				keyContainerPermission.AccessEntries.Add(accessEntry);
				keyContainerPermission.Demand();
			}
			return Utils.SignValue(this._safeKeyHandle, this._parameters.KeyNumber, 9216, calgHash, rgbHash);
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x0007CD7C File Offset: 0x0007AF7C
		public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			int calgHash = X509Utils.NameOrOidToAlgId(str, OidGroup.HashAlgorithm);
			return this.VerifyHash(rgbHash, calgHash, rgbSignature);
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x0007CDB6 File Offset: 0x0007AFB6
		[SecuritySafeCritical]
		internal bool VerifyHash(byte[] rgbHash, int calgHash, byte[] rgbSignature)
		{
			this.GetKeyPair();
			return Utils.VerifySign(this._safeKeyHandle, 9216, calgHash, rgbHash, rgbSignature);
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0007CDD4 File Offset: 0x0007AFD4
		[SecuritySafeCritical]
		public byte[] Encrypt(byte[] rgb, bool fOAEP)
		{
			if (rgb == null)
			{
				throw new ArgumentNullException("rgb");
			}
			this.GetKeyPair();
			byte[] result = null;
			RSACryptoServiceProvider.EncryptKey(this._safeKeyHandle, rgb, rgb.Length, fOAEP, JitHelpers.GetObjectHandleOnStack<byte[]>(ref result));
			return result;
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0007CE10 File Offset: 0x0007B010
		[SecuritySafeCritical]
		public byte[] Decrypt(byte[] rgb, bool fOAEP)
		{
			if (rgb == null)
			{
				throw new ArgumentNullException("rgb");
			}
			this.GetKeyPair();
			if (rgb.Length > this.KeySize / 8)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_Padding_DecDataTooBig", new object[]
				{
					this.KeySize / 8
				}));
			}
			if (!this.CspKeyContainerInfo.RandomlyGenerated && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Decrypt);
				keyContainerPermission.AccessEntries.Add(accessEntry);
				keyContainerPermission.Demand();
			}
			byte[] result = null;
			RSACryptoServiceProvider.DecryptKey(this._safeKeyHandle, rgb, rgb.Length, fOAEP, JitHelpers.GetObjectHandleOnStack<byte[]>(ref result));
			return result;
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0007CEBD File Offset: 0x0007B0BD
		public override byte[] DecryptValue(byte[] rgb)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0007CECE File Offset: 0x0007B0CE
		public override byte[] EncryptValue(byte[] rgb)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0007CEE0 File Offset: 0x0007B0E0
		private static RSAParameters RSAObjectToStruct(RSACspObject rsaCspObject)
		{
			return new RSAParameters
			{
				Exponent = rsaCspObject.Exponent,
				Modulus = rsaCspObject.Modulus,
				P = rsaCspObject.P,
				Q = rsaCspObject.Q,
				DP = rsaCspObject.DP,
				DQ = rsaCspObject.DQ,
				InverseQ = rsaCspObject.InverseQ,
				D = rsaCspObject.D
			};
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x0007CF60 File Offset: 0x0007B160
		private static RSACspObject RSAStructToObject(RSAParameters rsaParams)
		{
			return new RSACspObject
			{
				Exponent = rsaParams.Exponent,
				Modulus = rsaParams.Modulus,
				P = rsaParams.P,
				Q = rsaParams.Q,
				DP = rsaParams.DP,
				DQ = rsaParams.DQ,
				InverseQ = rsaParams.InverseQ,
				D = rsaParams.D
			};
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x0007CFD4 File Offset: 0x0007B1D4
		private static bool IsPublic(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			return keyBlob[0] == 6 && keyBlob[11] == 49 && keyBlob[10] == 65 && keyBlob[9] == 83 && keyBlob[8] == 82;
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x0007D00E File Offset: 0x0007B20E
		private static bool IsPublic(RSAParameters rsaParams)
		{
			return rsaParams.P == null;
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x0007D01C File Offset: 0x0007B21C
		[SecuritySafeCritical]
		protected override byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			byte[] result;
			using (SafeHashHandle safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm)))
			{
				Utils.HashData(safeHashHandle, data, offset, count);
				result = Utils.EndHash(safeHashHandle);
			}
			return result;
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x0007D068 File Offset: 0x0007B268
		[SecuritySafeCritical]
		protected override byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			byte[] result;
			using (SafeHashHandle safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm)))
			{
				byte[] array = new byte[4096];
				int num;
				do
				{
					num = data.Read(array, 0, array.Length);
					if (num > 0)
					{
						Utils.HashData(safeHashHandle, array, 0, num);
					}
				}
				while (num > 0);
				result = Utils.EndHash(safeHashHandle);
			}
			return result;
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0007D0D4 File Offset: 0x0007B2D4
		private static int GetAlgorithmId(HashAlgorithmName hashAlgorithm)
		{
			string name = hashAlgorithm.Name;
			if (name == "MD5")
			{
				return 32771;
			}
			if (name == "SHA1")
			{
				return 32772;
			}
			if (name == "SHA256")
			{
				return 32780;
			}
			if (name == "SHA384")
			{
				return 32781;
			}
			if (!(name == "SHA512"))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_UnknownHashAlgorithm", new object[]
				{
					hashAlgorithm.Name
				}));
			}
			return 32782;
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x0007D16C File Offset: 0x0007B36C
		public override byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding == RSAEncryptionPadding.Pkcs1)
			{
				return this.Encrypt(data, false);
			}
			if (padding == RSAEncryptionPadding.OaepSHA1)
			{
				return this.Encrypt(data, true);
			}
			throw RSACryptoServiceProvider.PaddingModeNotSupported();
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x0007D1CC File Offset: 0x0007B3CC
		public override byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding == RSAEncryptionPadding.Pkcs1)
			{
				return this.Decrypt(data, false);
			}
			if (padding == RSAEncryptionPadding.OaepSHA1)
			{
				return this.Decrypt(data, true);
			}
			throw RSACryptoServiceProvider.PaddingModeNotSupported();
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x0007D22C File Offset: 0x0007B42C
		public override byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding != RSASignaturePadding.Pkcs1)
			{
				throw RSACryptoServiceProvider.PaddingModeNotSupported();
			}
			return this.SignHash(hash, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm));
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x0007D290 File Offset: 0x0007B490
		public override bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding != RSASignaturePadding.Pkcs1)
			{
				throw RSACryptoServiceProvider.PaddingModeNotSupported();
			}
			return this.VerifyHash(hash, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm), signature);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x0007D304 File Offset: 0x0007B504
		private static Exception PaddingModeNotSupported()
		{
			return new CryptographicException(Environment.GetResourceString("Cryptography_InvalidPaddingMode"));
		}

		// Token: 0x04000C9B RID: 3227
		private int _dwKeySize;

		// Token: 0x04000C9C RID: 3228
		private CspParameters _parameters;

		// Token: 0x04000C9D RID: 3229
		private bool _randomKeyContainer;

		// Token: 0x04000C9E RID: 3230
		[SecurityCritical]
		private SafeProvHandle _safeProvHandle;

		// Token: 0x04000C9F RID: 3231
		[SecurityCritical]
		private SafeKeyHandle _safeKeyHandle;

		// Token: 0x04000CA0 RID: 3232
		private static volatile CspProviderFlags s_UseMachineKeyStore;
	}
}
