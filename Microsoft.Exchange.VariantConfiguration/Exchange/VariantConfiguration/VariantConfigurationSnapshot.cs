using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.AirSync;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.AutoDiscover;
using Microsoft.Exchange.Calendar;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Flighting;
using Microsoft.Exchange.HolidayCalendars;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.MessageDepot;
using Microsoft.Exchange.Search;
using Microsoft.Exchange.TextProcessing.Boomerang;
using Microsoft.Exchange.VariantConfiguration.Settings;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Search.Platform.Parallax;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x020000C3 RID: 195
	public class VariantConfigurationSnapshot
	{
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00012F79 File Offset: 0x00011179
		public VariantConfigurationSnapshot.ActiveMonitoringSettingsIni ActiveMonitoring
		{
			get
			{
				return new VariantConfigurationSnapshot.ActiveMonitoringSettingsIni(this);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00012F81 File Offset: 0x00011181
		public VariantConfigurationSnapshot.ActiveSyncSettingsIni ActiveSync
		{
			get
			{
				return new VariantConfigurationSnapshot.ActiveSyncSettingsIni(this);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00012F89 File Offset: 0x00011189
		public VariantConfigurationSnapshot.ADSettingsIni AD
		{
			get
			{
				return new VariantConfigurationSnapshot.ADSettingsIni(this);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00012F91 File Offset: 0x00011191
		public VariantConfigurationSnapshot.AutodiscoverSettingsIni Autodiscover
		{
			get
			{
				return new VariantConfigurationSnapshot.AutodiscoverSettingsIni(this);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00012F99 File Offset: 0x00011199
		public VariantConfigurationSnapshot.BoomerangSettingsIni Boomerang
		{
			get
			{
				return new VariantConfigurationSnapshot.BoomerangSettingsIni(this);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00012FA1 File Offset: 0x000111A1
		public VariantConfigurationSnapshot.CafeSettingsIni Cafe
		{
			get
			{
				return new VariantConfigurationSnapshot.CafeSettingsIni(this);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00012FA9 File Offset: 0x000111A9
		public VariantConfigurationSnapshot.CalendarLoggingSettingsIni CalendarLogging
		{
			get
			{
				return new VariantConfigurationSnapshot.CalendarLoggingSettingsIni(this);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00012FB1 File Offset: 0x000111B1
		public VariantConfigurationSnapshot.ClientAccessRulesCommonSettingsIni ClientAccessRulesCommon
		{
			get
			{
				return new VariantConfigurationSnapshot.ClientAccessRulesCommonSettingsIni(this);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00012FB9 File Offset: 0x000111B9
		public VariantConfigurationSnapshot.CmdletInfraSettingsIni CmdletInfra
		{
			get
			{
				return new VariantConfigurationSnapshot.CmdletInfraSettingsIni(this);
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00012FC1 File Offset: 0x000111C1
		public VariantConfigurationSnapshot.CompliancePolicySettingsIni CompliancePolicy
		{
			get
			{
				return new VariantConfigurationSnapshot.CompliancePolicySettingsIni(this);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00012FC9 File Offset: 0x000111C9
		public VariantConfigurationSnapshot.DataStorageSettingsIni DataStorage
		{
			get
			{
				return new VariantConfigurationSnapshot.DataStorageSettingsIni(this);
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00012FD1 File Offset: 0x000111D1
		public VariantConfigurationSnapshot.DiagnosticsSettingsIni Diagnostics
		{
			get
			{
				return new VariantConfigurationSnapshot.DiagnosticsSettingsIni(this);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00012FD9 File Offset: 0x000111D9
		public VariantConfigurationSnapshot.DiscoverySettingsIni Discovery
		{
			get
			{
				return new VariantConfigurationSnapshot.DiscoverySettingsIni(this);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00012FE1 File Offset: 0x000111E1
		public VariantConfigurationSnapshot.E4ESettingsIni E4E
		{
			get
			{
				return new VariantConfigurationSnapshot.E4ESettingsIni(this);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00012FE9 File Offset: 0x000111E9
		public VariantConfigurationSnapshot.EacSettingsIni Eac
		{
			get
			{
				return new VariantConfigurationSnapshot.EacSettingsIni(this);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00012FF1 File Offset: 0x000111F1
		public VariantConfigurationSnapshot.EwsSettingsIni Ews
		{
			get
			{
				return new VariantConfigurationSnapshot.EwsSettingsIni(this);
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00012FF9 File Offset: 0x000111F9
		public VariantConfigurationSnapshot.GlobalSettingsIni Global
		{
			get
			{
				return new VariantConfigurationSnapshot.GlobalSettingsIni(this);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00013001 File Offset: 0x00011201
		public VariantConfigurationSnapshot.HighAvailabilitySettingsIni HighAvailability
		{
			get
			{
				return new VariantConfigurationSnapshot.HighAvailabilitySettingsIni(this);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00013009 File Offset: 0x00011209
		public VariantConfigurationSnapshot.HolidayCalendarsSettingsIni HolidayCalendars
		{
			get
			{
				return new VariantConfigurationSnapshot.HolidayCalendarsSettingsIni(this);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00013011 File Offset: 0x00011211
		public VariantConfigurationSnapshot.HxSettingsIni Hx
		{
			get
			{
				return new VariantConfigurationSnapshot.HxSettingsIni(this);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00013019 File Offset: 0x00011219
		public VariantConfigurationSnapshot.ImapSettingsIni Imap
		{
			get
			{
				return new VariantConfigurationSnapshot.ImapSettingsIni(this);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00013021 File Offset: 0x00011221
		public VariantConfigurationSnapshot.InferenceSettingsIni Inference
		{
			get
			{
				return new VariantConfigurationSnapshot.InferenceSettingsIni(this);
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00013029 File Offset: 0x00011229
		public VariantConfigurationSnapshot.IpaedSettingsIni Ipaed
		{
			get
			{
				return new VariantConfigurationSnapshot.IpaedSettingsIni(this);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00013031 File Offset: 0x00011231
		public VariantConfigurationSnapshot.MailboxAssistantsSettingsIni MailboxAssistants
		{
			get
			{
				return new VariantConfigurationSnapshot.MailboxAssistantsSettingsIni(this);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00013039 File Offset: 0x00011239
		public VariantConfigurationSnapshot.MailboxPlansSettingsIni MailboxPlans
		{
			get
			{
				return new VariantConfigurationSnapshot.MailboxPlansSettingsIni(this);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00013041 File Offset: 0x00011241
		public VariantConfigurationSnapshot.MailboxTransportSettingsIni MailboxTransport
		{
			get
			{
				return new VariantConfigurationSnapshot.MailboxTransportSettingsIni(this);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00013049 File Offset: 0x00011249
		public VariantConfigurationSnapshot.MalwareAgentSettingsIni MalwareAgent
		{
			get
			{
				return new VariantConfigurationSnapshot.MalwareAgentSettingsIni(this);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00013051 File Offset: 0x00011251
		public VariantConfigurationSnapshot.MessageTrackingSettingsIni MessageTracking
		{
			get
			{
				return new VariantConfigurationSnapshot.MessageTrackingSettingsIni(this);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00013059 File Offset: 0x00011259
		public VariantConfigurationSnapshot.MexAgentsSettingsIni MexAgents
		{
			get
			{
				return new VariantConfigurationSnapshot.MexAgentsSettingsIni(this);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00013061 File Offset: 0x00011261
		public VariantConfigurationSnapshot.MrsSettingsIni Mrs
		{
			get
			{
				return new VariantConfigurationSnapshot.MrsSettingsIni(this);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00013069 File Offset: 0x00011269
		public VariantConfigurationSnapshot.NotificationBrokerServiceSettingsIni NotificationBrokerService
		{
			get
			{
				return new VariantConfigurationSnapshot.NotificationBrokerServiceSettingsIni(this);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00013071 File Offset: 0x00011271
		public VariantConfigurationSnapshot.OABSettingsIni OAB
		{
			get
			{
				return new VariantConfigurationSnapshot.OABSettingsIni(this);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00013079 File Offset: 0x00011279
		public VariantConfigurationSnapshot.OfficeGraphSettingsIni OfficeGraph
		{
			get
			{
				return new VariantConfigurationSnapshot.OfficeGraphSettingsIni(this);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00013081 File Offset: 0x00011281
		public VariantConfigurationSnapshot.OwaClientSettingsIni OwaClient
		{
			get
			{
				return new VariantConfigurationSnapshot.OwaClientSettingsIni(this);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00013089 File Offset: 0x00011289
		public VariantConfigurationSnapshot.OwaClientServerSettingsIni OwaClientServer
		{
			get
			{
				return new VariantConfigurationSnapshot.OwaClientServerSettingsIni(this);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00013091 File Offset: 0x00011291
		public VariantConfigurationSnapshot.OwaServerSettingsIni OwaServer
		{
			get
			{
				return new VariantConfigurationSnapshot.OwaServerSettingsIni(this);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00013099 File Offset: 0x00011299
		public VariantConfigurationSnapshot.OwaDeploymentSettingsIni OwaDeployment
		{
			get
			{
				return new VariantConfigurationSnapshot.OwaDeploymentSettingsIni(this);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x000130A1 File Offset: 0x000112A1
		public VariantConfigurationSnapshot.PopSettingsIni Pop
		{
			get
			{
				return new VariantConfigurationSnapshot.PopSettingsIni(this);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x000130A9 File Offset: 0x000112A9
		public VariantConfigurationSnapshot.RpcClientAccessSettingsIni RpcClientAccess
		{
			get
			{
				return new VariantConfigurationSnapshot.RpcClientAccessSettingsIni(this);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x000130B1 File Offset: 0x000112B1
		public VariantConfigurationSnapshot.SearchSettingsIni Search
		{
			get
			{
				return new VariantConfigurationSnapshot.SearchSettingsIni(this);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x000130B9 File Offset: 0x000112B9
		public VariantConfigurationSnapshot.SharedCacheSettingsIni SharedCache
		{
			get
			{
				return new VariantConfigurationSnapshot.SharedCacheSettingsIni(this);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x000130C1 File Offset: 0x000112C1
		public VariantConfigurationSnapshot.SharedMailboxSettingsIni SharedMailbox
		{
			get
			{
				return new VariantConfigurationSnapshot.SharedMailboxSettingsIni(this);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x000130C9 File Offset: 0x000112C9
		public VariantConfigurationSnapshot.TestSettingsIni Test
		{
			get
			{
				return new VariantConfigurationSnapshot.TestSettingsIni(this);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x000130D1 File Offset: 0x000112D1
		public VariantConfigurationSnapshot.Test2SettingsIni Test2
		{
			get
			{
				return new VariantConfigurationSnapshot.Test2SettingsIni(this);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000130D9 File Offset: 0x000112D9
		public VariantConfigurationSnapshot.TransportSettingsIni Transport
		{
			get
			{
				return new VariantConfigurationSnapshot.TransportSettingsIni(this);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x000130E1 File Offset: 0x000112E1
		public VariantConfigurationSnapshot.UCCSettingsIni UCC
		{
			get
			{
				return new VariantConfigurationSnapshot.UCCSettingsIni(this);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x000130E9 File Offset: 0x000112E9
		public VariantConfigurationSnapshot.UMSettingsIni UM
		{
			get
			{
				return new VariantConfigurationSnapshot.UMSettingsIni(this);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x000130F1 File Offset: 0x000112F1
		public VariantConfigurationSnapshot.VariantConfigSettingsIni VariantConfig
		{
			get
			{
				return new VariantConfigurationSnapshot.VariantConfigSettingsIni(this);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x000130F9 File Offset: 0x000112F9
		public VariantConfigurationSnapshot.WorkingSetSettingsIni WorkingSet
		{
			get
			{
				return new VariantConfigurationSnapshot.WorkingSetSettingsIni(this);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00013101 File Offset: 0x00011301
		public VariantConfigurationSnapshot.WorkloadManagementSettingsIni WorkloadManagement
		{
			get
			{
				return new VariantConfigurationSnapshot.WorkloadManagementSettingsIni(this);
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00013109 File Offset: 0x00011309
		internal VariantConfigurationSnapshot(VariantObjectStore store, int rotationHash, string rampId, bool evaluateFlights, VariantConfigurationSnapshotProvider snapshotProvider)
		{
			this.store = store;
			this.rotationHash = rotationHash;
			this.rampId = rampId;
			this.snapshotProvider = snapshotProvider;
			this.evaluateFlights = evaluateFlights;
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00013141 File Offset: 0x00011341
		public KeyValuePair<string, string>[] Constraints
		{
			get
			{
				return this.store.DefaultContext.GetVariantFilters();
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00013180 File Offset: 0x00011380
		public string[] Flights
		{
			get
			{
				return (from pair in this.Constraints
				where pair.Key.StartsWith("flt.", StringComparison.OrdinalIgnoreCase)
				select pair.Key.Substring("flt.".Length)).ToArray<string>();
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000131DC File Offset: 0x000113DC
		public T GetObject<T>(string config, string section) where T : class, ISettings
		{
			if (string.IsNullOrEmpty(config))
			{
				throw new ArgumentNullException("config");
			}
			if (string.IsNullOrEmpty(section))
			{
				throw new ArgumentNullException("section");
			}
			if (VariantConfiguration.TestOverride != null)
			{
				ISettings settings = VariantConfiguration.TestOverride(config, section);
				if (settings != null)
				{
					return (T)((object)settings);
				}
			}
			this.LoadDataSourceAndCreateNewSnapshot(config);
			T @object;
			try
			{
				@object = this.store.GetResolvedObjectProvider(config).GetObject<T>(section);
			}
			catch (DataSourceNotFoundException)
			{
				throw new KeyNotFoundException(config + " could not be found.");
			}
			return @object;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001326C File Offset: 0x0001146C
		public T GetObject<T>(string config, object id1, params object[] ids) where T : class, ISettings
		{
			if (string.IsNullOrEmpty(config))
			{
				throw new ArgumentNullException("config");
			}
			if (id1 == null)
			{
				throw new ArgumentNullException("id1");
			}
			string text = id1.ToString();
			if (ids != null && ids.Length > 0)
			{
				text = text + "-" + string.Join("-", ids);
			}
			return this.GetObject<T>(config, text);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000132CC File Offset: 0x000114CC
		public IDictionary<string, T> GetObjectsOfType<T>(string config) where T : class, ISettings
		{
			if (string.IsNullOrEmpty(config))
			{
				throw new ArgumentNullException("config");
			}
			this.LoadDataSourceAndCreateNewSnapshot(config);
			IDictionary<string, T> objectsOfType;
			try
			{
				objectsOfType = this.store.GetResolvedObjectProvider(config).GetObjectsOfType<T>();
			}
			catch (DataSourceNotFoundException)
			{
				throw new KeyNotFoundException(config + " could not be found.");
			}
			return objectsOfType;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001332C File Offset: 0x0001152C
		private void LoadDataSourceAndCreateNewSnapshot(string config)
		{
			if (this.store.DataSourceNames.Contains(config))
			{
				return;
			}
			lock (this.storeLock)
			{
				if (!this.store.DataSourceNames.Contains(config))
				{
					string[] array = new string[]
					{
						config
					};
					this.snapshotProvider.DataLoader.LoadIfNotLoaded(array);
					VariantObjectStore variantObjectStore = this.store;
					VariantObjectStore currentSnapshot = this.snapshotProvider.Container.GetCurrentSnapshot();
					currentSnapshot.DefaultContext.InitializeFrom(variantObjectStore.DefaultContext);
					if (this.evaluateFlights)
					{
						this.snapshotProvider.AddFlightsToStoreContext(currentSnapshot, array, this.rotationHash, this.rampId);
					}
					this.store = currentSnapshot;
				}
			}
		}

		// Token: 0x04000448 RID: 1096
		private const string SectionIdSeparator = "-";

		// Token: 0x04000449 RID: 1097
		private const string FlightPrefix = "flt.";

		// Token: 0x0400044A RID: 1098
		private readonly VariantConfigurationSnapshotProvider snapshotProvider;

		// Token: 0x0400044B RID: 1099
		private readonly object storeLock = new object();

		// Token: 0x0400044C RID: 1100
		private readonly int rotationHash;

		// Token: 0x0400044D RID: 1101
		private readonly string rampId;

		// Token: 0x0400044E RID: 1102
		private readonly bool evaluateFlights;

		// Token: 0x0400044F RID: 1103
		private VariantObjectStore store;

		// Token: 0x020000C4 RID: 196
		public struct ActiveMonitoringSettingsIni
		{
			// Token: 0x06000696 RID: 1686 RVA: 0x00013404 File Offset: 0x00011604
			internal ActiveMonitoringSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000697 RID: 1687 RVA: 0x0001340D File Offset: 0x0001160D
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("ActiveMonitoring.settings.ini");
			}

			// Token: 0x06000698 RID: 1688 RVA: 0x0001341F File Offset: 0x0001161F
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("ActiveMonitoring.settings.ini", id);
			}

			// Token: 0x06000699 RID: 1689 RVA: 0x00013432 File Offset: 0x00011632
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("ActiveMonitoring.settings.ini", id1, ids);
			}

			// Token: 0x17000470 RID: 1136
			// (get) Token: 0x0600069A RID: 1690 RVA: 0x00013446 File Offset: 0x00011646
			public IFeature ProcessIsolationResetIISAppPoolResponder
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "ProcessIsolationResetIISAppPoolResponder");
				}
			}

			// Token: 0x17000471 RID: 1137
			// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001345D File Offset: 0x0001165D
			public IFeature WatsonResponder
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "WatsonResponder");
				}
			}

			// Token: 0x17000472 RID: 1138
			// (get) Token: 0x0600069C RID: 1692 RVA: 0x00013474 File Offset: 0x00011674
			public IFeature DirectoryAccessor
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "DirectoryAccessor");
				}
			}

			// Token: 0x17000473 RID: 1139
			// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001348B File Offset: 0x0001168B
			public IFeature GetExchangeDiagnosticsInfoResponder
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "GetExchangeDiagnosticsInfoResponder");
				}
			}

			// Token: 0x17000474 RID: 1140
			// (get) Token: 0x0600069E RID: 1694 RVA: 0x000134A2 File Offset: 0x000116A2
			public IFeature PushNotificationsDiscoveryMbx
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "PushNotificationsDiscoveryMbx");
				}
			}

			// Token: 0x17000475 RID: 1141
			// (get) Token: 0x0600069F RID: 1695 RVA: 0x000134B9 File Offset: 0x000116B9
			public IFeature EscalateResponder
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "EscalateResponder");
				}
			}

			// Token: 0x17000476 RID: 1142
			// (get) Token: 0x060006A0 RID: 1696 RVA: 0x000134D0 File Offset: 0x000116D0
			public IFeature CafeOfflineRespondersUseClientAccessArray
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "CafeOfflineRespondersUseClientAccessArray");
				}
			}

			// Token: 0x17000477 RID: 1143
			// (get) Token: 0x060006A1 RID: 1697 RVA: 0x000134E7 File Offset: 0x000116E7
			public IFeature PopImapDiscoveryCommon
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "PopImapDiscoveryCommon");
				}
			}

			// Token: 0x17000478 RID: 1144
			// (get) Token: 0x060006A2 RID: 1698 RVA: 0x000134FE File Offset: 0x000116FE
			public IFeature TraceLogResponder
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "TraceLogResponder");
				}
			}

			// Token: 0x17000479 RID: 1145
			// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00013515 File Offset: 0x00011715
			public IFeature AllowBasicAuthForOutsideInMonitoringMailboxes
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "AllowBasicAuthForOutsideInMonitoringMailboxes");
				}
			}

			// Token: 0x1700047A RID: 1146
			// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0001352C File Offset: 0x0001172C
			public IFeature ActiveSyncDiscovery
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "ActiveSyncDiscovery");
				}
			}

			// Token: 0x1700047B RID: 1147
			// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00013543 File Offset: 0x00011743
			public IFeature ClearLsassCacheResponder
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "ClearLsassCacheResponder");
				}
			}

			// Token: 0x1700047C RID: 1148
			// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0001355A File Offset: 0x0001175A
			public IFeature ProcessIsolationRestartServiceResponder
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "ProcessIsolationRestartServiceResponder");
				}
			}

			// Token: 0x1700047D RID: 1149
			// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00013571 File Offset: 0x00011771
			public IFeature SubjectMaintenance
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "SubjectMaintenance");
				}
			}

			// Token: 0x1700047E RID: 1150
			// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00013588 File Offset: 0x00011788
			public IFeature LocalEndpointManager
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "LocalEndpointManager");
				}
			}

			// Token: 0x1700047F RID: 1151
			// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001359F File Offset: 0x0001179F
			public IFeature F1TraceResponder
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "F1TraceResponder");
				}
			}

			// Token: 0x17000480 RID: 1152
			// (get) Token: 0x060006AA RID: 1706 RVA: 0x000135B6 File Offset: 0x000117B6
			public IFeature RpcProbe
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "RpcProbe");
				}
			}

			// Token: 0x17000481 RID: 1153
			// (get) Token: 0x060006AB RID: 1707 RVA: 0x000135CD File Offset: 0x000117CD
			public IFeature PushNotificationsDiscoveryCafe
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "PushNotificationsDiscoveryCafe");
				}
			}

			// Token: 0x17000482 RID: 1154
			// (get) Token: 0x060006AC RID: 1708 RVA: 0x000135E4 File Offset: 0x000117E4
			public IFeature AutoDiscoverExternalUrlVerification
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveMonitoring.settings.ini", "AutoDiscoverExternalUrlVerification");
				}
			}

			// Token: 0x17000483 RID: 1155
			// (get) Token: 0x060006AD RID: 1709 RVA: 0x000135FB File Offset: 0x000117FB
			public ICmdletSettings PinMonitoringMailboxesToDatabases
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("ActiveMonitoring.settings.ini", "PinMonitoringMailboxesToDatabases");
				}
			}

			// Token: 0x04000452 RID: 1106
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000C5 RID: 197
		public struct ActiveSyncSettingsIni
		{
			// Token: 0x060006AE RID: 1710 RVA: 0x00013612 File Offset: 0x00011812
			internal ActiveSyncSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060006AF RID: 1711 RVA: 0x0001361B File Offset: 0x0001181B
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("ActiveSync.settings.ini");
			}

			// Token: 0x060006B0 RID: 1712 RVA: 0x0001362D File Offset: 0x0001182D
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("ActiveSync.settings.ini", id);
			}

			// Token: 0x060006B1 RID: 1713 RVA: 0x00013640 File Offset: 0x00011840
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("ActiveSync.settings.ini", id1, ids);
			}

			// Token: 0x17000484 RID: 1156
			// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00013654 File Offset: 0x00011854
			public IFeature SyncStateOnDirectItems
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "SyncStateOnDirectItems");
				}
			}

			// Token: 0x17000485 RID: 1157
			// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001366B File Offset: 0x0001186B
			public IMdmSupportedPlatformsSettings MdmSupportedPlatforms
			{
				get
				{
					return this.snapshot.GetObject<IMdmSupportedPlatformsSettings>("ActiveSync.settings.ini", "MdmSupportedPlatforms");
				}
			}

			// Token: 0x17000486 RID: 1158
			// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00013682 File Offset: 0x00011882
			public IFeature GlobalCriminalCompliance
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "GlobalCriminalCompliance");
				}
			}

			// Token: 0x17000487 RID: 1159
			// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00013699 File Offset: 0x00011899
			public IFeature ConsumerOrganizationUser
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "ConsumerOrganizationUser");
				}
			}

			// Token: 0x17000488 RID: 1160
			// (get) Token: 0x060006B6 RID: 1718 RVA: 0x000136B0 File Offset: 0x000118B0
			public IFeature HDPhotos
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "HDPhotos");
				}
			}

			// Token: 0x17000489 RID: 1161
			// (get) Token: 0x060006B7 RID: 1719 RVA: 0x000136C7 File Offset: 0x000118C7
			public IFeature MailboxLoggingVerboseMode
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "MailboxLoggingVerboseMode");
				}
			}

			// Token: 0x1700048A RID: 1162
			// (get) Token: 0x060006B8 RID: 1720 RVA: 0x000136DE File Offset: 0x000118DE
			public IFeature ActiveSyncClientAccessRulesEnabled
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "ActiveSyncClientAccessRulesEnabled");
				}
			}

			// Token: 0x1700048B RID: 1163
			// (get) Token: 0x060006B9 RID: 1721 RVA: 0x000136F5 File Offset: 0x000118F5
			public IFeature ForceSingleNameSpaceUsage
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "ForceSingleNameSpaceUsage");
				}
			}

			// Token: 0x1700048C RID: 1164
			// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001370C File Offset: 0x0001190C
			public IMdmNotificationSettings MdmNotification
			{
				get
				{
					return this.snapshot.GetObject<IMdmNotificationSettings>("ActiveSync.settings.ini", "MdmNotification");
				}
			}

			// Token: 0x1700048D RID: 1165
			// (get) Token: 0x060006BB RID: 1723 RVA: 0x00013723 File Offset: 0x00011923
			public IFeature ActiveSyncDiagnosticsLogABQPeriodicEvent
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "ActiveSyncDiagnosticsLogABQPeriodicEvent");
				}
			}

			// Token: 0x1700048E RID: 1166
			// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001373A File Offset: 0x0001193A
			public IFeature RedirectForOnBoarding
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "RedirectForOnBoarding");
				}
			}

			// Token: 0x1700048F RID: 1167
			// (get) Token: 0x060006BD RID: 1725 RVA: 0x00013751 File Offset: 0x00011951
			public IFeature CloudMdmEnrolled
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "CloudMdmEnrolled");
				}
			}

			// Token: 0x17000490 RID: 1168
			// (get) Token: 0x060006BE RID: 1726 RVA: 0x00013768 File Offset: 0x00011968
			public IFeature UseOAuthMasterSidForSecurityContext
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "UseOAuthMasterSidForSecurityContext");
				}
			}

			// Token: 0x17000491 RID: 1169
			// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001377F File Offset: 0x0001197F
			public IFeature EnableV160
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "EnableV160");
				}
			}

			// Token: 0x17000492 RID: 1170
			// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00013796 File Offset: 0x00011996
			public IFeature EasPartialIcsSync
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "EasPartialIcsSync");
				}
			}

			// Token: 0x17000493 RID: 1171
			// (get) Token: 0x060006C1 RID: 1729 RVA: 0x000137AD File Offset: 0x000119AD
			public IFeature DisableCharsetDetectionInCopyMessageContents
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "DisableCharsetDetectionInCopyMessageContents");
				}
			}

			// Token: 0x17000494 RID: 1172
			// (get) Token: 0x060006C2 RID: 1730 RVA: 0x000137C4 File Offset: 0x000119C4
			public IFeature GetGoidFromCalendarItemForMeetingResponse
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "GetGoidFromCalendarItemForMeetingResponse");
				}
			}

			// Token: 0x17000495 RID: 1173
			// (get) Token: 0x060006C3 RID: 1731 RVA: 0x000137DB File Offset: 0x000119DB
			public IFeature SyncStatusOnGlobalInfo
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ActiveSync.settings.ini", "SyncStatusOnGlobalInfo");
				}
			}

			// Token: 0x04000453 RID: 1107
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000C6 RID: 198
		public struct ADSettingsIni
		{
			// Token: 0x060006C4 RID: 1732 RVA: 0x000137F2 File Offset: 0x000119F2
			internal ADSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060006C5 RID: 1733 RVA: 0x000137FB File Offset: 0x000119FB
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("AD.settings.ini");
			}

			// Token: 0x060006C6 RID: 1734 RVA: 0x0001380D File Offset: 0x00011A0D
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("AD.settings.ini", id);
			}

			// Token: 0x060006C7 RID: 1735 RVA: 0x00013820 File Offset: 0x00011A20
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("AD.settings.ini", id1, ids);
			}

			// Token: 0x17000496 RID: 1174
			// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00013834 File Offset: 0x00011A34
			public IDelegatedSetupRoleGroupSettings DelegatedSetupRoleGroupValue
			{
				get
				{
					return this.snapshot.GetObject<IDelegatedSetupRoleGroupSettings>("AD.settings.ini", "DelegatedSetupRoleGroupValue");
				}
			}

			// Token: 0x17000497 RID: 1175
			// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001384B File Offset: 0x00011A4B
			public IFeature DisplayNameMustContainReadableCharacter
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("AD.settings.ini", "DisplayNameMustContainReadableCharacter");
				}
			}

			// Token: 0x17000498 RID: 1176
			// (get) Token: 0x060006CA RID: 1738 RVA: 0x00013862 File Offset: 0x00011A62
			public IFeature MailboxLocations
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("AD.settings.ini", "MailboxLocations");
				}
			}

			// Token: 0x17000499 RID: 1177
			// (get) Token: 0x060006CB RID: 1739 RVA: 0x00013879 File Offset: 0x00011A79
			public IFeature EnableUseIsDescendantOfForRecipientViewRoot
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("AD.settings.ini", "EnableUseIsDescendantOfForRecipientViewRoot");
				}
			}

			// Token: 0x1700049A RID: 1178
			// (get) Token: 0x060006CC RID: 1740 RVA: 0x00013890 File Offset: 0x00011A90
			public IFeature UseGlobalCatalogIsSetToFalse
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("AD.settings.ini", "UseGlobalCatalogIsSetToFalse");
				}
			}

			// Token: 0x04000454 RID: 1108
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000C7 RID: 199
		public struct AutodiscoverSettingsIni
		{
			// Token: 0x060006CD RID: 1741 RVA: 0x000138A7 File Offset: 0x00011AA7
			internal AutodiscoverSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060006CE RID: 1742 RVA: 0x000138B0 File Offset: 0x00011AB0
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Autodiscover.settings.ini");
			}

			// Token: 0x060006CF RID: 1743 RVA: 0x000138C2 File Offset: 0x00011AC2
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Autodiscover.settings.ini", id);
			}

			// Token: 0x060006D0 RID: 1744 RVA: 0x000138D5 File Offset: 0x00011AD5
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Autodiscover.settings.ini", id1, ids);
			}

			// Token: 0x1700049B RID: 1179
			// (get) Token: 0x060006D1 RID: 1745 RVA: 0x000138E9 File Offset: 0x00011AE9
			public IFeature AnonymousAuthentication
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "AnonymousAuthentication");
				}
			}

			// Token: 0x1700049C RID: 1180
			// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00013900 File Offset: 0x00011B00
			public IFeature EnableMobileSyncRedirectBypass
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "EnableMobileSyncRedirectBypass");
				}
			}

			// Token: 0x1700049D RID: 1181
			// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00013917 File Offset: 0x00011B17
			public IFeature ParseBinarySecretHeader
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "ParseBinarySecretHeader");
				}
			}

			// Token: 0x1700049E RID: 1182
			// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001392E File Offset: 0x00011B2E
			public IFeature SkipServiceTopologyDiscovery
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "SkipServiceTopologyDiscovery");
				}
			}

			// Token: 0x1700049F RID: 1183
			// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00013945 File Offset: 0x00011B45
			public IFeature StreamInsightUploader
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "StreamInsightUploader");
				}
			}

			// Token: 0x170004A0 RID: 1184
			// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001395C File Offset: 0x00011B5C
			public IFeature LoadNegoExSspNames
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "LoadNegoExSspNames");
				}
			}

			// Token: 0x170004A1 RID: 1185
			// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00013973 File Offset: 0x00011B73
			public IFeature NoADLookupForUser
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "NoADLookupForUser");
				}
			}

			// Token: 0x170004A2 RID: 1186
			// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001398A File Offset: 0x00011B8A
			public IFeature NoCrossForestDiscover
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "NoCrossForestDiscover");
				}
			}

			// Token: 0x170004A3 RID: 1187
			// (get) Token: 0x060006D9 RID: 1753 RVA: 0x000139A1 File Offset: 0x00011BA1
			public IFeature EcpInternalExternalUrl
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "EcpInternalExternalUrl");
				}
			}

			// Token: 0x170004A4 RID: 1188
			// (get) Token: 0x060006DA RID: 1754 RVA: 0x000139B8 File Offset: 0x00011BB8
			public IFeature MapiHttpForOutlook14
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "MapiHttpForOutlook14");
				}
			}

			// Token: 0x170004A5 RID: 1189
			// (get) Token: 0x060006DB RID: 1755 RVA: 0x000139CF File Offset: 0x00011BCF
			public IOWAUrl OWAUrl
			{
				get
				{
					return this.snapshot.GetObject<IOWAUrl>("Autodiscover.settings.ini", "OWAUrl");
				}
			}

			// Token: 0x170004A6 RID: 1190
			// (get) Token: 0x060006DC RID: 1756 RVA: 0x000139E6 File Offset: 0x00011BE6
			public IFeature AccountInCloud
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "AccountInCloud");
				}
			}

			// Token: 0x170004A7 RID: 1191
			// (get) Token: 0x060006DD RID: 1757 RVA: 0x000139FD File Offset: 0x00011BFD
			public IFeature ConfigurePerformanceCounters
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "ConfigurePerformanceCounters");
				}
			}

			// Token: 0x170004A8 RID: 1192
			// (get) Token: 0x060006DE RID: 1758 RVA: 0x00013A14 File Offset: 0x00011C14
			public IFeature RedirectOutlookClient
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "RedirectOutlookClient");
				}
			}

			// Token: 0x170004A9 RID: 1193
			// (get) Token: 0x060006DF RID: 1759 RVA: 0x00013A2B File Offset: 0x00011C2B
			public IFeature WsSecurityEndpoint
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "WsSecurityEndpoint");
				}
			}

			// Token: 0x170004AA RID: 1194
			// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00013A42 File Offset: 0x00011C42
			public IFeature UseMapiHttpADSetting
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "UseMapiHttpADSetting");
				}
			}

			// Token: 0x170004AB RID: 1195
			// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00013A59 File Offset: 0x00011C59
			public IFeature NoAuthenticationTokenToNego
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "NoAuthenticationTokenToNego");
				}
			}

			// Token: 0x170004AC RID: 1196
			// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00013A70 File Offset: 0x00011C70
			public IFeature RestrictedSettings
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "RestrictedSettings");
				}
			}

			// Token: 0x170004AD RID: 1197
			// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00013A87 File Offset: 0x00011C87
			public IFeature MapiHttp
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "MapiHttp");
				}
			}

			// Token: 0x170004AE RID: 1198
			// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00013A9E File Offset: 0x00011C9E
			public IFeature LogonViaStandardTokens
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Autodiscover.settings.ini", "LogonViaStandardTokens");
				}
			}

			// Token: 0x04000455 RID: 1109
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000C8 RID: 200
		public struct BoomerangSettingsIni
		{
			// Token: 0x060006E5 RID: 1765 RVA: 0x00013AB5 File Offset: 0x00011CB5
			internal BoomerangSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060006E6 RID: 1766 RVA: 0x00013ABE File Offset: 0x00011CBE
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Boomerang.settings.ini");
			}

			// Token: 0x060006E7 RID: 1767 RVA: 0x00013AD0 File Offset: 0x00011CD0
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Boomerang.settings.ini", id);
			}

			// Token: 0x060006E8 RID: 1768 RVA: 0x00013AE3 File Offset: 0x00011CE3
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Boomerang.settings.ini", id1, ids);
			}

			// Token: 0x170004AF RID: 1199
			// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00013AF7 File Offset: 0x00011CF7
			public IBoomerangSettings BoomerangSettings
			{
				get
				{
					return this.snapshot.GetObject<IBoomerangSettings>("Boomerang.settings.ini", "BoomerangSettings");
				}
			}

			// Token: 0x170004B0 RID: 1200
			// (get) Token: 0x060006EA RID: 1770 RVA: 0x00013B0E File Offset: 0x00011D0E
			public IFeature BoomerangMessageId
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Boomerang.settings.ini", "BoomerangMessageId");
				}
			}

			// Token: 0x04000456 RID: 1110
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000C9 RID: 201
		public struct CafeSettingsIni
		{
			// Token: 0x060006EB RID: 1771 RVA: 0x00013B25 File Offset: 0x00011D25
			internal CafeSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060006EC RID: 1772 RVA: 0x00013B2E File Offset: 0x00011D2E
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Cafe.settings.ini");
			}

			// Token: 0x060006ED RID: 1773 RVA: 0x00013B40 File Offset: 0x00011D40
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Cafe.settings.ini", id);
			}

			// Token: 0x060006EE RID: 1774 RVA: 0x00013B53 File Offset: 0x00011D53
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Cafe.settings.ini", id1, ids);
			}

			// Token: 0x170004B1 RID: 1201
			// (get) Token: 0x060006EF RID: 1775 RVA: 0x00013B67 File Offset: 0x00011D67
			public IFeature CheckServerOnlineForActiveServer
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "CheckServerOnlineForActiveServer");
				}
			}

			// Token: 0x170004B2 RID: 1202
			// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00013B7E File Offset: 0x00011D7E
			public IFeature ExplicitDomain
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "ExplicitDomain");
				}
			}

			// Token: 0x170004B3 RID: 1203
			// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00013B95 File Offset: 0x00011D95
			public IFeature UseExternalPopIMapSettings
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "UseExternalPopIMapSettings");
				}
			}

			// Token: 0x170004B4 RID: 1204
			// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00013BAC File Offset: 0x00011DAC
			public IFeature NoServiceTopologyTryGetServerVersion
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "NoServiceTopologyTryGetServerVersion");
				}
			}

			// Token: 0x170004B5 RID: 1205
			// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00013BC3 File Offset: 0x00011DC3
			public IFeature NoFormBasedAuthentication
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "NoFormBasedAuthentication");
				}
			}

			// Token: 0x170004B6 RID: 1206
			// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00013BDA File Offset: 0x00011DDA
			public IFeature RUMUseADCache
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "RUMUseADCache");
				}
			}

			// Token: 0x170004B7 RID: 1207
			// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00013BF1 File Offset: 0x00011DF1
			public IFeature PartitionedRouting
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "PartitionedRouting");
				}
			}

			// Token: 0x170004B8 RID: 1208
			// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00013C08 File Offset: 0x00011E08
			public IFeature DownLevelServerPing
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "DownLevelServerPing");
				}
			}

			// Token: 0x170004B9 RID: 1209
			// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00013C1F File Offset: 0x00011E1F
			public IFeature UseResourceForest
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "UseResourceForest");
				}
			}

			// Token: 0x170004BA RID: 1210
			// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00013C36 File Offset: 0x00011E36
			public IFeature TrustClientXForwardedFor
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "TrustClientXForwardedFor");
				}
			}

			// Token: 0x170004BB RID: 1211
			// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00013C4D File Offset: 0x00011E4D
			public IFeature MailboxServerSharedCache
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "MailboxServerSharedCache");
				}
			}

			// Token: 0x170004BC RID: 1212
			// (get) Token: 0x060006FA RID: 1786 RVA: 0x00013C64 File Offset: 0x00011E64
			public IFeature LoadBalancedPartnerRouting
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "LoadBalancedPartnerRouting");
				}
			}

			// Token: 0x170004BD RID: 1213
			// (get) Token: 0x060006FB RID: 1787 RVA: 0x00013C7B File Offset: 0x00011E7B
			public IFeature CompositeIdentity
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "CompositeIdentity");
				}
			}

			// Token: 0x170004BE RID: 1214
			// (get) Token: 0x060006FC RID: 1788 RVA: 0x00013C92 File Offset: 0x00011E92
			public IFeature CafeV2
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "CafeV2");
				}
			}

			// Token: 0x170004BF RID: 1215
			// (get) Token: 0x060006FD RID: 1789 RVA: 0x00013CA9 File Offset: 0x00011EA9
			public IFeature RetryOnError
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "RetryOnError");
				}
			}

			// Token: 0x170004C0 RID: 1216
			// (get) Token: 0x060006FE RID: 1790 RVA: 0x00013CC0 File Offset: 0x00011EC0
			public IFeature PreferServersCacheForRandomBackEnd
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "PreferServersCacheForRandomBackEnd");
				}
			}

			// Token: 0x170004C1 RID: 1217
			// (get) Token: 0x060006FF RID: 1791 RVA: 0x00013CD7 File Offset: 0x00011ED7
			public IFeature AnchorMailboxSharedCache
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "AnchorMailboxSharedCache");
				}
			}

			// Token: 0x170004C2 RID: 1218
			// (get) Token: 0x06000700 RID: 1792 RVA: 0x00013CEE File Offset: 0x00011EEE
			public IFeature CafeV1RUM
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "CafeV1RUM");
				}
			}

			// Token: 0x170004C3 RID: 1219
			// (get) Token: 0x06000701 RID: 1793 RVA: 0x00013D05 File Offset: 0x00011F05
			public IFeature DebugResponseHeaders
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "DebugResponseHeaders");
				}
			}

			// Token: 0x170004C4 RID: 1220
			// (get) Token: 0x06000702 RID: 1794 RVA: 0x00013D1C File Offset: 0x00011F1C
			public IFeature SyndicatedAdmin
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "SyndicatedAdmin");
				}
			}

			// Token: 0x170004C5 RID: 1221
			// (get) Token: 0x06000703 RID: 1795 RVA: 0x00013D33 File Offset: 0x00011F33
			public IFeature EnableTls11
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "EnableTls11");
				}
			}

			// Token: 0x170004C6 RID: 1222
			// (get) Token: 0x06000704 RID: 1796 RVA: 0x00013D4A File Offset: 0x00011F4A
			public IFeature ConfigurePerformanceCounters
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "ConfigurePerformanceCounters");
				}
			}

			// Token: 0x170004C7 RID: 1223
			// (get) Token: 0x06000705 RID: 1797 RVA: 0x00013D61 File Offset: 0x00011F61
			public IFeature EnableTls12
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "EnableTls12");
				}
			}

			// Token: 0x170004C8 RID: 1224
			// (get) Token: 0x06000706 RID: 1798 RVA: 0x00013D78 File Offset: 0x00011F78
			public IFeature ServersCache
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "ServersCache");
				}
			}

			// Token: 0x170004C9 RID: 1225
			// (get) Token: 0x06000707 RID: 1799 RVA: 0x00013D8F File Offset: 0x00011F8F
			public IFeature NoCrossForestServerLocate
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "NoCrossForestServerLocate");
				}
			}

			// Token: 0x170004CA RID: 1226
			// (get) Token: 0x06000708 RID: 1800 RVA: 0x00013DA6 File Offset: 0x00011FA6
			public IFeature SiteNameFromServerFqdnTranslation
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "SiteNameFromServerFqdnTranslation");
				}
			}

			// Token: 0x170004CB RID: 1227
			// (get) Token: 0x06000709 RID: 1801 RVA: 0x00013DBD File Offset: 0x00011FBD
			public IFeature CacheLocalSiteLiveE15Servers
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "CacheLocalSiteLiveE15Servers");
				}
			}

			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x0600070A RID: 1802 RVA: 0x00013DD4 File Offset: 0x00011FD4
			public IFeature EnforceConcurrencyGuards
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "EnforceConcurrencyGuards");
				}
			}

			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x0600070B RID: 1803 RVA: 0x00013DEB File Offset: 0x00011FEB
			public IFeature NoVDirLocationHint
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "NoVDirLocationHint");
				}
			}

			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x0600070C RID: 1804 RVA: 0x00013E02 File Offset: 0x00012002
			public IFeature NoCrossSiteRedirect
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "NoCrossSiteRedirect");
				}
			}

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x0600070D RID: 1805 RVA: 0x00013E19 File Offset: 0x00012019
			public IFeature CheckServerLocatorServersForMaintenanceMode
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "CheckServerLocatorServersForMaintenanceMode");
				}
			}

			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x0600070E RID: 1806 RVA: 0x00013E30 File Offset: 0x00012030
			public IFeature UseExchClientVerInRPS
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "UseExchClientVerInRPS");
				}
			}

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x0600070F RID: 1807 RVA: 0x00013E47 File Offset: 0x00012047
			public IFeature RUMLegacyRoutingEntry
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Cafe.settings.ini", "RUMLegacyRoutingEntry");
				}
			}

			// Token: 0x04000457 RID: 1111
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000CA RID: 202
		public struct CalendarLoggingSettingsIni
		{
			// Token: 0x06000710 RID: 1808 RVA: 0x00013E5E File Offset: 0x0001205E
			internal CalendarLoggingSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000711 RID: 1809 RVA: 0x00013E67 File Offset: 0x00012067
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("CalendarLogging.settings.ini");
			}

			// Token: 0x06000712 RID: 1810 RVA: 0x00013E79 File Offset: 0x00012079
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("CalendarLogging.settings.ini", id);
			}

			// Token: 0x06000713 RID: 1811 RVA: 0x00013E8C File Offset: 0x0001208C
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("CalendarLogging.settings.ini", id1, ids);
			}

			// Token: 0x170004D2 RID: 1234
			// (get) Token: 0x06000714 RID: 1812 RVA: 0x00013EA0 File Offset: 0x000120A0
			public IFeature FixMissingMeetingBody
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CalendarLogging.settings.ini", "FixMissingMeetingBody");
				}
			}

			// Token: 0x170004D3 RID: 1235
			// (get) Token: 0x06000715 RID: 1813 RVA: 0x00013EB7 File Offset: 0x000120B7
			public IFeature CalendarLoggingIncludeSeriesMeetingMessagesInCVS
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CalendarLogging.settings.ini", "CalendarLoggingIncludeSeriesMeetingMessagesInCVS");
				}
			}

			// Token: 0x04000458 RID: 1112
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000CB RID: 203
		public struct ClientAccessRulesCommonSettingsIni
		{
			// Token: 0x06000716 RID: 1814 RVA: 0x00013ECE File Offset: 0x000120CE
			internal ClientAccessRulesCommonSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000717 RID: 1815 RVA: 0x00013ED7 File Offset: 0x000120D7
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("ClientAccessRulesCommon.settings.ini");
			}

			// Token: 0x06000718 RID: 1816 RVA: 0x00013EE9 File Offset: 0x000120E9
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("ClientAccessRulesCommon.settings.ini", id);
			}

			// Token: 0x06000719 RID: 1817 RVA: 0x00013EFC File Offset: 0x000120FC
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("ClientAccessRulesCommon.settings.ini", id1, ids);
			}

			// Token: 0x170004D4 RID: 1236
			// (get) Token: 0x0600071A RID: 1818 RVA: 0x00013F10 File Offset: 0x00012110
			public IFeature ImplicitAllowLocalClientAccessRulesEnabled
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("ClientAccessRulesCommon.settings.ini", "ImplicitAllowLocalClientAccessRulesEnabled");
				}
			}

			// Token: 0x170004D5 RID: 1237
			// (get) Token: 0x0600071B RID: 1819 RVA: 0x00013F27 File Offset: 0x00012127
			public ICacheExpiryTimeInMinutes ClientAccessRulesCacheExpiryTime
			{
				get
				{
					return this.snapshot.GetObject<ICacheExpiryTimeInMinutes>("ClientAccessRulesCommon.settings.ini", "ClientAccessRulesCacheExpiryTime");
				}
			}

			// Token: 0x04000459 RID: 1113
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000CC RID: 204
		public struct CmdletInfraSettingsIni
		{
			// Token: 0x0600071C RID: 1820 RVA: 0x00013F3E File Offset: 0x0001213E
			internal CmdletInfraSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x0600071D RID: 1821 RVA: 0x00013F47 File Offset: 0x00012147
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("CmdletInfra.settings.ini");
			}

			// Token: 0x0600071E RID: 1822 RVA: 0x00013F59 File Offset: 0x00012159
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("CmdletInfra.settings.ini", id);
			}

			// Token: 0x0600071F RID: 1823 RVA: 0x00013F6C File Offset: 0x0001216C
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("CmdletInfra.settings.ini", id1, ids);
			}

			// Token: 0x170004D6 RID: 1238
			// (get) Token: 0x06000720 RID: 1824 RVA: 0x00013F80 File Offset: 0x00012180
			public ICmdletSettings NewTransportRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-TransportRule");
				}
			}

			// Token: 0x170004D7 RID: 1239
			// (get) Token: 0x06000721 RID: 1825 RVA: 0x00013F97 File Offset: 0x00012197
			public IFeature ReportingWebService
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "ReportingWebService");
				}
			}

			// Token: 0x170004D8 RID: 1240
			// (get) Token: 0x06000722 RID: 1826 RVA: 0x00013FAE File Offset: 0x000121AE
			public IFeature PrePopulateCacheForMailboxBasedOnDatabase
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "PrePopulateCacheForMailboxBasedOnDatabase");
				}
			}

			// Token: 0x170004D9 RID: 1241
			// (get) Token: 0x06000723 RID: 1827 RVA: 0x00013FC5 File Offset: 0x000121C5
			public ICmdletSettings SetMailboxImportRequest
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-MailboxImportRequest");
				}
			}

			// Token: 0x170004DA RID: 1242
			// (get) Token: 0x06000724 RID: 1828 RVA: 0x00013FDC File Offset: 0x000121DC
			public ICmdletSettings SetHoldComplianceRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-HoldComplianceRule");
				}
			}

			// Token: 0x170004DB RID: 1243
			// (get) Token: 0x06000725 RID: 1829 RVA: 0x00013FF3 File Offset: 0x000121F3
			public ICmdletSettings GetHistoricalSearch
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-HistoricalSearch");
				}
			}

			// Token: 0x170004DC RID: 1244
			// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001400A File Offset: 0x0001220A
			public ICmdletSettings GetSPOOneDriveForBusinessFileActivityReport
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-SPOOneDriveForBusinessFileActivityReport");
				}
			}

			// Token: 0x170004DD RID: 1245
			// (get) Token: 0x06000727 RID: 1831 RVA: 0x00014021 File Offset: 0x00012221
			public ICmdletSettings RemoveDataClassification
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Remove-DataClassification");
				}
			}

			// Token: 0x170004DE RID: 1246
			// (get) Token: 0x06000728 RID: 1832 RVA: 0x00014038 File Offset: 0x00012238
			public IFeature SetPasswordWithoutOldPassword
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "SetPasswordWithoutOldPassword");
				}
			}

			// Token: 0x170004DF RID: 1247
			// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001404F File Offset: 0x0001224F
			public ICmdletSettings NewAuditConfigurationPolicy
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-AuditConfigurationPolicy");
				}
			}

			// Token: 0x170004E0 RID: 1248
			// (get) Token: 0x0600072A RID: 1834 RVA: 0x00014066 File Offset: 0x00012266
			public ICmdletSettings NewMailboxSearch
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-MailboxSearch");
				}
			}

			// Token: 0x170004E1 RID: 1249
			// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001407D File Offset: 0x0001227D
			public IFeature SiteMailboxCheckSharePointUrlAgainstTrustedHosts
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "SiteMailboxCheckSharePointUrlAgainstTrustedHosts");
				}
			}

			// Token: 0x170004E2 RID: 1250
			// (get) Token: 0x0600072C RID: 1836 RVA: 0x00014094 File Offset: 0x00012294
			public ICmdletSettings SetDataClassification
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-DataClassification");
				}
			}

			// Token: 0x170004E3 RID: 1251
			// (get) Token: 0x0600072D RID: 1837 RVA: 0x000140AB File Offset: 0x000122AB
			public ICmdletSettings NewAuditConfigurationRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-AuditConfigurationRule");
				}
			}

			// Token: 0x170004E4 RID: 1252
			// (get) Token: 0x0600072E RID: 1838 RVA: 0x000140C2 File Offset: 0x000122C2
			public IFeature LimitNameMaxlength
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "LimitNameMaxlength");
				}
			}

			// Token: 0x170004E5 RID: 1253
			// (get) Token: 0x0600072F RID: 1839 RVA: 0x000140D9 File Offset: 0x000122D9
			public IFeature CmdletMonitoring
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "CmdletMonitoring");
				}
			}

			// Token: 0x170004E6 RID: 1254
			// (get) Token: 0x06000730 RID: 1840 RVA: 0x000140F0 File Offset: 0x000122F0
			public ICmdletSettings SetReportSchedule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-ReportSchedule");
				}
			}

			// Token: 0x170004E7 RID: 1255
			// (get) Token: 0x06000731 RID: 1841 RVA: 0x00014107 File Offset: 0x00012307
			public ICmdletSettings GetHoldComplianceRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-HoldComplianceRule");
				}
			}

			// Token: 0x170004E8 RID: 1256
			// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001411E File Offset: 0x0001231E
			public IFeature GlobalAddressListAttrbutes
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "GlobalAddressListAttrbutes");
				}
			}

			// Token: 0x170004E9 RID: 1257
			// (get) Token: 0x06000733 RID: 1843 RVA: 0x00014135 File Offset: 0x00012335
			public ICmdletSettings GetComplianceSearch
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-ComplianceSearch");
				}
			}

			// Token: 0x170004EA RID: 1258
			// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001414C File Offset: 0x0001234C
			public ICmdletSettings AddMailbox
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Add-Mailbox");
				}
			}

			// Token: 0x170004EB RID: 1259
			// (get) Token: 0x06000735 RID: 1845 RVA: 0x00014163 File Offset: 0x00012363
			public ICmdletSettings InstallUnifiedCompliancePrerequisite
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Install-UnifiedCompliancePrerequisite");
				}
			}

			// Token: 0x170004EC RID: 1260
			// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001417A File Offset: 0x0001237A
			public IFeature ServiceAccountForest
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "ServiceAccountForest");
				}
			}

			// Token: 0x170004ED RID: 1261
			// (get) Token: 0x06000737 RID: 1847 RVA: 0x00014191 File Offset: 0x00012391
			public ICmdletSettings GetSPOSkyDriveProStorageReport
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-SPOSkyDriveProStorageReport");
				}
			}

			// Token: 0x170004EE RID: 1262
			// (get) Token: 0x06000738 RID: 1848 RVA: 0x000141A8 File Offset: 0x000123A8
			public IFeature InactiveMailbox
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "InactiveMailbox");
				}
			}

			// Token: 0x170004EF RID: 1263
			// (get) Token: 0x06000739 RID: 1849 RVA: 0x000141BF File Offset: 0x000123BF
			public ICmdletSettings NewDlpComplianceRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-DlpComplianceRule");
				}
			}

			// Token: 0x170004F0 RID: 1264
			// (get) Token: 0x0600073A RID: 1850 RVA: 0x000141D6 File Offset: 0x000123D6
			public ICmdletSettings RemoveHoldComplianceRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Remove-HoldComplianceRule");
				}
			}

			// Token: 0x170004F1 RID: 1265
			// (get) Token: 0x0600073B RID: 1851 RVA: 0x000141ED File Offset: 0x000123ED
			public IFeature Psws
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "Psws");
				}
			}

			// Token: 0x170004F2 RID: 1266
			// (get) Token: 0x0600073C RID: 1852 RVA: 0x00014204 File Offset: 0x00012404
			public ICmdletSettings RemoveReportSchedule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Remove-ReportSchedule");
				}
			}

			// Token: 0x170004F3 RID: 1267
			// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001421B File Offset: 0x0001241B
			public ICmdletSettings GetClientAccessRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-ClientAccessRule");
				}
			}

			// Token: 0x170004F4 RID: 1268
			// (get) Token: 0x0600073E RID: 1854 RVA: 0x00014232 File Offset: 0x00012432
			public IFeature SetDefaultProhibitSendReceiveQuota
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "SetDefaultProhibitSendReceiveQuota");
				}
			}

			// Token: 0x170004F5 RID: 1269
			// (get) Token: 0x0600073F RID: 1855 RVA: 0x00014249 File Offset: 0x00012449
			public ICmdletSettings SetMailbox
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-Mailbox");
				}
			}

			// Token: 0x170004F6 RID: 1270
			// (get) Token: 0x06000740 RID: 1856 RVA: 0x00014260 File Offset: 0x00012460
			public ICmdletSettings GetExternalActivityReport
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-ExternalActivityReport");
				}
			}

			// Token: 0x170004F7 RID: 1271
			// (get) Token: 0x06000741 RID: 1857 RVA: 0x00014277 File Offset: 0x00012477
			public ICmdletSettings GetDlpCompliancePolicy
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-DlpCompliancePolicy");
				}
			}

			// Token: 0x170004F8 RID: 1272
			// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001428E File Offset: 0x0001248E
			public IFeature ReportToOriginator
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "ReportToOriginator");
				}
			}

			// Token: 0x170004F9 RID: 1273
			// (get) Token: 0x06000743 RID: 1859 RVA: 0x000142A5 File Offset: 0x000124A5
			public ICmdletSettings SetMailUser
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-MailUser");
				}
			}

			// Token: 0x170004FA RID: 1274
			// (get) Token: 0x06000744 RID: 1860 RVA: 0x000142BC File Offset: 0x000124BC
			public ICmdletSettings NewMailboxExportRequest
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-MailboxExportRequest");
				}
			}

			// Token: 0x170004FB RID: 1275
			// (get) Token: 0x06000745 RID: 1861 RVA: 0x000142D3 File Offset: 0x000124D3
			public ICmdletSettings TestClientAccessRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Test-ClientAccessRule");
				}
			}

			// Token: 0x170004FC RID: 1276
			// (get) Token: 0x06000746 RID: 1862 RVA: 0x000142EA File Offset: 0x000124EA
			public ICmdletSettings GetExternalActivitySummaryReport
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-ExternalActivitySummaryReport");
				}
			}

			// Token: 0x170004FD RID: 1277
			// (get) Token: 0x06000747 RID: 1863 RVA: 0x00014301 File Offset: 0x00012501
			public ICmdletSettings NewClientAccessRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-ClientAccessRule");
				}
			}

			// Token: 0x170004FE RID: 1278
			// (get) Token: 0x06000748 RID: 1864 RVA: 0x00014318 File Offset: 0x00012518
			public ICmdletSettings GetExternalActivityByUserReport
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-ExternalActivityByUserReport");
				}
			}

			// Token: 0x170004FF RID: 1279
			// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001432F File Offset: 0x0001252F
			public ICmdletSettings GetFolderMoveRequest
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-FolderMoveRequest");
				}
			}

			// Token: 0x17000500 RID: 1280
			// (get) Token: 0x0600074A RID: 1866 RVA: 0x00014346 File Offset: 0x00012546
			public ICmdletSettings StopHistoricalSearch
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Stop-HistoricalSearch");
				}
			}

			// Token: 0x17000501 RID: 1281
			// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001435D File Offset: 0x0001255D
			public ICmdletSettings NewDlpCompliancePolicy
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-DlpCompliancePolicy");
				}
			}

			// Token: 0x17000502 RID: 1282
			// (get) Token: 0x0600074C RID: 1868 RVA: 0x00014374 File Offset: 0x00012574
			public ICmdletSettings SetDeviceConfigurationRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-DeviceConfigurationRule");
				}
			}

			// Token: 0x17000503 RID: 1283
			// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001438B File Offset: 0x0001258B
			public IFeature WinRMExchangeDataUseTypeNamedPipe
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "WinRMExchangeDataUseTypeNamedPipe");
				}
			}

			// Token: 0x17000504 RID: 1284
			// (get) Token: 0x0600074E RID: 1870 RVA: 0x000143A2 File Offset: 0x000125A2
			public ICmdletSettings GetReportScheduleHistory
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-ReportScheduleHistory");
				}
			}

			// Token: 0x17000505 RID: 1285
			// (get) Token: 0x0600074F RID: 1871 RVA: 0x000143B9 File Offset: 0x000125B9
			public ICmdletSettings RemoveFolderMoveRequest
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Remove-FolderMoveRequest");
				}
			}

			// Token: 0x17000506 RID: 1286
			// (get) Token: 0x06000750 RID: 1872 RVA: 0x000143D0 File Offset: 0x000125D0
			public IFeature RecoverMailBox
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "RecoverMailBox");
				}
			}

			// Token: 0x17000507 RID: 1287
			// (get) Token: 0x06000751 RID: 1873 RVA: 0x000143E7 File Offset: 0x000125E7
			public IFeature SiteMailboxProvisioningInExecutingUserOUEnabled
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "SiteMailboxProvisioningInExecutingUserOUEnabled");
				}
			}

			// Token: 0x17000508 RID: 1288
			// (get) Token: 0x06000752 RID: 1874 RVA: 0x000143FE File Offset: 0x000125FE
			public ICmdletSettings GetListedIPWrapper
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-ListedIPWrapper");
				}
			}

			// Token: 0x17000509 RID: 1289
			// (get) Token: 0x06000753 RID: 1875 RVA: 0x00014415 File Offset: 0x00012615
			public ICmdletSettings SetClientAccessRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-ClientAccessRule");
				}
			}

			// Token: 0x1700050A RID: 1290
			// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001442C File Offset: 0x0001262C
			public ICmdletSettings GetExternalActivityByDomainReport
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-ExternalActivityByDomainReport");
				}
			}

			// Token: 0x1700050B RID: 1291
			// (get) Token: 0x06000755 RID: 1877 RVA: 0x00014443 File Offset: 0x00012643
			public ICmdletSettings NewDeviceConfigurationRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-DeviceConfigurationRule");
				}
			}

			// Token: 0x1700050C RID: 1292
			// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001445A File Offset: 0x0001265A
			public ICmdletSettings GetCsClientDeviceReport
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-CsClientDeviceReport");
				}
			}

			// Token: 0x1700050D RID: 1293
			// (get) Token: 0x06000757 RID: 1879 RVA: 0x00014471 File Offset: 0x00012671
			public ICmdletSettings GetDlpComplianceRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-DlpComplianceRule");
				}
			}

			// Token: 0x1700050E RID: 1294
			// (get) Token: 0x06000758 RID: 1880 RVA: 0x00014488 File Offset: 0x00012688
			public ICmdletSettings GetDeviceConfigurationRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-DeviceConfigurationRule");
				}
			}

			// Token: 0x1700050F RID: 1295
			// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001449F File Offset: 0x0001269F
			public ICmdletSettings RemoveHoldCompliancePolicy
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Remove-HoldCompliancePolicy");
				}
			}

			// Token: 0x17000510 RID: 1296
			// (get) Token: 0x0600075A RID: 1882 RVA: 0x000144B6 File Offset: 0x000126B6
			public IFeature ShowFismaBanner
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "ShowFismaBanner");
				}
			}

			// Token: 0x17000511 RID: 1297
			// (get) Token: 0x0600075B RID: 1883 RVA: 0x000144CD File Offset: 0x000126CD
			public IFeature UseDatabaseQuotaDefaults
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "UseDatabaseQuotaDefaults");
				}
			}

			// Token: 0x17000512 RID: 1298
			// (get) Token: 0x0600075C RID: 1884 RVA: 0x000144E4 File Offset: 0x000126E4
			public ICmdletSettings GetAuditConfigurationRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-AuditConfigurationRule");
				}
			}

			// Token: 0x17000513 RID: 1299
			// (get) Token: 0x0600075D RID: 1885 RVA: 0x000144FB File Offset: 0x000126FB
			public IFeature WriteEventLogInEnglish
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "WriteEventLogInEnglish");
				}
			}

			// Token: 0x17000514 RID: 1300
			// (get) Token: 0x0600075E RID: 1886 RVA: 0x00014512 File Offset: 0x00012712
			public ICmdletSettings SetDlpCompliancePolicy
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-DlpCompliancePolicy");
				}
			}

			// Token: 0x17000515 RID: 1301
			// (get) Token: 0x0600075F RID: 1887 RVA: 0x00014529 File Offset: 0x00012729
			public IFeature SupportOptimizedFilterOnlyInDDG
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "SupportOptimizedFilterOnlyInDDG");
				}
			}

			// Token: 0x17000516 RID: 1302
			// (get) Token: 0x06000760 RID: 1888 RVA: 0x00014540 File Offset: 0x00012740
			public ICmdletSettings RemoveComplianceSearch
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Remove-ComplianceSearch");
				}
			}

			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x06000761 RID: 1889 RVA: 0x00014557 File Offset: 0x00012757
			public IFeature DepthTwoTypeEntry
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "DepthTwoTypeEntry");
				}
			}

			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x06000762 RID: 1890 RVA: 0x0001456E File Offset: 0x0001276E
			public ICmdletSettings SetAuditConfig
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-AuditConfig");
				}
			}

			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x06000763 RID: 1891 RVA: 0x00014585 File Offset: 0x00012785
			public ICmdletSettings NewDataClassification
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-DataClassification");
				}
			}

			// Token: 0x1700051A RID: 1306
			// (get) Token: 0x06000764 RID: 1892 RVA: 0x0001459C File Offset: 0x0001279C
			public ICmdletSettings NewMigrationEndpoint
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-MigrationEndpoint");
				}
			}

			// Token: 0x1700051B RID: 1307
			// (get) Token: 0x06000765 RID: 1893 RVA: 0x000145B3 File Offset: 0x000127B3
			public ICmdletSettings SetMailboxExportRequest
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-MailboxExportRequest");
				}
			}

			// Token: 0x1700051C RID: 1308
			// (get) Token: 0x06000766 RID: 1894 RVA: 0x000145CA File Offset: 0x000127CA
			public IFeature ValidateExternalEmailAddressInAcceptedDomain
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "ValidateExternalEmailAddressInAcceptedDomain");
				}
			}

			// Token: 0x1700051D RID: 1309
			// (get) Token: 0x06000767 RID: 1895 RVA: 0x000145E1 File Offset: 0x000127E1
			public ICmdletSettings EnableEOPMailUser
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Enable-EOPMailUser");
				}
			}

			// Token: 0x1700051E RID: 1310
			// (get) Token: 0x06000768 RID: 1896 RVA: 0x000145F8 File Offset: 0x000127F8
			public ICmdletSettings GetOMEConfiguration
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-OMEConfiguration");
				}
			}

			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x06000769 RID: 1897 RVA: 0x0001460F File Offset: 0x0001280F
			public ICmdletSettings NewFolderMoveRequest
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-FolderMoveRequest");
				}
			}

			// Token: 0x17000520 RID: 1312
			// (get) Token: 0x0600076A RID: 1898 RVA: 0x00014626 File Offset: 0x00012826
			public IFeature EmailAddressPolicy
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "EmailAddressPolicy");
				}
			}

			// Token: 0x17000521 RID: 1313
			// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001463D File Offset: 0x0001283D
			public IFeature SkipPiiRedactionForForestWideObject
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "SkipPiiRedactionForForestWideObject");
				}
			}

			// Token: 0x17000522 RID: 1314
			// (get) Token: 0x0600076C RID: 1900 RVA: 0x00014654 File Offset: 0x00012854
			public ICmdletSettings GetPartnerClientExpiringSubscriptionReport
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-PartnerClientExpiringSubscriptionReport");
				}
			}

			// Token: 0x17000523 RID: 1315
			// (get) Token: 0x0600076D RID: 1901 RVA: 0x0001466B File Offset: 0x0001286B
			public IFeature PiiRedaction
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "PiiRedaction");
				}
			}

			// Token: 0x17000524 RID: 1316
			// (get) Token: 0x0600076E RID: 1902 RVA: 0x00014682 File Offset: 0x00012882
			public IFeature ValidateFilteringOnlyUser
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "ValidateFilteringOnlyUser");
				}
			}

			// Token: 0x17000525 RID: 1317
			// (get) Token: 0x0600076F RID: 1903 RVA: 0x00014699 File Offset: 0x00012899
			public IFeature SoftDeleteObject
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "SoftDeleteObject");
				}
			}

			// Token: 0x17000526 RID: 1318
			// (get) Token: 0x06000770 RID: 1904 RVA: 0x000146B0 File Offset: 0x000128B0
			public ICmdletSettings SetMailboxSearch
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-MailboxSearch");
				}
			}

			// Token: 0x17000527 RID: 1319
			// (get) Token: 0x06000771 RID: 1905 RVA: 0x000146C7 File Offset: 0x000128C7
			public ICmdletSettings GetSPOOneDriveForBusinessUserStatisticsReport
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-SPOOneDriveForBusinessUserStatisticsReport");
				}
			}

			// Token: 0x17000528 RID: 1320
			// (get) Token: 0x06000772 RID: 1906 RVA: 0x000146DE File Offset: 0x000128DE
			public ICmdletSettings SetFolderMoveRequest
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-FolderMoveRequest");
				}
			}

			// Token: 0x17000529 RID: 1321
			// (get) Token: 0x06000773 RID: 1907 RVA: 0x000146F5 File Offset: 0x000128F5
			public ICmdletSettings AddDelistIP
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Add-DelistIP");
				}
			}

			// Token: 0x1700052A RID: 1322
			// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001470C File Offset: 0x0001290C
			public IFeature GenerateNewExternalDirectoryObjectId
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "GenerateNewExternalDirectoryObjectId");
				}
			}

			// Token: 0x1700052B RID: 1323
			// (get) Token: 0x06000775 RID: 1909 RVA: 0x00014723 File Offset: 0x00012923
			public ICmdletSettings NewComplianceSearch
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-ComplianceSearch");
				}
			}

			// Token: 0x1700052C RID: 1324
			// (get) Token: 0x06000776 RID: 1910 RVA: 0x0001473A File Offset: 0x0001293A
			public IFeature IncludeFBOnlyForCalendarContributor
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "IncludeFBOnlyForCalendarContributor");
				}
			}

			// Token: 0x1700052D RID: 1325
			// (get) Token: 0x06000777 RID: 1911 RVA: 0x00014751 File Offset: 0x00012951
			public IFeature ValidateEnableRoomMailboxAccount
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "ValidateEnableRoomMailboxAccount");
				}
			}

			// Token: 0x1700052E RID: 1326
			// (get) Token: 0x06000778 RID: 1912 RVA: 0x00014768 File Offset: 0x00012968
			public ICmdletSettings SetDlpComplianceRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-DlpComplianceRule");
				}
			}

			// Token: 0x1700052F RID: 1327
			// (get) Token: 0x06000779 RID: 1913 RVA: 0x0001477F File Offset: 0x0001297F
			public ICmdletSettings RemoveDlpComplianceRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Remove-DlpComplianceRule");
				}
			}

			// Token: 0x17000530 RID: 1328
			// (get) Token: 0x0600077A RID: 1914 RVA: 0x00014796 File Offset: 0x00012996
			public IFeature PswsCmdletProxy
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "PswsCmdletProxy");
				}
			}

			// Token: 0x17000531 RID: 1329
			// (get) Token: 0x0600077B RID: 1915 RVA: 0x000147AD File Offset: 0x000129AD
			public ICmdletSettings SetHoldCompliancePolicy
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-HoldCompliancePolicy");
				}
			}

			// Token: 0x17000532 RID: 1330
			// (get) Token: 0x0600077C RID: 1916 RVA: 0x000147C4 File Offset: 0x000129C4
			public IFeature LegacyRegCodeSupport
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "LegacyRegCodeSupport");
				}
			}

			// Token: 0x17000533 RID: 1331
			// (get) Token: 0x0600077D RID: 1917 RVA: 0x000147DB File Offset: 0x000129DB
			public ICmdletSettings SetOMEConfiguration
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-OMEConfiguration");
				}
			}

			// Token: 0x17000534 RID: 1332
			// (get) Token: 0x0600077E RID: 1918 RVA: 0x000147F2 File Offset: 0x000129F2
			public ICmdletSettings GetSPOActiveUserReport
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-SPOActiveUserReport");
				}
			}

			// Token: 0x17000535 RID: 1333
			// (get) Token: 0x0600077F RID: 1919 RVA: 0x00014809 File Offset: 0x00012A09
			public ICmdletSettings RemoveAuditConfigurationRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Remove-AuditConfigurationRule");
				}
			}

			// Token: 0x17000536 RID: 1334
			// (get) Token: 0x06000780 RID: 1920 RVA: 0x00014820 File Offset: 0x00012A20
			public ICmdletSettings GetSPOSkyDriveProDeployedReport
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-SPOSkyDriveProDeployedReport");
				}
			}

			// Token: 0x17000537 RID: 1335
			// (get) Token: 0x06000781 RID: 1921 RVA: 0x00014837 File Offset: 0x00012A37
			public ICmdletSettings SetTransportRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-TransportRule");
				}
			}

			// Token: 0x17000538 RID: 1336
			// (get) Token: 0x06000782 RID: 1922 RVA: 0x0001484E File Offset: 0x00012A4E
			public ICmdletSettings NewFingerprint
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-Fingerprint");
				}
			}

			// Token: 0x17000539 RID: 1337
			// (get) Token: 0x06000783 RID: 1923 RVA: 0x00014865 File Offset: 0x00012A65
			public ICmdletSettings GetReputationOverride
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-ReputationOverride");
				}
			}

			// Token: 0x1700053A RID: 1338
			// (get) Token: 0x06000784 RID: 1924 RVA: 0x0001487C File Offset: 0x00012A7C
			public ICmdletSettings NewReportSchedule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-ReportSchedule");
				}
			}

			// Token: 0x1700053B RID: 1339
			// (get) Token: 0x06000785 RID: 1925 RVA: 0x00014893 File Offset: 0x00012A93
			public ICmdletSettings NewMailbox
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-Mailbox");
				}
			}

			// Token: 0x1700053C RID: 1340
			// (get) Token: 0x06000786 RID: 1926 RVA: 0x000148AA File Offset: 0x00012AAA
			public IFeature InstallModernGroupsAddressList
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "InstallModernGroupsAddressList");
				}
			}

			// Token: 0x1700053D RID: 1341
			// (get) Token: 0x06000787 RID: 1927 RVA: 0x000148C1 File Offset: 0x00012AC1
			public IFeature GenericExchangeSnapin
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "GenericExchangeSnapin");
				}
			}

			// Token: 0x1700053E RID: 1342
			// (get) Token: 0x06000788 RID: 1928 RVA: 0x000148D8 File Offset: 0x00012AD8
			public ICmdletSettings SetMigrationBatch
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-MigrationBatch");
				}
			}

			// Token: 0x1700053F RID: 1343
			// (get) Token: 0x06000789 RID: 1929 RVA: 0x000148EF File Offset: 0x00012AEF
			public ICmdletSettings RemoveAuditConfigurationPolicy
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Remove-AuditConfigurationPolicy");
				}
			}

			// Token: 0x17000540 RID: 1344
			// (get) Token: 0x0600078A RID: 1930 RVA: 0x00014906 File Offset: 0x00012B06
			public ICmdletSettings SetAuditConfigurationRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-AuditConfigurationRule");
				}
			}

			// Token: 0x17000541 RID: 1345
			// (get) Token: 0x0600078B RID: 1931 RVA: 0x0001491D File Offset: 0x00012B1D
			public ICmdletSettings RemoveClientAccessRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Remove-ClientAccessRule");
				}
			}

			// Token: 0x17000542 RID: 1346
			// (get) Token: 0x0600078C RID: 1932 RVA: 0x00014934 File Offset: 0x00012B34
			public IFeature OverWriteElcMailboxFlags
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "OverWriteElcMailboxFlags");
				}
			}

			// Token: 0x17000543 RID: 1347
			// (get) Token: 0x0600078D RID: 1933 RVA: 0x0001494B File Offset: 0x00012B4B
			public IFeature MaxAddressBookPolicies
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "MaxAddressBookPolicies");
				}
			}

			// Token: 0x17000544 RID: 1348
			// (get) Token: 0x0600078E RID: 1934 RVA: 0x00014962 File Offset: 0x00012B62
			public ICmdletSettings StartComplianceSearch
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Start-ComplianceSearch");
				}
			}

			// Token: 0x17000545 RID: 1349
			// (get) Token: 0x0600078F RID: 1935 RVA: 0x00014979 File Offset: 0x00012B79
			public ICmdletSettings TestMigrationServerAvailability
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Test-MigrationServerAvailability");
				}
			}

			// Token: 0x17000546 RID: 1350
			// (get) Token: 0x06000790 RID: 1936 RVA: 0x00014990 File Offset: 0x00012B90
			public IFeature WinRMExchangeDataUseAuthenticationType
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "WinRMExchangeDataUseAuthenticationType");
				}
			}

			// Token: 0x17000547 RID: 1351
			// (get) Token: 0x06000791 RID: 1937 RVA: 0x000149A7 File Offset: 0x00012BA7
			public IFeature RpsClientAccessRulesEnabled
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "RpsClientAccessRulesEnabled");
				}
			}

			// Token: 0x17000548 RID: 1352
			// (get) Token: 0x06000792 RID: 1938 RVA: 0x000149BE File Offset: 0x00012BBE
			public ICmdletSettings StopComplianceSearch
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Stop-ComplianceSearch");
				}
			}

			// Token: 0x17000549 RID: 1353
			// (get) Token: 0x06000793 RID: 1939 RVA: 0x000149D5 File Offset: 0x00012BD5
			public ICmdletSettings ResumeFolderMoveRequest
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Resume-FolderMoveRequest");
				}
			}

			// Token: 0x1700054A RID: 1354
			// (get) Token: 0x06000794 RID: 1940 RVA: 0x000149EC File Offset: 0x00012BEC
			public ICmdletSettings RemoveDlpCompliancePolicy
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Remove-DlpCompliancePolicy");
				}
			}

			// Token: 0x1700054B RID: 1355
			// (get) Token: 0x06000795 RID: 1941 RVA: 0x00014A03 File Offset: 0x00012C03
			public ICmdletSettings RemoveMailbox
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Remove-Mailbox");
				}
			}

			// Token: 0x1700054C RID: 1356
			// (get) Token: 0x06000796 RID: 1942 RVA: 0x00014A1A File Offset: 0x00012C1A
			public ICmdletSettings GetSPOTeamSiteDeployedReport
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-SPOTeamSiteDeployedReport");
				}
			}

			// Token: 0x1700054D RID: 1357
			// (get) Token: 0x06000797 RID: 1943 RVA: 0x00014A31 File Offset: 0x00012C31
			public ICmdletSettings NewHoldComplianceRule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-HoldComplianceRule");
				}
			}

			// Token: 0x1700054E RID: 1358
			// (get) Token: 0x06000798 RID: 1944 RVA: 0x00014A48 File Offset: 0x00012C48
			public IFeature PswsClientAccessRulesEnabled
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "PswsClientAccessRulesEnabled");
				}
			}

			// Token: 0x1700054F RID: 1359
			// (get) Token: 0x06000799 RID: 1945 RVA: 0x00014A5F File Offset: 0x00012C5F
			public ICmdletSettings RemoveReputationOverride
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Remove-ReputationOverride");
				}
			}

			// Token: 0x17000550 RID: 1360
			// (get) Token: 0x0600079A RID: 1946 RVA: 0x00014A76 File Offset: 0x00012C76
			public ICmdletSettings GetAuditConfigurationPolicy
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-AuditConfigurationPolicy");
				}
			}

			// Token: 0x17000551 RID: 1361
			// (get) Token: 0x0600079B RID: 1947 RVA: 0x00014A8D File Offset: 0x00012C8D
			public ICmdletSettings GetDnsBlocklistInfo
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-DnsBlocklistInfo");
				}
			}

			// Token: 0x17000552 RID: 1362
			// (get) Token: 0x0600079C RID: 1948 RVA: 0x00014AA4 File Offset: 0x00012CA4
			public ICmdletSettings GetFolderMoveRequestStatistics
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-FolderMoveRequestStatistics");
				}
			}

			// Token: 0x17000553 RID: 1363
			// (get) Token: 0x0600079D RID: 1949 RVA: 0x00014ABB File Offset: 0x00012CBB
			public ICmdletSettings StartHistoricalSearch
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Start-HistoricalSearch");
				}
			}

			// Token: 0x17000554 RID: 1364
			// (get) Token: 0x0600079E RID: 1950 RVA: 0x00014AD2 File Offset: 0x00012CD2
			public IFeature CheckForDedicatedTenantAdminRoleNamePrefix
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "CheckForDedicatedTenantAdminRoleNamePrefix");
				}
			}

			// Token: 0x17000555 RID: 1365
			// (get) Token: 0x0600079F RID: 1951 RVA: 0x00014AE9 File Offset: 0x00012CE9
			public ICmdletSettings SuspendFolderMoveRequest
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Suspend-FolderMoveRequest");
				}
			}

			// Token: 0x17000556 RID: 1366
			// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00014B00 File Offset: 0x00012D00
			public ICmdletSettings NewMailboxImportRequest
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-MailboxImportRequest");
				}
			}

			// Token: 0x17000557 RID: 1367
			// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00014B17 File Offset: 0x00012D17
			public ICmdletSettings NewMigrationBatch
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-MigrationBatch");
				}
			}

			// Token: 0x17000558 RID: 1368
			// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00014B2E File Offset: 0x00012D2E
			public ICmdletSettings SetComplianceSearch
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Set-ComplianceSearch");
				}
			}

			// Token: 0x17000559 RID: 1369
			// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00014B45 File Offset: 0x00012D45
			public ICmdletSettings GetSPOTeamSiteStorageReport
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-SPOTeamSiteStorageReport");
				}
			}

			// Token: 0x1700055A RID: 1370
			// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00014B5C File Offset: 0x00012D5C
			public ICmdletSettings GetHoldCompliancePolicy
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-HoldCompliancePolicy");
				}
			}

			// Token: 0x1700055B RID: 1371
			// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00014B73 File Offset: 0x00012D73
			public ICmdletSettings GetDlpSensitiveInformationType
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-DlpSensitiveInformationType");
				}
			}

			// Token: 0x1700055C RID: 1372
			// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00014B8A File Offset: 0x00012D8A
			public ICmdletSettings GetReportScheduleList
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-ReportScheduleList");
				}
			}

			// Token: 0x1700055D RID: 1373
			// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00014BA1 File Offset: 0x00012DA1
			public ICmdletSettings GetMailbox
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-Mailbox");
				}
			}

			// Token: 0x1700055E RID: 1374
			// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00014BB8 File Offset: 0x00012DB8
			public ICmdletSettings GetSPOTenantStorageMetricReport
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-SPOTenantStorageMetricReport");
				}
			}

			// Token: 0x1700055F RID: 1375
			// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00014BCF File Offset: 0x00012DCF
			public ICmdletSettings NewMailUser
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-MailUser");
				}
			}

			// Token: 0x17000560 RID: 1376
			// (get) Token: 0x060007AA RID: 1962 RVA: 0x00014BE6 File Offset: 0x00012DE6
			public ICmdletSettings GetReportSchedule
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-ReportSchedule");
				}
			}

			// Token: 0x17000561 RID: 1377
			// (get) Token: 0x060007AB RID: 1963 RVA: 0x00014BFD File Offset: 0x00012DFD
			public IFeature SetActiveArchiveStatus
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "SetActiveArchiveStatus");
				}
			}

			// Token: 0x17000562 RID: 1378
			// (get) Token: 0x060007AC RID: 1964 RVA: 0x00014C14 File Offset: 0x00012E14
			public ICmdletSettings GetAuditConfig
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "Get-AuditConfig");
				}
			}

			// Token: 0x17000563 RID: 1379
			// (get) Token: 0x060007AD RID: 1965 RVA: 0x00014C2B File Offset: 0x00012E2B
			public IFeature WsSecuritySymmetricAndX509Cert
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "WsSecuritySymmetricAndX509Cert");
				}
			}

			// Token: 0x17000564 RID: 1380
			// (get) Token: 0x060007AE RID: 1966 RVA: 0x00014C42 File Offset: 0x00012E42
			public IFeature ProxyDllUpdate
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CmdletInfra.settings.ini", "ProxyDllUpdate");
				}
			}

			// Token: 0x17000565 RID: 1381
			// (get) Token: 0x060007AF RID: 1967 RVA: 0x00014C59 File Offset: 0x00012E59
			public ICmdletSettings NewHoldCompliancePolicy
			{
				get
				{
					return this.snapshot.GetObject<ICmdletSettings>("CmdletInfra.settings.ini", "New-HoldCompliancePolicy");
				}
			}

			// Token: 0x0400045A RID: 1114
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000CD RID: 205
		public struct CompliancePolicySettingsIni
		{
			// Token: 0x060007B0 RID: 1968 RVA: 0x00014C70 File Offset: 0x00012E70
			internal CompliancePolicySettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060007B1 RID: 1969 RVA: 0x00014C79 File Offset: 0x00012E79
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("CompliancePolicy.settings.ini");
			}

			// Token: 0x060007B2 RID: 1970 RVA: 0x00014C8B File Offset: 0x00012E8B
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("CompliancePolicy.settings.ini", id);
			}

			// Token: 0x060007B3 RID: 1971 RVA: 0x00014C9E File Offset: 0x00012E9E
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("CompliancePolicy.settings.ini", id1, ids);
			}

			// Token: 0x17000566 RID: 1382
			// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00014CB2 File Offset: 0x00012EB2
			public IFeature ProcessForestWideOrgEtrs
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CompliancePolicy.settings.ini", "ProcessForestWideOrgEtrs");
				}
			}

			// Token: 0x17000567 RID: 1383
			// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00014CC9 File Offset: 0x00012EC9
			public IFeature ShowSupervisionPredicate
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CompliancePolicy.settings.ini", "ShowSupervisionPredicate");
				}
			}

			// Token: 0x17000568 RID: 1384
			// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00014CE0 File Offset: 0x00012EE0
			public IFeature ValidateTenantOutboundConnector
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CompliancePolicy.settings.ini", "ValidateTenantOutboundConnector");
				}
			}

			// Token: 0x17000569 RID: 1385
			// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00014CF7 File Offset: 0x00012EF7
			public IFeature RuleConfigurationAdChangeNotifications
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CompliancePolicy.settings.ini", "RuleConfigurationAdChangeNotifications");
				}
			}

			// Token: 0x1700056A RID: 1386
			// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00014D0E File Offset: 0x00012F0E
			public IFeature QuarantineAction
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("CompliancePolicy.settings.ini", "QuarantineAction");
				}
			}

			// Token: 0x0400045B RID: 1115
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000CE RID: 206
		public struct DataStorageSettingsIni
		{
			// Token: 0x060007B9 RID: 1977 RVA: 0x00014D25 File Offset: 0x00012F25
			internal DataStorageSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060007BA RID: 1978 RVA: 0x00014D2E File Offset: 0x00012F2E
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("DataStorage.settings.ini");
			}

			// Token: 0x060007BB RID: 1979 RVA: 0x00014D40 File Offset: 0x00012F40
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("DataStorage.settings.ini", id);
			}

			// Token: 0x060007BC RID: 1980 RVA: 0x00014D53 File Offset: 0x00012F53
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("DataStorage.settings.ini", id1, ids);
			}

			// Token: 0x1700056B RID: 1387
			// (get) Token: 0x060007BD RID: 1981 RVA: 0x00014D67 File Offset: 0x00012F67
			public IFeature CheckForRemoteConnections
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "CheckForRemoteConnections");
				}
			}

			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x060007BE RID: 1982 RVA: 0x00014D7E File Offset: 0x00012F7E
			public IFeature PeopleCentricConversation
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "PeopleCentricConversation");
				}
			}

			// Token: 0x1700056D RID: 1389
			// (get) Token: 0x060007BF RID: 1983 RVA: 0x00014D95 File Offset: 0x00012F95
			public IFeature UseOfflineRms
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "UseOfflineRms");
				}
			}

			// Token: 0x1700056E RID: 1390
			// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00014DAC File Offset: 0x00012FAC
			public IFeature CalendarUpgrade
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "CalendarUpgrade");
				}
			}

			// Token: 0x1700056F RID: 1391
			// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00014DC3 File Offset: 0x00012FC3
			public IFeature IgnoreInessentialMetaDataLoadErrors
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "IgnoreInessentialMetaDataLoadErrors");
				}
			}

			// Token: 0x17000570 RID: 1392
			// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00014DDA File Offset: 0x00012FDA
			public IFeature ModernMailInfra
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "ModernMailInfra");
				}
			}

			// Token: 0x17000571 RID: 1393
			// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00014DF1 File Offset: 0x00012FF1
			public IFeature CalendarView
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "CalendarView");
				}
			}

			// Token: 0x17000572 RID: 1394
			// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00014E08 File Offset: 0x00013008
			public IFeature GroupsForOlkDesktop
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "GroupsForOlkDesktop");
				}
			}

			// Token: 0x17000573 RID: 1395
			// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00014E1F File Offset: 0x0001301F
			public IFeature FindOrgMailboxInMultiTenantEnvironment
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "FindOrgMailboxInMultiTenantEnvironment");
				}
			}

			// Token: 0x17000574 RID: 1396
			// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00014E36 File Offset: 0x00013036
			public IFeature DeleteGroupConversation
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "DeleteGroupConversation");
				}
			}

			// Token: 0x17000575 RID: 1397
			// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00014E4D File Offset: 0x0001304D
			public IFeature ModernConversationPrep
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "ModernConversationPrep");
				}
			}

			// Token: 0x17000576 RID: 1398
			// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00014E64 File Offset: 0x00013064
			public IFeature CheckLicense
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "CheckLicense");
				}
			}

			// Token: 0x17000577 RID: 1399
			// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00014E7B File Offset: 0x0001307B
			public IFeature LoadHostedMailboxLimits
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "LoadHostedMailboxLimits");
				}
			}

			// Token: 0x17000578 RID: 1400
			// (get) Token: 0x060007CA RID: 1994 RVA: 0x00014E92 File Offset: 0x00013092
			public IFeature RepresentRemoteMailbox
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "RepresentRemoteMailbox");
				}
			}

			// Token: 0x17000579 RID: 1401
			// (get) Token: 0x060007CB RID: 1995 RVA: 0x00014EA9 File Offset: 0x000130A9
			public ICalendarUpgradeSettings CalendarUpgradeSettings
			{
				get
				{
					return this.snapshot.GetObject<ICalendarUpgradeSettings>("DataStorage.settings.ini", "CalendarUpgradeSettings");
				}
			}

			// Token: 0x1700057A RID: 1402
			// (get) Token: 0x060007CC RID: 1996 RVA: 0x00014EC0 File Offset: 0x000130C0
			public IFeature CrossPremiseDelegate
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "CrossPremiseDelegate");
				}
			}

			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x060007CD RID: 1997 RVA: 0x00014ED7 File Offset: 0x000130D7
			public ICalendarIcalConversionSettings CalendarIcalConversionSettings
			{
				get
				{
					return this.snapshot.GetObject<ICalendarIcalConversionSettings>("DataStorage.settings.ini", "CalendarIcalConversionSettings");
				}
			}

			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x060007CE RID: 1998 RVA: 0x00014EEE File Offset: 0x000130EE
			public IFeature CalendarViewPropertyRule
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "CalendarViewPropertyRule");
				}
			}

			// Token: 0x1700057D RID: 1405
			// (get) Token: 0x060007CF RID: 1999 RVA: 0x00014F05 File Offset: 0x00013105
			public IFeature CheckR3Coexistence
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "CheckR3Coexistence");
				}
			}

			// Token: 0x1700057E RID: 1406
			// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00014F1C File Offset: 0x0001311C
			public IFeature XOWAConsumerSharing
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "XOWAConsumerSharing");
				}
			}

			// Token: 0x1700057F RID: 1407
			// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00014F33 File Offset: 0x00013133
			public IFeature UserConfigurationAggregation
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "UserConfigurationAggregation");
				}
			}

			// Token: 0x17000580 RID: 1408
			// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00014F4A File Offset: 0x0001314A
			public IFeature StorageAttachmentImageAnalysis
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "StorageAttachmentImageAnalysis");
				}
			}

			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00014F61 File Offset: 0x00013161
			public IFeature LogIpEndpoints
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "LogIpEndpoints");
				}
			}

			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00014F78 File Offset: 0x00013178
			public IFeature CheckExternalAccess
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "CheckExternalAccess");
				}
			}

			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00014F8F File Offset: 0x0001318F
			public IFeature ThreadedConversation
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("DataStorage.settings.ini", "ThreadedConversation");
				}
			}

			// Token: 0x0400045C RID: 1116
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000CF RID: 207
		public struct DiagnosticsSettingsIni
		{
			// Token: 0x060007D6 RID: 2006 RVA: 0x00014FA6 File Offset: 0x000131A6
			internal DiagnosticsSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060007D7 RID: 2007 RVA: 0x00014FAF File Offset: 0x000131AF
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Diagnostics.settings.ini");
			}

			// Token: 0x060007D8 RID: 2008 RVA: 0x00014FC1 File Offset: 0x000131C1
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Diagnostics.settings.ini", id);
			}

			// Token: 0x060007D9 RID: 2009 RVA: 0x00014FD4 File Offset: 0x000131D4
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Diagnostics.settings.ini", id1, ids);
			}

			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x060007DA RID: 2010 RVA: 0x00014FE8 File Offset: 0x000131E8
			public IFeature TraceToHeadersLogger
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Diagnostics.settings.ini", "TraceToHeadersLogger");
				}
			}

			// Token: 0x0400045D RID: 1117
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000D0 RID: 208
		public struct DiscoverySettingsIni
		{
			// Token: 0x060007DB RID: 2011 RVA: 0x00014FFF File Offset: 0x000131FF
			internal DiscoverySettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060007DC RID: 2012 RVA: 0x00015008 File Offset: 0x00013208
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Discovery.settings.ini");
			}

			// Token: 0x060007DD RID: 2013 RVA: 0x0001501A File Offset: 0x0001321A
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Discovery.settings.ini", id);
			}

			// Token: 0x060007DE RID: 2014 RVA: 0x0001502D File Offset: 0x0001322D
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Discovery.settings.ini", id1, ids);
			}

			// Token: 0x17000585 RID: 1413
			// (get) Token: 0x060007DF RID: 2015 RVA: 0x00015041 File Offset: 0x00013241
			public ISettingsValue DiscoveryServerLookupConcurrency
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryServerLookupConcurrency");
				}
			}

			// Token: 0x17000586 RID: 1414
			// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00015058 File Offset: 0x00013258
			public ISettingsValue DiscoveryMaxAllowedExecutorItems
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryMaxAllowedExecutorItems");
				}
			}

			// Token: 0x17000587 RID: 1415
			// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0001506F File Offset: 0x0001326F
			public ISettingsValue DiscoveryKeywordsBatchSize
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryKeywordsBatchSize");
				}
			}

			// Token: 0x17000588 RID: 1416
			// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00015086 File Offset: 0x00013286
			public ISettingsValue DiscoveryExecutesInParallel
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryExecutesInParallel");
				}
			}

			// Token: 0x17000589 RID: 1417
			// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0001509D File Offset: 0x0001329D
			public IFeature UrlRebind
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Discovery.settings.ini", "UrlRebind");
				}
			}

			// Token: 0x1700058A RID: 1418
			// (get) Token: 0x060007E4 RID: 2020 RVA: 0x000150B4 File Offset: 0x000132B4
			public ISettingsValue DiscoveryDisplaySearchPageSize
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryDisplaySearchPageSize");
				}
			}

			// Token: 0x1700058B RID: 1419
			// (get) Token: 0x060007E5 RID: 2021 RVA: 0x000150CB File Offset: 0x000132CB
			public ISettingsValue DiscoveryLocalSearchConcurrency
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryLocalSearchConcurrency");
				}
			}

			// Token: 0x1700058C RID: 1420
			// (get) Token: 0x060007E6 RID: 2022 RVA: 0x000150E2 File Offset: 0x000132E2
			public ISettingsValue SearchTimeout
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "SearchTimeout");
				}
			}

			// Token: 0x1700058D RID: 1421
			// (get) Token: 0x060007E7 RID: 2023 RVA: 0x000150F9 File Offset: 0x000132F9
			public ISettingsValue ServiceTopologyTimeout
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "ServiceTopologyTimeout");
				}
			}

			// Token: 0x1700058E RID: 1422
			// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00015110 File Offset: 0x00013310
			public ISettingsValue DiscoveryDisplaySearchBatchSize
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryDisplaySearchBatchSize");
				}
			}

			// Token: 0x1700058F RID: 1423
			// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00015127 File Offset: 0x00013327
			public ISettingsValue DiscoveryDefaultPageSize
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryDefaultPageSize");
				}
			}

			// Token: 0x17000590 RID: 1424
			// (get) Token: 0x060007EA RID: 2026 RVA: 0x0001513E File Offset: 0x0001333E
			public ISettingsValue DiscoveryServerLookupBatch
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryServerLookupBatch");
				}
			}

			// Token: 0x17000591 RID: 1425
			// (get) Token: 0x060007EB RID: 2027 RVA: 0x00015155 File Offset: 0x00013355
			public ISettingsValue DiscoveryMaxAllowedResultsPageSize
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryMaxAllowedResultsPageSize");
				}
			}

			// Token: 0x17000592 RID: 1426
			// (get) Token: 0x060007EC RID: 2028 RVA: 0x0001516C File Offset: 0x0001336C
			public IFeature SearchScale
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Discovery.settings.ini", "SearchScale");
				}
			}

			// Token: 0x17000593 RID: 1427
			// (get) Token: 0x060007ED RID: 2029 RVA: 0x00015183 File Offset: 0x00013383
			public ISettingsValue MailboxServerLocatorTimeout
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "MailboxServerLocatorTimeout");
				}
			}

			// Token: 0x17000594 RID: 1428
			// (get) Token: 0x060007EE RID: 2030 RVA: 0x0001519A File Offset: 0x0001339A
			public ISettingsValue DiscoveryADPageSize
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryADPageSize");
				}
			}

			// Token: 0x17000595 RID: 1429
			// (get) Token: 0x060007EF RID: 2031 RVA: 0x000151B1 File Offset: 0x000133B1
			public ISettingsValue DiscoveryMailboxMaxProhibitSendReceiveQuota
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryMailboxMaxProhibitSendReceiveQuota");
				}
			}

			// Token: 0x17000596 RID: 1430
			// (get) Token: 0x060007F0 RID: 2032 RVA: 0x000151C8 File Offset: 0x000133C8
			public ISettingsValue DiscoveryFanoutConcurrency
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryFanoutConcurrency");
				}
			}

			// Token: 0x17000597 RID: 1431
			// (get) Token: 0x060007F1 RID: 2033 RVA: 0x000151DF File Offset: 0x000133DF
			public ISettingsValue DiscoveryExcludedFolders
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryExcludedFolders");
				}
			}

			// Token: 0x17000598 RID: 1432
			// (get) Token: 0x060007F2 RID: 2034 RVA: 0x000151F6 File Offset: 0x000133F6
			public ISettingsValue DiscoveryUseFastSearch
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryUseFastSearch");
				}
			}

			// Token: 0x17000599 RID: 1433
			// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0001520D File Offset: 0x0001340D
			public ISettingsValue DiscoveryFanoutBatch
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryFanoutBatch");
				}
			}

			// Token: 0x1700059A RID: 1434
			// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00015224 File Offset: 0x00013424
			public ISettingsValue DiscoveryLocalSearchIsParallel
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryLocalSearchIsParallel");
				}
			}

			// Token: 0x1700059B RID: 1435
			// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0001523B File Offset: 0x0001343B
			public ISettingsValue DiscoveryAggregateLogs
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryAggregateLogs");
				}
			}

			// Token: 0x1700059C RID: 1436
			// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00015252 File Offset: 0x00013452
			public ISettingsValue DiscoveryMailboxMaxProhibitSendQuota
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryMailboxMaxProhibitSendQuota");
				}
			}

			// Token: 0x1700059D RID: 1437
			// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00015269 File Offset: 0x00013469
			public ISettingsValue DiscoveryMaxAllowedMailboxQueriesPerRequest
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryMaxAllowedMailboxQueriesPerRequest");
				}
			}

			// Token: 0x1700059E RID: 1438
			// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00015280 File Offset: 0x00013480
			public ISettingsValue DiscoveryMaxMailboxes
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryMaxMailboxes");
				}
			}

			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00015297 File Offset: 0x00013497
			public ISettingsValue DiscoveryADLookupConcurrency
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryADLookupConcurrency");
				}
			}

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x060007FA RID: 2042 RVA: 0x000152AE File Offset: 0x000134AE
			public ISettingsValue DiscoveryExcludedFoldersEnabled
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("Discovery.settings.ini", "DiscoveryExcludedFoldersEnabled");
				}
			}

			// Token: 0x0400045E RID: 1118
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000D1 RID: 209
		public struct E4ESettingsIni
		{
			// Token: 0x060007FB RID: 2043 RVA: 0x000152C5 File Offset: 0x000134C5
			internal E4ESettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060007FC RID: 2044 RVA: 0x000152CE File Offset: 0x000134CE
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("E4E.settings.ini");
			}

			// Token: 0x060007FD RID: 2045 RVA: 0x000152E0 File Offset: 0x000134E0
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("E4E.settings.ini", id);
			}

			// Token: 0x060007FE RID: 2046 RVA: 0x000152F3 File Offset: 0x000134F3
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("E4E.settings.ini", id1, ids);
			}

			// Token: 0x170005A1 RID: 1441
			// (get) Token: 0x060007FF RID: 2047 RVA: 0x00015307 File Offset: 0x00013507
			public IFeature OTP
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("E4E.settings.ini", "OTP");
				}
			}

			// Token: 0x170005A2 RID: 1442
			// (get) Token: 0x06000800 RID: 2048 RVA: 0x0001531E File Offset: 0x0001351E
			public IVersion Version
			{
				get
				{
					return this.snapshot.GetObject<IVersion>("E4E.settings.ini", "Version");
				}
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x06000801 RID: 2049 RVA: 0x00015335 File Offset: 0x00013535
			public IFeature LogoffViaOwa
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("E4E.settings.ini", "LogoffViaOwa");
				}
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x06000802 RID: 2050 RVA: 0x0001534C File Offset: 0x0001354C
			public IFeature MsodsGraphQuery
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("E4E.settings.ini", "MsodsGraphQuery");
				}
			}

			// Token: 0x170005A5 RID: 1445
			// (get) Token: 0x06000803 RID: 2051 RVA: 0x00015363 File Offset: 0x00013563
			public IFeature E4E
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("E4E.settings.ini", "E4E");
				}
			}

			// Token: 0x170005A6 RID: 1446
			// (get) Token: 0x06000804 RID: 2052 RVA: 0x0001537A File Offset: 0x0001357A
			public IFeature TouchLayout
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("E4E.settings.ini", "TouchLayout");
				}
			}

			// Token: 0x0400045F RID: 1119
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000D2 RID: 210
		public struct EacSettingsIni
		{
			// Token: 0x06000805 RID: 2053 RVA: 0x00015391 File Offset: 0x00013591
			internal EacSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000806 RID: 2054 RVA: 0x0001539A File Offset: 0x0001359A
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Eac.settings.ini");
			}

			// Token: 0x06000807 RID: 2055 RVA: 0x000153AC File Offset: 0x000135AC
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Eac.settings.ini", id);
			}

			// Token: 0x06000808 RID: 2056 RVA: 0x000153BF File Offset: 0x000135BF
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Eac.settings.ini", id1, ids);
			}

			// Token: 0x170005A7 RID: 1447
			// (get) Token: 0x06000809 RID: 2057 RVA: 0x000153D3 File Offset: 0x000135D3
			public IFeature ManageMailboxAuditing
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "ManageMailboxAuditing");
				}
			}

			// Token: 0x170005A8 RID: 1448
			// (get) Token: 0x0600080A RID: 2058 RVA: 0x000153EA File Offset: 0x000135EA
			public IFeature UnifiedPolicy
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "UnifiedPolicy");
				}
			}

			// Token: 0x170005A9 RID: 1449
			// (get) Token: 0x0600080B RID: 2059 RVA: 0x00015401 File Offset: 0x00013601
			public IFeature DiscoverySearchStats
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "DiscoverySearchStats");
				}
			}

			// Token: 0x170005AA RID: 1450
			// (get) Token: 0x0600080C RID: 2060 RVA: 0x00015418 File Offset: 0x00013618
			public IFeature AllowRemoteOnboardingMovesOnly
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "AllowRemoteOnboardingMovesOnly");
				}
			}

			// Token: 0x170005AB RID: 1451
			// (get) Token: 0x0600080D RID: 2061 RVA: 0x0001542F File Offset: 0x0001362F
			public IFeature DlpFingerprint
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "DlpFingerprint");
				}
			}

			// Token: 0x170005AC RID: 1452
			// (get) Token: 0x0600080E RID: 2062 RVA: 0x00015446 File Offset: 0x00013646
			public IFeature GeminiShell
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "GeminiShell");
				}
			}

			// Token: 0x170005AD RID: 1453
			// (get) Token: 0x0600080F RID: 2063 RVA: 0x0001545D File Offset: 0x0001365D
			public IFeature DevicePolicyMgmtUI
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "DevicePolicyMgmtUI");
				}
			}

			// Token: 0x170005AE RID: 1454
			// (get) Token: 0x06000810 RID: 2064 RVA: 0x00015474 File Offset: 0x00013674
			public IFeature UnifiedAuditPolicy
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "UnifiedAuditPolicy");
				}
			}

			// Token: 0x170005AF RID: 1455
			// (get) Token: 0x06000811 RID: 2065 RVA: 0x0001548B File Offset: 0x0001368B
			public IFeature EACClientAccessRulesEnabled
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "EACClientAccessRulesEnabled");
				}
			}

			// Token: 0x170005B0 RID: 1456
			// (get) Token: 0x06000812 RID: 2066 RVA: 0x000154A2 File Offset: 0x000136A2
			public IFeature RemoteDomain
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "RemoteDomain");
				}
			}

			// Token: 0x170005B1 RID: 1457
			// (get) Token: 0x06000813 RID: 2067 RVA: 0x000154B9 File Offset: 0x000136B9
			public IFeature CmdletLogging
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "CmdletLogging");
				}
			}

			// Token: 0x170005B2 RID: 1458
			// (get) Token: 0x06000814 RID: 2068 RVA: 0x000154D0 File Offset: 0x000136D0
			public IFeature UnifiedComplianceCenter
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "UnifiedComplianceCenter");
				}
			}

			// Token: 0x170005B3 RID: 1459
			// (get) Token: 0x06000815 RID: 2069 RVA: 0x000154E7 File Offset: 0x000136E7
			public IFeature Office365DIcon
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "Office365DIcon");
				}
			}

			// Token: 0x170005B4 RID: 1460
			// (get) Token: 0x06000816 RID: 2070 RVA: 0x000154FE File Offset: 0x000136FE
			public IFeature DiscoveryPFSearch
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "DiscoveryPFSearch");
				}
			}

			// Token: 0x170005B5 RID: 1461
			// (get) Token: 0x06000817 RID: 2071 RVA: 0x00015515 File Offset: 0x00013715
			public IFeature ModernGroups
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "ModernGroups");
				}
			}

			// Token: 0x170005B6 RID: 1462
			// (get) Token: 0x06000818 RID: 2072 RVA: 0x0001552C File Offset: 0x0001372C
			public IFeature OrgIdADSeverSettings
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "OrgIdADSeverSettings");
				}
			}

			// Token: 0x170005B7 RID: 1463
			// (get) Token: 0x06000819 RID: 2073 RVA: 0x00015543 File Offset: 0x00013743
			public IFeature UCCPermissions
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "UCCPermissions");
				}
			}

			// Token: 0x170005B8 RID: 1464
			// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001555A File Offset: 0x0001375A
			public IFeature AllowMailboxArchiveOnlyMigration
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "AllowMailboxArchiveOnlyMigration");
				}
			}

			// Token: 0x170005B9 RID: 1465
			// (get) Token: 0x0600081B RID: 2075 RVA: 0x00015571 File Offset: 0x00013771
			public IFeature DiscoveryDocIdHint
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "DiscoveryDocIdHint");
				}
			}

			// Token: 0x170005BA RID: 1466
			// (get) Token: 0x0600081C RID: 2076 RVA: 0x00015588 File Offset: 0x00013788
			public IUrl AdminHomePage
			{
				get
				{
					return this.snapshot.GetObject<IUrl>("Eac.settings.ini", "AdminHomePage");
				}
			}

			// Token: 0x170005BB RID: 1467
			// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001559F File Offset: 0x0001379F
			public IFeature CrossPremiseMigration
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "CrossPremiseMigration");
				}
			}

			// Token: 0x170005BC RID: 1468
			// (get) Token: 0x0600081E RID: 2078 RVA: 0x000155B6 File Offset: 0x000137B6
			public IFeature UCCAuditReports
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "UCCAuditReports");
				}
			}

			// Token: 0x170005BD RID: 1469
			// (get) Token: 0x0600081F RID: 2079 RVA: 0x000155CD File Offset: 0x000137CD
			public IFeature UnlistedServices
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "UnlistedServices");
				}
			}

			// Token: 0x170005BE RID: 1470
			// (get) Token: 0x06000820 RID: 2080 RVA: 0x000155E4 File Offset: 0x000137E4
			public IFeature BulkPermissionAddRemove
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Eac.settings.ini", "BulkPermissionAddRemove");
				}
			}

			// Token: 0x04000460 RID: 1120
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000D3 RID: 211
		public struct EwsSettingsIni
		{
			// Token: 0x06000821 RID: 2081 RVA: 0x000155FB File Offset: 0x000137FB
			internal EwsSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000822 RID: 2082 RVA: 0x00015604 File Offset: 0x00013804
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Ews.settings.ini");
			}

			// Token: 0x06000823 RID: 2083 RVA: 0x00015616 File Offset: 0x00013816
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Ews.settings.ini", id);
			}

			// Token: 0x06000824 RID: 2084 RVA: 0x00015629 File Offset: 0x00013829
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Ews.settings.ini", id1, ids);
			}

			// Token: 0x170005BF RID: 1471
			// (get) Token: 0x06000825 RID: 2085 RVA: 0x0001563D File Offset: 0x0001383D
			public IFeature AutoSubscribeNewGroupMembers
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "AutoSubscribeNewGroupMembers");
				}
			}

			// Token: 0x170005C0 RID: 1472
			// (get) Token: 0x06000826 RID: 2086 RVA: 0x00015654 File Offset: 0x00013854
			public IFeature OnlineArchive
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "OnlineArchive");
				}
			}

			// Token: 0x170005C1 RID: 1473
			// (get) Token: 0x06000827 RID: 2087 RVA: 0x0001566B File Offset: 0x0001386B
			public IFeature UserPasswordExpirationDate
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "UserPasswordExpirationDate");
				}
			}

			// Token: 0x170005C2 RID: 1474
			// (get) Token: 0x06000828 RID: 2088 RVA: 0x00015682 File Offset: 0x00013882
			public IFeature InstantSearchFoldersForPublicFolders
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "InstantSearchFoldersForPublicFolders");
				}
			}

			// Token: 0x170005C3 RID: 1475
			// (get) Token: 0x06000829 RID: 2089 RVA: 0x00015699 File Offset: 0x00013899
			public IFeature LinkedAccountTokenMunging
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "LinkedAccountTokenMunging");
				}
			}

			// Token: 0x170005C4 RID: 1476
			// (get) Token: 0x0600082A RID: 2090 RVA: 0x000156B0 File Offset: 0x000138B0
			public IFeature EwsServiceCredentials
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "EwsServiceCredentials");
				}
			}

			// Token: 0x170005C5 RID: 1477
			// (get) Token: 0x0600082B RID: 2091 RVA: 0x000156C7 File Offset: 0x000138C7
			public IFeature ExternalUser
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "ExternalUser");
				}
			}

			// Token: 0x170005C6 RID: 1478
			// (get) Token: 0x0600082C RID: 2092 RVA: 0x000156DE File Offset: 0x000138DE
			public IFeature UseInternalEwsUrlForExtensionEwsProxyInOwa
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "UseInternalEwsUrlForExtensionEwsProxyInOwa");
				}
			}

			// Token: 0x170005C7 RID: 1479
			// (get) Token: 0x0600082D RID: 2093 RVA: 0x000156F5 File Offset: 0x000138F5
			public IFeature EwsClientAccessRulesEnabled
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "EwsClientAccessRulesEnabled");
				}
			}

			// Token: 0x170005C8 RID: 1480
			// (get) Token: 0x0600082E RID: 2094 RVA: 0x0001570C File Offset: 0x0001390C
			public IFeature LongRunningScenarioThrottling
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "LongRunningScenarioThrottling");
				}
			}

			// Token: 0x170005C9 RID: 1481
			// (get) Token: 0x0600082F RID: 2095 RVA: 0x00015723 File Offset: 0x00013923
			public IFeature HttpProxyToCafe
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "HttpProxyToCafe");
				}
			}

			// Token: 0x170005CA RID: 1482
			// (get) Token: 0x06000830 RID: 2096 RVA: 0x0001573A File Offset: 0x0001393A
			public IFeature OData
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "OData");
				}
			}

			// Token: 0x170005CB RID: 1483
			// (get) Token: 0x06000831 RID: 2097 RVA: 0x00015751 File Offset: 0x00013951
			public IFeature EwsHttpHandler
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "EwsHttpHandler");
				}
			}

			// Token: 0x170005CC RID: 1484
			// (get) Token: 0x06000832 RID: 2098 RVA: 0x00015768 File Offset: 0x00013968
			public IFeature WsPerformanceCounters
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "WsPerformanceCounters");
				}
			}

			// Token: 0x170005CD RID: 1485
			// (get) Token: 0x06000833 RID: 2099 RVA: 0x0001577F File Offset: 0x0001397F
			public IFeature CreateUnifiedMailbox
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ews.settings.ini", "CreateUnifiedMailbox");
				}
			}

			// Token: 0x04000461 RID: 1121
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000D4 RID: 212
		public struct GlobalSettingsIni
		{
			// Token: 0x06000834 RID: 2100 RVA: 0x00015796 File Offset: 0x00013996
			internal GlobalSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000835 RID: 2101 RVA: 0x0001579F File Offset: 0x0001399F
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Global.settings.ini");
			}

			// Token: 0x06000836 RID: 2102 RVA: 0x000157B1 File Offset: 0x000139B1
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Global.settings.ini", id);
			}

			// Token: 0x06000837 RID: 2103 RVA: 0x000157C4 File Offset: 0x000139C4
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Global.settings.ini", id1, ids);
			}

			// Token: 0x170005CE RID: 1486
			// (get) Token: 0x06000838 RID: 2104 RVA: 0x000157D8 File Offset: 0x000139D8
			public IFeature GlobalCriminalCompliance
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Global.settings.ini", "GlobalCriminalCompliance");
				}
			}

			// Token: 0x170005CF RID: 1487
			// (get) Token: 0x06000839 RID: 2105 RVA: 0x000157EF File Offset: 0x000139EF
			public IFeature WindowsLiveID
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Global.settings.ini", "WindowsLiveID");
				}
			}

			// Token: 0x170005D0 RID: 1488
			// (get) Token: 0x0600083A RID: 2106 RVA: 0x00015806 File Offset: 0x00013A06
			public IFeature DistributedKeyManagement
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Global.settings.ini", "DistributedKeyManagement");
				}
			}

			// Token: 0x170005D1 RID: 1489
			// (get) Token: 0x0600083B RID: 2107 RVA: 0x0001581D File Offset: 0x00013A1D
			public IFeature MultiTenancy
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Global.settings.ini", "MultiTenancy");
				}
			}

			// Token: 0x04000462 RID: 1122
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000D5 RID: 213
		public struct HighAvailabilitySettingsIni
		{
			// Token: 0x0600083C RID: 2108 RVA: 0x00015834 File Offset: 0x00013A34
			internal HighAvailabilitySettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x0600083D RID: 2109 RVA: 0x0001583D File Offset: 0x00013A3D
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("HighAvailability.settings.ini");
			}

			// Token: 0x0600083E RID: 2110 RVA: 0x0001584F File Offset: 0x00013A4F
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("HighAvailability.settings.ini", id);
			}

			// Token: 0x0600083F RID: 2111 RVA: 0x00015862 File Offset: 0x00013A62
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("HighAvailability.settings.ini", id1, ids);
			}

			// Token: 0x170005D2 RID: 1490
			// (get) Token: 0x06000840 RID: 2112 RVA: 0x00015876 File Offset: 0x00013A76
			public IActiveManagerSettings ActiveManager
			{
				get
				{
					return this.snapshot.GetObject<IActiveManagerSettings>("HighAvailability.settings.ini", "ActiveManager");
				}
			}

			// Token: 0x04000463 RID: 1123
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000D6 RID: 214
		public struct HolidayCalendarsSettingsIni
		{
			// Token: 0x06000841 RID: 2113 RVA: 0x0001588D File Offset: 0x00013A8D
			internal HolidayCalendarsSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000842 RID: 2114 RVA: 0x00015896 File Offset: 0x00013A96
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("HolidayCalendars.settings.ini");
			}

			// Token: 0x06000843 RID: 2115 RVA: 0x000158A8 File Offset: 0x00013AA8
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("HolidayCalendars.settings.ini", id);
			}

			// Token: 0x06000844 RID: 2116 RVA: 0x000158BB File Offset: 0x00013ABB
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("HolidayCalendars.settings.ini", id1, ids);
			}

			// Token: 0x170005D3 RID: 1491
			// (get) Token: 0x06000845 RID: 2117 RVA: 0x000158CF File Offset: 0x00013ACF
			public IHostSettings HostConfiguration
			{
				get
				{
					return this.snapshot.GetObject<IHostSettings>("HolidayCalendars.settings.ini", "HostConfiguration");
				}
			}

			// Token: 0x04000464 RID: 1124
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000D7 RID: 215
		public struct HxSettingsIni
		{
			// Token: 0x06000846 RID: 2118 RVA: 0x000158E6 File Offset: 0x00013AE6
			internal HxSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000847 RID: 2119 RVA: 0x000158EF File Offset: 0x00013AEF
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Hx.settings.ini");
			}

			// Token: 0x06000848 RID: 2120 RVA: 0x00015901 File Offset: 0x00013B01
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Hx.settings.ini", id);
			}

			// Token: 0x06000849 RID: 2121 RVA: 0x00015914 File Offset: 0x00013B14
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Hx.settings.ini", id1, ids);
			}

			// Token: 0x170005D4 RID: 1492
			// (get) Token: 0x0600084A RID: 2122 RVA: 0x00015928 File Offset: 0x00013B28
			public IFeature SmartSyncWebSockets
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Hx.settings.ini", "SmartSyncWebSockets");
				}
			}

			// Token: 0x170005D5 RID: 1493
			// (get) Token: 0x0600084B RID: 2123 RVA: 0x0001593F File Offset: 0x00013B3F
			public IFeature EnforceDevicePolicy
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Hx.settings.ini", "EnforceDevicePolicy");
				}
			}

			// Token: 0x170005D6 RID: 1494
			// (get) Token: 0x0600084C RID: 2124 RVA: 0x00015956 File Offset: 0x00013B56
			public IFeature Irm
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Hx.settings.ini", "Irm");
				}
			}

			// Token: 0x170005D7 RID: 1495
			// (get) Token: 0x0600084D RID: 2125 RVA: 0x0001596D File Offset: 0x00013B6D
			public IFeature ServiceAvailable
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Hx.settings.ini", "ServiceAvailable");
				}
			}

			// Token: 0x170005D8 RID: 1496
			// (get) Token: 0x0600084E RID: 2126 RVA: 0x00015984 File Offset: 0x00013B84
			public IFeature ClientSettingsPane
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Hx.settings.ini", "ClientSettingsPane");
				}
			}

			// Token: 0x04000465 RID: 1125
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000D8 RID: 216
		public struct ImapSettingsIni
		{
			// Token: 0x0600084F RID: 2127 RVA: 0x0001599B File Offset: 0x00013B9B
			internal ImapSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000850 RID: 2128 RVA: 0x000159A4 File Offset: 0x00013BA4
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Imap.settings.ini");
			}

			// Token: 0x06000851 RID: 2129 RVA: 0x000159B6 File Offset: 0x00013BB6
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Imap.settings.ini", id);
			}

			// Token: 0x06000852 RID: 2130 RVA: 0x000159C9 File Offset: 0x00013BC9
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Imap.settings.ini", id1, ids);
			}

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x06000853 RID: 2131 RVA: 0x000159DD File Offset: 0x00013BDD
			public IFeature RfcIDImap
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "RfcIDImap");
				}
			}

			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x06000854 RID: 2132 RVA: 0x000159F4 File Offset: 0x00013BF4
			public IFeature IgnoreNonProvisionedServers
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "IgnoreNonProvisionedServers");
				}
			}

			// Token: 0x170005DB RID: 1499
			// (get) Token: 0x06000855 RID: 2133 RVA: 0x00015A0B File Offset: 0x00013C0B
			public IFeature UseSamAccountNameAsUsername
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "UseSamAccountNameAsUsername");
				}
			}

			// Token: 0x170005DC RID: 1500
			// (get) Token: 0x06000856 RID: 2134 RVA: 0x00015A22 File Offset: 0x00013C22
			public IFeature SkipAuthOnCafe
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "SkipAuthOnCafe");
				}
			}

			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x06000857 RID: 2135 RVA: 0x00015A39 File Offset: 0x00013C39
			public IFeature AllowPlainTextConversionWithoutUsingSkeleton
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "AllowPlainTextConversionWithoutUsingSkeleton");
				}
			}

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x06000858 RID: 2136 RVA: 0x00015A50 File Offset: 0x00013C50
			public IFeature RfcIDImapCafe
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "RfcIDImapCafe");
				}
			}

			// Token: 0x170005DF RID: 1503
			// (get) Token: 0x06000859 RID: 2137 RVA: 0x00015A67 File Offset: 0x00013C67
			public IFeature GlobalCriminalCompliance
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "GlobalCriminalCompliance");
				}
			}

			// Token: 0x170005E0 RID: 1504
			// (get) Token: 0x0600085A RID: 2138 RVA: 0x00015A7E File Offset: 0x00013C7E
			public IFeature CheckOnlyAuthenticationStatus
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "CheckOnlyAuthenticationStatus");
				}
			}

			// Token: 0x170005E1 RID: 1505
			// (get) Token: 0x0600085B RID: 2139 RVA: 0x00015A95 File Offset: 0x00013C95
			public IFeature RfcMoveImap
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "RfcMoveImap");
				}
			}

			// Token: 0x170005E2 RID: 1506
			// (get) Token: 0x0600085C RID: 2140 RVA: 0x00015AAC File Offset: 0x00013CAC
			public IFeature RefreshSearchFolderItems
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "RefreshSearchFolderItems");
				}
			}

			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x0600085D RID: 2141 RVA: 0x00015AC3 File Offset: 0x00013CC3
			public IFeature ReloadMailboxBeforeGettingSubscriptionList
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "ReloadMailboxBeforeGettingSubscriptionList");
				}
			}

			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x0600085E RID: 2142 RVA: 0x00015ADA File Offset: 0x00013CDA
			public IFeature EnforceLogsRetentionPolicy
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "EnforceLogsRetentionPolicy");
				}
			}

			// Token: 0x170005E5 RID: 1509
			// (get) Token: 0x0600085F RID: 2143 RVA: 0x00015AF1 File Offset: 0x00013CF1
			public IFeature AppendServerNameInBanner
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "AppendServerNameInBanner");
				}
			}

			// Token: 0x170005E6 RID: 1510
			// (get) Token: 0x06000860 RID: 2144 RVA: 0x00015B08 File Offset: 0x00013D08
			public IFeature UsePrimarySmtpAddress
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "UsePrimarySmtpAddress");
				}
			}

			// Token: 0x170005E7 RID: 1511
			// (get) Token: 0x06000861 RID: 2145 RVA: 0x00015B1F File Offset: 0x00013D1F
			public IFeature ImapClientAccessRulesEnabled
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "ImapClientAccessRulesEnabled");
				}
			}

			// Token: 0x170005E8 RID: 1512
			// (get) Token: 0x06000862 RID: 2146 RVA: 0x00015B36 File Offset: 0x00013D36
			public IFeature DontReturnLastMessageForUInt32MaxValue
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "DontReturnLastMessageForUInt32MaxValue");
				}
			}

			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x06000863 RID: 2147 RVA: 0x00015B4D File Offset: 0x00013D4D
			public IFeature LrsLogging
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "LrsLogging");
				}
			}

			// Token: 0x170005EA RID: 1514
			// (get) Token: 0x06000864 RID: 2148 RVA: 0x00015B64 File Offset: 0x00013D64
			public IFeature AllowKerberosAuth
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "AllowKerberosAuth");
				}
			}

			// Token: 0x170005EB RID: 1515
			// (get) Token: 0x06000865 RID: 2149 RVA: 0x00015B7B File Offset: 0x00013D7B
			public IFeature RfcMoveImapCafe
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Imap.settings.ini", "RfcMoveImapCafe");
				}
			}

			// Token: 0x04000466 RID: 1126
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000D9 RID: 217
		public struct InferenceSettingsIni
		{
			// Token: 0x06000866 RID: 2150 RVA: 0x00015B92 File Offset: 0x00013D92
			internal InferenceSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000867 RID: 2151 RVA: 0x00015B9B File Offset: 0x00013D9B
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Inference.settings.ini");
			}

			// Token: 0x06000868 RID: 2152 RVA: 0x00015BAD File Offset: 0x00013DAD
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Inference.settings.ini", id);
			}

			// Token: 0x06000869 RID: 2153 RVA: 0x00015BC0 File Offset: 0x00013DC0
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Inference.settings.ini", id1, ids);
			}

			// Token: 0x170005EC RID: 1516
			// (get) Token: 0x0600086A RID: 2154 RVA: 0x00015BD4 File Offset: 0x00013DD4
			public IFeature ActivityLogging
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Inference.settings.ini", "ActivityLogging");
				}
			}

			// Token: 0x170005ED RID: 1517
			// (get) Token: 0x0600086B RID: 2155 RVA: 0x00015BEB File Offset: 0x00013DEB
			public IInferenceTrainingConfigurationSettings InferenceTrainingConfigurationSettings
			{
				get
				{
					return this.snapshot.GetObject<IInferenceTrainingConfigurationSettings>("Inference.settings.ini", "InferenceTrainingConfigurationSettings");
				}
			}

			// Token: 0x170005EE RID: 1518
			// (get) Token: 0x0600086C RID: 2156 RVA: 0x00015C02 File Offset: 0x00013E02
			public IFeature InferenceGroupingModel
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Inference.settings.ini", "InferenceGroupingModel");
				}
			}

			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x0600086D RID: 2157 RVA: 0x00015C19 File Offset: 0x00013E19
			public IFeature InferenceLatentLabelModel
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Inference.settings.ini", "InferenceLatentLabelModel");
				}
			}

			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x0600086E RID: 2158 RVA: 0x00015C30 File Offset: 0x00013E30
			public IFeature InferenceClutterInvitation
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Inference.settings.ini", "InferenceClutterInvitation");
				}
			}

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x0600086F RID: 2159 RVA: 0x00015C47 File Offset: 0x00013E47
			public IFeature InferenceEventBasedAssistant
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Inference.settings.ini", "InferenceEventBasedAssistant");
				}
			}

			// Token: 0x170005F2 RID: 1522
			// (get) Token: 0x06000870 RID: 2160 RVA: 0x00015C5E File Offset: 0x00013E5E
			public IFeature InferenceAutoEnableClutter
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Inference.settings.ini", "InferenceAutoEnableClutter");
				}
			}

			// Token: 0x170005F3 RID: 1523
			// (get) Token: 0x06000871 RID: 2161 RVA: 0x00015C75 File Offset: 0x00013E75
			public IClutterModelConfigurationSettings InferenceClutterModelConfigurationSettings
			{
				get
				{
					return this.snapshot.GetObject<IClutterModelConfigurationSettings>("Inference.settings.ini", "InferenceClutterModelConfigurationSettings");
				}
			}

			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x06000872 RID: 2162 RVA: 0x00015C8C File Offset: 0x00013E8C
			public IClutterDataSelectionSettings InferenceClutterDataSelectionSettings
			{
				get
				{
					return this.snapshot.GetObject<IClutterDataSelectionSettings>("Inference.settings.ini", "InferenceClutterDataSelectionSettings");
				}
			}

			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x06000873 RID: 2163 RVA: 0x00015CA3 File Offset: 0x00013EA3
			public IFeature InferenceClutterAutoEnablementNotice
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Inference.settings.ini", "InferenceClutterAutoEnablementNotice");
				}
			}

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x06000874 RID: 2164 RVA: 0x00015CBA File Offset: 0x00013EBA
			public IFeature InferenceModelComparison
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Inference.settings.ini", "InferenceModelComparison");
				}
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x06000875 RID: 2165 RVA: 0x00015CD1 File Offset: 0x00013ED1
			public IFeature InferenceFolderBasedClutter
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Inference.settings.ini", "InferenceFolderBasedClutter");
				}
			}

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x06000876 RID: 2166 RVA: 0x00015CE8 File Offset: 0x00013EE8
			public IFeature InferenceStampTracking
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Inference.settings.ini", "InferenceStampTracking");
				}
			}

			// Token: 0x04000467 RID: 1127
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000DA RID: 218
		public struct IpaedSettingsIni
		{
			// Token: 0x06000877 RID: 2167 RVA: 0x00015CFF File Offset: 0x00013EFF
			internal IpaedSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000878 RID: 2168 RVA: 0x00015D08 File Offset: 0x00013F08
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Ipaed.settings.ini");
			}

			// Token: 0x06000879 RID: 2169 RVA: 0x00015D1A File Offset: 0x00013F1A
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Ipaed.settings.ini", id);
			}

			// Token: 0x0600087A RID: 2170 RVA: 0x00015D2D File Offset: 0x00013F2D
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Ipaed.settings.ini", id1, ids);
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x0600087B RID: 2171 RVA: 0x00015D41 File Offset: 0x00013F41
			public IFeature ProcessedByUnjournal
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "ProcessedByUnjournal");
				}
			}

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x0600087C RID: 2172 RVA: 0x00015D58 File Offset: 0x00013F58
			public IFeature ProcessForestWideOrgJournal
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "ProcessForestWideOrgJournal");
				}
			}

			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x0600087D RID: 2173 RVA: 0x00015D6F File Offset: 0x00013F6F
			public IFeature MoveDeletionsToPurges
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "MoveDeletionsToPurges");
				}
			}

			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x0600087E RID: 2174 RVA: 0x00015D86 File Offset: 0x00013F86
			public IFeature InternalJournaling
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "InternalJournaling");
				}
			}

			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x0600087F RID: 2175 RVA: 0x00015D9D File Offset: 0x00013F9D
			public IFeature IncreaseQuotaForOnHoldMailboxes
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "IncreaseQuotaForOnHoldMailboxes");
				}
			}

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x06000880 RID: 2176 RVA: 0x00015DB4 File Offset: 0x00013FB4
			public IFeature AdminAuditLocalQueue
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "AdminAuditLocalQueue");
				}
			}

			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x06000881 RID: 2177 RVA: 0x00015DCB File Offset: 0x00013FCB
			public IFeature AdminAuditCmdletBlockList
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "AdminAuditCmdletBlockList");
				}
			}

			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x06000882 RID: 2178 RVA: 0x00015DE2 File Offset: 0x00013FE2
			public IFeature AdminAuditEventLogThrottling
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "AdminAuditEventLogThrottling");
				}
			}

			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x06000883 RID: 2179 RVA: 0x00015DF9 File Offset: 0x00013FF9
			public IFeature AuditConfigFromUCCPolicy
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "AuditConfigFromUCCPolicy");
				}
			}

			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x06000884 RID: 2180 RVA: 0x00015E10 File Offset: 0x00014010
			public IFeature PartitionedMailboxAuditLogs
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "PartitionedMailboxAuditLogs");
				}
			}

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x06000885 RID: 2181 RVA: 0x00015E27 File Offset: 0x00014027
			public IFeature MailboxAuditLocalQueue
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "MailboxAuditLocalQueue");
				}
			}

			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x06000886 RID: 2182 RVA: 0x00015E3E File Offset: 0x0001403E
			public IFeature RemoveMailboxFromJournalRecipients
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "RemoveMailboxFromJournalRecipients");
				}
			}

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x06000887 RID: 2183 RVA: 0x00015E55 File Offset: 0x00014055
			public IFeature MoveClearNrn
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "MoveClearNrn");
				}
			}

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x06000888 RID: 2184 RVA: 0x00015E6C File Offset: 0x0001406C
			public IFeature FolderBindExtendedThrottling
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "FolderBindExtendedThrottling");
				}
			}

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x06000889 RID: 2185 RVA: 0x00015E83 File Offset: 0x00014083
			public IFeature PartitionedAdminAuditLogs
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "PartitionedAdminAuditLogs");
				}
			}

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x0600088A RID: 2186 RVA: 0x00015E9A File Offset: 0x0001409A
			public IFeature AdminAuditExternalAccessCheckOnDedicated
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "AdminAuditExternalAccessCheckOnDedicated");
				}
			}

			// Token: 0x17000609 RID: 1545
			// (get) Token: 0x0600088B RID: 2187 RVA: 0x00015EB1 File Offset: 0x000140B1
			public IFeature LegacyJournaling
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "LegacyJournaling");
				}
			}

			// Token: 0x1700060A RID: 1546
			// (get) Token: 0x0600088C RID: 2188 RVA: 0x00015EC8 File Offset: 0x000140C8
			public IFeature EHAJournaling
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Ipaed.settings.ini", "EHAJournaling");
				}
			}

			// Token: 0x04000468 RID: 1128
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000DB RID: 219
		public struct MailboxAssistantsSettingsIni
		{
			// Token: 0x0600088D RID: 2189 RVA: 0x00015EDF File Offset: 0x000140DF
			internal MailboxAssistantsSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x0600088E RID: 2190 RVA: 0x00015EE8 File Offset: 0x000140E8
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("MailboxAssistants.settings.ini");
			}

			// Token: 0x0600088F RID: 2191 RVA: 0x00015EFA File Offset: 0x000140FA
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("MailboxAssistants.settings.ini", id);
			}

			// Token: 0x06000890 RID: 2192 RVA: 0x00015F0D File Offset: 0x0001410D
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("MailboxAssistants.settings.ini", id1, ids);
			}

			// Token: 0x1700060B RID: 1547
			// (get) Token: 0x06000891 RID: 2193 RVA: 0x00015F21 File Offset: 0x00014121
			public IFeature FlagPlus
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "FlagPlus");
				}
			}

			// Token: 0x1700060C RID: 1548
			// (get) Token: 0x06000892 RID: 2194 RVA: 0x00015F38 File Offset: 0x00014138
			public IFeature ApprovalAssistantCheckRateLimit
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "ApprovalAssistantCheckRateLimit");
				}
			}

			// Token: 0x1700060D RID: 1549
			// (get) Token: 0x06000893 RID: 2195 RVA: 0x00015F4F File Offset: 0x0001414F
			public IMailboxAssistantSettings StoreUrgentMaintenanceAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "StoreUrgentMaintenanceAssistant");
				}
			}

			// Token: 0x1700060E RID: 1550
			// (get) Token: 0x06000894 RID: 2196 RVA: 0x00015F66 File Offset: 0x00014166
			public IMailboxAssistantSettings SharePointSignalStoreAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "SharePointSignalStoreAssistant");
				}
			}

			// Token: 0x1700060F RID: 1551
			// (get) Token: 0x06000895 RID: 2197 RVA: 0x00015F7D File Offset: 0x0001417D
			public IMailboxAssistantSettings StoreOnlineIntegrityCheckAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "StoreOnlineIntegrityCheckAssistant");
				}
			}

			// Token: 0x17000610 RID: 1552
			// (get) Token: 0x06000896 RID: 2198 RVA: 0x00015F94 File Offset: 0x00014194
			public IFeature DirectoryProcessorTenantLogging
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "DirectoryProcessorTenantLogging");
				}
			}

			// Token: 0x17000611 RID: 1553
			// (get) Token: 0x06000897 RID: 2199 RVA: 0x00015FAB File Offset: 0x000141AB
			public ICalendarRepairLoggerSettings CalendarRepairAssistantLogging
			{
				get
				{
					return this.snapshot.GetObject<ICalendarRepairLoggerSettings>("MailboxAssistants.settings.ini", "CalendarRepairAssistantLogging");
				}
			}

			// Token: 0x17000612 RID: 1554
			// (get) Token: 0x06000898 RID: 2200 RVA: 0x00015FC2 File Offset: 0x000141C2
			public IFeature CalendarNotificationAssistantSkipUserSettingsUpdate
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "CalendarNotificationAssistantSkipUserSettingsUpdate");
				}
			}

			// Token: 0x17000613 RID: 1555
			// (get) Token: 0x06000899 RID: 2201 RVA: 0x00015FD9 File Offset: 0x000141D9
			public IFeature ElcAssistantTryProcessEhaMigratedMessages
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "ElcAssistantTryProcessEhaMigratedMessages");
				}
			}

			// Token: 0x17000614 RID: 1556
			// (get) Token: 0x0600089A RID: 2202 RVA: 0x00015FF0 File Offset: 0x000141F0
			public IMailboxAssistantSettings InferenceDataCollectionAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "InferenceDataCollectionAssistant");
				}
			}

			// Token: 0x17000615 RID: 1557
			// (get) Token: 0x0600089B RID: 2203 RVA: 0x00016007 File Offset: 0x00014207
			public IMailboxAssistantSettings SearchIndexRepairAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "SearchIndexRepairAssistant");
				}
			}

			// Token: 0x17000616 RID: 1558
			// (get) Token: 0x0600089C RID: 2204 RVA: 0x0001601E File Offset: 0x0001421E
			public IFeature TimeBasedAssistantsMonitoring
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "TimeBasedAssistantsMonitoring");
				}
			}

			// Token: 0x17000617 RID: 1559
			// (get) Token: 0x0600089D RID: 2205 RVA: 0x00016035 File Offset: 0x00014235
			public IMailboxAssistantSettings TestTBA
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "TestTBA");
				}
			}

			// Token: 0x17000618 RID: 1560
			// (get) Token: 0x0600089E RID: 2206 RVA: 0x0001604C File Offset: 0x0001424C
			public IFeature OrgMailboxCheckScaleRequirements
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "OrgMailboxCheckScaleRequirements");
				}
			}

			// Token: 0x17000619 RID: 1561
			// (get) Token: 0x0600089F RID: 2207 RVA: 0x00016063 File Offset: 0x00014263
			public IMailboxAssistantSettings PeopleRelevanceAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "PeopleRelevanceAssistant");
				}
			}

			// Token: 0x1700061A RID: 1562
			// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0001607A File Offset: 0x0001427A
			public IMailboxAssistantSettings PublicFolderAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "PublicFolderAssistant");
				}
			}

			// Token: 0x1700061B RID: 1563
			// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00016091 File Offset: 0x00014291
			public IMailboxAssistantSettings ElcAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "ElcAssistant");
				}
			}

			// Token: 0x1700061C RID: 1564
			// (get) Token: 0x060008A2 RID: 2210 RVA: 0x000160A8 File Offset: 0x000142A8
			public IFeature ElcRemoteArchive
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "ElcRemoteArchive");
				}
			}

			// Token: 0x1700061D RID: 1565
			// (get) Token: 0x060008A3 RID: 2211 RVA: 0x000160BF File Offset: 0x000142BF
			public IFeature PublicFolderSplit
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "PublicFolderSplit");
				}
			}

			// Token: 0x1700061E RID: 1566
			// (get) Token: 0x060008A4 RID: 2212 RVA: 0x000160D6 File Offset: 0x000142D6
			public IMailboxAssistantSettings InferenceTrainingAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "InferenceTrainingAssistant");
				}
			}

			// Token: 0x1700061F RID: 1567
			// (get) Token: 0x060008A5 RID: 2213 RVA: 0x000160ED File Offset: 0x000142ED
			public IFeature SharePointSignalStore
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "SharePointSignalStore");
				}
			}

			// Token: 0x17000620 RID: 1568
			// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00016104 File Offset: 0x00014304
			public IMailboxAssistantSettings ProbeTimeBasedAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "ProbeTimeBasedAssistant");
				}
			}

			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0001611B File Offset: 0x0001431B
			public IFeature SharePointSignalStoreInDatacenter
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "SharePointSignalStoreInDatacenter");
				}
			}

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00016132 File Offset: 0x00014332
			public IFeature GenerateGroupPhoto
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "GenerateGroupPhoto");
				}
			}

			// Token: 0x17000623 RID: 1571
			// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00016149 File Offset: 0x00014349
			public IMailboxAssistantSettings StoreMaintenanceAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "StoreMaintenanceAssistant");
				}
			}

			// Token: 0x17000624 RID: 1572
			// (get) Token: 0x060008AA RID: 2218 RVA: 0x00016160 File Offset: 0x00014360
			public IMailboxAssistantSettings CalendarSyncAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "CalendarSyncAssistant");
				}
			}

			// Token: 0x17000625 RID: 1573
			// (get) Token: 0x060008AB RID: 2219 RVA: 0x00016177 File Offset: 0x00014377
			public IFeature EmailReminders
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "EmailReminders");
				}
			}

			// Token: 0x17000626 RID: 1574
			// (get) Token: 0x060008AC RID: 2220 RVA: 0x0001618E File Offset: 0x0001438E
			public IFeature DelegateRulesLogger
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "DelegateRulesLogger");
				}
			}

			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x060008AD RID: 2221 RVA: 0x000161A5 File Offset: 0x000143A5
			public IMailboxAssistantSettings OABGeneratorAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "OABGeneratorAssistant");
				}
			}

			// Token: 0x17000628 RID: 1576
			// (get) Token: 0x060008AE RID: 2222 RVA: 0x000161BC File Offset: 0x000143BC
			public IMailboxAssistantSettings StoreScheduledIntegrityCheckAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "StoreScheduledIntegrityCheckAssistant");
				}
			}

			// Token: 0x17000629 RID: 1577
			// (get) Token: 0x060008AF RID: 2223 RVA: 0x000161D3 File Offset: 0x000143D3
			public IFeature MwiAssistantGetUMEnabledUsersFromDatacenter
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "MwiAssistantGetUMEnabledUsersFromDatacenter");
				}
			}

			// Token: 0x1700062A RID: 1578
			// (get) Token: 0x060008B0 RID: 2224 RVA: 0x000161EA File Offset: 0x000143EA
			public IMailboxAssistantSettings MailboxProcessorAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "MailboxProcessorAssistant");
				}
			}

			// Token: 0x1700062B RID: 1579
			// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00016201 File Offset: 0x00014401
			public IMailboxAssistantSettings PeopleCentricTriageAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "PeopleCentricTriageAssistant");
				}
			}

			// Token: 0x1700062C RID: 1580
			// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00016218 File Offset: 0x00014418
			public IMailboxAssistantServiceSettings MailboxAssistantService
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantServiceSettings>("MailboxAssistants.settings.ini", "MailboxAssistantService");
				}
			}

			// Token: 0x1700062D RID: 1581
			// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0001622F File Offset: 0x0001442F
			public IFeature ElcAssistantApplyLitigationHoldDuration
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "ElcAssistantApplyLitigationHoldDuration");
				}
			}

			// Token: 0x1700062E RID: 1582
			// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00016246 File Offset: 0x00014446
			public IMailboxAssistantSettings TopNWordsAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "TopNWordsAssistant");
				}
			}

			// Token: 0x1700062F RID: 1583
			// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0001625D File Offset: 0x0001445D
			public IMailboxAssistantSettings SiteMailboxAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "SiteMailboxAssistant");
				}
			}

			// Token: 0x17000630 RID: 1584
			// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00016274 File Offset: 0x00014474
			public IMailboxAssistantSettings UMReportingAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "UMReportingAssistant");
				}
			}

			// Token: 0x17000631 RID: 1585
			// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0001628B File Offset: 0x0001448B
			public IMailboxAssistantSettings DarTaskStoreAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "DarTaskStoreAssistant");
				}
			}

			// Token: 0x17000632 RID: 1586
			// (get) Token: 0x060008B8 RID: 2232 RVA: 0x000162A2 File Offset: 0x000144A2
			public IMailboxAssistantSettings GroupMailboxAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "GroupMailboxAssistant");
				}
			}

			// Token: 0x17000633 RID: 1587
			// (get) Token: 0x060008B9 RID: 2233 RVA: 0x000162B9 File Offset: 0x000144B9
			public IFeature QuickCapture
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "QuickCapture");
				}
			}

			// Token: 0x17000634 RID: 1588
			// (get) Token: 0x060008BA RID: 2234 RVA: 0x000162D0 File Offset: 0x000144D0
			public IMailboxAssistantSettings JunkEmailOptionsCommitterAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "JunkEmailOptionsCommitterAssistant");
				}
			}

			// Token: 0x17000635 RID: 1589
			// (get) Token: 0x060008BB RID: 2235 RVA: 0x000162E7 File Offset: 0x000144E7
			public IMailboxAssistantSettings DirectoryProcessorAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "DirectoryProcessorAssistant");
				}
			}

			// Token: 0x17000636 RID: 1590
			// (get) Token: 0x060008BC RID: 2236 RVA: 0x000162FE File Offset: 0x000144FE
			public IMailboxAssistantSettings CalendarRepairAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "CalendarRepairAssistant");
				}
			}

			// Token: 0x17000637 RID: 1591
			// (get) Token: 0x060008BD RID: 2237 RVA: 0x00016315 File Offset: 0x00014515
			public IMailboxAssistantSettings MailboxAssociationReplicationAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "MailboxAssociationReplicationAssistant");
				}
			}

			// Token: 0x17000638 RID: 1592
			// (get) Token: 0x060008BE RID: 2238 RVA: 0x0001632C File Offset: 0x0001452C
			public IMailboxAssistantSettings StoreDSMaintenanceAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "StoreDSMaintenanceAssistant");
				}
			}

			// Token: 0x17000639 RID: 1593
			// (get) Token: 0x060008BF RID: 2239 RVA: 0x00016343 File Offset: 0x00014543
			public IFeature ElcAssistantDiscoveryHoldSynchronizer
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "ElcAssistantDiscoveryHoldSynchronizer");
				}
			}

			// Token: 0x1700063A RID: 1594
			// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0001635A File Offset: 0x0001455A
			public IMailboxAssistantSettings SharingPolicyAssistant
			{
				get
				{
					return this.snapshot.GetObject<IMailboxAssistantSettings>("MailboxAssistants.settings.ini", "SharingPolicyAssistant");
				}
			}

			// Token: 0x1700063B RID: 1595
			// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00016371 File Offset: 0x00014571
			public IFeature ElcAssistantAlwaysProcessMailbox
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "ElcAssistantAlwaysProcessMailbox");
				}
			}

			// Token: 0x1700063C RID: 1596
			// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00016388 File Offset: 0x00014588
			public IFeature CalendarRepairAssistantReliabilityLogger
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "CalendarRepairAssistantReliabilityLogger");
				}
			}

			// Token: 0x1700063D RID: 1597
			// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0001639F File Offset: 0x0001459F
			public IFeature UnifiedPolicyHold
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "UnifiedPolicyHold");
				}
			}

			// Token: 0x1700063E RID: 1598
			// (get) Token: 0x060008C4 RID: 2244 RVA: 0x000163B6 File Offset: 0x000145B6
			public IFeature PerformRecipientDLExpansion
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxAssistants.settings.ini", "PerformRecipientDLExpansion");
				}
			}

			// Token: 0x04000469 RID: 1129
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000DC RID: 220
		public struct MailboxPlansSettingsIni
		{
			// Token: 0x060008C5 RID: 2245 RVA: 0x000163CD File Offset: 0x000145CD
			internal MailboxPlansSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060008C6 RID: 2246 RVA: 0x000163D6 File Offset: 0x000145D6
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("MailboxPlans.settings.ini");
			}

			// Token: 0x060008C7 RID: 2247 RVA: 0x000163E8 File Offset: 0x000145E8
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("MailboxPlans.settings.ini", id);
			}

			// Token: 0x060008C8 RID: 2248 RVA: 0x000163FB File Offset: 0x000145FB
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("MailboxPlans.settings.ini", id1, ids);
			}

			// Token: 0x1700063F RID: 1599
			// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0001640F File Offset: 0x0001460F
			public IFeature CloneLimitedSetOfMailboxPlanProperties
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxPlans.settings.ini", "CloneLimitedSetOfMailboxPlanProperties");
				}
			}

			// Token: 0x0400046A RID: 1130
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000DD RID: 221
		public struct MailboxTransportSettingsIni
		{
			// Token: 0x060008CA RID: 2250 RVA: 0x00016426 File Offset: 0x00014626
			internal MailboxTransportSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060008CB RID: 2251 RVA: 0x0001642F File Offset: 0x0001462F
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("MailboxTransport.settings.ini");
			}

			// Token: 0x060008CC RID: 2252 RVA: 0x00016441 File Offset: 0x00014641
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("MailboxTransport.settings.ini", id);
			}

			// Token: 0x060008CD RID: 2253 RVA: 0x00016454 File Offset: 0x00014654
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("MailboxTransport.settings.ini", id1, ids);
			}

			// Token: 0x17000640 RID: 1600
			// (get) Token: 0x060008CE RID: 2254 RVA: 0x00016468 File Offset: 0x00014668
			public ISettingsValue ParkedMeetingMessagesRetentionPeriod
			{
				get
				{
					return this.snapshot.GetObject<ISettingsValue>("MailboxTransport.settings.ini", "ParkedMeetingMessagesRetentionPeriod");
				}
			}

			// Token: 0x17000641 RID: 1601
			// (get) Token: 0x060008CF RID: 2255 RVA: 0x0001647F File Offset: 0x0001467F
			public IFeature MailboxTransportSmtpIn
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxTransport.settings.ini", "MailboxTransportSmtpIn");
				}
			}

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00016496 File Offset: 0x00014696
			public IFeature DeliveryHangRecovery
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxTransport.settings.ini", "DeliveryHangRecovery");
				}
			}

			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x060008D1 RID: 2257 RVA: 0x000164AD File Offset: 0x000146AD
			public IFeature InferenceClassificationAgent
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxTransport.settings.ini", "InferenceClassificationAgent");
				}
			}

			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x060008D2 RID: 2258 RVA: 0x000164C4 File Offset: 0x000146C4
			public IFeature UseParticipantSmtpEmailAddress
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxTransport.settings.ini", "UseParticipantSmtpEmailAddress");
				}
			}

			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x060008D3 RID: 2259 RVA: 0x000164DB File Offset: 0x000146DB
			public IFeature CheckArbitrationMailboxCapacity
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxTransport.settings.ini", "CheckArbitrationMailboxCapacity");
				}
			}

			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x060008D4 RID: 2260 RVA: 0x000164F2 File Offset: 0x000146F2
			public IFeature ProcessSeriesMeetingMessages
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxTransport.settings.ini", "ProcessSeriesMeetingMessages");
				}
			}

			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00016509 File Offset: 0x00014709
			public IFeature UseFopeReceivedSpfHeader
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxTransport.settings.ini", "UseFopeReceivedSpfHeader");
				}
			}

			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00016520 File Offset: 0x00014720
			public IFeature OrderSeriesMeetingMessages
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MailboxTransport.settings.ini", "OrderSeriesMeetingMessages");
				}
			}

			// Token: 0x0400046B RID: 1131
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000DE RID: 222
		public struct MalwareAgentSettingsIni
		{
			// Token: 0x060008D7 RID: 2263 RVA: 0x00016537 File Offset: 0x00014737
			internal MalwareAgentSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060008D8 RID: 2264 RVA: 0x00016540 File Offset: 0x00014740
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("MalwareAgent.settings.ini");
			}

			// Token: 0x060008D9 RID: 2265 RVA: 0x00016552 File Offset: 0x00014752
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("MalwareAgent.settings.ini", id);
			}

			// Token: 0x060008DA RID: 2266 RVA: 0x00016565 File Offset: 0x00014765
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("MalwareAgent.settings.ini", id1, ids);
			}

			// Token: 0x17000649 RID: 1609
			// (get) Token: 0x060008DB RID: 2267 RVA: 0x00016579 File Offset: 0x00014779
			public IFeature MalwareAgentV2
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MalwareAgent.settings.ini", "MalwareAgentV2");
				}
			}

			// Token: 0x0400046C RID: 1132
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000DF RID: 223
		public struct MessageTrackingSettingsIni
		{
			// Token: 0x060008DC RID: 2268 RVA: 0x00016590 File Offset: 0x00014790
			internal MessageTrackingSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060008DD RID: 2269 RVA: 0x00016599 File Offset: 0x00014799
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("MessageTracking.settings.ini");
			}

			// Token: 0x060008DE RID: 2270 RVA: 0x000165AB File Offset: 0x000147AB
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("MessageTracking.settings.ini", id);
			}

			// Token: 0x060008DF RID: 2271 RVA: 0x000165BE File Offset: 0x000147BE
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("MessageTracking.settings.ini", id1, ids);
			}

			// Token: 0x1700064A RID: 1610
			// (get) Token: 0x060008E0 RID: 2272 RVA: 0x000165D2 File Offset: 0x000147D2
			public IFeature StatsLogging
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MessageTracking.settings.ini", "StatsLogging");
				}
			}

			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x060008E1 RID: 2273 RVA: 0x000165E9 File Offset: 0x000147E9
			public IFeature QueueViewerDiagnostics
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MessageTracking.settings.ini", "QueueViewerDiagnostics");
				}
			}

			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00016600 File Offset: 0x00014800
			public IFeature AllowDebugMode
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MessageTracking.settings.ini", "AllowDebugMode");
				}
			}

			// Token: 0x1700064D RID: 1613
			// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00016617 File Offset: 0x00014817
			public IFeature UseBackEndLocator
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MessageTracking.settings.ini", "UseBackEndLocator");
				}
			}

			// Token: 0x0400046D RID: 1133
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000E0 RID: 224
		public struct MexAgentsSettingsIni
		{
			// Token: 0x060008E4 RID: 2276 RVA: 0x0001662E File Offset: 0x0001482E
			internal MexAgentsSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060008E5 RID: 2277 RVA: 0x00016637 File Offset: 0x00014837
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("MexAgents.settings.ini");
			}

			// Token: 0x060008E6 RID: 2278 RVA: 0x00016649 File Offset: 0x00014849
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("MexAgents.settings.ini", id);
			}

			// Token: 0x060008E7 RID: 2279 RVA: 0x0001665C File Offset: 0x0001485C
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("MexAgents.settings.ini", id1, ids);
			}

			// Token: 0x1700064E RID: 1614
			// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00016670 File Offset: 0x00014870
			public IFeature TrustedMailAgents_CrossPremisesHeadersPreserved
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MexAgents.settings.ini", "TrustedMailAgents_CrossPremisesHeadersPreserved");
				}
			}

			// Token: 0x1700064F RID: 1615
			// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00016687 File Offset: 0x00014887
			public IFeature TrustedMailAgents_AcceptAnyRecipientOnPremises
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MexAgents.settings.ini", "TrustedMailAgents_AcceptAnyRecipientOnPremises");
				}
			}

			// Token: 0x17000650 RID: 1616
			// (get) Token: 0x060008EA RID: 2282 RVA: 0x0001669E File Offset: 0x0001489E
			public IFeature TrustedMailAgents_StampOriginatorOrgForMsitConnector
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MexAgents.settings.ini", "TrustedMailAgents_StampOriginatorOrgForMsitConnector");
				}
			}

			// Token: 0x17000651 RID: 1617
			// (get) Token: 0x060008EB RID: 2283 RVA: 0x000166B5 File Offset: 0x000148B5
			public IFeature TrustedMailAgents_HandleCrossPremisesProbe
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MexAgents.settings.ini", "TrustedMailAgents_HandleCrossPremisesProbe");
				}
			}

			// Token: 0x17000652 RID: 1618
			// (get) Token: 0x060008EC RID: 2284 RVA: 0x000166CC File Offset: 0x000148CC
			public IFeature TrustedMailAgents_CheckOutboundDeliveryTypeSmtpConnector
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("MexAgents.settings.ini", "TrustedMailAgents_CheckOutboundDeliveryTypeSmtpConnector");
				}
			}

			// Token: 0x0400046E RID: 1134
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000E1 RID: 225
		public struct MrsSettingsIni
		{
			// Token: 0x060008ED RID: 2285 RVA: 0x000166E3 File Offset: 0x000148E3
			internal MrsSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060008EE RID: 2286 RVA: 0x000166EC File Offset: 0x000148EC
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Mrs.settings.ini");
			}

			// Token: 0x060008EF RID: 2287 RVA: 0x000166FE File Offset: 0x000148FE
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Mrs.settings.ini", id);
			}

			// Token: 0x060008F0 RID: 2288 RVA: 0x00016711 File Offset: 0x00014911
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Mrs.settings.ini", id1, ids);
			}

			// Token: 0x17000653 RID: 1619
			// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00016725 File Offset: 0x00014925
			public IFeature MigrationMonitor
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Mrs.settings.ini", "MigrationMonitor");
				}
			}

			// Token: 0x17000654 RID: 1620
			// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0001673C File Offset: 0x0001493C
			public IFeature PublicFolderMailboxesMigration
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Mrs.settings.ini", "PublicFolderMailboxesMigration");
				}
			}

			// Token: 0x17000655 RID: 1621
			// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00016753 File Offset: 0x00014953
			public IFeature UseDefaultValueForCheckInitialProvisioningForMovesParameter
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Mrs.settings.ini", "UseDefaultValueForCheckInitialProvisioningForMovesParameter");
				}
			}

			// Token: 0x17000656 RID: 1622
			// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0001676A File Offset: 0x0001496A
			public IFeature SlowMRSDetector
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Mrs.settings.ini", "SlowMRSDetector");
				}
			}

			// Token: 0x17000657 RID: 1623
			// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00016781 File Offset: 0x00014981
			public IFeature CheckProvisioningSettings
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Mrs.settings.ini", "CheckProvisioningSettings");
				}
			}

			// Token: 0x17000658 RID: 1624
			// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00016798 File Offset: 0x00014998
			public IFeature TxSyncMrsImapExecute
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Mrs.settings.ini", "TxSyncMrsImapExecute");
				}
			}

			// Token: 0x17000659 RID: 1625
			// (get) Token: 0x060008F7 RID: 2295 RVA: 0x000167AF File Offset: 0x000149AF
			public IFeature TxSyncMrsImapCopy
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Mrs.settings.ini", "TxSyncMrsImapCopy");
				}
			}

			// Token: 0x1700065A RID: 1626
			// (get) Token: 0x060008F8 RID: 2296 RVA: 0x000167C6 File Offset: 0x000149C6
			public IFeature AutomaticMailboxLoadBalancing
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Mrs.settings.ini", "AutomaticMailboxLoadBalancing");
				}
			}

			// Token: 0x0400046F RID: 1135
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000E2 RID: 226
		public struct NotificationBrokerServiceSettingsIni
		{
			// Token: 0x060008F9 RID: 2297 RVA: 0x000167DD File Offset: 0x000149DD
			internal NotificationBrokerServiceSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060008FA RID: 2298 RVA: 0x000167E6 File Offset: 0x000149E6
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("NotificationBrokerService.settings.ini");
			}

			// Token: 0x060008FB RID: 2299 RVA: 0x000167F8 File Offset: 0x000149F8
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("NotificationBrokerService.settings.ini", id);
			}

			// Token: 0x060008FC RID: 2300 RVA: 0x0001680B File Offset: 0x00014A0B
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("NotificationBrokerService.settings.ini", id1, ids);
			}

			// Token: 0x1700065B RID: 1627
			// (get) Token: 0x060008FD RID: 2301 RVA: 0x0001681F File Offset: 0x00014A1F
			public IFeature Service
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("NotificationBrokerService.settings.ini", "Service");
				}
			}

			// Token: 0x04000470 RID: 1136
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000E3 RID: 227
		public struct OABSettingsIni
		{
			// Token: 0x060008FE RID: 2302 RVA: 0x00016836 File Offset: 0x00014A36
			internal OABSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060008FF RID: 2303 RVA: 0x0001683F File Offset: 0x00014A3F
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("OAB.settings.ini");
			}

			// Token: 0x06000900 RID: 2304 RVA: 0x00016851 File Offset: 0x00014A51
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("OAB.settings.ini", id);
			}

			// Token: 0x06000901 RID: 2305 RVA: 0x00016864 File Offset: 0x00014A64
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("OAB.settings.ini", id1, ids);
			}

			// Token: 0x1700065C RID: 1628
			// (get) Token: 0x06000902 RID: 2306 RVA: 0x00016878 File Offset: 0x00014A78
			public IFeature LinkedOABGenMailboxes
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OAB.settings.ini", "LinkedOABGenMailboxes");
				}
			}

			// Token: 0x1700065D RID: 1629
			// (get) Token: 0x06000903 RID: 2307 RVA: 0x0001688F File Offset: 0x00014A8F
			public IFeature SkipServiceTopologyDiscovery
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OAB.settings.ini", "SkipServiceTopologyDiscovery");
				}
			}

			// Token: 0x1700065E RID: 1630
			// (get) Token: 0x06000904 RID: 2308 RVA: 0x000168A6 File Offset: 0x00014AA6
			public IFeature EnforceManifestVersionMatch
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OAB.settings.ini", "EnforceManifestVersionMatch");
				}
			}

			// Token: 0x1700065F RID: 1631
			// (get) Token: 0x06000905 RID: 2309 RVA: 0x000168BD File Offset: 0x00014ABD
			public IFeature SharedTemplateFiles
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OAB.settings.ini", "SharedTemplateFiles");
				}
			}

			// Token: 0x17000660 RID: 1632
			// (get) Token: 0x06000906 RID: 2310 RVA: 0x000168D4 File Offset: 0x00014AD4
			public IFeature GenerateRequestedOABsOnly
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OAB.settings.ini", "GenerateRequestedOABsOnly");
				}
			}

			// Token: 0x17000661 RID: 1633
			// (get) Token: 0x06000907 RID: 2311 RVA: 0x000168EB File Offset: 0x00014AEB
			public IFeature OabHttpClientAccessRulesEnabled
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OAB.settings.ini", "OabHttpClientAccessRulesEnabled");
				}
			}

			// Token: 0x04000471 RID: 1137
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000E4 RID: 228
		public struct OfficeGraphSettingsIni
		{
			// Token: 0x06000908 RID: 2312 RVA: 0x00016902 File Offset: 0x00014B02
			internal OfficeGraphSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000909 RID: 2313 RVA: 0x0001690B File Offset: 0x00014B0B
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("OfficeGraph.settings.ini");
			}

			// Token: 0x0600090A RID: 2314 RVA: 0x0001691D File Offset: 0x00014B1D
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("OfficeGraph.settings.ini", id);
			}

			// Token: 0x0600090B RID: 2315 RVA: 0x00016930 File Offset: 0x00014B30
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("OfficeGraph.settings.ini", id1, ids);
			}

			// Token: 0x17000662 RID: 1634
			// (get) Token: 0x0600090C RID: 2316 RVA: 0x00016944 File Offset: 0x00014B44
			public IFeature OfficeGraphGenerateSignals
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OfficeGraph.settings.ini", "OfficeGraphGenerateSignals");
				}
			}

			// Token: 0x17000663 RID: 1635
			// (get) Token: 0x0600090D RID: 2317 RVA: 0x0001695B File Offset: 0x00014B5B
			public IFeature OfficeGraphAgent
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OfficeGraph.settings.ini", "OfficeGraphAgent");
				}
			}

			// Token: 0x04000472 RID: 1138
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000E5 RID: 229
		public struct OwaClientSettingsIni
		{
			// Token: 0x0600090E RID: 2318 RVA: 0x00016972 File Offset: 0x00014B72
			internal OwaClientSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x0600090F RID: 2319 RVA: 0x0001697B File Offset: 0x00014B7B
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("OwaClient.settings.ini");
			}

			// Token: 0x06000910 RID: 2320 RVA: 0x0001698D File Offset: 0x00014B8D
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("OwaClient.settings.ini", id);
			}

			// Token: 0x06000911 RID: 2321 RVA: 0x000169A0 File Offset: 0x00014BA0
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("OwaClient.settings.ini", id1, ids);
			}

			// Token: 0x17000664 RID: 1636
			// (get) Token: 0x06000912 RID: 2322 RVA: 0x000169B4 File Offset: 0x00014BB4
			public IFeature TopNSuggestions
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "TopNSuggestions");
				}
			}

			// Token: 0x17000665 RID: 1637
			// (get) Token: 0x06000913 RID: 2323 RVA: 0x000169CB File Offset: 0x00014BCB
			public IFeature O365ShellPlus
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "O365ShellPlus");
				}
			}

			// Token: 0x17000666 RID: 1638
			// (get) Token: 0x06000914 RID: 2324 RVA: 0x000169E2 File Offset: 0x00014BE2
			public IFeature XOWABirthdayCalendar
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "XOWABirthdayCalendar");
				}
			}

			// Token: 0x17000667 RID: 1639
			// (get) Token: 0x06000915 RID: 2325 RVA: 0x000169F9 File Offset: 0x00014BF9
			public IFeature CalendarSearchSurvey
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "CalendarSearchSurvey");
				}
			}

			// Token: 0x17000668 RID: 1640
			// (get) Token: 0x06000916 RID: 2326 RVA: 0x00016A10 File Offset: 0x00014C10
			public IFeature LWX
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "LWX");
				}
			}

			// Token: 0x17000669 RID: 1641
			// (get) Token: 0x06000917 RID: 2327 RVA: 0x00016A27 File Offset: 0x00014C27
			public IFeature FlagPlus
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "FlagPlus");
				}
			}

			// Token: 0x1700066A RID: 1642
			// (get) Token: 0x06000918 RID: 2328 RVA: 0x00016A3E File Offset: 0x00014C3E
			public IFeature PALDogfoodEnforcement
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "PALDogfoodEnforcement");
				}
			}

			// Token: 0x1700066B RID: 1643
			// (get) Token: 0x06000919 RID: 2329 RVA: 0x00016A55 File Offset: 0x00014C55
			public IFeature EnableFBL
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "EnableFBL");
				}
			}

			// Token: 0x1700066C RID: 1644
			// (get) Token: 0x0600091A RID: 2330 RVA: 0x00016A6C File Offset: 0x00014C6C
			public IFeature ModernGroupsQuotedText
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "ModernGroupsQuotedText");
				}
			}

			// Token: 0x1700066D RID: 1645
			// (get) Token: 0x0600091B RID: 2331 RVA: 0x00016A83 File Offset: 0x00014C83
			public IFeature InstantEventCreate
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "InstantEventCreate");
				}
			}

			// Token: 0x1700066E RID: 1646
			// (get) Token: 0x0600091C RID: 2332 RVA: 0x00016A9A File Offset: 0x00014C9A
			public IFeature BuildGreenLightSurveyFlight
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "BuildGreenLightSurveyFlight");
				}
			}

			// Token: 0x1700066F RID: 1647
			// (get) Token: 0x0600091D RID: 2333 RVA: 0x00016AB1 File Offset: 0x00014CB1
			public IFeature XOWAShowPersonaCardOnHover
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "XOWAShowPersonaCardOnHover");
				}
			}

			// Token: 0x17000670 RID: 1648
			// (get) Token: 0x0600091E RID: 2334 RVA: 0x00016AC8 File Offset: 0x00014CC8
			public IFeature CalendarEventSearch
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "CalendarEventSearch");
				}
			}

			// Token: 0x17000671 RID: 1649
			// (get) Token: 0x0600091F RID: 2335 RVA: 0x00016ADF File Offset: 0x00014CDF
			public IFeature InstantSearch
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "InstantSearch");
				}
			}

			// Token: 0x17000672 RID: 1650
			// (get) Token: 0x06000920 RID: 2336 RVA: 0x00016AF6 File Offset: 0x00014CF6
			public IFeature Like
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "Like");
				}
			}

			// Token: 0x17000673 RID: 1651
			// (get) Token: 0x06000921 RID: 2337 RVA: 0x00016B0D File Offset: 0x00014D0D
			public IFeature iOSSharePointRichTextEditor
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "iOSSharePointRichTextEditor");
				}
			}

			// Token: 0x17000674 RID: 1652
			// (get) Token: 0x06000922 RID: 2338 RVA: 0x00016B24 File Offset: 0x00014D24
			public IFeature ModernGroupsTrendingConversations
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "ModernGroupsTrendingConversations");
				}
			}

			// Token: 0x17000675 RID: 1653
			// (get) Token: 0x06000923 RID: 2339 RVA: 0x00016B3B File Offset: 0x00014D3B
			public IFeature AttachmentsHub
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "AttachmentsHub");
				}
			}

			// Token: 0x17000676 RID: 1654
			// (get) Token: 0x06000924 RID: 2340 RVA: 0x00016B52 File Offset: 0x00014D52
			public IFeature LocationReminder
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "LocationReminder");
				}
			}

			// Token: 0x17000677 RID: 1655
			// (get) Token: 0x06000925 RID: 2341 RVA: 0x00016B69 File Offset: 0x00014D69
			public IFeature OWADiagnostics
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "OWADiagnostics");
				}
			}

			// Token: 0x17000678 RID: 1656
			// (get) Token: 0x06000926 RID: 2342 RVA: 0x00016B80 File Offset: 0x00014D80
			public IFeature DeleteGroupConversation
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "DeleteGroupConversation");
				}
			}

			// Token: 0x17000679 RID: 1657
			// (get) Token: 0x06000927 RID: 2343 RVA: 0x00016B97 File Offset: 0x00014D97
			public IFeature Oops
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "Oops");
				}
			}

			// Token: 0x1700067A RID: 1658
			// (get) Token: 0x06000928 RID: 2344 RVA: 0x00016BAE File Offset: 0x00014DAE
			public IFeature DisableAnimations
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "DisableAnimations");
				}
			}

			// Token: 0x1700067B RID: 1659
			// (get) Token: 0x06000929 RID: 2345 RVA: 0x00016BC5 File Offset: 0x00014DC5
			public IFeature UnifiedMailboxUI
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "UnifiedMailboxUI");
				}
			}

			// Token: 0x1700067C RID: 1660
			// (get) Token: 0x0600092A RID: 2346 RVA: 0x00016BDC File Offset: 0x00014DDC
			public IFeature XOWAUnifiedForms
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "XOWAUnifiedForms");
				}
			}

			// Token: 0x1700067D RID: 1661
			// (get) Token: 0x0600092B RID: 2347 RVA: 0x00016BF3 File Offset: 0x00014DF3
			public IFeature O365ShellCore
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "O365ShellCore");
				}
			}

			// Token: 0x1700067E RID: 1662
			// (get) Token: 0x0600092C RID: 2348 RVA: 0x00016C0A File Offset: 0x00014E0A
			public IFeature XOWAFrequentContacts
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "XOWAFrequentContacts");
				}
			}

			// Token: 0x1700067F RID: 1663
			// (get) Token: 0x0600092D RID: 2349 RVA: 0x00016C21 File Offset: 0x00014E21
			public IFeature InstantPopout
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "InstantPopout");
				}
			}

			// Token: 0x17000680 RID: 1664
			// (get) Token: 0x0600092E RID: 2350 RVA: 0x00016C38 File Offset: 0x00014E38
			public IFeature Water
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "Water");
				}
			}

			// Token: 0x17000681 RID: 1665
			// (get) Token: 0x0600092F RID: 2351 RVA: 0x00016C4F File Offset: 0x00014E4F
			public IFeature EmailReminders
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "EmailReminders");
				}
			}

			// Token: 0x17000682 RID: 1666
			// (get) Token: 0x06000930 RID: 2352 RVA: 0x00016C66 File Offset: 0x00014E66
			public IFeature ProposeNewTime
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "ProposeNewTime");
				}
			}

			// Token: 0x17000683 RID: 1667
			// (get) Token: 0x06000931 RID: 2353 RVA: 0x00016C7D File Offset: 0x00014E7D
			public IFeature EnableAnimations
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "EnableAnimations");
				}
			}

			// Token: 0x17000684 RID: 1668
			// (get) Token: 0x06000932 RID: 2354 RVA: 0x00016C94 File Offset: 0x00014E94
			public IFeature SuperMailLink
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "SuperMailLink");
				}
			}

			// Token: 0x17000685 RID: 1669
			// (get) Token: 0x06000933 RID: 2355 RVA: 0x00016CAB File Offset: 0x00014EAB
			public IFeature OwaFlow
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "OwaFlow");
				}
			}

			// Token: 0x17000686 RID: 1670
			// (get) Token: 0x06000934 RID: 2356 RVA: 0x00016CC2 File Offset: 0x00014EC2
			public IFeature OptionsLimited
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "OptionsLimited");
				}
			}

			// Token: 0x17000687 RID: 1671
			// (get) Token: 0x06000935 RID: 2357 RVA: 0x00016CD9 File Offset: 0x00014ED9
			public IFeature XOWACalendar
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "XOWACalendar");
				}
			}

			// Token: 0x17000688 RID: 1672
			// (get) Token: 0x06000936 RID: 2358 RVA: 0x00016CF0 File Offset: 0x00014EF0
			public IFeature SuperSwipe
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "SuperSwipe");
				}
			}

			// Token: 0x17000689 RID: 1673
			// (get) Token: 0x06000937 RID: 2359 RVA: 0x00016D07 File Offset: 0x00014F07
			public IFeature XOWASuperCommand
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "XOWASuperCommand");
				}
			}

			// Token: 0x1700068A RID: 1674
			// (get) Token: 0x06000938 RID: 2360 RVA: 0x00016D1E File Offset: 0x00014F1E
			public IFeature OWADelayedBinding
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "OWADelayedBinding");
				}
			}

			// Token: 0x1700068B RID: 1675
			// (get) Token: 0x06000939 RID: 2361 RVA: 0x00016D35 File Offset: 0x00014F35
			public IFeature SharePointOneDrive
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "SharePointOneDrive");
				}
			}

			// Token: 0x1700068C RID: 1676
			// (get) Token: 0x0600093A RID: 2362 RVA: 0x00016D4C File Offset: 0x00014F4C
			public IFeature SendLinkClickedSignalToSP
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "SendLinkClickedSignalToSP");
				}
			}

			// Token: 0x1700068D RID: 1677
			// (get) Token: 0x0600093B RID: 2363 RVA: 0x00016D63 File Offset: 0x00014F63
			public IFeature XOWAAwesomeReadingPane
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "XOWAAwesomeReadingPane");
				}
			}

			// Token: 0x1700068E RID: 1678
			// (get) Token: 0x0600093C RID: 2364 RVA: 0x00016D7A File Offset: 0x00014F7A
			public IFeature OrganizationBrowser
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "OrganizationBrowser");
				}
			}

			// Token: 0x1700068F RID: 1679
			// (get) Token: 0x0600093D RID: 2365 RVA: 0x00016D91 File Offset: 0x00014F91
			public IFeature O365Miniatures
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "O365Miniatures");
				}
			}

			// Token: 0x17000690 RID: 1680
			// (get) Token: 0x0600093E RID: 2366 RVA: 0x00016DA8 File Offset: 0x00014FA8
			public IFeature ModernGroupsSurveyGroupA
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "ModernGroupsSurveyGroupA");
				}
			}

			// Token: 0x17000691 RID: 1681
			// (get) Token: 0x0600093F RID: 2367 RVA: 0x00016DBF File Offset: 0x00014FBF
			public IFeature OwaPublicFolderFavorites
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "OwaPublicFolderFavorites");
				}
			}

			// Token: 0x17000692 RID: 1682
			// (get) Token: 0x06000940 RID: 2368 RVA: 0x00016DD6 File Offset: 0x00014FD6
			public IFeature MailSatisfactionSurvey
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "MailSatisfactionSurvey");
				}
			}

			// Token: 0x17000693 RID: 1683
			// (get) Token: 0x06000941 RID: 2369 RVA: 0x00016DED File Offset: 0x00014FED
			public IFeature QuickCapture
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "QuickCapture");
				}
			}

			// Token: 0x17000694 RID: 1684
			// (get) Token: 0x06000942 RID: 2370 RVA: 0x00016E04 File Offset: 0x00015004
			public IFeature OwaLinkPrefetch
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "OwaLinkPrefetch");
				}
			}

			// Token: 0x17000695 RID: 1685
			// (get) Token: 0x06000943 RID: 2371 RVA: 0x00016E1B File Offset: 0x0001501B
			public IFeature Options
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "Options");
				}
			}

			// Token: 0x17000696 RID: 1686
			// (get) Token: 0x06000944 RID: 2372 RVA: 0x00016E32 File Offset: 0x00015032
			public IFeature SuppressPushNotificationsWhenOof
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "SuppressPushNotificationsWhenOof");
				}
			}

			// Token: 0x17000697 RID: 1687
			// (get) Token: 0x06000945 RID: 2373 RVA: 0x00016E49 File Offset: 0x00015049
			public IFeature AndroidCED
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "AndroidCED");
				}
			}

			// Token: 0x17000698 RID: 1688
			// (get) Token: 0x06000946 RID: 2374 RVA: 0x00016E60 File Offset: 0x00015060
			public IFeature InstantPopout2
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "InstantPopout2");
				}
			}

			// Token: 0x17000699 RID: 1689
			// (get) Token: 0x06000947 RID: 2375 RVA: 0x00016E77 File Offset: 0x00015077
			public IFeature LanguageQualitySurvey
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "LanguageQualitySurvey");
				}
			}

			// Token: 0x1700069A RID: 1690
			// (get) Token: 0x06000948 RID: 2376 RVA: 0x00016E8E File Offset: 0x0001508E
			public IFeature O365Panorama
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "O365Panorama");
				}
			}

			// Token: 0x1700069B RID: 1691
			// (get) Token: 0x06000949 RID: 2377 RVA: 0x00016EA5 File Offset: 0x000150A5
			public IFeature ShowClientWatson
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "ShowClientWatson");
				}
			}

			// Token: 0x1700069C RID: 1692
			// (get) Token: 0x0600094A RID: 2378 RVA: 0x00016EBC File Offset: 0x000150BC
			public IFeature HelpPanel
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "HelpPanel");
				}
			}

			// Token: 0x1700069D RID: 1693
			// (get) Token: 0x0600094B RID: 2379 RVA: 0x00016ED3 File Offset: 0x000150D3
			public IFeature InstantSearchAlpha
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "InstantSearchAlpha");
				}
			}

			// Token: 0x1700069E RID: 1694
			// (get) Token: 0x0600094C RID: 2380 RVA: 0x00016EEA File Offset: 0x000150EA
			public IFeature MowaInternalFeedback
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "MowaInternalFeedback");
				}
			}

			// Token: 0x1700069F RID: 1695
			// (get) Token: 0x0600094D RID: 2381 RVA: 0x00016F01 File Offset: 0x00015101
			public IFeature XOWATasks
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "XOWATasks");
				}
			}

			// Token: 0x170006A0 RID: 1696
			// (get) Token: 0x0600094E RID: 2382 RVA: 0x00016F18 File Offset: 0x00015118
			public IFeature XOWAEmoji
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "XOWAEmoji");
				}
			}

			// Token: 0x170006A1 RID: 1697
			// (get) Token: 0x0600094F RID: 2383 RVA: 0x00016F2F File Offset: 0x0001512F
			public IFeature ContextualApps
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "ContextualApps");
				}
			}

			// Token: 0x170006A2 RID: 1698
			// (get) Token: 0x06000950 RID: 2384 RVA: 0x00016F46 File Offset: 0x00015146
			public IFeature SuperZoom
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "SuperZoom");
				}
			}

			// Token: 0x170006A3 RID: 1699
			// (get) Token: 0x06000951 RID: 2385 RVA: 0x00016F5D File Offset: 0x0001515D
			public IFeature AgavePerformance
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "AgavePerformance");
				}
			}

			// Token: 0x170006A4 RID: 1700
			// (get) Token: 0x06000952 RID: 2386 RVA: 0x00016F74 File Offset: 0x00015174
			public IFeature ComposeBread1
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "ComposeBread1");
				}
			}

			// Token: 0x170006A5 RID: 1701
			// (get) Token: 0x06000953 RID: 2387 RVA: 0x00016F8B File Offset: 0x0001518B
			public IFeature WorkingSetAgent
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClient.settings.ini", "WorkingSetAgent");
				}
			}

			// Token: 0x04000473 RID: 1139
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000E6 RID: 230
		public struct OwaClientServerSettingsIni
		{
			// Token: 0x06000954 RID: 2388 RVA: 0x00016FA2 File Offset: 0x000151A2
			internal OwaClientServerSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000955 RID: 2389 RVA: 0x00016FAB File Offset: 0x000151AB
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("OwaClientServer.settings.ini");
			}

			// Token: 0x06000956 RID: 2390 RVA: 0x00016FBD File Offset: 0x000151BD
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("OwaClientServer.settings.ini", id);
			}

			// Token: 0x06000957 RID: 2391 RVA: 0x00016FD0 File Offset: 0x000151D0
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("OwaClientServer.settings.ini", id1, ids);
			}

			// Token: 0x170006A6 RID: 1702
			// (get) Token: 0x06000958 RID: 2392 RVA: 0x00016FE4 File Offset: 0x000151E4
			public IFeature FolderBasedClutter
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "FolderBasedClutter");
				}
			}

			// Token: 0x170006A7 RID: 1703
			// (get) Token: 0x06000959 RID: 2393 RVA: 0x00016FFB File Offset: 0x000151FB
			public IFeature FlightsView
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "FlightsView");
				}
			}

			// Token: 0x170006A8 RID: 1704
			// (get) Token: 0x0600095A RID: 2394 RVA: 0x00017012 File Offset: 0x00015212
			public IFeature O365Header
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "O365Header");
				}
			}

			// Token: 0x170006A9 RID: 1705
			// (get) Token: 0x0600095B RID: 2395 RVA: 0x00017029 File Offset: 0x00015229
			public IFeature OwaVersioning
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "OwaVersioning");
				}
			}

			// Token: 0x170006AA RID: 1706
			// (get) Token: 0x0600095C RID: 2396 RVA: 0x00017040 File Offset: 0x00015240
			public IFeature AutoSubscribeNewGroupMembers
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "AutoSubscribeNewGroupMembers");
				}
			}

			// Token: 0x170006AB RID: 1707
			// (get) Token: 0x0600095D RID: 2397 RVA: 0x00017057 File Offset: 0x00015257
			public IFeature XOWAHolidayCalendars
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "XOWAHolidayCalendars");
				}
			}

			// Token: 0x170006AC RID: 1708
			// (get) Token: 0x0600095E RID: 2398 RVA: 0x0001706E File Offset: 0x0001526E
			public IFeature AttachmentsFilePicker
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "AttachmentsFilePicker");
				}
			}

			// Token: 0x170006AD RID: 1709
			// (get) Token: 0x0600095F RID: 2399 RVA: 0x00017085 File Offset: 0x00015285
			public IFeature GroupRegionalConfiguration
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "GroupRegionalConfiguration");
				}
			}

			// Token: 0x170006AE RID: 1710
			// (get) Token: 0x06000960 RID: 2400 RVA: 0x0001709C File Offset: 0x0001529C
			public IFeature DocCollab
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "DocCollab");
				}
			}

			// Token: 0x170006AF RID: 1711
			// (get) Token: 0x06000961 RID: 2401 RVA: 0x000170B3 File Offset: 0x000152B3
			public IFeature OwaPublicFolders
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "OwaPublicFolders");
				}
			}

			// Token: 0x170006B0 RID: 1712
			// (get) Token: 0x06000962 RID: 2402 RVA: 0x000170CA File Offset: 0x000152CA
			public IFeature O365ParityHeader
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "O365ParityHeader");
				}
			}

			// Token: 0x170006B1 RID: 1713
			// (get) Token: 0x06000963 RID: 2403 RVA: 0x000170E1 File Offset: 0x000152E1
			public IFeature ModernMail
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "ModernMail");
				}
			}

			// Token: 0x170006B2 RID: 1714
			// (get) Token: 0x06000964 RID: 2404 RVA: 0x000170F8 File Offset: 0x000152F8
			public IFeature SmimeConversation
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "SmimeConversation");
				}
			}

			// Token: 0x170006B3 RID: 1715
			// (get) Token: 0x06000965 RID: 2405 RVA: 0x0001710F File Offset: 0x0001530F
			public IFeature ActiveViewConvergence
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "ActiveViewConvergence");
				}
			}

			// Token: 0x170006B4 RID: 1716
			// (get) Token: 0x06000966 RID: 2406 RVA: 0x00017126 File Offset: 0x00015326
			public IFeature ModernGroupsWorkingSet
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "ModernGroupsWorkingSet");
				}
			}

			// Token: 0x170006B5 RID: 1717
			// (get) Token: 0x06000967 RID: 2407 RVA: 0x0001713D File Offset: 0x0001533D
			public IFeature InlinePreview
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "InlinePreview");
				}
			}

			// Token: 0x170006B6 RID: 1718
			// (get) Token: 0x06000968 RID: 2408 RVA: 0x00017154 File Offset: 0x00015354
			public IFeature PeopleCentricTriage
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "PeopleCentricTriage");
				}
			}

			// Token: 0x170006B7 RID: 1719
			// (get) Token: 0x06000969 RID: 2409 RVA: 0x0001716B File Offset: 0x0001536B
			public IFeature ChangeLayout
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "ChangeLayout");
				}
			}

			// Token: 0x170006B8 RID: 1720
			// (get) Token: 0x0600096A RID: 2410 RVA: 0x00017182 File Offset: 0x00015382
			public IFeature SuperStart
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "SuperStart");
				}
			}

			// Token: 0x170006B9 RID: 1721
			// (get) Token: 0x0600096B RID: 2411 RVA: 0x00017199 File Offset: 0x00015399
			public IFeature SuperNormal
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "SuperNormal");
				}
			}

			// Token: 0x170006BA RID: 1722
			// (get) Token: 0x0600096C RID: 2412 RVA: 0x000171B0 File Offset: 0x000153B0
			public IFeature FasterPhoto
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "FasterPhoto");
				}
			}

			// Token: 0x170006BB RID: 1723
			// (get) Token: 0x0600096D RID: 2413 RVA: 0x000171C7 File Offset: 0x000153C7
			public IFeature NotificationBroker
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "NotificationBroker");
				}
			}

			// Token: 0x170006BC RID: 1724
			// (get) Token: 0x0600096E RID: 2414 RVA: 0x000171DE File Offset: 0x000153DE
			public IFeature ModernGroupsNewArchitecture
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "ModernGroupsNewArchitecture");
				}
			}

			// Token: 0x170006BD RID: 1725
			// (get) Token: 0x0600096F RID: 2415 RVA: 0x000171F5 File Offset: 0x000153F5
			public IFeature SuperSort
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "SuperSort");
				}
			}

			// Token: 0x170006BE RID: 1726
			// (get) Token: 0x06000970 RID: 2416 RVA: 0x0001720C File Offset: 0x0001540C
			public IFeature AutoSubscribeSetByDefault
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "AutoSubscribeSetByDefault");
				}
			}

			// Token: 0x170006BF RID: 1727
			// (get) Token: 0x06000971 RID: 2417 RVA: 0x00017223 File Offset: 0x00015423
			public IFeature SafeHtml
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "SafeHtml");
				}
			}

			// Token: 0x170006C0 RID: 1728
			// (get) Token: 0x06000972 RID: 2418 RVA: 0x0001723A File Offset: 0x0001543A
			public IFeature Weather
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "Weather");
				}
			}

			// Token: 0x170006C1 RID: 1729
			// (get) Token: 0x06000973 RID: 2419 RVA: 0x00017251 File Offset: 0x00015451
			public IFeature ModernGroups
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "ModernGroups");
				}
			}

			// Token: 0x170006C2 RID: 1730
			// (get) Token: 0x06000974 RID: 2420 RVA: 0x00017268 File Offset: 0x00015468
			public IFeature ModernAttachments
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "ModernAttachments");
				}
			}

			// Token: 0x170006C3 RID: 1731
			// (get) Token: 0x06000975 RID: 2421 RVA: 0x0001727F File Offset: 0x0001547F
			public IFeature OWAPLTPerf
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "OWAPLTPerf");
				}
			}

			// Token: 0x170006C4 RID: 1732
			// (get) Token: 0x06000976 RID: 2422 RVA: 0x00017296 File Offset: 0x00015496
			public IFeature O365G2Header
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaClientServer.settings.ini", "O365G2Header");
				}
			}

			// Token: 0x04000474 RID: 1140
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000E7 RID: 231
		public struct OwaServerSettingsIni
		{
			// Token: 0x06000977 RID: 2423 RVA: 0x000172AD File Offset: 0x000154AD
			internal OwaServerSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000978 RID: 2424 RVA: 0x000172B6 File Offset: 0x000154B6
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("OwaServer.settings.ini");
			}

			// Token: 0x06000979 RID: 2425 RVA: 0x000172C8 File Offset: 0x000154C8
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("OwaServer.settings.ini", id);
			}

			// Token: 0x0600097A RID: 2426 RVA: 0x000172DB File Offset: 0x000154DB
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("OwaServer.settings.ini", id1, ids);
			}

			// Token: 0x170006C5 RID: 1733
			// (get) Token: 0x0600097B RID: 2427 RVA: 0x000172EF File Offset: 0x000154EF
			public IFeature OwaMailboxSessionCloning
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "OwaMailboxSessionCloning");
				}
			}

			// Token: 0x170006C6 RID: 1734
			// (get) Token: 0x0600097C RID: 2428 RVA: 0x00017306 File Offset: 0x00015506
			public IFeature PeopleCentricConversation
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "PeopleCentricConversation");
				}
			}

			// Token: 0x170006C7 RID: 1735
			// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001731D File Offset: 0x0001551D
			public IFeature OwaSessionDataPreload
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "OwaSessionDataPreload");
				}
			}

			// Token: 0x170006C8 RID: 1736
			// (get) Token: 0x0600097E RID: 2430 RVA: 0x00017334 File Offset: 0x00015534
			public IFeature ShouldSkipAdfsGroupReadOnFrontend
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "ShouldSkipAdfsGroupReadOnFrontend");
				}
			}

			// Token: 0x170006C9 RID: 1737
			// (get) Token: 0x0600097F RID: 2431 RVA: 0x0001734B File Offset: 0x0001554B
			public IFeature XOWABirthdayAssistant
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "XOWABirthdayAssistant");
				}
			}

			// Token: 0x170006CA RID: 1738
			// (get) Token: 0x06000980 RID: 2432 RVA: 0x00017362 File Offset: 0x00015562
			public IInlineExploreSettings InlineExploreSettings
			{
				get
				{
					return this.snapshot.GetObject<IInlineExploreSettings>("OwaServer.settings.ini", "InlineExploreSettings");
				}
			}

			// Token: 0x170006CB RID: 1739
			// (get) Token: 0x06000981 RID: 2433 RVA: 0x00017379 File Offset: 0x00015579
			public IFeature InferenceUI
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "InferenceUI");
				}
			}

			// Token: 0x170006CC RID: 1740
			// (get) Token: 0x06000982 RID: 2434 RVA: 0x00017390 File Offset: 0x00015590
			public IFeature OwaHttpHandler
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "OwaHttpHandler");
				}
			}

			// Token: 0x170006CD RID: 1741
			// (get) Token: 0x06000983 RID: 2435 RVA: 0x000173A7 File Offset: 0x000155A7
			public IFeature FlightFormat
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "FlightFormat");
				}
			}

			// Token: 0x170006CE RID: 1742
			// (get) Token: 0x06000984 RID: 2436 RVA: 0x000173BE File Offset: 0x000155BE
			public IFeature AndroidPremium
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "AndroidPremium");
				}
			}

			// Token: 0x170006CF RID: 1743
			// (get) Token: 0x06000985 RID: 2437 RVA: 0x000173D5 File Offset: 0x000155D5
			public IFeature ModernConversationPrep
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "ModernConversationPrep");
				}
			}

			// Token: 0x170006D0 RID: 1744
			// (get) Token: 0x06000986 RID: 2438 RVA: 0x000173EC File Offset: 0x000155EC
			public IFeature OptimizedParticipantResolver
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "OptimizedParticipantResolver");
				}
			}

			// Token: 0x170006D1 RID: 1745
			// (get) Token: 0x06000987 RID: 2439 RVA: 0x00017403 File Offset: 0x00015603
			public IFeature OwaHostNameSwitch
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "OwaHostNameSwitch");
				}
			}

			// Token: 0x170006D2 RID: 1746
			// (get) Token: 0x06000988 RID: 2440 RVA: 0x0001741A File Offset: 0x0001561A
			public IFeature OwaVNext
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "OwaVNext");
				}
			}

			// Token: 0x170006D3 RID: 1747
			// (get) Token: 0x06000989 RID: 2441 RVA: 0x00017431 File Offset: 0x00015631
			public IFeature OWAEdgeMode
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "OWAEdgeMode");
				}
			}

			// Token: 0x170006D4 RID: 1748
			// (get) Token: 0x0600098A RID: 2442 RVA: 0x00017448 File Offset: 0x00015648
			public IFeature OwaCompositeSessionData
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "OwaCompositeSessionData");
				}
			}

			// Token: 0x170006D5 RID: 1749
			// (get) Token: 0x0600098B RID: 2443 RVA: 0x0001745F File Offset: 0x0001565F
			public IFeature ReportJunk
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "ReportJunk");
				}
			}

			// Token: 0x170006D6 RID: 1750
			// (get) Token: 0x0600098C RID: 2444 RVA: 0x00017476 File Offset: 0x00015676
			public IFeature OwaClientAccessRulesEnabled
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "OwaClientAccessRulesEnabled");
				}
			}

			// Token: 0x170006D7 RID: 1751
			// (get) Token: 0x0600098D RID: 2445 RVA: 0x0001748D File Offset: 0x0001568D
			public IFeature OwaServerLogonActivityLogging
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "OwaServerLogonActivityLogging");
				}
			}

			// Token: 0x170006D8 RID: 1752
			// (get) Token: 0x0600098E RID: 2446 RVA: 0x000174A4 File Offset: 0x000156A4
			public IFeature InlineExploreUI
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaServer.settings.ini", "InlineExploreUI");
				}
			}

			// Token: 0x04000475 RID: 1141
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000E8 RID: 232
		public struct OwaDeploymentSettingsIni
		{
			// Token: 0x0600098F RID: 2447 RVA: 0x000174BB File Offset: 0x000156BB
			internal OwaDeploymentSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000990 RID: 2448 RVA: 0x000174C4 File Offset: 0x000156C4
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("OwaDeployment.settings.ini");
			}

			// Token: 0x06000991 RID: 2449 RVA: 0x000174D6 File Offset: 0x000156D6
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("OwaDeployment.settings.ini", id);
			}

			// Token: 0x06000992 RID: 2450 RVA: 0x000174E9 File Offset: 0x000156E9
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("OwaDeployment.settings.ini", id1, ids);
			}

			// Token: 0x170006D9 RID: 1753
			// (get) Token: 0x06000993 RID: 2451 RVA: 0x000174FD File Offset: 0x000156FD
			public IFeature PublicFolderTreePerTenanant
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "PublicFolderTreePerTenanant");
				}
			}

			// Token: 0x170006DA RID: 1754
			// (get) Token: 0x06000994 RID: 2452 RVA: 0x00017514 File Offset: 0x00015714
			public IFeature ExplicitLogonAuthFilter
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "ExplicitLogonAuthFilter");
				}
			}

			// Token: 0x170006DB RID: 1755
			// (get) Token: 0x06000995 RID: 2453 RVA: 0x0001752B File Offset: 0x0001572B
			public IFeature Places
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "Places");
				}
			}

			// Token: 0x170006DC RID: 1756
			// (get) Token: 0x06000996 RID: 2454 RVA: 0x00017542 File Offset: 0x00015742
			public IFeature IncludeAccountAccessDisclaimer
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "IncludeAccountAccessDisclaimer");
				}
			}

			// Token: 0x170006DD RID: 1757
			// (get) Token: 0x06000997 RID: 2455 RVA: 0x00017559 File Offset: 0x00015759
			public IFeature FilterETag
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "FilterETag");
				}
			}

			// Token: 0x170006DE RID: 1758
			// (get) Token: 0x06000998 RID: 2456 RVA: 0x00017570 File Offset: 0x00015770
			public IFeature CacheUMCultures
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "CacheUMCultures");
				}
			}

			// Token: 0x170006DF RID: 1759
			// (get) Token: 0x06000999 RID: 2457 RVA: 0x00017587 File Offset: 0x00015787
			public IFeature RedirectToServer
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "RedirectToServer");
				}
			}

			// Token: 0x170006E0 RID: 1760
			// (get) Token: 0x0600099A RID: 2458 RVA: 0x0001759E File Offset: 0x0001579E
			public IFeature UseAccessProxyForInstantMessagingServerName
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "UseAccessProxyForInstantMessagingServerName");
				}
			}

			// Token: 0x170006E1 RID: 1761
			// (get) Token: 0x0600099B RID: 2459 RVA: 0x000175B5 File Offset: 0x000157B5
			public IFeature UseBackendVdirConfiguration
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "UseBackendVdirConfiguration");
				}
			}

			// Token: 0x170006E2 RID: 1762
			// (get) Token: 0x0600099C RID: 2460 RVA: 0x000175CC File Offset: 0x000157CC
			public IFeature OneDriveProProviderAvailable
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "OneDriveProProviderAvailable");
				}
			}

			// Token: 0x170006E3 RID: 1763
			// (get) Token: 0x0600099D RID: 2461 RVA: 0x000175E3 File Offset: 0x000157E3
			public IFeature LogTenantInfo
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "LogTenantInfo");
				}
			}

			// Token: 0x170006E4 RID: 1764
			// (get) Token: 0x0600099E RID: 2462 RVA: 0x000175FA File Offset: 0x000157FA
			public IFeature RedirectToLogoffPage
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "RedirectToLogoffPage");
				}
			}

			// Token: 0x170006E5 RID: 1765
			// (get) Token: 0x0600099F RID: 2463 RVA: 0x00017611 File Offset: 0x00015811
			public IFeature IsBranded
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "IsBranded");
				}
			}

			// Token: 0x170006E6 RID: 1766
			// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00017628 File Offset: 0x00015828
			public IFeature SkipPushNotificationStorageTenantId
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "SkipPushNotificationStorageTenantId");
				}
			}

			// Token: 0x170006E7 RID: 1767
			// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0001763F File Offset: 0x0001583F
			public IFeature UseVdirConfigForInstantMessaging
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "UseVdirConfigForInstantMessaging");
				}
			}

			// Token: 0x170006E8 RID: 1768
			// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00017656 File Offset: 0x00015856
			public IFeature RenderPrivacyStatement
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "RenderPrivacyStatement");
				}
			}

			// Token: 0x170006E9 RID: 1769
			// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0001766D File Offset: 0x0001586D
			public IFeature UseRootDirForAppCacheVdir
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "UseRootDirForAppCacheVdir");
				}
			}

			// Token: 0x170006EA RID: 1770
			// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00017684 File Offset: 0x00015884
			public IFeature IsLogonFormatEmail
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "IsLogonFormatEmail");
				}
			}

			// Token: 0x170006EB RID: 1771
			// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0001769B File Offset: 0x0001589B
			public IFeature WacConfigurationFromOrgConfig
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "WacConfigurationFromOrgConfig");
				}
			}

			// Token: 0x170006EC RID: 1772
			// (get) Token: 0x060009A6 RID: 2470 RVA: 0x000176B2 File Offset: 0x000158B2
			public IFeature MrsConnectedAccountsSync
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "MrsConnectedAccountsSync");
				}
			}

			// Token: 0x170006ED RID: 1773
			// (get) Token: 0x060009A7 RID: 2471 RVA: 0x000176C9 File Offset: 0x000158C9
			public IFeature UsePersistedCapabilities
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "UsePersistedCapabilities");
				}
			}

			// Token: 0x170006EE RID: 1774
			// (get) Token: 0x060009A8 RID: 2472 RVA: 0x000176E0 File Offset: 0x000158E0
			public IFeature CheckFeatureRestrictions
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "CheckFeatureRestrictions");
				}
			}

			// Token: 0x170006EF RID: 1775
			// (get) Token: 0x060009A9 RID: 2473 RVA: 0x000176F7 File Offset: 0x000158F7
			public IFeature HideInternalUrls
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "HideInternalUrls");
				}
			}

			// Token: 0x170006F0 RID: 1776
			// (get) Token: 0x060009AA RID: 2474 RVA: 0x0001770E File Offset: 0x0001590E
			public IFeature IncludeImportContactListButton
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "IncludeImportContactListButton");
				}
			}

			// Token: 0x170006F1 RID: 1777
			// (get) Token: 0x060009AB RID: 2475 RVA: 0x00017725 File Offset: 0x00015925
			public IFeature UseThemeStorageFolder
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "UseThemeStorageFolder");
				}
			}

			// Token: 0x170006F2 RID: 1778
			// (get) Token: 0x060009AC RID: 2476 RVA: 0x0001773C File Offset: 0x0001593C
			public IFeature ConnectedAccountsSync
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("OwaDeployment.settings.ini", "ConnectedAccountsSync");
				}
			}

			// Token: 0x04000476 RID: 1142
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000E9 RID: 233
		public struct PopSettingsIni
		{
			// Token: 0x060009AD RID: 2477 RVA: 0x00017753 File Offset: 0x00015953
			internal PopSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060009AE RID: 2478 RVA: 0x0001775C File Offset: 0x0001595C
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Pop.settings.ini");
			}

			// Token: 0x060009AF RID: 2479 RVA: 0x0001776E File Offset: 0x0001596E
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Pop.settings.ini", id);
			}

			// Token: 0x060009B0 RID: 2480 RVA: 0x00017781 File Offset: 0x00015981
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Pop.settings.ini", id1, ids);
			}

			// Token: 0x170006F3 RID: 1779
			// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00017795 File Offset: 0x00015995
			public IFeature PopClientAccessRulesEnabled
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Pop.settings.ini", "PopClientAccessRulesEnabled");
				}
			}

			// Token: 0x170006F4 RID: 1780
			// (get) Token: 0x060009B2 RID: 2482 RVA: 0x000177AC File Offset: 0x000159AC
			public IFeature IgnoreNonProvisionedServers
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Pop.settings.ini", "IgnoreNonProvisionedServers");
				}
			}

			// Token: 0x170006F5 RID: 1781
			// (get) Token: 0x060009B3 RID: 2483 RVA: 0x000177C3 File Offset: 0x000159C3
			public IFeature UseSamAccountNameAsUsername
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Pop.settings.ini", "UseSamAccountNameAsUsername");
				}
			}

			// Token: 0x170006F6 RID: 1782
			// (get) Token: 0x060009B4 RID: 2484 RVA: 0x000177DA File Offset: 0x000159DA
			public IFeature SkipAuthOnCafe
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Pop.settings.ini", "SkipAuthOnCafe");
				}
			}

			// Token: 0x170006F7 RID: 1783
			// (get) Token: 0x060009B5 RID: 2485 RVA: 0x000177F1 File Offset: 0x000159F1
			public IFeature GlobalCriminalCompliance
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Pop.settings.ini", "GlobalCriminalCompliance");
				}
			}

			// Token: 0x170006F8 RID: 1784
			// (get) Token: 0x060009B6 RID: 2486 RVA: 0x00017808 File Offset: 0x00015A08
			public IFeature CheckOnlyAuthenticationStatus
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Pop.settings.ini", "CheckOnlyAuthenticationStatus");
				}
			}

			// Token: 0x170006F9 RID: 1785
			// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0001781F File Offset: 0x00015A1F
			public IFeature EnforceLogsRetentionPolicy
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Pop.settings.ini", "EnforceLogsRetentionPolicy");
				}
			}

			// Token: 0x170006FA RID: 1786
			// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00017836 File Offset: 0x00015A36
			public IFeature AppendServerNameInBanner
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Pop.settings.ini", "AppendServerNameInBanner");
				}
			}

			// Token: 0x170006FB RID: 1787
			// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0001784D File Offset: 0x00015A4D
			public IFeature UsePrimarySmtpAddress
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Pop.settings.ini", "UsePrimarySmtpAddress");
				}
			}

			// Token: 0x170006FC RID: 1788
			// (get) Token: 0x060009BA RID: 2490 RVA: 0x00017864 File Offset: 0x00015A64
			public IFeature LrsLogging
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Pop.settings.ini", "LrsLogging");
				}
			}

			// Token: 0x04000477 RID: 1143
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000EA RID: 234
		public struct RpcClientAccessSettingsIni
		{
			// Token: 0x060009BB RID: 2491 RVA: 0x0001787B File Offset: 0x00015A7B
			internal RpcClientAccessSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060009BC RID: 2492 RVA: 0x00017884 File Offset: 0x00015A84
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("RpcClientAccess.settings.ini");
			}

			// Token: 0x060009BD RID: 2493 RVA: 0x00017896 File Offset: 0x00015A96
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("RpcClientAccess.settings.ini", id);
			}

			// Token: 0x060009BE RID: 2494 RVA: 0x000178A9 File Offset: 0x00015AA9
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("RpcClientAccess.settings.ini", id1, ids);
			}

			// Token: 0x170006FD RID: 1789
			// (get) Token: 0x060009BF RID: 2495 RVA: 0x000178BD File Offset: 0x00015ABD
			public IFeature FilterModernCalendarItemsMomtIcs
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("RpcClientAccess.settings.ini", "FilterModernCalendarItemsMomtIcs");
				}
			}

			// Token: 0x170006FE RID: 1790
			// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000178D4 File Offset: 0x00015AD4
			public IFeature BlockInsufficientClientVersions
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("RpcClientAccess.settings.ini", "BlockInsufficientClientVersions");
				}
			}

			// Token: 0x170006FF RID: 1791
			// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000178EB File Offset: 0x00015AEB
			public IFeature StreamInsightUploader
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("RpcClientAccess.settings.ini", "StreamInsightUploader");
				}
			}

			// Token: 0x17000700 RID: 1792
			// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00017902 File Offset: 0x00015B02
			public IFeature FilterModernCalendarItemsMomtSearch
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("RpcClientAccess.settings.ini", "FilterModernCalendarItemsMomtSearch");
				}
			}

			// Token: 0x17000701 RID: 1793
			// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00017919 File Offset: 0x00015B19
			public IFeature RpcHttpClientAccessRulesEnabled
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("RpcClientAccess.settings.ini", "RpcHttpClientAccessRulesEnabled");
				}
			}

			// Token: 0x17000702 RID: 1794
			// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00017930 File Offset: 0x00015B30
			public IFeature DetectCharsetAndConvertHtmlBodyOnSave
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("RpcClientAccess.settings.ini", "DetectCharsetAndConvertHtmlBodyOnSave");
				}
			}

			// Token: 0x17000703 RID: 1795
			// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00017947 File Offset: 0x00015B47
			public IFeature MimumResponseSizeEnforcement
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("RpcClientAccess.settings.ini", "MimumResponseSizeEnforcement");
				}
			}

			// Token: 0x17000704 RID: 1796
			// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0001795E File Offset: 0x00015B5E
			public IFeature XtcEndpoint
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("RpcClientAccess.settings.ini", "XtcEndpoint");
				}
			}

			// Token: 0x17000705 RID: 1797
			// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00017975 File Offset: 0x00015B75
			public IFeature IncludeTheBodyPropertyBeingOpeningWhenEvaluatingIfAnyBodyPropertyIsDirty
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("RpcClientAccess.settings.ini", "IncludeTheBodyPropertyBeingOpeningWhenEvaluatingIfAnyBodyPropertyIsDirty");
				}
			}

			// Token: 0x17000706 RID: 1798
			// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0001798C File Offset: 0x00015B8C
			public IFeature ServerInformation
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("RpcClientAccess.settings.ini", "ServerInformation");
				}
			}

			// Token: 0x17000707 RID: 1799
			// (get) Token: 0x060009C9 RID: 2505 RVA: 0x000179A3 File Offset: 0x00015BA3
			public IFeature FilterModernCalendarItemsMomtView
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("RpcClientAccess.settings.ini", "FilterModernCalendarItemsMomtView");
				}
			}

			// Token: 0x04000478 RID: 1144
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000EB RID: 235
		public struct SearchSettingsIni
		{
			// Token: 0x060009CA RID: 2506 RVA: 0x000179BA File Offset: 0x00015BBA
			internal SearchSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060009CB RID: 2507 RVA: 0x000179C3 File Offset: 0x00015BC3
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Search.settings.ini");
			}

			// Token: 0x060009CC RID: 2508 RVA: 0x000179D5 File Offset: 0x00015BD5
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Search.settings.ini", id);
			}

			// Token: 0x060009CD RID: 2509 RVA: 0x000179E8 File Offset: 0x00015BE8
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Search.settings.ini", id1, ids);
			}

			// Token: 0x17000708 RID: 1800
			// (get) Token: 0x060009CE RID: 2510 RVA: 0x000179FC File Offset: 0x00015BFC
			public ITransportFlowSettings TransportFlowSettings
			{
				get
				{
					return this.snapshot.GetObject<ITransportFlowSettings>("Search.settings.ini", "TransportFlowSettings");
				}
			}

			// Token: 0x17000709 RID: 1801
			// (get) Token: 0x060009CF RID: 2511 RVA: 0x00017A13 File Offset: 0x00015C13
			public IFeature RequireMountedForCrawl
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "RequireMountedForCrawl");
				}
			}

			// Token: 0x1700070A RID: 1802
			// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00017A2A File Offset: 0x00015C2A
			public IFeature RemoveOrphanedCatalogs
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "RemoveOrphanedCatalogs");
				}
			}

			// Token: 0x1700070B RID: 1803
			// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00017A41 File Offset: 0x00015C41
			public IIndexStatusSettings IndexStatusInvalidateInterval
			{
				get
				{
					return this.snapshot.GetObject<IIndexStatusSettings>("Search.settings.ini", "IndexStatusInvalidateInterval");
				}
			}

			// Token: 0x1700070C RID: 1804
			// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00017A58 File Offset: 0x00015C58
			public IFeature ProcessItemsWithNullCompositeId
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "ProcessItemsWithNullCompositeId");
				}
			}

			// Token: 0x1700070D RID: 1805
			// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00017A6F File Offset: 0x00015C6F
			public IInstantSearch InstantSearch
			{
				get
				{
					return this.snapshot.GetObject<IInstantSearch>("Search.settings.ini", "InstantSearch");
				}
			}

			// Token: 0x1700070E RID: 1806
			// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00017A86 File Offset: 0x00015C86
			public IFeature CrawlerFeederUpdateCrawlingStatusResetCache
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "CrawlerFeederUpdateCrawlingStatusResetCache");
				}
			}

			// Token: 0x1700070F RID: 1807
			// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00017A9D File Offset: 0x00015C9D
			public ILanguageDetection LanguageDetection
			{
				get
				{
					return this.snapshot.GetObject<ILanguageDetection>("Search.settings.ini", "LanguageDetection");
				}
			}

			// Token: 0x17000710 RID: 1808
			// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00017AB4 File Offset: 0x00015CB4
			public IFeature CachePreWarmingEnabled
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "CachePreWarmingEnabled");
				}
			}

			// Token: 0x17000711 RID: 1809
			// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00017ACB File Offset: 0x00015CCB
			public IFeature MonitorDocumentValidationFailures
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "MonitorDocumentValidationFailures");
				}
			}

			// Token: 0x17000712 RID: 1810
			// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00017AE2 File Offset: 0x00015CE2
			public IFeature UseAlphaSchema
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "UseAlphaSchema");
				}
			}

			// Token: 0x17000713 RID: 1811
			// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00017AF9 File Offset: 0x00015CF9
			public IFeature EnableIndexPartsCache
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "EnableIndexPartsCache");
				}
			}

			// Token: 0x17000714 RID: 1812
			// (get) Token: 0x060009DA RID: 2522 RVA: 0x00017B10 File Offset: 0x00015D10
			public IFeature SchemaUpgrading
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "SchemaUpgrading");
				}
			}

			// Token: 0x17000715 RID: 1813
			// (get) Token: 0x060009DB RID: 2523 RVA: 0x00017B27 File Offset: 0x00015D27
			public IFeature EnableGracefulDegradation
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "EnableGracefulDegradation");
				}
			}

			// Token: 0x17000716 RID: 1814
			// (get) Token: 0x060009DC RID: 2524 RVA: 0x00017B3E File Offset: 0x00015D3E
			public IFeature EnableIndexStatusTimestampVerification
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "EnableIndexStatusTimestampVerification");
				}
			}

			// Token: 0x17000717 RID: 1815
			// (get) Token: 0x060009DD RID: 2525 RVA: 0x00017B55 File Offset: 0x00015D55
			public IFeature EnableDynamicActivationPreference
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "EnableDynamicActivationPreference");
				}
			}

			// Token: 0x17000718 RID: 1816
			// (get) Token: 0x060009DE RID: 2526 RVA: 0x00017B6C File Offset: 0x00015D6C
			public IFeature UseExecuteAndReadPage
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "UseExecuteAndReadPage");
				}
			}

			// Token: 0x17000719 RID: 1817
			// (get) Token: 0x060009DF RID: 2527 RVA: 0x00017B83 File Offset: 0x00015D83
			public ICompletions Completions
			{
				get
				{
					return this.snapshot.GetObject<ICompletions>("Search.settings.ini", "Completions");
				}
			}

			// Token: 0x1700071A RID: 1818
			// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00017B9A File Offset: 0x00015D9A
			public IFeature UseBetaSchema
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "UseBetaSchema");
				}
			}

			// Token: 0x1700071B RID: 1819
			// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00017BB1 File Offset: 0x00015DB1
			public IFeature ReadFlag
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "ReadFlag");
				}
			}

			// Token: 0x1700071C RID: 1820
			// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00017BC8 File Offset: 0x00015DC8
			public IFeature EnableSingleValueRefiners
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "EnableSingleValueRefiners");
				}
			}

			// Token: 0x1700071D RID: 1821
			// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00017BDF File Offset: 0x00015DDF
			public IDocumentFeederSettings DocumentFeederSettings
			{
				get
				{
					return this.snapshot.GetObject<IDocumentFeederSettings>("Search.settings.ini", "DocumentFeederSettings");
				}
			}

			// Token: 0x1700071E RID: 1822
			// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00017BF6 File Offset: 0x00015DF6
			public IFeature CrawlerFeederCollectDocumentsVerifyPendingWatermarks
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "CrawlerFeederCollectDocumentsVerifyPendingWatermarks");
				}
			}

			// Token: 0x1700071F RID: 1823
			// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00017C0D File Offset: 0x00015E0D
			public IMemoryModelSettings MemoryModel
			{
				get
				{
					return this.snapshot.GetObject<IMemoryModelSettings>("Search.settings.ini", "MemoryModel");
				}
			}

			// Token: 0x17000720 RID: 1824
			// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00017C24 File Offset: 0x00015E24
			public IFeature EnableTopN
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "EnableTopN");
				}
			}

			// Token: 0x17000721 RID: 1825
			// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00017C3B File Offset: 0x00015E3B
			public IFeederSettings FeederSettings
			{
				get
				{
					return this.snapshot.GetObject<IFeederSettings>("Search.settings.ini", "FeederSettings");
				}
			}

			// Token: 0x17000722 RID: 1826
			// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00017C52 File Offset: 0x00015E52
			public IFeature WaitForMountPoints
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "WaitForMountPoints");
				}
			}

			// Token: 0x17000723 RID: 1827
			// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00017C69 File Offset: 0x00015E69
			public IFeature EnableInstantSearch
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Search.settings.ini", "EnableInstantSearch");
				}
			}

			// Token: 0x04000479 RID: 1145
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000EC RID: 236
		public struct SharedCacheSettingsIni
		{
			// Token: 0x060009EA RID: 2538 RVA: 0x00017C80 File Offset: 0x00015E80
			internal SharedCacheSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060009EB RID: 2539 RVA: 0x00017C89 File Offset: 0x00015E89
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("SharedCache.settings.ini");
			}

			// Token: 0x060009EC RID: 2540 RVA: 0x00017C9B File Offset: 0x00015E9B
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("SharedCache.settings.ini", id);
			}

			// Token: 0x060009ED RID: 2541 RVA: 0x00017CAE File Offset: 0x00015EAE
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("SharedCache.settings.ini", id1, ids);
			}

			// Token: 0x17000724 RID: 1828
			// (get) Token: 0x060009EE RID: 2542 RVA: 0x00017CC2 File Offset: 0x00015EC2
			public IFeature UsePersistenceForCafe
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("SharedCache.settings.ini", "UsePersistenceForCafe");
				}
			}

			// Token: 0x0400047A RID: 1146
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000ED RID: 237
		public struct SharedMailboxSettingsIni
		{
			// Token: 0x060009EF RID: 2543 RVA: 0x00017CD9 File Offset: 0x00015ED9
			internal SharedMailboxSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060009F0 RID: 2544 RVA: 0x00017CE2 File Offset: 0x00015EE2
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("SharedMailbox.settings.ini");
			}

			// Token: 0x060009F1 RID: 2545 RVA: 0x00017CF4 File Offset: 0x00015EF4
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("SharedMailbox.settings.ini", id);
			}

			// Token: 0x060009F2 RID: 2546 RVA: 0x00017D07 File Offset: 0x00015F07
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("SharedMailbox.settings.ini", id1, ids);
			}

			// Token: 0x17000725 RID: 1829
			// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00017D1B File Offset: 0x00015F1B
			public IFeature SharedMailboxSentItemCopy
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("SharedMailbox.settings.ini", "SharedMailboxSentItemCopy");
				}
			}

			// Token: 0x17000726 RID: 1830
			// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00017D32 File Offset: 0x00015F32
			public IFeature SharedMailboxSentItemsRoutingAgent
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("SharedMailbox.settings.ini", "SharedMailboxSentItemsRoutingAgent");
				}
			}

			// Token: 0x17000727 RID: 1831
			// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00017D49 File Offset: 0x00015F49
			public IFeature SharedMailboxSentItemsDeliveryAgent
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("SharedMailbox.settings.ini", "SharedMailboxSentItemsDeliveryAgent");
				}
			}

			// Token: 0x0400047B RID: 1147
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000EE RID: 238
		public struct TestSettingsIni
		{
			// Token: 0x060009F6 RID: 2550 RVA: 0x00017D60 File Offset: 0x00015F60
			internal TestSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060009F7 RID: 2551 RVA: 0x00017D69 File Offset: 0x00015F69
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Test.settings.ini");
			}

			// Token: 0x060009F8 RID: 2552 RVA: 0x00017D7B File Offset: 0x00015F7B
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Test.settings.ini", id);
			}

			// Token: 0x060009F9 RID: 2553 RVA: 0x00017D8E File Offset: 0x00015F8E
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Test.settings.ini", id1, ids);
			}

			// Token: 0x17000728 RID: 1832
			// (get) Token: 0x060009FA RID: 2554 RVA: 0x00017DA2 File Offset: 0x00015FA2
			public IFeature TestSettingsEnterprise
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Test.settings.ini", "TestSettingsEnterprise");
				}
			}

			// Token: 0x17000729 RID: 1833
			// (get) Token: 0x060009FB RID: 2555 RVA: 0x00017DB9 File Offset: 0x00015FB9
			public IFeature TestSettings
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Test.settings.ini", "TestSettings");
				}
			}

			// Token: 0x1700072A RID: 1834
			// (get) Token: 0x060009FC RID: 2556 RVA: 0x00017DD0 File Offset: 0x00015FD0
			public IFeature TestSettingsOn
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Test.settings.ini", "TestSettingsOn");
				}
			}

			// Token: 0x1700072B RID: 1835
			// (get) Token: 0x060009FD RID: 2557 RVA: 0x00017DE7 File Offset: 0x00015FE7
			public IFeature TestSettingsOff
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Test.settings.ini", "TestSettingsOff");
				}
			}

			// Token: 0x0400047C RID: 1148
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000EF RID: 239
		public struct Test2SettingsIni
		{
			// Token: 0x060009FE RID: 2558 RVA: 0x00017DFE File Offset: 0x00015FFE
			internal Test2SettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x060009FF RID: 2559 RVA: 0x00017E07 File Offset: 0x00016007
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Test2.settings.ini");
			}

			// Token: 0x06000A00 RID: 2560 RVA: 0x00017E19 File Offset: 0x00016019
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Test2.settings.ini", id);
			}

			// Token: 0x06000A01 RID: 2561 RVA: 0x00017E2C File Offset: 0x0001602C
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Test2.settings.ini", id1, ids);
			}

			// Token: 0x1700072C RID: 1836
			// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00017E40 File Offset: 0x00016040
			public IFeature Test2SettingsOn
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Test2.settings.ini", "Test2SettingsOn");
				}
			}

			// Token: 0x1700072D RID: 1837
			// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00017E57 File Offset: 0x00016057
			public IFeature Test2Settings
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Test2.settings.ini", "Test2Settings");
				}
			}

			// Token: 0x1700072E RID: 1838
			// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00017E6E File Offset: 0x0001606E
			public IFeature Test2SettingsEnterprise
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Test2.settings.ini", "Test2SettingsEnterprise");
				}
			}

			// Token: 0x1700072F RID: 1839
			// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00017E85 File Offset: 0x00016085
			public IFeature Test2SettingsOff
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Test2.settings.ini", "Test2SettingsOff");
				}
			}

			// Token: 0x0400047D RID: 1149
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000F0 RID: 240
		public struct TransportSettingsIni
		{
			// Token: 0x06000A06 RID: 2566 RVA: 0x00017E9C File Offset: 0x0001609C
			internal TransportSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000A07 RID: 2567 RVA: 0x00017EA5 File Offset: 0x000160A5
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("Transport.settings.ini");
			}

			// Token: 0x06000A08 RID: 2568 RVA: 0x00017EB7 File Offset: 0x000160B7
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Transport.settings.ini", id);
			}

			// Token: 0x06000A09 RID: 2569 RVA: 0x00017ECA File Offset: 0x000160CA
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("Transport.settings.ini", id1, ids);
			}

			// Token: 0x17000730 RID: 1840
			// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00017EDE File Offset: 0x000160DE
			public IFeature VerboseLogging
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "VerboseLogging");
				}
			}

			// Token: 0x17000731 RID: 1841
			// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00017EF5 File Offset: 0x000160F5
			public IFeature TargetAddressRoutingForRemoteGroupMailbox
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "TargetAddressRoutingForRemoteGroupMailbox");
				}
			}

			// Token: 0x17000732 RID: 1842
			// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00017F0C File Offset: 0x0001610C
			public IMessageDepotSettings MessageDepot
			{
				get
				{
					return this.snapshot.GetObject<IMessageDepotSettings>("Transport.settings.ini", "MessageDepot");
				}
			}

			// Token: 0x17000733 RID: 1843
			// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00017F23 File Offset: 0x00016123
			public IFeature SelectHubServersForClientProxy
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "SelectHubServersForClientProxy");
				}
			}

			// Token: 0x17000734 RID: 1844
			// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00017F3A File Offset: 0x0001613A
			public IFeature TestProcessingQuota
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "TestProcessingQuota");
				}
			}

			// Token: 0x17000735 RID: 1845
			// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00017F51 File Offset: 0x00016151
			public IFeature SystemMessageOverrides
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "SystemMessageOverrides");
				}
			}

			// Token: 0x17000736 RID: 1846
			// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00017F68 File Offset: 0x00016168
			public IFeature DirectTrustCache
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "DirectTrustCache");
				}
			}

			// Token: 0x17000737 RID: 1847
			// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00017F7F File Offset: 0x0001617F
			public IFeature UseNewConnectorMatchingOrder
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "UseNewConnectorMatchingOrder");
				}
			}

			// Token: 0x17000738 RID: 1848
			// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00017F96 File Offset: 0x00016196
			public IFeature TargetAddressDistributionGroupAsExternal
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "TargetAddressDistributionGroupAsExternal");
				}
			}

			// Token: 0x17000739 RID: 1849
			// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00017FAD File Offset: 0x000161AD
			public IFeature ConsolidateAdvancedRouting
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "ConsolidateAdvancedRouting");
				}
			}

			// Token: 0x1700073A RID: 1850
			// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00017FC4 File Offset: 0x000161C4
			public IFeature ClientAuthRequireMailboxDatabase
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "ClientAuthRequireMailboxDatabase");
				}
			}

			// Token: 0x1700073B RID: 1851
			// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00017FDB File Offset: 0x000161DB
			public IFeature UseTenantPartitionToCreateOrganizationId
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "UseTenantPartitionToCreateOrganizationId");
				}
			}

			// Token: 0x1700073C RID: 1852
			// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00017FF2 File Offset: 0x000161F2
			public IFeature LimitTransportRules
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "LimitTransportRules");
				}
			}

			// Token: 0x1700073D RID: 1853
			// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00018009 File Offset: 0x00016209
			public IFeature SmtpAcceptAnyRecipient
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "SmtpAcceptAnyRecipient");
				}
			}

			// Token: 0x1700073E RID: 1854
			// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00018020 File Offset: 0x00016220
			public IFeature RiskBasedCounters
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "RiskBasedCounters");
				}
			}

			// Token: 0x1700073F RID: 1855
			// (get) Token: 0x06000A19 RID: 2585 RVA: 0x00018037 File Offset: 0x00016237
			public IFeature DefaultTransportServiceStateToInactive
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "DefaultTransportServiceStateToInactive");
				}
			}

			// Token: 0x17000740 RID: 1856
			// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0001804E File Offset: 0x0001624E
			public IFeature LimitRemoteDomains
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "LimitRemoteDomains");
				}
			}

			// Token: 0x17000741 RID: 1857
			// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00018065 File Offset: 0x00016265
			public IFeature IgnoreArbitrationMailboxForModeratedRecipient
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "IgnoreArbitrationMailboxForModeratedRecipient");
				}
			}

			// Token: 0x17000742 RID: 1858
			// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0001807C File Offset: 0x0001627C
			public IFeature TransferAdditionalTenantDataThroughXATTR
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "TransferAdditionalTenantDataThroughXATTR");
				}
			}

			// Token: 0x17000743 RID: 1859
			// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00018093 File Offset: 0x00016293
			public IFeature ADExceptionHandling
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "ADExceptionHandling");
				}
			}

			// Token: 0x17000744 RID: 1860
			// (get) Token: 0x06000A1E RID: 2590 RVA: 0x000180AA File Offset: 0x000162AA
			public IFeature EnforceProcessingQuota
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "EnforceProcessingQuota");
				}
			}

			// Token: 0x17000745 RID: 1861
			// (get) Token: 0x06000A1F RID: 2591 RVA: 0x000180C1 File Offset: 0x000162C1
			public IFeature SystemProbeDropAgent
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "SystemProbeDropAgent");
				}
			}

			// Token: 0x17000746 RID: 1862
			// (get) Token: 0x06000A20 RID: 2592 RVA: 0x000180D8 File Offset: 0x000162D8
			public IFeature SetMustDeliverJournalReport
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "SetMustDeliverJournalReport");
				}
			}

			// Token: 0x17000747 RID: 1863
			// (get) Token: 0x06000A21 RID: 2593 RVA: 0x000180EF File Offset: 0x000162EF
			public IFeature SendUserAddressInXproxyCommand
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "SendUserAddressInXproxyCommand");
				}
			}

			// Token: 0x17000748 RID: 1864
			// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00018106 File Offset: 0x00016306
			public IFeature UseAdditionalTenantDataFromXATTR
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "UseAdditionalTenantDataFromXATTR");
				}
			}

			// Token: 0x17000749 RID: 1865
			// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0001811D File Offset: 0x0001631D
			public IFeature DelayDsn
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "DelayDsn");
				}
			}

			// Token: 0x1700074A RID: 1866
			// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00018134 File Offset: 0x00016334
			public IFeature ExplicitDeletedObjectNotifications
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "ExplicitDeletedObjectNotifications");
				}
			}

			// Token: 0x1700074B RID: 1867
			// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0001814B File Offset: 0x0001634B
			public IFeature EnforceQueueQuota
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "EnforceQueueQuota");
				}
			}

			// Token: 0x1700074C RID: 1868
			// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00018162 File Offset: 0x00016362
			public IFeature OrganizationMailboxRouting
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "OrganizationMailboxRouting");
				}
			}

			// Token: 0x1700074D RID: 1869
			// (get) Token: 0x06000A27 RID: 2599 RVA: 0x00018179 File Offset: 0x00016379
			public IFeature StringentHeaderTransformationMode
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "StringentHeaderTransformationMode");
				}
			}

			// Token: 0x1700074E RID: 1870
			// (get) Token: 0x06000A28 RID: 2600 RVA: 0x00018190 File Offset: 0x00016390
			public IFeature SmtpReceiveCountersStripServerName
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "SmtpReceiveCountersStripServerName");
				}
			}

			// Token: 0x1700074F RID: 1871
			// (get) Token: 0x06000A29 RID: 2601 RVA: 0x000181A7 File Offset: 0x000163A7
			public IFeature ClientSubmissionToDelivery
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "ClientSubmissionToDelivery");
				}
			}

			// Token: 0x17000750 RID: 1872
			// (get) Token: 0x06000A2A RID: 2602 RVA: 0x000181BE File Offset: 0x000163BE
			public IFeature SmtpXproxyConstructUpnFromSamAccountNameAndParitionFqdn
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "SmtpXproxyConstructUpnFromSamAccountNameAndParitionFqdn");
				}
			}

			// Token: 0x17000751 RID: 1873
			// (get) Token: 0x06000A2B RID: 2603 RVA: 0x000181D5 File Offset: 0x000163D5
			public IFeature EnforceOutboundConnectorAndAcceptedDomainsRestriction
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "EnforceOutboundConnectorAndAcceptedDomainsRestriction");
				}
			}

			// Token: 0x17000752 RID: 1874
			// (get) Token: 0x06000A2C RID: 2604 RVA: 0x000181EC File Offset: 0x000163EC
			public IFeature TenantThrottling
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "TenantThrottling");
				}
			}

			// Token: 0x17000753 RID: 1875
			// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00018203 File Offset: 0x00016403
			public IFeature SystemProbeLogging
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("Transport.settings.ini", "SystemProbeLogging");
				}
			}

			// Token: 0x0400047E RID: 1150
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000F1 RID: 241
		public struct UCCSettingsIni
		{
			// Token: 0x06000A2E RID: 2606 RVA: 0x0001821A File Offset: 0x0001641A
			internal UCCSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000A2F RID: 2607 RVA: 0x00018223 File Offset: 0x00016423
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("UCC.settings.ini");
			}

			// Token: 0x06000A30 RID: 2608 RVA: 0x00018235 File Offset: 0x00016435
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("UCC.settings.ini", id);
			}

			// Token: 0x06000A31 RID: 2609 RVA: 0x00018248 File Offset: 0x00016448
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("UCC.settings.ini", id1, ids);
			}

			// Token: 0x17000754 RID: 1876
			// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0001825C File Offset: 0x0001645C
			public IFeature UCC
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UCC.settings.ini", "UCC");
				}
			}

			// Token: 0x0400047F RID: 1151
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000F2 RID: 242
		public struct UMSettingsIni
		{
			// Token: 0x06000A33 RID: 2611 RVA: 0x00018273 File Offset: 0x00016473
			internal UMSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000A34 RID: 2612 RVA: 0x0001827C File Offset: 0x0001647C
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("UM.settings.ini");
			}

			// Token: 0x06000A35 RID: 2613 RVA: 0x0001828E File Offset: 0x0001648E
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("UM.settings.ini", id);
			}

			// Token: 0x06000A36 RID: 2614 RVA: 0x000182A1 File Offset: 0x000164A1
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("UM.settings.ini", id1, ids);
			}

			// Token: 0x17000755 RID: 1877
			// (get) Token: 0x06000A37 RID: 2615 RVA: 0x000182B5 File Offset: 0x000164B5
			public IFeature UMDataCenterLogging
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "UMDataCenterLogging");
				}
			}

			// Token: 0x17000756 RID: 1878
			// (get) Token: 0x06000A38 RID: 2616 RVA: 0x000182CC File Offset: 0x000164CC
			public IFeature VoicemailDiskSpaceDatacenterLimit
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "VoicemailDiskSpaceDatacenterLimit");
				}
			}

			// Token: 0x17000757 RID: 1879
			// (get) Token: 0x06000A39 RID: 2617 RVA: 0x000182E3 File Offset: 0x000164E3
			public IFeature DatacenterUMGrammarTenantCache
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "DatacenterUMGrammarTenantCache");
				}
			}

			// Token: 0x17000758 RID: 1880
			// (get) Token: 0x06000A3A RID: 2618 RVA: 0x000182FA File Offset: 0x000164FA
			public IFeature DirectoryGrammarCountLimit
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "DirectoryGrammarCountLimit");
				}
			}

			// Token: 0x17000759 RID: 1881
			// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00018311 File Offset: 0x00016511
			public IFeature UMDataCenterAD
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "UMDataCenterAD");
				}
			}

			// Token: 0x1700075A RID: 1882
			// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00018328 File Offset: 0x00016528
			public IFeature AddressListGrammars
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "AddressListGrammars");
				}
			}

			// Token: 0x1700075B RID: 1883
			// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0001833F File Offset: 0x0001653F
			public IFeature GetServerDialPlans
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "GetServerDialPlans");
				}
			}

			// Token: 0x1700075C RID: 1884
			// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00018356 File Offset: 0x00016556
			public IFeature UMDataCenterLanguages
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "UMDataCenterLanguages");
				}
			}

			// Token: 0x1700075D RID: 1885
			// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0001836D File Offset: 0x0001656D
			public IFeature UMDataCenterCallRouting
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "UMDataCenterCallRouting");
				}
			}

			// Token: 0x1700075E RID: 1886
			// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00018384 File Offset: 0x00016584
			public IFeature HuntGroupCreationForSipDialplans
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "HuntGroupCreationForSipDialplans");
				}
			}

			// Token: 0x1700075F RID: 1887
			// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0001839B File Offset: 0x0001659B
			public IFeature DTMFMapGenerator
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "DTMFMapGenerator");
				}
			}

			// Token: 0x17000760 RID: 1888
			// (get) Token: 0x06000A42 RID: 2626 RVA: 0x000183B2 File Offset: 0x000165B2
			public IFeature AlwaysLogTraces
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "AlwaysLogTraces");
				}
			}

			// Token: 0x17000761 RID: 1889
			// (get) Token: 0x06000A43 RID: 2627 RVA: 0x000183C9 File Offset: 0x000165C9
			public IFeature AnonymizeLogging
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "AnonymizeLogging");
				}
			}

			// Token: 0x17000762 RID: 1890
			// (get) Token: 0x06000A44 RID: 2628 RVA: 0x000183E0 File Offset: 0x000165E0
			public IFeature ServerDialPlanLink
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "ServerDialPlanLink");
				}
			}

			// Token: 0x17000763 RID: 1891
			// (get) Token: 0x06000A45 RID: 2629 RVA: 0x000183F7 File Offset: 0x000165F7
			public IFeature SipInfoNotifications
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("UM.settings.ini", "SipInfoNotifications");
				}
			}

			// Token: 0x04000480 RID: 1152
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000F3 RID: 243
		public struct VariantConfigSettingsIni
		{
			// Token: 0x06000A46 RID: 2630 RVA: 0x0001840E File Offset: 0x0001660E
			internal VariantConfigSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000A47 RID: 2631 RVA: 0x00018417 File Offset: 0x00016617
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("VariantConfig.settings.ini");
			}

			// Token: 0x06000A48 RID: 2632 RVA: 0x00018429 File Offset: 0x00016629
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("VariantConfig.settings.ini", id);
			}

			// Token: 0x06000A49 RID: 2633 RVA: 0x0001843C File Offset: 0x0001663C
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("VariantConfig.settings.ini", id1, ids);
			}

			// Token: 0x17000764 RID: 1892
			// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00018450 File Offset: 0x00016650
			public IOrganizationNameSettings Microsoft
			{
				get
				{
					return this.snapshot.GetObject<IOrganizationNameSettings>("VariantConfig.settings.ini", "Microsoft");
				}
			}

			// Token: 0x17000765 RID: 1893
			// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00018467 File Offset: 0x00016667
			public IFeature InternalAccess
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("VariantConfig.settings.ini", "InternalAccess");
				}
			}

			// Token: 0x17000766 RID: 1894
			// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0001847E File Offset: 0x0001667E
			public IOverrideSyncSettings SettingOverrideSync
			{
				get
				{
					return this.snapshot.GetObject<IOverrideSyncSettings>("VariantConfig.settings.ini", "SettingOverrideSync");
				}
			}

			// Token: 0x04000481 RID: 1153
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000F4 RID: 244
		public struct WorkingSetSettingsIni
		{
			// Token: 0x06000A4D RID: 2637 RVA: 0x00018495 File Offset: 0x00016695
			internal WorkingSetSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000A4E RID: 2638 RVA: 0x0001849E File Offset: 0x0001669E
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("WorkingSet.settings.ini");
			}

			// Token: 0x06000A4F RID: 2639 RVA: 0x000184B0 File Offset: 0x000166B0
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("WorkingSet.settings.ini", id);
			}

			// Token: 0x06000A50 RID: 2640 RVA: 0x000184C3 File Offset: 0x000166C3
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("WorkingSet.settings.ini", id1, ids);
			}

			// Token: 0x17000767 RID: 1895
			// (get) Token: 0x06000A51 RID: 2641 RVA: 0x000184D7 File Offset: 0x000166D7
			public IFeature WorkingSetAgent
			{
				get
				{
					return this.snapshot.GetObject<IFeature>("WorkingSet.settings.ini", "WorkingSetAgent");
				}
			}

			// Token: 0x04000482 RID: 1154
			private VariantConfigurationSnapshot snapshot;
		}

		// Token: 0x020000F5 RID: 245
		public struct WorkloadManagementSettingsIni
		{
			// Token: 0x06000A52 RID: 2642 RVA: 0x000184EE File Offset: 0x000166EE
			internal WorkloadManagementSettingsIni(VariantConfigurationSnapshot snapshot)
			{
				this.snapshot = snapshot;
			}

			// Token: 0x06000A53 RID: 2643 RVA: 0x000184F7 File Offset: 0x000166F7
			public IDictionary<string, T> GetObjectsOfType<T>() where T : class, ISettings
			{
				return this.snapshot.GetObjectsOfType<T>("WorkloadManagement.settings.ini");
			}

			// Token: 0x06000A54 RID: 2644 RVA: 0x00018509 File Offset: 0x00016709
			public T GetObject<T>(string id) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("WorkloadManagement.settings.ini", id);
			}

			// Token: 0x06000A55 RID: 2645 RVA: 0x0001851C File Offset: 0x0001671C
			public T GetObject<T>(object id1, params object[] ids) where T : class, ISettings
			{
				return this.snapshot.GetObject<T>("WorkloadManagement.settings.ini", id1, ids);
			}

			// Token: 0x17000768 RID: 1896
			// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00018530 File Offset: 0x00016730
			public IWorkloadSettings PowerShell
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "PowerShell");
				}
			}

			// Token: 0x17000769 RID: 1897
			// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00018547 File Offset: 0x00016747
			public IWorkloadSettings PowerShellForwardSync
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "PowerShellForwardSync");
				}
			}

			// Token: 0x1700076A RID: 1898
			// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0001855E File Offset: 0x0001675E
			public IWorkloadSettings Ews
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "Ews");
				}
			}

			// Token: 0x1700076B RID: 1899
			// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00018575 File Offset: 0x00016775
			public IResourceSettings Processor
			{
				get
				{
					return this.snapshot.GetObject<IResourceSettings>("WorkloadManagement.settings.ini", "Processor");
				}
			}

			// Token: 0x1700076C RID: 1900
			// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0001858C File Offset: 0x0001678C
			public IWorkloadSettings StoreUrgentMaintenanceAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "StoreUrgentMaintenanceAssistant");
				}
			}

			// Token: 0x1700076D RID: 1901
			// (get) Token: 0x06000A5B RID: 2651 RVA: 0x000185A3 File Offset: 0x000167A3
			public IWorkloadSettings SharePointSignalStoreAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "SharePointSignalStoreAssistant");
				}
			}

			// Token: 0x1700076E RID: 1902
			// (get) Token: 0x06000A5C RID: 2652 RVA: 0x000185BA File Offset: 0x000167BA
			public IWorkloadSettings StoreOnlineIntegrityCheckAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "StoreOnlineIntegrityCheckAssistant");
				}
			}

			// Token: 0x1700076F RID: 1903
			// (get) Token: 0x06000A5D RID: 2653 RVA: 0x000185D1 File Offset: 0x000167D1
			public IWorkloadSettings PowerShellLowPriorityWorkFlow
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "PowerShellLowPriorityWorkFlow");
				}
			}

			// Token: 0x17000770 RID: 1904
			// (get) Token: 0x06000A5E RID: 2654 RVA: 0x000185E8 File Offset: 0x000167E8
			public IWorkloadSettings E4eRecipient
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "E4eRecipient");
				}
			}

			// Token: 0x17000771 RID: 1905
			// (get) Token: 0x06000A5F RID: 2655 RVA: 0x000185FF File Offset: 0x000167FF
			public IWorkloadSettings Eas
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "Eas");
				}
			}

			// Token: 0x17000772 RID: 1906
			// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00018616 File Offset: 0x00016816
			public IWorkloadSettings Transport
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "Transport");
				}
			}

			// Token: 0x17000773 RID: 1907
			// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0001862D File Offset: 0x0001682D
			public IWorkloadSettings InferenceDataCollectionAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "InferenceDataCollectionAssistant");
				}
			}

			// Token: 0x17000774 RID: 1908
			// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00018644 File Offset: 0x00016844
			public IWorkloadSettings Owa
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "Owa");
				}
			}

			// Token: 0x17000775 RID: 1909
			// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0001865B File Offset: 0x0001685B
			public IWorkloadSettings PeopleRelevanceAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "PeopleRelevanceAssistant");
				}
			}

			// Token: 0x17000776 RID: 1910
			// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00018672 File Offset: 0x00016872
			public IWorkloadSettings PublicFolderAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "PublicFolderAssistant");
				}
			}

			// Token: 0x17000777 RID: 1911
			// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00018689 File Offset: 0x00016889
			public IWorkloadSettings TransportSync
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "TransportSync");
				}
			}

			// Token: 0x17000778 RID: 1912
			// (get) Token: 0x06000A66 RID: 2662 RVA: 0x000186A0 File Offset: 0x000168A0
			public IWorkloadSettings OrgContactsSyncAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "OrgContactsSyncAssistant");
				}
			}

			// Token: 0x17000779 RID: 1913
			// (get) Token: 0x06000A67 RID: 2663 RVA: 0x000186B7 File Offset: 0x000168B7
			public IWorkloadSettings InferenceTrainingAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "InferenceTrainingAssistant");
				}
			}

			// Token: 0x1700077A RID: 1914
			// (get) Token: 0x06000A68 RID: 2664 RVA: 0x000186CE File Offset: 0x000168CE
			public IWorkloadSettings ProbeTimeBasedAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "ProbeTimeBasedAssistant");
				}
			}

			// Token: 0x1700077B RID: 1915
			// (get) Token: 0x06000A69 RID: 2665 RVA: 0x000186E5 File Offset: 0x000168E5
			public IWorkloadSettings DarRuntime
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "DarRuntime");
				}
			}

			// Token: 0x1700077C RID: 1916
			// (get) Token: 0x06000A6A RID: 2666 RVA: 0x000186FC File Offset: 0x000168FC
			public IBlackoutSettings Blackout
			{
				get
				{
					return this.snapshot.GetObject<IBlackoutSettings>("WorkloadManagement.settings.ini", "Blackout");
				}
			}

			// Token: 0x1700077D RID: 1917
			// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00018713 File Offset: 0x00016913
			public IWorkloadSettings MailboxReplicationServiceHighPriority
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "MailboxReplicationServiceHighPriority");
				}
			}

			// Token: 0x1700077E RID: 1918
			// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0001872A File Offset: 0x0001692A
			public IWorkloadSettings MailboxReplicationServiceInteractive
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "MailboxReplicationServiceInteractive");
				}
			}

			// Token: 0x1700077F RID: 1919
			// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00018741 File Offset: 0x00016941
			public IWorkloadSettings StoreMaintenanceAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "StoreMaintenanceAssistant");
				}
			}

			// Token: 0x17000780 RID: 1920
			// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00018758 File Offset: 0x00016958
			public IWorkloadSettings CalendarSyncAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "CalendarSyncAssistant");
				}
			}

			// Token: 0x17000781 RID: 1921
			// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0001876F File Offset: 0x0001696F
			public IWorkloadSettings DarTaskStoreTimeBasedAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "DarTaskStoreTimeBasedAssistant");
				}
			}

			// Token: 0x17000782 RID: 1922
			// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00018786 File Offset: 0x00016986
			public IWorkloadSettings ELCAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "ELCAssistant");
				}
			}

			// Token: 0x17000783 RID: 1923
			// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0001879D File Offset: 0x0001699D
			public ISystemWorkloadManagerSettings SystemWorkloadManager
			{
				get
				{
					return this.snapshot.GetObject<ISystemWorkloadManagerSettings>("WorkloadManagement.settings.ini", "SystemWorkloadManager");
				}
			}

			// Token: 0x17000784 RID: 1924
			// (get) Token: 0x06000A72 RID: 2674 RVA: 0x000187B4 File Offset: 0x000169B4
			public IResourceSettings DiskLatency
			{
				get
				{
					return this.snapshot.GetObject<IResourceSettings>("WorkloadManagement.settings.ini", "DiskLatency");
				}
			}

			// Token: 0x17000785 RID: 1925
			// (get) Token: 0x06000A73 RID: 2675 RVA: 0x000187CB File Offset: 0x000169CB
			public IWorkloadSettings O365SuiteService
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "O365SuiteService");
				}
			}

			// Token: 0x17000786 RID: 1926
			// (get) Token: 0x06000A74 RID: 2676 RVA: 0x000187E2 File Offset: 0x000169E2
			public IDiskLatencyMonitorSettings DiskLatencySettings
			{
				get
				{
					return this.snapshot.GetObject<IDiskLatencyMonitorSettings>("WorkloadManagement.settings.ini", "DiskLatencySettings");
				}
			}

			// Token: 0x17000787 RID: 1927
			// (get) Token: 0x06000A75 RID: 2677 RVA: 0x000187F9 File Offset: 0x000169F9
			public IWorkloadSettings PublicFolderMailboxSync
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "PublicFolderMailboxSync");
				}
			}

			// Token: 0x17000788 RID: 1928
			// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00018810 File Offset: 0x00016A10
			public IWorkloadSettings OABGeneratorAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "OABGeneratorAssistant");
				}
			}

			// Token: 0x17000789 RID: 1929
			// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00018827 File Offset: 0x00016A27
			public IWorkloadSettings TeamMailboxSync
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "TeamMailboxSync");
				}
			}

			// Token: 0x1700078A RID: 1930
			// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0001883E File Offset: 0x00016A3E
			public IWorkloadSettings StoreScheduledIntegrityCheckAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "StoreScheduledIntegrityCheckAssistant");
				}
			}

			// Token: 0x1700078B RID: 1931
			// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00018855 File Offset: 0x00016A55
			public IWorkloadSettings Domt
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "Domt");
				}
			}

			// Token: 0x1700078C RID: 1932
			// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0001886C File Offset: 0x00016A6C
			public IWorkloadSettings MailboxReplicationServiceInternalMaintenance
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "MailboxReplicationServiceInternalMaintenance");
				}
			}

			// Token: 0x1700078D RID: 1933
			// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00018883 File Offset: 0x00016A83
			public IWorkloadSettings PowerShellGalSync
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "PowerShellGalSync");
				}
			}

			// Token: 0x1700078E RID: 1934
			// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0001889A File Offset: 0x00016A9A
			public IWorkloadSettings Momt
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "Momt");
				}
			}

			// Token: 0x1700078F RID: 1935
			// (get) Token: 0x06000A7D RID: 2685 RVA: 0x000188B1 File Offset: 0x00016AB1
			public IWorkloadSettings MailboxProcessorAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "MailboxProcessorAssistant");
				}
			}

			// Token: 0x17000790 RID: 1936
			// (get) Token: 0x06000A7E RID: 2686 RVA: 0x000188C8 File Offset: 0x00016AC8
			public IWorkloadSettings SearchIndexRepairTimebasedAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "SearchIndexRepairTimebasedAssistant");
				}
			}

			// Token: 0x17000791 RID: 1937
			// (get) Token: 0x06000A7F RID: 2687 RVA: 0x000188DF File Offset: 0x00016ADF
			public IWorkloadSettings PeopleCentricTriageAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "PeopleCentricTriageAssistant");
				}
			}

			// Token: 0x17000792 RID: 1938
			// (get) Token: 0x06000A80 RID: 2688 RVA: 0x000188F6 File Offset: 0x00016AF6
			public IWorkloadSettings TopNAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "TopNAssistant");
				}
			}

			// Token: 0x17000793 RID: 1939
			// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0001890D File Offset: 0x00016B0D
			public IWorkloadSettings OwaVoice
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "OwaVoice");
				}
			}

			// Token: 0x17000794 RID: 1940
			// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00018924 File Offset: 0x00016B24
			public IWorkloadSettings E4eSender
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "E4eSender");
				}
			}

			// Token: 0x17000795 RID: 1941
			// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0001893B File Offset: 0x00016B3B
			public IWorkloadSettings Imap
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "Imap");
				}
			}

			// Token: 0x17000796 RID: 1942
			// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00018952 File Offset: 0x00016B52
			public IWorkloadSettings SiteMailboxAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "SiteMailboxAssistant");
				}
			}

			// Token: 0x17000797 RID: 1943
			// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00018969 File Offset: 0x00016B69
			public IWorkloadSettings UMReportingAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "UMReportingAssistant");
				}
			}

			// Token: 0x17000798 RID: 1944
			// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00018980 File Offset: 0x00016B80
			public IResourceSettings ActiveDirectoryReplicationLatency
			{
				get
				{
					return this.snapshot.GetObject<IResourceSettings>("WorkloadManagement.settings.ini", "ActiveDirectoryReplicationLatency");
				}
			}

			// Token: 0x17000799 RID: 1945
			// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00018997 File Offset: 0x00016B97
			public IWorkloadSettings OutlookService
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "OutlookService");
				}
			}

			// Token: 0x1700079A RID: 1946
			// (get) Token: 0x06000A88 RID: 2696 RVA: 0x000189AE File Offset: 0x00016BAE
			public IWorkloadSettings GroupMailboxAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "GroupMailboxAssistant");
				}
			}

			// Token: 0x1700079B RID: 1947
			// (get) Token: 0x06000A89 RID: 2697 RVA: 0x000189C5 File Offset: 0x00016BC5
			public IResourceSettings MdbAvailability
			{
				get
				{
					return this.snapshot.GetObject<IResourceSettings>("WorkloadManagement.settings.ini", "MdbAvailability");
				}
			}

			// Token: 0x1700079C RID: 1948
			// (get) Token: 0x06000A8A RID: 2698 RVA: 0x000189DC File Offset: 0x00016BDC
			public IResourceSettings MdbLatency
			{
				get
				{
					return this.snapshot.GetObject<IResourceSettings>("WorkloadManagement.settings.ini", "MdbLatency");
				}
			}

			// Token: 0x1700079D RID: 1949
			// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000189F3 File Offset: 0x00016BF3
			public IWorkloadSettings JunkEmailOptionsCommitterAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "JunkEmailOptionsCommitterAssistant");
				}
			}

			// Token: 0x1700079E RID: 1950
			// (get) Token: 0x06000A8C RID: 2700 RVA: 0x00018A0A File Offset: 0x00016C0A
			public IWorkloadSettings DirectoryProcessorAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "DirectoryProcessorAssistant");
				}
			}

			// Token: 0x1700079F RID: 1951
			// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00018A21 File Offset: 0x00016C21
			public IWorkloadSettings CalendarRepairAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "CalendarRepairAssistant");
				}
			}

			// Token: 0x170007A0 RID: 1952
			// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00018A38 File Offset: 0x00016C38
			public IWorkloadSettings MailboxAssociationReplicationAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "MailboxAssociationReplicationAssistant");
				}
			}

			// Token: 0x170007A1 RID: 1953
			// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00018A4F File Offset: 0x00016C4F
			public IWorkloadSettings StoreDSMaintenanceAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "StoreDSMaintenanceAssistant");
				}
			}

			// Token: 0x170007A2 RID: 1954
			// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00018A66 File Offset: 0x00016C66
			public IResourceSettings CiAgeOfLastNotification
			{
				get
				{
					return this.snapshot.GetObject<IResourceSettings>("WorkloadManagement.settings.ini", "CiAgeOfLastNotification");
				}
			}

			// Token: 0x170007A3 RID: 1955
			// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00018A7D File Offset: 0x00016C7D
			public IWorkloadSettings SharingPolicyAssistant
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "SharingPolicyAssistant");
				}
			}

			// Token: 0x170007A4 RID: 1956
			// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00018A94 File Offset: 0x00016C94
			public IResourceSettings MdbReplication
			{
				get
				{
					return this.snapshot.GetObject<IResourceSettings>("WorkloadManagement.settings.ini", "MdbReplication");
				}
			}

			// Token: 0x170007A5 RID: 1957
			// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00018AAB File Offset: 0x00016CAB
			public IWorkloadSettings PowerShellBackSync
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "PowerShellBackSync");
				}
			}

			// Token: 0x170007A6 RID: 1958
			// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00018AC2 File Offset: 0x00016CC2
			public IWorkloadSettings Pop
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "Pop");
				}
			}

			// Token: 0x170007A7 RID: 1959
			// (get) Token: 0x06000A95 RID: 2709 RVA: 0x00018AD9 File Offset: 0x00016CD9
			public IWorkloadSettings MailboxReplicationService
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "MailboxReplicationService");
				}
			}

			// Token: 0x170007A8 RID: 1960
			// (get) Token: 0x06000A96 RID: 2710 RVA: 0x00018AF0 File Offset: 0x00016CF0
			public IWorkloadSettings PushNotificationService
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "PushNotificationService");
				}
			}

			// Token: 0x170007A9 RID: 1961
			// (get) Token: 0x06000A97 RID: 2711 RVA: 0x00018B07 File Offset: 0x00016D07
			public IWorkloadSettings PowerShellDiscretionaryWorkFlow
			{
				get
				{
					return this.snapshot.GetObject<IWorkloadSettings>("WorkloadManagement.settings.ini", "PowerShellDiscretionaryWorkFlow");
				}
			}

			// Token: 0x04000483 RID: 1155
			private VariantConfigurationSnapshot snapshot;
		}
	}
}
