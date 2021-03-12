﻿using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace System
{
	// Token: 0x020000DE RID: 222
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public static class Environment
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0002BCA0 File Offset: 0x00029EA0
		private static object InternalSyncObject
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				if (Environment.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref Environment.s_InternalSyncObject, value, null);
				}
				return Environment.s_InternalSyncObject;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000E31 RID: 3633
		[__DynamicallyInvokable]
		public static extern int TickCount { [SecuritySafeCritical] [__DynamicallyInvokable] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000E32 RID: 3634
		internal static extern long TickCount64 { [SecuritySafeCritical] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000E33 RID: 3635
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void _Exit(int exitCode);

		// Token: 0x06000E34 RID: 3636 RVA: 0x0002BCCC File Offset: 0x00029ECC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void Exit(int exitCode)
		{
			Environment._Exit(exitCode);
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000E35 RID: 3637
		// (set) Token: 0x06000E36 RID: 3638
		public static extern int ExitCode { [SecuritySafeCritical] [MethodImpl(MethodImplOptions.InternalCall)] get; [SecuritySafeCritical] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000E37 RID: 3639
		[SecurityCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FailFast(string message);

		// Token: 0x06000E38 RID: 3640
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FailFast(string message, uint exitCode);

		// Token: 0x06000E39 RID: 3641
		[SecurityCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FailFast(string message, Exception exception);

		// Token: 0x06000E3A RID: 3642
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void TriggerCodeContractFailure(ContractFailureKind failureKind, string message, string condition, string exceptionAsString);

		// Token: 0x06000E3B RID: 3643
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetIsCLRHosted();

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0002BCD4 File Offset: 0x00029ED4
		internal static bool IsCLRHosted
		{
			[SecuritySafeCritical]
			get
			{
				return Environment.GetIsCLRHosted();
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0002BCDC File Offset: 0x00029EDC
		public static string CommandLine
		{
			[SecuritySafeCritical]
			get
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, "Path").Demand();
				string result = null;
				Environment.GetCommandLine(JitHelpers.GetStringHandleOnStack(ref result));
				return result;
			}
		}

		// Token: 0x06000E3E RID: 3646
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetCommandLine(StringHandleOnStack retString);

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0002BD08 File Offset: 0x00029F08
		// (set) Token: 0x06000E40 RID: 3648 RVA: 0x0002BD0F File Offset: 0x00029F0F
		public static string CurrentDirectory
		{
			get
			{
				return Directory.GetCurrentDirectory();
			}
			set
			{
				Directory.SetCurrentDirectory(value);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0002BD18 File Offset: 0x00029F18
		public static string SystemDirectory
		{
			[SecuritySafeCritical]
			get
			{
				StringBuilder stringBuilder = new StringBuilder(260);
				if (Win32Native.GetSystemDirectory(stringBuilder, 260) == 0)
				{
					__Error.WinIOError();
				}
				string text = stringBuilder.ToString();
				FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, text, false, true);
				return text;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0002BD58 File Offset: 0x00029F58
		internal static string InternalWindowsDirectory
		{
			[SecurityCritical]
			get
			{
				StringBuilder stringBuilder = new StringBuilder(260);
				if (Win32Native.GetWindowsDirectory(stringBuilder, 260) == 0)
				{
					__Error.WinIOError();
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0002BD8C File Offset: 0x00029F8C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string ExpandEnvironmentVariables(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				return name;
			}
			if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
			{
				return name;
			}
			int num = 100;
			StringBuilder stringBuilder = new StringBuilder(num);
			bool flag = CodeAccessSecurityEngine.QuickCheckForAllDemands();
			string[] array = name.Split(new char[]
			{
				'%'
			});
			StringBuilder stringBuilder2 = flag ? null : new StringBuilder();
			bool flag2 = false;
			int j;
			for (int i = 1; i < array.Length - 1; i++)
			{
				if (array[i].Length == 0 || flag2)
				{
					flag2 = false;
				}
				else
				{
					stringBuilder.Length = 0;
					string text = "%" + array[i] + "%";
					j = Win32Native.ExpandEnvironmentStrings(text, stringBuilder, num);
					if (j == 0)
					{
						Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
					}
					while (j > num)
					{
						num = j;
						stringBuilder.Capacity = num;
						stringBuilder.Length = 0;
						j = Win32Native.ExpandEnvironmentStrings(text, stringBuilder, num);
						if (j == 0)
						{
							Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
						}
					}
					if (!flag)
					{
						string a = stringBuilder.ToString();
						flag2 = (a != text);
						if (flag2)
						{
							stringBuilder2.Append(array[i]);
							stringBuilder2.Append(';');
						}
					}
				}
			}
			if (!flag)
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, stringBuilder2.ToString()).Demand();
			}
			stringBuilder.Length = 0;
			j = Win32Native.ExpandEnvironmentStrings(name, stringBuilder, num);
			if (j == 0)
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			while (j > num)
			{
				num = j;
				stringBuilder.Capacity = num;
				stringBuilder.Length = 0;
				j = Win32Native.ExpandEnvironmentStrings(name, stringBuilder, num);
				if (j == 0)
				{
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0002BF20 File Offset: 0x0002A120
		public static string MachineName
		{
			[SecuritySafeCritical]
			get
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, "COMPUTERNAME").Demand();
				StringBuilder stringBuilder = new StringBuilder(256);
				int num = 256;
				if (Win32Native.GetComputerName(stringBuilder, ref num) == 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ComputerName"));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000E45 RID: 3653
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetProcessorCount();

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0002BF6E File Offset: 0x0002A16E
		[__DynamicallyInvokable]
		public static int ProcessorCount
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return Environment.GetProcessorCount();
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0002BF78 File Offset: 0x0002A178
		public static int SystemPageSize
		{
			[SecuritySafeCritical]
			get
			{
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				Win32Native.SYSTEM_INFO system_INFO = default(Win32Native.SYSTEM_INFO);
				Win32Native.GetSystemInfo(ref system_INFO);
				return system_INFO.dwPageSize;
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0002BFA5 File Offset: 0x0002A1A5
		[SecuritySafeCritical]
		public static string[] GetCommandLineArgs()
		{
			new EnvironmentPermission(EnvironmentPermissionAccess.Read, "Path").Demand();
			return Environment.GetCommandLineArgsNative();
		}

		// Token: 0x06000E49 RID: 3657
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetCommandLineArgsNative();

		// Token: 0x06000E4A RID: 3658
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string nativeGetEnvironmentVariable(string variable);

		// Token: 0x06000E4B RID: 3659 RVA: 0x0002BFBC File Offset: 0x0002A1BC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string GetEnvironmentVariable(string variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
			{
				return null;
			}
			new EnvironmentPermission(EnvironmentPermissionAccess.Read, variable).Demand();
			StringBuilder stringBuilder = StringBuilderCache.Acquire(128);
			int i = Win32Native.GetEnvironmentVariable(variable, stringBuilder, stringBuilder.Capacity);
			if (i == 0 && Marshal.GetLastWin32Error() == 203)
			{
				StringBuilderCache.Release(stringBuilder);
				return null;
			}
			while (i > stringBuilder.Capacity)
			{
				stringBuilder.Capacity = i;
				stringBuilder.Length = 0;
				i = Win32Native.GetEnvironmentVariable(variable, stringBuilder, stringBuilder.Capacity);
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0002C050 File Offset: 0x0002A250
		[SecuritySafeCritical]
		public static string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (target == EnvironmentVariableTarget.Process)
			{
				return Environment.GetEnvironmentVariable(variable);
			}
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			if (target == EnvironmentVariableTarget.Machine)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Session Manager\\Environment", false))
				{
					if (registryKey == null)
					{
						return null;
					}
					return registryKey.GetValue(variable) as string;
				}
			}
			if (target == EnvironmentVariableTarget.User)
			{
				using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Environment", false))
				{
					if (registryKey2 == null)
					{
						return null;
					}
					return registryKey2.GetValue(variable) as string;
				}
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
			{
				(int)target
			}));
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0002C12C File Offset: 0x0002A32C
		[SecurityCritical]
		private unsafe static char[] GetEnvironmentCharArray()
		{
			char[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				char* ptr = null;
				try
				{
					ptr = Win32Native.GetEnvironmentStrings();
					if (ptr == null)
					{
						throw new OutOfMemoryException();
					}
					char* ptr2 = ptr;
					while (*ptr2 != '\0' || ptr2[1] != '\0')
					{
						ptr2++;
					}
					int num = (int)((long)(ptr2 - ptr) + 1L);
					array = new char[num];
					try
					{
						fixed (char* ptr3 = array)
						{
							string.wstrcpy(ptr3, ptr, num);
						}
					}
					finally
					{
						char* ptr3 = null;
					}
				}
				finally
				{
					if (ptr != null)
					{
						Win32Native.FreeEnvironmentStrings(ptr);
					}
				}
			}
			return array;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0002C1E0 File Offset: 0x0002A3E0
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static IDictionary GetEnvironmentVariables()
		{
			if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
			{
				return new Hashtable(0);
			}
			bool flag = CodeAccessSecurityEngine.QuickCheckForAllDemands();
			StringBuilder stringBuilder = flag ? null : new StringBuilder();
			bool flag2 = true;
			char[] environmentCharArray = Environment.GetEnvironmentCharArray();
			Hashtable hashtable = new Hashtable(20);
			for (int i = 0; i < environmentCharArray.Length; i++)
			{
				int num = i;
				while (environmentCharArray[i] != '=' && environmentCharArray[i] != '\0')
				{
					i++;
				}
				if (environmentCharArray[i] != '\0')
				{
					if (i - num == 0)
					{
						while (environmentCharArray[i] != '\0')
						{
							i++;
						}
					}
					else
					{
						string text = new string(environmentCharArray, num, i - num);
						i++;
						int num2 = i;
						while (environmentCharArray[i] != '\0')
						{
							i++;
						}
						string value = new string(environmentCharArray, num2, i - num2);
						hashtable[text] = value;
						if (!flag)
						{
							if (flag2)
							{
								flag2 = false;
							}
							else
							{
								stringBuilder.Append(';');
							}
							stringBuilder.Append(text);
						}
					}
				}
			}
			if (!flag)
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, stringBuilder.ToString()).Demand();
			}
			return hashtable;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0002C2E8 File Offset: 0x0002A4E8
		internal static IDictionary GetRegistryKeyNameValuePairs(RegistryKey registryKey)
		{
			Hashtable hashtable = new Hashtable(20);
			if (registryKey != null)
			{
				string[] valueNames = registryKey.GetValueNames();
				foreach (string text in valueNames)
				{
					string value = registryKey.GetValue(text, "").ToString();
					hashtable.Add(text, value);
				}
			}
			return hashtable;
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0002C33C File Offset: 0x0002A53C
		[SecuritySafeCritical]
		public static IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target)
		{
			if (target == EnvironmentVariableTarget.Process)
			{
				return Environment.GetEnvironmentVariables();
			}
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			if (target == EnvironmentVariableTarget.Machine)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Session Manager\\Environment", false))
				{
					return Environment.GetRegistryKeyNameValuePairs(registryKey);
				}
			}
			if (target == EnvironmentVariableTarget.User)
			{
				using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Environment", false))
				{
					return Environment.GetRegistryKeyNameValuePairs(registryKey2);
				}
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
			{
				(int)target
			}));
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0002C3EC File Offset: 0x0002A5EC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void SetEnvironmentVariable(string variable, string value)
		{
			Environment.CheckEnvironmentVariableName(variable);
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			if (string.IsNullOrEmpty(value) || value[0] == '\0')
			{
				value = null;
			}
			else if (value.Length >= 32767)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
			}
			if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
			{
				throw new PlatformNotSupportedException();
			}
			if (Win32Native.SetEnvironmentVariable(variable, value))
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 203)
			{
				return;
			}
			if (lastWin32Error == 206)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
			}
			throw new ArgumentException(Win32Native.GetMessage(lastWin32Error));
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0002C490 File Offset: 0x0002A690
		private static void CheckEnvironmentVariableName(string variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (variable.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), "variable");
			}
			if (variable[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StringFirstCharIsZero"), "variable");
			}
			if (variable.Length >= 32767)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
			}
			if (variable.IndexOf('=') != -1)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalEnvVarName"));
			}
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0002C520 File Offset: 0x0002A720
		[SecuritySafeCritical]
		public static void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)
		{
			if (target == EnvironmentVariableTarget.Process)
			{
				Environment.SetEnvironmentVariable(variable, value);
				return;
			}
			Environment.CheckEnvironmentVariableName(variable);
			if (variable.Length >= 1024)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarName"));
			}
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			if (string.IsNullOrEmpty(value) || value[0] == '\0')
			{
				value = null;
			}
			if (target == EnvironmentVariableTarget.Machine)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Session Manager\\Environment", true))
				{
					if (registryKey != null)
					{
						if (value == null)
						{
							registryKey.DeleteValue(variable, false);
						}
						else
						{
							registryKey.SetValue(variable, value);
						}
					}
					goto IL_FE;
				}
			}
			if (target == EnvironmentVariableTarget.User)
			{
				if (variable.Length >= 255)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
				}
				using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Environment", true))
				{
					if (registryKey2 != null)
					{
						if (value == null)
						{
							registryKey2.DeleteValue(variable, false);
						}
						else
						{
							registryKey2.SetValue(variable, value);
						}
					}
					goto IL_FE;
				}
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
			{
				(int)target
			}));
			IL_FE:
			IntPtr value2 = Win32Native.SendMessageTimeout(new IntPtr(65535), 26, IntPtr.Zero, "Environment", 0U, 1000U, IntPtr.Zero);
			value2 == IntPtr.Zero;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0002C67C File Offset: 0x0002A87C
		[SecuritySafeCritical]
		public static string[] GetLogicalDrives()
		{
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			int logicalDrives = Win32Native.GetLogicalDrives();
			if (logicalDrives == 0)
			{
				__Error.WinIOError();
			}
			uint num = (uint)logicalDrives;
			int num2 = 0;
			while (num != 0U)
			{
				if ((num & 1U) != 0U)
				{
					num2++;
				}
				num >>= 1;
			}
			string[] array = new string[num2];
			char[] array2 = new char[]
			{
				'A',
				':',
				'\\'
			};
			num = (uint)logicalDrives;
			num2 = 0;
			while (num != 0U)
			{
				if ((num & 1U) != 0U)
				{
					array[num2++] = new string(array2);
				}
				num >>= 1;
				char[] array3 = array2;
				int num3 = 0;
				array3[num3] += '\u0001';
			}
			return array;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0002C701 File Offset: 0x0002A901
		[__DynamicallyInvokable]
		public static string NewLine
		{
			[__DynamicallyInvokable]
			get
			{
				return "\r\n";
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0002C708 File Offset: 0x0002A908
		public static Version Version
		{
			get
			{
				return new Version(4, 0, 30319, 42000);
			}
		}

		// Token: 0x06000E57 RID: 3671
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern long GetWorkingSet();

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0002C71B File Offset: 0x0002A91B
		public static long WorkingSet
		{
			[SecuritySafeCritical]
			get
			{
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				return Environment.GetWorkingSet();
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0002C730 File Offset: 0x0002A930
		public static OperatingSystem OSVersion
		{
			[SecuritySafeCritical]
			get
			{
				if (Environment.m_os == null)
				{
					Win32Native.OSVERSIONINFO osversioninfo = new Win32Native.OSVERSIONINFO();
					if (!Environment.GetVersion(osversioninfo))
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GetVersion"));
					}
					Win32Native.OSVERSIONINFOEX osversioninfoex = new Win32Native.OSVERSIONINFOEX();
					if (!Environment.GetVersionEx(osversioninfoex))
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GetVersion"));
					}
					PlatformID platform = PlatformID.Win32NT;
					Version version = new Version(osversioninfo.MajorVersion, osversioninfo.MinorVersion, osversioninfo.BuildNumber, (int)osversioninfoex.ServicePackMajor << 16 | (int)osversioninfoex.ServicePackMinor);
					Environment.m_os = new OperatingSystem(platform, version, osversioninfo.CSDVersion);
				}
				return Environment.m_os;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0002C7C8 File Offset: 0x0002A9C8
		internal static bool IsWindows8OrAbove
		{
			get
			{
				if (!Environment.s_CheckedOSWin8OrAbove)
				{
					OperatingSystem osversion = Environment.OSVersion;
					Environment.s_IsWindows8OrAbove = (osversion.Platform == PlatformID.Win32NT && ((osversion.Version.Major == 6 && osversion.Version.Minor >= 2) || osversion.Version.Major > 6));
					Environment.s_CheckedOSWin8OrAbove = true;
				}
				return Environment.s_IsWindows8OrAbove;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x0002C833 File Offset: 0x0002AA33
		internal static bool IsWinRTSupported
		{
			[SecuritySafeCritical]
			get
			{
				if (!Environment.s_CheckedWinRT)
				{
					Environment.s_WinRTSupported = Environment.WinRTSupported();
					Environment.s_CheckedWinRT = true;
				}
				return Environment.s_WinRTSupported;
			}
		}

		// Token: 0x06000E5C RID: 3676
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool WinRTSupported();

		// Token: 0x06000E5D RID: 3677
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetVersion(Win32Native.OSVERSIONINFO osVer);

		// Token: 0x06000E5E RID: 3678
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetVersionEx(Win32Native.OSVERSIONINFOEX osVer);

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0002C859 File Offset: 0x0002AA59
		[__DynamicallyInvokable]
		public static string StackTrace
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				return Environment.GetStackTrace(null, true);
			}
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0002C870 File Offset: 0x0002AA70
		internal static string GetStackTrace(Exception e, bool needFileInfo)
		{
			StackTrace stackTrace;
			if (e == null)
			{
				stackTrace = new StackTrace(needFileInfo);
			}
			else
			{
				stackTrace = new StackTrace(e, needFileInfo);
			}
			return stackTrace.ToString(System.Diagnostics.StackTrace.TraceFormat.Normal);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0002C898 File Offset: 0x0002AA98
		[SecuritySafeCritical]
		private static void InitResourceHelper()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(Environment.InternalSyncObject, ref flag);
				if (Environment.m_resHelper == null)
				{
					Environment.ResourceHelper resHelper = new Environment.ResourceHelper("mscorlib");
					Thread.MemoryBarrier();
					Environment.m_resHelper = resHelper;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(Environment.InternalSyncObject);
				}
			}
		}

		// Token: 0x06000E62 RID: 3682
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetResourceFromDefault(string key);

		// Token: 0x06000E63 RID: 3683 RVA: 0x0002C8FC File Offset: 0x0002AAFC
		internal static string GetResourceStringLocal(string key)
		{
			if (Environment.m_resHelper == null)
			{
				Environment.InitResourceHelper();
			}
			return Environment.m_resHelper.GetResourceString(key);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0002C919 File Offset: 0x0002AB19
		[SecuritySafeCritical]
		internal static string GetResourceString(string key)
		{
			return Environment.GetResourceFromDefault(key);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0002C924 File Offset: 0x0002AB24
		[SecuritySafeCritical]
		internal static string GetResourceString(string key, params object[] values)
		{
			string resourceString = Environment.GetResourceString(key);
			return string.Format(CultureInfo.CurrentCulture, resourceString, values);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0002C944 File Offset: 0x0002AB44
		internal static string GetRuntimeResourceString(string key)
		{
			return Environment.GetResourceString(key);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0002C94C File Offset: 0x0002AB4C
		internal static string GetRuntimeResourceString(string key, params object[] values)
		{
			return Environment.GetResourceString(key, values);
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0002C955 File Offset: 0x0002AB55
		public static bool Is64BitProcess
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0002C958 File Offset: 0x0002AB58
		public static bool Is64BitOperatingSystem
		{
			[SecuritySafeCritical]
			get
			{
				return true;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000E6A RID: 3690
		[__DynamicallyInvokable]
		public static extern bool HasShutdownStarted { [SecuritySafeCritical] [__DynamicallyInvokable] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000E6B RID: 3691
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetCompatibilityFlag(CompatibilityFlag flag);

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0002C95C File Offset: 0x0002AB5C
		public static string UserName
		{
			[SecuritySafeCritical]
			get
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, "UserName").Demand();
				StringBuilder stringBuilder = new StringBuilder(256);
				int capacity = stringBuilder.Capacity;
				if (Win32Native.GetUserName(stringBuilder, ref capacity))
				{
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0002C9A4 File Offset: 0x0002ABA4
		public static bool UserInteractive
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr processWindowStation = Win32Native.GetProcessWindowStation();
				if (processWindowStation != IntPtr.Zero && Environment.processWinStation != processWindowStation)
				{
					int num = 0;
					Win32Native.USEROBJECTFLAGS userobjectflags = new Win32Native.USEROBJECTFLAGS();
					if (Win32Native.GetUserObjectInformation(processWindowStation, 1, userobjectflags, Marshal.SizeOf<Win32Native.USEROBJECTFLAGS>(userobjectflags), ref num) && (userobjectflags.dwFlags & 1) == 0)
					{
						Environment.isUserNonInteractive = true;
					}
					Environment.processWinStation = processWindowStation;
				}
				return !Environment.isUserNonInteractive;
			}
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0002CA11 File Offset: 0x0002AC11
		[SecuritySafeCritical]
		public static string GetFolderPath(Environment.SpecialFolder folder)
		{
			if (!Enum.IsDefined(typeof(Environment.SpecialFolder), folder))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)folder
				}));
			}
			return Environment.InternalGetFolderPath(folder, Environment.SpecialFolderOption.None, false);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0002CA54 File Offset: 0x0002AC54
		[SecuritySafeCritical]
		public static string GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
		{
			if (!Enum.IsDefined(typeof(Environment.SpecialFolder), folder))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)folder
				}));
			}
			if (!Enum.IsDefined(typeof(Environment.SpecialFolderOption), option))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)option
				}));
			}
			return Environment.InternalGetFolderPath(folder, option, false);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0002CAD5 File Offset: 0x0002ACD5
		[SecurityCritical]
		internal static string UnsafeGetFolderPath(Environment.SpecialFolder folder)
		{
			return Environment.InternalGetFolderPath(folder, Environment.SpecialFolderOption.None, true);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0002CAE0 File Offset: 0x0002ACE0
		[SecurityCritical]
		private static string InternalGetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option, bool suppressSecurityChecks = false)
		{
			if (option == Environment.SpecialFolderOption.Create && !suppressSecurityChecks)
			{
				new FileIOPermission(PermissionState.None)
				{
					AllFiles = FileIOPermissionAccess.Write
				}.Demand();
			}
			StringBuilder stringBuilder = new StringBuilder(260);
			int num = Win32Native.SHGetFolderPath(IntPtr.Zero, (int)(folder | (Environment.SpecialFolder)option), IntPtr.Zero, 0, stringBuilder);
			string text;
			if (num < 0)
			{
				if (num == -2146233031)
				{
					throw new PlatformNotSupportedException();
				}
				text = string.Empty;
			}
			else
			{
				text = stringBuilder.ToString();
			}
			if (!suppressSecurityChecks)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
			}
			return text;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0002CB60 File Offset: 0x0002AD60
		public static string UserDomainName
		{
			[SecuritySafeCritical]
			get
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, "UserDomain").Demand();
				byte[] array = new byte[1024];
				int num = array.Length;
				StringBuilder stringBuilder = new StringBuilder(1024);
				uint capacity = (uint)stringBuilder.Capacity;
				byte userNameEx = Win32Native.GetUserNameEx(2, stringBuilder, ref capacity);
				if (userNameEx == 1)
				{
					string text = stringBuilder.ToString();
					int num2 = text.IndexOf('\\');
					if (num2 != -1)
					{
						return text.Substring(0, num2);
					}
				}
				capacity = (uint)stringBuilder.Capacity;
				int num3;
				if (!Win32Native.LookupAccountName(null, Environment.UserName, array, ref num, stringBuilder, ref capacity, out num3))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new InvalidOperationException(Win32Native.GetMessage(lastWin32Error));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0002CC0B File Offset: 0x0002AE0B
		[__DynamicallyInvokable]
		public static int CurrentManagedThreadId
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return Thread.CurrentThread.ManagedThreadId;
			}
		}

		// Token: 0x0400056F RID: 1391
		private const int MaxEnvVariableValueLength = 32767;

		// Token: 0x04000570 RID: 1392
		private const int MaxSystemEnvVariableLength = 1024;

		// Token: 0x04000571 RID: 1393
		private const int MaxUserEnvVariableLength = 255;

		// Token: 0x04000572 RID: 1394
		private static volatile Environment.ResourceHelper m_resHelper;

		// Token: 0x04000573 RID: 1395
		private const int MaxMachineNameLength = 256;

		// Token: 0x04000574 RID: 1396
		private static object s_InternalSyncObject;

		// Token: 0x04000575 RID: 1397
		private static volatile OperatingSystem m_os;

		// Token: 0x04000576 RID: 1398
		private static volatile bool s_IsWindows8OrAbove;

		// Token: 0x04000577 RID: 1399
		private static volatile bool s_CheckedOSWin8OrAbove;

		// Token: 0x04000578 RID: 1400
		private static volatile bool s_WinRTSupported;

		// Token: 0x04000579 RID: 1401
		private static volatile bool s_CheckedWinRT;

		// Token: 0x0400057A RID: 1402
		private static volatile IntPtr processWinStation;

		// Token: 0x0400057B RID: 1403
		private static volatile bool isUserNonInteractive;

		// Token: 0x02000AB7 RID: 2743
		internal sealed class ResourceHelper
		{
			// Token: 0x060068E3 RID: 26851 RVA: 0x001684FF File Offset: 0x001666FF
			internal ResourceHelper(string name)
			{
				this.m_name = name;
			}

			// Token: 0x060068E4 RID: 26852 RVA: 0x0016850E File Offset: 0x0016670E
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			internal string GetResourceString(string key)
			{
				if (key == null || key.Length == 0)
				{
					return "[Resource lookup failed - null or empty resource name]";
				}
				return this.GetResourceString(key, null);
			}

			// Token: 0x060068E5 RID: 26853 RVA: 0x0016852C File Offset: 0x0016672C
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			internal string GetResourceString(string key, CultureInfo culture)
			{
				if (key == null || key.Length == 0)
				{
					return "[Resource lookup failed - null or empty resource name]";
				}
				Environment.ResourceHelper.GetResourceStringUserData getResourceStringUserData = new Environment.ResourceHelper.GetResourceStringUserData(this, key, culture);
				RuntimeHelpers.TryCode code = new RuntimeHelpers.TryCode(this.GetResourceStringCode);
				RuntimeHelpers.CleanupCode backoutCode = new RuntimeHelpers.CleanupCode(this.GetResourceStringBackoutCode);
				RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(code, backoutCode, getResourceStringUserData);
				return getResourceStringUserData.m_retVal;
			}

			// Token: 0x060068E6 RID: 26854 RVA: 0x0016857C File Offset: 0x0016677C
			[SecuritySafeCritical]
			private void GetResourceStringCode(object userDataIn)
			{
				Environment.ResourceHelper.GetResourceStringUserData getResourceStringUserData = (Environment.ResourceHelper.GetResourceStringUserData)userDataIn;
				Environment.ResourceHelper resourceHelper = getResourceStringUserData.m_resourceHelper;
				string key = getResourceStringUserData.m_key;
				CultureInfo culture = getResourceStringUserData.m_culture;
				Monitor.Enter(resourceHelper, ref getResourceStringUserData.m_lockWasTaken);
				if (resourceHelper.currentlyLoading != null && resourceHelper.currentlyLoading.Count > 0 && resourceHelper.currentlyLoading.Contains(key))
				{
					if (resourceHelper.infinitelyRecursingCount > 0)
					{
						getResourceStringUserData.m_retVal = "[Resource lookup failed - infinite recursion or critical failure detected.]";
						return;
					}
					resourceHelper.infinitelyRecursingCount++;
					string message = "Infinite recursion during resource lookup within mscorlib.  This may be a bug in mscorlib, or potentially in certain extensibility points such as assembly resolve events or CultureInfo names.  Resource name: " + key;
					Assert.Fail("[mscorlib recursive resource lookup bug]", message, -2146232797, System.Diagnostics.StackTrace.TraceFormat.NoResourceLookup);
					Environment.FailFast(message);
				}
				if (resourceHelper.currentlyLoading == null)
				{
					resourceHelper.currentlyLoading = new Stack(4);
				}
				if (!resourceHelper.resourceManagerInited)
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						RuntimeHelpers.RunClassConstructor(typeof(ResourceManager).TypeHandle);
						RuntimeHelpers.RunClassConstructor(typeof(ResourceReader).TypeHandle);
						RuntimeHelpers.RunClassConstructor(typeof(RuntimeResourceSet).TypeHandle);
						RuntimeHelpers.RunClassConstructor(typeof(BinaryReader).TypeHandle);
						resourceHelper.resourceManagerInited = true;
					}
				}
				resourceHelper.currentlyLoading.Push(key);
				if (resourceHelper.SystemResMgr == null)
				{
					resourceHelper.SystemResMgr = new ResourceManager(this.m_name, typeof(object).Assembly);
				}
				string @string = resourceHelper.SystemResMgr.GetString(key, null);
				resourceHelper.currentlyLoading.Pop();
				getResourceStringUserData.m_retVal = @string;
			}

			// Token: 0x060068E7 RID: 26855 RVA: 0x00168700 File Offset: 0x00166900
			[PrePrepareMethod]
			private void GetResourceStringBackoutCode(object userDataIn, bool exceptionThrown)
			{
				Environment.ResourceHelper.GetResourceStringUserData getResourceStringUserData = (Environment.ResourceHelper.GetResourceStringUserData)userDataIn;
				Environment.ResourceHelper resourceHelper = getResourceStringUserData.m_resourceHelper;
				if (exceptionThrown && getResourceStringUserData.m_lockWasTaken)
				{
					resourceHelper.SystemResMgr = null;
					resourceHelper.currentlyLoading = null;
				}
				if (getResourceStringUserData.m_lockWasTaken)
				{
					Monitor.Exit(resourceHelper);
				}
			}

			// Token: 0x0400306F RID: 12399
			private string m_name;

			// Token: 0x04003070 RID: 12400
			private ResourceManager SystemResMgr;

			// Token: 0x04003071 RID: 12401
			private Stack currentlyLoading;

			// Token: 0x04003072 RID: 12402
			internal bool resourceManagerInited;

			// Token: 0x04003073 RID: 12403
			private int infinitelyRecursingCount;

			// Token: 0x02000CC3 RID: 3267
			internal class GetResourceStringUserData
			{
				// Token: 0x060070A3 RID: 28835 RVA: 0x001828FD File Offset: 0x00180AFD
				public GetResourceStringUserData(Environment.ResourceHelper resourceHelper, string key, CultureInfo culture)
				{
					this.m_resourceHelper = resourceHelper;
					this.m_key = key;
					this.m_culture = culture;
				}

				// Token: 0x0400382A RID: 14378
				public Environment.ResourceHelper m_resourceHelper;

				// Token: 0x0400382B RID: 14379
				public string m_key;

				// Token: 0x0400382C RID: 14380
				public CultureInfo m_culture;

				// Token: 0x0400382D RID: 14381
				public string m_retVal;

				// Token: 0x0400382E RID: 14382
				public bool m_lockWasTaken;
			}
		}

		// Token: 0x02000AB8 RID: 2744
		public enum SpecialFolderOption
		{
			// Token: 0x04003075 RID: 12405
			None,
			// Token: 0x04003076 RID: 12406
			Create = 32768,
			// Token: 0x04003077 RID: 12407
			DoNotVerify = 16384
		}

		// Token: 0x02000AB9 RID: 2745
		[ComVisible(true)]
		public enum SpecialFolder
		{
			// Token: 0x04003079 RID: 12409
			ApplicationData = 26,
			// Token: 0x0400307A RID: 12410
			CommonApplicationData = 35,
			// Token: 0x0400307B RID: 12411
			LocalApplicationData = 28,
			// Token: 0x0400307C RID: 12412
			Cookies = 33,
			// Token: 0x0400307D RID: 12413
			Desktop = 0,
			// Token: 0x0400307E RID: 12414
			Favorites = 6,
			// Token: 0x0400307F RID: 12415
			History = 34,
			// Token: 0x04003080 RID: 12416
			InternetCache = 32,
			// Token: 0x04003081 RID: 12417
			Programs = 2,
			// Token: 0x04003082 RID: 12418
			MyComputer = 17,
			// Token: 0x04003083 RID: 12419
			MyMusic = 13,
			// Token: 0x04003084 RID: 12420
			MyPictures = 39,
			// Token: 0x04003085 RID: 12421
			MyVideos = 14,
			// Token: 0x04003086 RID: 12422
			Recent = 8,
			// Token: 0x04003087 RID: 12423
			SendTo,
			// Token: 0x04003088 RID: 12424
			StartMenu = 11,
			// Token: 0x04003089 RID: 12425
			Startup = 7,
			// Token: 0x0400308A RID: 12426
			System = 37,
			// Token: 0x0400308B RID: 12427
			Templates = 21,
			// Token: 0x0400308C RID: 12428
			DesktopDirectory = 16,
			// Token: 0x0400308D RID: 12429
			Personal = 5,
			// Token: 0x0400308E RID: 12430
			MyDocuments = 5,
			// Token: 0x0400308F RID: 12431
			ProgramFiles = 38,
			// Token: 0x04003090 RID: 12432
			CommonProgramFiles = 43,
			// Token: 0x04003091 RID: 12433
			AdminTools = 48,
			// Token: 0x04003092 RID: 12434
			CDBurning = 59,
			// Token: 0x04003093 RID: 12435
			CommonAdminTools = 47,
			// Token: 0x04003094 RID: 12436
			CommonDocuments = 46,
			// Token: 0x04003095 RID: 12437
			CommonMusic = 53,
			// Token: 0x04003096 RID: 12438
			CommonOemLinks = 58,
			// Token: 0x04003097 RID: 12439
			CommonPictures = 54,
			// Token: 0x04003098 RID: 12440
			CommonStartMenu = 22,
			// Token: 0x04003099 RID: 12441
			CommonPrograms,
			// Token: 0x0400309A RID: 12442
			CommonStartup,
			// Token: 0x0400309B RID: 12443
			CommonDesktopDirectory,
			// Token: 0x0400309C RID: 12444
			CommonTemplates = 45,
			// Token: 0x0400309D RID: 12445
			CommonVideos = 55,
			// Token: 0x0400309E RID: 12446
			Fonts = 20,
			// Token: 0x0400309F RID: 12447
			NetworkShortcuts = 19,
			// Token: 0x040030A0 RID: 12448
			PrinterShortcuts = 27,
			// Token: 0x040030A1 RID: 12449
			UserProfile = 40,
			// Token: 0x040030A2 RID: 12450
			CommonProgramFilesX86 = 44,
			// Token: 0x040030A3 RID: 12451
			ProgramFilesX86 = 42,
			// Token: 0x040030A4 RID: 12452
			Resources = 56,
			// Token: 0x040030A5 RID: 12453
			LocalizedResources,
			// Token: 0x040030A6 RID: 12454
			SystemX86 = 41,
			// Token: 0x040030A7 RID: 12455
			Windows = 36
		}
	}
}
