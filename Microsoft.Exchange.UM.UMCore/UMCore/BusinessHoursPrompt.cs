using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000043 RID: 67
	internal class BusinessHoursPrompt : VariablePrompt<UMAutoAttendant>
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000C17E File Offset: 0x0000A37E
		protected override PromptConfigBase PreviewConfig
		{
			get
			{
				return GlobCfg.DefaultPromptsForPreview.AABusinessHours;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000C18C File Offset: 0x0000A38C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"businessHours",
				base.Config.PromptName,
				string.Empty,
				base.SbLog.ToString()
			});
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Business hours prompt returning ssmlstring: {0}.", new object[]
			{
				base.SbSsml.ToString()
			});
			return base.SbSsml.ToString();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000C21C File Offset: 0x0000A41C
		protected override void InternalInitialize()
		{
			if (base.InitVal == null)
			{
				return;
			}
			this.aa = base.InitVal;
			base.Culture = this.aa.Language.Culture;
			if (LocConfig.Instance[base.Culture].General.SmartReadingHours)
			{
				this.InitializeSmartReading();
			}
			else
			{
				this.InitializeStandardReading();
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "BusinessHoursPrompt successfully intialized with ssml '{0}'.", new object[]
			{
				base.SbSsml.ToString()
			});
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
		private static bool AreScheduleIntervalsEqual(TimeRange s1, TimeRange s2)
		{
			TimeSpan t = s1.EndTime - s1.StartTime;
			TimeSpan t2 = s2.EndTime - s2.StartTime;
			return t == t2 && s1.StartTime.Hour == s2.StartTime.Hour && s1.StartTime.Minute == s2.StartTime.Minute;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000C320 File Offset: 0x0000A520
		private static bool AreSchedulesEqual(List<TimeRange> siListOne, List<TimeRange> siListTwo)
		{
			if (siListOne.Count != siListTwo.Count)
			{
				return false;
			}
			for (int i = 0; i < siListOne.Count; i++)
			{
				if (!BusinessHoursPrompt.AreScheduleIntervalsEqual(siListOne[i], siListTwo[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000C368 File Offset: 0x0000A568
		private void AddAlwaysClosedPrompt()
		{
			base.AddPrompts(GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
			{
				GlobCfg.DefaultPromptsForAA.WeAreAlwaysClosed
			}));
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000C39C File Offset: 0x0000A59C
		private void AddAlwaysOpenPrompt()
		{
			base.AddPrompts(GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
			{
				GlobCfg.DefaultPromptsForAA.WeAreAlwaysOpen
			}));
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000C3D0 File Offset: 0x0000A5D0
		private ExTimeZone GetTimeZone()
		{
			ExTimeZone result;
			if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(this.aa.TimeZone, out result))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid or null timezone", new object[0]));
			}
			return result;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000C412 File Offset: 0x0000A612
		private ExDateTime GetNormalizedExchangeTime(int day, int hour, int minute)
		{
			return new ExDateTime(this.GetTimeZone(), 2006, 1, 1 + day, hour, minute, 0);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000C42B File Offset: 0x0000A62B
		private ExDateTime GetDate(DayOfWeek dow)
		{
			return this.GetNormalizedExchangeTime((int)dow, 0, 0);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000C438 File Offset: 0x0000A638
		private TimeRange GetTimeRange(ScheduleInterval si)
		{
			ExDateTime normalizedExchangeTime = this.GetNormalizedExchangeTime((int)si.StartDay, si.StartHour, si.StartMinute);
			ExDateTime exDateTime = this.GetNormalizedExchangeTime((int)si.EndDay, si.EndHour, si.EndMinute);
			if (si.StartDay > si.EndDay)
			{
				exDateTime += BusinessHoursPrompt.SevenDays;
			}
			return new TimeRange(normalizedExchangeTime, exDateTime);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000C4A0 File Offset: 0x0000A6A0
		private void InitializeSmartReading()
		{
			this.ClearStringBuilders(true);
			ScheduleInterval[] businessHoursSchedule = this.aa.BusinessHoursSchedule;
			if (businessHoursSchedule == null || businessHoursSchedule.Length == 0)
			{
				this.AddAlwaysClosedPrompt();
				return;
			}
			TimeSpan t = BusinessHoursPrompt.Never;
			List<TimeRange>[] array = new List<TimeRange>[]
			{
				new List<TimeRange>(),
				new List<TimeRange>(),
				new List<TimeRange>(),
				new List<TimeRange>(),
				new List<TimeRange>(),
				new List<TimeRange>(),
				new List<TimeRange>()
			};
			for (int i = 0; i < businessHoursSchedule.Length; i++)
			{
				if (businessHoursSchedule[i].Length >= BusinessHoursPrompt.TwentyFourHours)
				{
					this.InitializeStandardReading();
					return;
				}
				array[(int)businessHoursSchedule[i].StartDay].Add(this.GetTimeRange(businessHoursSchedule[i]));
				t += businessHoursSchedule[i].Length;
			}
			if (t == BusinessHoursPrompt.SevenDays)
			{
				this.AddAlwaysOpenPrompt();
				return;
			}
			List<ScheduleGroup> list = new List<ScheduleGroup>();
			bool[] array2 = new bool[7];
			for (int j = 0; j < 7; j++)
			{
				List<ExDateTime> list2 = new List<ExDateTime>();
				int num = (int)((j + this.aa.WeekStartDay) % (DayOfWeek)7);
				if (!array2[num] && array[num].Count != 0)
				{
					list2.Add(this.GetDate((DayOfWeek)num));
					for (int k = 1; k < 7; k++)
					{
						int num2 = (num + k) % 7;
						if (!array2[num2] && array[num2].Count != 0 && BusinessHoursPrompt.AreSchedulesEqual(array[num], array[num2]))
						{
							list2.Add(this.GetDate((DayOfWeek)num2));
							array2[num2] = true;
						}
					}
					array2[num] = true;
					list.Add(new ScheduleGroup(list2, array[num]));
				}
			}
			if (list.Count == 1 && list[0].DaysOfWeek.Count == 7)
			{
				this.AddWeAreOpenEverydayPrompt(list);
				return;
			}
			this.AddOurOpeningHoursPrompt(list);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000C694 File Offset: 0x0000A894
		private void InitializeStandardReading()
		{
			this.ClearStringBuilders(false);
			ScheduleInterval[] businessHoursSchedule = this.aa.BusinessHoursSchedule;
			if (businessHoursSchedule == null || businessHoursSchedule.Length == 0)
			{
				this.AddAlwaysClosedPrompt();
				return;
			}
			TimeSpan t = BusinessHoursPrompt.Never;
			List<TimeRange> list = new List<TimeRange>();
			List<ScheduleInterval> list2 = new List<ScheduleInterval>();
			for (int i = 0; i < businessHoursSchedule.Length; i++)
			{
				if (businessHoursSchedule[i].StartDay < this.aa.WeekStartDay)
				{
					list2.Add(businessHoursSchedule[i]);
				}
				else
				{
					list.Add(this.GetTimeRange(businessHoursSchedule[i]));
					t += businessHoursSchedule[i].Length;
				}
			}
			foreach (ScheduleInterval si in list2)
			{
				list.Add(this.GetTimeRange(si));
				t += si.Length;
			}
			if (t == BusinessHoursPrompt.SevenDays)
			{
				this.ClearStringBuilders(false);
				this.AddAlwaysOpenPrompt();
				return;
			}
			this.AddOurOpeningHoursStandardPrompt(list);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000C7C0 File Offset: 0x0000A9C0
		private void AddOurOpeningHoursPrompt(List<ScheduleGroup> scheduleGroups)
		{
			ArrayList prompts = GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
			{
				GlobCfg.DefaultPromptsForAA.OpeningHours
			});
			VariablePrompt<List<ScheduleGroup>>.SetActualPromptValues(prompts, "varScheduleGroupList", scheduleGroups);
			VariablePrompt<ExTimeZone>.SetActualPromptValues(prompts, "varTimeZone", this.GetTimeZone());
			base.AddPrompts(prompts);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000C814 File Offset: 0x0000AA14
		private void AddOurOpeningHoursStandardPrompt(List<TimeRange> timeRanges)
		{
			ArrayList prompts = GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
			{
				GlobCfg.DefaultPromptsForAA.OpeningHoursStandard
			});
			VariablePrompt<List<TimeRange>>.SetActualPromptValues(prompts, "varScheduleIntervalList", timeRanges);
			VariablePrompt<ExTimeZone>.SetActualPromptValues(prompts, "varTimeZone", this.GetTimeZone());
			base.AddPrompts(prompts);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000C868 File Offset: 0x0000AA68
		private void AddWeAreOpenEverydayPrompt(List<ScheduleGroup> scheduleGroups)
		{
			ArrayList prompts = GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
			{
				GlobCfg.DefaultPromptsForAA.Everyday
			});
			VariablePrompt<List<ScheduleGroup>>.SetActualPromptValues(prompts, "varScheduleGroupList", scheduleGroups);
			VariablePrompt<ExTimeZone>.SetActualPromptValues(prompts, "varTimeZone", this.GetTimeZone());
			base.AddPrompts(prompts);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000C8BB File Offset: 0x0000AABB
		private void ClearStringBuilders(bool smartReading)
		{
			base.SbSsml = new StringBuilder();
			base.SbLog = new StringBuilder();
			base.SbLog.AppendLine(smartReading ? "Smart" : "Standard");
		}

		// Token: 0x040000DC RID: 220
		internal static readonly TimeSpan Never = new TimeSpan(0L);

		// Token: 0x040000DD RID: 221
		internal static readonly TimeSpan SevenDays = new TimeSpan(7, 0, 0, 0);

		// Token: 0x040000DE RID: 222
		internal static readonly TimeSpan TwentyFourHours = new TimeSpan(24, 0, 0);

		// Token: 0x040000DF RID: 223
		private UMAutoAttendant aa;
	}
}
