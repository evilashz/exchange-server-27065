using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000ACD RID: 2765
	internal static class Privileges
	{
		// Token: 0x06003B38 RID: 15160 RVA: 0x00098828 File Offset: 0x00096A28
		public static int RemoveAllExcept(string[] privilegesToKeep)
		{
			int result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				result = Privileges.RemoveAllExcept(currentProcess, privilegesToKeep, string.Empty);
			}
			return result;
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x00098868 File Offset: 0x00096A68
		public static int RemoveAllExcept(string[] privilegesToKeep, string processLabel)
		{
			int result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				result = Privileges.RemoveAllExcept(currentProcess, privilegesToKeep, processLabel);
			}
			return result;
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x000988A4 File Offset: 0x00096AA4
		public static int RemoveAllExcept(Process managedProcessHandle, string[] privilegesToKeep)
		{
			return Privileges.RemoveAllExcept(managedProcessHandle, privilegesToKeep, string.Empty);
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x000988B4 File Offset: 0x00096AB4
		public static int RemoveAllExcept(Process managedProcessHandle, string[] privilegesToKeep, string processLabel)
		{
			int num = -1;
			SafeUserTokenHandle safeUserTokenHandle = null;
			SafeHGlobalHandle safeHGlobalHandle = null;
			try
			{
				safeUserTokenHandle = new SafeUserTokenHandle(IntPtr.Zero);
				if (NativeMethods.OpenProcessToken(managedProcessHandle.Handle, 40U, ref safeUserTokenHandle))
				{
					num = Privileges.GetTokenInformation(safeUserTokenHandle, NativeMethods.TOKEN_INFORMATION_CLASS.TokenPrivileges, ref safeHGlobalHandle);
					if (num == 0 && safeHGlobalHandle != null && !safeHGlobalHandle.IsInvalid)
					{
						num = Privileges.RemoveUnnecessaryPrivileges(ref safeUserTokenHandle, ref safeHGlobalHandle, privilegesToKeep);
					}
				}
				else
				{
					num = Marshal.GetLastWin32Error();
				}
			}
			finally
			{
				if (safeHGlobalHandle != null)
				{
					safeHGlobalHandle.Close();
				}
				if (safeUserTokenHandle != null)
				{
					safeUserTokenHandle.Close();
				}
				if (num != 0)
				{
					ExEventLog exEventLog = new ExEventLog(Privileges.componentGuid, "MSExchange Common");
					object[] array = new object[4];
					array[0] = managedProcessHandle.MainModule.FileName;
					array[1] = managedProcessHandle.Id;
					array[2] = processLabel;
					long num2 = (num < 0) ? ((long)num) : (((long)num & 65535L) | (long)((ulong)-2147024896));
					array[3] = string.Format(CultureInfo.InvariantCulture, "0x{0:X8}", new object[]
					{
						num2
					});
					exEventLog.LogEvent(CommonEventLogConstants.Tuple_PrivilegeRemovalFailure, null, array);
				}
			}
			return num;
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x000989CC File Offset: 0x00096BCC
		private static int GetTokenInformation(SafeUserTokenHandle tokenHandle, NativeMethods.TOKEN_INFORMATION_CLASS tokenInformationClass, ref SafeHGlobalHandle processTokenInfoHandle)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			SafeHGlobalHandle safeHGlobalHandle = new SafeHGlobalHandle(IntPtr.Zero);
			if (!NativeMethods.GetTokenInformation(tokenHandle, tokenInformationClass, safeHGlobalHandle, num2, ref num3))
			{
				num = Marshal.GetLastWin32Error();
				if (num == 122)
				{
					num = 0;
					num2 = num3;
					processTokenInfoHandle = NativeMethods.AllocHGlobal(num2);
					if (!NativeMethods.GetTokenInformation(tokenHandle, tokenInformationClass, processTokenInfoHandle, num2, ref num3))
					{
						num = Marshal.GetLastWin32Error();
						if (processTokenInfoHandle != null)
						{
							processTokenInfoHandle.Close();
							processTokenInfoHandle = null;
						}
					}
				}
			}
			safeHGlobalHandle.Close();
			return num;
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x00098A38 File Offset: 0x00096C38
		private unsafe static int RemoveUnnecessaryPrivileges(ref SafeUserTokenHandle tokenHandle, ref SafeHGlobalHandle processTokenInfoHandle, string[] privilegesToKeep)
		{
			int num = 0;
			Hashtable hashtable = new Hashtable(privilegesToKeep.Length);
			SafeHGlobalHandle safeHGlobalHandle = null;
			NativeMethods.LUID luid;
			luid.LowPart = 0U;
			luid.HighPart = 0U;
			foreach (string lpName in privilegesToKeep)
			{
				if (!NativeMethods.LookupPrivilegeValue(null, lpName, ref luid))
				{
					num = Marshal.GetLastWin32Error();
					break;
				}
				hashtable.Add(luid, null);
			}
			if (num == 0)
			{
				try
				{
					uint* ptr = (uint*)processTokenInfoHandle.DangerousGetHandle().ToPointer();
					NativeMethods.LUID_AND_ATTRIBUTES* ptr2 = (NativeMethods.LUID_AND_ATTRIBUTES*)(ptr + 1);
					safeHGlobalHandle = NativeMethods.AllocHGlobal(Marshal.SizeOf(typeof(uint)) + (int)((long)Marshal.SizeOf(typeof(NativeMethods.LUID_AND_ATTRIBUTES)) * (long)((ulong)(*ptr))));
					uint* ptr3 = (uint*)safeHGlobalHandle.DangerousGetHandle().ToPointer();
					*ptr3 = 0U;
					NativeMethods.LUID_AND_ATTRIBUTES* ptr4 = (NativeMethods.LUID_AND_ATTRIBUTES*)(ptr3 + 1);
					int num2 = 0;
					while ((long)num2 < (long)((ulong)(*ptr)))
					{
						if (!hashtable.Contains(ptr2->Luid))
						{
							*ptr3 += 1U;
							ptr4->Luid = ptr2->Luid;
							ptr4->Attributes = 4U;
							ptr4++;
						}
						ptr2++;
						num2++;
					}
					if (!NativeMethods.AdjustTokenPrivileges(tokenHandle, false, safeHGlobalHandle, 0U, IntPtr.Zero, IntPtr.Zero))
					{
						num = Marshal.GetLastWin32Error();
					}
				}
				finally
				{
					if (safeHGlobalHandle != null)
					{
						safeHGlobalHandle.Close();
					}
				}
			}
			return num;
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x00098BA0 File Offset: 0x00096DA0
		private static RawSecurityDescriptor GetTokenSecurityDescriptor(SafeHandle tokenHandle)
		{
			uint num = 0U;
			if (NativeMethods.GetKernelObjectSecurity(tokenHandle, SecurityInfos.DiscretionaryAcl, IntPtr.Zero, 0U, out num) || Marshal.GetLastWin32Error() != 122)
			{
				return null;
			}
			IntPtr intPtr = Marshal.AllocHGlobal((int)num);
			try
			{
				if (NativeMethods.GetKernelObjectSecurity(tokenHandle, SecurityInfos.DiscretionaryAcl, intPtr, num, out num))
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, (int)num);
					return new RawSecurityDescriptor(array, 0);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return null;
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x00098C1C File Offset: 0x00096E1C
		private static bool SetTokenSecurityDescriptor(SafeHandle tokenHandle, RawSecurityDescriptor sd)
		{
			int binaryLength = sd.BinaryLength;
			byte[] array = new byte[binaryLength];
			sd.GetBinaryForm(array, 0);
			IntPtr intPtr = Marshal.AllocHGlobal(binaryLength);
			bool result;
			try
			{
				Marshal.Copy(array, 0, intPtr, binaryLength);
				result = NativeMethods.SetKernelObjectSecurity(tokenHandle, SecurityInfos.DiscretionaryAcl, intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x00098C74 File Offset: 0x00096E74
		public static bool UpdateProcessDacl(Process process, Privileges.UpdateDacl customUpdate)
		{
			SafeUserTokenHandle safeUserTokenHandle = new SafeUserTokenHandle(IntPtr.Zero);
			if (!NativeMethods.OpenProcessToken(process.Handle, 393216U, ref safeUserTokenHandle))
			{
				return false;
			}
			bool result;
			try
			{
				RawSecurityDescriptor tokenSecurityDescriptor = Privileges.GetTokenSecurityDescriptor(safeUserTokenHandle);
				if (tokenSecurityDescriptor == null)
				{
					result = false;
				}
				else
				{
					Privileges.UpdateDaclFromSD(tokenSecurityDescriptor, customUpdate);
					result = Privileges.SetTokenSecurityDescriptor(safeUserTokenHandle, tokenSecurityDescriptor);
				}
			}
			finally
			{
				safeUserTokenHandle.Close();
			}
			return result;
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x00098CDC File Offset: 0x00096EDC
		private static void UpdateDaclFromSD(RawSecurityDescriptor sd, Privileges.UpdateDacl customUpdate)
		{
			DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(false, false, sd.DiscretionaryAcl);
			customUpdate(discretionaryAcl);
			byte[] binaryForm = new byte[discretionaryAcl.BinaryLength];
			discretionaryAcl.GetBinaryForm(binaryForm, 0);
			sd.DiscretionaryAcl = new RawAcl(binaryForm, 0);
		}

		// Token: 0x04003401 RID: 13313
		public const string SeAssignPrimaryTokenPrivilege = "SeAssignPrimaryTokenPrivilege";

		// Token: 0x04003402 RID: 13314
		public const string SeAuditPrivilege = "SeAuditPrivilege";

		// Token: 0x04003403 RID: 13315
		public const string SeBackupPrivilege = "SeBackupPrivilege";

		// Token: 0x04003404 RID: 13316
		public const string SeChangeNotifyPrivilege = "SeChangeNotifyPrivilege";

		// Token: 0x04003405 RID: 13317
		public const string SeCreateGlobalPrivilege = "SeCreateGlobalPrivilege";

		// Token: 0x04003406 RID: 13318
		public const string SeCreatePagefilePrivilege = "SeCreatePagefilePrivilege";

		// Token: 0x04003407 RID: 13319
		public const string SeCreatePermanentPrivilege = "SeCreatePermanentPrivilege";

		// Token: 0x04003408 RID: 13320
		public const string SeCreateSymbolicLinkPrivilege = "SeCreateSymbolicLinkPrivilege";

		// Token: 0x04003409 RID: 13321
		public const string SeCreateTokenPrivilege = "SeCreateTokenPrivilege";

		// Token: 0x0400340A RID: 13322
		public const string SeDebugPrivilege = "SeDebugPrivilege";

		// Token: 0x0400340B RID: 13323
		public const string SeEnableDelegationPrivilege = "SeEnableDelegationPrivilege";

		// Token: 0x0400340C RID: 13324
		public const string SeImpersonatePrivilege = "SeImpersonatePrivilege";

		// Token: 0x0400340D RID: 13325
		public const string SeIncreaseBasePriorityPrivilege = "SeIncreaseBasePriorityPrivilege";

		// Token: 0x0400340E RID: 13326
		public const string SeIncreaseQuotaPrivilege = "SeIncreaseQuotaPrivilege";

		// Token: 0x0400340F RID: 13327
		public const string SeLoadDriverPrivilege = "SeLoadDriverPrivilege";

		// Token: 0x04003410 RID: 13328
		public const string SeLockMemoryPrivilege = "SeLockMemoryPrivilege";

		// Token: 0x04003411 RID: 13329
		public const string SeMachineAccountPrivilege = "SeMachineAccountPrivilege";

		// Token: 0x04003412 RID: 13330
		public const string SeManageVolumePrivilege = "SeManageVolumePrivilege";

		// Token: 0x04003413 RID: 13331
		public const string SeProfileSingleProcessPrivilege = "SeProfileSingleProcessPrivilege";

		// Token: 0x04003414 RID: 13332
		public const string SeRemoteShutdownPrivilege = "SeRemoteShutdownPrivilege";

		// Token: 0x04003415 RID: 13333
		public const string SeRestorePrivilege = "SeRestorePrivilege";

		// Token: 0x04003416 RID: 13334
		public const string SeSecurityPrivilege = "SeSecurityPrivilege";

		// Token: 0x04003417 RID: 13335
		public const string SeShutdownPrivilege = "SeShutdownPrivilege";

		// Token: 0x04003418 RID: 13336
		public const string SeSyncAgentPrivilege = "SeSyncAgentPrivilege";

		// Token: 0x04003419 RID: 13337
		public const string SeSystemEnvironmentPrivilege = "SeSystemEnvironmentPrivilege";

		// Token: 0x0400341A RID: 13338
		public const string SeSystemProfilePrivilege = "SeSystemProfilePrivilege";

		// Token: 0x0400341B RID: 13339
		public const string SeSystemtimePrivilege = "SeSystemtimePrivilege";

		// Token: 0x0400341C RID: 13340
		public const string SeTakeOwnershipPrivilege = "SeTakeOwnershipPrivilege";

		// Token: 0x0400341D RID: 13341
		public const string SeTcbPrivilege = "SeTcbPrivilege";

		// Token: 0x0400341E RID: 13342
		public const string SeUndockPrivilege = "SeUndockPrivilege";

		// Token: 0x0400341F RID: 13343
		public const string SeUnsolicitedInputPrivilege = "SeUnsolicitedInputPrivilege";

		// Token: 0x04003420 RID: 13344
		private static Guid componentGuid = new Guid("{995740FC-0735-4d83-8DDA-5CB1D427560D}");

		// Token: 0x02000ACE RID: 2766
		// (Invoke) Token: 0x06003B44 RID: 15172
		public delegate void UpdateDacl(DiscretionaryAcl dacl);
	}
}
