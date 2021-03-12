using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001CD RID: 461
	internal class ScheduleGroupPrompt : VariablePrompt<ScheduleGroup>
	{
		// Token: 0x06000D74 RID: 3444 RVA: 0x0003BE4C File Offset: 0x0003A04C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"scheduleGroup",
				base.Config.PromptName,
				string.Empty,
				base.SbLog.ToString()
			});
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0003BE9C File Offset: 0x0003A09C
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ScheduleGroupPrompt returning ssmlstring: {0}.", new object[]
			{
				base.SbSsml.ToString()
			});
			return base.SbSsml.ToString();
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0003BEDC File Offset: 0x0003A0DC
		protected override void InternalInitialize()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ScheduleGroupPrompt initializing default SSML.", new object[0]);
			this.scheduleGroup = base.InitVal;
			string spokenScheduleGroupFormat = ScheduleGroupPrompt.GetSpokenScheduleGroupFormat(base.Culture);
			foreach (char c in spokenScheduleGroupFormat)
			{
				char c2 = c;
				if (c2 != 'd')
				{
					if (c2 == 'i')
					{
						using (List<TimeRange>.Enumerator enumerator = this.scheduleGroup.ScheduleIntervals.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								TimeRange si = enumerator.Current;
								this.AddScheduleIntervalPrompt(si);
							}
							goto IL_BA;
						}
					}
					CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, null, "ScheduleGroup ignoring invalided specifier '{0}'", new object[]
					{
						c
					});
				}
				else
				{
					this.AddDayOfWeekListPrompt();
				}
				IL_BA:;
			}
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0003BFC8 File Offset: 0x0003A1C8
		private static string GetSpokenScheduleGroupFormat(CultureInfo c)
		{
			return PromptConfigBase.PromptResourceManager.GetString(ScheduleGroupPrompt.SpokenScheduleGroupFormatId, c);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0003BFDC File Offset: 0x0003A1DC
		private void AddScheduleIntervalPrompt(TimeRange si)
		{
			ScheduleIntervalPrompt scheduleIntervalPrompt = new ScheduleIntervalPrompt(false);
			scheduleIntervalPrompt.Initialize(base.Config, base.Culture, si);
			base.AddPrompt(scheduleIntervalPrompt);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0003C00C File Offset: 0x0003A20C
		private void AddDayOfWeekListPrompt()
		{
			DayOfWeekListPrompt dayOfWeekListPrompt = new DayOfWeekListPrompt();
			dayOfWeekListPrompt.Initialize(base.Config, base.Culture, this.scheduleGroup.DaysOfWeek);
			base.AddPrompt(dayOfWeekListPrompt);
		}

		// Token: 0x04000A91 RID: 2705
		private static readonly string SpokenScheduleGroupFormatId = "SpokenScheduleGroupFormat";

		// Token: 0x04000A92 RID: 2706
		private ScheduleGroup scheduleGroup;
	}
}
