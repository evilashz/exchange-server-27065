using System;
using System.IO;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000214 RID: 532
	internal sealed class ClientLogConfiguration : ILogConfiguration
	{
		// Token: 0x06002716 RID: 10006 RVA: 0x0007A898 File Offset: 0x00078A98
		public ClientLogConfiguration()
		{
			this.IsLoggingEnabled = AppConfigLoader.GetConfigBoolValue("ECPIsClientLoggingEnabled", true);
			this.LogPath = AppConfigLoader.GetConfigStringValue("ECPClientLogPath", ClientLogConfiguration.DefaultLogPath);
			this.MaxLogAge = AppConfigLoader.GetConfigTimeSpanValue("ECPClientMaxLogAge", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(30.0));
			this.MaxLogDirectorySizeInBytes = (long)ClientLogConfiguration.GetConfigByteQuantifiedSizeValue("ECPClientMaxLogDirectorySize", ByteQuantifiedSize.FromGB(1UL)).ToBytes();
			this.MaxLogFileSizeInBytes = (long)ClientLogConfiguration.GetConfigByteQuantifiedSizeValue("ECPClientMaxLogFileSize", ByteQuantifiedSize.FromMB(10UL)).ToBytes();
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x0007A938 File Offset: 0x00078B38
		internal static ByteQuantifiedSize GetConfigByteQuantifiedSizeValue(string configName, ByteQuantifiedSize defaultValue)
		{
			string expression = null;
			ByteQuantifiedSize result;
			if (AppConfigLoader.TryGetConfigRawValue(configName, out expression) && ByteQuantifiedSize.TryParse(expression, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x17001BFA RID: 7162
		// (get) Token: 0x06002718 RID: 10008 RVA: 0x0007A95E File Offset: 0x00078B5E
		public static string DefaultLogPath
		{
			get
			{
				if (ClientLogConfiguration.defaultLogPath == null)
				{
					ClientLogConfiguration.defaultLogPath = Path.Combine(ConfigurationContext.Setup.InstallPath, "Logging\\ECP\\Client");
				}
				return ClientLogConfiguration.defaultLogPath;
			}
		}

		// Token: 0x17001BFB RID: 7163
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x0007A980 File Offset: 0x00078B80
		// (set) Token: 0x0600271A RID: 10010 RVA: 0x0007A988 File Offset: 0x00078B88
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x17001BFC RID: 7164
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x0007A991 File Offset: 0x00078B91
		public bool IsActivityEventHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BFD RID: 7165
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x0007A994 File Offset: 0x00078B94
		// (set) Token: 0x0600271D RID: 10013 RVA: 0x0007A99C File Offset: 0x00078B9C
		public string LogPath { get; private set; }

		// Token: 0x17001BFE RID: 7166
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x0007A9A5 File Offset: 0x00078BA5
		// (set) Token: 0x0600271F RID: 10015 RVA: 0x0007A9AD File Offset: 0x00078BAD
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x17001BFF RID: 7167
		// (get) Token: 0x06002720 RID: 10016 RVA: 0x0007A9B6 File Offset: 0x00078BB6
		// (set) Token: 0x06002721 RID: 10017 RVA: 0x0007A9BE File Offset: 0x00078BBE
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x17001C00 RID: 7168
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x0007A9C7 File Offset: 0x00078BC7
		// (set) Token: 0x06002723 RID: 10019 RVA: 0x0007A9CF File Offset: 0x00078BCF
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x17001C01 RID: 7169
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x0007A9D8 File Offset: 0x00078BD8
		public string LogComponent
		{
			get
			{
				return "ECPClientLog";
			}
		}

		// Token: 0x17001C02 RID: 7170
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x0007A9DF File Offset: 0x00078BDF
		public string LogPrefix
		{
			get
			{
				return ClientLogConfiguration.LogPrefixValue;
			}
		}

		// Token: 0x17001C03 RID: 7171
		// (get) Token: 0x06002726 RID: 10022 RVA: 0x0007A9E6 File Offset: 0x00078BE6
		public string LogType
		{
			get
			{
				return "ECP Client Log";
			}
		}

		// Token: 0x04001FC0 RID: 8128
		private const string LogTypeValue = "ECP Client Log";

		// Token: 0x04001FC1 RID: 8129
		private const string LogComponentValue = "ECPClientLog";

		// Token: 0x04001FC2 RID: 8130
		private const string DefaultRelativeFilePath = "Logging\\ECP\\Client";

		// Token: 0x04001FC3 RID: 8131
		public static readonly string LogPrefixValue = "ECPClient";

		// Token: 0x04001FC4 RID: 8132
		private static string defaultLogPath;
	}
}
