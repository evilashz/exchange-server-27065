using System;
using System.IO;
using System.Security;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000078 RID: 120
	internal static class ConfigFiles
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00008ECE File Offset: 0x000070CE
		private static FileHandler Application
		{
			get
			{
				if (ConfigFiles.application == null)
				{
					ConfigFiles.application = new FileHandler(ConfigFiles.InternalGetConfigPath());
				}
				return ConfigFiles.application;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00008EEC File Offset: 0x000070EC
		internal static ConfigFileHandler Trace
		{
			get
			{
				if (ConfigFiles.trace == null)
				{
					lock (ConfigFiles.locker)
					{
						if (ConfigFiles.trace == null)
						{
							ConfigFiles.trace = new ConfigFileHandler("ExTraceConfiguration", "EnabledTraces.Config");
							ConfigFiles.Application.Changed += ConfigFiles.trace.UpdateConfigFilePath;
						}
					}
				}
				return ConfigFiles.trace;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00008F68 File Offset: 0x00007168
		internal static ConfigFileHandler FaultInjection
		{
			get
			{
				if (ConfigFiles.faultInjection == null)
				{
					lock (ConfigFiles.locker)
					{
						if (ConfigFiles.faultInjection == null)
						{
							ConfigFiles.faultInjection = new ConfigFileHandler("ExFaultInjectionConfiguration", "FaultInjection.Config");
							ConfigFiles.Application.Changed += ConfigFiles.faultInjection.UpdateConfigFilePath;
						}
					}
				}
				return ConfigFiles.faultInjection;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00008FE4 File Offset: 0x000071E4
		internal static ConfigFileHandler InMemory
		{
			get
			{
				if (ConfigFiles.inMemory == null)
				{
					lock (ConfigFiles.locker)
					{
						if (ConfigFiles.inMemory == null)
						{
							ConfigFiles.inMemory = new ConfigFileHandler("ExInMemoryTraceConfiguration", "EnabledInMemoryTraces.Config");
							ConfigFiles.Application.Changed += ConfigFiles.inMemory.UpdateConfigFilePath;
						}
					}
				}
				return ConfigFiles.inMemory;
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009060 File Offset: 0x00007260
		internal static void SetConfigSource(string configSource, string siteName)
		{
			ConfigFiles.Trace.SetConfigSource(configSource, siteName);
			ConfigFiles.InMemory.SetConfigSource(configSource, siteName);
			ConfigFiles.FaultInjection.SetConfigSource(configSource, siteName);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00009086 File Offset: 0x00007286
		internal static string GetDefaultConfigFilePath()
		{
			return ConfigFiles.GetConfigFilePath("EnabledTraces.Config");
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009092 File Offset: 0x00007292
		internal static string GetDefaultInMemoryConfigFilePath()
		{
			return ConfigFiles.GetConfigFilePath("EnabledInMemoryTraces.Config");
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000090A0 File Offset: 0x000072A0
		internal static string GetConfigFilePath(string fileName)
		{
			string text = null;
			try
			{
				text = ConfigFiles.GetSystemDriveDirectory();
			}
			catch (SecurityException)
			{
				text = null;
			}
			if (string.IsNullOrEmpty(text) || text.Length != 2)
			{
				return null;
			}
			text += ConfigFiles.DirectorySeparator;
			if (!Directory.Exists(text))
			{
				return null;
			}
			return Path.Combine(text, fileName);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00009104 File Offset: 0x00007304
		private static char DirectorySeparator
		{
			get
			{
				return Path.DirectorySeparatorChar;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000910B File Offset: 0x0000730B
		private static string GetSystemDriveDirectory()
		{
			return Environment.GetEnvironmentVariable("SystemDrive");
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00009117 File Offset: 0x00007317
		private static string InternalGetConfigPath()
		{
			return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
		}

		// Token: 0x04000261 RID: 609
		private const string TraceConfigurationFileKey = "ExTraceConfiguration";

		// Token: 0x04000262 RID: 610
		private const string InMemoryTraceConfigurationFileKey = "ExInMemoryTraceConfiguration";

		// Token: 0x04000263 RID: 611
		private const string FaultInjectionConfigurationFileKey = "ExFaultInjectionConfiguration";

		// Token: 0x04000264 RID: 612
		internal const string DefaultTraceConfigFileName = "EnabledTraces.Config";

		// Token: 0x04000265 RID: 613
		internal const string DefaultInMemoryTraceConfigFileName = "EnabledInMemoryTraces.Config";

		// Token: 0x04000266 RID: 614
		internal const string DefaultFaultInjectionConfigFileName = "FaultInjection.Config";

		// Token: 0x04000267 RID: 615
		private static object locker = new object();

		// Token: 0x04000268 RID: 616
		private static FileHandler application;

		// Token: 0x04000269 RID: 617
		private static ConfigFileHandler trace;

		// Token: 0x0400026A RID: 618
		private static ConfigFileHandler faultInjection;

		// Token: 0x0400026B RID: 619
		private static ConfigFileHandler inMemory;
	}
}
