using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x020004F2 RID: 1266
	public class Configurations
	{
		// Token: 0x06001F45 RID: 8005 RVA: 0x000BF44E File Offset: 0x000BD64E
		public Configurations(Dictionary<string, string> attributes)
		{
			this.server = LocalServer.GetServer();
			Configurations.CopyConfigurations(attributes, this.settings);
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x000BF478 File Offset: 0x000BD678
		public Server Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x000BF480 File Offset: 0x000BD680
		public bool DatabaseConsistencyEnabled
		{
			get
			{
				return this.IsWorkItemEnabled(Configurations.DatabaseConsistencyEnabledName);
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x000BF490 File Offset: 0x000BD690
		public TimeSpan DatabaseConsistencyRecurrenceInterval
		{
			get
			{
				TimeSpan result;
				if (this.settings.ContainsKey(Configurations.DatabaseConsistencyRecurrenceIntervalName) && TimeSpan.TryParse(this.settings[Configurations.DatabaseConsistencyRecurrenceIntervalName], out result))
				{
					return result;
				}
				return Configurations.DefaultDatabaseConsistencyRecurrenceInterval;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x000BF4D0 File Offset: 0x000BD6D0
		public int DatabaseConsistencyMonitorThreshold
		{
			get
			{
				int result;
				if (!int.TryParse(this.settings[Configurations.DatabaseConsistencyMonitorThresholdName], out result))
				{
					result = 10;
				}
				return result;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x000BF4FA File Offset: 0x000BD6FA
		public bool DeltaSyncEndpointUnreachableMonitorAndResponderEnabled
		{
			get
			{
				return this.IsWorkItemEnabled(Configurations.DeltaSyncEndpointUnreachableMonitorAndResponderEnabledName);
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x000BF507 File Offset: 0x000BD707
		public bool DeltaSyncServiceEndpointsLoadFailedMonitorAndResponderEnabled
		{
			get
			{
				return this.IsWorkItemEnabled(Configurations.DeltaSyncServiceEndpointsLoadFailedMonitorAndResponderEnabledName);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x000BF514 File Offset: 0x000BD714
		public bool DeltaSyncPartnerAuthenticationFailedMonitorAndResponderEnabled
		{
			get
			{
				return this.IsWorkItemEnabled(Configurations.DeltaSyncPartnerAuthenticationFailedMonitorAndResponderEnabledName);
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x000BF521 File Offset: 0x000BD721
		public bool ProcessCrashDetectionEnabled
		{
			get
			{
				return this.IsWorkItemEnabled(Configurations.ProcessCrashDetectionEnabledName);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x000BF530 File Offset: 0x000BD730
		public TimeSpan ProcessCrashDetectionRecurrenceInterval
		{
			get
			{
				TimeSpan result;
				if (this.settings.ContainsKey(Configurations.ProcessCrashDetectionRecurrenceIntervalName) && TimeSpan.TryParse(this.settings[Configurations.ProcessCrashDetectionRecurrenceIntervalName], out result))
				{
					return result;
				}
				return Configurations.DefaultProcessCrashDetectionRecurrenceInterval;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x000BF570 File Offset: 0x000BD770
		public int ProcessCrashDetectionMonitorThreshold
		{
			get
			{
				int result;
				if (!int.TryParse(this.settings[Configurations.ProcessCrashDetectionMonitorThresholdName], out result))
				{
					result = 10;
				}
				return result;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x000BF59A File Offset: 0x000BD79A
		public bool RegistryAccessDeniedMonitorAndResponderEnabled
		{
			get
			{
				return this.IsWorkItemEnabled(Configurations.RegistryAccessDeniedMonitorAndResponderEnabledName);
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x000BF5A7 File Offset: 0x000BD7A7
		public bool ServiceAvailabilityEnabled
		{
			get
			{
				return this.IsWorkItemEnabled(Configurations.ServiceAvailabilityEnabledName);
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x000BF5B4 File Offset: 0x000BD7B4
		public TimeSpan ServiceAvailabilityRecurrenceInterval
		{
			get
			{
				TimeSpan result;
				if (this.settings.ContainsKey(Configurations.ServiceAvailabilityRecurrenceIntervalName) && TimeSpan.TryParse(this.settings[Configurations.ServiceAvailabilityRecurrenceIntervalName], out result))
				{
					return result;
				}
				return Configurations.DefaultServiceAvailabilityRecurrenceInterval;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x000BF5F3 File Offset: 0x000BD7F3
		public bool SubscriptionSlaMissedMonitorAndResponderEnabled
		{
			get
			{
				return this.IsWorkItemEnabled(Configurations.SubscriptionSlaMissedMonitorAndResponderEnabledName);
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x000BF600 File Offset: 0x000BD800
		public int SubscriptionSlaMissedMonitorThreshold
		{
			get
			{
				int result;
				if (!int.TryParse(this.settings[Configurations.SubscriptionSlaMissedMonitorThresholdName], out result))
				{
					result = 12;
				}
				return result;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001F55 RID: 8021 RVA: 0x000BF62C File Offset: 0x000BD82C
		public int SubscriptionSlaMissedPerfCounterThreshold
		{
			get
			{
				int result;
				if (!int.TryParse(this.settings[Configurations.SubscriptionSlaMissedPerfCounterThresholdName], out result))
				{
					result = 3900;
				}
				return result;
			}
		}

		// Token: 0x17000673 RID: 1651
		public string this[string name]
		{
			get
			{
				if (!this.settings.ContainsKey(name))
				{
					return null;
				}
				return this.settings[name];
			}
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x000BF677 File Offset: 0x000BD877
		public static Configurations CreateFromWorkDefinition(WorkDefinition workDefinition)
		{
			return new Configurations(workDefinition.Attributes);
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x000BF684 File Offset: 0x000BD884
		public void Add(string key, string value)
		{
			this.settings.Add(key, value);
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x000BF694 File Offset: 0x000BD894
		private static void CopyConfigurations(Dictionary<string, string> source, Dictionary<string, string> target)
		{
			foreach (string key in source.Keys)
			{
				target[key] = source[key];
			}
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x000BF6F0 File Offset: 0x000BD8F0
		private bool IsWorkItemEnabled(string workitemName)
		{
			bool flag;
			return this.settings.ContainsKey(workitemName) && bool.TryParse(this.settings[workitemName], out flag) && flag;
		}

		// Token: 0x040016DC RID: 5852
		private const int DefaultDatabaseConsistencyMonitorThreshold = 10;

		// Token: 0x040016DD RID: 5853
		private const int DefaultProcessCrashDetectionMonitorThreshold = 10;

		// Token: 0x040016DE RID: 5854
		private const int DefaultSubscriptionSlaMissedMonitorThreshold = 12;

		// Token: 0x040016DF RID: 5855
		private const int DefaultSubscriptionSlaMissedPerfCounterThreshold = 3900;

		// Token: 0x040016E0 RID: 5856
		public static readonly TimeSpan GetExchangeDiagnosticInfoTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x040016E1 RID: 5857
		public static readonly string TransportSyncManagerProcessName = "Microsoft.Exchange.TransportSyncManagerSvc";

		// Token: 0x040016E2 RID: 5858
		public static readonly string TransportSyncManagerServiceName = "MSExchangeTransportSyncManagerSvc";

		// Token: 0x040016E3 RID: 5859
		private static readonly string DatabaseConsistencyEnabledName = "DatabaseConsistencyEnabled";

		// Token: 0x040016E4 RID: 5860
		private static readonly string DatabaseConsistencyRecurrenceIntervalName = "DatabaseConsistencyRecurrenceInterval";

		// Token: 0x040016E5 RID: 5861
		private static readonly string DatabaseConsistencyMonitorThresholdName = "DatabaseConsistencyMonitorThreshold";

		// Token: 0x040016E6 RID: 5862
		private static readonly string DeltaSyncEndpointUnreachableMonitorAndResponderEnabledName = "DeltaSyncEndpointUnreachableMonitorAndResponderEnabled";

		// Token: 0x040016E7 RID: 5863
		private static readonly string DeltaSyncServiceEndpointsLoadFailedMonitorAndResponderEnabledName = "DeltaSyncServiceEndpointsLoadFailedMonitorAndResponderEnabled";

		// Token: 0x040016E8 RID: 5864
		private static readonly string DeltaSyncPartnerAuthenticationFailedMonitorAndResponderEnabledName = "DeltaSyncPartnerAuthenticationFailedMonitorAndResponderEnabled";

		// Token: 0x040016E9 RID: 5865
		private static readonly string ProcessCrashDetectionEnabledName = "ProcessCrashDetectionEnabled";

		// Token: 0x040016EA RID: 5866
		private static readonly string ProcessCrashDetectionRecurrenceIntervalName = "ProcessCrashDetectionRecurrenceInterval";

		// Token: 0x040016EB RID: 5867
		private static readonly string ProcessCrashDetectionMonitorThresholdName = "ProcessCrashDetectionMonitorThreshold";

		// Token: 0x040016EC RID: 5868
		private static readonly string RegistryAccessDeniedMonitorAndResponderEnabledName = "RegistryAccessDeniedMonitorAndResponderEnabled";

		// Token: 0x040016ED RID: 5869
		private static readonly string ServiceAvailabilityEnabledName = "ServiceAvailabilityEnabled";

		// Token: 0x040016EE RID: 5870
		private static readonly string ServiceAvailabilityRecurrenceIntervalName = "ServiceAvailabilityRecurrenceInterval";

		// Token: 0x040016EF RID: 5871
		private static readonly string SubscriptionSlaMissedMonitorAndResponderEnabledName = "SubscriptionSlaMissedMonitorAndResponderEnabled";

		// Token: 0x040016F0 RID: 5872
		private static readonly string SubscriptionSlaMissedMonitorThresholdName = "SubscriptionSlaMissedMonitorThreshold";

		// Token: 0x040016F1 RID: 5873
		private static readonly string SubscriptionSlaMissedPerfCounterThresholdName = "SubscriptionSlaMissedPerfCounterThreshold";

		// Token: 0x040016F2 RID: 5874
		private static readonly TimeSpan DefaultDatabaseConsistencyRecurrenceInterval = TimeSpan.FromSeconds(360.0);

		// Token: 0x040016F3 RID: 5875
		private static readonly TimeSpan DefaultProcessCrashDetectionRecurrenceInterval = TimeSpan.FromSeconds(360.0);

		// Token: 0x040016F4 RID: 5876
		private static readonly TimeSpan DefaultServiceAvailabilityRecurrenceInterval = TimeSpan.FromSeconds(360.0);

		// Token: 0x040016F5 RID: 5877
		private Dictionary<string, string> settings = new Dictionary<string, string>();

		// Token: 0x040016F6 RID: 5878
		private readonly Server server;
	}
}
