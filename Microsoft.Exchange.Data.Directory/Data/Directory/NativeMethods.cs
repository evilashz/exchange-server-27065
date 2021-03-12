using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000161 RID: 353
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		// Token: 0x06000F4F RID: 3919
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int EnumSystemGeoID(int GeoClass, int ParentGeoId, NativeMethods.GeoEnumProc lpGeoEnumProc);

		// Token: 0x06000F50 RID: 3920
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetGeoInfo(int GeoId, NativeMethods.SysGeoType GeoType, StringBuilder lpGeoData, int cchData, int language);

		// Token: 0x06000F51 RID: 3921
		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetComputerNameEx(int nameType, StringBuilder buffer, ref uint bufferSize);

		// Token: 0x06000F52 RID: 3922
		[DllImport("kernel32.dll")]
		internal static extern IntPtr GetCurrentProcess();

		// Token: 0x06000F53 RID: 3923
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetProcessTimes(IntPtr process, out long creationTime, out long exitTime, out long kernelTime, out long userTime);

		// Token: 0x06000F54 RID: 3924
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetSystemTimes(out long idleTime, out long kernelTime, out long userTime);

		// Token: 0x06000F55 RID: 3925
		[DllImport("kernel32.dll")]
		internal static extern uint GetLastError();

		// Token: 0x06000F56 RID: 3926
		[DllImport("NETAPI32.DLL", CharSet = CharSet.Unicode)]
		internal static extern int DsRoleGetPrimaryDomainInformation(string server, int infoLevel, out SafeDsRolePrimaryDomainInfoLevelHandle buffer);

		// Token: 0x06000F57 RID: 3927
		[DllImport("NETAPI32.DLL")]
		internal static extern void DsRoleFreeMemory(IntPtr buffer);

		// Token: 0x06000F58 RID: 3928
		[DllImport("NETAPI32.DLL", CharSet = CharSet.Unicode)]
		internal static extern int DsGetSiteName(string server, out SafeDsSiteNameHandle siteName);

		// Token: 0x06000F59 RID: 3929
		[DllImport("NETAPI32.DLL", CharSet = CharSet.Unicode)]
		internal static extern int NetApiBufferFree(IntPtr buffer);

		// Token: 0x06000F5A RID: 3930
		[DllImport("NETAPI32.DLL", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int DsGetDcSiteCoverage([MarshalAs(UnmanagedType.LPTStr)] string ServerName, out long EntryCount, out SafeDsSiteNameHandle SiteNames);

		// Token: 0x06000F5B RID: 3931
		[DllImport("NtDsapi.dll", CharSet = CharSet.Unicode, EntryPoint = "DsBindW")]
		internal static extern uint DsBind([MarshalAs(UnmanagedType.LPWStr)] string DomainControllerName, [MarshalAs(UnmanagedType.LPWStr)] string DnsDomainName, out SafeDsBindHandle phDS);

		// Token: 0x06000F5C RID: 3932
		[DllImport("Ntdsapi.dll", EntryPoint = "DsUnBindW", ExactSpelling = true)]
		internal static extern uint DsUnBind(ref IntPtr phDS);

		// Token: 0x06000F5D RID: 3933
		[DllImport("ntdsapi.dll", CharSet = CharSet.Unicode, EntryPoint = "DsCrackNamesW", ExactSpelling = true, SetLastError = true)]
		internal static extern uint DsCrackNames(SafeDsBindHandle phDS, NativeMethods.DsNameFlags flags, ExtendedNameFormat formatOffered, ExtendedNameFormat formatDesired, uint cNames, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 4)] string[] rpNames, out SafeDsNameResultHandle ppResult);

		// Token: 0x06000F5E RID: 3934
		[DllImport("ntdsapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern void DsFreeNameResult(IntPtr pResult);

		// Token: 0x06000F5F RID: 3935
		[DllImport("ntdsapi.dll", CharSet = CharSet.Unicode, EntryPoint = "DsRemoveDsServerW", ExactSpelling = true, SetLastError = true)]
		internal static extern uint DsRemoveDsServer(SafeDsBindHandle phDS, [MarshalAs(UnmanagedType.LPWStr)] string ServerDN, [MarshalAs(UnmanagedType.LPWStr)] string DomainDN, out bool pfLastDCInDomain, bool fCommit);

		// Token: 0x06000F60 RID: 3936
		[DllImport("ntdsapi.dll", CharSet = CharSet.Unicode, EntryPoint = "DsAddSidHistoryW", ExactSpelling = true, SetLastError = true)]
		internal static extern uint DsAddSidHistory(SafeDsBindHandle phDS, uint flags, [MarshalAs(UnmanagedType.LPWStr)] string srcDomain, [MarshalAs(UnmanagedType.LPWStr)] string srcPrincipal, [MarshalAs(UnmanagedType.LPWStr)] string srcDomainController, IntPtr srcDomainCreds, [MarshalAs(UnmanagedType.LPWStr)] string dstDomain, [MarshalAs(UnmanagedType.LPWStr)] string dstPrincipal);

		// Token: 0x06000F61 RID: 3937
		[DllImport("ntdsapi.dll", CharSet = CharSet.Unicode, EntryPoint = "DsBindWithSpnExW", ExactSpelling = true, SetLastError = true)]
		internal static extern uint DsBindWithSpnEx([MarshalAs(UnmanagedType.LPWStr)] string DomainControllerName, [MarshalAs(UnmanagedType.LPWStr)] string DnsDomainName, IntPtr AuthIdentity, [MarshalAs(UnmanagedType.LPWStr)] string ServicePrincipalName, uint BindFlags, out SafeDsBindHandle phDS);

		// Token: 0x06000F62 RID: 3938
		[DllImport("NETAPI32.DLL", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "DsGetDcOpenW")]
		internal static extern int DsGetDcOpen([In] string dnsName, [In] int optionFlags, [In] string siteName, [In] IntPtr domainGuid, [In] string dnsForestName, [In] int dcFlags, out SafeDsGetDcContextHandle retGetDcContext);

		// Token: 0x06000F63 RID: 3939
		[DllImport("NETAPI32.DLL", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "DsGetDcNextW")]
		internal static extern int DsGetDcNext([In] SafeDsGetDcContextHandle getDcContextHandle, [In] [Out] ref IntPtr sockAddressCount, out IntPtr sockAdresses, out SafeDnsHostNameHandle dnsHostName);

		// Token: 0x06000F64 RID: 3940
		[DllImport("NETAPI32.DLL", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "DsGetDcCloseW")]
		internal static extern void DsGetDcClose([In] IntPtr getDcContextHandle);

		// Token: 0x06000F65 RID: 3941
		[DllImport("dsaccessperf.dll", CharSet = CharSet.Ansi)]
		internal static extern void DsaccessPerfCounterUpdate(uint objectType, uint counterId, uint modType, uint value, string instanceName);

		// Token: 0x06000F66 RID: 3942
		[DllImport("dsaccessperf.dll")]
		internal static extern void DsaccessPerfDCPrepareForRefresh();

		// Token: 0x06000F67 RID: 3943
		[DllImport("dsaccessperf.dll")]
		internal static extern void DsaccessPerfDCFinalizeRefresh();

		// Token: 0x06000F68 RID: 3944
		[DllImport("dsaccessperf.dll", CharSet = CharSet.Ansi)]
		internal static extern void DsaccessPerfDCAddToList(string serverName);

		// Token: 0x06000F69 RID: 3945
		[DllImport("dsaccessperf.dll", CharSet = CharSet.Ansi)]
		internal static extern void DsaccessPerfSetProcessName(string processName, string applicationName, bool hasMultiInstance);

		// Token: 0x06000F6A RID: 3946
		[DllImport("NETAPI32.DLL", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int I_NetLogonControl2([In] string lpServerName, uint lpFunctionCode, uint lpQueryLevel, ref IntPtr lpInputData, out IntPtr queryInformation);

		// Token: 0x040008CF RID: 2255
		private const string NetApi32 = "NETAPI32.DLL";

		// Token: 0x040008D0 RID: 2256
		internal const string DsAccessPerf = "dsaccessperf.dll";

		// Token: 0x040008D1 RID: 2257
		internal const int ERROR_NO_MORE_ITEMS = 259;

		// Token: 0x040008D2 RID: 2258
		internal const int ERROR_FILE_MARK_DETECTED = 1101;

		// Token: 0x040008D3 RID: 2259
		internal const int DNS_ERROR_RCODE_NAME_ERROR = 9003;

		// Token: 0x040008D4 RID: 2260
		internal static readonly int DsRolePrimaryDomainInfoBasic = 1;

		// Token: 0x040008D5 RID: 2261
		internal static readonly int ERROR_NO_SITE = 1919;

		// Token: 0x040008D6 RID: 2262
		internal static readonly int ERROR_NOT_ENOUGH_MEMORY = 8;

		// Token: 0x02000162 RID: 354
		internal enum ComputerNameFormat
		{
			// Token: 0x040008D8 RID: 2264
			ComputerNameNetBIOS,
			// Token: 0x040008D9 RID: 2265
			ComputerNameDnsHostname,
			// Token: 0x040008DA RID: 2266
			ComputerNameDnsDomain,
			// Token: 0x040008DB RID: 2267
			ComputerNameDnsFullyQualified,
			// Token: 0x040008DC RID: 2268
			ComputerNamePhysicalNetBIOS,
			// Token: 0x040008DD RID: 2269
			ComputerNamePhysicalDnsHostname,
			// Token: 0x040008DE RID: 2270
			ComputerNamePhysicalDnsDomain,
			// Token: 0x040008DF RID: 2271
			ComputerNamePhysicalDnsFullyQualified,
			// Token: 0x040008E0 RID: 2272
			ComputerNameMax
		}

		// Token: 0x02000163 RID: 355
		internal enum SysGeoType
		{
			// Token: 0x040008E2 RID: 2274
			Iso2 = 4,
			// Token: 0x040008E3 RID: 2275
			FriendlyName = 8
		}

		// Token: 0x02000164 RID: 356
		// (Invoke) Token: 0x06000F6D RID: 3949
		internal delegate bool GeoEnumProc(int geiId);

		// Token: 0x02000165 RID: 357
		internal enum MachineRole
		{
			// Token: 0x040008E5 RID: 2277
			StandaloneWorkstation,
			// Token: 0x040008E6 RID: 2278
			MemberWorkstation,
			// Token: 0x040008E7 RID: 2279
			StandaloneServer,
			// Token: 0x040008E8 RID: 2280
			MemberServer,
			// Token: 0x040008E9 RID: 2281
			BackupDomainController,
			// Token: 0x040008EA RID: 2282
			PrimaryDomainController
		}

		// Token: 0x02000166 RID: 358
		[StructLayout(LayoutKind.Sequential)]
		internal class InteropDsRolePrimaryDomainInfoBasic
		{
			// Token: 0x040008EB RID: 2283
			public NativeMethods.MachineRole machineRole;

			// Token: 0x040008EC RID: 2284
			public int flags;

			// Token: 0x040008ED RID: 2285
			[MarshalAs(UnmanagedType.LPWStr)]
			public string domainNameFlat;

			// Token: 0x040008EE RID: 2286
			[MarshalAs(UnmanagedType.LPWStr)]
			public string domainNameDns;

			// Token: 0x040008EF RID: 2287
			[MarshalAs(UnmanagedType.LPWStr)]
			public string domainForestName;

			// Token: 0x040008F0 RID: 2288
			public Guid guid;
		}

		// Token: 0x02000167 RID: 359
		[StructLayout(LayoutKind.Sequential)]
		internal class DsNameResult
		{
			// Token: 0x040008F1 RID: 2289
			public uint cItems;

			// Token: 0x040008F2 RID: 2290
			public IntPtr rItems;
		}

		// Token: 0x02000168 RID: 360
		[StructLayout(LayoutKind.Sequential)]
		internal class DsNameResultItem
		{
			// Token: 0x040008F3 RID: 2291
			public int status;

			// Token: 0x040008F4 RID: 2292
			[MarshalAs(UnmanagedType.LPWStr)]
			public string domain;

			// Token: 0x040008F5 RID: 2293
			[MarshalAs(UnmanagedType.LPWStr)]
			public string name;
		}

		// Token: 0x02000169 RID: 361
		[Flags]
		internal enum DsNameFlags
		{
			// Token: 0x040008F7 RID: 2295
			NoFlags = 0,
			// Token: 0x040008F8 RID: 2296
			SyntacticalOnly = 1,
			// Token: 0x040008F9 RID: 2297
			EvalAtDC = 2,
			// Token: 0x040008FA RID: 2298
			GCVerify = 4,
			// Token: 0x040008FB RID: 2299
			TrustReferral = 8
		}

		// Token: 0x0200016A RID: 362
		internal enum BindFlags : uint
		{
			// Token: 0x040008FD RID: 2301
			BIND_ALLOW_DELEGATION = 1U,
			// Token: 0x040008FE RID: 2302
			BIND_FIND_BINDING,
			// Token: 0x040008FF RID: 2303
			BIND_FORCE_KERBEROS = 4U
		}

		// Token: 0x0200016B RID: 363
		[Flags]
		public enum DsGetDcOpenFlags
		{
			// Token: 0x04000901 RID: 2305
			ForceRediscovery = 1,
			// Token: 0x04000902 RID: 2306
			GCRequired = 64,
			// Token: 0x04000903 RID: 2307
			PdcRequired = 128,
			// Token: 0x04000904 RID: 2308
			DSWriteableRequired = 4096
		}

		// Token: 0x0200016C RID: 364
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct NetLogonInfo2
		{
			// Token: 0x04000905 RID: 2309
			internal uint Flags;

			// Token: 0x04000906 RID: 2310
			internal uint PdcConnectionStatus;

			// Token: 0x04000907 RID: 2311
			internal string TrustedDcName;

			// Token: 0x04000908 RID: 2312
			internal uint TdcConnectionStatus;
		}

		// Token: 0x0200016D RID: 365
		internal enum NetLogonControlOperation : uint
		{
			// Token: 0x0400090A RID: 2314
			NetLogonControlRediscover = 5U,
			// Token: 0x0400090B RID: 2315
			NetLogonControlTrustedChannelStatus
		}
	}
}
