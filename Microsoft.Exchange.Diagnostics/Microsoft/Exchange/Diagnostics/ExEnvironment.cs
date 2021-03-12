using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security;
using Microsoft.Exchange.Diagnostics.Components.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ExEnvironment
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000042AE File Offset: 0x000024AE
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000042BA File Offset: 0x000024BA
		public static bool IsTest
		{
			get
			{
				return ExEnvironment.Instance.IsTest;
			}
			set
			{
				ExEnvironment.Instance.IsTest = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000042C7 File Offset: 0x000024C7
		public static bool IsTestDomain
		{
			get
			{
				return ExEnvironment.Instance.IsTestDomain;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000042D3 File Offset: 0x000024D3
		public static bool IsDogfoodDomain
		{
			get
			{
				return ExEnvironment.Instance.IsDogfoodDomain;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000042DF File Offset: 0x000024DF
		public static bool IsSdfDomain
		{
			get
			{
				return ExEnvironment.Instance.IsSdfDomain;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000042EB File Offset: 0x000024EB
		public static bool TestRegistryKeyExists
		{
			get
			{
				return ExEnvironment.Instance.TestRegistryKeyExists;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000042F7 File Offset: 0x000024F7
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004303 File Offset: 0x00002503
		public static bool IsTestProcess
		{
			get
			{
				return ExEnvironment.Instance.IsTestProcess;
			}
			set
			{
				ExEnvironment.Instance.IsTestProcess = value;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004310 File Offset: 0x00002510
		public static void Reset()
		{
			ExEnvironment.instance = null;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004318 File Offset: 0x00002518
		public static int GetTestRegistryValue(string subPath, string keyName, int defaultValue)
		{
			string keyName2 = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Exchange_Test\\v15" + subPath;
			try
			{
				object value = Registry.GetValue(keyName2, keyName, defaultValue);
				if (value is int)
				{
					return (int)value;
				}
			}
			catch (SecurityException)
			{
				ExTraceGlobals.CommonTracer.TraceError(0L, "Access to the Exchange test registry key was denied.");
			}
			catch (IOException)
			{
				ExTraceGlobals.CommonTracer.TraceError(0L, "IO exception occurred to access to the Exchange test registry.");
			}
			catch (ArgumentException)
			{
				ExTraceGlobals.CommonTracer.TraceError(0L, "Argument Exception occurred to access to the Exchange test registry key.");
			}
			return defaultValue;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000043BC File Offset: 0x000025BC
		private static ExEnvironment.Singleton Instance
		{
			get
			{
				if (ExEnvironment.instance == null)
				{
					ExEnvironment.instance = new ExEnvironment.Singleton();
				}
				return ExEnvironment.instance;
			}
		}

		// Token: 0x040000B6 RID: 182
		private static ExEnvironment.Singleton instance;

		// Token: 0x0200002A RID: 42
		private class Singleton
		{
			// Token: 0x060000D9 RID: 217 RVA: 0x000043D4 File Offset: 0x000025D4
			public Singleton()
			{
				this.isTestDomain = ExEnvironment.Singleton.GetIsTestDomain();
				this.isDogfoodDomain = ExEnvironment.Singleton.GetIsDogfoodDomain();
				this.isSdfDomain = ExEnvironment.Singleton.GetIsSdfDomain();
				this.testRegistryKeyExists = ExEnvironment.Singleton.GetTestRegistryKeyExists();
				this.isTestProcess = ExEnvironment.Singleton.GetIsTestProcess();
				this.isTest = false;
				if (this.isTestDomain && this.testRegistryKeyExists)
				{
					this.isTest = true;
				}
				if (!this.isTest)
				{
					this.isTest = this.isTestProcess;
				}
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000DA RID: 218 RVA: 0x00004450 File Offset: 0x00002650
			// (set) Token: 0x060000DB RID: 219 RVA: 0x00004458 File Offset: 0x00002658
			public bool IsTest
			{
				get
				{
					return this.isTest;
				}
				set
				{
					this.isTest = value;
				}
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000DC RID: 220 RVA: 0x00004461 File Offset: 0x00002661
			public bool IsTestDomain
			{
				get
				{
					return this.isTestDomain;
				}
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060000DD RID: 221 RVA: 0x00004469 File Offset: 0x00002669
			public bool IsDogfoodDomain
			{
				get
				{
					return this.isDogfoodDomain;
				}
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060000DE RID: 222 RVA: 0x00004471 File Offset: 0x00002671
			public bool IsSdfDomain
			{
				get
				{
					return this.isSdfDomain;
				}
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000DF RID: 223 RVA: 0x00004479 File Offset: 0x00002679
			public bool TestRegistryKeyExists
			{
				get
				{
					return this.testRegistryKeyExists;
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004481 File Offset: 0x00002681
			// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004489 File Offset: 0x00002689
			public bool IsTestProcess
			{
				get
				{
					return this.isTestProcess;
				}
				set
				{
					this.isTestProcess = value;
				}
			}

			// Token: 0x060000E2 RID: 226 RVA: 0x00004492 File Offset: 0x00002692
			public static bool GetIsTestDomain()
			{
				return ExEnvironment.Singleton.GetIsInDomain(".EXTEST.MICROSOFT.COM");
			}

			// Token: 0x060000E3 RID: 227 RVA: 0x0000449E File Offset: 0x0000269E
			public static bool GetIsDogfoodDomain()
			{
				return ExEnvironment.Singleton.GetIsInDomain(".EXCHANGE.CORP.MICROSOFT.COM");
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x000044AA File Offset: 0x000026AA
			public static bool GetIsSdfDomain()
			{
				return ExEnvironment.Singleton.GetIsInDomain(".SDF.EXCHANGELABS.COM");
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x000044B8 File Offset: 0x000026B8
			public static bool GetTestRegistryKeyExists()
			{
				bool result = false;
				try
				{
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Exchange_Test"))
					{
						result = (registryKey != null);
					}
				}
				catch (SecurityException)
				{
					ExTraceGlobals.CommonTracer.TraceError(0L, "Access to the Exchange test registry key was denied.");
				}
				return result;
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x00004520 File Offset: 0x00002720
			public static bool GetIsTestProcess()
			{
				bool result = false;
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					if (string.Equals(currentProcess.ProcessName, "PERSEUSHARNESSSERVICE", StringComparison.OrdinalIgnoreCase) || string.Equals(currentProcess.ProcessName, "PERSEUSHARNESSRUNTIME", StringComparison.OrdinalIgnoreCase) || string.Equals(currentProcess.ProcessName, "PERSEUSSTUDIO", StringComparison.OrdinalIgnoreCase) || string.Equals(currentProcess.ProcessName, "CPERSEUS", StringComparison.OrdinalIgnoreCase) || string.Equals(currentProcess.ProcessName, "contactsimporter", StringComparison.OrdinalIgnoreCase) || string.Equals(currentProcess.ProcessName, "Internal.Exchange.Inference.InferenceTool", StringComparison.OrdinalIgnoreCase) || string.Equals(currentProcess.ProcessName, "Internal.Exchange.Shared.Resource.Service", StringComparison.OrdinalIgnoreCase) || string.Equals(currentProcess.ProcessName, "topoagent", StringComparison.OrdinalIgnoreCase) || string.Equals(currentProcess.ProcessName, "xsoexplorer", StringComparison.OrdinalIgnoreCase) || string.Equals(currentProcess.ProcessName, "UNITP", StringComparison.OrdinalIgnoreCase))
					{
						result = true;
					}
				}
				return result;
			}

			// Token: 0x060000E7 RID: 231 RVA: 0x0000461C File Offset: 0x0000281C
			private static bool GetIsInDomain(string domainSuffix)
			{
				bool result = false;
				string hostName = Dns.GetHostName();
				if (!string.IsNullOrEmpty(hostName))
				{
					IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
					if (hostEntry != null && !string.IsNullOrEmpty(hostEntry.HostName))
					{
						result = hostEntry.HostName.EndsWith(domainSuffix, StringComparison.OrdinalIgnoreCase);
					}
				}
				return result;
			}

			// Token: 0x040000B7 RID: 183
			private readonly bool isTestDomain;

			// Token: 0x040000B8 RID: 184
			private readonly bool isDogfoodDomain;

			// Token: 0x040000B9 RID: 185
			private readonly bool isSdfDomain;

			// Token: 0x040000BA RID: 186
			private readonly bool testRegistryKeyExists;

			// Token: 0x040000BB RID: 187
			private bool isTestProcess;

			// Token: 0x040000BC RID: 188
			private bool isTest;
		}
	}
}
