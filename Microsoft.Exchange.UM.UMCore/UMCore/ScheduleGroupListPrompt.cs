using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001CC RID: 460
	internal class ScheduleGroupListPrompt : VariablePrompt<List<ScheduleGroup>>
	{
		// Token: 0x06000D6F RID: 3439 RVA: 0x0003BD08 File Offset: 0x00039F08
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"scheduleGroupList",
				base.Config.PromptName,
				string.Empty,
				base.SbLog.ToString()
			});
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0003BD58 File Offset: 0x00039F58
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ScheduleGroupListPrompt returning ssmlstring: {0}.", new object[]
			{
				base.SbSsml.ToString()
			});
			return base.SbSsml.ToString();
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0003BD98 File Offset: 0x00039F98
		protected override void InternalInitialize()
		{
			if (base.InitVal == null || base.InitVal.Count == 0)
			{
				return;
			}
			foreach (ScheduleGroup group in base.InitVal)
			{
				this.AddScheduleGroupPrompt(group);
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0003BE04 File Offset: 0x0003A004
		private void AddScheduleGroupPrompt(ScheduleGroup group)
		{
			ScheduleGroupPrompt scheduleGroupPrompt = new ScheduleGroupPrompt();
			scheduleGroupPrompt.Initialize(base.Config, base.Culture, group);
			base.AddPrompt(scheduleGroupPrompt);
			base.SbSsml.Append("<break time=\"400ms\"/>");
		}
	}
}
