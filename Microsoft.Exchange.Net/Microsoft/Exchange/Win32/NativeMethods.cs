using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000083 RID: 131
	[ComVisible(false)]
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		// Token: 0x06000447 RID: 1095
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool FileTimeToSystemTime([In] ref long FileTime, out NativeMethods.SystemTime systemTime);

		// Token: 0x06000448 RID: 1096
		[DllImport("KERNEL32.DLL")]
		internal static extern NativeMethods.TimeZoneId GetTimeZoneInformation(out NativeMethods.TIME_ZONE_INFORMATION timeZoneInformation);

		// Token: 0x06000449 RID: 1097
		[DllImport("KERNEL32.DLL", CharSet = CharSet.Unicode, EntryPoint = "DeleteFileW", SetLastError = true)]
		internal static extern bool DeleteFile([In] string fileName);

		// Token: 0x0600044A RID: 1098
		[DllImport("KERNEL32.DLL", CharSet = CharSet.Unicode, EntryPoint = "GetTempFileNameW", SetLastError = true)]
		internal static extern int GetTempFileName([In] string pathName, [In] string prefixString, [In] uint unique, [Out] StringBuilder tempFileName);

		// Token: 0x0600044B RID: 1099 RVA: 0x00011B61 File Offset: 0x0000FD61
		public static SafeHGlobalHandle AllocHGlobal(int size)
		{
			return SafeHGlobalHandle.AllocHGlobal(size);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00011B6C File Offset: 0x0000FD6C
		internal static NativeMethods.SecurityIdentifierAndAttributes AuthzGetInformationFromContextTokenUser(AuthzContextHandle hAuthzClientContext)
		{
			uint num = 0U;
			NativeMethods.AuthzGetInformationFromContext(hAuthzClientContext, AuthzContextInformation.UserSid, 0U, ref num, SafeHGlobalHandle.InvalidHandle);
			if (num == 0U)
			{
				throw new Win32Exception();
			}
			NativeMethods.SecurityIdentifierAndAttributes result;
			using (SafeHGlobalHandle safeHGlobalHandle = new SafeHGlobalHandle(Marshal.AllocHGlobal((int)num)))
			{
				if (!NativeMethods.AuthzGetInformationFromContext(hAuthzClientContext, AuthzContextInformation.UserSid, num, ref num, safeHGlobalHandle))
				{
					throw new Win32Exception();
				}
				result = new NativeMethods.SecurityIdentifierAndAttributes(safeHGlobalHandle.DangerousGetHandle());
			}
			return result;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00011BE0 File Offset: 0x0000FDE0
		internal static NativeMethods.SecurityIdentifierAndAttributes[] AuthzGetInformationFromContextTokenGroup(AuthzContextHandle hAuthzClientContext)
		{
			uint num = 0U;
			NativeMethods.SecurityIdentifierAndAttributes[] array = null;
			NativeMethods.AuthzGetInformationFromContext(hAuthzClientContext, AuthzContextInformation.GroupSids, 0U, ref num, SafeHGlobalHandle.InvalidHandle);
			if (num == 0U)
			{
				throw new Win32Exception();
			}
			using (SafeHGlobalHandle safeHGlobalHandle = new SafeHGlobalHandle(Marshal.AllocHGlobal((int)num)))
			{
				if (!NativeMethods.AuthzGetInformationFromContext(hAuthzClientContext, AuthzContextInformation.GroupSids, num, ref num, safeHGlobalHandle))
				{
					throw new Win32Exception();
				}
				int num2 = Marshal.ReadInt32(safeHGlobalHandle.DangerousGetHandle());
				if (num2 > 0)
				{
					array = new NativeMethods.SecurityIdentifierAndAttributes[num2];
					IntPtr value = new IntPtr((long)safeHGlobalHandle.DangerousGetHandle() + (long)NativeMethods.TokenGroups.SidAndAttributesOffset);
					for (int i = 0; i < num2; i++)
					{
						array[i] = new NativeMethods.SecurityIdentifierAndAttributes(new IntPtr((long)value + (long)(i * NativeMethods.SecurityIdentifierAndAttributes.SidAndAttributesSize)));
					}
				}
			}
			return array;
		}

		// Token: 0x0600044E RID: 1102
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("AUTHZ.DLL", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool AuthzInitializeResourceManager([In] ResourceManagerFlags flags, [In] IntPtr pfnAccessCheck, [In] IntPtr pfnComputeDynamicGroups, [In] IntPtr pfnFreeDynamicGroups, [In] string resourceManagerName, out ResourceManagerHandle resourceManagerHandle);

		// Token: 0x0600044F RID: 1103
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("AUTHZ.DLL", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AuthzAccessCheck(uint flags, AuthzContextHandle hClientContext, SafeHGlobalHandle pRequest, IntPtr hAuditInfo, byte[] securityDescriptor, IntPtr optionalSecurityDescriptorArray, uint optionalSecurityDescriptorCount, SafeHGlobalHandle pReply, [In] [Out] IntPtr pAuthzHandle);

		// Token: 0x06000450 RID: 1104
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("AUTHZ.DLL", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AuthzGetInformationFromContext(AuthzContextHandle hAuthzClientContext, AuthzContextInformation InfoClass, uint BufferSize, ref uint SizeRequired, SafeHGlobalHandle ContextInfoBuffer);

		// Token: 0x06000451 RID: 1105
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("AUTHZ.DLL", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AuthzInitializeContextFromSid(AuthzFlags Flags, byte[] UserSid, ResourceManagerHandle AuthzResourceManager, IntPtr pExpirationTime, NativeMethods.AuthzLuid Identifier, IntPtr DynamicGroupArgs, out AuthzContextHandle pAuthzClientContext);

		// Token: 0x06000452 RID: 1106
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("AUTHZ.DLL", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AuthzInitializeContextFromToken(AuthzFlags Flags, IntPtr TokenHandle, ResourceManagerHandle AuthzResourceManager, IntPtr pExpirationTime, NativeMethods.AuthzLuid Identifier, IntPtr DynamicGroupArgs, out AuthzContextHandle pAuthzClientContext);

		// Token: 0x06000453 RID: 1107
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("AUTHZ.DLL", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AuthzInitializeContextFromAuthzContext(AuthzFlags Flags, IntPtr authzClientContext, IntPtr pExpirationTime, NativeMethods.AuthzLuid Identifier, IntPtr DynamicGroupArgs, out AuthzContextHandle pAuthzNewClientContext);

		// Token: 0x06000454 RID: 1108
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("AUTHZ.DLL", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AuthzAddSidsToContext(AuthzContextHandle OrigClientContext, NativeMethods.SidAndAttributes[] groupSids, uint groupSidCount, NativeMethods.SidAndAttributes[] restrictedSids, uint restrictedSidCount, out AuthzContextHandle pNewClientContext);

		// Token: 0x06000455 RID: 1109
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("ADVAPI32.DLL", SetLastError = true)]
		public static extern int MakeAbsoluteSD([MarshalAs(UnmanagedType.LPArray)] byte[] pSecurityDescriptor, IntPtr pAbsoluteSD, [In] [Out] ref int lpdwAbsoluteSDSize, IntPtr pDacl, [In] [Out] ref int lpdwDaclSize, IntPtr pSacl, [In] [Out] ref int lpdwSaclSize, IntPtr pOwner, [In] [Out] ref int lpdwOwnerSize, IntPtr pPrimaryGroup, [In] [Out] ref int lpdwPrimaryGroupSize);

		// Token: 0x06000456 RID: 1110
		[DllImport("SECUR32.DLL", CharSet = CharSet.Unicode, EntryPoint = "GetComputerObjectNameW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetComputerObjectName(NativeMethods.ExtendedNameFormat nameFormat, StringBuilder nameBuffer, ref int size);

		// Token: 0x06000457 RID: 1111
		[DllImport("SECUR32.DLL", CharSet = CharSet.Unicode, EntryPoint = "GetUserNameExW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetUserNameEx(NativeMethods.ExtendedNameFormat nameFormat, StringBuilder nameBuffer, ref int nSize);

		// Token: 0x06000458 RID: 1112
		[DllImport("NTDSAPI.DLL", CharSet = CharSet.Unicode, EntryPoint = "DsServerRegisterSpnW")]
		internal static extern int DsServerRegisterSpn(NativeMethods.SpnWriteOperation op, string serviceClass, string userObjectDn);

		// Token: 0x06000459 RID: 1113
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GlobalMemoryStatusEx(ref NativeMethods.MEMORYSTATUSEX lpBuffer);

		// Token: 0x0600045A RID: 1114
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		internal static extern uint WaitForMultipleObjects(uint count, IntPtr[] handles, [MarshalAs(UnmanagedType.Bool)] bool isWaitAll, uint milliseconds);

		// Token: 0x0600045B RID: 1115
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		internal static extern uint WaitForSingleObject(SafeProcessHandle processHandle, uint milliseconds);

		// Token: 0x0600045C RID: 1116
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetQueuedCompletionStatus(IoCompletionPort completionPort, out uint numberOfBytes, out UIntPtr completionKey, out IntPtr overlapped, uint milliseconds);

		// Token: 0x0600045D RID: 1117
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool PostQueuedCompletionStatus(IoCompletionPort completionPort, uint numberOfBytes, UIntPtr completionKey, IntPtr overlapped);

		// Token: 0x0600045E RID: 1118
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.I4)]
		internal static extern uint GetProcessId(IntPtr hProcess);

		// Token: 0x0600045F RID: 1119
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

		// Token: 0x06000460 RID: 1120
		[DllImport("ADVAPI32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CreateProcessAsUser(SafeUserTokenHandle hToken, string lpApplicationName, string lpCommandLine, IntPtr processAttributes, IntPtr threadAttributes, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, ref NativeMethods.STARTUPINFO lpStartupInfo, ref NativeMethods.PROCESS_INFORMATION lpProcessInformation);

		// Token: 0x06000461 RID: 1121
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, ref NativeMethods.STARTUPINFO lpStartupInfo, ref NativeMethods.PROCESS_INFORMATION lpProcessInformation);

		// Token: 0x06000462 RID: 1122
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		public static extern SafeProcessHandle OpenProcess(NativeMethods.ProcessAccess dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwProcessId);

		// Token: 0x06000463 RID: 1123
		[DllImport("KERNEL32.DLL")]
		internal static extern uint GetCurrentProcessId();

		// Token: 0x06000464 RID: 1124
		[DllImport("ADVAPI32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool LogonUser(string userName, string domainName, string password, int logonType, int logonProvider, out SafeUserTokenHandle token);

		// Token: 0x06000465 RID: 1125
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("ADVAPI32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool OpenProcessToken([In] IntPtr ProcessHandle, [In] uint DesiredAccess, [In] [Out] ref SafeUserTokenHandle TokenHandle);

		// Token: 0x06000466 RID: 1126
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("ADVAPI32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetTokenInformation([In] SafeUserTokenHandle TokenHandle, [In] NativeMethods.TOKEN_INFORMATION_CLASS TokenInformationClass, [Out] SafeHGlobalHandle TokenInformation, [In] int TokenInformationLength, [In] [Out] ref int ReturnLength);

		// Token: 0x06000467 RID: 1127
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("ADVAPI32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool LookupPrivilegeValue([In] string lpSystemName, [In] string lpName, [In] [Out] ref NativeMethods.LUID Luid);

		// Token: 0x06000468 RID: 1128
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("ADVAPI32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AdjustTokenPrivileges([In] SafeUserTokenHandle TokenHandle, [MarshalAs(UnmanagedType.Bool)] [In] bool DisableAllPrivileges, [In] SafeHGlobalHandle NewState, [In] uint BufferLength, [In] [Out] IntPtr PreviousState, [In] [Out] IntPtr ReturnLength);

		// Token: 0x06000469 RID: 1129 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		internal static bool GlobalMemoryStatusEx(out NativeMethods.MemoryStatusEx memoryStatusEx)
		{
			memoryStatusEx = default(NativeMethods.MemoryStatusEx);
			memoryStatusEx.Length = NativeMethods.MemoryStatusEx.Size;
			return NativeMethods.GlobalMemoryStatusExInternal(ref memoryStatusEx);
		}

		// Token: 0x0600046A RID: 1130
		[DllImport("KERNEL32.DLL", EntryPoint = "GlobalMemoryStatusEx", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GlobalMemoryStatusExInternal([In] [Out] ref NativeMethods.MemoryStatusEx memoryStatusEx);

		// Token: 0x0600046B RID: 1131
		[DllImport("PSAPI.DLL")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetProcessMemoryInfo([In] SafeProcessHandle processHandle, out NativeMethods.ProcessMemoryCounterEx counters, [In] uint size);

		// Token: 0x0600046C RID: 1132
		[DllImport("KERNEL32.DLL", CharSet = CharSet.Unicode, EntryPoint = "GetDiskFreeSpaceExW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetDiskFreeSpaceEx([In] string directoryName, out ulong freeBytesAvailable, out ulong totalNumberOfBytes, out ulong totalNumberOfFreeBytes);

		// Token: 0x0600046D RID: 1133
		[DllImport("RPCRT4.DLL")]
		internal static extern int RpcTestCancel();

		// Token: 0x0600046E RID: 1134
		[DllImport("KERNEL32.DLL", BestFitMapping = false, EntryPoint = "DuplicateHandle", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DuplicateThreadHandle([In] SafeProcessHandle sourceProcessHandle, [In] SafeThreadHandle sourceHandle, [In] SafeProcessHandle targetProcessHandle, out SafeThreadHandle targetHandle, [In] uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] [In] bool bInheritHandle, [In] uint dwOptions);

		// Token: 0x0600046F RID: 1135
		[DllImport("KERNEL32.DLL")]
		internal static extern SafeThreadHandle GetCurrentThread();

		// Token: 0x06000470 RID: 1136
		[DllImport("KERNEL32.DLL")]
		internal static extern SafeProcessHandle GetCurrentProcess();

		// Token: 0x06000471 RID: 1137
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool FlushViewOfFile([In] SafeViewOfFileHandle lpBaseAddress, [In] UIntPtr dwNumBytesToFlush);

		// Token: 0x06000472 RID: 1138
		[DllImport("ADVAPI32.DLL", CharSet = CharSet.Unicode, EntryPoint = "ConvertStringSecurityDescriptorToSecurityDescriptorW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ConvertStringSecurityDescriptorToSecurityDescriptor([In] string StringSecurityDescriptor, [In] uint SDRevision, out SafeHGlobalHandle SecurityDescriptor, out ulong SecurityDescriptorSize);

		// Token: 0x06000473 RID: 1139
		[DllImport("KERNEL32.DLL", CharSet = CharSet.Unicode, EntryPoint = "GetComputerNameExW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetComputerNameEx([In] NativeMethods.ComputerNameFormat nameType, [Out] StringBuilder buffer, [In] [Out] ref uint bufferSize);

		// Token: 0x06000474 RID: 1140 RVA: 0x00011CD0 File Offset: 0x0000FED0
		public static bool CreatePrivateObjectSecurityEx([In] RawSecurityDescriptor parentDescriptor, [In] RawSecurityDescriptor creatorDescriptor, out RawSecurityDescriptor newDescriptor, [In] Guid objectType, [In] bool isContainerObject, [In] uint autoInheritFlags, [In] WindowsIdentity identity, [In] NativeMethods.GENERIC_MAPPING mapping)
		{
			byte[] array = null;
			byte[] array2 = null;
			byte[] binaryForm = null;
			newDescriptor = null;
			if (parentDescriptor != null)
			{
				array = new byte[parentDescriptor.BinaryLength];
				parentDescriptor.GetBinaryForm(array, 0);
			}
			if (creatorDescriptor != null)
			{
				array2 = new byte[creatorDescriptor.BinaryLength];
				creatorDescriptor.GetBinaryForm(array2, 0);
			}
			bool result = NativeMethods.CreatePrivateObjectSecurityEx(array, array2, out binaryForm, objectType, isContainerObject, autoInheritFlags, identity, mapping);
			newDescriptor = new RawSecurityDescriptor(binaryForm, 0);
			return result;
		}

		// Token: 0x06000475 RID: 1141
		[DllImport("ADVAPI32.DLL", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U4)]
		public static extern uint GetSecurityDescriptorLength(SafeHandle pSecurityDescriptor);

		// Token: 0x06000476 RID: 1142
		[DllImport("ADVAPI32.DLL", SetLastError = true)]
		public static extern void MapGenericMask(ref uint AccessMask, ref NativeMethods.GENERIC_MAPPING GenericMapping);

		// Token: 0x06000477 RID: 1143 RVA: 0x00011D34 File Offset: 0x0000FF34
		public static bool LookupAccountSid(SecurityIdentifier sid, out string domainName, out string accountName)
		{
			byte[] array = new byte[sid.BinaryLength];
			sid.GetBinaryForm(array, 0);
			uint capacity = 64U;
			uint capacity2 = 64U;
			StringBuilder stringBuilder = new StringBuilder((int)capacity);
			StringBuilder stringBuilder2 = new StringBuilder((int)capacity2);
			int num;
			bool flag = NativeMethods.LookupAccountSid(null, array, stringBuilder, ref capacity, stringBuilder2, ref capacity2, out num);
			if (!flag && Marshal.GetLastWin32Error() == 122)
			{
				stringBuilder = new StringBuilder((int)capacity);
				stringBuilder2 = new StringBuilder((int)capacity2);
				flag = NativeMethods.LookupAccountSid(null, array, stringBuilder, ref capacity, stringBuilder2, ref capacity2, out num);
			}
			if (flag)
			{
				domainName = stringBuilder2.ToString();
				accountName = stringBuilder.ToString();
			}
			else
			{
				accountName = null;
				domainName = null;
			}
			return flag;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00011DCE File Offset: 0x0000FFCE
		public static int HRESULT_FROM_WIN32(uint win32)
		{
			if (win32 <= 0U)
			{
				return (int)win32;
			}
			return (int)((win32 & 65535U) | 2147942400U);
		}

		// Token: 0x06000479 RID: 1145
		[DllImport("ADVAPI32.DLL", CharSet = CharSet.Unicode, EntryPoint = "LookupAccountSidW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool LookupAccountSid(string systemName, byte[] sid, StringBuilder accountName, ref uint accountNameLength, StringBuilder domainName, ref uint domainNameLength, out int usage);

		// Token: 0x0600047A RID: 1146
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		internal static extern void ZeroMemory(IntPtr handle, uint length);

		// Token: 0x0600047B RID: 1147
		[DllImport("KERNEL32.DLL", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "FindFirstFileW", SetLastError = true)]
		internal static extern SafeFindHandle FindFirstFile([In] string fileName, out NativeMethods.WIN32_FIND_DATA data);

		// Token: 0x0600047C RID: 1148
		[DllImport("KERNEL32.DLL", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "FindNextFileW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FindNextFile([In] SafeFindHandle hndFindFile, out NativeMethods.WIN32_FIND_DATA lpFindFileData);

		// Token: 0x0600047D RID: 1149
		[DllImport("KERNEL32.DLL", CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryW", SetLastError = true)]
		public static extern SafeLibraryHandle LoadLibrary([In] string fileName);

		// Token: 0x0600047E RID: 1150 RVA: 0x00011DE3 File Offset: 0x0000FFE3
		internal static bool AuthzInitializeResourceManager(ResourceManagerFlags flags, string resourceManagerName, out ResourceManagerHandle resourceManagerHandle)
		{
			return NativeMethods.AuthzInitializeResourceManager(flags, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, resourceManagerName, out resourceManagerHandle);
		}

		// Token: 0x0600047F RID: 1151
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("AUTHZ.DLL")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AuthzFreeContext(IntPtr clientContextHandle);

		// Token: 0x06000480 RID: 1152
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("AUTHZ.DLL")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AuthzFreeResourceManager(IntPtr resourceManagerHandle);

		// Token: 0x06000481 RID: 1153
		[DllImport("NTDSAPI.DLL", CharSet = CharSet.Unicode, EntryPoint = "DsBindW")]
		internal static extern uint DsBind([In] string domainControllerName, [In] string dnsDomainName, out SafeDsHandle handle);

		// Token: 0x06000482 RID: 1154
		[DllImport("NTDSAPI.DLL", CharSet = CharSet.Unicode, EntryPoint = "DsWriteAccountSpnW")]
		internal static extern uint DsWriteAccountSpn([In] SafeDsHandle handle, [In] NativeMethods.SpnWriteOperation operation, [In] string account, [In] uint spnCount, [In] string[] spns);

		// Token: 0x06000483 RID: 1155
		[DllImport("NTDSAPI.DLL", CharSet = CharSet.Unicode, EntryPoint = "DsGetSpnW")]
		internal static extern int DsGetSpn([In] NativeMethods.SpnNameType serviceType, [In] string serviceClass, [In] string serviceName, [In] ushort instancePort, [In] ushort instanceNameCount, [In] string[] instanceNames, [In] ushort[] instancePorts, out uint spnCount, out SafeSpnArrayHandle spnArray);

		// Token: 0x06000484 RID: 1156 RVA: 0x00011DFC File Offset: 0x0000FFFC
		internal static uint DsGetDcName(string server, string domainName, string siteName, NativeMethods.DsGetDCNameFlags flags, out SafeDomainControllerInfoHandle domainControllerInfo)
		{
			return NativeMethods.DsGetDcName(server, domainName, IntPtr.Zero, siteName, flags, out domainControllerInfo);
		}

		// Token: 0x06000485 RID: 1157
		[DllImport("NETAPI32.DLL", CharSet = CharSet.Unicode, EntryPoint = "DsGetDcNameW")]
		private static extern uint DsGetDcName([In] string server, [In] string domainName, [In] IntPtr guidPtr, [In] string siteName, [In] NativeMethods.DsGetDCNameFlags flags, out SafeDomainControllerInfoHandle domainControllerInfo);

		// Token: 0x06000486 RID: 1158
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		public static extern IoCompletionPort CreateIoCompletionPort(SafeFileHandle fileHandle, IoCompletionPort existingCompletionPort, UIntPtr completionKey, uint numberOfConcurrentThreads);

		// Token: 0x06000487 RID: 1159
		[DllImport("KERNEL32.DLL", CharSet = CharSet.Unicode, EntryPoint = "CreateJobObjectW", SetLastError = true)]
		public static extern SafeJobHandle CreateJobObject(IntPtr jobAttributes, string name);

		// Token: 0x06000488 RID: 1160
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		public static extern SafeJobHandle OpenJobObject(uint dwDesiredAccess, bool bInheritHandles, string jobName);

		// Token: 0x06000489 RID: 1161
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsProcessInJob(SafeProcessHandle processHandle, SafeJobHandle job, [MarshalAs(UnmanagedType.Bool)] out bool isProcessInJob);

		// Token: 0x0600048A RID: 1162
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public unsafe static extern bool SetInformationJobObject(SafeJobHandle job, NativeMethods.JOBOBJECTINFOCLASS jobObjectInfoClass, void* jobObjectInfo, int jobObjectInfoLength);

		// Token: 0x0600048B RID: 1163
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public unsafe static extern bool QueryInformationJobObject(SafeJobHandle job, NativeMethods.JOBOBJECTINFOCLASS iobObjectInfoClass, void* jobObjectInfo, uint jobObjectInfoLength, out uint returnLength);

		// Token: 0x0600048C RID: 1164
		[DllImport("KERNEL32.DLL", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AssignProcessToJobObject(SafeJobHandle job, IntPtr processHandle);

		// Token: 0x0600048D RID: 1165
		[DllImport("KERNEL32.DLL", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AssignProcessToJobObject(SafeJobHandle job, SafeProcessHandle processHandle);

		// Token: 0x0600048E RID: 1166
		[DllImport("KERNEL32.DLL", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool TerminateJobObject(SafeJobHandle job, uint uExitCode);

		// Token: 0x0600048F RID: 1167
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("ADVAPI32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetKernelObjectSecurity(SafeHandle handle, [MarshalAs(UnmanagedType.U4)] SecurityInfos requestedInformation, IntPtr securityDescriptor, uint length, out uint lengthNeeded);

		// Token: 0x06000490 RID: 1168
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("ADVAPI32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetKernelObjectSecurity(SafeHandle handle, [MarshalAs(UnmanagedType.U4)] SecurityInfos securityInformation, IntPtr securityDescriptor);

		// Token: 0x06000491 RID: 1169
		[DllImport("KERNEL32.DLL", CharSet = CharSet.Unicode, EntryPoint = "CreateNamedPipeW", SetLastError = true)]
		internal static extern SafeFileHandle CreateNamedPipe(string name, NativeMethods.PipeOpen openMode, NativeMethods.PipeModes pipeMode, uint maxInstances, uint outBufferSize, uint inBufferSize, uint defaultTimeOut, IntPtr securityAttributes);

		// Token: 0x06000492 RID: 1170
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetNamedPipeHandleState(SafeFileHandle pipeHandle, ref NativeMethods.PipeModes mode, IntPtr maxCollectionCount, IntPtr collectDataTimeout);

		// Token: 0x06000493 RID: 1171
		[DllImport("KERNEL32.DLL", CharSet = CharSet.Unicode, EntryPoint = "CreateFileW", SetLastError = true)]
		internal static extern SafeFileHandle CreateFile([In] string lpFileName, [In] NativeMethods.CreateFileAccess dwDesiredAccess, [In] NativeMethods.CreateFileShare dwShareMode, [In] ref NativeMethods.SECURITY_ATTRIBUTES lpSecurityAttributes, [In] FileMode dwCreationDisposition, [In] NativeMethods.CreateFileFileAttributes dwFlagsAndAttributes, [In] IntPtr hTemplateFile);

		// Token: 0x06000494 RID: 1172
		[DllImport("RPCRT4.DLL")]
		internal static extern int RpcCancelThreadEx([In] SafeThreadHandle threadHandle, [In] int timeoutSeconds);

		// Token: 0x06000495 RID: 1173
		[DllImport("KERNEL32.DLL", CharSet = CharSet.Unicode, EntryPoint = "CreateFileMappingW", SetLastError = true)]
		public static extern SafeFileHandle CreateFileMapping([In] SafeFileHandle hFile, [In] ref NativeMethods.SECURITY_ATTRIBUTES lpAttributes, [In] NativeMethods.MemoryAccessControl accessControl, [In] uint dwMaximumSizeHigh, [In] uint dwMaximumSizeLow, [In] string lpName);

		// Token: 0x06000496 RID: 1174
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		public static extern SafeViewOfFileHandle MapViewOfFile([In] SafeFileHandle fileMappingObject, [In] NativeMethods.FileMapAccessControl desiredAccess, [In] uint dwFileOffsetHigh, [In] uint dwFileOffsetLow, [In] UIntPtr dwNumBytesToMap);

		// Token: 0x06000497 RID: 1175
		[DllImport("NTDSAPI.DLL", CharSet = CharSet.Unicode, EntryPoint = "DsCrackNamesW", ExactSpelling = true)]
		internal static extern uint DsCrackNames(SafeDsHandle dsHandle, NativeMethods.DSNameFlags flags, NativeMethods.DSNameFormat formatOffered, NativeMethods.DSNameFormat formatDesired, uint nameCount, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 4)] string[] toBeResolvedNames, out SafeDsNameResultHandle dsNameResultHandle);

		// Token: 0x06000498 RID: 1176 RVA: 0x00011E10 File Offset: 0x00010010
		public static bool CreatePrivateObjectSecurityEx([In] byte[] parentDescriptorBlob, [In] byte[] creatorDescriptorBlob, out byte[] newDescriptorBlob, [In] Guid objectType, [In] bool isContainerObject, [In] uint autoInheritFlags, [In] WindowsIdentity identity, [In] NativeMethods.GENERIC_MAPPING mapping)
		{
			bool flag = false;
			SafeHGlobalHandle safeHGlobalHandle = null;
			SafePrivateObjectSecurityDescriptorHandle safePrivateObjectSecurityDescriptorHandle = null;
			newDescriptorBlob = null;
			try
			{
				if (objectType != Guid.Empty)
				{
					byte[] array = objectType.ToByteArray();
					safeHGlobalHandle = NativeMethods.AllocHGlobal(array.Length);
					Marshal.Copy(array, 0, safeHGlobalHandle.DangerousGetHandle(), array.Length);
				}
				else
				{
					safeHGlobalHandle = SafeHGlobalHandle.InvalidHandle;
				}
				flag = NativeMethods.CreatePrivateObjectSecurityEx(parentDescriptorBlob, creatorDescriptorBlob, out safePrivateObjectSecurityDescriptorHandle, safeHGlobalHandle, isContainerObject, autoInheritFlags, (identity != null) ? identity.Token : ((IntPtr)0), ref mapping);
				if (flag)
				{
					int securityDescriptorLength = (int)NativeMethods.GetSecurityDescriptorLength(safePrivateObjectSecurityDescriptorHandle);
					newDescriptorBlob = new byte[securityDescriptorLength];
					Marshal.Copy(safePrivateObjectSecurityDescriptorHandle.DangerousGetHandle(), newDescriptorBlob, 0, securityDescriptorLength);
				}
			}
			finally
			{
				safeHGlobalHandle.Dispose();
				if (safePrivateObjectSecurityDescriptorHandle != null)
				{
					safePrivateObjectSecurityDescriptorHandle.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x06000499 RID: 1177
		[DllImport("ADVAPI32.DLL", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CreatePrivateObjectSecurityEx([MarshalAs(UnmanagedType.LPArray)] [In] byte[] parentDescriptor, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] creatorDescriptor, out SafePrivateObjectSecurityDescriptorHandle newDescriptor, [In] SafeHGlobalHandle objectType, [MarshalAs(UnmanagedType.Bool)] [In] bool isContainerObject, [MarshalAs(UnmanagedType.U4)] [In] uint autoInheritFlags, [In] IntPtr token, [In] ref NativeMethods.GENERIC_MAPPING mapping);

		// Token: 0x0400020D RID: 525
		private const string Kernel32 = "KERNEL32.DLL";

		// Token: 0x0400020E RID: 526
		private const string AdvApi32 = "ADVAPI32.DLL";

		// Token: 0x0400020F RID: 527
		private const string AuthZ32 = "AUTHZ.DLL";

		// Token: 0x04000210 RID: 528
		private const string NtdsApi = "NTDSAPI.DLL";

		// Token: 0x04000211 RID: 529
		private const string Secur32 = "SECUR32.DLL";

		// Token: 0x04000212 RID: 530
		private const string PSAPI = "PSAPI.DLL";

		// Token: 0x04000213 RID: 531
		private const string RpcRt4 = "RPCRT4.DLL";

		// Token: 0x04000214 RID: 532
		private const string NetApi32 = "NETAPI32.DLL";

		// Token: 0x04000215 RID: 533
		internal const uint JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE = 8192U;

		// Token: 0x04000216 RID: 534
		internal const uint NMPWAIT_WAIT_FOREVER = 4294967295U;

		// Token: 0x04000217 RID: 535
		internal const uint SDDL_REVISION_1 = 1U;

		// Token: 0x04000218 RID: 536
		internal const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x04000219 RID: 537
		internal const int ErrorSuccess = 0;

		// Token: 0x02000084 RID: 132
		internal enum ErrorCode
		{
			// Token: 0x0400021B RID: 539
			Success,
			// Token: 0x0400021C RID: 540
			FileNotFound = 2,
			// Token: 0x0400021D RID: 541
			PathNotFound,
			// Token: 0x0400021E RID: 542
			AccessDenied = 5,
			// Token: 0x0400021F RID: 543
			InvalidParameter = 87,
			// Token: 0x04000220 RID: 544
			InsufficientBuffer = 122,
			// Token: 0x04000221 RID: 545
			MoreData = 234,
			// Token: 0x04000222 RID: 546
			WaitTimeout = 258
		}

		// Token: 0x02000085 RID: 133
		[Flags]
		internal enum AccessRights : uint
		{
			// Token: 0x04000224 RID: 548
			Delete = 65536U,
			// Token: 0x04000225 RID: 549
			ReadControl = 131072U,
			// Token: 0x04000226 RID: 550
			WriteDac = 262144U,
			// Token: 0x04000227 RID: 551
			WriteOwner = 524288U,
			// Token: 0x04000228 RID: 552
			Synchronize = 1048576U,
			// Token: 0x04000229 RID: 553
			StandardRightsRequired = 983040U,
			// Token: 0x0400022A RID: 554
			StandardRightsRead = 131072U,
			// Token: 0x0400022B RID: 555
			StandardRightsWrite = 131072U,
			// Token: 0x0400022C RID: 556
			StandardRightsExecute = 131072U,
			// Token: 0x0400022D RID: 557
			StandardRightsAll = 2031616U,
			// Token: 0x0400022E RID: 558
			AccessSystemSecurity = 16777216U,
			// Token: 0x0400022F RID: 559
			MaximumAllowed = 33554432U,
			// Token: 0x04000230 RID: 560
			GenericRead = 2147483648U,
			// Token: 0x04000231 RID: 561
			GenericWrite = 1073741824U,
			// Token: 0x04000232 RID: 562
			GenericExecute = 536870912U,
			// Token: 0x04000233 RID: 563
			GenericAll = 268435456U
		}

		// Token: 0x02000086 RID: 134
		[Flags]
		internal enum MailboxPermissionRights : uint
		{
			// Token: 0x04000235 RID: 565
			SDPERM_USER_MAILBOX_OWNER = 1U,
			// Token: 0x04000236 RID: 566
			SDPERM_USER_SEND_AS = 2U,
			// Token: 0x04000237 RID: 567
			SDPERM_USER_PRIMARY_USER = 4U
		}

		// Token: 0x02000087 RID: 135
		[Flags]
		internal enum AutoInheritFlags : uint
		{
			// Token: 0x04000239 RID: 569
			AutoInheritDACL = 1U,
			// Token: 0x0400023A RID: 570
			AutoInheritSACL = 2U,
			// Token: 0x0400023B RID: 571
			DefaultDescriptorForObject = 4U,
			// Token: 0x0400023C RID: 572
			AvoidPrivilegeCheck = 8U,
			// Token: 0x0400023D RID: 573
			AvoidOwnerCheck = 16U,
			// Token: 0x0400023E RID: 574
			DefaultOwnerFromParent = 32U,
			// Token: 0x0400023F RID: 575
			DefaultGroupFromParent = 64U
		}

		// Token: 0x02000088 RID: 136
		internal struct AuthzLuid
		{
			// Token: 0x04000240 RID: 576
			internal uint LowPart;

			// Token: 0x04000241 RID: 577
			internal int HighPart;
		}

		// Token: 0x02000089 RID: 137
		internal struct SidAndAttributes
		{
			// Token: 0x04000242 RID: 578
			internal IntPtr Sid;

			// Token: 0x04000243 RID: 579
			internal uint Attributes;
		}

		// Token: 0x0200008A RID: 138
		internal struct UserToken
		{
			// Token: 0x04000244 RID: 580
			internal byte[] Sid;

			// Token: 0x04000245 RID: 581
			internal uint Attributes;
		}

		// Token: 0x0200008B RID: 139
		internal struct GroupsToken
		{
			// Token: 0x04000246 RID: 582
			internal uint GroupCount;

			// Token: 0x04000247 RID: 583
			internal NativeMethods.SidAndAttributes[] Groups;
		}

		// Token: 0x0200008C RID: 140
		public struct SystemTime : IEquatable<NativeMethods.SystemTime>
		{
			// Token: 0x0600049A RID: 1178 RVA: 0x00011EC8 File Offset: 0x000100C8
			public static bool operator ==(NativeMethods.SystemTime v1, NativeMethods.SystemTime v2)
			{
				return v1.Equals(v2);
			}

			// Token: 0x0600049B RID: 1179 RVA: 0x00011ED2 File Offset: 0x000100D2
			public static bool operator !=(NativeMethods.SystemTime v1, NativeMethods.SystemTime v2)
			{
				return !v1.Equals(v2);
			}

			// Token: 0x0600049C RID: 1180 RVA: 0x00011EE0 File Offset: 0x000100E0
			public static NativeMethods.SystemTime Parse(ArraySegment<byte> buffer)
			{
				if (buffer.Count < NativeMethods.SystemTime.Size)
				{
					throw new ArgumentOutOfRangeException();
				}
				NativeMethods.SystemTime result;
				result.Year = BitConverter.ToUInt16(buffer.Array, buffer.Offset + NativeMethods.SystemTime.YearOffset);
				result.Month = BitConverter.ToUInt16(buffer.Array, buffer.Offset + NativeMethods.SystemTime.MonthOffset);
				result.DayOfWeek = BitConverter.ToUInt16(buffer.Array, buffer.Offset + NativeMethods.SystemTime.DayOfWeekOffset);
				result.Day = BitConverter.ToUInt16(buffer.Array, buffer.Offset + NativeMethods.SystemTime.DayOffset);
				result.Hour = BitConverter.ToUInt16(buffer.Array, buffer.Offset + NativeMethods.SystemTime.HourOffset);
				result.Minute = BitConverter.ToUInt16(buffer.Array, buffer.Offset + NativeMethods.SystemTime.MinuteOffset);
				result.Second = BitConverter.ToUInt16(buffer.Array, buffer.Offset + NativeMethods.SystemTime.SecondOffset);
				result.Milliseconds = BitConverter.ToUInt16(buffer.Array, buffer.Offset + NativeMethods.SystemTime.MillisecondsOffset);
				return result;
			}

			// Token: 0x0600049D RID: 1181 RVA: 0x00012002 File Offset: 0x00010202
			public override bool Equals(object o)
			{
				return o is NativeMethods.SystemTime && this.Equals((NativeMethods.SystemTime)o);
			}

			// Token: 0x0600049E RID: 1182 RVA: 0x0001201C File Offset: 0x0001021C
			public bool Equals(NativeMethods.SystemTime v)
			{
				return this.Milliseconds == v.Milliseconds && this.Second == v.Second && this.Minute == v.Minute && this.Hour == v.Hour && this.Day == v.Day && this.DayOfWeek == v.DayOfWeek && this.Month == v.Month && this.Year == v.Year;
			}

			// Token: 0x0600049F RID: 1183 RVA: 0x000120A3 File Offset: 0x000102A3
			public override int GetHashCode()
			{
				return (int)this.Hour << 28 | (int)this.Minute << 20 | (int)this.Second << 12 | (int)this.Milliseconds;
			}

			// Token: 0x060004A0 RID: 1184 RVA: 0x000120CC File Offset: 0x000102CC
			public int Write(ArraySegment<byte> buffer)
			{
				if (buffer.Count < NativeMethods.SystemTime.Size)
				{
					throw new ArgumentOutOfRangeException();
				}
				ExBitConverter.Write(this.Year, buffer.Array, buffer.Offset + NativeMethods.SystemTime.YearOffset);
				ExBitConverter.Write(this.Month, buffer.Array, buffer.Offset + NativeMethods.SystemTime.MonthOffset);
				ExBitConverter.Write(this.DayOfWeek, buffer.Array, buffer.Offset + NativeMethods.SystemTime.DayOfWeekOffset);
				ExBitConverter.Write(this.Day, buffer.Array, buffer.Offset + NativeMethods.SystemTime.DayOffset);
				ExBitConverter.Write(this.Hour, buffer.Array, buffer.Offset + NativeMethods.SystemTime.HourOffset);
				ExBitConverter.Write(this.Minute, buffer.Array, buffer.Offset + NativeMethods.SystemTime.MinuteOffset);
				ExBitConverter.Write(this.Second, buffer.Array, buffer.Offset + NativeMethods.SystemTime.SecondOffset);
				ExBitConverter.Write(this.Milliseconds, buffer.Array, buffer.Offset + NativeMethods.SystemTime.MillisecondsOffset);
				return NativeMethods.SystemTime.Size;
			}

			// Token: 0x04000248 RID: 584
			internal ushort Year;

			// Token: 0x04000249 RID: 585
			internal ushort Month;

			// Token: 0x0400024A RID: 586
			internal ushort DayOfWeek;

			// Token: 0x0400024B RID: 587
			internal ushort Day;

			// Token: 0x0400024C RID: 588
			internal ushort Hour;

			// Token: 0x0400024D RID: 589
			internal ushort Minute;

			// Token: 0x0400024E RID: 590
			internal ushort Second;

			// Token: 0x0400024F RID: 591
			internal ushort Milliseconds;

			// Token: 0x04000250 RID: 592
			public static readonly int Size = Marshal.SizeOf(typeof(NativeMethods.SystemTime));

			// Token: 0x04000251 RID: 593
			private static readonly int YearOffset = (int)Marshal.OffsetOf(typeof(NativeMethods.SystemTime), "Year");

			// Token: 0x04000252 RID: 594
			private static readonly int MonthOffset = (int)Marshal.OffsetOf(typeof(NativeMethods.SystemTime), "Month");

			// Token: 0x04000253 RID: 595
			private static readonly int DayOfWeekOffset = (int)Marshal.OffsetOf(typeof(NativeMethods.SystemTime), "DayOfWeek");

			// Token: 0x04000254 RID: 596
			private static readonly int DayOffset = (int)Marshal.OffsetOf(typeof(NativeMethods.SystemTime), "Day");

			// Token: 0x04000255 RID: 597
			private static readonly int HourOffset = (int)Marshal.OffsetOf(typeof(NativeMethods.SystemTime), "Hour");

			// Token: 0x04000256 RID: 598
			private static readonly int MinuteOffset = (int)Marshal.OffsetOf(typeof(NativeMethods.SystemTime), "Minute");

			// Token: 0x04000257 RID: 599
			private static readonly int SecondOffset = (int)Marshal.OffsetOf(typeof(NativeMethods.SystemTime), "Second");

			// Token: 0x04000258 RID: 600
			private static readonly int MillisecondsOffset = (int)Marshal.OffsetOf(typeof(NativeMethods.SystemTime), "Milliseconds");
		}

		// Token: 0x0200008D RID: 141
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct TIME_ZONE_INFORMATION
		{
			// Token: 0x04000259 RID: 601
			public int Bias;

			// Token: 0x0400025A RID: 602
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string StandardName;

			// Token: 0x0400025B RID: 603
			public NativeMethods.SystemTime StandardDate;

			// Token: 0x0400025C RID: 604
			public int StandardBias;

			// Token: 0x0400025D RID: 605
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string DaylightName;

			// Token: 0x0400025E RID: 606
			public NativeMethods.SystemTime DaylightDate;

			// Token: 0x0400025F RID: 607
			public int DaylightBias;
		}

		// Token: 0x0200008E RID: 142
		internal enum TimeZoneId : uint
		{
			// Token: 0x04000261 RID: 609
			Unknown,
			// Token: 0x04000262 RID: 610
			Standard,
			// Token: 0x04000263 RID: 611
			DayLight,
			// Token: 0x04000264 RID: 612
			Invalid = 4294967295U
		}

		// Token: 0x0200008F RID: 143
		[Flags]
		public enum FileAttributes
		{
			// Token: 0x04000266 RID: 614
			None = 0,
			// Token: 0x04000267 RID: 615
			ReadOnly = 1,
			// Token: 0x04000268 RID: 616
			Hidden = 2,
			// Token: 0x04000269 RID: 617
			System = 4,
			// Token: 0x0400026A RID: 618
			Directory = 16,
			// Token: 0x0400026B RID: 619
			Archive = 32,
			// Token: 0x0400026C RID: 620
			Device = 64,
			// Token: 0x0400026D RID: 621
			Normal = 128,
			// Token: 0x0400026E RID: 622
			Temporary = 256,
			// Token: 0x0400026F RID: 623
			SparseFile = 512,
			// Token: 0x04000270 RID: 624
			ReparsePoint = 1024,
			// Token: 0x04000271 RID: 625
			Compressed = 2048,
			// Token: 0x04000272 RID: 626
			Offline = 4096,
			// Token: 0x04000273 RID: 627
			NonContentIndexed = 8192,
			// Token: 0x04000274 RID: 628
			Encrypted = 16384,
			// Token: 0x04000275 RID: 629
			Virtual = 65536
		}

		// Token: 0x02000090 RID: 144
		[BestFitMapping(false)]
		[Serializable]
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct WIN32_FIND_DATA
		{
			// Token: 0x04000276 RID: 630
			internal NativeMethods.FileAttributes FileAttributes;

			// Token: 0x04000277 RID: 631
			internal System.Runtime.InteropServices.ComTypes.FILETIME CreationTime;

			// Token: 0x04000278 RID: 632
			internal System.Runtime.InteropServices.ComTypes.FILETIME LastAccessTime;

			// Token: 0x04000279 RID: 633
			internal System.Runtime.InteropServices.ComTypes.FILETIME LastWriteTime;

			// Token: 0x0400027A RID: 634
			internal uint FileSizeHigh;

			// Token: 0x0400027B RID: 635
			internal uint FileSizeLow;

			// Token: 0x0400027C RID: 636
			internal uint Reserved0;

			// Token: 0x0400027D RID: 637
			internal uint Reserved1;

			// Token: 0x0400027E RID: 638
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string FileName;

			// Token: 0x0400027F RID: 639
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
			internal string AlternateFileName;
		}

		// Token: 0x02000091 RID: 145
		internal struct TokenGroups
		{
			// Token: 0x04000280 RID: 640
			public int count;

			// Token: 0x04000281 RID: 641
			public NativeMethods.SidAndAttributes sidAndAttributes;

			// Token: 0x04000282 RID: 642
			internal static readonly int SidAndAttributesOffset = (int)Marshal.OffsetOf(typeof(NativeMethods.TokenGroups), "sidAndAttributes");

			// Token: 0x04000283 RID: 643
			internal static readonly int Size = Marshal.SizeOf(typeof(NativeMethods.TokenGroups));
		}

		// Token: 0x02000092 RID: 146
		public struct SecurityIdentifierAndAttributes
		{
			// Token: 0x060004A3 RID: 1187 RVA: 0x00012339 File Offset: 0x00010539
			public SecurityIdentifierAndAttributes(SecurityIdentifier sid, int attributes)
			{
				this.sid = sid;
				this.attributes = attributes;
			}

			// Token: 0x060004A4 RID: 1188 RVA: 0x00012349 File Offset: 0x00010549
			public SecurityIdentifierAndAttributes(IntPtr pointer)
			{
				this.sid = new SecurityIdentifier(Marshal.ReadIntPtr(pointer, NativeMethods.SecurityIdentifierAndAttributes.SidOffset));
				this.attributes = Marshal.ReadInt32(pointer, NativeMethods.SecurityIdentifierAndAttributes.AttributesOffset);
			}

			// Token: 0x04000284 RID: 644
			public SecurityIdentifier sid;

			// Token: 0x04000285 RID: 645
			public int attributes;

			// Token: 0x04000286 RID: 646
			private static readonly int SidOffset = (int)Marshal.OffsetOf(typeof(NativeMethods.SidAndAttributes), "Sid");

			// Token: 0x04000287 RID: 647
			private static readonly int AttributesOffset = (int)Marshal.OffsetOf(typeof(NativeMethods.SidAndAttributes), "Attributes");

			// Token: 0x04000288 RID: 648
			internal static readonly int SidAndAttributesSize = Marshal.SizeOf(typeof(NativeMethods.SidAndAttributes));
		}

		// Token: 0x02000093 RID: 147
		internal enum ExtendedNameFormat
		{
			// Token: 0x0400028A RID: 650
			Unknown,
			// Token: 0x0400028B RID: 651
			FullyQualifiedDN,
			// Token: 0x0400028C RID: 652
			SamCompatible,
			// Token: 0x0400028D RID: 653
			Display,
			// Token: 0x0400028E RID: 654
			UniqueId = 6,
			// Token: 0x0400028F RID: 655
			Canonical,
			// Token: 0x04000290 RID: 656
			UserPrincipal,
			// Token: 0x04000291 RID: 657
			CanonicalEx,
			// Token: 0x04000292 RID: 658
			ServicePrincipal,
			// Token: 0x04000293 RID: 659
			DnsDomain = 12
		}

		// Token: 0x02000094 RID: 148
		internal enum JobObjectRateControlToleranceInterval
		{
			// Token: 0x04000295 RID: 661
			ToleranceIntervalShort = 1,
			// Token: 0x04000296 RID: 662
			ToleranceIntervalMedium,
			// Token: 0x04000297 RID: 663
			ToleranceIntervalLong
		}

		// Token: 0x02000095 RID: 149
		internal enum JobObjectRateControlTolerance
		{
			// Token: 0x04000299 RID: 665
			ToleranceLow = 1,
			// Token: 0x0400029A RID: 666
			ToleranceMedium,
			// Token: 0x0400029B RID: 667
			ToleranceHigh
		}

		// Token: 0x02000096 RID: 150
		[Flags]
		internal enum JobCpuRateControlLimit : uint
		{
			// Token: 0x0400029D RID: 669
			CpuRateControlEnable = 1U,
			// Token: 0x0400029E RID: 670
			CpuRateControlWeightBased = 2U,
			// Token: 0x0400029F RID: 671
			CpuRateControlHardCap = 4U,
			// Token: 0x040002A0 RID: 672
			CpuRateControlNotify = 8U
		}

		// Token: 0x02000097 RID: 151
		[Flags]
		internal enum JobNotificationLimit : uint
		{
			// Token: 0x040002A2 RID: 674
			JobObjectLimitRateControl = 262144U,
			// Token: 0x040002A3 RID: 675
			JobObjectLimitJobMemory = 512U
		}

		// Token: 0x02000098 RID: 152
		internal enum JobObjectAccessRights : uint
		{
			// Token: 0x040002A5 RID: 677
			JobObjectAllAccess = 2031647U
		}

		// Token: 0x02000099 RID: 153
		internal enum SpnWriteOperation
		{
			// Token: 0x040002A7 RID: 679
			Add,
			// Token: 0x040002A8 RID: 680
			Replace,
			// Token: 0x040002A9 RID: 681
			Delete
		}

		// Token: 0x0200009A RID: 154
		internal enum SpnNameType
		{
			// Token: 0x040002AB RID: 683
			DnsHost,
			// Token: 0x040002AC RID: 684
			DnHost,
			// Token: 0x040002AD RID: 685
			NetbiosHost,
			// Token: 0x040002AE RID: 686
			Domain,
			// Token: 0x040002AF RID: 687
			NetbiosDomain,
			// Token: 0x040002B0 RID: 688
			Service
		}

		// Token: 0x0200009B RID: 155
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct DomainControllerInformation
		{
			// Token: 0x040002B1 RID: 689
			public string DomainControllerName;

			// Token: 0x040002B2 RID: 690
			public string DomainControllerAddress;

			// Token: 0x040002B3 RID: 691
			public uint DomainControllerAddressType;

			// Token: 0x040002B4 RID: 692
			public Guid DomainGuid;

			// Token: 0x040002B5 RID: 693
			public string DomainName;

			// Token: 0x040002B6 RID: 694
			public string DnsForestName;

			// Token: 0x040002B7 RID: 695
			public uint Flags;

			// Token: 0x040002B8 RID: 696
			public string DcSiteName;

			// Token: 0x040002B9 RID: 697
			public string ClientSiteName;
		}

		// Token: 0x0200009C RID: 156
		[Flags]
		internal enum DsGetDCNameFlags : uint
		{
			// Token: 0x040002BB RID: 699
			ForceRediscovery = 1U,
			// Token: 0x040002BC RID: 700
			DirectoryServiceRequired = 16U,
			// Token: 0x040002BD RID: 701
			DirectoryServicePreferred = 32U,
			// Token: 0x040002BE RID: 702
			GCServerRequired = 64U,
			// Token: 0x040002BF RID: 703
			PDCRequired = 128U,
			// Token: 0x040002C0 RID: 704
			BackgroundOnly = 256U,
			// Token: 0x040002C1 RID: 705
			IPRequired = 512U,
			// Token: 0x040002C2 RID: 706
			KDCRequired = 1024U,
			// Token: 0x040002C3 RID: 707
			TimeServRequired = 2048U,
			// Token: 0x040002C4 RID: 708
			WritableRequired = 4096U,
			// Token: 0x040002C5 RID: 709
			GoodTimeServPreferred = 8192U,
			// Token: 0x040002C6 RID: 710
			AvoidSelf = 16384U,
			// Token: 0x040002C7 RID: 711
			OnlyLDAPNeeded = 32768U,
			// Token: 0x040002C8 RID: 712
			IsFlatName = 65536U,
			// Token: 0x040002C9 RID: 713
			IsDNSName = 131072U,
			// Token: 0x040002CA RID: 714
			ReturnDNSName = 1073741824U,
			// Token: 0x040002CB RID: 715
			ReturnFlatName = 2147483648U
		}

		// Token: 0x0200009D RID: 157
		internal struct MEMORYSTATUSEX
		{
			// Token: 0x040002CC RID: 716
			public uint dwLength;

			// Token: 0x040002CD RID: 717
			public uint dwMemoryLoad;

			// Token: 0x040002CE RID: 718
			public ulong ullTotalPhys;

			// Token: 0x040002CF RID: 719
			public ulong ullAvailPhys;

			// Token: 0x040002D0 RID: 720
			public ulong ullTotalPageFile;

			// Token: 0x040002D1 RID: 721
			public ulong ullAvailPageFile;

			// Token: 0x040002D2 RID: 722
			public ulong ullTotalVirtual;

			// Token: 0x040002D3 RID: 723
			public ulong ullAvailVirtual;

			// Token: 0x040002D4 RID: 724
			public ulong ullAvailExtendedVirtual;
		}

		// Token: 0x0200009E RID: 158
		internal struct IO_COUNTERS
		{
			// Token: 0x040002D5 RID: 725
			public ulong ReadOperationCount;

			// Token: 0x040002D6 RID: 726
			public ulong WriteOperationCount;

			// Token: 0x040002D7 RID: 727
			public ulong OtherOperationCount;

			// Token: 0x040002D8 RID: 728
			public ulong ReadTransferCount;

			// Token: 0x040002D9 RID: 729
			public ulong WriteTransferCount;

			// Token: 0x040002DA RID: 730
			public ulong OtherTransferCount;
		}

		// Token: 0x0200009F RID: 159
		internal struct JOBOBJECT_BASIC_LIMIT_INFORMATION
		{
			// Token: 0x040002DB RID: 731
			public long PerProcessUserTimeLimit;

			// Token: 0x040002DC RID: 732
			public long PerJobUserTimeLimit;

			// Token: 0x040002DD RID: 733
			public uint LimitFlags;

			// Token: 0x040002DE RID: 734
			public UIntPtr MinimumWorkingSetSize;

			// Token: 0x040002DF RID: 735
			public UIntPtr MaximumWorkingSetSize;

			// Token: 0x040002E0 RID: 736
			public uint ActiveProcessLimit;

			// Token: 0x040002E1 RID: 737
			public IntPtr Affinity;

			// Token: 0x040002E2 RID: 738
			public uint PriorityClass;

			// Token: 0x040002E3 RID: 739
			public uint SchedulingClass;
		}

		// Token: 0x020000A0 RID: 160
		internal struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
		{
			// Token: 0x040002E4 RID: 740
			public NativeMethods.JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;

			// Token: 0x040002E5 RID: 741
			public NativeMethods.IO_COUNTERS IoInfo;

			// Token: 0x040002E6 RID: 742
			public UIntPtr ProcessMemoryLimit;

			// Token: 0x040002E7 RID: 743
			public UIntPtr JobMemoryLimit;

			// Token: 0x040002E8 RID: 744
			public UIntPtr PeakProcessMemoryUsed;

			// Token: 0x040002E9 RID: 745
			public UIntPtr PeakJobMemoryUsed;
		}

		// Token: 0x020000A1 RID: 161
		internal struct JOBOBJECT_CPU_RATE_CONTROL_INFORMATION
		{
			// Token: 0x040002EA RID: 746
			public uint ControlFlags;

			// Token: 0x040002EB RID: 747
			public uint CpuRate;
		}

		// Token: 0x020000A2 RID: 162
		internal struct JOBOBJECT_NOTIFICATION_LIMIT_INFORMATION
		{
			// Token: 0x040002EC RID: 748
			public ulong IoReadBytesLimit;

			// Token: 0x040002ED RID: 749
			public ulong IoWriteBytesLimit;

			// Token: 0x040002EE RID: 750
			public long PerJobUserTimeLimit;

			// Token: 0x040002EF RID: 751
			public ulong JobMemoryLimit;

			// Token: 0x040002F0 RID: 752
			public NativeMethods.JobObjectRateControlTolerance RateControlTolerance;

			// Token: 0x040002F1 RID: 753
			public NativeMethods.JobObjectRateControlToleranceInterval RateControlToleranceInterval;

			// Token: 0x040002F2 RID: 754
			public uint LimitFlags;
		}

		// Token: 0x020000A3 RID: 163
		internal struct JOBOBJECT_LIMIT_VIOLATION_INFORMATION
		{
			// Token: 0x040002F3 RID: 755
			public uint LimitFlags;

			// Token: 0x040002F4 RID: 756
			public uint ViolationLimitFlags;

			// Token: 0x040002F5 RID: 757
			public ulong IoReadBytes;

			// Token: 0x040002F6 RID: 758
			public ulong IoReadBytesLimit;

			// Token: 0x040002F7 RID: 759
			public ulong IoWriteBytes;

			// Token: 0x040002F8 RID: 760
			public ulong IoWriteBytesLimit;

			// Token: 0x040002F9 RID: 761
			public long PerJobUserTime;

			// Token: 0x040002FA RID: 762
			public long PerJobUserTimeLimit;

			// Token: 0x040002FB RID: 763
			public ulong JobMemory;

			// Token: 0x040002FC RID: 764
			public ulong JobMemoryLimit;

			// Token: 0x040002FD RID: 765
			public NativeMethods.JobObjectRateControlTolerance RateControlTolerance;

			// Token: 0x040002FE RID: 766
			public NativeMethods.JobObjectRateControlTolerance RateControlToleranceLimit;
		}

		// Token: 0x020000A4 RID: 164
		internal struct JobObjectAssociateCompletionPort
		{
			// Token: 0x060004A6 RID: 1190 RVA: 0x000123D1 File Offset: 0x000105D1
			public JobObjectAssociateCompletionPort(IntPtr completionKey, IntPtr completionPort)
			{
				this.completionKey = completionKey;
				this.completionPort = completionPort;
			}

			// Token: 0x040002FF RID: 767
			private IntPtr completionKey;

			// Token: 0x04000300 RID: 768
			private IntPtr completionPort;
		}

		// Token: 0x020000A5 RID: 165
		internal struct JobObjectBasicProcessIdList
		{
			// Token: 0x04000301 RID: 769
			public uint NumberOfAssignedProcess;

			// Token: 0x04000302 RID: 770
			public uint NumberOfProcessIdsInList;

			// Token: 0x04000303 RID: 771
			public UIntPtr ProcessIdList;
		}

		// Token: 0x020000A6 RID: 166
		public enum JOBOBJECTINFOCLASS
		{
			// Token: 0x04000305 RID: 773
			JobObjectBasicAccountingInformation = 1,
			// Token: 0x04000306 RID: 774
			JobObjectBasicLimitInformation,
			// Token: 0x04000307 RID: 775
			JobObjectBasicProcessIdList,
			// Token: 0x04000308 RID: 776
			JobObjectBasicUIRestrictions,
			// Token: 0x04000309 RID: 777
			JobObjectSecurityLimitInformation,
			// Token: 0x0400030A RID: 778
			JobObjectEndOfJobTimeInformation,
			// Token: 0x0400030B RID: 779
			JobObjectAssociateCompletionPortInformation,
			// Token: 0x0400030C RID: 780
			JobObjectBasicAndIoAccountingInformation,
			// Token: 0x0400030D RID: 781
			JobObjectExtendedLimitInformation,
			// Token: 0x0400030E RID: 782
			JobObjectJobSetInformation,
			// Token: 0x0400030F RID: 783
			JobObjectGroupInformation,
			// Token: 0x04000310 RID: 784
			JobObjectNotificationLimitInformation,
			// Token: 0x04000311 RID: 785
			JobObjectLimitViolationInformation,
			// Token: 0x04000312 RID: 786
			JobObjectGroupInformationEx,
			// Token: 0x04000313 RID: 787
			JobObjectCpuRateControlInformation,
			// Token: 0x04000314 RID: 788
			JobObjectCompletionFilter,
			// Token: 0x04000315 RID: 789
			JobObjectCompletionCounter,
			// Token: 0x04000316 RID: 790
			JobObjectReserved1Information,
			// Token: 0x04000317 RID: 791
			JobObjectReserved2Information,
			// Token: 0x04000318 RID: 792
			JobObjectReserved3Information,
			// Token: 0x04000319 RID: 793
			JobObjectReserved4Information,
			// Token: 0x0400031A RID: 794
			JobObjectReserved5Information,
			// Token: 0x0400031B RID: 795
			JobObjectReserved6Information,
			// Token: 0x0400031C RID: 796
			JobObjectReserved7Information,
			// Token: 0x0400031D RID: 797
			JobObjectReserved8Information,
			// Token: 0x0400031E RID: 798
			MaxJobObjectInfoClass
		}

		// Token: 0x020000A7 RID: 167
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct STARTUPINFO
		{
			// Token: 0x0400031F RID: 799
			private int cb;

			// Token: 0x04000320 RID: 800
			private string lpReserved;

			// Token: 0x04000321 RID: 801
			private string lpDesktop;

			// Token: 0x04000322 RID: 802
			private string lpTitle;

			// Token: 0x04000323 RID: 803
			private int dwX;

			// Token: 0x04000324 RID: 804
			private int dwY;

			// Token: 0x04000325 RID: 805
			private int dwXSize;

			// Token: 0x04000326 RID: 806
			private int dwYSize;

			// Token: 0x04000327 RID: 807
			private int dwXCountChars;

			// Token: 0x04000328 RID: 808
			private int dwYCountChars;

			// Token: 0x04000329 RID: 809
			private int dwFillAttribute;

			// Token: 0x0400032A RID: 810
			private int dwFlags;

			// Token: 0x0400032B RID: 811
			private short wShowWindow;

			// Token: 0x0400032C RID: 812
			private short cbReserved2;

			// Token: 0x0400032D RID: 813
			private IntPtr lpReserved2;

			// Token: 0x0400032E RID: 814
			private IntPtr hStdInput;

			// Token: 0x0400032F RID: 815
			private IntPtr hStdOutput;

			// Token: 0x04000330 RID: 816
			private IntPtr hStdError;
		}

		// Token: 0x020000A8 RID: 168
		internal struct PROCESS_INFORMATION
		{
			// Token: 0x04000331 RID: 817
			public IntPtr hProcess;

			// Token: 0x04000332 RID: 818
			public IntPtr hThread;

			// Token: 0x04000333 RID: 819
			public int dwProcessId;

			// Token: 0x04000334 RID: 820
			public int dwThreadId;
		}

		// Token: 0x020000A9 RID: 169
		[Flags]
		internal enum ProcessAccess
		{
			// Token: 0x04000336 RID: 822
			DupHandle = 64,
			// Token: 0x04000337 RID: 823
			QueryInformation = 1024,
			// Token: 0x04000338 RID: 824
			Synchronize = 1048576,
			// Token: 0x04000339 RID: 825
			SetQuota = 256,
			// Token: 0x0400033A RID: 826
			Terminate = 1
		}

		// Token: 0x020000AA RID: 170
		internal enum TOKEN_INFORMATION_CLASS
		{
			// Token: 0x0400033C RID: 828
			TokenUser = 1,
			// Token: 0x0400033D RID: 829
			TokenGroups,
			// Token: 0x0400033E RID: 830
			TokenPrivileges,
			// Token: 0x0400033F RID: 831
			TokenOwner,
			// Token: 0x04000340 RID: 832
			TokenPrimaryGroup,
			// Token: 0x04000341 RID: 833
			TokenDefaultDacl,
			// Token: 0x04000342 RID: 834
			TokenSource,
			// Token: 0x04000343 RID: 835
			TokenType,
			// Token: 0x04000344 RID: 836
			TokenImpersonationLevel,
			// Token: 0x04000345 RID: 837
			TokenStatistics,
			// Token: 0x04000346 RID: 838
			TokenRestrictedSids,
			// Token: 0x04000347 RID: 839
			TokenSessionId,
			// Token: 0x04000348 RID: 840
			TokenGroupsAndPrivileges,
			// Token: 0x04000349 RID: 841
			TokenSessionReference,
			// Token: 0x0400034A RID: 842
			TokenSandBoxInert,
			// Token: 0x0400034B RID: 843
			TokenAuditPolicy,
			// Token: 0x0400034C RID: 844
			TokenOrigin
		}

		// Token: 0x020000AB RID: 171
		[Flags]
		internal enum TOKEN_PRIVILEGES_ATTRIBUTES : uint
		{
			// Token: 0x0400034E RID: 846
			SE_PRIVILEGE_DISABLED = 0U,
			// Token: 0x0400034F RID: 847
			SE_PRIVILEGE_ENABLED_BY_DEFAULT = 1U,
			// Token: 0x04000350 RID: 848
			SE_PRIVILEGE_ENABLED = 2U,
			// Token: 0x04000351 RID: 849
			SE_PRIVILEGE_REMOVED = 4U,
			// Token: 0x04000352 RID: 850
			SE_PRIVILEGE_USED_FOR_ACCESS = 2147483648U
		}

		// Token: 0x020000AC RID: 172
		internal struct TOKEN_PRIVILEGES
		{
			// Token: 0x04000353 RID: 851
			internal uint PrivilegeCount;

			// Token: 0x04000354 RID: 852
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
			internal NativeMethods.LUID_AND_ATTRIBUTES[] Privileges;
		}

		// Token: 0x020000AD RID: 173
		internal struct LUID_AND_ATTRIBUTES
		{
			// Token: 0x04000355 RID: 853
			internal NativeMethods.LUID Luid;

			// Token: 0x04000356 RID: 854
			internal uint Attributes;
		}

		// Token: 0x020000AE RID: 174
		internal struct LUID
		{
			// Token: 0x04000357 RID: 855
			internal uint LowPart;

			// Token: 0x04000358 RID: 856
			internal uint HighPart;
		}

		// Token: 0x020000AF RID: 175
		[Flags]
		internal enum PipeOpen : uint
		{
			// Token: 0x0400035A RID: 858
			AccessInbound = 1U,
			// Token: 0x0400035B RID: 859
			AccessOutbound = 2U,
			// Token: 0x0400035C RID: 860
			AccessDuplex = 3U,
			// Token: 0x0400035D RID: 861
			FileFlagWriteThrough = 2147483648U
		}

		// Token: 0x020000B0 RID: 176
		[Flags]
		internal enum PipeModes : uint
		{
			// Token: 0x0400035F RID: 863
			TypeByte = 0U,
			// Token: 0x04000360 RID: 864
			TypeMessage = 4U,
			// Token: 0x04000361 RID: 865
			ReadModeByte = 0U,
			// Token: 0x04000362 RID: 866
			ReadModeMessage = 2U
		}

		// Token: 0x020000B1 RID: 177
		[Flags]
		internal enum CreateFileAccess : uint
		{
			// Token: 0x04000364 RID: 868
			GenericRead = 2147483648U,
			// Token: 0x04000365 RID: 869
			GenericWrite = 1073741824U,
			// Token: 0x04000366 RID: 870
			GenericExecute = 536870912U,
			// Token: 0x04000367 RID: 871
			GenericAll = 268435456U,
			// Token: 0x04000368 RID: 872
			FileWriteAttributes = 256U
		}

		// Token: 0x020000B2 RID: 178
		[Flags]
		internal enum CreateFileShare : uint
		{
			// Token: 0x0400036A RID: 874
			None = 0U,
			// Token: 0x0400036B RID: 875
			Read = 1U,
			// Token: 0x0400036C RID: 876
			Write = 2U,
			// Token: 0x0400036D RID: 877
			Delete = 4U
		}

		// Token: 0x020000B3 RID: 179
		[Flags]
		internal enum CreateFileFileAttributes : uint
		{
			// Token: 0x0400036F RID: 879
			None = 0U,
			// Token: 0x04000370 RID: 880
			Readonly = 1U,
			// Token: 0x04000371 RID: 881
			Hidden = 2U,
			// Token: 0x04000372 RID: 882
			System = 4U,
			// Token: 0x04000373 RID: 883
			Directory = 16U,
			// Token: 0x04000374 RID: 884
			Archive = 32U,
			// Token: 0x04000375 RID: 885
			Device = 64U,
			// Token: 0x04000376 RID: 886
			Normal = 128U,
			// Token: 0x04000377 RID: 887
			Temporary = 256U,
			// Token: 0x04000378 RID: 888
			SparseFile = 512U,
			// Token: 0x04000379 RID: 889
			ReparsePoint = 1024U,
			// Token: 0x0400037A RID: 890
			Compressed = 2048U,
			// Token: 0x0400037B RID: 891
			Offline = 4096U,
			// Token: 0x0400037C RID: 892
			NotContentIndexed = 8192U,
			// Token: 0x0400037D RID: 893
			Encrypted = 16384U,
			// Token: 0x0400037E RID: 894
			Write_Through = 2147483648U,
			// Token: 0x0400037F RID: 895
			Overlapped = 1073741824U,
			// Token: 0x04000380 RID: 896
			NoBuffering = 536870912U,
			// Token: 0x04000381 RID: 897
			RandomAccess = 268435456U,
			// Token: 0x04000382 RID: 898
			SequentialScan = 134217728U,
			// Token: 0x04000383 RID: 899
			DeleteOnClose = 67108864U,
			// Token: 0x04000384 RID: 900
			BackupSemantics = 33554432U,
			// Token: 0x04000385 RID: 901
			PosixSemantics = 16777216U,
			// Token: 0x04000386 RID: 902
			OpenReparsePoint = 2097152U,
			// Token: 0x04000387 RID: 903
			OpenNoRecall = 1048576U,
			// Token: 0x04000388 RID: 904
			FirstPipeInstance = 524288U
		}

		// Token: 0x020000B4 RID: 180
		public struct MemoryStatusEx
		{
			// Token: 0x04000389 RID: 905
			public static readonly uint Size = (uint)Marshal.SizeOf(typeof(NativeMethods.MemoryStatusEx));

			// Token: 0x0400038A RID: 906
			public uint Length;

			// Token: 0x0400038B RID: 907
			public uint MemoryLoad;

			// Token: 0x0400038C RID: 908
			public ulong TotalPhys;

			// Token: 0x0400038D RID: 909
			public ulong AvailPhys;

			// Token: 0x0400038E RID: 910
			public ulong TotalPageFile;

			// Token: 0x0400038F RID: 911
			public ulong AvailPageFile;

			// Token: 0x04000390 RID: 912
			public ulong TotalVirtual;

			// Token: 0x04000391 RID: 913
			public ulong AvailVirtual;

			// Token: 0x04000392 RID: 914
			public ulong AvailExtendedVirtual;
		}

		// Token: 0x020000B5 RID: 181
		public struct ProcessMemoryCounterEx
		{
			// Token: 0x04000393 RID: 915
			public static readonly uint Size = (uint)Marshal.SizeOf(typeof(NativeMethods.ProcessMemoryCounterEx));

			// Token: 0x04000394 RID: 916
			public uint cb;

			// Token: 0x04000395 RID: 917
			public uint pageFaultCount;

			// Token: 0x04000396 RID: 918
			public UIntPtr peakWorkingSetSize;

			// Token: 0x04000397 RID: 919
			public UIntPtr workingSetSize;

			// Token: 0x04000398 RID: 920
			public UIntPtr quotaPeakPagedPoolUsage;

			// Token: 0x04000399 RID: 921
			public UIntPtr quotaPagedPoolUsage;

			// Token: 0x0400039A RID: 922
			public UIntPtr quotaPeakNonPagedPoolUsage;

			// Token: 0x0400039B RID: 923
			public UIntPtr quotaNonPagedPoolUsage;

			// Token: 0x0400039C RID: 924
			public UIntPtr pagefileUsage;

			// Token: 0x0400039D RID: 925
			public UIntPtr peakPagefileUsage;

			// Token: 0x0400039E RID: 926
			public UIntPtr privateUsage;
		}

		// Token: 0x020000B6 RID: 182
		[Flags]
		internal enum MemoryAccessControl : uint
		{
			// Token: 0x040003A0 RID: 928
			Readonly = 2U,
			// Token: 0x040003A1 RID: 929
			ReadWrite = 4U,
			// Token: 0x040003A2 RID: 930
			WriteCopy = 8U,
			// Token: 0x040003A3 RID: 931
			ExecuteRead = 32U,
			// Token: 0x040003A4 RID: 932
			ExecuteReadWrite = 64U,
			// Token: 0x040003A5 RID: 933
			SecFile = 8388608U,
			// Token: 0x040003A6 RID: 934
			SecImage = 16777216U,
			// Token: 0x040003A7 RID: 935
			SecProtectedImage = 33554432U,
			// Token: 0x040003A8 RID: 936
			SecReserver = 67108864U,
			// Token: 0x040003A9 RID: 937
			SecCommit = 134217728U,
			// Token: 0x040003AA RID: 938
			SecNoCache = 268435456U,
			// Token: 0x040003AB RID: 939
			SecWriteCombine = 1073741824U,
			// Token: 0x040003AC RID: 940
			SecLargePages = 2147483648U
		}

		// Token: 0x020000B7 RID: 183
		internal enum FileMapAccessControl : uint
		{
			// Token: 0x040003AE RID: 942
			Copy = 1U,
			// Token: 0x040003AF RID: 943
			Write,
			// Token: 0x040003B0 RID: 944
			Read = 4U,
			// Token: 0x040003B1 RID: 945
			Execute = 32U
		}

		// Token: 0x020000B8 RID: 184
		public struct SECURITY_ATTRIBUTES
		{
			// Token: 0x060004A9 RID: 1193 RVA: 0x0001240D File Offset: 0x0001060D
			public SECURITY_ATTRIBUTES(SafeHGlobalHandle securityDescriptor)
			{
				this.nLength = NativeMethods.SECURITY_ATTRIBUTES.Size;
				this.lpSecurityDescriptor = securityDescriptor;
				this.bInheritHandle = false;
			}

			// Token: 0x040003B2 RID: 946
			public static readonly uint Size = (uint)Marshal.SizeOf(typeof(NativeMethods.SECURITY_ATTRIBUTES));

			// Token: 0x040003B3 RID: 947
			internal uint nLength;

			// Token: 0x040003B4 RID: 948
			internal SafeHGlobalHandle lpSecurityDescriptor;

			// Token: 0x040003B5 RID: 949
			[MarshalAs(UnmanagedType.Bool)]
			internal bool bInheritHandle;
		}

		// Token: 0x020000B9 RID: 185
		public struct GENERIC_MAPPING
		{
			// Token: 0x040003B6 RID: 950
			internal static int StructSize = Marshal.SizeOf(typeof(NativeMethods.GENERIC_MAPPING));

			// Token: 0x040003B7 RID: 951
			internal uint GenericRead;

			// Token: 0x040003B8 RID: 952
			internal uint GenericWrite;

			// Token: 0x040003B9 RID: 953
			internal uint GenericExecute;

			// Token: 0x040003BA RID: 954
			internal uint GenericAll;
		}

		// Token: 0x020000BA RID: 186
		internal enum ComputerNameFormat
		{
			// Token: 0x040003BC RID: 956
			NetBios,
			// Token: 0x040003BD RID: 957
			DnsHostname,
			// Token: 0x040003BE RID: 958
			DnsDomain,
			// Token: 0x040003BF RID: 959
			DnsFullyQualified,
			// Token: 0x040003C0 RID: 960
			PhysicalNetBios,
			// Token: 0x040003C1 RID: 961
			PhysicalDnsHostname,
			// Token: 0x040003C2 RID: 962
			PhysicalDnsDomain,
			// Token: 0x040003C3 RID: 963
			PhysicalDnsFullyQualified
		}

		// Token: 0x020000BB RID: 187
		internal enum DSNameError
		{
			// Token: 0x040003C5 RID: 965
			NoError,
			// Token: 0x040003C6 RID: 966
			ErrorResolving,
			// Token: 0x040003C7 RID: 967
			ErrorNotFound,
			// Token: 0x040003C8 RID: 968
			ErrorNotUnique,
			// Token: 0x040003C9 RID: 969
			ErrorNoMapping,
			// Token: 0x040003CA RID: 970
			ErrorDomainOnly,
			// Token: 0x040003CB RID: 971
			ErrorNoSyntacticalMapping,
			// Token: 0x040003CC RID: 972
			ErrorTrustReferral
		}

		// Token: 0x020000BC RID: 188
		[Flags]
		internal enum DSNameFlags
		{
			// Token: 0x040003CE RID: 974
			NoFlags = 0,
			// Token: 0x040003CF RID: 975
			SyntacticalOnly = 1,
			// Token: 0x040003D0 RID: 976
			EvalAtDC = 2,
			// Token: 0x040003D1 RID: 977
			FlagGCVerify = 4,
			// Token: 0x040003D2 RID: 978
			TrustReferral = 8
		}

		// Token: 0x020000BD RID: 189
		internal enum DSNameFormat
		{
			// Token: 0x040003D4 RID: 980
			UnknownName,
			// Token: 0x040003D5 RID: 981
			FQDN1779Name,
			// Token: 0x040003D6 RID: 982
			NT4AccountName,
			// Token: 0x040003D7 RID: 983
			DisplayName,
			// Token: 0x040003D8 RID: 984
			UniqueIdName = 6,
			// Token: 0x040003D9 RID: 985
			CanonicalName,
			// Token: 0x040003DA RID: 986
			UserPrincipalName,
			// Token: 0x040003DB RID: 987
			CanonicalNameEx,
			// Token: 0x040003DC RID: 988
			ServicePrincipalName,
			// Token: 0x040003DD RID: 989
			SidOrSidHistoryName,
			// Token: 0x040003DE RID: 990
			DnsDomainName
		}

		// Token: 0x020000BE RID: 190
		internal struct DSNameResultItem
		{
			// Token: 0x040003DF RID: 991
			public NativeMethods.DSNameError Status;

			// Token: 0x040003E0 RID: 992
			public string Domain;

			// Token: 0x040003E1 RID: 993
			public string Name;
		}

		// Token: 0x020000BF RID: 191
		internal struct DSNameResult
		{
			// Token: 0x040003E2 RID: 994
			public uint Count;

			// Token: 0x040003E3 RID: 995
			public IntPtr Items;
		}
	}
}
