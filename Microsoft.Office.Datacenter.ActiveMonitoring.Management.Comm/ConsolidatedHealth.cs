using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class ConsolidatedHealth : ConfigurableObject
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00004C03 File Offset: 0x00002E03
		public ConsolidatedHealth() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004C10 File Offset: 0x00002E10
		internal ConsolidatedHealth(string server, string healthSet, string healthGroup) : this(server, healthSet, healthGroup, MonitorAlertState.Unknown, MonitorServerComponentState.Unknown, 0, 0, DateTime.MinValue, new List<MonitorHealthEntry>())
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004C34 File Offset: 0x00002E34
		internal ConsolidatedHealth(string server, string healthSet, string healthGroup, MonitorAlertState alertValue, MonitorServerComponentState state, int monitorCount, int haImpactingMonitorCount, DateTime lastTransitionTime, IEnumerable<MonitorHealthEntry> groupedEntries) : this()
		{
			this.Server = server;
			this.HealthSet = healthSet;
			this.HealthGroup = healthGroup;
			this.AlertValue = alertValue;
			this.State = state;
			this.MonitorCount = monitorCount;
			this.HaImpactingMonitorCount = haImpactingMonitorCount;
			this.LastTransitionTime = lastTransitionTime;
			this.Entries = new MultiValuedProperty<MonitorHealthEntry>(groupedEntries.ToArray<MonitorHealthEntry>());
			this[SimpleProviderObjectSchema.Identity] = new ConsolidatedHealth.ConsolidatedHealthId(healthSet, server);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004CA8 File Offset: 0x00002EA8
		internal ConsolidatedHealth(string healthSet, string healthGroup, MonitorAlertState alertValue, MonitorServerComponentState state, int monitorCount, int haImpactingMonitorCount, DateTime lastTransitionTime, IEnumerable<ConsolidatedHealth> consolidatedEntries) : this()
		{
			this.Server = null;
			this.HealthSet = healthSet;
			this.HealthGroup = healthGroup;
			this.AlertValue = alertValue;
			this.State = state;
			this.MonitorCount = monitorCount;
			this.HaImpactingMonitorCount = haImpactingMonitorCount;
			this.LastTransitionTime = lastTransitionTime;
			MultiValuedProperty<MonitorHealthEntry> multiValuedProperty = new MultiValuedProperty<MonitorHealthEntry>();
			foreach (ConsolidatedHealth consolidatedHealth in consolidatedEntries)
			{
				foreach (MonitorHealthEntry item in consolidatedHealth.Entries)
				{
					multiValuedProperty.Add(item);
				}
			}
			this.Entries = multiValuedProperty;
			this[SimpleProviderObjectSchema.Identity] = new ConsolidatedHealth.ConsolidatedHealthId(healthSet, null);
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004D90 File Offset: 0x00002F90
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00004DA2 File Offset: 0x00002FA2
		public string Server
		{
			get
			{
				return (string)this[ConsolidatedHealthSchema.Server];
			}
			private set
			{
				this[ConsolidatedHealthSchema.Server] = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004DB0 File Offset: 0x00002FB0
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00004DC2 File Offset: 0x00002FC2
		public MonitorServerComponentState State
		{
			get
			{
				return (MonitorServerComponentState)this[ConsolidatedHealthSchema.State];
			}
			private set
			{
				this[ConsolidatedHealthSchema.State] = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004DD5 File Offset: 0x00002FD5
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00004DE7 File Offset: 0x00002FE7
		public string HealthSet
		{
			get
			{
				return (string)this[ConsolidatedHealthSchema.HealthSet];
			}
			private set
			{
				this[ConsolidatedHealthSchema.HealthSet] = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004DF5 File Offset: 0x00002FF5
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00004E07 File Offset: 0x00003007
		public string HealthGroup
		{
			get
			{
				return (string)this[ConsolidatedHealthSchema.HealthGroup];
			}
			private set
			{
				this[ConsolidatedHealthSchema.HealthGroup] = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004E15 File Offset: 0x00003015
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00004E27 File Offset: 0x00003027
		public MonitorAlertState AlertValue
		{
			get
			{
				return (MonitorAlertState)this[ConsolidatedHealthSchema.AlertValue];
			}
			private set
			{
				this[ConsolidatedHealthSchema.AlertValue] = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004E3A File Offset: 0x0000303A
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00004E4C File Offset: 0x0000304C
		public DateTime LastTransitionTime
		{
			get
			{
				return (DateTime)this[ConsolidatedHealthSchema.LastTransitionTime];
			}
			private set
			{
				this[ConsolidatedHealthSchema.LastTransitionTime] = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004E5F File Offset: 0x0000305F
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00004E71 File Offset: 0x00003071
		public int MonitorCount
		{
			get
			{
				return (int)this[ConsolidatedHealthSchema.MonitorCount];
			}
			private set
			{
				this[ConsolidatedHealthSchema.MonitorCount] = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004E84 File Offset: 0x00003084
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00004E96 File Offset: 0x00003096
		public int HaImpactingMonitorCount
		{
			get
			{
				return (int)this[ConsolidatedHealthSchema.HaImpactingMonitorCount];
			}
			private set
			{
				this[ConsolidatedHealthSchema.HaImpactingMonitorCount] = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004EA9 File Offset: 0x000030A9
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00004EB1 File Offset: 0x000030B1
		public MultiValuedProperty<MonitorHealthEntry> Entries
		{
			get
			{
				return this.healthEntries;
			}
			private set
			{
				this.healthEntries = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004EBA File Offset: 0x000030BA
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004EC1 File Offset: 0x000030C1
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ConsolidatedHealth.schema;
			}
		}

		// Token: 0x04000071 RID: 113
		private static ConsolidatedHealthSchema schema = ObjectSchema.GetInstance<ConsolidatedHealthSchema>();

		// Token: 0x04000072 RID: 114
		private MultiValuedProperty<MonitorHealthEntry> healthEntries;

		// Token: 0x02000010 RID: 16
		[Serializable]
		public class ConsolidatedHealthId : ObjectId
		{
			// Token: 0x0600010B RID: 267 RVA: 0x00004ED4 File Offset: 0x000030D4
			public ConsolidatedHealthId(string healthSetName, string server)
			{
				string arg = (server != null) ? server : string.Empty;
				this.identity = string.Format("{0}\\{1}", healthSetName, arg);
			}

			// Token: 0x0600010C RID: 268 RVA: 0x00004F05 File Offset: 0x00003105
			public override string ToString()
			{
				return this.identity;
			}

			// Token: 0x0600010D RID: 269 RVA: 0x00004F0D File Offset: 0x0000310D
			public override byte[] GetBytes()
			{
				return Encoding.Unicode.GetBytes(this.ToString());
			}

			// Token: 0x04000073 RID: 115
			private readonly string identity;
		}
	}
}
