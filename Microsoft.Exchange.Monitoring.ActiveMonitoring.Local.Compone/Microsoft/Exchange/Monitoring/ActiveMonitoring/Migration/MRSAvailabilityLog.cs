using System;
using System.IO;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Migration
{
	// Token: 0x02000215 RID: 533
	internal class MRSAvailabilityLog : ObjectLog<MRSAvailabilityData>
	{
		// Token: 0x06000F06 RID: 3846 RVA: 0x00064138 File Offset: 0x00062338
		private MRSAvailabilityLog() : base(new MRSAvailabilityLog.MRSAvailabilityLogSchema(), new MRSAvailabilityLog.MRSAvailabilityLogConfiguration())
		{
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0006414A File Offset: 0x0006234A
		private static MRSAvailabilityLog Instance
		{
			get
			{
				if (MRSAvailabilityLog.instance == null)
				{
					MRSAvailabilityLog.instance = new MRSAvailabilityLog();
				}
				return MRSAvailabilityLog.instance;
			}
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00064164 File Offset: 0x00062364
		public static void Write(string context, string data)
		{
			MRSAvailabilityData objectToLog = default(MRSAvailabilityData);
			objectToLog.EventContext = context;
			objectToLog.EventData = data;
			MRSAvailabilityLog.Instance.LogObject(objectToLog);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00064194 File Offset: 0x00062394
		public static void SetLogEnabledStatus(bool isEnabled)
		{
			MRSAvailabilityLog.MRSAvailabilityLogConfiguration.LoggingEnabled = isEnabled;
		}

		// Token: 0x04000B38 RID: 2872
		private static MRSAvailabilityLog instance;

		// Token: 0x02000216 RID: 534
		private class MRSAvailabilityLogSchema : ObjectLogSchema
		{
			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0006419C File Offset: 0x0006239C
			public override string LogType
			{
				get
				{
					return "MRSAvailability Log";
				}
			}

			// Token: 0x04000B39 RID: 2873
			public static readonly ObjectLogSimplePropertyDefinition<MRSAvailabilityData> EntryType = new ObjectLogSimplePropertyDefinition<MRSAvailabilityData>("Server", (MRSAvailabilityData d) => d.Server);

			// Token: 0x04000B3A RID: 2874
			public static readonly ObjectLogSimplePropertyDefinition<MRSAvailabilityData> ExecutionGuid = new ObjectLogSimplePropertyDefinition<MRSAvailabilityData>("Version", (MRSAvailabilityData d) => d.Version.ToString());

			// Token: 0x04000B3B RID: 2875
			public static readonly ObjectLogSimplePropertyDefinition<MRSAvailabilityData> Failure = new ObjectLogSimplePropertyDefinition<MRSAvailabilityData>("EventContext", (MRSAvailabilityData d) => d.EventContext);

			// Token: 0x04000B3C RID: 2876
			public static readonly ObjectLogSimplePropertyDefinition<MRSAvailabilityData> ExchangeVersion = new ObjectLogSimplePropertyDefinition<MRSAvailabilityData>("EventData", (MRSAvailabilityData d) => d.EventData);
		}

		// Token: 0x02000217 RID: 535
		private class MRSAvailabilityLogConfiguration : ObjectLogConfiguration
		{
			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x06000F11 RID: 3857 RVA: 0x000642A1 File Offset: 0x000624A1
			// (set) Token: 0x06000F12 RID: 3858 RVA: 0x000642A8 File Offset: 0x000624A8
			public static bool LoggingEnabled
			{
				get
				{
					return MRSAvailabilityLog.MRSAvailabilityLogConfiguration.loggingEnabled;
				}
				set
				{
					MRSAvailabilityLog.MRSAvailabilityLogConfiguration.loggingEnabled = value;
				}
			}

			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x06000F13 RID: 3859 RVA: 0x000642B0 File Offset: 0x000624B0
			public override string FilenamePrefix
			{
				get
				{
					return "MRSAvailability_";
				}
			}

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x06000F14 RID: 3860 RVA: 0x000642B7 File Offset: 0x000624B7
			public override bool IsEnabled
			{
				get
				{
					return MRSAvailabilityLog.MRSAvailabilityLogConfiguration.LoggingEnabled;
				}
			}

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06000F15 RID: 3861 RVA: 0x000642BE File Offset: 0x000624BE
			public override string LogComponentName
			{
				get
				{
					return "MRSAvailability";
				}
			}

			// Token: 0x170002F5 RID: 757
			// (get) Token: 0x06000F16 RID: 3862 RVA: 0x000642C5 File Offset: 0x000624C5
			public override string LoggingFolder
			{
				get
				{
					return MRSAvailabilityLog.MRSAvailabilityLogConfiguration.DefaultLoggingPath;
				}
			}

			// Token: 0x170002F6 RID: 758
			// (get) Token: 0x06000F17 RID: 3863 RVA: 0x000642CC File Offset: 0x000624CC
			public override TimeSpan MaxLogAge
			{
				get
				{
					return TimeSpan.FromDays(30.0);
				}
			}

			// Token: 0x170002F7 RID: 759
			// (get) Token: 0x06000F18 RID: 3864 RVA: 0x000642DC File Offset: 0x000624DC
			public override long MaxLogDirSize
			{
				get
				{
					return 50000000L;
				}
			}

			// Token: 0x170002F8 RID: 760
			// (get) Token: 0x06000F19 RID: 3865 RVA: 0x000642E4 File Offset: 0x000624E4
			public override long MaxLogFileSize
			{
				get
				{
					return 500000L;
				}
			}

			// Token: 0x04000B41 RID: 2881
			private const string LogFilePrefix = "MRSAvailability_";

			// Token: 0x04000B42 RID: 2882
			private const string MRSAvailabilityLogName = "MRSAvailability";

			// Token: 0x04000B43 RID: 2883
			private static readonly string DefaultLoggingPath = Path.Combine(ConfigurationContext.Setup.LoggingPath, "MailboxReplicationService", "MRSAvailability");

			// Token: 0x04000B44 RID: 2884
			public static bool loggingEnabled = true;
		}
	}
}
