using System;
using System.Collections;
using System.Collections.Specialized;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis
{
	// Token: 0x02000035 RID: 53
	internal class FactorySenderData : SenderData
	{
		// Token: 0x06000135 RID: 309 RVA: 0x0000A35F File Offset: 0x0000855F
		public FactorySenderData(DateTime tsCreate) : base(tsCreate)
		{
			this.helloDomains = new HybridDictionary(10);
			this.reverseDns = string.Empty;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000A380 File Offset: 0x00008580
		public IDictionaryEnumerator HelloDomainEnumerator
		{
			get
			{
				return this.helloDomains.GetEnumerator();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000A38D File Offset: 0x0000858D
		public DateTime LastUpdateTime
		{
			get
			{
				return this.lastUpdate;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000A395 File Offset: 0x00008595
		public string ReverseDns
		{
			get
			{
				return this.reverseDns;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000A3A0 File Offset: 0x000085A0
		public void Merge(AgentSenderData agentData)
		{
			this.lastUpdate = DateTime.UtcNow;
			base.Merge(agentData);
			int num = 0;
			if (this.helloDomains.Contains(agentData.HelloDomain))
			{
				num = (int)this.helloDomains[agentData.HelloDomain];
			}
			num += agentData.NumMsgs;
			this.helloDomains[agentData.HelloDomain] = num;
			if (!string.IsNullOrEmpty(agentData.ReverseDns))
			{
				this.reverseDns = agentData.ReverseDns;
			}
		}

		// Token: 0x04000143 RID: 323
		private HybridDictionary helloDomains;

		// Token: 0x04000144 RID: 324
		private DateTime lastUpdate;

		// Token: 0x04000145 RID: 325
		private string reverseDns;
	}
}
