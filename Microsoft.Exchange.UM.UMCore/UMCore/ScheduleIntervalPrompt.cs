using System;
using System.Collections;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001CF RID: 463
	internal class ScheduleIntervalPrompt : VariablePrompt<TimeRange>
	{
		// Token: 0x06000D81 RID: 3457 RVA: 0x0003C19B File Offset: 0x0003A39B
		public ScheduleIntervalPrompt(bool showFirstDayOfInterval)
		{
			this.showFirstDayOfInterval = showFirstDayOfInterval;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0003C1AC File Offset: 0x0003A3AC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"scheduleInterval",
				base.Config.PromptName,
				string.Empty,
				base.SbLog.ToString()
			});
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0003C1FC File Offset: 0x0003A3FC
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ScheduleIntervalPrompt returning ssmlstring: {0}.", new object[]
			{
				base.SbSsml.ToString()
			});
			return base.SbSsml.ToString();
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0003C23C File Offset: 0x0003A43C
		protected override void InternalInitialize()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ScheduleIntervalPrompt initializing default SSML.", new object[0]);
			ArrayList prompts = GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
			{
				GlobCfg.DefaultPromptsForAA.DayTimeRange
			});
			DayOfWeekTimeContext varValue = this.showFirstDayOfInterval ? DayOfWeekTimeContext.WithDayAndTime(base.InitVal.StartTime) : DayOfWeekTimeContext.WithTimeOnly(base.InitVal.StartTime);
			VariablePrompt<DayOfWeekTimeContext>.SetActualPromptValues(prompts, "startDayTime", varValue);
			DayOfWeekTimeContext varValue2 = this.showFirstDayOfInterval ? DayOfWeekTimeContext.WithDayAndTime(base.InitVal.EndTime) : DayOfWeekTimeContext.WithTimeOnly(base.InitVal.EndTime);
			VariablePrompt<DayOfWeekTimeContext>.SetActualPromptValues(prompts, "endDayTime", varValue2);
			base.AddPrompts(prompts);
		}

		// Token: 0x04000A93 RID: 2707
		private bool showFirstDayOfInterval;
	}
}
