using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Provisioning
{
	// Token: 0x020002AE RID: 686
	internal class NativeMethods
	{
		// Token: 0x0600136C RID: 4972 RVA: 0x000893A2 File Offset: 0x000875A2
		internal NativeMethods()
		{
			NativeMethods.Initialize();
		}

		// Token: 0x0600136D RID: 4973
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] [In] string dllname);

		// Token: 0x0600136E RID: 4974
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr GetProcAddress([In] IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] [In] string procname);

		// Token: 0x0600136F RID: 4975 RVA: 0x000893AF File Offset: 0x000875AF
		internal virtual int CloseIdentityHandle([In] IntPtr hIdentity)
		{
			return NativeMethods.closeIdentityHandleFuncPtr(hIdentity);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x000893BC File Offset: 0x000875BC
		internal virtual int LogonIdentityExSSO([In] IntPtr hIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string authPolicy, [In] uint dwAuthFlags, [In] uint dwSSOFlags, [In] [Out] [Optional] NativeMethods.PCUIParam2 pcUIParam2, [MarshalAs(UnmanagedType.LPArray)] [In] NativeMethods.RstParams[] pcRSTParams, [In] uint dwpcRSTParamsCount)
		{
			return NativeMethods.logonIdentityExSSOFuncPtr(hIdentity, authPolicy, dwAuthFlags, dwSSOFlags, pcUIParam2, pcRSTParams, dwpcRSTParamsCount);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x000893D3 File Offset: 0x000875D3
		internal virtual int LogonIdentityEx([In] IntPtr hIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string authPolicy, [In] uint dwAuthFlags, [MarshalAs(UnmanagedType.LPArray)] [In] NativeMethods.RstParams[] pcRSTParams, [In] uint dwpcRSTParamsCount)
		{
			return NativeMethods.logonIdentityExFuncPtr(hIdentity, authPolicy, dwAuthFlags, pcRSTParams, dwpcRSTParamsCount);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x000893E6 File Offset: 0x000875E6
		internal virtual int GetAuthenticationStatus([In] IntPtr hIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] string wzServiceTarget, [In] uint dwVersion, out IntPtr ppStatus)
		{
			return NativeMethods.getAuthenticationStatusFuncPtr(hIdentity, wzServiceTarget, dwVersion, out ppStatus);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x000893F7 File Offset: 0x000875F7
		internal virtual int PassportFreeMemory([In] [Out] IntPtr pMemoryToFree)
		{
			return NativeMethods.passportFreeMemoryFuncPtr(pMemoryToFree);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00089404 File Offset: 0x00087604
		internal virtual int AuthIdentityToService([In] IntPtr hIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] string szServiceTarget, [MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string szServicePolicy, [In] uint dwTokenRequestFlags, out IntPtr szToken, out uint pdwResultFlags, out IntPtr ppbSessionKey, out uint pcbSessionKeyLength)
		{
			return NativeMethods.authIdentityToServiceFuncPtr(hIdentity, szServiceTarget, szServicePolicy, dwTokenRequestFlags, out szToken, out pdwResultFlags, out ppbSessionKey, out pcbSessionKeyLength);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00089428 File Offset: 0x00087628
		internal virtual int SetCredential([In] IntPtr hIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszCredType, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszCredValue)
		{
			return NativeMethods.setCredentialFuncPtr(hIdentity, wszCredType, wszCredValue);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00089437 File Offset: 0x00087637
		internal virtual int CreateIdentityHandle2([MarshalAs(UnmanagedType.LPWStr)] [In] string wszFederationProvider, [MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string wszMemberName, [In] uint dwFlags, out IntPtr pihIdentity)
		{
			return NativeMethods.createIdentityHandle2FuncPtr(wszFederationProvider, wszMemberName, dwFlags, out pihIdentity);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00089448 File Offset: 0x00087648
		internal virtual int InitializeEx([In] ref Guid guid, [In] int lPPCRLVersion, [In] uint dwFlags, [MarshalAs(UnmanagedType.LPArray)] [In] NativeMethods.IdcrlOption[] pOptions, [In] uint dwOptions)
		{
			return NativeMethods.initializeExFuncPtr(ref guid, lPPCRLVersion, dwFlags, pOptions, dwOptions);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0008945B File Offset: 0x0008765B
		internal virtual int EnumIdentitiesWithCachedCredentials([MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string wszCachedCredType, out IntPtr peihEnumHandle)
		{
			return NativeMethods.enumIdentitiesWithCachedCredentialsFuncPtr(wszCachedCredType, out peihEnumHandle);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0008946C File Offset: 0x0008766C
		internal virtual int NextIdentity(IntPtr hEnumHandle, ref string wszMemberName)
		{
			IntPtr ptr = 0;
			int result = NativeMethods.nextIdentityFuncPtr(hEnumHandle, ref ptr);
			wszMemberName = null;
			wszMemberName = Marshal.PtrToStringUni(ptr);
			return result;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0008949B File Offset: 0x0008769B
		internal virtual int CloseEnumIdentitiesHandle(IntPtr hEnumHandle)
		{
			return NativeMethods.closeEnumIdentitiesHandleFuncPtr(hEnumHandle);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000894A8 File Offset: 0x000876A8
		internal virtual int Uninitialize()
		{
			return NativeMethods.uninitializeFuncPtr();
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x000894B4 File Offset: 0x000876B4
		private static string GetIdentityCrlDllPath()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\MSOIdentityCRL");
			string path = (string)registryKey.GetValue("TargetDir");
			return Path.Combine(path, "msoidcli.dll");
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x000894F0 File Offset: 0x000876F0
		private static Delegate GetFunctionPointer(IntPtr msoidcli, string methodName, Type delegateType)
		{
			IntPtr procAddress = NativeMethods.GetProcAddress(msoidcli, methodName);
			if (procAddress == IntPtr.Zero)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw new Exception(string.Format(CultureInfo.InvariantCulture, "Failed to get address for method: {0} from library: {1}. GetLastError code: {2}", new object[]
				{
					methodName,
					NativeMethods.identityCrlDllPath,
					lastWin32Error
				}));
			}
			return Marshal.GetDelegateForFunctionPointer(procAddress, delegateType);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00089554 File Offset: 0x00087754
		private static void Initialize()
		{
			if (NativeMethods.initialized)
			{
				return;
			}
			lock (NativeMethods.syncObject)
			{
				if (!NativeMethods.initialized)
				{
					NativeMethods.identityCrlDllPath = NativeMethods.GetIdentityCrlDllPath();
					IntPtr intPtr = NativeMethods.LoadLibrary(NativeMethods.identityCrlDllPath);
					if (intPtr == IntPtr.Zero)
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						throw new Exception(string.Format(CultureInfo.InvariantCulture, "Failed to load library: {0}. GetLastError code: {1}", new object[]
						{
							NativeMethods.identityCrlDllPath,
							lastWin32Error
						}));
					}
					NativeMethods.closeIdentityHandleFuncPtr = (NativeMethods._CloseIdentityHandle)NativeMethods.GetFunctionPointer(intPtr, "CloseIdentityHandle", typeof(NativeMethods._CloseIdentityHandle));
					NativeMethods.logonIdentityExSSOFuncPtr = (NativeMethods._LogonIdentityExSSO)NativeMethods.GetFunctionPointer(intPtr, "LogonIdentityExSSO", typeof(NativeMethods._LogonIdentityExSSO));
					NativeMethods.logonIdentityExFuncPtr = (NativeMethods._LogonIdentityEx)NativeMethods.GetFunctionPointer(intPtr, "LogonIdentityEx", typeof(NativeMethods._LogonIdentityEx));
					NativeMethods.getAuthenticationStatusFuncPtr = (NativeMethods._GetAuthenticationStatus)NativeMethods.GetFunctionPointer(intPtr, "GetAuthenticationStatus", typeof(NativeMethods._GetAuthenticationStatus));
					NativeMethods.passportFreeMemoryFuncPtr = (NativeMethods._PassportFreeMemory)NativeMethods.GetFunctionPointer(intPtr, "PassportFreeMemory", typeof(NativeMethods._PassportFreeMemory));
					NativeMethods.authIdentityToServiceFuncPtr = (NativeMethods._AuthIdentityToService)NativeMethods.GetFunctionPointer(intPtr, "AuthIdentityToService", typeof(NativeMethods._AuthIdentityToService));
					NativeMethods.setCredentialFuncPtr = (NativeMethods._SetCredential)NativeMethods.GetFunctionPointer(intPtr, "SetCredential", typeof(NativeMethods._SetCredential));
					NativeMethods.createIdentityHandle2FuncPtr = (NativeMethods._CreateIdentityHandle2)NativeMethods.GetFunctionPointer(intPtr, "CreateIdentityHandle2", typeof(NativeMethods._CreateIdentityHandle2));
					NativeMethods.initializeExFuncPtr = (NativeMethods._InitializeEx)NativeMethods.GetFunctionPointer(intPtr, "InitializeEx", typeof(NativeMethods._InitializeEx));
					NativeMethods.uninitializeFuncPtr = (NativeMethods._Uninitialize)NativeMethods.GetFunctionPointer(intPtr, "Uninitialize", typeof(NativeMethods._Uninitialize));
					NativeMethods.enumIdentitiesWithCachedCredentialsFuncPtr = (NativeMethods._EnumIdentitiesWithCachedCredentials)NativeMethods.GetFunctionPointer(intPtr, "EnumIdentitiesWithCachedCredentials", typeof(NativeMethods._EnumIdentitiesWithCachedCredentials));
					NativeMethods.nextIdentityFuncPtr = (NativeMethods._NextIdentity)NativeMethods.GetFunctionPointer(intPtr, "NextIdentity", typeof(NativeMethods._NextIdentity));
					NativeMethods.closeEnumIdentitiesHandleFuncPtr = (NativeMethods._CloseEnumIdentitiesHandle)NativeMethods.GetFunctionPointer(intPtr, "CloseEnumIdentitiesHandle", typeof(NativeMethods._CloseEnumIdentitiesHandle));
					NativeMethods.initialized = true;
				}
			}
		}

		// Token: 0x04000EB5 RID: 3765
		private const string IdentityCrlRegistrySubKey = "Software\\Microsoft\\MSOIdentityCRL";

		// Token: 0x04000EB6 RID: 3766
		private const string IdentityCrlInstallPathRegKeyName = "TargetDir";

		// Token: 0x04000EB7 RID: 3767
		private const string IdentityCrlDllToLoadName = "msoidcli.dll";

		// Token: 0x04000EB8 RID: 3768
		private static bool initialized;

		// Token: 0x04000EB9 RID: 3769
		private static object syncObject = new object();

		// Token: 0x04000EBA RID: 3770
		private static string identityCrlDllPath = string.Empty;

		// Token: 0x04000EBB RID: 3771
		private static NativeMethods._CloseIdentityHandle closeIdentityHandleFuncPtr;

		// Token: 0x04000EBC RID: 3772
		private static NativeMethods._LogonIdentityExSSO logonIdentityExSSOFuncPtr;

		// Token: 0x04000EBD RID: 3773
		private static NativeMethods._LogonIdentityEx logonIdentityExFuncPtr;

		// Token: 0x04000EBE RID: 3774
		private static NativeMethods._GetAuthenticationStatus getAuthenticationStatusFuncPtr;

		// Token: 0x04000EBF RID: 3775
		private static NativeMethods._PassportFreeMemory passportFreeMemoryFuncPtr;

		// Token: 0x04000EC0 RID: 3776
		private static NativeMethods._AuthIdentityToService authIdentityToServiceFuncPtr;

		// Token: 0x04000EC1 RID: 3777
		private static NativeMethods._SetCredential setCredentialFuncPtr;

		// Token: 0x04000EC2 RID: 3778
		private static NativeMethods._CreateIdentityHandle2 createIdentityHandle2FuncPtr;

		// Token: 0x04000EC3 RID: 3779
		private static NativeMethods._InitializeEx initializeExFuncPtr;

		// Token: 0x04000EC4 RID: 3780
		private static NativeMethods._Uninitialize uninitializeFuncPtr;

		// Token: 0x04000EC5 RID: 3781
		private static NativeMethods._EnumIdentitiesWithCachedCredentials enumIdentitiesWithCachedCredentialsFuncPtr;

		// Token: 0x04000EC6 RID: 3782
		private static NativeMethods._NextIdentity nextIdentityFuncPtr;

		// Token: 0x04000EC7 RID: 3783
		private static NativeMethods._CloseEnumIdentitiesHandle closeEnumIdentitiesHandleFuncPtr;

		// Token: 0x020002AF RID: 687
		// (Invoke) Token: 0x06001381 RID: 4993
		private delegate int _CloseIdentityHandle([In] IntPtr hIdentity);

		// Token: 0x020002B0 RID: 688
		// (Invoke) Token: 0x06001385 RID: 4997
		private delegate int _LogonIdentityExSSO([In] IntPtr Identity, [MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string authPolicy, [In] uint dwAuthFlags, [In] uint dwSsoFlags, [In] [Out] [Optional] NativeMethods.PCUIParam2 pcUIParam2, [MarshalAs(UnmanagedType.LPArray)] [In] NativeMethods.RstParams[] pcRSTParams, [In] uint dwpcRSTParamsCount);

		// Token: 0x020002B1 RID: 689
		// (Invoke) Token: 0x06001389 RID: 5001
		private delegate int _LogonIdentityEx([In] IntPtr Identity, [MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string authPolicy, [In] uint dwAuthFlags, [MarshalAs(UnmanagedType.LPArray)] [In] NativeMethods.RstParams[] pcRSTParams, [In] uint dwpcRSTParamsCount);

		// Token: 0x020002B2 RID: 690
		// (Invoke) Token: 0x0600138D RID: 5005
		private delegate int _GetAuthenticationStatus([In] IntPtr hIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] string wzServiceTarget, [In] uint dwVersion, out IntPtr ppStatus);

		// Token: 0x020002B3 RID: 691
		// (Invoke) Token: 0x06001391 RID: 5009
		private delegate int _PassportFreeMemory([In] [Out] IntPtr pMemoryToFree);

		// Token: 0x020002B4 RID: 692
		// (Invoke) Token: 0x06001395 RID: 5013
		private delegate int _AuthIdentityToService([In] IntPtr hIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] string szServiceTarget, [MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string szServicePolicy, [In] uint dwTokenRequestFlags, out IntPtr szToken, out uint pdwResultFlags, out IntPtr ppbSessionKey, out uint pcbSessionKeyLength);

		// Token: 0x020002B5 RID: 693
		// (Invoke) Token: 0x06001399 RID: 5017
		private delegate int _CreateIdentityHandle2([MarshalAs(UnmanagedType.LPWStr)] [In] string wszFederationProvider, [MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string wszMemberName, [In] uint dwFlags, out IntPtr pihIdentity);

		// Token: 0x020002B6 RID: 694
		// (Invoke) Token: 0x0600139D RID: 5021
		private delegate int _SetCredential([In] IntPtr hIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszCredType, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszCredValue);

		// Token: 0x020002B7 RID: 695
		// (Invoke) Token: 0x060013A1 RID: 5025
		private delegate int _InitializeEx([In] ref Guid guid, [In] int lPPCRLVersion, [In] uint dwFlags, [MarshalAs(UnmanagedType.LPArray)] [In] NativeMethods.IdcrlOption[] pOptions, [In] uint dwOptions);

		// Token: 0x020002B8 RID: 696
		// (Invoke) Token: 0x060013A5 RID: 5029
		private delegate int _Uninitialize();

		// Token: 0x020002B9 RID: 697
		// (Invoke) Token: 0x060013A9 RID: 5033
		private delegate int _EnumIdentitiesWithCachedCredentials([MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string wszCachedCredType, out IntPtr peihEnumHandle);

		// Token: 0x020002BA RID: 698
		// (Invoke) Token: 0x060013AD RID: 5037
		private delegate int _NextIdentity(IntPtr hEnumHandle, ref IntPtr wszMemberName);

		// Token: 0x020002BB RID: 699
		// (Invoke) Token: 0x060013B1 RID: 5041
		private delegate int _CloseEnumIdentitiesHandle(IntPtr hEnumHandle);

		// Token: 0x020002BC RID: 700
		internal enum LogOnFlag
		{
			// Token: 0x04000EC9 RID: 3785
			LogOnIdentityAllBit = 511,
			// Token: 0x04000ECA RID: 3786
			LogOnIdentityDefault = 0,
			// Token: 0x04000ECB RID: 3787
			LogOnIdentityAllowOffline,
			// Token: 0x04000ECC RID: 3788
			LogOnIdentityForceOffline,
			// Token: 0x04000ECD RID: 3789
			LogOnIdentityCreateOfflineHash = 4,
			// Token: 0x04000ECE RID: 3790
			LogOnIdentityAllowPersistentCookies = 8,
			// Token: 0x04000ECF RID: 3791
			LogOnIdentityUseEasyIdAuth = 16,
			// Token: 0x04000ED0 RID: 3792
			LogOnIdentityUseLinkedAccounts = 32,
			// Token: 0x04000ED1 RID: 3793
			LogOnIdentityFederated = 64,
			// Token: 0x04000ED2 RID: 3794
			LogOnIdentityWindowsLiveId = 128,
			// Token: 0x04000ED3 RID: 3795
			LogOnIdentityAutoPartnerRedirect = 256
		}

		// Token: 0x020002BD RID: 701
		internal enum SsoFlag
		{
			// Token: 0x04000ED5 RID: 3797
			SsoAllBit = 15,
			// Token: 0x04000ED6 RID: 3798
			SsoDefault = 0,
			// Token: 0x04000ED7 RID: 3799
			SsoNoUi,
			// Token: 0x04000ED8 RID: 3800
			SsoNoAutoSignIn,
			// Token: 0x04000ED9 RID: 3801
			SsoNoHandleError = 4
		}

		// Token: 0x020002BE RID: 702
		internal enum SsoGroup
		{
			// Token: 0x04000EDB RID: 3803
			SsoGroupNone,
			// Token: 0x04000EDC RID: 3804
			SsoGroupLive = 16,
			// Token: 0x04000EDD RID: 3805
			SsoGroupEnterprise = 32
		}

		// Token: 0x020002BF RID: 703
		internal enum UpdateFlag : uint
		{
			// Token: 0x04000EDF RID: 3807
			UpdateFlagAllBit = 15U,
			// Token: 0x04000EE0 RID: 3808
			DefaultUpdatePolicy = 0U,
			// Token: 0x04000EE1 RID: 3809
			OfflineModeAllowed,
			// Token: 0x04000EE2 RID: 3810
			NoUI,
			// Token: 0x04000EE3 RID: 3811
			SkipConnectionCheck = 4U,
			// Token: 0x04000EE4 RID: 3812
			SetExtendedError = 8U,
			// Token: 0x04000EE5 RID: 3813
			SendVersion = 16U,
			// Token: 0x04000EE6 RID: 3814
			UpdateDefault = 0U
		}

		// Token: 0x020002C0 RID: 704
		internal enum ServiceTokenFlags : uint
		{
			// Token: 0x04000EE8 RID: 3816
			ServiceTokenLegacyPassport = 1U,
			// Token: 0x04000EE9 RID: 3817
			ServiceTokenWebSso,
			// Token: 0x04000EEA RID: 3818
			ServiceTokenCompactWebSso = 4U,
			// Token: 0x04000EEB RID: 3819
			ServiceTokenAny = 255U,
			// Token: 0x04000EEC RID: 3820
			ServiceTokenFromCache = 65536U,
			// Token: 0x04000EED RID: 3821
			ServiceTokenIgnoreCache = 131072U,
			// Token: 0x04000EEE RID: 3822
			ServiceTokenX509v3 = 8U,
			// Token: 0x04000EEF RID: 3823
			ServiceTokenCertInMemoryPrivateKey = 16U,
			// Token: 0x04000EF0 RID: 3824
			ServiceTokenRequestTypeNone = 0U,
			// Token: 0x04000EF1 RID: 3825
			ServiceTokenTypeProprietary,
			// Token: 0x04000EF2 RID: 3826
			ServiceTokenTypeSaml
		}

		// Token: 0x020002C1 RID: 705
		internal enum IdentityFlag : uint
		{
			// Token: 0x04000EF4 RID: 3828
			IdentityAllBit = 1023U,
			// Token: 0x04000EF5 RID: 3829
			IdentityShareAll = 255U,
			// Token: 0x04000EF6 RID: 3830
			IdentityLoadFromPersistedStore,
			// Token: 0x04000EF7 RID: 3831
			IdentityAuthStateEncrypted = 512U
		}

		// Token: 0x020002C2 RID: 706
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct IdcrlOption
		{
			// Token: 0x04000EF8 RID: 3832
			public int EnvironmentId;

			// Token: 0x04000EF9 RID: 3833
			public IntPtr EnvironmentValue;

			// Token: 0x04000EFA RID: 3834
			public uint EnvironmentLength;
		}

		// Token: 0x020002C3 RID: 707
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct RstParams
		{
			// Token: 0x04000EFB RID: 3835
			internal int CbSize;

			// Token: 0x04000EFC RID: 3836
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string ServiceName;

			// Token: 0x04000EFD RID: 3837
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string ServicePolicy;

			// Token: 0x04000EFE RID: 3838
			internal int TokenFlags;

			// Token: 0x04000EFF RID: 3839
			internal int TokenParams;
		}

		// Token: 0x020002C4 RID: 708
		[StructLayout(LayoutKind.Sequential)]
		internal class IdcrlStatusCurrent
		{
			// Token: 0x170003B0 RID: 944
			// (get) Token: 0x060013B4 RID: 5044 RVA: 0x000897B6 File Offset: 0x000879B6
			// (set) Token: 0x060013B5 RID: 5045 RVA: 0x000897BE File Offset: 0x000879BE
			internal int AuthState { get; set; }

			// Token: 0x170003B1 RID: 945
			// (get) Token: 0x060013B6 RID: 5046 RVA: 0x000897C7 File Offset: 0x000879C7
			// (set) Token: 0x060013B7 RID: 5047 RVA: 0x000897CF File Offset: 0x000879CF
			internal int AuthRequired { get; set; }

			// Token: 0x170003B2 RID: 946
			// (get) Token: 0x060013B8 RID: 5048 RVA: 0x000897D8 File Offset: 0x000879D8
			// (set) Token: 0x060013B9 RID: 5049 RVA: 0x000897E0 File Offset: 0x000879E0
			internal int RequestStatus { get; set; }

			// Token: 0x170003B3 RID: 947
			// (get) Token: 0x060013BA RID: 5050 RVA: 0x000897E9 File Offset: 0x000879E9
			// (set) Token: 0x060013BB RID: 5051 RVA: 0x000897F1 File Offset: 0x000879F1
			internal int UserInterfaceError { get; set; }

			// Token: 0x170003B4 RID: 948
			// (get) Token: 0x060013BC RID: 5052 RVA: 0x000897FA File Offset: 0x000879FA
			// (set) Token: 0x060013BD RID: 5053 RVA: 0x00089802 File Offset: 0x00087A02
			internal string WebFlowUrl { get; set; }
		}

		// Token: 0x020002C5 RID: 709
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal class PCUIParam2
		{
		}
	}
}
