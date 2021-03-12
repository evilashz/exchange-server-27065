using System;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x0200006F RID: 111
	public sealed class ExTimeZoneRule
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x000105FC File Offset: 0x0000E7FC
		public ExTimeZoneRule(string id, string displayName, TimeSpan bias, ExYearlyRecurringTime observanceEnd)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (displayName == null)
			{
				throw new ArgumentNullException("displayName");
			}
			if (id == string.Empty)
			{
				throw new ArgumentException("id");
			}
			if (displayName == string.Empty)
			{
				throw new ArgumentException("displayName");
			}
			if (Math.Abs(bias.Ticks) > TimeLibConsts.MaxBias.Ticks)
			{
				throw new ArgumentOutOfRangeException("bias");
			}
			this.Id = id;
			this.DisplayName = displayName;
			this.Bias = bias;
			this.ObservanceEnd = observanceEnd;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0001069B File Offset: 0x0000E89B
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x000106A3 File Offset: 0x0000E8A3
		public ExYearlyRecurringTime ObservanceEnd
		{
			get
			{
				return this.observanceEnd;
			}
			internal set
			{
				this.observanceEnd = value;
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000106AC File Offset: 0x0000E8AC
		public override string ToString()
		{
			return string.Format("Rule: Id={0}; DisplayName={1}", this.Id, this.DisplayName);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000106C4 File Offset: 0x0000E8C4
		internal void Validate()
		{
			if (this.observanceEnd != null)
			{
				this.observanceEnd.Validate();
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000106D9 File Offset: 0x0000E8D9
		internal DateTime ToUtc(DateTime dateTime)
		{
			if (dateTime == DateTime.MinValue)
			{
				return TimeLibConsts.MinSystemDateTimeValue;
			}
			if (dateTime == DateTime.MaxValue)
			{
				return TimeLibConsts.MaxSystemDateTimeValue;
			}
			return DateTime.SpecifyKind(dateTime.Subtract(this.Bias), DateTimeKind.Utc);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00010714 File Offset: 0x0000E914
		internal DateTime FromUtc(DateTime dateTime)
		{
			return DateTime.SpecifyKind(dateTime.Add(this.Bias), DateTimeKind.Unspecified);
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00010729 File Offset: 0x0000E929
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00010731 File Offset: 0x0000E931
		internal ExTimeZoneRuleGroup RuleGroup
		{
			get
			{
				return this.ruleGroup;
			}
			set
			{
				if (this.ruleGroup != null)
				{
					throw new InvalidOperationException("Cannot change rule set");
				}
				this.ruleGroup = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001074D File Offset: 0x0000E94D
		internal ExTimeZone TimeZone
		{
			get
			{
				return this.ruleGroup.TimeZone;
			}
		}

		// Token: 0x040001E5 RID: 485
		public readonly string Id;

		// Token: 0x040001E6 RID: 486
		public readonly string DisplayName;

		// Token: 0x040001E7 RID: 487
		public readonly TimeSpan Bias;

		// Token: 0x040001E8 RID: 488
		private ExYearlyRecurringTime observanceEnd;

		// Token: 0x040001E9 RID: 489
		private ExTimeZoneRuleGroup ruleGroup;
	}
}
