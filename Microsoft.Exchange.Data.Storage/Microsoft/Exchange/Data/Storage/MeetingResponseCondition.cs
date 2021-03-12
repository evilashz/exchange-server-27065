using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B93 RID: 2963
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MeetingResponseCondition : FormsCondition
	{
		// Token: 0x06006A83 RID: 27267 RVA: 0x001C70A6 File Offset: 0x001C52A6
		private MeetingResponseCondition(Rule rule, string[] text) : base(ConditionType.MeetingResponseCondition, rule, text)
		{
		}

		// Token: 0x06006A84 RID: 27268 RVA: 0x001C70B4 File Offset: 0x001C52B4
		public static MeetingResponseCondition Create(Rule rule)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			string[] text = new string[]
			{
				"IPM.Schedule.Meeting.Resp.Pos",
				"IPM.Schedule.Meeting.Resp.Neg",
				"IPM.Schedule.Meeting.Resp.Tent"
			};
			return new MeetingResponseCondition(rule, text);
		}
	}
}
