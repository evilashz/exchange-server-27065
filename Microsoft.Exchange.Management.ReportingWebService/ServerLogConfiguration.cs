using System;
using System.IO;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000006 RID: 6
	internal sealed class ServerLogConfiguration : ILogConfiguration
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000023C4 File Offset: 0x000005C4
		public ServerLogConfiguration()
		{
			this.IsLoggingEnabled = AppConfigLoader.GetConfigBoolValue("RWSIsServerLoggingEnabled", true);
			this.LogPath = AppConfigLoader.GetConfigStringValue("RWSServerLogPath", ServerLogConfiguration.DefaultLogPath);
			this.MaxLogAge = AppConfigLoader.GetConfigTimeSpanValue("RWSServerMaxLogAge", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(30.0));
			this.MaxLogDirectorySizeInBytes = (long)ServerLogConfiguration.GetConfigByteQuantifiedSizeValue("RWSServerMaxLogDirectorySize", ByteQuantifiedSize.FromGB(1UL)).ToBytes();
			this.MaxLogFileSizeInBytes = (long)ServerLogConfiguration.GetConfigByteQuantifiedSizeValue("RWSServerMaxLogFileSize", ByteQuantifiedSize.FromMB(10UL)).ToBytes();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002464 File Offset: 0x00000664
		public static string DefaultLogPath
		{
			get
			{
				if (ServerLogConfiguration.defaultLogPath == null)
				{
					ServerLogConfiguration.defaultLogPath = Path.Combine(ConfigurationContext.Setup.InstallPath, "Logging\\RWSServer");
				}
				return ServerLogConfiguration.defaultLogPath;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002486 File Offset: 0x00000686
		public bool IsActivityEventHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002489 File Offset: 0x00000689
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002491 File Offset: 0x00000691
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000249A File Offset: 0x0000069A
		public string LogComponent
		{
			get
			{
				return "RWSServerLog";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000024A1 File Offset: 0x000006A1
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000024A9 File Offset: 0x000006A9
		public string LogPath { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024B2 File Offset: 0x000006B2
		public string LogPrefix
		{
			get
			{
				return ServerLogConfiguration.LogPrefixValue;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000024B9 File Offset: 0x000006B9
		public string LogType
		{
			get
			{
				return "RWS Server Log";
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024C0 File Offset: 0x000006C0
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000024C8 File Offset: 0x000006C8
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024D1 File Offset: 0x000006D1
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000024D9 File Offset: 0x000006D9
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000024E2 File Offset: 0x000006E2
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000024EA File Offset: 0x000006EA
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x0600001F RID: 31 RVA: 0x000024F4 File Offset: 0x000006F4
		private static ByteQuantifiedSize GetConfigByteQuantifiedSizeValue(string configName, ByteQuantifiedSize defaultValue)
		{
			string expression = null;
			ByteQuantifiedSize result;
			if (AppConfigLoader.TryGetConfigRawValue(configName, out expression) && ByteQuantifiedSize.TryParse(expression, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x0400001F RID: 31
		private const string LogTypeValue = "RWS Server Log";

		// Token: 0x04000020 RID: 32
		private const string LogComponentValue = "RWSServerLog";

		// Token: 0x04000021 RID: 33
		private const string DefaultRelativeFilePath = "Logging\\RWSServer";

		// Token: 0x04000022 RID: 34
		public static readonly string LogPrefixValue = "RWSServer";

		// Token: 0x04000023 RID: 35
		private static string defaultLogPath;
	}
}
