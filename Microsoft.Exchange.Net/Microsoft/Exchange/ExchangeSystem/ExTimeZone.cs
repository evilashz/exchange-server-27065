using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000066 RID: 102
	public sealed class ExTimeZone
	{
		// Token: 0x06000384 RID: 900 RVA: 0x0000EBC4 File Offset: 0x0000CDC4
		static ExTimeZone()
		{
			ExTimeZoneRuleGroup exTimeZoneRuleGroup = new ExTimeZoneRuleGroup(null);
			exTimeZoneRuleGroup.AddRule(ExTimeZone.UtcTimeZoneRule);
			ExTimeZoneInformation exTimeZoneInformation = new ExTimeZoneInformation("tzone://Microsoft/Utc", "UTC");
			exTimeZoneInformation.AddGroup(exTimeZoneRuleGroup);
			ExTimeZone.UtcTimeZone = new ExTimeZone(exTimeZoneInformation);
			ExTimeZone.CurrentTimeZone = ExTimeZone.GetCurrentTimeZone();
			ExTimeZone.UnspecifiedTimeZone = ExTimeZone.BuildUnspecifiedTimeZone();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000EC3D File Offset: 0x0000CE3D
		public ExTimeZone(ExTimeZoneInformation timeZoneInfo)
		{
			timeZoneInfo.Validate();
			timeZoneInfo.TimeZone = this;
			this.TimeZoneInformation = timeZoneInfo;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000EC59 File Offset: 0x0000CE59
		public static TimeSpan MaxBias
		{
			get
			{
				return TimeLibConsts.MaxBias;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000EC60 File Offset: 0x0000CE60
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000EC67 File Offset: 0x0000CE67
		public static ExTimeZone UtcTimeZone { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000EC6F File Offset: 0x0000CE6F
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000EC76 File Offset: 0x0000CE76
		public static ExTimeZoneRule UtcTimeZoneRule { get; private set; } = new ExTimeZoneRule("tzrule://Microsoft/UtcRule", "UTC rule", TimeSpan.FromTicks(0L), null);

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000EC7E File Offset: 0x0000CE7E
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000EC85 File Offset: 0x0000CE85
		public static ExTimeZone CurrentTimeZone { get; private set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000EC8D File Offset: 0x0000CE8D
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000EC94 File Offset: 0x0000CE94
		public static ExTimeZone UnspecifiedTimeZone { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		public bool IsCustomTimeZone
		{
			get
			{
				return this.Id == "tzone://Microsoft/Custom";
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000ECAE File Offset: 0x0000CEAE
		public string Id
		{
			get
			{
				return this.TimeZoneInformation.Id;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000ECBB File Offset: 0x0000CEBB
		public LocalizedString LocalizableDisplayName
		{
			get
			{
				return this.TimeZoneInformation.LocalizedDisplayName;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		public string DisplayName
		{
			get
			{
				return this.TimeZoneInformation.DisplayName;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000ECD5 File Offset: 0x0000CED5
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000ECDD File Offset: 0x0000CEDD
		public ExTimeZoneInformation TimeZoneInformation { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000ECE6 File Offset: 0x0000CEE6
		internal string AlternativeId
		{
			get
			{
				if (!this.IsCustomTimeZone)
				{
					return this.Id;
				}
				return this.TimeZoneInformation.AlternativeId;
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000ED04 File Offset: 0x0000CF04
		public static ExTimeZone TimeZoneFromKind(DateTimeKind kind)
		{
			switch (kind)
			{
			case DateTimeKind.Utc:
				return ExTimeZone.UtcTimeZone;
			case DateTimeKind.Local:
				return ExTimeZone.CurrentTimeZone;
			}
			return ExTimeZone.UnspecifiedTimeZone;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000ED38 File Offset: 0x0000CF38
		public DaylightTime GetDaylightChanges(int year)
		{
			ExDateTime exDateTime = new ExDateTime(this, year, 1, 1);
			ExTimeZoneRule ruleForUtcTime = this.TimeZoneInformation.GetRuleForUtcTime(exDateTime.UniversalTime);
			if (ruleForUtcTime == null)
			{
				throw new InvalidTimeZoneException("no rule covers year: " + year);
			}
			if (ruleForUtcTime.RuleGroup.Rules.Count <= 1)
			{
				return new DaylightTime(DateTime.MinValue, DateTime.MinValue, TimeSpan.Zero);
			}
			ExTimeZoneRule exTimeZoneRule = null;
			ExTimeZoneRule exTimeZoneRule2 = null;
			foreach (ExTimeZoneRule exTimeZoneRule3 in ruleForUtcTime.RuleGroup.Rules)
			{
				if (exTimeZoneRule3.Bias == this.TimeZoneInformation.StandardBias)
				{
					exTimeZoneRule = exTimeZoneRule3;
				}
				else
				{
					exTimeZoneRule2 = exTimeZoneRule3;
				}
			}
			if (exTimeZoneRule == null || exTimeZoneRule2 == null)
			{
				ExTraceGlobals.CommonTracer.TraceError((long)this.GetHashCode(), "No different Bias for standard time and daylight saving time. Treat it as No DST.");
				return new DaylightTime(DateTime.MinValue, DateTime.MinValue, TimeSpan.Zero);
			}
			DateTime instance = exTimeZoneRule.ObservanceEnd.GetInstance(year);
			DateTime instance2 = exTimeZoneRule2.ObservanceEnd.GetInstance(year);
			TimeSpan timeSpan = exTimeZoneRule2.Bias - exTimeZoneRule.Bias;
			if (timeSpan < TimeSpan.Zero)
			{
				ExTraceGlobals.CommonTracer.TraceError((long)this.GetHashCode(), "Rare time zone rule found, the DST bias is less than standard bias.");
			}
			return new DaylightTime(instance, instance2, timeSpan);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000EE9C File Offset: 0x0000D09C
		public bool IsDaylightSavingTime(ExDateTime dateTime)
		{
			return dateTime.Bias != this.TimeZoneInformation.StandardBias;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000EEB8 File Offset: 0x0000D0B8
		public ExDateTime ConvertDateTime(ExDateTime exDateTime)
		{
			if (!exDateTime.HasTimeZone)
			{
				ExTimeZoneHelperForMigrationOnly.CheckValidationLevel(false, ExTimeZoneHelperForMigrationOnly.ValidationLevel.VeryHigh, "ConvertDateTime: UnspecifiedTimeZone: UnspecifiedTimeZone", new object[0]);
				return new ExDateTime(this, exDateTime.LocalTime);
			}
			return new ExDateTime(this, exDateTime.UniversalTime, null);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000EF04 File Offset: 0x0000D104
		public ExDateTime Assign(ExDateTime exDateTime)
		{
			ExTimeZoneHelperForMigrationOnly.CheckValidationLevel<string>(!exDateTime.HasTimeZone, ExTimeZoneHelperForMigrationOnly.ValidationLevel.VeryHigh, "ExTimeZone.Assign:. ExDateTime alreayd has time zone: {0}.", exDateTime.TimeZone.Id);
			if (exDateTime.LocalTime <= TimeLibConsts.MinSystemDateTimeValue)
			{
				return this.ConvertDateTime(ExDateTime.MinValue);
			}
			if (exDateTime.LocalTime >= TimeLibConsts.MaxSystemDateTimeValue)
			{
				return this.ConvertDateTime(ExDateTime.MaxValue);
			}
			return new ExDateTime(this, exDateTime.LocalTime);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000EF7D File Offset: 0x0000D17D
		public override string ToString()
		{
			return string.Format("Time zone: Id={0}; DisplayName={1}", this.Id, this.DisplayName);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000EF95 File Offset: 0x0000D195
		internal TimeSpan GetBiasForUtcTime(DateTime utcDateTime)
		{
			return this.TimeZoneInformation.GetRuleForUtcTime(utcDateTime).Bias;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000F150 File Offset: 0x0000D350
		internal IEnumerable<TimeSpan> GetBiasesForLocalTime(DateTime dateTime)
		{
			foreach (ExTimeZoneRule rule in this.TimeZoneInformation.GetRulesForLocalTime(dateTime))
			{
				yield return rule.Bias;
			}
			yield break;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000F174 File Offset: 0x0000D374
		private static ExTimeZone GetCurrentTimeZone()
		{
			ExTimeZone exTimeZone = null;
			string currentTimeZoneName = ExRegistryReader.GetCurrentTimeZoneName();
			if (!string.IsNullOrEmpty(currentTimeZoneName))
			{
				ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(currentTimeZoneName, out exTimeZone);
			}
			if (exTimeZone == null)
			{
				string currentTimeZoneMuiStandardName = ExRegistryReader.GetCurrentTimeZoneMuiStandardName();
				if (!string.IsNullOrEmpty(currentTimeZoneMuiStandardName))
				{
					ExTraceGlobals.CommonTracer.TraceInformation<string, string>(0, 0L, "Current time zone name '{0}' from registry is invalid, fallback to use MUI standard name '{1}' to look up system time zone list.", currentTimeZoneName, currentTimeZoneMuiStandardName);
					foreach (ExTimeZone exTimeZone2 in ExTimeZoneEnumerator.Instance)
					{
						if (currentTimeZoneMuiStandardName.Equals(exTimeZone2.TimeZoneInformation.MuiStandardName, StringComparison.OrdinalIgnoreCase))
						{
							exTimeZone = exTimeZone2;
							break;
						}
					}
				}
			}
			if (exTimeZone == null)
			{
				ExTraceGlobals.CommonTracer.TraceInformation(0, 0L, "Unable to get current time zone according to registry, UTC time zone is being used instead.");
				exTimeZone = ExTimeZone.UtcTimeZone;
			}
			return exTimeZone;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000F238 File Offset: 0x0000D438
		private static ExTimeZone BuildUnspecifiedTimeZone()
		{
			string id = "tzrule://Microsoft/UnspecifiedRule";
			string displayName = "Unspecified time zone rule";
			string timeZoneId = "tzone://Microsoft/Unspecified";
			string displayName2 = "UnspecifiedTimeZone time zone";
			ExTimeZoneRule ruleInfo = new ExTimeZoneRule(id, displayName, TimeSpan.FromTicks(0L), null);
			ExTimeZoneRuleGroup exTimeZoneRuleGroup = new ExTimeZoneRuleGroup(null);
			exTimeZoneRuleGroup.AddRule(ruleInfo);
			ExTimeZoneInformation exTimeZoneInformation = new ExTimeZoneInformation(timeZoneId, displayName2);
			exTimeZoneInformation.AddGroup(exTimeZoneRuleGroup);
			return new ExTimeZone(exTimeZoneInformation);
		}

		// Token: 0x040001B8 RID: 440
		public const string CustomTimeZoneId = "tzone://Microsoft/Custom";

		// Token: 0x040001B9 RID: 441
		public const string CustomTimeZoneName = "Customized Time Zone";

		// Token: 0x040001BA RID: 442
		public const string UtcRuleId = "tzrule://Microsoft/UtcRule";

		// Token: 0x040001BB RID: 443
		public const string UtcRuleName = "UTC rule";

		// Token: 0x040001BC RID: 444
		public const string UtcZoneId = "tzone://Microsoft/Utc";

		// Token: 0x040001BD RID: 445
		public const string UtcZoneName = "UTC";
	}
}
