using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Exchange.Diagnostics.Audit
{
	// Token: 0x02000191 RID: 401
	[SuppressUnmanagedCodeSecurity]
	internal sealed class NativeMethods
	{
		// Token: 0x06000B54 RID: 2900
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x06000B55 RID: 2901
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AdjustTokenPrivileges([In] SafeTokenHandle TokenHandle, [In] bool DisableAllPrivileges, [In] ref NativeMethods.TOKEN_PRIVILEGE NewState, [In] uint BufferLength, [In] [Out] ref NativeMethods.TOKEN_PRIVILEGE PreviousState, [In] [Out] ref uint ReturnLength);

		// Token: 0x06000B56 RID: 2902
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool RevertToSelf();

		// Token: 0x06000B57 RID: 2903
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "LookupPrivilegeValueW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool LookupPrivilegeValue([In] string lpSystemName, [In] string lpName, [In] [Out] ref NativeMethods.LUID Luid);

		// Token: 0x06000B58 RID: 2904
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetCurrentProcess();

		// Token: 0x06000B59 RID: 2905
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern SafeTokenHandle GetCurrentThread();

		// Token: 0x06000B5A RID: 2906
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool OpenProcessToken([In] IntPtr ProcessToken, [In] TokenAccessLevels DesiredAccess, [In] [Out] ref SafeTokenHandle TokenHandle);

		// Token: 0x06000B5B RID: 2907
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool OpenThreadToken([In] SafeTokenHandle ThreadToken, [In] TokenAccessLevels DesiredAccess, [In] bool OpenAsSelf, out SafeTokenHandle TokenHandle);

		// Token: 0x06000B5C RID: 2908
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DuplicateTokenEx([In] SafeTokenHandle ExistingTokenHandle, [In] TokenAccessLevels DesiredAccess, [In] IntPtr TokenAttributes, [In] SecurityImpersonationLevel ImpersonationLevel, [In] TokenType TokenType, [In] [Out] ref SafeTokenHandle DuplicateTokenHandle);

		// Token: 0x06000B5D RID: 2909
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetThreadToken([In] IntPtr Thread, [In] SafeTokenHandle Token);

		// Token: 0x06000B5E RID: 2910
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ImpersonateSelf(int impersonationLevel);

		// Token: 0x06000B5F RID: 2911
		[DllImport("advapi32.dll")]
		internal static extern void MapGenericMask(ref uint accessMask, [In] ref NativeMethods.GENERIC_MAPPING genericMapping);

		// Token: 0x06000B60 RID: 2912
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AccessCheckByType([MarshalAs(UnmanagedType.LPArray)] byte[] pSecurityDescriptor, IntPtr principalSelfSid, SafeTokenHandle clientToken, uint DesiredAccess, IntPtr objectTypeList, int ObjectTypeListLength, ref NativeMethods.GENERIC_MAPPING GenericMapping, SafeHandle PrivilegeSet, ref int PrivilegeSetLength, out uint GrantedAccess, out bool AccessStatus);

		// Token: 0x06000B61 RID: 2913
		[DllImport("authz.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AuthzUnregisterSecurityEventSource(uint dwFlags, [In] [Out] ref IntPtr providerHandle);

		// Token: 0x06000B62 RID: 2914
		[DllImport("authz.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AuthzRegisterSecurityEventSource(uint dwFlags, string szEventSourceName, out SafeAuditHandle ProviderHandle);

		// Token: 0x06000B63 RID: 2915
		[DllImport("authz.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AuthzReportSecurityEventFromParams(uint dwFlags, SafeAuditHandle providerHandle, uint auditId, byte[] securityIdentifier, [In] ref NativeMethods.AUDIT_PARAMS auditParams);

		// Token: 0x06000B64 RID: 2916
		[DllImport("authz.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AuthzInstallSecurityEventSource(uint dwFlags, [In] ref NativeMethods.AUTHZ_SOURCE_SCHEMA_REGISTRATION pRegistration);

		// Token: 0x06000B65 RID: 2917
		[DllImport("authz.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AuthzUninstallSecurityEventSource(uint dwFlags, string eventSourceName);

		// Token: 0x040007F8 RID: 2040
		internal const string KERNEL32 = "kernel32.dll";

		// Token: 0x040007F9 RID: 2041
		internal const string AUTHZ = "authz.dll";

		// Token: 0x040007FA RID: 2042
		internal const string ADVAPI32 = "advapi32.dll";

		// Token: 0x040007FB RID: 2043
		internal const string SECUR32 = "secur32.dll";

		// Token: 0x040007FC RID: 2044
		internal const uint SE_PRIVILEGE_DISABLED = 0U;

		// Token: 0x040007FD RID: 2045
		internal const uint SE_PRIVILEGE_ENABLED_BY_DEFAULT = 1U;

		// Token: 0x040007FE RID: 2046
		internal const uint SE_PRIVILEGE_ENABLED = 2U;

		// Token: 0x040007FF RID: 2047
		internal const uint SE_PRIVILEGE_USED_FOR_ACCESS = 2147483648U;

		// Token: 0x04000800 RID: 2048
		internal const uint APF_AuditFailure = 0U;

		// Token: 0x04000801 RID: 2049
		internal const uint APF_AuditSuccess = 1U;

		// Token: 0x04000802 RID: 2050
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x04000803 RID: 2051
		internal const int ERROR_INVALID_FUNCTION = 1;

		// Token: 0x04000804 RID: 2052
		internal const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x04000805 RID: 2053
		internal const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x04000806 RID: 2054
		internal const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x04000807 RID: 2055
		internal const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x04000808 RID: 2056
		internal const int ERROR_NOT_ENOUGH_MEMORY = 8;

		// Token: 0x04000809 RID: 2057
		internal const int ERROR_INVALID_DRIVE = 15;

		// Token: 0x0400080A RID: 2058
		internal const int ERROR_NO_MORE_FILES = 18;

		// Token: 0x0400080B RID: 2059
		internal const int ERROR_NOT_READY = 21;

		// Token: 0x0400080C RID: 2060
		internal const int ERROR_BAD_LENGTH = 24;

		// Token: 0x0400080D RID: 2061
		internal const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x0400080E RID: 2062
		internal const int ERROR_NOT_SUPPORTED = 50;

		// Token: 0x0400080F RID: 2063
		internal const int ERROR_FILE_EXISTS = 80;

		// Token: 0x04000810 RID: 2064
		internal const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x04000811 RID: 2065
		internal const int ERROR_CALL_NOT_IMPLEMENTED = 120;

		// Token: 0x04000812 RID: 2066
		internal const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x04000813 RID: 2067
		internal const int ERROR_INVALID_NAME = 123;

		// Token: 0x04000814 RID: 2068
		internal const int ERROR_BAD_PATHNAME = 161;

		// Token: 0x04000815 RID: 2069
		internal const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x04000816 RID: 2070
		internal const int ERROR_ENVVAR_NOT_FOUND = 203;

		// Token: 0x04000817 RID: 2071
		internal const int ERROR_FILENAME_EXCED_RANGE = 206;

		// Token: 0x04000818 RID: 2072
		internal const int ERROR_MORE_DATA = 234;

		// Token: 0x04000819 RID: 2073
		internal const int ERROR_OPERATION_ABORTED = 995;

		// Token: 0x0400081A RID: 2074
		internal const int ERROR_NO_TOKEN = 1008;

		// Token: 0x0400081B RID: 2075
		internal const int ERROR_DLL_INIT_FAILED = 1114;

		// Token: 0x0400081C RID: 2076
		internal const int ERROR_NON_ACCOUNT_SID = 1257;

		// Token: 0x0400081D RID: 2077
		internal const int ERROR_NOT_ALL_ASSIGNED = 1300;

		// Token: 0x0400081E RID: 2078
		internal const int ERROR_UNKNOWN_REVISION = 1305;

		// Token: 0x0400081F RID: 2079
		internal const int ERROR_INVALID_OWNER = 1307;

		// Token: 0x04000820 RID: 2080
		internal const int ERROR_INVALID_PRIMARY_GROUP = 1308;

		// Token: 0x04000821 RID: 2081
		internal const int ERROR_NO_SUCH_PRIVILEGE = 1313;

		// Token: 0x04000822 RID: 2082
		internal const int ERROR_PRIVILEGE_NOT_HELD = 1314;

		// Token: 0x04000823 RID: 2083
		internal const int ERROR_NONE_MAPPED = 1332;

		// Token: 0x04000824 RID: 2084
		internal const int ERROR_INVALID_ACL = 1336;

		// Token: 0x04000825 RID: 2085
		internal const int ERROR_INVALID_SID = 1337;

		// Token: 0x04000826 RID: 2086
		internal const int ERROR_INVALID_SECURITY_DESCR = 1338;

		// Token: 0x04000827 RID: 2087
		internal const int ERROR_BAD_IMPERSONATION_LEVEL = 1346;

		// Token: 0x04000828 RID: 2088
		internal const int ERROR_CANT_OPEN_ANONYMOUS = 1347;

		// Token: 0x04000829 RID: 2089
		internal const int ERROR_NO_SECURITY_ON_OBJECT = 1350;

		// Token: 0x0400082A RID: 2090
		internal const int ERROR_TRUSTED_RELATIONSHIP_FAILURE = 1789;

		// Token: 0x0400082B RID: 2091
		internal const int ERROR_OBJECT_ALREADY_EXISTS = 5010;

		// Token: 0x0400082C RID: 2092
		internal const uint STATUS_SOME_NOT_MAPPED = 263U;

		// Token: 0x0400082D RID: 2093
		internal const uint STATUS_NO_MEMORY = 3221225495U;

		// Token: 0x0400082E RID: 2094
		internal const uint STATUS_NONE_MAPPED = 3221225587U;

		// Token: 0x0400082F RID: 2095
		internal const uint STATUS_INSUFFICIENT_RESOURCES = 3221225626U;

		// Token: 0x04000830 RID: 2096
		internal const uint STATUS_ACCESS_DENIED = 3221225506U;

		// Token: 0x04000831 RID: 2097
		internal const int STATUS_ACCOUNT_RESTRICTION = -1073741714;

		// Token: 0x02000192 RID: 402
		internal struct GENERIC_MAPPING
		{
			// Token: 0x04000832 RID: 2098
			public uint GenericRead;

			// Token: 0x04000833 RID: 2099
			public uint GenericWrite;

			// Token: 0x04000834 RID: 2100
			public uint GenericExecute;

			// Token: 0x04000835 RID: 2101
			public uint GenericAll;
		}

		// Token: 0x02000193 RID: 403
		internal struct LUID
		{
			// Token: 0x04000836 RID: 2102
			internal uint LowPart;

			// Token: 0x04000837 RID: 2103
			internal uint HighPart;
		}

		// Token: 0x02000194 RID: 404
		internal struct LUID_AND_ATTRIBUTES
		{
			// Token: 0x04000838 RID: 2104
			internal NativeMethods.LUID Luid;

			// Token: 0x04000839 RID: 2105
			internal uint Attributes;
		}

		// Token: 0x02000195 RID: 405
		internal struct TOKEN_PRIVILEGE
		{
			// Token: 0x0400083A RID: 2106
			internal uint PrivilegeCount;

			// Token: 0x0400083B RID: 2107
			internal NativeMethods.LUID_AND_ATTRIBUTES Privilege;
		}

		// Token: 0x02000196 RID: 406
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct AUTHZ_REGISTRATION_OBJECT_TYPE_NAME_OFFSET
		{
			// Token: 0x0400083C RID: 2108
			internal string szObjectTypeName;

			// Token: 0x0400083D RID: 2109
			internal uint dwOffset;
		}

		// Token: 0x02000197 RID: 407
		internal struct AUDIT_PARAM
		{
			// Token: 0x0400083E RID: 2110
			internal uint Type;

			// Token: 0x0400083F RID: 2111
			internal uint Length;

			// Token: 0x04000840 RID: 2112
			internal uint Flags;

			// Token: 0x04000841 RID: 2113
			internal IntPtr Data0;

			// Token: 0x04000842 RID: 2114
			internal IntPtr Data1;
		}

		// Token: 0x02000198 RID: 408
		internal struct AUDIT_PARAMS
		{
			// Token: 0x04000843 RID: 2115
			internal uint Length;

			// Token: 0x04000844 RID: 2116
			internal uint Flags;

			// Token: 0x04000845 RID: 2117
			internal ushort Count;

			// Token: 0x04000846 RID: 2118
			internal IntPtr Parameters;
		}

		// Token: 0x02000199 RID: 409
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct AUTHZ_SOURCE_SCHEMA_REGISTRATION
		{
			// Token: 0x04000847 RID: 2119
			internal uint dwFlags;

			// Token: 0x04000848 RID: 2120
			internal string eventSourceName;

			// Token: 0x04000849 RID: 2121
			internal string eventMessageFile;

			// Token: 0x0400084A RID: 2122
			internal string eventSourceXmlSchemaFile;

			// Token: 0x0400084B RID: 2123
			internal string eventAccessStringsFile;

			// Token: 0x0400084C RID: 2124
			internal string executableImagePath;

			// Token: 0x0400084D RID: 2125
			internal IntPtr pReserved;

			// Token: 0x0400084E RID: 2126
			internal uint dwObjectTypeNameCount;

			// Token: 0x0400084F RID: 2127
			internal NativeMethods.AUTHZ_REGISTRATION_OBJECT_TYPE_NAME_OFFSET objectTypeNames;
		}
	}
}
