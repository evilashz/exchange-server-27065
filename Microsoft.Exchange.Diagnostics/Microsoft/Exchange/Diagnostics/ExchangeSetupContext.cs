using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200012B RID: 299
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExchangeSetupContext
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00021EB8 File Offset: 0x000200B8
		public static bool IsUnpacked
		{
			get
			{
				string value = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiInstallPath", null);
				return !string.IsNullOrEmpty(value);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x00021EE4 File Offset: 0x000200E4
		public static string InstallPath
		{
			get
			{
				if (ExchangeSetupContext.installPath == null)
				{
					if (!ExchangeSetupContext.IsUnpacked)
					{
						throw new SetupVersionInformationCorruptException("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup");
					}
					ExchangeSetupContext.installPath = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiInstallPath", null);
				}
				return ExchangeSetupContext.installPath;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00021F20 File Offset: 0x00020120
		public static string AssemblyPath
		{
			get
			{
				if (ExchangeSetupContext.assemblyPath == null)
				{
					string location = Assembly.GetExecutingAssembly().Location;
					ExchangeSetupContext.assemblyPath = Path.GetDirectoryName(location);
				}
				return ExchangeSetupContext.assemblyPath;
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00021F4F File Offset: 0x0002014F
		public static void UseAssemblyPathAsInstallPath()
		{
			if (!ExchangeSetupContext.IsUnpacked || !ExchangeSetupContext.assemblyPath.Equals(ExchangeSetupContext.BinPath, StringComparison.OrdinalIgnoreCase))
			{
				ExchangeSetupContext.installPath = ExchangeSetupContext.AssemblyPath;
				ExchangeSetupContext.installedVersion = ExchangeSetupContext.GetExecutingVersion();
				ExchangeSetupContext.useAssemblyPathAsBinPath = true;
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00021F84 File Offset: 0x00020184
		public static void ResetInstallPath()
		{
			ExchangeSetupContext.installPath = null;
			ExchangeSetupContext.installedVersion = null;
			ExchangeSetupContext.useAssemblyPathAsBinPath = false;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00021F98 File Offset: 0x00020198
		public static Version GetExecutingVersion()
		{
			if (ExchangeSetupContext.executingVersion == null)
			{
				string text = Path.Combine(ExchangeSetupContext.AssemblyPath, "ExSetup.exe");
				try
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(text);
					if (string.IsNullOrEmpty(versionInfo.FileVersion))
					{
						throw new FileVersionNotFoundException(DiagnosticsResources.ExceptionFileVersionNotFound(text));
					}
					ExchangeSetupContext.executingVersion = new Version(versionInfo.FileVersion);
				}
				catch (FileNotFoundException)
				{
					throw new FileVersionNotFoundException(DiagnosticsResources.ExceptionWantedVersionButFileNotFound(text));
				}
			}
			return ExchangeSetupContext.executingVersion;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00022018 File Offset: 0x00020218
		public static string SetupPath
		{
			get
			{
				return Path.Combine(ExchangeSetupContext.InstallPath, "Setup");
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00022029 File Offset: 0x00020229
		public static string SetupDataPath
		{
			get
			{
				return Path.Combine(ExchangeSetupContext.SetupPath, "Data");
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0002203A File Offset: 0x0002023A
		public static string SetupLoggingPath
		{
			get
			{
				return ExchangeSetupContext.setupLoggingPath;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00022041 File Offset: 0x00020241
		public static string SetupLogFileName
		{
			get
			{
				return ExchangeSetupContext.setupLogFileName;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00022048 File Offset: 0x00020248
		public static string SetupLogFileNameForWatson
		{
			get
			{
				return ExchangeSetupContext.setupLogFileNameForWatson;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0002204F File Offset: 0x0002024F
		public static string SetupPerfPath
		{
			get
			{
				return Path.Combine(ExchangeSetupContext.SetupPath, "Perf");
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00022060 File Offset: 0x00020260
		public static string DataPath
		{
			get
			{
				return Path.Combine(ExchangeSetupContext.InstallPath, "Data");
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00022071 File Offset: 0x00020271
		public static string ScriptPath
		{
			get
			{
				return Path.Combine(ExchangeSetupContext.InstallPath, "Scripts");
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00022082 File Offset: 0x00020282
		public static string RemoteScriptPath
		{
			get
			{
				return Path.Combine(ExchangeSetupContext.InstallPath, "RemoteScripts");
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00022093 File Offset: 0x00020293
		public static string BinPath
		{
			get
			{
				if (ExchangeSetupContext.useAssemblyPathAsBinPath)
				{
					return ExchangeSetupContext.InstallPath;
				}
				return Path.Combine(ExchangeSetupContext.InstallPath, "Bin");
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x000220B1 File Offset: 0x000202B1
		public static string DatacenterPath
		{
			get
			{
				return Path.Combine(ExchangeSetupContext.InstallPath, "Datacenter");
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x000220C2 File Offset: 0x000202C2
		public static string LoggingPath
		{
			get
			{
				return Path.Combine(ExchangeSetupContext.InstallPath, "Logging");
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x000220D3 File Offset: 0x000202D3
		public static string ResPath
		{
			get
			{
				return Path.Combine(ExchangeSetupContext.BinPath, "Res");
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x000220E4 File Offset: 0x000202E4
		public static string TransportDataPath
		{
			get
			{
				return Path.Combine(ExchangeSetupContext.InstallPath, "TransportRoles\\Data");
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x000220F8 File Offset: 0x000202F8
		public static string BinPerfProcessorPath
		{
			get
			{
				string environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
				return Path.Combine(Path.Combine(ExchangeSetupContext.BinPath, "Perf"), environmentVariable);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00022128 File Offset: 0x00020328
		public static Version InstalledVersion
		{
			get
			{
				if (null == ExchangeSetupContext.installedVersion)
				{
					try
					{
						using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\ExchangeServer\\v15\\Setup"))
						{
							int major = (int)registryKey.GetValue("MsiProductMajor");
							int minor = (int)registryKey.GetValue("MsiProductMinor");
							int build = (int)registryKey.GetValue("MsiBuildMajor");
							int revision = (int)registryKey.GetValue("MsiBuildMinor");
							ExchangeSetupContext.installedVersion = new Version(major, minor, build, revision);
						}
					}
					catch (Exception innerException)
					{
						throw new SetupVersionInformationCorruptException("Software\\Microsoft\\ExchangeServer\\v15\\Setup", innerException);
					}
				}
				return ExchangeSetupContext.installedVersion;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x000221E8 File Offset: 0x000203E8
		public static string PSHostPath
		{
			get
			{
				if (ExchangeSetupContext.mshHostPath == null)
				{
					try
					{
						ExchangeSetupContext.mshHostPath = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\PowerShell\\1\\PowerShellEngine", "ApplicationBase", null);
					}
					catch (Exception innerException)
					{
						throw new SetupVersionInformationCorruptException("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\PowerShell\\1\\PowerShellEngine", innerException);
					}
					if (ExchangeSetupContext.mshHostPath == null)
					{
						throw new SetupVersionInformationCorruptException("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\PowerShell\\1\\PowerShellEngine");
					}
				}
				return ExchangeSetupContext.mshHostPath;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0002224C File Offset: 0x0002044C
		public static bool IsLonghornServer
		{
			get
			{
				return Environment.OSVersion.Version.Major == 6;
			}
		}

		// Token: 0x040005BE RID: 1470
		private const string SetupInstallKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";

		// Token: 0x040005BF RID: 1471
		private const string InstallPathName = "MsiInstallPath";

		// Token: 0x040005C0 RID: 1472
		private const string MshEngineInstallKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\PowerShell\\1\\PowerShellEngine";

		// Token: 0x040005C1 RID: 1473
		private const string MshInstallPathName = "ApplicationBase";

		// Token: 0x040005C2 RID: 1474
		private const string OfficialVersionFile = "ExSetup.exe";

		// Token: 0x040005C3 RID: 1475
		private static string setupLoggingPath = Environment.ExpandEnvironmentVariables("%systemdrive%\\ExchangeSetupLogs");

		// Token: 0x040005C4 RID: 1476
		private static string setupLogFileName = "ExchangeSetup.log";

		// Token: 0x040005C5 RID: 1477
		private static string setupLogFileNameForWatson = "ExchangeSetupWatson.log";

		// Token: 0x040005C6 RID: 1478
		private static string mshHostPath;

		// Token: 0x040005C7 RID: 1479
		private static Version executingVersion;

		// Token: 0x040005C8 RID: 1480
		private static string assemblyPath;

		// Token: 0x040005C9 RID: 1481
		private static bool useAssemblyPathAsBinPath = false;

		// Token: 0x040005CA RID: 1482
		private static string installPath;

		// Token: 0x040005CB RID: 1483
		private static Version installedVersion;
	}
}
