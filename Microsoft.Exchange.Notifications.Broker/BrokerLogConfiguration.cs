using System;
using System.IO;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class BrokerLogConfiguration : ILogConfiguration
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000215C File Offset: 0x0000035C
		public BrokerLogConfiguration()
		{
			this.IsLoggingEnabled = AppConfigLoader.GetConfigBoolValue("IsServiceLoggingEnabled", true);
			this.LogPath = AppConfigLoader.GetConfigStringValue("ServiceLogPath", BrokerLogConfiguration.DefaultLogPath);
			this.MaxLogAge = AppConfigLoader.GetConfigTimeSpanValue("ServiceMaxLogAge", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(30.0));
			this.MaxLogDirectorySizeInBytes = (long)ByteQuantifiedSize.FromGB(1UL).ToBytes();
			this.MaxLogFileSizeInBytes = (long)ByteQuantifiedSize.FromMB(10UL).ToBytes();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000021E8 File Offset: 0x000003E8
		public static string DefaultLogPath
		{
			get
			{
				if (BrokerLogConfiguration.defaultLogPath == null)
				{
					BrokerLogConfiguration.defaultLogPath = Path.Combine(ExchangeSetupContext.LoggingPath, "NotificationBroker\\Service");
				}
				return BrokerLogConfiguration.defaultLogPath;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000220A File Offset: 0x0000040A
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002212 File Offset: 0x00000412
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000221B File Offset: 0x0000041B
		public bool IsActivityEventHandler
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000221E File Offset: 0x0000041E
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002226 File Offset: 0x00000426
		public string LogPath { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000222F File Offset: 0x0000042F
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002237 File Offset: 0x00000437
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002240 File Offset: 0x00000440
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002248 File Offset: 0x00000448
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002251 File Offset: 0x00000451
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002259 File Offset: 0x00000459
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002262 File Offset: 0x00000462
		public string LogComponent
		{
			get
			{
				return "NotificationBrokerServiceLog";
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002269 File Offset: 0x00000469
		public string LogPrefix
		{
			get
			{
				return BrokerLogConfiguration.LogPrefixValue;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002270 File Offset: 0x00000470
		public string LogType
		{
			get
			{
				return "Notification Broker Service Log";
			}
		}

		// Token: 0x04000002 RID: 2
		private const string LogTypeValue = "Notification Broker Service Log";

		// Token: 0x04000003 RID: 3
		private const string LogComponentValue = "NotificationBrokerServiceLog";

		// Token: 0x04000004 RID: 4
		private const string DefaultRelativeFilePath = "NotificationBroker\\Service";

		// Token: 0x04000005 RID: 5
		public static readonly string LogPrefixValue = "NotificationBrokerService";

		// Token: 0x04000006 RID: 6
		private static string defaultLogPath;
	}
}
