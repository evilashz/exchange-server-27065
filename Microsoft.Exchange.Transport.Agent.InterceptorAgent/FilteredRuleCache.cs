using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x0200000A RID: 10
	internal class FilteredRuleCache
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002EA9 File Offset: 0x000010A9
		public FilteredRuleCache(InterceptorAgentEvent eventTypes)
		{
			this.eventTypes = eventTypes;
			InterceptorAgentRulesCache.Instance.RegisterCache(this);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002ECE File Offset: 0x000010CE
		public IEnumerable<InterceptorAgentRule> Rules
		{
			get
			{
				return this.rulesCache;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002ED6 File Offset: 0x000010D6
		public InterceptorAgentEvent EventTypes
		{
			get
			{
				return this.eventTypes;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002F28 File Offset: 0x00001128
		public void UpdateCache(IList<InterceptorAgentRule> allRules)
		{
			ArgumentValidator.ThrowIfNull("allRules", allRules);
			List<InterceptorAgentRule> newCache = new List<InterceptorAgentRule>(this.rulesCache);
			List<InterceptorAgentRule> list = new List<InterceptorAgentRule>(from r in allRules
			where (ushort)(r.Events & this.eventTypes) != 0
			select r);
			List<InterceptorAgentRule> list2 = newCache.Except(list).ToList<InterceptorAgentRule>();
			List<InterceptorAgentRule> list3 = list.Except(newCache).ToList<InterceptorAgentRule>();
			list2.ForEach(delegate(InterceptorAgentRule rule)
			{
				newCache.Remove(rule);
				rule.DeactivateRule();
			});
			list3.ForEach(delegate(InterceptorAgentRule rule)
			{
				newCache.Add(rule);
				rule.ActivateRule();
			});
			this.rulesCache = newCache;
		}

		// Token: 0x0400001F RID: 31
		private readonly InterceptorAgentEvent eventTypes;

		// Token: 0x04000020 RID: 32
		private List<InterceptorAgentRule> rulesCache = new List<InterceptorAgentRule>();
	}
}
