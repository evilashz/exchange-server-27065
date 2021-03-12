using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management
{
	// Token: 0x02000007 RID: 7
	[OutputType(new Type[]
	{
		typeof(ConsolidatedHealth)
	})]
	[Cmdlet("Get", "HealthReport")]
	public sealed class GetHealthReport : GetHealthBase
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023E4 File Offset: 0x000005E4
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000023EC File Offset: 0x000005EC
		[Parameter(Mandatory = false)]
		public SwitchParameter RollupGroup { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023F5 File Offset: 0x000005F5
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002402 File Offset: 0x00000602
		[Parameter(Mandatory = false)]
		public int GroupSize
		{
			get
			{
				return this.monitorHealthCommon.GroupSize;
			}
			set
			{
				this.monitorHealthCommon.GroupSize = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002410 File Offset: 0x00000610
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000241D File Offset: 0x0000061D
		[Parameter(Mandatory = false)]
		public int MinimumOnlinePercent
		{
			get
			{
				return this.monitorHealthCommon.MinimumOnlinePercent;
			}
			set
			{
				this.monitorHealthCommon.MinimumOnlinePercent = value;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000242C File Offset: 0x0000062C
		protected override void BeginProcessing()
		{
			this.monitorHealthCommon = new MonitorHealthCommon(base.Identity, base.HealthSet, base.HaImpactingOnly);
			LocalizedException ex = null;
			List<MonitorHealthEntry> monitorHealthEntries = this.monitorHealthCommon.GetMonitorHealthEntries(out ex);
			if (ex != null)
			{
				base.WriteWarning(ex.LocalizedString);
			}
			this.monitorHealthCommon.SetServerHealthMap(monitorHealthEntries);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000248B File Offset: 0x0000068B
		protected override void EndProcessing()
		{
			if (this.monitorHealthCommon.ServerHealthMap.Count > 0)
			{
				if (this.RollupGroup)
				{
					this.ProcessRollupByGroup();
				}
				else
				{
					this.ProcessRollupByServerHealthSet();
				}
			}
			base.EndProcessing();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024C4 File Offset: 0x000006C4
		private void ProcessRollupByServerHealthSet()
		{
			List<ConsolidatedHealth> consolidateHealthEntries = this.monitorHealthCommon.GetConsolidateHealthEntries();
			if (consolidateHealthEntries != null)
			{
				foreach (ConsolidatedHealth sendToPipeline in consolidateHealthEntries)
				{
					base.WriteObject(sendToPipeline);
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002524 File Offset: 0x00000724
		private void ProcessRollupByGroup()
		{
			Dictionary<string, Dictionary<string, ConsolidatedHealth>> dictionary = new Dictionary<string, Dictionary<string, ConsolidatedHealth>>();
			IEnumerable<string> keys = this.monitorHealthCommon.ServerHealthMap.Keys;
			List<ConsolidatedHealth> consolidateHealthEntries = this.monitorHealthCommon.GetConsolidateHealthEntries();
			if (consolidateHealthEntries != null)
			{
				foreach (ConsolidatedHealth consolidatedHealth in consolidateHealthEntries)
				{
					Dictionary<string, ConsolidatedHealth> dictionary2 = null;
					if (!dictionary.TryGetValue(consolidatedHealth.HealthSet, out dictionary2))
					{
						dictionary2 = new Dictionary<string, ConsolidatedHealth>();
						foreach (string key in keys)
						{
							dictionary2[key] = null;
						}
						dictionary[consolidatedHealth.HealthSet] = dictionary2;
					}
					dictionary2[consolidatedHealth.Server] = consolidatedHealth;
				}
			}
			foreach (KeyValuePair<string, Dictionary<string, ConsolidatedHealth>> keyValuePair in dictionary)
			{
				Dictionary<string, ConsolidatedHealth> value = keyValuePair.Value;
				ConsolidatedHealth sendToPipeline = this.monitorHealthCommon.ConsolidateAcrossServers(value);
				base.WriteObject(sendToPipeline);
			}
		}

		// Token: 0x0400000A RID: 10
		private MonitorHealthCommon monitorHealthCommon;
	}
}
