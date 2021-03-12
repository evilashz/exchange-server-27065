using System;
using System.IO;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class NotificationBrokerClientLogConfiguration : ILogConfiguration
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002BFC File Offset: 0x00000DFC
		public NotificationBrokerClientLogConfiguration()
		{
			this.IsLoggingEnabled = AppConfigLoader.GetConfigBoolValue("IsClientLoggingEnabled", true);
			this.LogPath = AppConfigLoader.GetConfigStringValue("ClientLogPath", NotificationBrokerClientLogConfiguration.DefaultLogPath);
			this.MaxLogAge = AppConfigLoader.GetConfigTimeSpanValue("ClientMaxLogAge", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(30.0));
			this.MaxLogDirectorySizeInBytes = (long)ByteQuantifiedSize.FromGB(1UL).ToBytes();
			this.MaxLogFileSizeInBytes = (long)ByteQuantifiedSize.FromMB(10UL).ToBytes();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002C88 File Offset: 0x00000E88
		public static string DefaultLogPath
		{
			get
			{
				if (NotificationBrokerClientLogConfiguration.defaultLogPath == null)
				{
					NotificationBrokerClientLogConfiguration.defaultLogPath = Path.Combine(ExchangeSetupContext.LoggingPath, "NotificationBroker\\Client");
				}
				return NotificationBrokerClientLogConfiguration.defaultLogPath;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002CAA File Offset: 0x00000EAA
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002CB2 File Offset: 0x00000EB2
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002CBB File Offset: 0x00000EBB
		public bool IsActivityEventHandler
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002CBE File Offset: 0x00000EBE
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002CC6 File Offset: 0x00000EC6
		public string LogPath { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002CCF File Offset: 0x00000ECF
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002CD7 File Offset: 0x00000ED7
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002CE0 File Offset: 0x00000EE0
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002CF1 File Offset: 0x00000EF1
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002CF9 File Offset: 0x00000EF9
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002D02 File Offset: 0x00000F02
		public string LogComponent
		{
			get
			{
				return "NotificationBrokerClientLog";
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002D09 File Offset: 0x00000F09
		public string LogPrefix
		{
			get
			{
				return NotificationBrokerClientLogConfiguration.LogPrefixValue;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002D10 File Offset: 0x00000F10
		public string LogType
		{
			get
			{
				return "Notification Broker Client Log";
			}
		}

		// Token: 0x0400000E RID: 14
		private const string LogTypeValue = "Notification Broker Client Log";

		// Token: 0x0400000F RID: 15
		private const string LogComponentValue = "NotificationBrokerClientLog";

		// Token: 0x04000010 RID: 16
		private const string DefaultRelativeFilePath = "NotificationBroker\\Client";

		// Token: 0x04000011 RID: 17
		public static readonly string LogPrefixValue = "NotificationBrokerClient";

		// Token: 0x04000012 RID: 18
		private static string defaultLogPath;
	}
}
