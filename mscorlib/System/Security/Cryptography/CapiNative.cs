using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200023C RID: 572
	internal static class CapiNative
	{
		// Token: 0x06002095 RID: 8341 RVA: 0x00072548 File Offset: 0x00070748
		[SecurityCritical]
		internal static SafeCspHandle AcquireCsp(string keyContainer, string providerName, CapiNative.ProviderType providerType, CapiNative.CryptAcquireContextFlags flags)
		{
			if ((flags & CapiNative.CryptAcquireContextFlags.VerifyContext) == CapiNative.CryptAcquireContextFlags.VerifyContext && (flags & CapiNative.CryptAcquireContextFlags.MachineKeyset) == CapiNative.CryptAcquireContextFlags.MachineKeyset)
			{
				flags &= ~CapiNative.CryptAcquireContextFlags.MachineKeyset;
			}
			SafeCspHandle result = null;
			if (!CapiNative.UnsafeNativeMethods.CryptAcquireContext(out result, keyContainer, providerName, providerType, flags))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x0007258C File Offset: 0x0007078C
		[SecurityCritical]
		internal static SafeCspHashHandle CreateHashAlgorithm(SafeCspHandle cspHandle, CapiNative.AlgorithmID algorithm)
		{
			SafeCspHashHandle result = null;
			if (!CapiNative.UnsafeNativeMethods.CryptCreateHash(cspHandle, algorithm, IntPtr.Zero, 0, out result))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x000725B8 File Offset: 0x000707B8
		[SecurityCritical]
		internal static void GenerateRandomBytes(SafeCspHandle cspHandle, byte[] buffer)
		{
			if (!CapiNative.UnsafeNativeMethods.CryptGenRandom(cspHandle, buffer.Length, buffer))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000725D4 File Offset: 0x000707D4
		[SecurityCritical]
		internal unsafe static void GenerateRandomBytes(SafeCspHandle cspHandle, byte[] buffer, int offset, int count)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				if (!CapiNative.UnsafeNativeMethods.CryptGenRandom(cspHandle, count, ptr))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x00072604 File Offset: 0x00070804
		[SecurityCritical]
		internal static int GetHashPropertyInt32(SafeCspHashHandle hashHandle, CapiNative.HashProperty property)
		{
			byte[] hashProperty = CapiNative.GetHashProperty(hashHandle, property);
			if (hashProperty.Length != 4)
			{
				return 0;
			}
			return BitConverter.ToInt32(hashProperty, 0);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x00072628 File Offset: 0x00070828
		[SecurityCritical]
		internal static byte[] GetHashProperty(SafeCspHashHandle hashHandle, CapiNative.HashProperty property)
		{
			int num = 0;
			byte[] array = null;
			if (!CapiNative.UnsafeNativeMethods.CryptGetHashParam(hashHandle, property, array, ref num, 0))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 234)
				{
					throw new CryptographicException(lastWin32Error);
				}
			}
			array = new byte[num];
			if (!CapiNative.UnsafeNativeMethods.CryptGetHashParam(hashHandle, property, array, ref num, 0))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return array;
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x0007267C File Offset: 0x0007087C
		[SecurityCritical]
		internal static int GetKeyPropertyInt32(SafeCspKeyHandle keyHandle, CapiNative.KeyProperty property)
		{
			byte[] keyProperty = CapiNative.GetKeyProperty(keyHandle, property);
			if (keyProperty.Length != 4)
			{
				return 0;
			}
			return BitConverter.ToInt32(keyProperty, 0);
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000726A0 File Offset: 0x000708A0
		[SecurityCritical]
		internal static byte[] GetKeyProperty(SafeCspKeyHandle keyHandle, CapiNative.KeyProperty property)
		{
			int num = 0;
			byte[] array = null;
			if (!CapiNative.UnsafeNativeMethods.CryptGetKeyParam(keyHandle, property, array, ref num, 0))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 234)
				{
					throw new CryptographicException(lastWin32Error);
				}
			}
			array = new byte[num];
			if (!CapiNative.UnsafeNativeMethods.CryptGetKeyParam(keyHandle, property, array, ref num, 0))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return array;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000726F3 File Offset: 0x000708F3
		[SecurityCritical]
		internal static void SetHashProperty(SafeCspHashHandle hashHandle, CapiNative.HashProperty property, byte[] value)
		{
			if (!CapiNative.UnsafeNativeMethods.CryptSetHashParam(hashHandle, property, value, 0))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x0007270C File Offset: 0x0007090C
		[SecurityCritical]
		internal static bool VerifySignature(SafeCspHandle cspHandle, SafeCspKeyHandle keyHandle, CapiNative.AlgorithmID signatureAlgorithm, CapiNative.AlgorithmID hashAlgorithm, byte[] hashValue, byte[] signature)
		{
			byte[] array = new byte[signature.Length];
			Array.Copy(signature, array, array.Length);
			Array.Reverse(array);
			bool result;
			using (SafeCspHashHandle safeCspHashHandle = CapiNative.CreateHashAlgorithm(cspHandle, hashAlgorithm))
			{
				if (hashValue.Length != CapiNative.GetHashPropertyInt32(safeCspHashHandle, CapiNative.HashProperty.HashSize))
				{
					throw new CryptographicException(-2146893822);
				}
				CapiNative.SetHashProperty(safeCspHashHandle, CapiNative.HashProperty.HashValue, hashValue);
				if (CapiNative.UnsafeNativeMethods.CryptVerifySignature(safeCspHashHandle, array, array.Length, keyHandle, null, 0))
				{
					result = true;
				}
				else
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != -2146893818)
					{
						throw new CryptographicException(lastWin32Error);
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x02000B06 RID: 2822
		internal enum AlgorithmClass
		{
			// Token: 0x04003290 RID: 12944
			Any,
			// Token: 0x04003291 RID: 12945
			Signature = 8192,
			// Token: 0x04003292 RID: 12946
			Hash = 32768,
			// Token: 0x04003293 RID: 12947
			KeyExchange = 40960
		}

		// Token: 0x02000B07 RID: 2823
		internal enum AlgorithmType
		{
			// Token: 0x04003295 RID: 12949
			Any,
			// Token: 0x04003296 RID: 12950
			Rsa = 1024
		}

		// Token: 0x02000B08 RID: 2824
		internal enum AlgorithmSubId
		{
			// Token: 0x04003298 RID: 12952
			Any,
			// Token: 0x04003299 RID: 12953
			RsaAny = 0,
			// Token: 0x0400329A RID: 12954
			Sha1 = 4,
			// Token: 0x0400329B RID: 12955
			Sha256 = 12,
			// Token: 0x0400329C RID: 12956
			Sha384,
			// Token: 0x0400329D RID: 12957
			Sha512
		}

		// Token: 0x02000B09 RID: 2825
		internal enum AlgorithmID
		{
			// Token: 0x0400329F RID: 12959
			None,
			// Token: 0x040032A0 RID: 12960
			RsaSign = 9216,
			// Token: 0x040032A1 RID: 12961
			RsaKeyExchange = 41984,
			// Token: 0x040032A2 RID: 12962
			Sha1 = 32772,
			// Token: 0x040032A3 RID: 12963
			Sha256 = 32780,
			// Token: 0x040032A4 RID: 12964
			Sha384,
			// Token: 0x040032A5 RID: 12965
			Sha512
		}

		// Token: 0x02000B0A RID: 2826
		[Flags]
		internal enum CryptAcquireContextFlags
		{
			// Token: 0x040032A7 RID: 12967
			None = 0,
			// Token: 0x040032A8 RID: 12968
			NewKeyset = 8,
			// Token: 0x040032A9 RID: 12969
			DeleteKeyset = 16,
			// Token: 0x040032AA RID: 12970
			MachineKeyset = 32,
			// Token: 0x040032AB RID: 12971
			Silent = 64,
			// Token: 0x040032AC RID: 12972
			VerifyContext = -268435456
		}

		// Token: 0x02000B0B RID: 2827
		internal enum ErrorCode
		{
			// Token: 0x040032AE RID: 12974
			Ok,
			// Token: 0x040032AF RID: 12975
			MoreData = 234,
			// Token: 0x040032B0 RID: 12976
			BadHash = -2146893822,
			// Token: 0x040032B1 RID: 12977
			BadData = -2146893819,
			// Token: 0x040032B2 RID: 12978
			BadSignature,
			// Token: 0x040032B3 RID: 12979
			NoKey = -2146893811
		}

		// Token: 0x02000B0C RID: 2828
		internal enum HashProperty
		{
			// Token: 0x040032B5 RID: 12981
			None,
			// Token: 0x040032B6 RID: 12982
			HashValue = 2,
			// Token: 0x040032B7 RID: 12983
			HashSize = 4
		}

		// Token: 0x02000B0D RID: 2829
		[Flags]
		internal enum KeyGenerationFlags
		{
			// Token: 0x040032B9 RID: 12985
			None = 0,
			// Token: 0x040032BA RID: 12986
			Exportable = 1,
			// Token: 0x040032BB RID: 12987
			UserProtected = 2,
			// Token: 0x040032BC RID: 12988
			Archivable = 16384
		}

		// Token: 0x02000B0E RID: 2830
		internal enum KeyProperty
		{
			// Token: 0x040032BE RID: 12990
			None,
			// Token: 0x040032BF RID: 12991
			AlgorithmID = 7,
			// Token: 0x040032C0 RID: 12992
			KeyLength = 9
		}

		// Token: 0x02000B0F RID: 2831
		internal enum KeySpec
		{
			// Token: 0x040032C2 RID: 12994
			KeyExchange = 1,
			// Token: 0x040032C3 RID: 12995
			Signature
		}

		// Token: 0x02000B10 RID: 2832
		internal static class ProviderNames
		{
			// Token: 0x040032C4 RID: 12996
			internal const string MicrosoftEnhanced = "Microsoft Enhanced Cryptographic Provider v1.0";
		}

		// Token: 0x02000B11 RID: 2833
		internal enum ProviderType
		{
			// Token: 0x040032C6 RID: 12998
			RsaFull = 1
		}

		// Token: 0x02000B12 RID: 2834
		[SecurityCritical]
		internal static class UnsafeNativeMethods
		{
			// Token: 0x06006A5A RID: 27226
			[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptAcquireContext(out SafeCspHandle phProv, string pszContainer, string pszProvider, CapiNative.ProviderType dwProvType, CapiNative.CryptAcquireContextFlags dwFlags);

			// Token: 0x06006A5B RID: 27227
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptCreateHash(SafeCspHandle hProv, CapiNative.AlgorithmID Algid, IntPtr hKey, int dwFlags, out SafeCspHashHandle phHash);

			// Token: 0x06006A5C RID: 27228
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptGenKey(SafeCspHandle hProv, int Algid, uint dwFlags, out SafeCspKeyHandle phKey);

			// Token: 0x06006A5D RID: 27229
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptGenRandom(SafeCspHandle hProv, int dwLen, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbBuffer);

			// Token: 0x06006A5E RID: 27230
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal unsafe static extern bool CryptGenRandom(SafeCspHandle hProv, int dwLen, byte* pbBuffer);

			// Token: 0x06006A5F RID: 27231
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptGetHashParam(SafeCspHashHandle hHash, CapiNative.HashProperty dwParam, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbData, [In] [Out] ref int pdwDataLen, int dwFlags);

			// Token: 0x06006A60 RID: 27232
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptGetKeyParam(SafeCspKeyHandle hKey, CapiNative.KeyProperty dwParam, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbData, [In] [Out] ref int pdwDataLen, int dwFlags);

			// Token: 0x06006A61 RID: 27233
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptImportKey(SafeCspHandle hProv, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbData, int pdwDataLen, IntPtr hPubKey, CapiNative.KeyGenerationFlags dwFlags, out SafeCspKeyHandle phKey);

			// Token: 0x06006A62 RID: 27234
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptSetHashParam(SafeCspHashHandle hHash, CapiNative.HashProperty dwParam, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbData, int dwFlags);

			// Token: 0x06006A63 RID: 27235
			[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptVerifySignature(SafeCspHashHandle hHash, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSignature, int dwSigLen, SafeCspKeyHandle hPubKey, string sDescription, int dwFlags);
		}
	}
}
