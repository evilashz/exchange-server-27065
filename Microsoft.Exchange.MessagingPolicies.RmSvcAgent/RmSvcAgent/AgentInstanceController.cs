using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x0200001D RID: 29
	internal sealed class AgentInstanceController
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00007C00 File Offset: 0x00005E00
		private AgentInstanceController(int maxActiveAgents, bool multiTenancyEnabled)
		{
			this.totalSlots = maxActiveAgents;
			this.freeSlots = maxActiveAgents;
			this.multiTenancyEnabled = multiTenancyEnabled;
			if (multiTenancyEnabled)
			{
				this.agents = new Dictionary<Guid, int>(100);
			}
			else
			{
				this.agents = new Dictionary<Guid, int>(1);
			}
			RmSvcAgentPerfCounters.CurrentActiveAgents.RawValue = 0L;
			RmSvcAgentPerfCounters.TotalSuccessfulActiveRequests.RawValue = 0L;
			RmSvcAgentPerfCounters.TotalUnsuccessfulActiveRequests.RawValue = 0L;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00007C6C File Offset: 0x00005E6C
		public static AgentInstanceController Instance
		{
			get
			{
				AgentInstanceController result;
				lock (AgentInstanceController.syncRoot)
				{
					if (AgentInstanceController.instance == null)
					{
						throw new InvalidOperationException("AgentInstanceController.Instance is not initialized yet!");
					}
					result = AgentInstanceController.instance;
				}
				return result;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00007CC0 File Offset: 0x00005EC0
		public static void Initialize()
		{
			lock (AgentInstanceController.syncRoot)
			{
				if (AgentInstanceController.instance == null)
				{
					bool enabled = VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;
					AgentInstanceController.instance = new AgentInstanceController(Utils.GetMaxActiveAgents(), enabled);
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00007D28 File Offset: 0x00005F28
		public bool TryMakeActive(Guid tenantId)
		{
			bool flag = false;
			lock (this.agents)
			{
				if (this.freeSlots > 0)
				{
					int num = 0;
					bool flag3 = this.agents.TryGetValue(tenantId, out num);
					if ((this.multiTenancyEnabled && this.freeSlots > num) || (!this.multiTenancyEnabled && this.freeSlots > 0))
					{
						if (flag3)
						{
							this.agents[tenantId] = num + 1;
						}
						else
						{
							this.agents.Add(tenantId, 1);
						}
						this.freeSlots--;
						flag = true;
						RmSvcAgentPerfCounters.CurrentActiveAgents.RawValue = (long)(this.totalSlots - this.freeSlots);
					}
				}
			}
			if (flag)
			{
				RmSvcAgentPerfCounters.TotalSuccessfulActiveRequests.Increment();
			}
			else
			{
				RmSvcAgentPerfCounters.TotalUnsuccessfulActiveRequests.Increment();
			}
			return flag;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00007E08 File Offset: 0x00006008
		public void MakeInactive(Guid tenantId)
		{
			lock (this.agents)
			{
				int num = 0;
				bool flag2 = this.agents.TryGetValue(tenantId, out num);
				if (flag2)
				{
					this.freeSlots++;
					if (num > 1)
					{
						this.agents[tenantId] = num - 1;
					}
					else
					{
						this.agents.Remove(tenantId);
					}
					RmSvcAgentPerfCounters.CurrentActiveAgents.RawValue = (long)(this.totalSlots - this.freeSlots);
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00007EA0 File Offset: 0x000060A0
		public override string ToString()
		{
			string text = string.Format("Total slots: {0}, Free slots: {1}", this.totalSlots, this.freeSlots);
			if (this.multiTenancyEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder(text);
				lock (this.agents)
				{
					foreach (Guid guid in this.agents.Keys)
					{
						int num = this.agents[guid];
						if (num > 0)
						{
							stringBuilder.AppendFormat("{0}Tenant: {1} - Used Slots: {2}", Environment.NewLine, guid, num);
						}
					}
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00007F88 File Offset: 0x00006188
		internal static void ReInitializeForUnitTest(int maxActiveAgents, bool multiTenancyEnabled)
		{
			lock (AgentInstanceController.syncRoot)
			{
				AgentInstanceController.instance = new AgentInstanceController(maxActiveAgents, multiTenancyEnabled);
			}
		}

		// Token: 0x040000E9 RID: 233
		private static readonly object syncRoot = new object();

		// Token: 0x040000EA RID: 234
		private static AgentInstanceController instance;

		// Token: 0x040000EB RID: 235
		private readonly int totalSlots;

		// Token: 0x040000EC RID: 236
		private int freeSlots;

		// Token: 0x040000ED RID: 237
		private Dictionary<Guid, int> agents;

		// Token: 0x040000EE RID: 238
		private readonly bool multiTenancyEnabled;
	}
}
