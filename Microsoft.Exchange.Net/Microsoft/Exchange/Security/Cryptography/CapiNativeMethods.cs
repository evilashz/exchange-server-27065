using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security.Cryptography
{
	// Token: 0x02000A4A RID: 2634
	[SuppressUnmanagedCodeSecurity]
	[ComVisible(false)]
	internal static class CapiNativeMethods
	{
		// Token: 0x060039AF RID: 14767
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CryptAcquireContextW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptAcquireContext(out SafeCryptProvHandle hCryptProv, string pszContainer, string pszProvider, CapiNativeMethods.ProviderType provType, CapiNativeMethods.AcquireContext dwFlags);

		// Token: 0x060039B0 RID: 14768
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("crypt32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertFreeCertificateContext(IntPtr pCertContext);

		// Token: 0x060039B1 RID: 14769
		[DllImport("crypt32.dll", SetLastError = true)]
		public static extern SafeCertContextHandle CertDuplicateCertificateContext(IntPtr handle);

		// Token: 0x060039B2 RID: 14770
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertCloseStore(IntPtr hCertStore, uint dwFlags);

		// Token: 0x060039B3 RID: 14771
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertGetCertificateContextProperty(SafeCertContextHandle certContext, CapiNativeMethods.CertificatePropertyId property, SafeHGlobalHandle data, [In] [Out] ref uint size);

		// Token: 0x060039B4 RID: 14772
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertSetCertificateContextProperty(SafeCertContextHandle certContext, CapiNativeMethods.CertificatePropertyId property, uint flags, SafeHGlobalHandle data);

		// Token: 0x060039B5 RID: 14773
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertSetCertificateContextProperty(SafeCertContextHandle certContext, CapiNativeMethods.CertificatePropertyId propertyId, uint flags, [In] ref CapiNativeMethods.CryptoApiBlob data);

		// Token: 0x060039B6 RID: 14774
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptSetProvParam(SafeCryptProvHandle hCryptProv, CapiNativeMethods.SetProvParam dwParam, [In] byte[] pbData, uint dwFlags);

		// Token: 0x060039B7 RID: 14775
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptReleaseContext(IntPtr hCryptProv, uint dwFlags);

		// Token: 0x060039B8 RID: 14776
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptEncodeObject(CapiNativeMethods.EncodeType encoding, [MarshalAs(UnmanagedType.LPStr)] string oidString, ref CapiNativeMethods.CryptoApiBlob bytes, [In] [Out] byte[] encoded, [In] [Out] ref uint size);

		// Token: 0x060039B9 RID: 14777
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptEncodeObject(CapiNativeMethods.EncodeType encoding, IntPtr oidString, ref CapiNativeMethods.CertPublicKeyInfo bytes, [In] [Out] byte[] encoded, [In] [Out] ref uint size);

		// Token: 0x060039BA RID: 14778
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptEncodeObject(CapiNativeMethods.EncodeType dwCertEncodingType, IntPtr oid, ref CapiNativeMethods.CryptoApiBlob bytes, [In] [Out] byte[] encoded, [In] [Out] ref uint size);

		// Token: 0x060039BB RID: 14779
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptDecodeObject(CapiNativeMethods.EncodeType encodingType, IntPtr oidString, [In] byte[] data, uint count, uint flags, [In] [Out] SafeHGlobalHandle pvStructInfo, [In] [Out] ref uint size);

		// Token: 0x060039BC RID: 14780
		[DllImport("crypt32.dll", SetLastError = true)]
		public static extern SafeCertContextHandle CertCreateCertificateContext(CapiNativeMethods.EncodeType dwCertEncodingType, byte[] rawData, int length);

		// Token: 0x060039BD RID: 14781
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptGetUserKey(SafeCryptProvHandle CryptProv, CapiNativeMethods.KeySpec KeySpec, out SafeCryptKeyHandle Key);

		// Token: 0x060039BE RID: 14782
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptGetKeyParam(SafeCryptKeyHandle key, CapiNativeMethods.KeyParameter parameter, [In] [Out] ref uint value, [In] [Out] ref uint size, uint reserved);

		// Token: 0x060039BF RID: 14783
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptDestroyKey(IntPtr key);

		// Token: 0x060039C0 RID: 14784
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptGenKey(SafeCryptProvHandle hCryptProv, CapiNativeMethods.KeySpec Algid, uint dwFlags, [In] [Out] ref SafeCryptProvHandle hKey);

		// Token: 0x060039C1 RID: 14785
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptGetProvParam(SafeCryptProvHandle handle, CapiNativeMethods.ProviderParameter parameter, out uint value, [In] [Out] ref uint length, uint extra);

		// Token: 0x060039C2 RID: 14786
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern void CertFreeCertificateChain(IntPtr chainContext);

		// Token: 0x060039C3 RID: 14787
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertCreateCertificateChainEngine(ref ChainEnginePool.ChainEngineConfig configuration, out SafeChainEngineHandle engine);

		// Token: 0x060039C4 RID: 14788
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern void CertFreeCertificateChainEngine(IntPtr handle);

		// Token: 0x060039C5 RID: 14789
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertControlStore([In] SafeCertStoreHandle hCertStore, [In] uint dwFlags, [In] CapiNativeMethods.StoreControl dwCtrlType, [In] ref IntPtr pvCtrlPara);

		// Token: 0x060039C6 RID: 14790 RVA: 0x000923F8 File Offset: 0x000905F8
		public unsafe static X509Certificate2 CreateSelfSignCertificate(SafeCryptProvHandle providerHandle, X500DistinguishedName subjectName, uint flags, CapiNativeMethods.CryptKeyProvInfo keyProvInfo, string signatureAlgorithm, DateTime fromTime, DateTime toTime, X509ExtensionCollection extensions, string friendlyName)
		{
			if (string.IsNullOrEmpty(signatureAlgorithm))
			{
				throw new ArgumentNullException("signatureAlgorithm");
			}
			long num = fromTime.ToFileTimeUtc();
			NativeMethods.SystemTime systemTime;
			NativeMethods.FileTimeToSystemTime(ref num, out systemTime);
			num = toTime.ToFileTimeUtc();
			NativeMethods.SystemTime systemTime2;
			NativeMethods.FileTimeToSystemTime(ref num, out systemTime2);
			CapiNativeMethods.CryptAlgorithmIdentifier cryptAlgorithmIdentifier = default(CapiNativeMethods.CryptAlgorithmIdentifier);
			cryptAlgorithmIdentifier.ObjectId = signatureAlgorithm;
			SafeCertContextHandle safeCertContextHandle = null;
			X509Certificate2 result;
			try
			{
				SafeHGlobalHandle pExtensions = CapiNativeMethods.AllocateExtensions(extensions);
				int num2 = 0;
				try
				{
					fixed (byte* ptr = subjectName.RawData)
					{
						CapiNativeMethods.CryptoApiBlob cryptoApiBlob = default(CapiNativeMethods.CryptoApiBlob);
						cryptoApiBlob.Count = (uint)subjectName.RawData.Length;
						cryptoApiBlob.DataPointer = new IntPtr((void*)ptr);
						safeCertContextHandle = CapiNativeMethods.CertCreateSelfSignCertificate(providerHandle, new IntPtr((void*)(&cryptoApiBlob)), 0U, ref keyProvInfo, ref cryptAlgorithmIdentifier, ref systemTime, ref systemTime2, pExtensions);
						if (safeCertContextHandle.IsInvalid)
						{
							num2 = Marshal.GetLastWin32Error();
						}
					}
				}
				finally
				{
					byte* ptr = null;
				}
				if (num2 != 0)
				{
					throw new CryptographicException(num2);
				}
				if (!string.IsNullOrEmpty(friendlyName))
				{
					using (SafeHGlobalHandle safeHGlobalHandle = new SafeHGlobalHandle(Marshal.StringToHGlobalUni(friendlyName)))
					{
						CapiNativeMethods.CryptoApiBlob cryptoApiBlob2 = new CapiNativeMethods.CryptoApiBlob((uint)(2 * (friendlyName.Length + 1)), safeHGlobalHandle);
						if (!CapiNativeMethods.CertSetCertificateContextProperty(safeCertContextHandle, CapiNativeMethods.CertificatePropertyId.FriendlyName, 0U, ref cryptoApiBlob2))
						{
							throw new CryptographicException(Marshal.GetLastWin32Error());
						}
					}
				}
				X509Certificate2 x509Certificate = new X509Certificate2(safeCertContextHandle.DangerousGetHandle());
				result = x509Certificate;
			}
			finally
			{
				if (safeCertContextHandle != null)
				{
					safeCertContextHandle.Dispose();
					safeCertContextHandle = null;
				}
			}
			return result;
		}

		// Token: 0x060039C7 RID: 14791
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeCertStoreHandle CertOpenStore(IntPtr lpszStoreProvider, CapiNativeMethods.EncodeType dwMsgAndCertEncodingType, IntPtr hCryptProv, CapiNativeMethods.CertificateStoreOptions dwFlags, string pvPara);

		// Token: 0x060039C8 RID: 14792
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeCertStoreHandle CertOpenStore(IntPtr lpszStoreProvider, CapiNativeMethods.EncodeType dwMsgAndCertEncodingType, IntPtr hCryptProv, CapiNativeMethods.CertificateStoreOptions dwFlags, CapiNativeMethods.CryptoApiBlob pvPara);

		// Token: 0x060039C9 RID: 14793
		[DllImport("crypt32.dll", SetLastError = true)]
		internal static extern SafeCertStoreHandle CertDuplicateStore(IntPtr handle);

		// Token: 0x060039CA RID: 14794
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CertCloseStore(IntPtr certStore, CapiNativeMethods.CertCloseStoreFlag flags);

		// Token: 0x060039CB RID: 14795
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int CertRDNValueToStr([In] int dwValueType, [In] ref CapiNativeMethods.CryptoApiBlob pValue, [In] [Out] SafeHGlobalHandle pszNameString, [In] int cchNameString);

		// Token: 0x060039CC RID: 14796 RVA: 0x00092588 File Offset: 0x00090788
		internal static CapiNativeMethods.AlgorithmId GetAlgorithmId(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			string keyAlgorithm = certificate.GetKeyAlgorithm();
			return (CapiNativeMethods.AlgorithmId)CapiNativeMethods.FindOIDInfo(CapiNativeMethods.OidSearchKey.Oid, keyAlgorithm, CapiNativeMethods.OidSearchScope.PublicKeyAlgorithm).Algid;
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000925BC File Offset: 0x000907BC
		internal static string GetKeyAlgorithmName(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			string keyAlgorithm = certificate.GetKeyAlgorithm();
			return CapiNativeMethods.FindOIDInfo(CapiNativeMethods.OidSearchKey.Oid, keyAlgorithm, CapiNativeMethods.OidSearchScope.PublicKeyAlgorithm).Name;
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000925F0 File Offset: 0x000907F0
		internal static string[] GetEnhancedKeyUsage(X509Certificate2 certificate, CapiNativeMethods.EnhancedKeyUsageSearch flags)
		{
			int num = 0;
			uint size = 512U;
			SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.InvalidHandle;
			int i = 0;
			while (i < 2)
			{
				num = 0;
				SafeHGlobalHandle safeHGlobalHandle2;
				safeHGlobalHandle = (safeHGlobalHandle2 = NativeMethods.AllocHGlobal((int)size));
				try
				{
					bool flag = CapiNativeMethods.CertGetEnhancedKeyUsage(certificate.Handle, flags, safeHGlobalHandle, ref size);
					num = Marshal.GetLastWin32Error();
					if (flag)
					{
						CapiNativeMethods.CryptoApiBlob cryptoApiBlob = (CapiNativeMethods.CryptoApiBlob)Marshal.PtrToStructure(safeHGlobalHandle.DangerousGetHandle(), typeof(CapiNativeMethods.CryptoApiBlob));
						if (cryptoApiBlob.Count == 0U && -2146885628 != num)
						{
							return null;
						}
						string[] array = new string[cryptoApiBlob.Count];
						IntPtr dataPointer = cryptoApiBlob.DataPointer;
						for (int j = 0; j < (int)cryptoApiBlob.Count; j++)
						{
							IntPtr ptr = Marshal.ReadIntPtr(dataPointer);
							array[j] = Marshal.PtrToStringAnsi(ptr);
							dataPointer = new IntPtr((long)dataPointer + (long)IntPtr.Size);
						}
						return array;
					}
				}
				finally
				{
					if (safeHGlobalHandle2 != null)
					{
						((IDisposable)safeHGlobalHandle2).Dispose();
					}
				}
				if (234 == num)
				{
					i++;
					continue;
				}
				break;
			}
			throw new CryptographicException(num);
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x0009270C File Offset: 0x0009090C
		internal static bool DecodeObject(string sourceType, byte[] source, out SafeHGlobalHandle destination, out uint destinationSize)
		{
			IntPtr intPtr = IntPtr.Zero;
			bool result;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(sourceType);
				result = CapiNativeMethods.DecodeObject(intPtr, source, out destination, out destinationSize);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x00092758 File Offset: 0x00090958
		internal static bool DecodeObject(CapiNativeMethods.EncodeDecodeObjectType sourceType, byte[] source, out SafeHGlobalHandle destination, out uint destinationSize)
		{
			return CapiNativeMethods.DecodeObject((IntPtr)((long)((ulong)sourceType)), source, out destination, out destinationSize);
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x0009276C File Offset: 0x0009096C
		private static bool DecodeObject(IntPtr sourceType, byte[] source, out SafeHGlobalHandle destination, out uint destinationSize)
		{
			destination = SafeHGlobalHandle.InvalidHandle;
			destinationSize = 0U;
			uint num = 0U;
			SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.InvalidHandle;
			if (!CapiNativeMethods.CryptDecodeObject(CapiNativeMethods.EncodeType.X509Asn | CapiNativeMethods.EncodeType.Pkcs7Asn, sourceType, source, (uint)source.Length, 0U, safeHGlobalHandle, ref num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			safeHGlobalHandle = NativeMethods.AllocHGlobal((int)num);
			if (!CapiNativeMethods.CryptDecodeObject(CapiNativeMethods.EncodeType.X509Asn | CapiNativeMethods.EncodeType.Pkcs7Asn, sourceType, source, (uint)source.Length, 0U, safeHGlobalHandle, ref num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			destination = safeHGlobalHandle;
			destinationSize = num;
			return true;
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x000927DC File Offset: 0x000909DC
		internal static X509KeyUsageFlags GetIntendedKeyUsage(X509Certificate2 certificate)
		{
			byte[] array = new byte[2];
			if (CapiNativeMethods.CertGetIntendedKeyUsage(CapiNativeMethods.EncodeType.X509Asn | CapiNativeMethods.EncodeType.Pkcs7Asn, ((CapiNativeMethods.CertContext)Marshal.PtrToStructure(certificate.Handle, typeof(CapiNativeMethods.CertContext))).CertInfo, array, 2U))
			{
				return (X509KeyUsageFlags)((int)array[1] << 8 | (int)array[0]);
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error != 0)
			{
				throw new CryptographicException(lastWin32Error);
			}
			return X509KeyUsageFlags.None;
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x00092840 File Offset: 0x00090A40
		internal static SafeCertStoreHandle GetStoreHandleFromCertificate(X509Certificate2 certificate)
		{
			return SafeCertStoreHandle.Clone(((CapiNativeMethods.CertContext)Marshal.PtrToStructure(certificate.Handle, typeof(CapiNativeMethods.CertContext))).CertStore);
		}

		// Token: 0x060039D4 RID: 14804
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CertGetIntendedKeyUsage([In] CapiNativeMethods.EncodeType encodingType, [In] IntPtr pCertInfo, [In] [Out] byte[] usage, [In] uint size);

		// Token: 0x060039D5 RID: 14805
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CertGetEnhancedKeyUsage(IntPtr pCertContext, CapiNativeMethods.EnhancedKeyUsageSearch flags, [In] SafeHGlobalHandle usage, [In] [Out] ref uint size);

		// Token: 0x060039D6 RID: 14806
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern SafeCertContextHandle CertCreateSelfSignCertificate([In] SafeCryptProvHandle hProv, [In] IntPtr pSubjectIssuerBlob, [In] uint dwFlags, [In] ref CapiNativeMethods.CryptKeyProvInfo keyProvInfo, [In] ref CapiNativeMethods.CryptAlgorithmIdentifier signatureAlgorithm, [In] ref NativeMethods.SystemTime fromTime, [In] ref NativeMethods.SystemTime toTime, [In] SafeHGlobalHandle pExtensions);

		// Token: 0x060039D7 RID: 14807
		[DllImport("crypt32.dll", CharSet = CharSet.Ansi)]
		private static extern IntPtr CryptFindOIDInfo(CapiNativeMethods.OidSearchKey keyType, [MarshalAs(UnmanagedType.LPStr)] string key, CapiNativeMethods.OidSearchScope groupId);

		// Token: 0x060039D8 RID: 14808
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptDestroyHash(IntPtr handle);

		// Token: 0x060039D9 RID: 14809
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptCreateHash(IntPtr hProv, CapiNativeMethods.AlgorithmId Algid, IntPtr hKey, int flags, [In] [Out] ref SafeHashHandle hashHandle);

		// Token: 0x060039DA RID: 14810
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public unsafe static extern bool CryptHashData(SafeHashHandle hHash, byte* pbData, uint length, int flags);

		// Token: 0x060039DB RID: 14811
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CryptGetHashParam(SafeHashHandle hHash, CapiNativeMethods.HashParameter parameter, [In] [Out] byte[] bytes, [In] [Out] ref uint size, uint flags);

		// Token: 0x060039DC RID: 14812
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertVerifyCertificateChainPolicy([MarshalAs(UnmanagedType.LPStr)] string pszPolicyOID, SafeChainContextHandle chainContext, [In] ref CapiNativeMethods.CertChainPolicyParameters pPolicyPara, [In] [Out] ref CapiNativeMethods.CertChainPolicyStatus pPolicyStatus);

		// Token: 0x060039DD RID: 14813
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertVerifyCertificateChainPolicy(IntPtr pszPolicyOID, SafeChainContextHandle chainContext, [In] ref CapiNativeMethods.CertChainPolicyParameters pPolicyPara, [In] [Out] ref CapiNativeMethods.CertChainPolicyStatus pPolicyStatus);

		// Token: 0x060039DE RID: 14814
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CertGetCertificateChain(SafeChainEngineHandle engine, IntPtr pCertContext, IntPtr time, SafeCertStoreHandle hAdditionalStore, [In] ref CapiNativeMethods.CertChainParameter parameters, ChainBuildOptions flags, IntPtr reserved, out SafeChainContextHandle chainContext);

		// Token: 0x060039DF RID: 14815
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern uint CertGetNameStringW(IntPtr pCertContext, CapiNativeMethods.CertNameType displayType, uint dwFlags, [In] ref uint pvTypePara, [In] [Out] SafeHGlobalHandle pszNameString, uint cchNameString);

		// Token: 0x060039E0 RID: 14816
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern uint CertGetNameStringW(IntPtr pCertContext, CapiNativeMethods.CertNameType displayType, uint dwFlags, [MarshalAs(UnmanagedType.LPStr)] string oidString, [In] [Out] SafeHGlobalHandle pszNameString, [In] uint cchNameString);

		// Token: 0x060039E1 RID: 14817 RVA: 0x00092874 File Offset: 0x00090A74
		public static string GetCertNameInfo([In] X509Certificate2 certificate, [In] uint dwFlags, [In] CapiNativeMethods.CertNameType displayType)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.InvalidHandle;
			string result;
			try
			{
				if (displayType == CapiNativeMethods.CertNameType.Attr)
				{
					uint num = CapiNativeMethods.CertGetNameStringW(certificate.Handle, displayType, dwFlags, WellKnownOid.CommonName.Value, safeHGlobalHandle, 0U);
					if (num == 0U)
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					safeHGlobalHandle = NativeMethods.AllocHGlobal((int)(2U * num));
					if (CapiNativeMethods.CertGetNameStringW(certificate.Handle, displayType, dwFlags, WellKnownOid.CommonName.Value, safeHGlobalHandle, num) == 0U)
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					result = Marshal.PtrToStringUni(safeHGlobalHandle.DangerousGetHandle());
				}
				else
				{
					uint num2 = 33554435U;
					uint num3 = CapiNativeMethods.CertGetNameStringW(certificate.Handle, displayType, dwFlags, ref num2, safeHGlobalHandle, 0U);
					if (num3 == 0U)
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					safeHGlobalHandle = NativeMethods.AllocHGlobal((int)(2U * num3));
					if (CapiNativeMethods.CertGetNameStringW(certificate.Handle, displayType, dwFlags, ref num2, safeHGlobalHandle, num3) == 0U)
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					result = Marshal.PtrToStringUni(safeHGlobalHandle.DangerousGetHandle());
				}
			}
			finally
			{
				safeHGlobalHandle.Dispose();
			}
			return result;
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x00092978 File Offset: 0x00090B78
		private static CapiNativeMethods.CryptOidInfo FindOIDInfo(CapiNativeMethods.OidSearchKey keyType, string key, CapiNativeMethods.OidSearchScope groupId)
		{
			CapiNativeMethods.CryptOidInfo result = new CapiNativeMethods.CryptOidInfo(CapiNativeMethods.CryptOidInfo.MarshalSize);
			IntPtr intPtr = CapiNativeMethods.CryptFindOIDInfo(keyType, key, groupId);
			if (intPtr != IntPtr.Zero)
			{
				result = (CapiNativeMethods.CryptOidInfo)Marshal.PtrToStructure(intPtr, typeof(CapiNativeMethods.CryptOidInfo));
			}
			return result;
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x000929C0 File Offset: 0x00090BC0
		private static SafeHGlobalHandle AllocateExtensions(X509ExtensionCollection extensions)
		{
			SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.InvalidHandle;
			if (extensions == null || extensions.Count == 0)
			{
				return safeHGlobalHandle;
			}
			int num = CapiNativeMethods.CryptoApiBlob.MarshalSize;
			foreach (X509Extension x509Extension in extensions)
			{
				num += CapiNativeMethods.CertExtension.MarshalSize;
				num += x509Extension.Oid.Value.Length + 1;
				num += x509Extension.RawData.Length;
			}
			safeHGlobalHandle = NativeMethods.AllocHGlobal(num);
			CapiNativeMethods.CryptoApiBlob cryptoApiBlob;
			cryptoApiBlob.Count = (uint)extensions.Count;
			cryptoApiBlob.DataPointer = (IntPtr)((long)safeHGlobalHandle.DangerousGetHandle() + (long)CapiNativeMethods.CryptoApiBlob.MarshalSize);
			Marshal.StructureToPtr(cryptoApiBlob, safeHGlobalHandle.DangerousGetHandle(), false);
			IntPtr intPtr = cryptoApiBlob.DataPointer;
			IntPtr intPtr2 = (IntPtr)((long)intPtr + (long)extensions.Count * (long)CapiNativeMethods.CertExtension.MarshalSize);
			foreach (X509Extension x509Extension2 in extensions)
			{
				byte[] rawData = x509Extension2.RawData;
				CapiNativeMethods.CertExtension certExtension;
				certExtension.ObjectId = intPtr2;
				byte[] array = new byte[x509Extension2.Oid.Value.Length + 1];
				Encoding.ASCII.GetBytes(x509Extension2.Oid.Value, 0, x509Extension2.Oid.Value.Length, array, 0);
				Marshal.Copy(array, 0, intPtr2, array.Length);
				intPtr2 = (IntPtr)((long)intPtr2 + (long)array.Length);
				certExtension.IsCritical = x509Extension2.Critical;
				certExtension.Value.Count = (uint)rawData.Length;
				certExtension.Value.DataPointer = ((rawData.Length != 0) ? intPtr2 : IntPtr.Zero);
				Marshal.StructureToPtr(certExtension, intPtr, false);
				intPtr = (IntPtr)((long)intPtr + (long)CapiNativeMethods.CertExtension.MarshalSize);
				if (rawData.Length != 0)
				{
					Marshal.Copy(rawData, 0, intPtr2, rawData.Length);
					intPtr2 = (IntPtr)((long)intPtr2 + (long)rawData.Length);
				}
			}
			return safeHGlobalHandle;
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x00092BB8 File Offset: 0x00090DB8
		internal static string RDNValueToString(CapiNativeMethods.CertRdnAttribute attribute)
		{
			SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.InvalidHandle;
			int num = CapiNativeMethods.CertRDNValueToStr(attribute.dwValueType, ref attribute.value, safeHGlobalHandle, 0);
			if (num == 0)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			safeHGlobalHandle = NativeMethods.AllocHGlobal(2 * num);
			if (CapiNativeMethods.CertRDNValueToStr(attribute.dwValueType, ref attribute.value, safeHGlobalHandle, num) != num)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return Marshal.PtrToStringUni(safeHGlobalHandle.DangerousGetHandle());
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x00092C28 File Offset: 0x00090E28
		public static bool GetPrivateKeyInfo(X509Certificate2 certificate, out CapiNativeMethods.CryptKeyProvInfo info)
		{
			info = default(CapiNativeMethods.CryptKeyProvInfo);
			using (SafeCertContextHandle safeCertContextHandle = SafeCertContextHandle.Clone(certificate.Handle))
			{
				uint size = 0U;
				if (!CapiNativeMethods.CertGetCertificateContextProperty(safeCertContextHandle, CapiNativeMethods.CertificatePropertyId.KeyProviderInfo, SafeHGlobalHandle.InvalidHandle, ref size))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == -2146885628)
					{
						return false;
					}
					throw new CryptographicException(lastWin32Error);
				}
				else
				{
					SafeHGlobalHandle safeHGlobalHandle = NativeMethods.AllocHGlobal((int)size);
					if (!CapiNativeMethods.CertGetCertificateContextProperty(safeCertContextHandle, CapiNativeMethods.CertificatePropertyId.KeyProviderInfo, safeHGlobalHandle, ref size))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					info = (CapiNativeMethods.CryptKeyProvInfo)Marshal.PtrToStructure(safeHGlobalHandle.DangerousGetHandle(), typeof(CapiNativeMethods.CryptKeyProvInfo));
				}
			}
			return true;
		}

		// Token: 0x060039E6 RID: 14822
		[DllImport("Crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptProtectData(ref CapiNativeMethods.CryptoApiBlob pDataIn, IntPtr szDataDescr, IntPtr pOptionalEntropy, IntPtr pvReserved, IntPtr szPrompt, CapiNativeMethods.DPAPIFlags dwFlags, out CapiNativeMethods.CryptoApiBlob pDataOut);

		// Token: 0x060039E7 RID: 14823
		[DllImport("Crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptUnprotectData(ref CapiNativeMethods.CryptoApiBlob pDataIn, IntPtr ppszDataDescr, IntPtr pOptionalEntropy, IntPtr pvReserved, IntPtr szPrompt, CapiNativeMethods.DPAPIFlags dwFlags, out CapiNativeMethods.CryptoApiBlob pDataOut);

		// Token: 0x060039E8 RID: 14824 RVA: 0x00092D01 File Offset: 0x00090F01
		public static bool DPAPIDecryptData(byte[] pIn, out byte[] pOut)
		{
			if (pIn.Length > 0)
			{
				pOut = CapiNativeMethods.DPAPIDecryptData<byte[]>(pIn, CapiNativeMethods.DPAPIFlags.CRYPTPROTECT_LOCAL_MACHINE, delegate(SafeSecureHGlobalHandle decryptedData)
				{
					byte[] array = new byte[decryptedData.Length];
					Marshal.Copy(decryptedData.DangerousGetHandle(), array, 0, decryptedData.Length);
					return array;
				});
				return true;
			}
			pOut = null;
			return false;
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x00092D60 File Offset: 0x00090F60
		public unsafe static SecureString DPAPIDecryptDataToSecureString(byte[] pIn, CapiNativeMethods.DPAPIFlags flags)
		{
			return CapiNativeMethods.DPAPIDecryptData<SecureString>(pIn, flags, (SafeSecureHGlobalHandle decryptedData) => new SecureString((char*)decryptedData.DangerousGetHandle().ToPointer(), decryptedData.Length / 2));
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x00092D88 File Offset: 0x00090F88
		[SecurityCritical]
		private static T DPAPIDecryptData<T>(byte[] encryptedData, CapiNativeMethods.DPAPIFlags flags, Func<SafeSecureHGlobalHandle, T> resultMarshaller) where T : class
		{
			using (SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.CopyToHGlobal(encryptedData))
			{
				CapiNativeMethods.CryptoApiBlob cryptoApiBlob = new CapiNativeMethods.CryptoApiBlob((uint)encryptedData.Length, safeHGlobalHandle);
				CapiNativeMethods.CryptoApiBlob cryptoApiBlob2 = default(CapiNativeMethods.CryptoApiBlob);
				if (CapiNativeMethods.CryptUnprotectData(ref cryptoApiBlob, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, flags | CapiNativeMethods.DPAPIFlags.CRYPTPROTECT_UI_FORBIDDEN, out cryptoApiBlob2))
				{
					using (SafeSecureHGlobalHandle safeSecureHGlobalHandle = SafeSecureHGlobalHandle.Assign(cryptoApiBlob2.DataPointer, (int)cryptoApiBlob2.Count))
					{
						return resultMarshaller(safeSecureHGlobalHandle);
					}
				}
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			T result;
			return result;
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x00092E34 File Offset: 0x00091034
		public static bool DPAPIEncryptData(byte[] sensitiveData, out byte[] encryptedData)
		{
			if (sensitiveData.Length > 0)
			{
				using (SafeSecureHGlobalHandle safeSecureHGlobalHandle = SafeSecureHGlobalHandle.CopyToHGlobal(sensitiveData))
				{
					encryptedData = CapiNativeMethods.DPAPIEncryptData(safeSecureHGlobalHandle, CapiNativeMethods.DPAPIFlags.CRYPTPROTECT_LOCAL_MACHINE);
					return true;
				}
			}
			encryptedData = null;
			return false;
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x00092E7C File Offset: 0x0009107C
		public static byte[] DPAPIEncryptData(SecureString sensitiveData, CapiNativeMethods.DPAPIFlags flags)
		{
			byte[] result;
			using (SafeSecureHGlobalHandle safeSecureHGlobalHandle = sensitiveData.ConvertToUnsecureHGlobal())
			{
				result = CapiNativeMethods.DPAPIEncryptData(safeSecureHGlobalHandle, flags);
			}
			return result;
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x00092EB8 File Offset: 0x000910B8
		private static byte[] DPAPIEncryptData(SafeSecureHGlobalHandle sensitiveData, CapiNativeMethods.DPAPIFlags flags)
		{
			CapiNativeMethods.CryptoApiBlob cryptoApiBlob = new CapiNativeMethods.CryptoApiBlob((uint)sensitiveData.Length, sensitiveData);
			CapiNativeMethods.CryptoApiBlob cryptoApiBlob2 = default(CapiNativeMethods.CryptoApiBlob);
			if (CapiNativeMethods.CryptProtectData(ref cryptoApiBlob, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, flags | CapiNativeMethods.DPAPIFlags.CRYPTPROTECT_UI_FORBIDDEN, out cryptoApiBlob2))
			{
				using (SafeHGlobalHandle safeHGlobalHandle = new SafeHGlobalHandle(cryptoApiBlob2.DataPointer))
				{
					byte[] array = new byte[cryptoApiBlob2.Count];
					Marshal.Copy(safeHGlobalHandle.DangerousGetHandle(), array, 0, (int)cryptoApiBlob2.Count);
					return array;
				}
			}
			throw new CryptographicException(Marshal.GetLastWin32Error());
		}

		// Token: 0x040030FB RID: 12539
		private const string ADVAPI32 = "advapi32.dll";

		// Token: 0x040030FC RID: 12540
		private const string CRYPT32 = "crypt32.dll";

		// Token: 0x040030FD RID: 12541
		private const uint CERT_X500_NAME_STR = 3U;

		// Token: 0x040030FE RID: 12542
		private const uint CERT_NAME_STR_REVERSE_FLAG = 33554432U;

		// Token: 0x040030FF RID: 12543
		internal const int CertSystemStoreServicesID = 5;

		// Token: 0x04003100 RID: 12544
		internal const int CertSystemStoreLocationShift = 16;

		// Token: 0x04003101 RID: 12545
		private static IntPtr x509PublicKeyInfo = new IntPtr(8);

		// Token: 0x02000A4B RID: 2635
		[Flags]
		public enum CertificateStoreOptions : uint
		{
			// Token: 0x04003105 RID: 12549
			Delete = 16U,
			// Token: 0x04003106 RID: 12550
			Create = 8192U,
			// Token: 0x04003107 RID: 12551
			OpenExisting = 16384U,
			// Token: 0x04003108 RID: 12552
			ReadOnly = 32768U,
			// Token: 0x04003109 RID: 12553
			LocalMachine = 131072U,
			// Token: 0x0400310A RID: 12554
			Services = 327680U
		}

		// Token: 0x02000A4C RID: 2636
		public enum CertificateStoreProvider : uint
		{
			// Token: 0x0400310C RID: 12556
			Memory = 2U,
			// Token: 0x0400310D RID: 12557
			Serialized = 6U,
			// Token: 0x0400310E RID: 12558
			System = 10U,
			// Token: 0x0400310F RID: 12559
			Physical = 14U
		}

		// Token: 0x02000A4D RID: 2637
		public enum WinCapiStatus : uint
		{
			// Token: 0x04003111 RID: 12561
			MoreData = 234U,
			// Token: 0x04003112 RID: 12562
			NotFound = 2148081668U,
			// Token: 0x04003113 RID: 12563
			SilentContext = 2148073506U
		}

		// Token: 0x02000A4E RID: 2638
		internal enum CertificatePropertyId : uint
		{
			// Token: 0x04003115 RID: 12565
			KeyProviderHandle = 1U,
			// Token: 0x04003116 RID: 12566
			KeyProviderInfo,
			// Token: 0x04003117 RID: 12567
			FriendlyName = 11U,
			// Token: 0x04003118 RID: 12568
			XEnrollmentRequest = 52312U
		}

		// Token: 0x02000A4F RID: 2639
		internal enum ProviderType : uint
		{
			// Token: 0x0400311A RID: 12570
			CNG,
			// Token: 0x0400311B RID: 12571
			RsaFull,
			// Token: 0x0400311C RID: 12572
			Dss = 3U,
			// Token: 0x0400311D RID: 12573
			RsaSChannel = 12U,
			// Token: 0x0400311E RID: 12574
			DssDiffieHellman,
			// Token: 0x0400311F RID: 12575
			AES = 24U
		}

		// Token: 0x02000A50 RID: 2640
		internal enum OidSearchKey : uint
		{
			// Token: 0x04003121 RID: 12577
			Oid = 1U,
			// Token: 0x04003122 RID: 12578
			Name,
			// Token: 0x04003123 RID: 12579
			AlgId,
			// Token: 0x04003124 RID: 12580
			Sign
		}

		// Token: 0x02000A51 RID: 2641
		internal enum OidSearchScope : uint
		{
			// Token: 0x04003126 RID: 12582
			All,
			// Token: 0x04003127 RID: 12583
			HashAlgorithm,
			// Token: 0x04003128 RID: 12584
			EncryptAlgorithm,
			// Token: 0x04003129 RID: 12585
			PublicKeyAlgorithm,
			// Token: 0x0400312A RID: 12586
			SignatureAlgorithm,
			// Token: 0x0400312B RID: 12587
			RdnAttribute,
			// Token: 0x0400312C RID: 12588
			ExtensionOrAttribute,
			// Token: 0x0400312D RID: 12589
			EnhancedKeyUsage,
			// Token: 0x0400312E RID: 12590
			Policy,
			// Token: 0x0400312F RID: 12591
			Template
		}

		// Token: 0x02000A52 RID: 2642
		[Flags]
		internal enum EncodeType : uint
		{
			// Token: 0x04003131 RID: 12593
			None = 0U,
			// Token: 0x04003132 RID: 12594
			X509Asn = 1U,
			// Token: 0x04003133 RID: 12595
			Pkcs7Asn = 65536U
		}

		// Token: 0x02000A53 RID: 2643
		[Flags]
		internal enum CertCloseStoreFlag : uint
		{
			// Token: 0x04003135 RID: 12597
			CertCloseStoreForceFlag = 1U,
			// Token: 0x04003136 RID: 12598
			CertCloseStoreCheckFlag = 2U
		}

		// Token: 0x02000A54 RID: 2644
		[Flags]
		internal enum AcquireContext : uint
		{
			// Token: 0x04003138 RID: 12600
			NewKeyset = 8U,
			// Token: 0x04003139 RID: 12601
			DeleteKeyset = 16U,
			// Token: 0x0400313A RID: 12602
			MachineKeyset = 32U,
			// Token: 0x0400313B RID: 12603
			Silent = 64U,
			// Token: 0x0400313C RID: 12604
			Verify = 4026531840U
		}

		// Token: 0x02000A55 RID: 2645
		internal enum SetProvParam : uint
		{
			// Token: 0x0400313E RID: 12606
			KeySetSecurityDescriptor = 8U
		}

		// Token: 0x02000A56 RID: 2646
		internal enum ProviderParameter : uint
		{
			// Token: 0x04003140 RID: 12608
			EnumerateAlgorithms = 1U,
			// Token: 0x04003141 RID: 12609
			EnumerateContainers,
			// Token: 0x04003142 RID: 12610
			ImplementationType,
			// Token: 0x04003143 RID: 12611
			Name,
			// Token: 0x04003144 RID: 12612
			Version,
			// Token: 0x04003145 RID: 12613
			Container
		}

		// Token: 0x02000A57 RID: 2647
		[Flags]
		internal enum ProviderImplementationType : uint
		{
			// Token: 0x04003147 RID: 12615
			Hardware = 1U,
			// Token: 0x04003148 RID: 12616
			Software = 2U,
			// Token: 0x04003149 RID: 12617
			Mixed = 3U,
			// Token: 0x0400314A RID: 12618
			Unknown = 4U,
			// Token: 0x0400314B RID: 12619
			Removable = 8U
		}

		// Token: 0x02000A58 RID: 2648
		[Flags]
		internal enum SecurityInformation : uint
		{
			// Token: 0x0400314D RID: 12621
			Owner = 1U,
			// Token: 0x0400314E RID: 12622
			Group = 2U,
			// Token: 0x0400314F RID: 12623
			Dacl = 4U,
			// Token: 0x04003150 RID: 12624
			Sacl = 8U
		}

		// Token: 0x02000A59 RID: 2649
		[Flags]
		internal enum CertKeyOptions : uint
		{
			// Token: 0x04003152 RID: 12626
			None = 0U,
			// Token: 0x04003153 RID: 12627
			Exportable = 1U,
			// Token: 0x04003154 RID: 12628
			UserProtected = 2U,
			// Token: 0x04003155 RID: 12629
			CreateSalt = 4U,
			// Token: 0x04003156 RID: 12630
			UpdateKey = 8U,
			// Token: 0x04003157 RID: 12631
			Archivable = 16384U
		}

		// Token: 0x02000A5A RID: 2650
		[Flags]
		internal enum CertKeyUsage : uint
		{
			// Token: 0x04003159 RID: 12633
			EncipherOnly = 1U,
			// Token: 0x0400315A RID: 12634
			CrlSign = 2U,
			// Token: 0x0400315B RID: 12635
			KeyCertSign = 4U,
			// Token: 0x0400315C RID: 12636
			KeyAgreement = 8U,
			// Token: 0x0400315D RID: 12637
			DataEncipherment = 16U,
			// Token: 0x0400315E RID: 12638
			KeyEncipherment = 32U,
			// Token: 0x0400315F RID: 12639
			NonRepudiation = 64U,
			// Token: 0x04003160 RID: 12640
			DigitalSignature = 128U,
			// Token: 0x04003161 RID: 12641
			DecipherOnly = 32768U
		}

		// Token: 0x02000A5B RID: 2651
		internal enum CertNameType : uint
		{
			// Token: 0x04003163 RID: 12643
			Email = 1U,
			// Token: 0x04003164 RID: 12644
			Rdn,
			// Token: 0x04003165 RID: 12645
			Attr,
			// Token: 0x04003166 RID: 12646
			SimpleDisplay,
			// Token: 0x04003167 RID: 12647
			FriendlyDisplay,
			// Token: 0x04003168 RID: 12648
			Dns,
			// Token: 0x04003169 RID: 12649
			Url,
			// Token: 0x0400316A RID: 12650
			Upn
		}

		// Token: 0x02000A5C RID: 2652
		internal enum CertAltNameType : uint
		{
			// Token: 0x0400316C RID: 12652
			OtherName = 1U,
			// Token: 0x0400316D RID: 12653
			Rfc822Name,
			// Token: 0x0400316E RID: 12654
			DnsName,
			// Token: 0x0400316F RID: 12655
			X400Address,
			// Token: 0x04003170 RID: 12656
			DirectoryName,
			// Token: 0x04003171 RID: 12657
			EdiPartyName,
			// Token: 0x04003172 RID: 12658
			Url,
			// Token: 0x04003173 RID: 12659
			IpAddress,
			// Token: 0x04003174 RID: 12660
			RegisteredId
		}

		// Token: 0x02000A5D RID: 2653
		internal enum KeySpec
		{
			// Token: 0x04003176 RID: 12662
			KeyExchange = 1,
			// Token: 0x04003177 RID: 12663
			Signature
		}

		// Token: 0x02000A5E RID: 2654
		internal enum AlgorithmId : uint
		{
			// Token: 0x04003179 RID: 12665
			RsaKeyExchange = 41984U,
			// Token: 0x0400317A RID: 12666
			DsaSignature = 8704U,
			// Token: 0x0400317B RID: 12667
			DiffieHellmanStoreAndForward = 43521U,
			// Token: 0x0400317C RID: 12668
			DiffieHellmanEphemeral,
			// Token: 0x0400317D RID: 12669
			Sha256 = 32780U,
			// Token: 0x0400317E RID: 12670
			Sha512 = 32782U,
			// Token: 0x0400317F RID: 12671
			CAlgOIDInfoParameters = 4294967294U
		}

		// Token: 0x02000A5F RID: 2655
		internal enum HashParameter : uint
		{
			// Token: 0x04003181 RID: 12673
			AlgorithmId = 1U,
			// Token: 0x04003182 RID: 12674
			HashValue,
			// Token: 0x04003183 RID: 12675
			HashSize = 4U
		}

		// Token: 0x02000A60 RID: 2656
		[Flags]
		internal enum EnhancedKeyUsageSearch : uint
		{
			// Token: 0x04003185 RID: 12677
			ExtensionAndProperty = 0U,
			// Token: 0x04003186 RID: 12678
			Extension = 2U,
			// Token: 0x04003187 RID: 12679
			Property = 4U
		}

		// Token: 0x02000A61 RID: 2657
		internal enum EncodeDecodeObjectType : uint
		{
			// Token: 0x04003189 RID: 12681
			X509Cert = 1U,
			// Token: 0x0400318A RID: 12682
			X509CertToBeSigned,
			// Token: 0x0400318B RID: 12683
			X509CertCrlToBeSigned,
			// Token: 0x0400318C RID: 12684
			X509CertRequestToBeSigned,
			// Token: 0x0400318D RID: 12685
			X509Extensions,
			// Token: 0x0400318E RID: 12686
			X509AnyString,
			// Token: 0x0400318F RID: 12687
			X509NameValue = 6U,
			// Token: 0x04003190 RID: 12688
			X509Name,
			// Token: 0x04003191 RID: 12689
			X509PublicKeyInfo,
			// Token: 0x04003192 RID: 12690
			X509AuthorityKeyId,
			// Token: 0x04003193 RID: 12691
			X509KeyAttributes,
			// Token: 0x04003194 RID: 12692
			X509KeyUsageRestriction,
			// Token: 0x04003195 RID: 12693
			X509AlternateName,
			// Token: 0x04003196 RID: 12694
			X509BasicConstraints,
			// Token: 0x04003197 RID: 12695
			X509KeyUsage,
			// Token: 0x04003198 RID: 12696
			X509BasicConstraints2,
			// Token: 0x04003199 RID: 12697
			X509CertPolicies,
			// Token: 0x0400319A RID: 12698
			pkcsUtcTime,
			// Token: 0x0400319B RID: 12699
			PkcsTimeRequest,
			// Token: 0x0400319C RID: 12700
			RsaCspPublickeyblob,
			// Token: 0x0400319D RID: 12701
			X509UnicodeName,
			// Token: 0x0400319E RID: 12702
			X509KeygenRequestToBeSigned,
			// Token: 0x0400319F RID: 12703
			PkcsAttribute,
			// Token: 0x040031A0 RID: 12704
			PkcsContentInfoSequenceOfAny,
			// Token: 0x040031A1 RID: 12705
			X509UnicodeAnyString,
			// Token: 0x040031A2 RID: 12706
			X509UnicodeNameValue = 24U,
			// Token: 0x040031A3 RID: 12707
			X509OctetString,
			// Token: 0x040031A4 RID: 12708
			X509Bits,
			// Token: 0x040031A5 RID: 12709
			X509Integer,
			// Token: 0x040031A6 RID: 12710
			X509MultiByteInteger,
			// Token: 0x040031A7 RID: 12711
			X509Enumerated,
			// Token: 0x040031A8 RID: 12712
			X509CrlReasonCode = 29U,
			// Token: 0x040031A9 RID: 12713
			X509ChoiceOfTime,
			// Token: 0x040031AA RID: 12714
			X509AuthorityKeyId2,
			// Token: 0x040031AB RID: 12715
			X509AuthorityInfoAccess,
			// Token: 0x040031AC RID: 12716
			PkcsContentInfo,
			// Token: 0x040031AD RID: 12717
			X509SequenceOfAny,
			// Token: 0x040031AE RID: 12718
			X509CrlDistPoints,
			// Token: 0x040031AF RID: 12719
			X509EnhancedKeyUsage,
			// Token: 0x040031B0 RID: 12720
			PkcsCtl,
			// Token: 0x040031B1 RID: 12721
			X509DssPublickey,
			// Token: 0x040031B2 RID: 12722
			X509MultiByteUint = 38U,
			// Token: 0x040031B3 RID: 12723
			X509DssParameters,
			// Token: 0x040031B4 RID: 12724
			X509DssSignature,
			// Token: 0x040031B5 RID: 12725
			PkcsRc2CbcParameters,
			// Token: 0x040031B6 RID: 12726
			pkcsSmimeCapabilities,
			// Token: 0x040031B7 RID: 12727
			X509CertPair = 53U,
			// Token: 0x040031B8 RID: 12728
			X509IssuingDistPoint,
			// Token: 0x040031B9 RID: 12729
			X509NameConstraints,
			// Token: 0x040031BA RID: 12730
			X509PolicyMappings,
			// Token: 0x040031BB RID: 12731
			X509PolicyConstraints,
			// Token: 0x040031BC RID: 12732
			X509CrossCertDistPoints,
			// Token: 0x040031BD RID: 12733
			cmcData,
			// Token: 0x040031BE RID: 12734
			cmcResponse,
			// Token: 0x040031BF RID: 12735
			cmcStatus,
			// Token: 0x040031C0 RID: 12736
			cmcAddExtensions,
			// Token: 0x040031C1 RID: 12737
			cmcAddAttributes,
			// Token: 0x040031C2 RID: 12738
			X509CertificateTemplate,
			// Token: 0x040031C3 RID: 12739
			Pkcs7SignerInfo = 500U,
			// Token: 0x040031C4 RID: 12740
			cmsSignerInfo
		}

		// Token: 0x02000A62 RID: 2658
		internal enum KeyParameter : uint
		{
			// Token: 0x040031C6 RID: 12742
			KeyLength = 9U
		}

		// Token: 0x02000A63 RID: 2659
		private enum AlgorithmClass : uint
		{
			// Token: 0x040031C8 RID: 12744
			KeyExchange = 40960U,
			// Token: 0x040031C9 RID: 12745
			Signature = 8192U,
			// Token: 0x040031CA RID: 12746
			Hash = 32768U
		}

		// Token: 0x02000A64 RID: 2660
		private enum Algorithm : uint
		{
			// Token: 0x040031CC RID: 12748
			Any,
			// Token: 0x040031CD RID: 12749
			Dss = 512U,
			// Token: 0x040031CE RID: 12750
			Rsa = 1024U,
			// Token: 0x040031CF RID: 12751
			Block = 1536U,
			// Token: 0x040031D0 RID: 12752
			Stream = 2048U,
			// Token: 0x040031D1 RID: 12753
			DiffieHellman = 2560U,
			// Token: 0x040031D2 RID: 12754
			SecureChannel = 3072U
		}

		// Token: 0x02000A65 RID: 2661
		private enum DssSubId : uint
		{
			// Token: 0x040031D4 RID: 12756
			Any
		}

		// Token: 0x02000A66 RID: 2662
		private enum RsaSubId : uint
		{
			// Token: 0x040031D6 RID: 12758
			Any
		}

		// Token: 0x02000A67 RID: 2663
		private enum DiffieHellmanSubId : uint
		{
			// Token: 0x040031D8 RID: 12760
			StoreAndForward = 1U,
			// Token: 0x040031D9 RID: 12761
			Ephemeral
		}

		// Token: 0x02000A68 RID: 2664
		private enum HashSubId : uint
		{
			// Token: 0x040031DB RID: 12763
			Sha1 = 4U,
			// Token: 0x040031DC RID: 12764
			Mac,
			// Token: 0x040031DD RID: 12765
			HMac = 9U,
			// Token: 0x040031DE RID: 12766
			Sha256 = 12U,
			// Token: 0x040031DF RID: 12767
			Sha384,
			// Token: 0x040031E0 RID: 12768
			Sha512
		}

		// Token: 0x02000A69 RID: 2665
		internal enum StoreControl : uint
		{
			// Token: 0x040031E2 RID: 12770
			Resync = 1U,
			// Token: 0x040031E3 RID: 12771
			NotifiyChange,
			// Token: 0x040031E4 RID: 12772
			Commit,
			// Token: 0x040031E5 RID: 12773
			AutoResync,
			// Token: 0x040031E6 RID: 12774
			CancelNotifyChange
		}

		// Token: 0x02000A6A RID: 2666
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CryptKeyProvInfo
		{
			// Token: 0x040031E7 RID: 12775
			public string ContainerName;

			// Token: 0x040031E8 RID: 12776
			public string ProviderName;

			// Token: 0x040031E9 RID: 12777
			public CapiNativeMethods.ProviderType ProviderType;

			// Token: 0x040031EA RID: 12778
			public CapiNativeMethods.AcquireContext Flags;

			// Token: 0x040031EB RID: 12779
			public uint CountParameters;

			// Token: 0x040031EC RID: 12780
			public IntPtr Parameters;

			// Token: 0x040031ED RID: 12781
			public CapiNativeMethods.KeySpec KeySpec;
		}

		// Token: 0x02000A6B RID: 2667
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CertAltNameDnsEntry
		{
			// Token: 0x040031EE RID: 12782
			public static readonly int MarshalSize = Marshal.SizeOf(typeof(CapiNativeMethods.CertAltNameDnsEntry));

			// Token: 0x040031EF RID: 12783
			public CapiNativeMethods.CertAltNameType Type;

			// Token: 0x040031F0 RID: 12784
			public string Name;

			// Token: 0x040031F1 RID: 12785
			private uint padding;
		}

		// Token: 0x02000A6C RID: 2668
		internal struct CertUsageMatch
		{
			// Token: 0x060039F2 RID: 14834 RVA: 0x00092F83 File Offset: 0x00091183
			public CertUsageMatch(CapiNativeMethods.CertUsageMatch.Operator flags, CapiNativeMethods.CryptoApiBlob usage)
			{
				this.type = flags;
				this.usage = usage;
			}

			// Token: 0x040031F2 RID: 12786
			public static CapiNativeMethods.CertUsageMatch Empty = new CapiNativeMethods.CertUsageMatch(CapiNativeMethods.CertUsageMatch.Operator.And, CapiNativeMethods.CryptoApiBlob.Empty);

			// Token: 0x040031F3 RID: 12787
			public CapiNativeMethods.CertUsageMatch.Operator type;

			// Token: 0x040031F4 RID: 12788
			public CapiNativeMethods.CryptoApiBlob usage;

			// Token: 0x02000A6D RID: 2669
			public enum Operator : uint
			{
				// Token: 0x040031F6 RID: 12790
				And,
				// Token: 0x040031F7 RID: 12791
				Or
			}
		}

		// Token: 0x02000A6E RID: 2670
		internal struct CertTrustStatus
		{
			// Token: 0x040031F8 RID: 12792
			public Microsoft.Exchange.Security.Cryptography.X509Certificates.TrustStatus error;

			// Token: 0x040031F9 RID: 12793
			public TrustInformation information;
		}

		// Token: 0x02000A6F RID: 2671
		internal struct CTLEntry
		{
			// Token: 0x040031FA RID: 12794
			public CapiNativeMethods.CryptoApiBlob subjectIdentifier;

			// Token: 0x040031FB RID: 12795
			public int attributeCount;

			// Token: 0x040031FC RID: 12796
			public IntPtr attributes;
		}

		// Token: 0x02000A70 RID: 2672
		internal struct CertTrustListInfo
		{
			// Token: 0x040031FD RID: 12797
			private uint size;

			// Token: 0x040031FE RID: 12798
			public IntPtr pCtlEntry;

			// Token: 0x040031FF RID: 12799
			public IntPtr pCtlContext;
		}

		// Token: 0x02000A71 RID: 2673
		internal struct CryptoApiBlob
		{
			// Token: 0x060039F4 RID: 14836 RVA: 0x00092FA5 File Offset: 0x000911A5
			public CryptoApiBlob(uint count, SafeHGlobalHandleBase handle)
			{
				this.Count = count;
				this.DataPointer = handle.DangerousGetHandle();
			}

			// Token: 0x04003200 RID: 12800
			public static readonly int MarshalSize = Marshal.SizeOf(typeof(CapiNativeMethods.CryptoApiBlob));

			// Token: 0x04003201 RID: 12801
			public static CapiNativeMethods.CryptoApiBlob Empty = new CapiNativeMethods.CryptoApiBlob(0U, SafeHGlobalHandle.InvalidHandle);

			// Token: 0x04003202 RID: 12802
			public uint Count;

			// Token: 0x04003203 RID: 12803
			public IntPtr DataPointer;
		}

		// Token: 0x02000A72 RID: 2674
		internal struct CertChainParameter
		{
			// Token: 0x060039F6 RID: 14838 RVA: 0x00092FE0 File Offset: 0x000911E0
			public CertChainParameter(CapiNativeMethods.CertUsageMatch match, TimeSpan timeout, bool overrideCRLTime, TimeSpan freshnessTime)
			{
				this.size = CapiNativeMethods.CertChainParameter.MarshalSize;
				this.requestedUsage = match;
				this.requestedIssuancePolicy = CapiNativeMethods.CertUsageMatch.Empty;
				this.urlRetrievalTimeout = (int)timeout.TotalMilliseconds;
				this.checkRevocationFreshnessTime = overrideCRLTime;
				this.revocationFreshnessTime = (int)freshnessTime.TotalSeconds;
			}

			// Token: 0x04003204 RID: 12804
			private static readonly int MarshalSize = Marshal.SizeOf(typeof(CapiNativeMethods.CertChainParameter));

			// Token: 0x04003205 RID: 12805
			private int size;

			// Token: 0x04003206 RID: 12806
			private CapiNativeMethods.CertUsageMatch requestedUsage;

			// Token: 0x04003207 RID: 12807
			private CapiNativeMethods.CertUsageMatch requestedIssuancePolicy;

			// Token: 0x04003208 RID: 12808
			private int urlRetrievalTimeout;

			// Token: 0x04003209 RID: 12809
			[MarshalAs(UnmanagedType.Bool)]
			private bool checkRevocationFreshnessTime;

			// Token: 0x0400320A RID: 12810
			private int revocationFreshnessTime;
		}

		// Token: 0x02000A73 RID: 2675
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct CryptAlgorithmIdentifier
		{
			// Token: 0x0400320B RID: 12811
			[MarshalAs(UnmanagedType.LPStr)]
			internal string ObjectId;

			// Token: 0x0400320C RID: 12812
			internal CapiNativeMethods.CryptoApiBlob Parameters;
		}

		// Token: 0x02000A74 RID: 2676
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct CertExtension
		{
			// Token: 0x0400320D RID: 12813
			public static readonly int MarshalSize = Marshal.SizeOf(typeof(CapiNativeMethods.CertExtension));

			// Token: 0x0400320E RID: 12814
			public IntPtr ObjectId;

			// Token: 0x0400320F RID: 12815
			[MarshalAs(UnmanagedType.Bool)]
			public bool IsCritical;

			// Token: 0x04003210 RID: 12816
			public CapiNativeMethods.CryptoApiBlob Value;
		}

		// Token: 0x02000A75 RID: 2677
		private struct CertContext
		{
			// Token: 0x04003211 RID: 12817
			public uint CertEncodingType;

			// Token: 0x04003212 RID: 12818
			public IntPtr CertEncoded;

			// Token: 0x04003213 RID: 12819
			public uint CertEncodedSize;

			// Token: 0x04003214 RID: 12820
			public IntPtr CertInfo;

			// Token: 0x04003215 RID: 12821
			public IntPtr CertStore;
		}

		// Token: 0x02000A76 RID: 2678
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct CryptOidInfo
		{
			// Token: 0x060039F9 RID: 14841 RVA: 0x00093059 File Offset: 0x00091259
			public CryptOidInfo(int size)
			{
				this.Size = (uint)size;
				this.OID = null;
				this.Name = null;
				this.GroupId = 0U;
				this.Algid = 0U;
				this.ExtraInfo = default(CapiNativeMethods.CryptoApiBlob);
			}

			// Token: 0x04003216 RID: 12822
			public static readonly int MarshalSize = Marshal.SizeOf(typeof(CapiNativeMethods.CryptOidInfo));

			// Token: 0x04003217 RID: 12823
			public uint Size;

			// Token: 0x04003218 RID: 12824
			[MarshalAs(UnmanagedType.LPStr)]
			public string OID;

			// Token: 0x04003219 RID: 12825
			[MarshalAs(UnmanagedType.LPWStr)]
			public string Name;

			// Token: 0x0400321A RID: 12826
			public uint GroupId;

			// Token: 0x0400321B RID: 12827
			public uint Algid;

			// Token: 0x0400321C RID: 12828
			public CapiNativeMethods.CryptoApiBlob ExtraInfo;
		}

		// Token: 0x02000A77 RID: 2679
		public struct CertChainPolicyParameters
		{
			// Token: 0x060039FB RID: 14843 RVA: 0x000930A0 File Offset: 0x000912A0
			public CertChainPolicyParameters(ChainPolicyOptions flags)
			{
				this.size = CapiNativeMethods.CertChainPolicyParameters.marshalSize;
				this.flags = flags;
				this.extraPolicyPara = IntPtr.Zero;
			}

			// Token: 0x17000E70 RID: 3696
			// (get) Token: 0x060039FC RID: 14844 RVA: 0x000930BF File Offset: 0x000912BF
			// (set) Token: 0x060039FD RID: 14845 RVA: 0x000930C7 File Offset: 0x000912C7
			public IntPtr ExtraPolicy
			{
				get
				{
					return this.extraPolicyPara;
				}
				set
				{
					this.extraPolicyPara = value;
				}
			}

			// Token: 0x0400321D RID: 12829
			private static uint marshalSize = (uint)Marshal.SizeOf(typeof(CapiNativeMethods.CertChainPolicyParameters));

			// Token: 0x0400321E RID: 12830
			private uint size;

			// Token: 0x0400321F RID: 12831
			private ChainPolicyOptions flags;

			// Token: 0x04003220 RID: 12832
			private IntPtr extraPolicyPara;
		}

		// Token: 0x02000A78 RID: 2680
		public struct CertChainPolicyStatus
		{
			// Token: 0x17000E71 RID: 3697
			// (get) Token: 0x060039FF RID: 14847 RVA: 0x000930E6 File Offset: 0x000912E6
			public ChainValidityStatus Status
			{
				get
				{
					return this.status;
				}
			}

			// Token: 0x06003A00 RID: 14848 RVA: 0x000930F0 File Offset: 0x000912F0
			public static CapiNativeMethods.CertChainPolicyStatus Create()
			{
				return new CapiNativeMethods.CertChainPolicyStatus
				{
					size = CapiNativeMethods.CertChainPolicyStatus.MarshalSize,
					status = ChainValidityStatus.Valid,
					chainIndex = 0,
					elementIndex = 0,
					extraPolicyStatus = IntPtr.Zero
				};
			}

			// Token: 0x04003221 RID: 12833
			private static uint MarshalSize = (uint)Marshal.SizeOf(typeof(CapiNativeMethods.CertChainPolicyStatus));

			// Token: 0x04003222 RID: 12834
			private uint size;

			// Token: 0x04003223 RID: 12835
			private ChainValidityStatus status;

			// Token: 0x04003224 RID: 12836
			private int chainIndex;

			// Token: 0x04003225 RID: 12837
			private int elementIndex;

			// Token: 0x04003226 RID: 12838
			private IntPtr extraPolicyStatus;
		}

		// Token: 0x02000A79 RID: 2681
		public struct CryptBitBlob
		{
			// Token: 0x06003A02 RID: 14850 RVA: 0x0009314C File Offset: 0x0009134C
			public CryptBitBlob(uint size, IntPtr data)
			{
				this.size = size;
				this.data = data;
				this.unusedBitsSize = 0U;
			}

			// Token: 0x04003227 RID: 12839
			private uint size;

			// Token: 0x04003228 RID: 12840
			private IntPtr data;

			// Token: 0x04003229 RID: 12841
			private uint unusedBitsSize;
		}

		// Token: 0x02000A7A RID: 2682
		public struct CertPublicKeyInfo
		{
			// Token: 0x06003A03 RID: 14851 RVA: 0x00093163 File Offset: 0x00091363
			public CertPublicKeyInfo(string objectId, CapiNativeMethods.CryptoApiBlob algorithmParams, CapiNativeMethods.CryptBitBlob publicKey)
			{
				this.algorithm.ObjectId = objectId;
				this.algorithm.Parameters = algorithmParams;
				this.publicKey = publicKey;
			}

			// Token: 0x06003A04 RID: 14852 RVA: 0x00093184 File Offset: 0x00091384
			public static byte[] Encode(X509Certificate2 certificate)
			{
				byte[] array;
				using (SafeHGlobalHandle safeHGlobalHandle = NativeMethods.AllocHGlobal(certificate.PublicKey.EncodedParameters.RawData.Length))
				{
					Marshal.Copy(certificate.PublicKey.EncodedParameters.RawData, 0, safeHGlobalHandle.DangerousGetHandle(), certificate.PublicKey.EncodedParameters.RawData.Length);
					CapiNativeMethods.CryptoApiBlob algorithmParams = new CapiNativeMethods.CryptoApiBlob((uint)certificate.PublicKey.EncodedParameters.RawData.Length, safeHGlobalHandle);
					using (SafeHGlobalHandle safeHGlobalHandle2 = NativeMethods.AllocHGlobal(certificate.PublicKey.EncodedKeyValue.RawData.Length))
					{
						Marshal.Copy(certificate.PublicKey.EncodedKeyValue.RawData, 0, safeHGlobalHandle2.DangerousGetHandle(), certificate.PublicKey.EncodedKeyValue.RawData.Length);
						CapiNativeMethods.CryptBitBlob cryptBitBlob = new CapiNativeMethods.CryptBitBlob((uint)certificate.PublicKey.EncodedKeyValue.RawData.Length, safeHGlobalHandle2.DangerousGetHandle());
						CapiNativeMethods.CertPublicKeyInfo certPublicKeyInfo = new CapiNativeMethods.CertPublicKeyInfo(certificate.PublicKey.Oid.Value, algorithmParams, cryptBitBlob);
						uint num = 0U;
						if (!CapiNativeMethods.CryptEncodeObject(CapiNativeMethods.EncodeType.X509Asn | CapiNativeMethods.EncodeType.Pkcs7Asn, CapiNativeMethods.x509PublicKeyInfo, ref certPublicKeyInfo, null, ref num))
						{
							throw new CryptographicException(Marshal.GetLastWin32Error());
						}
						array = new byte[num];
						if (!CapiNativeMethods.CryptEncodeObject(CapiNativeMethods.EncodeType.X509Asn | CapiNativeMethods.EncodeType.Pkcs7Asn, CapiNativeMethods.x509PublicKeyInfo, ref certPublicKeyInfo, array, ref num))
						{
							throw new CryptographicException(Marshal.GetLastWin32Error());
						}
					}
				}
				return array;
			}

			// Token: 0x0400322A RID: 12842
			private CapiNativeMethods.CryptAlgorithmIdentifier algorithm;

			// Token: 0x0400322B RID: 12843
			private CapiNativeMethods.CryptBitBlob publicKey;
		}

		// Token: 0x02000A7B RID: 2683
		public struct CertInfo
		{
			// Token: 0x06003A05 RID: 14853 RVA: 0x00093318 File Offset: 0x00091518
			public static CapiNativeMethods.CertInfo Create(X509Certificate2 certificate)
			{
				return (CapiNativeMethods.CertInfo)Marshal.PtrToStructure(((CapiNativeMethods.CertContext)Marshal.PtrToStructure(certificate.Handle, typeof(CapiNativeMethods.CertContext))).CertInfo, typeof(CapiNativeMethods.CertInfo));
			}

			// Token: 0x06003A06 RID: 14854 RVA: 0x0009335C File Offset: 0x0009155C
			public byte[] GetSerialNumberRawData()
			{
				byte[] array = new byte[this.serialNumber.Count];
				Marshal.Copy(this.serialNumber.DataPointer, array, 0, (int)this.serialNumber.Count);
				return array;
			}

			// Token: 0x0400322C RID: 12844
			private uint version;

			// Token: 0x0400322D RID: 12845
			private CapiNativeMethods.CryptoApiBlob serialNumber;

			// Token: 0x0400322E RID: 12846
			private CapiNativeMethods.CryptAlgorithmIdentifier signatureAlgorithm;

			// Token: 0x0400322F RID: 12847
			private CapiNativeMethods.CryptoApiBlob issuer;

			// Token: 0x04003230 RID: 12848
			private System.Runtime.InteropServices.ComTypes.FILETIME notBefore;

			// Token: 0x04003231 RID: 12849
			private System.Runtime.InteropServices.ComTypes.FILETIME notAfter;

			// Token: 0x04003232 RID: 12850
			private CapiNativeMethods.CryptoApiBlob subject;

			// Token: 0x04003233 RID: 12851
			private CapiNativeMethods.CertPublicKeyInfo subjectPublicKeyInfo;

			// Token: 0x04003234 RID: 12852
			private CapiNativeMethods.CryptBitBlob issuerUniqueId;

			// Token: 0x04003235 RID: 12853
			private CapiNativeMethods.CryptBitBlob subjectUniqueId;

			// Token: 0x04003236 RID: 12854
			private uint cExtension;

			// Token: 0x04003237 RID: 12855
			private CapiNativeMethods.CertExtension rgExtension;
		}

		// Token: 0x02000A7C RID: 2684
		internal struct CertRdnAttribute
		{
			// Token: 0x04003238 RID: 12856
			[MarshalAs(UnmanagedType.LPStr)]
			public string OID;

			// Token: 0x04003239 RID: 12857
			public int dwValueType;

			// Token: 0x0400323A RID: 12858
			public CapiNativeMethods.CryptoApiBlob value;
		}

		// Token: 0x02000A7D RID: 2685
		[Flags]
		public enum DPAPIFlags : uint
		{
			// Token: 0x0400323C RID: 12860
			CRYPTPROTECT_UI_FORBIDDEN = 1U,
			// Token: 0x0400323D RID: 12861
			CRYPTPROTECT_LOCAL_MACHINE = 4U
		}
	}
}
