using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001CE RID: 462
	internal class ScheduleIntervalListPrompt : VariablePrompt<List<TimeRange>>
	{
		// Token: 0x06000D7C RID: 3452 RVA: 0x0003C058 File Offset: 0x0003A258
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"scheduleIntervalList",
				base.Config.PromptName,
				string.Empty,
				base.SbLog.ToString()
			});
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0003C0A8 File Offset: 0x0003A2A8
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ScheduleIntervalListPrompt returning ssmlstring: {0}.", new object[]
			{
				base.SbSsml.ToString()
			});
			return base.SbSsml.ToString();
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0003C0E8 File Offset: 0x0003A2E8
		protected override void InternalInitialize()
		{
			if (base.InitVal == null || base.InitVal.Count == 0)
			{
				return;
			}
			foreach (TimeRange s in base.InitVal)
			{
				this.AddScheduleIntervalPrompt(s);
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0003C154 File Offset: 0x0003A354
		private void AddScheduleIntervalPrompt(TimeRange s)
		{
			ScheduleIntervalPrompt scheduleIntervalPrompt = new ScheduleIntervalPrompt(true);
			scheduleIntervalPrompt.Initialize(base.Config, base.Culture, s);
			base.AddPrompt(scheduleIntervalPrompt);
			base.SbSsml.Append("<break time=\"400ms\"/>");
		}
	}
}
