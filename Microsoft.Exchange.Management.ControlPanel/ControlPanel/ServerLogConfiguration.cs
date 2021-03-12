using System;
using System.IO;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000218 RID: 536
	internal sealed class ServerLogConfiguration : ILogConfiguration
	{
		// Token: 0x06002731 RID: 10033 RVA: 0x0007ACC8 File Offset: 0x00078EC8
		public ServerLogConfiguration()
		{
			this.IsLoggingEnabled = AppConfigLoader.GetConfigBoolValue("ECPIsServerLoggingEnabled", true);
			this.LogPath = AppConfigLoader.GetConfigStringValue("ECPServerLogPath", ServerLogConfiguration.DefaultLogPath);
			this.MaxLogAge = AppConfigLoader.GetConfigTimeSpanValue("ECPServerMaxLogAge", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(30.0));
			this.MaxLogDirectorySizeInBytes = (long)ClientLogConfiguration.GetConfigByteQuantifiedSizeValue("ECPServerMaxLogDirectorySize", ByteQuantifiedSize.FromGB(1UL)).ToBytes();
			this.MaxLogFileSizeInBytes = (long)ClientLogConfiguration.GetConfigByteQuantifiedSizeValue("ECPServerMaxLogFileSize", ByteQuantifiedSize.FromMB(10UL)).ToBytes();
		}

		// Token: 0x17001C06 RID: 7174
		// (get) Token: 0x06002732 RID: 10034 RVA: 0x0007AD68 File Offset: 0x00078F68
		public static string DefaultLogPath
		{
			get
			{
				if (ServerLogConfiguration.defaultLogPath == null)
				{
					ServerLogConfiguration.defaultLogPath = Path.Combine(ConfigurationContext.Setup.InstallPath, "Logging\\ECP\\Server");
				}
				return ServerLogConfiguration.defaultLogPath;
			}
		}

		// Token: 0x17001C07 RID: 7175
		// (get) Token: 0x06002733 RID: 10035 RVA: 0x0007AD8A File Offset: 0x00078F8A
		// (set) Token: 0x06002734 RID: 10036 RVA: 0x0007AD92 File Offset: 0x00078F92
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x17001C08 RID: 7176
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x0007AD9B File Offset: 0x00078F9B
		public bool IsActivityEventHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C09 RID: 7177
		// (get) Token: 0x06002736 RID: 10038 RVA: 0x0007AD9E File Offset: 0x00078F9E
		// (set) Token: 0x06002737 RID: 10039 RVA: 0x0007ADA6 File Offset: 0x00078FA6
		public string LogPath { get; private set; }

		// Token: 0x17001C0A RID: 7178
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x0007ADAF File Offset: 0x00078FAF
		// (set) Token: 0x06002739 RID: 10041 RVA: 0x0007ADB7 File Offset: 0x00078FB7
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x17001C0B RID: 7179
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x0007ADC0 File Offset: 0x00078FC0
		// (set) Token: 0x0600273B RID: 10043 RVA: 0x0007ADC8 File Offset: 0x00078FC8
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x17001C0C RID: 7180
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x0007ADD1 File Offset: 0x00078FD1
		// (set) Token: 0x0600273D RID: 10045 RVA: 0x0007ADD9 File Offset: 0x00078FD9
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x17001C0D RID: 7181
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x0007ADE2 File Offset: 0x00078FE2
		public string LogComponent
		{
			get
			{
				return "ECPServerLog";
			}
		}

		// Token: 0x17001C0E RID: 7182
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x0007ADE9 File Offset: 0x00078FE9
		public string LogPrefix
		{
			get
			{
				return ServerLogConfiguration.LogPrefixValue;
			}
		}

		// Token: 0x17001C0F RID: 7183
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x0007ADF0 File Offset: 0x00078FF0
		public string LogType
		{
			get
			{
				return "ECP Server Log";
			}
		}

		// Token: 0x04001FCF RID: 8143
		private const string LogTypeValue = "ECP Server Log";

		// Token: 0x04001FD0 RID: 8144
		private const string LogComponentValue = "ECPServerLog";

		// Token: 0x04001FD1 RID: 8145
		private const string DefaultRelativeFilePath = "Logging\\ECP\\Server";

		// Token: 0x04001FD2 RID: 8146
		public static readonly string LogPrefixValue = "ECPServer";

		// Token: 0x04001FD3 RID: 8147
		private static string defaultLogPath;
	}
}
