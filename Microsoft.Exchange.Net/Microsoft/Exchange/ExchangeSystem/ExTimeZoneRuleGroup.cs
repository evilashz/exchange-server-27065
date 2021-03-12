using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000071 RID: 113
	public sealed class ExTimeZoneRuleGroup
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x000107DC File Offset: 0x0000E9DC
		public ExTimeZoneRuleGroup(DateTime? endTransition)
		{
			this.endTransition = endTransition;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001080D File Offset: 0x0000EA0D
		public void AddRule(ExTimeZoneRule ruleInfo)
		{
			if (ruleInfo == null)
			{
				throw new ArgumentNullException("ruleInfo");
			}
			this.rules.Add(ruleInfo);
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00010829 File Offset: 0x0000EA29
		public IList<ExTimeZoneRule> Rules
		{
			get
			{
				if (this.readOnlyRules == null)
				{
					this.readOnlyRules = new ReadOnlyCollection<ExTimeZoneRule>(this.rules);
				}
				return this.readOnlyRules;
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0001084C File Offset: 0x0000EA4C
		public override string ToString()
		{
			return string.Format("RuleGroup; rule count={0}; transition = {1}", this.rules.Count, (this.EndTransition != null) ? this.EndTransition.Value.ToString() : "none");
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public DateTime? EndTransition
		{
			get
			{
				return this.endTransition;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000108AE File Offset: 0x0000EAAE
		internal ExTimeZone TimeZone
		{
			get
			{
				if (this.timeZoneInfo == null)
				{
					return null;
				}
				return this.timeZoneInfo.TimeZone;
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000108C8 File Offset: 0x0000EAC8
		internal void CalculateEffectiveUtcEnd()
		{
			if (this.EndTransition == null)
			{
				this.effectiveUtcEnd = DateTime.MaxValue;
				return;
			}
			DateTime value = this.EndTransition.Value;
			ExTimeZoneRule exTimeZoneRule = this.Rules[0];
			if (this.Rules.Count > 1)
			{
				int num = value.Year;
				if (value == new DateTime(num, 1, 1))
				{
					num--;
				}
				for (int i = this.Rules.Count - 1; i >= 0; i--)
				{
					DateTime instance = this.rules[i].ObservanceEnd.GetInstance(num);
					if (instance >= value)
					{
						exTimeZoneRule = this.rules[i];
						break;
					}
				}
			}
			this.effectiveUtcEnd = exTimeZoneRule.ToUtc(value);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00010990 File Offset: 0x0000EB90
		internal ExTimeZoneRule GetRuleForUtcTime(DateTime utcDateTime)
		{
			if (this.rules.Count == 1)
			{
				return this.rules[0];
			}
			int? num = null;
			for (int i = 0; i < this.rules.Count; i++)
			{
				ExTimeZoneRule exTimeZoneRule = this.rules[i];
				DateTime t = exTimeZoneRule.FromUtc(utcDateTime);
				if (num == null)
				{
					num = new int?(t.Year);
				}
				DateTime instance = exTimeZoneRule.ObservanceEnd.GetInstance(num.Value);
				if (t < instance)
				{
					return exTimeZoneRule;
				}
			}
			return this.rules[0];
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00010A34 File Offset: 0x0000EC34
		internal void Validate()
		{
			if (this.rules.Count == 0)
			{
				throw new InvalidTimeZoneException("Empty group");
			}
			this.rules.Sort(RuleComparer.Instance);
			int num = -1;
			foreach (ExTimeZoneRule exTimeZoneRule in this.rules)
			{
				if (exTimeZoneRule.ObservanceEnd != null)
				{
					int sortIndex = exTimeZoneRule.ObservanceEnd.SortIndex;
					if (sortIndex == num)
					{
						throw new InvalidTimeZoneException("Rules are too close");
					}
					num = sortIndex;
				}
				try
				{
					exTimeZoneRule.Validate();
				}
				catch (ArgumentOutOfRangeException innerException)
				{
					throw new InvalidTimeZoneException("Invalid rule", innerException);
				}
				exTimeZoneRule.RuleGroup = this;
			}
			this.CalculateEffectiveUtcEnd();
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00010B00 File Offset: 0x0000ED00
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x00010B08 File Offset: 0x0000ED08
		public ExTimeZoneInformation TimeZoneInfo
		{
			get
			{
				return this.timeZoneInfo;
			}
			internal set
			{
				this.timeZoneInfo = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00010B11 File Offset: 0x0000ED11
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00010B19 File Offset: 0x0000ED19
		internal DateTime EffectiveUtcStart
		{
			get
			{
				return this.effectiveUtcStart;
			}
			set
			{
				this.effectiveUtcStart = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00010B22 File Offset: 0x0000ED22
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00010B2A File Offset: 0x0000ED2A
		internal DateTime EffectiveUtcEnd
		{
			get
			{
				return this.effectiveUtcEnd;
			}
			set
			{
				this.effectiveUtcEnd = value;
			}
		}

		// Token: 0x040001EB RID: 491
		private DateTime effectiveUtcStart = DateTime.MinValue;

		// Token: 0x040001EC RID: 492
		private DateTime effectiveUtcEnd = DateTime.MaxValue;

		// Token: 0x040001ED RID: 493
		private ExTimeZoneInformation timeZoneInfo;

		// Token: 0x040001EE RID: 494
		private DateTime? endTransition;

		// Token: 0x040001EF RID: 495
		private List<ExTimeZoneRule> rules = new List<ExTimeZoneRule>(2);

		// Token: 0x040001F0 RID: 496
		private ReadOnlyCollection<ExTimeZoneRule> readOnlyRules;
	}
}
