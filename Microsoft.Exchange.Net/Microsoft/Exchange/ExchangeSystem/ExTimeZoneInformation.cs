using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x0200006C RID: 108
	public sealed class ExTimeZoneInformation
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x0000F79C File Offset: 0x0000D99C
		public ExTimeZoneInformation(string timeZoneId, string displayName) : this(timeZoneId, displayName, new LocalizedString(displayName))
		{
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000F7AC File Offset: 0x0000D9AC
		public ExTimeZoneInformation(string timeZoneId, string displayName, LocalizedString localizedDisplayName) : this(timeZoneId, displayName, localizedDisplayName, string.Empty)
		{
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000F7BC File Offset: 0x0000D9BC
		internal ExTimeZoneInformation(string timeZoneId, string displayName, LocalizedString localizedDisplayName, string muiStandardName)
		{
			if (timeZoneId == null)
			{
				throw new InvalidTimeZoneException("timeZoneId = null");
			}
			if (displayName == null)
			{
				throw new InvalidTimeZoneException("displayName = null");
			}
			if (timeZoneId.Length == 0)
			{
				throw new InvalidTimeZoneException("timeZoneId.Length = 0");
			}
			this.Id = timeZoneId;
			this.DisplayName = displayName;
			this.LocalizedDisplayName = (localizedDisplayName.IsEmpty ? new LocalizedString(this.DisplayName) : localizedDisplayName);
			this.MuiStandardName = muiStandardName;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000F83D File Offset: 0x0000DA3D
		public void AddGroup(ExTimeZoneRuleGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.groups.Add(group);
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000F859 File Offset: 0x0000DA59
		public IList<ExTimeZoneRuleGroup> Groups
		{
			get
			{
				return this.ReadOnlyGroups;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000F861 File Offset: 0x0000DA61
		private ReadOnlyCollection<ExTimeZoneRuleGroup> ReadOnlyGroups
		{
			get
			{
				if (this.readOnlyGroups == null)
				{
					this.readOnlyGroups = new ReadOnlyCollection<ExTimeZoneRuleGroup>(this.groups);
				}
				return this.readOnlyGroups;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000F882 File Offset: 0x0000DA82
		public IList<TimeSpan> Biases
		{
			get
			{
				return this.biases;
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000F88C File Offset: 0x0000DA8C
		internal void Validate()
		{
			if (this.groups.Count == 0)
			{
				throw new InvalidTimeZoneException("No rules");
			}
			foreach (ExTimeZoneRuleGroup exTimeZoneRuleGroup in this.groups)
			{
				exTimeZoneRuleGroup.Validate();
				exTimeZoneRuleGroup.TimeZoneInfo = this;
			}
			this.groups.Sort(GroupComparer.Instance);
			if (this.groups[this.groups.Count - 1].EndTransition != null)
			{
				throw new InvalidTimeZoneException("Last group should have no transition");
			}
			for (int i = 0; i < this.groups.Count - 1; i++)
			{
				ExTimeZoneRuleGroup exTimeZoneRuleGroup2 = this.groups[i];
				if (exTimeZoneRuleGroup2.EndTransition == null)
				{
					throw new InvalidTimeZoneException("Only last group is allowed to have no transition");
				}
				DateTime value = exTimeZoneRuleGroup2.EndTransition.Value;
				ExTimeZoneRuleGroup exTimeZoneRuleGroup3 = this.groups[i + 1];
				exTimeZoneRuleGroup3.EffectiveUtcStart = exTimeZoneRuleGroup2.EffectiveUtcEnd;
			}
			this.biases = new List<TimeSpan>();
			foreach (ExTimeZoneRuleGroup exTimeZoneRuleGroup4 in this.groups)
			{
				foreach (ExTimeZoneRule exTimeZoneRule in exTimeZoneRuleGroup4.Rules)
				{
					int num = this.biases.IndexOf(exTimeZoneRule.Bias);
					if (num < 0)
					{
						this.biases.Add(exTimeZoneRule.Bias);
					}
				}
			}
			this.biases.Sort();
			this.biases.Reverse();
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000FA74 File Offset: 0x0000DC74
		internal TimeSpan StandardBias
		{
			get
			{
				return this.biases[this.biases.Count - 1];
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000FA8E File Offset: 0x0000DC8E
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000FA96 File Offset: 0x0000DC96
		internal ExTimeZone TimeZone
		{
			get
			{
				return this.timeZone;
			}
			set
			{
				if (this.timeZone != null)
				{
					throw new InvalidOperationException("Cannot change time zone in rule set");
				}
				this.timeZone = value;
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000FAB4 File Offset: 0x0000DCB4
		internal ExTimeZoneRule GetRuleForUtcTime(DateTime utcDateTime)
		{
			ExTimeZoneRule exTimeZoneRule = null;
			foreach (ExTimeZoneRuleGroup exTimeZoneRuleGroup in this.ReadOnlyGroups)
			{
				if (exTimeZoneRuleGroup.EffectiveUtcStart <= utcDateTime && utcDateTime < exTimeZoneRuleGroup.EffectiveUtcEnd)
				{
					exTimeZoneRule = exTimeZoneRuleGroup.GetRuleForUtcTime(utcDateTime);
					break;
				}
			}
			if (exTimeZoneRule == null)
			{
				throw new InvalidOperationException(string.Format("Time zone {0} failed to find a rule for an UTC value", this.Id));
			}
			return exTimeZoneRule;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000FD30 File Offset: 0x0000DF30
		internal IEnumerable<ExTimeZoneRule> GetRulesForLocalTime(DateTime dateTime)
		{
			foreach (TimeSpan bias in this.biases)
			{
				DateTime utcCandidate = dateTime.Add(-bias);
				ExTimeZoneRule rule = this.GetRuleForUtcTime(utcCandidate);
				if (rule.FromUtc(utcCandidate) == dateTime)
				{
					yield return rule;
				}
			}
			yield break;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000FD54 File Offset: 0x0000DF54
		internal bool FindLeastBiasForLocalTime(DateTime dateTime, out TimeSpan bestBias)
		{
			bool flag = false;
			bestBias = TimeSpan.MinValue;
			foreach (TimeSpan t in this.biases)
			{
				DateTime dateTime2 = dateTime.Add(-t);
				ExTimeZoneRule ruleForUtcTime = this.GetRuleForUtcTime(dateTime2);
				if (ruleForUtcTime.FromUtc(dateTime2) == dateTime)
				{
					if (!flag)
					{
						flag = true;
						bestBias = ruleForUtcTime.Bias;
					}
					else if (ruleForUtcTime.Bias > bestBias)
					{
						bestBias = ruleForUtcTime.Bias;
					}
				}
			}
			return flag;
		}

		// Token: 0x040001D4 RID: 468
		public readonly string Id;

		// Token: 0x040001D5 RID: 469
		public readonly string DisplayName;

		// Token: 0x040001D6 RID: 470
		public readonly LocalizedString LocalizedDisplayName;

		// Token: 0x040001D7 RID: 471
		internal string AlternativeId;

		// Token: 0x040001D8 RID: 472
		internal readonly string MuiStandardName;

		// Token: 0x040001D9 RID: 473
		private readonly List<ExTimeZoneRuleGroup> groups = new List<ExTimeZoneRuleGroup>(2);

		// Token: 0x040001DA RID: 474
		private ReadOnlyCollection<ExTimeZoneRuleGroup> readOnlyGroups;

		// Token: 0x040001DB RID: 475
		private ExTimeZone timeZone;

		// Token: 0x040001DC RID: 476
		private List<TimeSpan> biases;
	}
}
