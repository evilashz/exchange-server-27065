using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.Transport;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Management.QueueDigest;
using Microsoft.Exchange.Net.DiagnosticsAggregation;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.DiagnosticsAggregationService;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x020001F4 RID: 500
	internal class QueueConfiguration
	{
		// Token: 0x06000ED2 RID: 3794 RVA: 0x00024BC0 File Offset: 0x00022DC0
		public QueueConfiguration()
		{
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00024C2C File Offset: 0x00022E2C
		public QueueConfiguration(XmlNode queueNode)
		{
			this.Name = Utils.GetOptionalXmlAttribute<string>(queueNode, "Name", string.Empty);
			this.QueueType = Utils.GetMandatoryXmlEnumAttribute<QueueType>(queueNode, "QueueType");
			this.MessageCountThreshold = Utils.GetOptionalXmlAttribute<int>(queueNode, "MessageCountThreshold", 0);
			this.DeferredMessageCountThreshold = Utils.GetOptionalXmlAttribute<int>(queueNode, "DeferredMessageCountThreshold", 0);
			this.LockedMessageCountThreshold = Utils.GetOptionalXmlAttribute<int>(queueNode, "LockedMessageCountThreshold", 0);
			if (this.QueueType == QueueType.WellKnownDestination)
			{
				this.Destination = Utils.GetMandatoryXmlAttribute<string>(queueNode, "Destination");
				this.AggregatedBy = QueueDigestGroupBy.NextHopDomain;
				if (this.DeferredMessageCountThreshold == 0 && this.LockedMessageCountThreshold == 0 && this.MessageCountThreshold == 0)
				{
					throw new ConfigurationErrorsException("WellKnownDestination: missing valid thresholds for processing queue stats.");
				}
			}
			else
			{
				this.Destination = Utils.GetOptionalXmlAttribute<string>(queueNode, "Destination", null);
				this.AggregatedBy = Utils.GetOptionalXmlEnumAttribute<QueueDigestGroupBy>(queueNode, "AggregatedBy", QueueDigestGroupBy.NextHopDomain);
				this.MessageCountThreshold = Utils.GetOptionalXmlAttribute<int>(queueNode, "MessageCountThreshold", 0);
				this.AverageMessageCountThreshold = Utils.GetOptionalXmlAttribute<int>(queueNode, "AverageMessageCountThreshold", 0);
				this.TotalMessageCountThreshold = Utils.GetOptionalXmlAttribute<int>(queueNode, "TotalMessageCountThreshold", 0);
				this.ExceedsAverageByPercent = Utils.GetOptionalXmlAttribute<int>(queueNode, "ExceedsAverageByPercent", 0);
				this.ExceedsAverageByNumber = Utils.GetOptionalXmlAttribute<int>(queueNode, "ExceedsAverageByNumber", 0);
				this.ExceedsLowestByPercent = Utils.GetOptionalXmlAttribute<int>(queueNode, "ExceedsLowestByPercent", 0);
				this.ExceedsLowestByNumber = Utils.GetOptionalXmlAttribute<int>(queueNode, "ExceedsLowestByNumber", 0);
			}
			this.DeliveryType = Utils.GetOptionalXmlEnumAttribute<DeliveryType>(queueNode, "DeliveryType", DeliveryType.Undefined);
			this.EventNotificationComponent = Utils.GetMandatoryXmlAttribute<string>(queueNode, "EventNotificationComponent");
			this.EventNotificationServiceName = Utils.GetMandatoryXmlAttribute<string>(queueNode, "EventNotificationServiceName");
			this.DetailsLevel = DetailsLevel.Verbose;
			this.EventNotificationTag = Utils.GetOptionalXmlAttribute<string>(queueNode, "EventNotificationTag", string.Empty);
			this.EventNotificationSeverityLevel = Utils.GetOptionalXmlEnumAttribute<ResultSeverityLevel>(queueNode, "EventNotificationSeverityLevel", ResultSeverityLevel.Critical);
			this.ResultSize = Utils.GetOptionalXmlAttribute<uint>(queueNode, "ResultSize", uint.MaxValue);
			this.MessageCountGreaterThan = Utils.GetOptionalXmlAttribute<int>(queueNode, "DeferredMessageCountLessThan", 0);
			this.RaiseWarningOnNoStats = Utils.GetOptionalXmlAttribute<bool>(queueNode, "RaiseWarningOnNoStats", false);
			this.EventNotificationTagForNoStats = Utils.GetOptionalXmlAttribute<string>(queueNode, "EventNotificationTagForNoStats", null);
			this.FreshnessCutoffTime = Utils.GetOptionalXmlAttribute<TimeSpan>(queueNode, "FreshnessCutoffTime", TimeSpan.Parse("02:00:00"));
			this.Dag = Utils.GetOptionalXmlAttribute<string>(queueNode, "Dag", null);
			this.Server = Utils.GetOptionalXmlAttribute<string>(queueNode, "Server", null);
			if (this.Server != null && this.Server.ToLowerInvariant().Contains("localhost"))
			{
				this.Server = this.Server.Replace("localhost", Environment.MachineName);
			}
			this.Site = Utils.GetOptionalXmlAttribute<string>(queueNode, "Site", null);
			string optionalXmlAttribute = Utils.GetOptionalXmlAttribute<string>(queueNode, "RiskLevel", string.Empty);
			RiskLevel riskLevel;
			this.RiskLevel = (Enum.TryParse<RiskLevel>(optionalXmlAttribute, out riskLevel) ? riskLevel.ToString() : string.Empty);
			optionalXmlAttribute = Utils.GetOptionalXmlAttribute<string>(queueNode, "QueueStatus", string.Empty);
			QueueStatus queueStatus;
			this.QueueStatus = (Enum.TryParse<QueueStatus>(optionalXmlAttribute, out queueStatus) ? queueStatus.ToString() : string.Empty);
			this.ShouldExemptPoisonQueues = Utils.GetOptionalXmlAttribute<bool>(queueNode, "ExemptPoisonQueues", true);
			this.Session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 186, ".ctor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\QueueDigest\\Probes\\QueueConfiguration.cs");
			List<int> list;
			if (this.QueueType == QueueType.WellKnownDestination)
			{
				list = new List<int>
				{
					this.MessageCountThreshold,
					this.DeferredMessageCountThreshold,
					this.LockedMessageCountThreshold
				};
			}
			else
			{
				list = new List<int>
				{
					this.MessageCountThreshold,
					this.DeferredMessageCountThreshold,
					this.LockedMessageCountThreshold,
					this.AverageMessageCountThreshold,
					this.TotalMessageCountThreshold,
					this.ExceedsAverageByNumber,
					this.ExceedsAverageByPercent,
					this.ExceedsLowestByNumber,
					this.ExceedsLowestByPercent
				};
			}
			int num = 0;
			using (List<int>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == 0)
					{
						num++;
					}
				}
			}
			if (num == list.Count)
			{
				throw new ConfigurationErrorsException(string.Format("You must specify a threshold for processing queue stats: {0}", this.ToString()));
			}
			foreach (int num2 in list)
			{
				if (num2 < 0)
				{
					throw new ConfigurationErrorsException(string.Format("{0} must be a non-negative number.", num2));
				}
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00025130 File Offset: 0x00023330
		// (set) Token: 0x06000ED5 RID: 3797 RVA: 0x00025138 File Offset: 0x00023338
		internal string Name { get; set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00025141 File Offset: 0x00023341
		// (set) Token: 0x06000ED7 RID: 3799 RVA: 0x00025149 File Offset: 0x00023349
		internal QueueType QueueType { get; set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00025152 File Offset: 0x00023352
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x0002515A File Offset: 0x0002335A
		internal QueueDigestGroupBy AggregatedBy { get; set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00025163 File Offset: 0x00023363
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x0002516B File Offset: 0x0002336B
		internal string Destination { get; set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00025174 File Offset: 0x00023374
		// (set) Token: 0x06000EDD RID: 3805 RVA: 0x0002517C File Offset: 0x0002337C
		internal DeliveryType DeliveryType { get; set; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00025185 File Offset: 0x00023385
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x0002518D File Offset: 0x0002338D
		internal int MessageCountThreshold { get; set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00025196 File Offset: 0x00023396
		// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x0002519E File Offset: 0x0002339E
		internal string Server { get; set; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x000251A7 File Offset: 0x000233A7
		// (set) Token: 0x06000EE3 RID: 3811 RVA: 0x000251AF File Offset: 0x000233AF
		internal string Site { get; set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x000251B8 File Offset: 0x000233B8
		// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x000251C0 File Offset: 0x000233C0
		internal string Dag { get; set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x000251C9 File Offset: 0x000233C9
		// (set) Token: 0x06000EE7 RID: 3815 RVA: 0x000251D1 File Offset: 0x000233D1
		internal string QueueStatus { get; set; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x000251DA File Offset: 0x000233DA
		// (set) Token: 0x06000EE9 RID: 3817 RVA: 0x000251E2 File Offset: 0x000233E2
		internal string RiskLevel { get; set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x000251EB File Offset: 0x000233EB
		// (set) Token: 0x06000EEB RID: 3819 RVA: 0x000251F3 File Offset: 0x000233F3
		internal uint ResultSize { get; set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x000251FC File Offset: 0x000233FC
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x00025204 File Offset: 0x00023404
		internal TimeSpan FreshnessCutoffTime { get; set; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0002520D File Offset: 0x0002340D
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x00025215 File Offset: 0x00023415
		internal DetailsLevel DetailsLevel { get; set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0002521E File Offset: 0x0002341E
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x00025226 File Offset: 0x00023426
		internal bool ShouldExemptPoisonQueues { get; set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x0002522F File Offset: 0x0002342F
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x00025237 File Offset: 0x00023437
		internal string EventNotificationServiceName { get; set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00025240 File Offset: 0x00023440
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x00025248 File Offset: 0x00023448
		internal string EventNotificationComponent { get; set; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00025251 File Offset: 0x00023451
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x00025259 File Offset: 0x00023459
		internal string EventNotificationTag { get; set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00025262 File Offset: 0x00023462
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x0002526A File Offset: 0x0002346A
		internal ResultSeverityLevel EventNotificationSeverityLevel { get; set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00025273 File Offset: 0x00023473
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x0002527B File Offset: 0x0002347B
		internal bool RaiseWarningOnNoStats { get; set; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00025284 File Offset: 0x00023484
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x0002528C File Offset: 0x0002348C
		internal string EventNotificationTagForNoStats { get; set; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00025295 File Offset: 0x00023495
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x0002529D File Offset: 0x0002349D
		internal int AverageMessageCountThreshold { get; set; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x000252A6 File Offset: 0x000234A6
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x000252AE File Offset: 0x000234AE
		internal int TotalMessageCountThreshold { get; set; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x000252B7 File Offset: 0x000234B7
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x000252BF File Offset: 0x000234BF
		internal int ExceedsAverageByPercent { get; set; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x000252C8 File Offset: 0x000234C8
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x000252D0 File Offset: 0x000234D0
		internal int ExceedsAverageByNumber { get; set; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x000252D9 File Offset: 0x000234D9
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x000252E1 File Offset: 0x000234E1
		internal int ExceedsLowestByPercent { get; set; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x000252EA File Offset: 0x000234EA
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x000252F2 File Offset: 0x000234F2
		internal int ExceedsLowestByNumber { get; set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x000252FB File Offset: 0x000234FB
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x00025303 File Offset: 0x00023503
		internal int DeferredMessageCountThreshold { get; set; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0002530C File Offset: 0x0002350C
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x00025314 File Offset: 0x00023514
		internal int LockedMessageCountThreshold { get; set; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0002531D File Offset: 0x0002351D
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x00025325 File Offset: 0x00023525
		internal int MessageCountGreaterThan { get; set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0002532E File Offset: 0x0002352E
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x00025336 File Offset: 0x00023536
		internal ITopologyConfigurationSession Session { get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0002533F File Offset: 0x0002353F
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x00025347 File Offset: 0x00023547
		internal Guid ForestId { get; set; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00025350 File Offset: 0x00023550
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x00025358 File Offset: 0x00023558
		internal string ForestName { get; set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00025361 File Offset: 0x00023561
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x00025369 File Offset: 0x00023569
		internal MultiValuedProperty<Guid> ServerIdList { get; set; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00025372 File Offset: 0x00023572
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x0002537A File Offset: 0x0002357A
		internal MultiValuedProperty<Guid> DagIdList { get; set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00025383 File Offset: 0x00023583
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x0002538B File Offset: 0x0002358B
		internal MultiValuedProperty<Guid> SiteIdList { get; set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00025394 File Offset: 0x00023594
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x0002539C File Offset: 0x0002359C
		internal MultiValuedProperty<ComparisonFilter> DataFilter { get; set; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x000253A5 File Offset: 0x000235A5
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x000253AD File Offset: 0x000235AD
		internal string Filter { get; set; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x000253B6 File Offset: 0x000235B6
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x000253BE File Offset: 0x000235BE
		internal uint ServerSideResultSize { get; set; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x000253C7 File Offset: 0x000235C7
		// (set) Token: 0x06000F23 RID: 3875 RVA: 0x000253CF File Offset: 0x000235CF
		internal QueueAggregator Aggregator { get; set; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x000253D8 File Offset: 0x000235D8
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x000253E0 File Offset: 0x000235E0
		internal int WebServiceRequestsPending { get; set; }

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x000253E9 File Offset: 0x000235E9
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x000253F1 File Offset: 0x000235F1
		internal AutoResetEvent WebServiceRequestsDone { get; set; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x000253FA File Offset: 0x000235FA
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x00025402 File Offset: 0x00023602
		internal Binding WebServiceBinding { get; set; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x0002540B File Offset: 0x0002360B
		internal Dictionary<GroupOfServersKey, List<ADObjectId>> DagToServersMap
		{
			get
			{
				return this.dagToServersMap;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00025413 File Offset: 0x00023613
		internal HashSet<ADObjectId> ServersNotBelongingToAnyDag
		{
			get
			{
				return this.serversNotBelongingToAnyDag;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0002541B File Offset: 0x0002361B
		internal List<string> FailedToConnectServers
		{
			get
			{
				return this.failedToConnectServers;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00025423 File Offset: 0x00023623
		internal List<string> FailedToConnectDags
		{
			get
			{
				return this.failedToConnectDags;
			}
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0002542C File Offset: 0x0002362C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Name={0}, ", this.Name);
			stringBuilder.AppendFormat("DeferredMessageCountThreshold={0}, ", this.DeferredMessageCountThreshold);
			stringBuilder.AppendFormat("Destination={0}, ", this.Destination);
			stringBuilder.AppendFormat("Dag={0}, ", string.Join(",", new string[]
			{
				this.Dag
			}));
			stringBuilder.AppendFormat("EventNotificationComponent={0}, ", this.EventNotificationComponent);
			stringBuilder.AppendFormat("EventNotificationServiceName={0}, ", this.EventNotificationServiceName);
			stringBuilder.AppendFormat("EventNotificationSeverityLevel={0}, ", this.EventNotificationSeverityLevel.ToString());
			stringBuilder.AppendFormat("EventNotificationTag={0} ", this.EventNotificationTag);
			stringBuilder.AppendFormat("Filter={0} ", this.Filter);
			stringBuilder.AppendFormat("ForestId={0} ", this.ForestId);
			stringBuilder.AppendFormat("ForestName={0} ", this.ForestName);
			stringBuilder.AppendFormat("FreshnessCutoffTime={0} ", this.FreshnessCutoffTime);
			stringBuilder.AppendFormat("LockedMessageCountThreshold={0}, ", this.LockedMessageCountThreshold);
			stringBuilder.AppendFormat("MessageCountThreshold={0}, ", this.MessageCountThreshold);
			stringBuilder.AppendFormat("QueueStatus={0}, ", this.QueueStatus);
			stringBuilder.AppendFormat("QueueType={0}, ", this.QueueType.ToString());
			stringBuilder.AppendFormat("RiskLevel={0}, ", this.RiskLevel);
			stringBuilder.AppendFormat("Server={0}, ", string.Join(",", new string[]
			{
				this.Server
			}));
			stringBuilder.AppendFormat("Site={0}, ", string.Join(",", new string[]
			{
				this.Site
			}));
			if (this.QueueType == QueueType.Aggregated)
			{
				stringBuilder.AppendFormat("AverageMessageCountThreshold={0}, ", this.AverageMessageCountThreshold);
				stringBuilder.AppendFormat("ExceedsAverageByNumber={0} ", this.ExceedsAverageByNumber);
				stringBuilder.AppendFormat("ExceedsAverageByPercent={0} ", this.ExceedsAverageByPercent);
				stringBuilder.AppendFormat("ExceedsLowestByNumber={0} ", this.ExceedsLowestByNumber);
				stringBuilder.AppendFormat("ExceedsLowestByPercent={0} ", this.ExceedsLowestByPercent);
				stringBuilder.AppendFormat("MessageCountGreaterThan={0}, ", this.MessageCountGreaterThan);
				stringBuilder.AppendFormat("TotalMessageCountThreshold={0}, ", this.TotalMessageCountThreshold);
				stringBuilder.AppendFormat("ExemptPoisonQueues={0}, ", this.ShouldExemptPoisonQueues);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x000256F4 File Offset: 0x000238F4
		internal static List<QueueConfiguration> GetRemoteDomains(ITopologyConfigurationSession session)
		{
			List<QueueConfiguration> list = new List<QueueConfiguration>();
			if (QueueConfiguration.lastADQueryTime == DateTime.MaxValue || DateTime.UtcNow - QueueConfiguration.lastADQueryTime >= TimeSpan.FromMinutes(QueueConfiguration.ADQueryIntervalMins))
			{
				QueueConfiguration.cachedWellKnownDestinations.Clear();
				QueueConfiguration.lastADQueryTime = DateTime.UtcNow;
				ADPagedReader<DomainContentConfig> remoteDomains = null;
				ADNotificationAdapter.TryRunADOperation(delegate()
				{
					remoteDomains = session.FindAllPaged<DomainContentConfig>();
				});
				if (remoteDomains == null)
				{
					return null;
				}
				using (IEnumerator<DomainContentConfig> enumerator = remoteDomains.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DomainContentConfig domainContentConfig = enumerator.Current;
						if (domainContentConfig.MessageCountThreshold != 2147483647)
						{
							QueueConfiguration queueConfiguration = new QueueConfiguration();
							queueConfiguration.Name = domainContentConfig.Name;
							queueConfiguration.Destination = domainContentConfig.DomainName.Domain;
							queueConfiguration.MessageCountThreshold = domainContentConfig.MessageCountThreshold;
							queueConfiguration.QueueType = QueueType.WellKnownDestination;
							queueConfiguration.AggregatedBy = QueueDigestGroupBy.NextHopDomain;
							queueConfiguration.DeliveryType = DeliveryType.Undefined;
							queueConfiguration.EventNotificationServiceName = "Transport";
							queueConfiguration.EventNotificationComponent = "WellKnownDestinationMessageCountExceedsThreshold";
							queueConfiguration.DetailsLevel = DetailsLevel.Verbose;
							queueConfiguration.EventNotificationSeverityLevel = ResultSeverityLevel.Critical;
							queueConfiguration.ResultSize = uint.MaxValue;
							queueConfiguration.MessageCountGreaterThan = 0;
							queueConfiguration.RaiseWarningOnNoStats = false;
							queueConfiguration.FreshnessCutoffTime = TimeSpan.Parse("02:00:00");
							queueConfiguration.Session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 583, "GetRemoteDomains", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\QueueDigest\\Probes\\QueueConfiguration.cs");
							if (QueueConfiguration.cachedWellKnownDestinations.TryAdd(domainContentConfig.Guid, queueConfiguration))
							{
								queueConfiguration.ResolveParameters();
								list.Add(queueConfiguration);
							}
						}
					}
					return list;
				}
			}
			foreach (KeyValuePair<Guid, QueueConfiguration> keyValuePair in QueueConfiguration.cachedWellKnownDestinations)
			{
				list.Add(keyValuePair.Value);
			}
			return list;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00025900 File Offset: 0x00023B00
		internal void ResolveParameters()
		{
			Guid forestId;
			string forestName;
			TransportADUtils.GetForestInformation(out forestId, out forestName);
			this.ForestId = forestId;
			this.ForestName = forestName;
			MultiValuedProperty<Guid> serverIdList;
			MultiValuedProperty<Guid> dagIdList;
			MultiValuedProperty<Guid> siteIdList;
			this.GetTargetResourceGuids(out serverIdList, out dagIdList, out siteIdList);
			this.ServerIdList = serverIdList;
			this.DagIdList = dagIdList;
			this.SiteIdList = siteIdList;
			this.DataFilter = this.GetDataFilter();
			this.Filter = this.GetFilters();
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00025960 File Offset: 0x00023B60
		internal void GetServersToConnectPreferingServersSpecified(KeyValuePair<GroupOfServersKey, List<ADObjectId>> serversInDag, out List<ADObjectId> serversToConnect, out HashSet<ADObjectId> serversToInclude)
		{
			serversToConnect = serversInDag.Value;
			serversToInclude = null;
			if (this.serversToIncludeInDag.ContainsKey(serversInDag.Key))
			{
				serversToInclude = this.serversToIncludeInDag[serversInDag.Key];
				serversToConnect = new List<ADObjectId>(this.serversToIncludeInDag[serversInDag.Key]);
				foreach (ADObjectId item in serversInDag.Value)
				{
					if (!serversToInclude.Contains(item))
					{
						serversToConnect.Add(item);
					}
				}
			}
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00025A0C File Offset: 0x00023C0C
		private MultiValuedProperty<ComparisonFilter> GetDataFilter()
		{
			MultiValuedProperty<ComparisonFilter> multiValuedProperty = new MultiValuedProperty<ComparisonFilter>();
			if (!string.IsNullOrWhiteSpace(this.Destination))
			{
				multiValuedProperty.Add(new ComparisonFilter(ComparisonOperator.Equal, TransportQueueQuerySchema.NextHopDomainQueryProperty, this.Destination));
			}
			if (!string.IsNullOrWhiteSpace(this.QueueStatus))
			{
				multiValuedProperty.Add(new ComparisonFilter(ComparisonOperator.Equal, TransportQueueQuerySchema.StatusQueryProperty, this.QueueStatus));
			}
			if (!string.IsNullOrWhiteSpace(this.RiskLevel))
			{
				multiValuedProperty.Add(new ComparisonFilter(ComparisonOperator.Equal, TransportQueueQuerySchema.RiskLevelQueryProperty, this.RiskLevel));
			}
			if (this.DeliveryType != DeliveryType.Undefined)
			{
				multiValuedProperty.Add(new ComparisonFilter(ComparisonOperator.Equal, TransportQueueQuerySchema.DeliveryTypeQueryProperty, this.DeliveryType));
			}
			if (this.MessageCountGreaterThan > 0)
			{
				multiValuedProperty.Add(new ComparisonFilter(ComparisonOperator.GreaterThan, TransportQueueQuerySchema.MessageCountQueryProperty, this.MessageCountGreaterThan));
			}
			return multiValuedProperty;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00025AD8 File Offset: 0x00023CD8
		private string GetFilters()
		{
			List<string> list = new List<string>();
			if (!string.IsNullOrWhiteSpace(this.Destination))
			{
				list.Add(string.Format("NextHopDomain -eq '{0}'", this.Destination));
			}
			if (!string.IsNullOrWhiteSpace(this.QueueStatus))
			{
				list.Add(string.Format("Status -eq '{0}'", this.QueueStatus));
			}
			if (!string.IsNullOrWhiteSpace(this.RiskLevel))
			{
				list.Add(string.Format("RiskLevel -eq '{0}'", this.RiskLevel));
			}
			if (this.DeliveryType != DeliveryType.Undefined)
			{
				list.Add(string.Format("DeliveryType -eq '{0}'", this.DeliveryType.ToString()));
			}
			if (this.MessageCountGreaterThan > 0)
			{
				list.Add(string.Format("MessageCount -gt '{0}'", this.MessageCountGreaterThan));
			}
			if (this.ShouldExemptPoisonQueues)
			{
				list.Add(string.Format("NextHopDomain -ne '{0}'", "Poison Message"));
			}
			return string.Join(" -and ", list.ToArray());
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00025BD0 File Offset: 0x00023DD0
		private void GetTargetResourceGuids(out MultiValuedProperty<Guid> serverIds, out MultiValuedProperty<Guid> dagIds, out MultiValuedProperty<Guid> siteIds)
		{
			serverIds = null;
			dagIds = null;
			siteIds = null;
			if (!string.IsNullOrWhiteSpace(this.Server))
			{
				serverIds = this.GetServerIds();
			}
			else if (!string.IsNullOrWhiteSpace(this.Dag))
			{
				dagIds = this.GetDagIds();
			}
			else if (!string.IsNullOrWhiteSpace(this.Site))
			{
				siteIds = this.GetSiteIds();
			}
			else
			{
				this.ResolveForForest();
			}
			this.ServerSideResultSize = (((this.resolvedServers.Count == 1 || this.resolvedDags.Count == 1) && this.ResultSize != uint.MaxValue) ? this.ResultSize : uint.MaxValue);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00025C68 File Offset: 0x00023E68
		private MultiValuedProperty<Guid> GetServerIds()
		{
			List<string> list = string.IsNullOrWhiteSpace(this.Server) ? new List<string>() : new List<string>(this.Server.Split(new char[]
			{
				','
			}));
			foreach (string text in list)
			{
				Guid guid;
				QueryFilter filter;
				if (Guid.TryParse(text, out guid))
				{
					filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, guid);
				}
				else
				{
					filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, text);
				}
				Server[] array = this.Session.Find<Server>(null, QueryScope.SubTree, filter, null, 0);
				if (array == null || array.Length <= 0)
				{
					throw new ConfigurationErrorsException(string.Format("Failed to resolve server {0}", text));
				}
				Server server = array[0];
				this.AddServer(server);
				this.resolvedServers.Add(server.Id);
			}
			return this.GetObjectGuids(this.resolvedServers);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00025D74 File Offset: 0x00023F74
		private MultiValuedProperty<Guid> GetDagIds()
		{
			List<string> list = string.IsNullOrWhiteSpace(this.Dag) ? new List<string>() : new List<string>(this.Dag.Split(new char[]
			{
				','
			}));
			foreach (string text in list)
			{
				Guid guid;
				QueryFilter filter;
				if (Guid.TryParse(text, out guid))
				{
					filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, guid);
				}
				else
				{
					filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, text);
				}
				DatabaseAvailabilityGroup[] array = this.Session.Find<DatabaseAvailabilityGroup>(null, QueryScope.SubTree, filter, null, 0);
				if (array == null || array.Length <= 0)
				{
					throw new ConfigurationErrorsException(string.Format("Failed to resolve dag {0}", text));
				}
				DatabaseAvailabilityGroup databaseAvailabilityGroup = array[0];
				ADPagedReader<Server> adpagedReader = this.Session.FindPaged<Server>(null, QueryScope.SubTree, new AndFilter(new QueryFilter[]
				{
					new BitMaskAndFilter(ServerSchema.CurrentServerRole, 32UL),
					new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.DatabaseAvailabilityGroup, databaseAvailabilityGroup.Id),
					DiagnosticsAggregationHelper.IsE15OrHigherQueryFilter
				}), null, 0);
				foreach (Server server in adpagedReader)
				{
					this.AddServer(server);
				}
				this.resolvedDags.Add(databaseAvailabilityGroup.Id);
			}
			return this.GetObjectGuids(this.resolvedDags);
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00025F24 File Offset: 0x00024124
		private MultiValuedProperty<Guid> GetSiteIds()
		{
			List<string> list = string.IsNullOrWhiteSpace(this.Site) ? new List<string>() : new List<string>(this.Site.Split(new char[]
			{
				','
			}));
			foreach (string text in list)
			{
				Guid guid;
				QueryFilter filter;
				if (Guid.TryParse(text, out guid))
				{
					filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, guid);
				}
				else
				{
					filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, text);
				}
				ADSite[] array = this.Session.Find<ADSite>(null, QueryScope.SubTree, filter, null, 0);
				if (array == null || array.Length <= 0)
				{
					throw new ConfigurationErrorsException(string.Format("Failed to resolve site {0}", text));
				}
				ADSite adsite = array[0];
				ADPagedReader<Server> adpagedReader = this.Session.FindPaged<Server>(null, QueryScope.SubTree, new AndFilter(new QueryFilter[]
				{
					new BitMaskAndFilter(ServerSchema.CurrentServerRole, 32UL),
					new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, adsite.Id),
					DiagnosticsAggregationHelper.IsE15OrHigherQueryFilter
				}), null, 0);
				foreach (Server server in adpagedReader)
				{
					this.AddServer(server);
				}
				this.resolvedAdSites.Add(adsite.Id);
			}
			return this.GetObjectGuids(this.resolvedAdSites);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x000260D4 File Offset: 0x000242D4
		private void ResolveForForest()
		{
			ADPagedReader<Server> adpagedReader = this.Session.FindPaged<Server>(null, QueryScope.SubTree, new AndFilter(new QueryFilter[]
			{
				new BitMaskAndFilter(ServerSchema.CurrentServerRole, 32UL),
				DiagnosticsAggregationHelper.IsE15OrHigherQueryFilter
			}), null, 0);
			foreach (Server server in adpagedReader)
			{
				this.AddServer(server);
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00026154 File Offset: 0x00024354
		private MultiValuedProperty<Guid> GetObjectGuids(HashSet<ADObjectId> objectIds)
		{
			MultiValuedProperty<Guid> multiValuedProperty = new MultiValuedProperty<Guid>();
			foreach (ADObjectId adobjectId in objectIds)
			{
				multiValuedProperty.Add(adobjectId.ObjectGuid);
			}
			return multiValuedProperty;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x000261B0 File Offset: 0x000243B0
		private void AddServer(Server server)
		{
			if (server.DatabaseAvailabilityGroup != null || server.ServerSite != null)
			{
				GroupOfServersKey key = (server.DatabaseAvailabilityGroup != null) ? GroupOfServersKey.CreateFromDag(server.DatabaseAvailabilityGroup) : GroupOfServersKey.CreateFromSite(server.ServerSite, server.MajorVersion);
				if (!this.dagToServersMap.ContainsKey(key))
				{
					HashSet<ADObjectId> groupForServer = DiagnosticsAggregationHelper.GetGroupForServer(server, this.Session);
					List<ADObjectId> list = new List<ADObjectId>(groupForServer);
					RoutingUtils.ShuffleList<ADObjectId>(list);
					this.dagToServersMap[key] = list;
				}
				if (!this.serversToIncludeInDag.ContainsKey(key))
				{
					this.serversToIncludeInDag[key] = new HashSet<ADObjectId>();
				}
				this.serversToIncludeInDag[key].Add(server.Id);
				return;
			}
			this.serversNotBelongingToAnyDag.Add(server.Id);
		}

		// Token: 0x04000714 RID: 1812
		internal const string PoisonMessageQueueId = "Poison Message";

		// Token: 0x04000715 RID: 1813
		private static readonly double ADQueryIntervalMins = 60.0;

		// Token: 0x04000716 RID: 1814
		private static DateTime lastADQueryTime = DateTime.MaxValue;

		// Token: 0x04000717 RID: 1815
		private static ConcurrentDictionary<Guid, QueueConfiguration> cachedWellKnownDestinations = new ConcurrentDictionary<Guid, QueueConfiguration>();

		// Token: 0x04000718 RID: 1816
		private Dictionary<GroupOfServersKey, List<ADObjectId>> dagToServersMap = new Dictionary<GroupOfServersKey, List<ADObjectId>>();

		// Token: 0x04000719 RID: 1817
		private Dictionary<GroupOfServersKey, HashSet<ADObjectId>> serversToIncludeInDag = new Dictionary<GroupOfServersKey, HashSet<ADObjectId>>();

		// Token: 0x0400071A RID: 1818
		private HashSet<ADObjectId> serversNotBelongingToAnyDag = new HashSet<ADObjectId>();

		// Token: 0x0400071B RID: 1819
		private HashSet<ADObjectId> resolvedAdSites = new HashSet<ADObjectId>();

		// Token: 0x0400071C RID: 1820
		private HashSet<ADObjectId> resolvedDags = new HashSet<ADObjectId>();

		// Token: 0x0400071D RID: 1821
		private HashSet<ADObjectId> resolvedServers = new HashSet<ADObjectId>();

		// Token: 0x0400071E RID: 1822
		private List<string> failedToConnectServers = new List<string>();

		// Token: 0x0400071F RID: 1823
		private List<string> failedToConnectDags = new List<string>();
	}
}
