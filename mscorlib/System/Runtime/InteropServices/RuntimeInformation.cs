using System;
using System.Reflection;
using System.Security;
using Microsoft.Win32;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200097E RID: 2430
	public static class RuntimeInformation
	{
		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x060061EB RID: 25067 RVA: 0x0014CD58 File Offset: 0x0014AF58
		public static string FrameworkDescription
		{
			get
			{
				if (RuntimeInformation.s_frameworkDescription == null)
				{
					AssemblyFileVersionAttribute assemblyFileVersionAttribute = (AssemblyFileVersionAttribute)typeof(object).GetTypeInfo().Assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute));
					RuntimeInformation.s_frameworkDescription = string.Format("{0} {1}", ".NET Framework", assemblyFileVersionAttribute.Version);
				}
				return RuntimeInformation.s_frameworkDescription;
			}
		}

		// Token: 0x060061EC RID: 25068 RVA: 0x0014CDB4 File Offset: 0x0014AFB4
		public static bool IsOSPlatform(OSPlatform osPlatform)
		{
			return OSPlatform.Windows == osPlatform;
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x060061ED RID: 25069 RVA: 0x0014CDC1 File Offset: 0x0014AFC1
		public static string OSDescription
		{
			[SecuritySafeCritical]
			get
			{
				if (RuntimeInformation.s_osDescription == null)
				{
					RuntimeInformation.s_osDescription = RuntimeInformation.RtlGetVersion();
				}
				return RuntimeInformation.s_osDescription;
			}
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x060061EE RID: 25070 RVA: 0x0014CDDC File Offset: 0x0014AFDC
		public static Architecture OSArchitecture
		{
			[SecuritySafeCritical]
			get
			{
				object obj = RuntimeInformation.s_osLock;
				lock (obj)
				{
					if (RuntimeInformation.s_osArch == null)
					{
						Win32Native.SYSTEM_INFO system_INFO;
						Win32Native.GetNativeSystemInfo(out system_INFO);
						RuntimeInformation.s_osArch = new Architecture?(RuntimeInformation.GetArchitecture(system_INFO.wProcessorArchitecture));
					}
				}
				return RuntimeInformation.s_osArch.Value;
			}
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x060061EF RID: 25071 RVA: 0x0014CE48 File Offset: 0x0014B048
		public static Architecture ProcessArchitecture
		{
			[SecuritySafeCritical]
			get
			{
				object obj = RuntimeInformation.s_processLock;
				lock (obj)
				{
					if (RuntimeInformation.s_processArch == null)
					{
						Win32Native.SYSTEM_INFO system_INFO = default(Win32Native.SYSTEM_INFO);
						Win32Native.GetSystemInfo(ref system_INFO);
						RuntimeInformation.s_processArch = new Architecture?(RuntimeInformation.GetArchitecture(system_INFO.wProcessorArchitecture));
					}
				}
				return RuntimeInformation.s_processArch.Value;
			}
		}

		// Token: 0x060061F0 RID: 25072 RVA: 0x0014CEBC File Offset: 0x0014B0BC
		private static Architecture GetArchitecture(ushort wProcessorArchitecture)
		{
			Architecture result = Architecture.X86;
			if (wProcessorArchitecture <= 5)
			{
				if (wProcessorArchitecture != 0)
				{
					if (wProcessorArchitecture == 5)
					{
						result = Architecture.Arm;
					}
				}
				else
				{
					result = Architecture.X86;
				}
			}
			else if (wProcessorArchitecture != 9)
			{
				if (wProcessorArchitecture == 12)
				{
					result = Architecture.Arm64;
				}
			}
			else
			{
				result = Architecture.X64;
			}
			return result;
		}

		// Token: 0x060061F1 RID: 25073 RVA: 0x0014CEF4 File Offset: 0x0014B0F4
		[SecuritySafeCritical]
		private static string RtlGetVersion()
		{
			Win32Native.RTL_OSVERSIONINFOEX rtl_OSVERSIONINFOEX = default(Win32Native.RTL_OSVERSIONINFOEX);
			rtl_OSVERSIONINFOEX.dwOSVersionInfoSize = (uint)Marshal.SizeOf<Win32Native.RTL_OSVERSIONINFOEX>(rtl_OSVERSIONINFOEX);
			if (Win32Native.RtlGetVersion(out rtl_OSVERSIONINFOEX) == 0)
			{
				return string.Format("{0} {1}.{2}.{3} {4}", new object[]
				{
					"Microsoft Windows",
					rtl_OSVERSIONINFOEX.dwMajorVersion,
					rtl_OSVERSIONINFOEX.dwMinorVersion,
					rtl_OSVERSIONINFOEX.dwBuildNumber,
					rtl_OSVERSIONINFOEX.szCSDVersion
				});
			}
			return "Microsoft Windows";
		}

		// Token: 0x04002BF3 RID: 11251
		private const string FrameworkName = ".NET Framework";

		// Token: 0x04002BF4 RID: 11252
		private static string s_frameworkDescription;

		// Token: 0x04002BF5 RID: 11253
		private static string s_osDescription = null;

		// Token: 0x04002BF6 RID: 11254
		private static object s_osLock = new object();

		// Token: 0x04002BF7 RID: 11255
		private static object s_processLock = new object();

		// Token: 0x04002BF8 RID: 11256
		private static Architecture? s_osArch = null;

		// Token: 0x04002BF9 RID: 11257
		private static Architecture? s_processArch = null;
	}
}
