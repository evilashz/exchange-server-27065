using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200004F RID: 79
	[XmlRoot("configurationContext")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ConfigurationContext
	{
		// Token: 0x0600034B RID: 843 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
		public ConfigurationContext() : this(null, null)
		{
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000CFC2 File Offset: 0x0000B1C2
		public ConfigurationContext(string serverName, string instanceName)
		{
			this.instance = new ConfigurationContext.InstanceContext(serverName, instanceName);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000CFD7 File Offset: 0x0000B1D7
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000CFDF File Offset: 0x0000B1DF
		[XmlElement("instance")]
		public ConfigurationContext.InstanceContext Instance
		{
			get
			{
				return this.instance;
			}
			set
			{
				this.instance = value;
			}
		}

		// Token: 0x040000DB RID: 219
		private ConfigurationContext.InstanceContext instance;

		// Token: 0x02000050 RID: 80
		[ClassAccessLevel(AccessLevel.MSInternal)]
		internal class Setup
		{
			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x0600034F RID: 847 RVA: 0x0000CFE8 File Offset: 0x0000B1E8
			public static bool IsUnpacked
			{
				get
				{
					return ExchangeSetupContext.IsUnpacked;
				}
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000350 RID: 848 RVA: 0x0000CFEF File Offset: 0x0000B1EF
			public static string InstallPath
			{
				get
				{
					return ExchangeSetupContext.InstallPath;
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000351 RID: 849 RVA: 0x0000CFF6 File Offset: 0x0000B1F6
			public static string AssemblyPath
			{
				get
				{
					return ExchangeSetupContext.AssemblyPath;
				}
			}

			// Token: 0x06000352 RID: 850 RVA: 0x0000CFFD File Offset: 0x0000B1FD
			public static void UseAssemblyPathAsInstallPath()
			{
				ExchangeSetupContext.UseAssemblyPathAsInstallPath();
			}

			// Token: 0x06000353 RID: 851 RVA: 0x0000D004 File Offset: 0x0000B204
			public static void ResetInstallPath()
			{
				ExchangeSetupContext.ResetInstallPath();
			}

			// Token: 0x06000354 RID: 852 RVA: 0x0000D00B File Offset: 0x0000B20B
			public static Version GetExecutingVersion()
			{
				return ExchangeSetupContext.GetExecutingVersion();
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000355 RID: 853 RVA: 0x0000D012 File Offset: 0x0000B212
			public static string SetupPath
			{
				get
				{
					return ExchangeSetupContext.SetupPath;
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000356 RID: 854 RVA: 0x0000D019 File Offset: 0x0000B219
			public static string SetupDataPath
			{
				get
				{
					return ExchangeSetupContext.SetupDataPath;
				}
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000357 RID: 855 RVA: 0x0000D020 File Offset: 0x0000B220
			public static string SetupLoggingPath
			{
				get
				{
					return ExchangeSetupContext.SetupLoggingPath;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000358 RID: 856 RVA: 0x0000D027 File Offset: 0x0000B227
			public static string SetupLogFileName
			{
				get
				{
					return ExchangeSetupContext.SetupLogFileName;
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x06000359 RID: 857 RVA: 0x0000D02E File Offset: 0x0000B22E
			public static string SetupLogFileNameForWatson
			{
				get
				{
					return ExchangeSetupContext.SetupLogFileNameForWatson;
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x0600035A RID: 858 RVA: 0x0000D035 File Offset: 0x0000B235
			public static string SetupPerfPath
			{
				get
				{
					return ExchangeSetupContext.SetupPerfPath;
				}
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x0600035B RID: 859 RVA: 0x0000D03C File Offset: 0x0000B23C
			public static string DataPath
			{
				get
				{
					return ExchangeSetupContext.DataPath;
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x0600035C RID: 860 RVA: 0x0000D043 File Offset: 0x0000B243
			public static string ScriptPath
			{
				get
				{
					return ExchangeSetupContext.ScriptPath;
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x0600035D RID: 861 RVA: 0x0000D04A File Offset: 0x0000B24A
			public static string RemoteScriptPath
			{
				get
				{
					return ExchangeSetupContext.RemoteScriptPath;
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x0600035E RID: 862 RVA: 0x0000D051 File Offset: 0x0000B251
			public static string BinPath
			{
				get
				{
					return ExchangeSetupContext.BinPath;
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x0600035F RID: 863 RVA: 0x0000D058 File Offset: 0x0000B258
			public static string DatacenterPath
			{
				get
				{
					return ExchangeSetupContext.DatacenterPath;
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000360 RID: 864 RVA: 0x0000D05F File Offset: 0x0000B25F
			public static string LoggingPath
			{
				get
				{
					return ExchangeSetupContext.LoggingPath;
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06000361 RID: 865 RVA: 0x0000D066 File Offset: 0x0000B266
			public static string ResPath
			{
				get
				{
					return ExchangeSetupContext.ResPath;
				}
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000362 RID: 866 RVA: 0x0000D06D File Offset: 0x0000B26D
			public static string TransportDataPath
			{
				get
				{
					return ExchangeSetupContext.TransportDataPath;
				}
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000363 RID: 867 RVA: 0x0000D074 File Offset: 0x0000B274
			public static string BinPerfProcessorPath
			{
				get
				{
					return ExchangeSetupContext.BinPerfProcessorPath;
				}
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x06000364 RID: 868 RVA: 0x0000D07B File Offset: 0x0000B27B
			public static Version InstalledVersion
			{
				get
				{
					return ExchangeSetupContext.InstalledVersion;
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x06000365 RID: 869 RVA: 0x0000D082 File Offset: 0x0000B282
			public static string PSHostPath
			{
				get
				{
					return ExchangeSetupContext.PSHostPath;
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06000366 RID: 870 RVA: 0x0000D089 File Offset: 0x0000B289
			public static bool IsLonghornServer
			{
				get
				{
					return Environment.OSVersion.Version.Major == 6;
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x06000367 RID: 871 RVA: 0x0000D09D File Offset: 0x0000B29D
			public static string FipsBinPath
			{
				get
				{
					return Path.Combine(ExchangeSetupContext.InstallPath, "FIP-FS\\Bin");
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06000368 RID: 872 RVA: 0x0000D0AE File Offset: 0x0000B2AE
			public static string FipsDataPath
			{
				get
				{
					return Path.Combine(ExchangeSetupContext.InstallPath, "FIP-FS\\Data");
				}
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000369 RID: 873 RVA: 0x0000D0BF File Offset: 0x0000B2BF
			public static string TorusPath
			{
				get
				{
					return "D:\\ManagedTools\\cmdlets";
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x0600036A RID: 874 RVA: 0x0000D0C6 File Offset: 0x0000B2C6
			public static string TorusRemoteScriptPath
			{
				get
				{
					return "D:\\ManagedTools\\RemoteScripts";
				}
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x0600036B RID: 875 RVA: 0x0000D0CD File Offset: 0x0000B2CD
			public static string TorusCmdletAssembly
			{
				get
				{
					return "Microsoft.Office.Datacenter.Torus.Cmdlets.dll";
				}
			}
		}

		// Token: 0x02000051 RID: 81
		[ClassAccessLevel(AccessLevel.MSInternal)]
		internal class InstanceContext
		{
			// Token: 0x0600036D RID: 877 RVA: 0x0000D0DC File Offset: 0x0000B2DC
			public InstanceContext() : this(null, null)
			{
			}

			// Token: 0x0600036E RID: 878 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
			internal InstanceContext(string serverName, string instanceName)
			{
				if (!string.IsNullOrEmpty(serverName))
				{
					this.serverName = serverName;
					this.dataSource = serverName;
				}
				else
				{
					this.serverName = ConfigurationContext.InstanceContext.defaultServerName;
					this.dataSource = ".";
				}
				if (!string.IsNullOrEmpty(instanceName))
				{
					this.instanceName = instanceName;
					this.dataSource = this.dataSource + "\\" + instanceName;
				}
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x0600036F RID: 879 RVA: 0x0000D14F File Offset: 0x0000B34F
			[XmlElement("serverName")]
			public string ServerName
			{
				get
				{
					return this.serverName;
				}
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x06000370 RID: 880 RVA: 0x0000D157 File Offset: 0x0000B357
			[XmlElement("instanceName")]
			public string InstanceName
			{
				get
				{
					return this.instanceName;
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x06000371 RID: 881 RVA: 0x0000D15F File Offset: 0x0000B35F
			[XmlIgnore]
			public string DataSource
			{
				get
				{
					return this.dataSource;
				}
			}

			// Token: 0x040000DC RID: 220
			private string serverName;

			// Token: 0x040000DD RID: 221
			private static string defaultServerName = Dns.GetHostEntry(Environment.MachineName).HostName;

			// Token: 0x040000DE RID: 222
			private string instanceName;

			// Token: 0x040000DF RID: 223
			private string dataSource;
		}
	}
}
