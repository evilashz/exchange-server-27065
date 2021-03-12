using System;
using System.IO;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001A7 RID: 423
	internal sealed class ActivityContextLogConfiguration : ILogConfiguration
	{
		// Token: 0x06002386 RID: 9094 RVA: 0x0006CD38 File Offset: 0x0006AF38
		public ActivityContextLogConfiguration()
		{
			this.IsLoggingEnabled = AppConfigLoader.GetConfigBoolValue("IsActivityContextLoggingEnabled", true);
			this.LogPath = AppConfigLoader.GetConfigStringValue("ActivityContextLogPath", ActivityContextLogConfiguration.DefaultLogPath);
			this.MaxLogAge = AppConfigLoader.GetConfigTimeSpanValue("ActivityContextMaxLogAge", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(30.0));
			this.MaxLogDirectorySizeInBytes = (long)ClientLogConfiguration.GetConfigByteQuantifiedSizeValue("ActivityContextMaxLogDirectorySize", ByteQuantifiedSize.FromGB(1UL)).ToBytes();
			this.MaxLogFileSizeInBytes = (long)ClientLogConfiguration.GetConfigByteQuantifiedSizeValue("ActivityContextMaxLogFileSize", ByteQuantifiedSize.FromMB(10UL)).ToBytes();
		}

		// Token: 0x17001AD1 RID: 6865
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x0006CDD8 File Offset: 0x0006AFD8
		public static string DefaultLogPath
		{
			get
			{
				if (ActivityContextLogConfiguration.defaultLogPath == null)
				{
					ActivityContextLogConfiguration.defaultLogPath = Path.Combine(ConfigurationContext.Setup.InstallPath, "Logging\\ECP\\Activity");
				}
				return ActivityContextLogConfiguration.defaultLogPath;
			}
		}

		// Token: 0x17001AD2 RID: 6866
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x0006CDFA File Offset: 0x0006AFFA
		// (set) Token: 0x06002389 RID: 9097 RVA: 0x0006CE02 File Offset: 0x0006B002
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x17001AD3 RID: 6867
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x0006CE0B File Offset: 0x0006B00B
		public bool IsActivityEventHandler
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001AD4 RID: 6868
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x0006CE0E File Offset: 0x0006B00E
		// (set) Token: 0x0600238C RID: 9100 RVA: 0x0006CE16 File Offset: 0x0006B016
		public string LogPath { get; private set; }

		// Token: 0x17001AD5 RID: 6869
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x0006CE1F File Offset: 0x0006B01F
		// (set) Token: 0x0600238E RID: 9102 RVA: 0x0006CE27 File Offset: 0x0006B027
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x17001AD6 RID: 6870
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x0006CE30 File Offset: 0x0006B030
		// (set) Token: 0x06002390 RID: 9104 RVA: 0x0006CE38 File Offset: 0x0006B038
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x17001AD7 RID: 6871
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x0006CE41 File Offset: 0x0006B041
		// (set) Token: 0x06002392 RID: 9106 RVA: 0x0006CE49 File Offset: 0x0006B049
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x17001AD8 RID: 6872
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x0006CE52 File Offset: 0x0006B052
		public string LogComponent
		{
			get
			{
				return "ECPActivityContextLog";
			}
		}

		// Token: 0x17001AD9 RID: 6873
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x0006CE59 File Offset: 0x0006B059
		public string LogPrefix
		{
			get
			{
				return ActivityContextLogConfiguration.LogPrefixValue;
			}
		}

		// Token: 0x17001ADA RID: 6874
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x0006CE60 File Offset: 0x0006B060
		public string LogType
		{
			get
			{
				return "ECP Activity Context Log";
			}
		}

		// Token: 0x04001DF3 RID: 7667
		private const string LogTypeValue = "ECP Activity Context Log";

		// Token: 0x04001DF4 RID: 7668
		private const string LogComponentValue = "ECPActivityContextLog";

		// Token: 0x04001DF5 RID: 7669
		private const string DefaultRelativeFilePath = "Logging\\ECP\\Activity";

		// Token: 0x04001DF6 RID: 7670
		public static readonly string LogPrefixValue = "ECPActivity";

		// Token: 0x04001DF7 RID: 7671
		private static string defaultLogPath;
	}
}
