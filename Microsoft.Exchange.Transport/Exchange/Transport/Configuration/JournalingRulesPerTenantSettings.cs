using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002D7 RID: 727
	internal sealed class JournalingRulesPerTenantSettings : TenantConfigurationCacheableItem<TransportRule>
	{
		// Token: 0x06002040 RID: 8256 RVA: 0x0007B912 File Offset: 0x00079B12
		public JournalingRulesPerTenantSettings()
		{
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x0007B91A File Offset: 0x00079B1A
		public JournalingRulesPerTenantSettings(IEnumerable<TransportRule> transportRules) : base(true)
		{
			this.SetInternalData(transportRules);
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x0007B92A File Offset: 0x00079B2A
		public List<JournalRuleData> JournalRuleDataList
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.journalRuleDataList;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x0007B93C File Offset: 0x00079B3C
		public override long ItemSize
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				if (this.journalRuleDataList == null)
				{
					return 0L;
				}
				long num = 0L;
				foreach (JournalRuleData journalRuleData in this.journalRuleDataList)
				{
					num += journalRuleData.ItemSize;
				}
				num += 18L;
				return num;
			}
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x0007B9B0 File Offset: 0x00079BB0
		public override void ReadData(IConfigurationSession session)
		{
			QueryFilter filter = new TextFilter(ADObjectSchema.Name, "JournalingVersioned", MatchOptions.FullString, MatchFlags.Default);
			TransportRuleCollection[] array = (TransportRuleCollection[])session.Find<TransportRuleCollection>(filter, null, true, null);
			if (array.Length != 1)
			{
				ExTraceGlobals.JournalingTracer.TraceError<int>(0L, "JournalingRulesPerTenantSettings - query for JournalRuleCollection returned '{0}' results", array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					ExTraceGlobals.JournalingTracer.TraceError<int, string>(0L, "JournalingRulesPerTenantSettings Result #{0} DN - '{1}'", i, array[i].DistinguishedName.ToString());
				}
				this.SetInternalData(null);
				return;
			}
			IEnumerable<TransportRule> internalData = session.FindPaged<TransportRule>(null, array[0].Id, false, null, 0);
			this.SetInternalData(internalData);
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x0007BA48 File Offset: 0x00079C48
		private void SetInternalData(IEnumerable<TransportRule> transportRules)
		{
			if (transportRules == null)
			{
				this.journalRuleDataList = null;
				return;
			}
			List<JournalRuleData> list = new List<JournalRuleData>();
			foreach (TransportRule rule in transportRules)
			{
				list.Add(new JournalRuleData(rule));
			}
			this.journalRuleDataList = list;
		}

		// Token: 0x040010E1 RID: 4321
		public const string RuleCollectionName = "JournalingVersioned";

		// Token: 0x040010E2 RID: 4322
		private List<JournalRuleData> journalRuleDataList;
	}
}
