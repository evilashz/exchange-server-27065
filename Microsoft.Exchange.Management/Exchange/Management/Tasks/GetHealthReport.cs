using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200058D RID: 1421
	[OutputType(new Type[]
	{
		typeof(ConsolidatedHealth)
	})]
	[Cmdlet("Get", "HealthReport")]
	public sealed class GetHealthReport : GetServerHealthBase
	{
		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x000CB645 File Offset: 0x000C9845
		// (set) Token: 0x06003212 RID: 12818 RVA: 0x000CB66B File Offset: 0x000C986B
		[Parameter(Mandatory = false)]
		public SwitchParameter RollupGroup
		{
			get
			{
				return (SwitchParameter)(base.Fields["RollupGroup"] ?? false);
			}
			set
			{
				base.Fields["RollupGroup"] = value;
			}
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06003213 RID: 12819 RVA: 0x000CB683 File Offset: 0x000C9883
		// (set) Token: 0x06003214 RID: 12820 RVA: 0x000CB690 File Offset: 0x000C9890
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

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x000CB69E File Offset: 0x000C989E
		// (set) Token: 0x06003216 RID: 12822 RVA: 0x000CB6AB File Offset: 0x000C98AB
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

		// Token: 0x06003217 RID: 12823 RVA: 0x000CB6BC File Offset: 0x000C98BC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				this.monitorHealthCommon = new MonitorHealthCommon((!string.IsNullOrWhiteSpace(base.Identity.Fqdn)) ? base.Identity.Fqdn : base.Identity.ToString(), base.HealthSet, base.HaImpactingOnly);
				LocalizedException ex = null;
				List<MonitorHealthEntry> monitorHealthEntries = this.monitorHealthCommon.GetMonitorHealthEntries(out ex);
				if (ex != null)
				{
					this.WriteWarning(ex.LocalizedString);
				}
				this.monitorHealthCommon.SetServerHealthMap(monitorHealthEntries);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x000CB758 File Offset: 0x000C9958
		protected override void InternalEndProcessing()
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
			base.InternalEndProcessing();
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x000CB790 File Offset: 0x000C9990
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

		// Token: 0x0600321A RID: 12826 RVA: 0x000CB7F0 File Offset: 0x000C99F0
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

		// Token: 0x04002346 RID: 9030
		private MonitorHealthCommon monitorHealthCommon;
	}
}
