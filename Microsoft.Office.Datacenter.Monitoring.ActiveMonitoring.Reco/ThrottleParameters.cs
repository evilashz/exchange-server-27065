using System;
using System.Collections.Generic;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000039 RID: 57
	public class ThrottleParameters
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00006834 File Offset: 0x00004A34
		public ThrottleParameters(bool isEnabled, int localMinimumMinutesBetweenAttempts, int localMaximumAllowedAttemptsInOneHour, int localMaximumAllowedAttemptsInADay, int groupMinimumMinutesBetweenAttempts, int groupMaximumAllowedAttemptsInADay)
		{
			this.IsEnabled = isEnabled;
			this.LocalMinimumMinutesBetweenAttempts = localMinimumMinutesBetweenAttempts;
			this.LocalMaximumAllowedAttemptsInOneHour = localMaximumAllowedAttemptsInOneHour;
			this.LocalMaximumAllowedAttemptsInADay = localMaximumAllowedAttemptsInADay;
			this.GroupMinimumMinutesBetweenAttempts = groupMinimumMinutesBetweenAttempts;
			this.GroupMaximumAllowedAttemptsInADay = groupMaximumAllowedAttemptsInADay;
			this.PropertyBag = this.ToDictionary();
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00006880 File Offset: 0x00004A80
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00006888 File Offset: 0x00004A88
		public bool IsEnabled { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00006891 File Offset: 0x00004A91
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00006899 File Offset: 0x00004A99
		public int LocalMinimumMinutesBetweenAttempts { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000068A2 File Offset: 0x00004AA2
		// (set) Token: 0x060001DD RID: 477 RVA: 0x000068AA File Offset: 0x00004AAA
		public int LocalMaximumAllowedAttemptsInOneHour { get; private set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000068B3 File Offset: 0x00004AB3
		// (set) Token: 0x060001DF RID: 479 RVA: 0x000068BB File Offset: 0x00004ABB
		public int LocalMaximumAllowedAttemptsInADay { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000068C4 File Offset: 0x00004AC4
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x000068CC File Offset: 0x00004ACC
		public int GroupMinimumMinutesBetweenAttempts { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000068D5 File Offset: 0x00004AD5
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000068DD File Offset: 0x00004ADD
		public int GroupMaximumAllowedAttemptsInADay { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000068E6 File Offset: 0x00004AE6
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000068EE File Offset: 0x00004AEE
		internal Dictionary<string, string> PropertyBag { get; private set; }

		// Token: 0x060001E6 RID: 486 RVA: 0x000068F8 File Offset: 0x00004AF8
		internal static bool GetBoolProperty(Dictionary<string, string> propertyBag, string propertyName, bool defaultValue)
		{
			bool result = defaultValue;
			bool flag = false;
			string text;
			if (propertyBag.TryGetValue(propertyName, out text) && !string.IsNullOrWhiteSpace(text))
			{
				flag = bool.TryParse(text, out result);
				if (!flag)
				{
					int num;
					flag = int.TryParse(text, out num);
					if (flag)
					{
						if (num == 0)
						{
							result = false;
						}
						else if (num == 1)
						{
							result = true;
						}
						else
						{
							flag = false;
						}
					}
				}
			}
			if (!flag)
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000694C File Offset: 0x00004B4C
		internal static int GetIntProperty(Dictionary<string, string> propertyBag, string propertyName, int defaultValue)
		{
			int result = defaultValue;
			string text;
			if (propertyBag.TryGetValue(propertyName, out text) && !string.IsNullOrWhiteSpace(text) && !int.TryParse(text, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000697C File Offset: 0x00004B7C
		internal Dictionary<string, string> ToDictionary()
		{
			return new Dictionary<string, string>
			{
				{
					"Enabled",
					this.IsEnabled.ToString()
				},
				{
					"LocalMinimumMinutesBetweenAttempts",
					this.LocalMinimumMinutesBetweenAttempts.ToString()
				},
				{
					"LocalMaximumAllowedAttemptsInOneHour",
					this.LocalMaximumAllowedAttemptsInOneHour.ToString()
				},
				{
					"LocalMaximumAllowedAttemptsInADay",
					this.LocalMaximumAllowedAttemptsInADay.ToString()
				},
				{
					"GroupMinimumMinutesBetweenAttempts",
					this.GroupMinimumMinutesBetweenAttempts.ToString()
				},
				{
					"GroupMaximumAllowedAttemptsInADay",
					this.GroupMaximumAllowedAttemptsInADay.ToString()
				}
			};
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006A2C File Offset: 0x00004C2C
		internal void ProcessProperty(Dictionary<string, string> propertyBag, string propertyName, string propertyNameShort, Action<string> action)
		{
			string text = null;
			if (propertyBag.ContainsKey(propertyName))
			{
				text = propertyName;
			}
			if (!string.IsNullOrWhiteSpace(propertyNameShort) && propertyBag.ContainsKey(propertyNameShort))
			{
				text = propertyNameShort;
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				action(text);
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006B4C File Offset: 0x00004D4C
		internal void ApplyPropertyOverrides(Dictionary<string, string> propertyBag)
		{
			this.ProcessProperty(propertyBag, "Enabled", null, delegate(string propertyName)
			{
				this.IsEnabled = ThrottleParameters.GetBoolProperty(propertyBag, propertyName, this.IsEnabled);
			});
			this.ProcessProperty(propertyBag, "LocalMinimumMinutesBetweenAttempts", "LocalMinutesMinimum", delegate(string propertyName)
			{
				this.LocalMinimumMinutesBetweenAttempts = ThrottleParameters.GetIntProperty(propertyBag, propertyName, this.LocalMinimumMinutesBetweenAttempts);
			});
			this.ProcessProperty(propertyBag, "LocalMaximumAllowedAttemptsInOneHour", "LocalMaxInHour", delegate(string propertyName)
			{
				this.LocalMaximumAllowedAttemptsInOneHour = ThrottleParameters.GetIntProperty(propertyBag, propertyName, this.LocalMaximumAllowedAttemptsInOneHour);
			});
			this.ProcessProperty(propertyBag, "LocalMaximumAllowedAttemptsInADay", "LocalMaxInDay", delegate(string propertyName)
			{
				this.LocalMaximumAllowedAttemptsInADay = ThrottleParameters.GetIntProperty(propertyBag, propertyName, this.LocalMaximumAllowedAttemptsInADay);
			});
			this.ProcessProperty(propertyBag, "GroupMinimumMinutesBetweenAttempts", "GroupMinutesMinimum", delegate(string propertyName)
			{
				this.GroupMinimumMinutesBetweenAttempts = ThrottleParameters.GetIntProperty(propertyBag, propertyName, this.GroupMinimumMinutesBetweenAttempts);
			});
			this.ProcessProperty(propertyBag, "GroupMaximumAllowedAttemptsInADay", "GroupMaxInDay", delegate(string propertyName)
			{
				this.GroupMaximumAllowedAttemptsInADay = ThrottleParameters.GetIntProperty(propertyBag, propertyName, this.GroupMaximumAllowedAttemptsInADay);
			});
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006C35 File Offset: 0x00004E35
		internal ThrottleParameters Clone()
		{
			return (ThrottleParameters)base.MemberwiseClone();
		}

		// Token: 0x04000106 RID: 262
		public const string EnabledProperty = "Enabled";

		// Token: 0x04000107 RID: 263
		public const string LocalMinMinutesGapProperty = "LocalMinimumMinutesBetweenAttempts";

		// Token: 0x04000108 RID: 264
		public const string LocalMaxAllowedInHourProperty = "LocalMaximumAllowedAttemptsInOneHour";

		// Token: 0x04000109 RID: 265
		public const string LocalMaxAllowedInDayProperty = "LocalMaximumAllowedAttemptsInADay";

		// Token: 0x0400010A RID: 266
		public const string GroupMinMinutesGapProperty = "GroupMinimumMinutesBetweenAttempts";

		// Token: 0x0400010B RID: 267
		public const string GroupMaxAllowedInDayProperty = "GroupMaximumAllowedAttemptsInADay";

		// Token: 0x0400010C RID: 268
		public const string LocalMinMinutesGapPropertyShort = "LocalMinutesMinimum";

		// Token: 0x0400010D RID: 269
		public const string LocalMaxAllowedInHourPropertyShort = "LocalMaxInHour";

		// Token: 0x0400010E RID: 270
		public const string LocalMaxAllowedInDayPropertyShort = "LocalMaxInDay";

		// Token: 0x0400010F RID: 271
		public const string GroupMinMinutesGapPropertyShort = "GroupMinutesMinimum";

		// Token: 0x04000110 RID: 272
		public const string GroupMaxAllowedInDayPropertyShort = "GroupMaxInDay";

		// Token: 0x04000111 RID: 273
		public const int CheckNotApplicable = -1;

		// Token: 0x04000112 RID: 274
		internal static readonly ThrottleParameters Default = new ThrottleParameters(true, 60, 1, 1, 60, 1);
	}
}
