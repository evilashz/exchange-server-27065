using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000022 RID: 34
	internal class PolicyBasedThrottler : ISchedulerThrottler
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000394C File Offset: 0x00001B4C
		public PolicyBasedThrottler(IEnumerable<IThrottlingPolicy> policies, IEnumerable<MessageScopeType> relevantTypes, ISchedulerMetering metering)
		{
			ArgumentValidator.ThrowIfNull("policies", policies);
			ArgumentValidator.ThrowIfNull("relevantTypes", relevantTypes);
			ArgumentValidator.ThrowIfNull("metering", metering);
			MessageScopeType[] array = (MessageScopeType[])Enum.GetValues(typeof(MessageScopeType));
			this.orderdScopeTypes = new int[array.Length];
			foreach (MessageScopeType messageScopeType in array)
			{
				this.orderdScopeTypes[(int)messageScopeType] = -1;
			}
			int num = 0;
			foreach (MessageScopeType messageScopeType2 in relevantTypes)
			{
				this.orderdScopeTypes[(int)messageScopeType2] = num;
				num++;
			}
			this.policies = policies;
			this.metering = metering;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003A20 File Offset: 0x00001C20
		public bool ShouldThrottle(IEnumerable<IMessageScope> scopes, out IMessageScope throttledScope)
		{
			ArgumentValidator.ThrowIfNull("scopes", scopes);
			UsageData usageData = null;
			foreach (IMessageScope messageScope in scopes)
			{
				if (usageData == null)
				{
					usageData = this.metering.GetTotalUsage();
				}
				switch (this.Evaluate(messageScope, usageData))
				{
				case PolicyDecision.Allow:
					throttledScope = null;
					return false;
				case PolicyDecision.Deny:
					throttledScope = messageScope;
					return true;
				}
			}
			throttledScope = null;
			return false;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public IEnumerable<IMessageScope> GetThrottlingScopes(IEnumerable<IMessageScope> candidateScopes)
		{
			ArgumentValidator.ThrowIfNull("candidateScopes", candidateScopes);
			IDictionary<MessageScopeType, IMessageScope> dictionary = new Dictionary<MessageScopeType, IMessageScope>();
			foreach (IMessageScope messageScope in candidateScopes)
			{
				MessageScopeType type = messageScope.Type;
				if (this.orderdScopeTypes[(int)type] >= 0 && !dictionary.ContainsKey(type))
				{
					dictionary.Add(type, messageScope);
				}
			}
			List<IMessageScope> list = new List<IMessageScope>(dictionary.Values);
			list.Sort((IMessageScope scopeA, IMessageScope scopeB) => this.orderdScopeTypes[(int)scopeA.Type].CompareTo(this.orderdScopeTypes[(int)scopeB.Type]));
			return list;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003B7C File Offset: 0x00001D7C
		public bool ShouldThrottle(IMessageScope scope)
		{
			ArgumentValidator.ThrowIfNull("scope", scope);
			PolicyDecision policyDecision = this.Evaluate(scope, this.metering.GetTotalUsage());
			return PolicyDecision.Deny == policyDecision;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003BB0 File Offset: 0x00001DB0
		private PolicyDecision Evaluate(IMessageScope scope, UsageData totalUsage)
		{
			UsageData usage;
			if (this.metering.TryGetUsage(scope, out usage))
			{
				foreach (IThrottlingPolicy throttlingPolicy in this.policies)
				{
					PolicyDecision policyDecision = throttlingPolicy.Evaluate(scope, usage, totalUsage);
					if (policyDecision != PolicyDecision.None)
					{
						return policyDecision;
					}
				}
				return PolicyDecision.None;
			}
			return PolicyDecision.None;
		}

		// Token: 0x04000053 RID: 83
		private readonly IEnumerable<IThrottlingPolicy> policies;

		// Token: 0x04000054 RID: 84
		private readonly int[] orderdScopeTypes;

		// Token: 0x04000055 RID: 85
		private readonly ISchedulerMetering metering;
	}
}
