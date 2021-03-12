using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Management.OutlookProtectionRules
{
	// Token: 0x02000AFC RID: 2812
	internal sealed class PriorityHelper
	{
		// Token: 0x060063F5 RID: 25589 RVA: 0x001A1B78 File Offset: 0x0019FD78
		public PriorityHelper(IConfigDataProvider session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			this.session = session;
			this.rules = new List<TransportRule>();
			IEnumerable<TransportRule> enumerable = session.FindPaged<TransportRule>(null, Utils.GetRuleCollectionId(this.session), false, null, 0);
			foreach (TransportRule item in enumerable)
			{
				this.rules.Add(item);
			}
			this.rules.Sort(new Comparison<TransportRule>(ADRuleStorageManager.CompareTransportRule));
			this.sequenceNumbers = new int[this.rules.Count];
			int num = 0;
			foreach (TransportRule transportRule in this.rules)
			{
				this.sequenceNumbers[num++] = transportRule.Priority;
			}
		}

		// Token: 0x060063F6 RID: 25590 RVA: 0x001A1C84 File Offset: 0x0019FE84
		public bool IsPriorityValidForInsertion(int priority)
		{
			return priority >= 0 && priority <= this.rules.Count;
		}

		// Token: 0x060063F7 RID: 25591 RVA: 0x001A1C9D File Offset: 0x0019FE9D
		public bool IsPriorityValidForUpdate(int priority)
		{
			return priority >= 0 && priority < this.rules.Count;
		}

		// Token: 0x060063F8 RID: 25592 RVA: 0x001A1CB3 File Offset: 0x0019FEB3
		public int GetSequenceNumberToInsertPriority(int priority)
		{
			if (priority < 0)
			{
				throw new ArgumentOutOfRangeException("priority");
			}
			return this.GetSequenceNumberForPriority(priority, int.MinValue);
		}

		// Token: 0x060063F9 RID: 25593 RVA: 0x001A1CD0 File Offset: 0x0019FED0
		public int GetSequenceNumberToUpdatePriority(TransportRule rule, int newPriority)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			if (newPriority < 0)
			{
				throw new ArgumentOutOfRangeException("newPriority");
			}
			return this.GetSequenceNumberForPriority(newPriority, rule.Priority);
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x001A1CFC File Offset: 0x0019FEFC
		public int GetPriorityFromSequenceNumber(int sequenceNumber)
		{
			if (sequenceNumber < 0)
			{
				throw new ArgumentOutOfRangeException("sequenceNumber");
			}
			int num = Array.BinarySearch<int>(this.sequenceNumbers, sequenceNumber);
			if (num >= 0)
			{
				return num;
			}
			return this.rules.Count;
		}

		// Token: 0x060063FB RID: 25595 RVA: 0x001A1D54 File Offset: 0x0019FF54
		private int GetSequenceNumberForPriority(int priority, int currentSequenceNumber)
		{
			if (priority < 0)
			{
				throw new ArgumentOutOfRangeException("priority");
			}
			List<TransportRule> list = (from r in this.rules
			where r.Priority != currentSequenceNumber
			select r).ToList<TransportRule>();
			if (priority > list.Count)
			{
				priority = list.Count;
			}
			ADRuleStorageManager.NormalizeInternalSequenceNumbersIfNecessary(list, this.session);
			return ADRuleStorageManager.AssignInternalSequenceNumber(list, priority);
		}

		// Token: 0x0400360E RID: 13838
		private readonly IConfigDataProvider session;

		// Token: 0x0400360F RID: 13839
		private readonly List<TransportRule> rules;

		// Token: 0x04003610 RID: 13840
		private readonly int[] sequenceNumbers;
	}
}
